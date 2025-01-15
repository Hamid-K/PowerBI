using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DsqGeneration;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Validation;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E5 RID: 229
	internal class SemanticQueryDataShapeCommandValidator : SemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x0001E5E5 File Offset: 0x0001C7E5
		private SemanticQueryDataShapeCommandValidator(DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext)
		{
			this._context = context;
			this._errorContext = errorContext;
			this._dataReductionValidator = new DataReductionValidator(this._context.FeatureSwitchProvider, this._errorContext);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001E617 File Offset: 0x0001C817
		internal static void Validate(DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext, SemanticQueryDataShapeCommand command)
		{
			new SemanticQueryDataShapeCommandValidator(context, errorContext).Visit(command);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001E628 File Offset: 0x0001C828
		protected override void Visit(SemanticQueryDataShapeCommand command)
		{
			if (command.Query == null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedSemanticQueryDataShapeCommand(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.MissingRequiredProperty("Query")));
				return;
			}
			if (!string.IsNullOrEmpty(command.DataSourceVariables) && !this._context.Model.Capabilities.SupportsDataSourceVariables)
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedDataSourceVariables(EngineMessageSeverity.Error));
			}
			this.ValidateScopedReductionInteractions(command);
			base.Visit(command);
			if (command.Query.Top != null)
			{
				DataShapeBinding binding = command.Binding;
				if (((binding != null) ? binding.DataReduction : null) != null)
				{
					this._errorContext.Register(DataShapeGenerationMessages.QueryTopWithDataReduction(EngineMessageSeverity.Error));
					return;
				}
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001E6E3 File Offset: 0x0001C8E3
		protected override void Visit(QueryExtensionSchema extensionSchema)
		{
			QueryExtensionSchemaValidator.IsValid(extensionSchema, this._errorContext, this._context.TelemetryInfo, this._context.Model, this._context.FeatureSwitchProvider);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001E714 File Offset: 0x0001C914
		protected override void Visit(QueryDefinition query)
		{
			if (!this._context.AllowQueryParameters && !query.Parameters.IsNullOrEmpty<QueryExpressionContainer>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureInSemanticQueryDataShapeCommand(EngineMessageSeverity.Error, "Parameters"));
			}
			DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(this._errorContext, DataShapeGenerationErrorCode.InvalidOrMalformedSemanticQueryDefinition, ErrorSourceCategory.MalformedExternalInput);
			new DataShapeGenerationQueryDefinitionValidator(new DataShapeGenerationQueryExpressionValidator(dataShapeGenerationErrorContextAdapter, this._errorContext, this._context.FeatureSwitchProvider, this._context.Model.Capabilities), this._errorContext, this._context.FeatureSwitchProvider).Visit(dataShapeGenerationErrorContextAdapter, query);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		protected override void Visit(DataShapeBinding binding)
		{
			new DataShapeBindingValidator(new DataShapeGenerationErrorContextAdapter(this._errorContext, DataShapeGenerationErrorCode.InvalidOrMalformedDataShapeBinding, ErrorSourceCategory.UserInput)).Validate(binding, null);
			bool flag = binding.Secondary != null;
			int num = 0;
			this.Visit(binding.Primary, true, flag, ref num);
			this.Visit(binding.Secondary, false, flag, ref num);
			this.ValidateDataShapeAggregates(binding.Aggregates, binding.Primary.Groupings.Count, flag, binding.Primary.Groupings.All(delegate(DataShapeBindingAxisGrouping g)
			{
				SubtotalType? subtotal = g.Subtotal;
				SubtotalType subtotalType = SubtotalType.None;
				return !((subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null));
			}), binding.Primary.Groupings.Any((DataShapeBindingAxisGrouping g) => g.ShowItemsWithNoData != null));
			this._dataReductionValidator.Validate(binding);
			this.ValidateHiddenProjections(binding);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001E894 File Offset: 0x0001CA94
		private void ValidateDataShapeAggregates(IList<DataShapeBindingAggregate> aggregates, int primaryGroupingCount, bool hasSecondary, bool hasSubtotal, bool hasShowAll)
		{
			if (aggregates.IsNullOrEmptyCollection<DataShapeBindingAggregate>())
			{
				return;
			}
			this.ValidateBindingAggregatesForScopedAggregates(null, primaryGroupingCount, aggregates, null, hasSubtotal, hasSecondary, hasShowAll);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		private void Visit(DataShapeBindingAxis axis, bool isPrimary, bool hasSecondary, ref int numberOfSyncBlocks)
		{
			if (axis == null)
			{
				return;
			}
			bool flag = axis.Groupings.Any((DataShapeBindingAxisGrouping g) => g.ShowItemsWithNoData != null);
			if (axis.Groupings != null)
			{
				for (int i = 0; i < axis.Groupings.Count; i++)
				{
					this.Visit(axis.Groupings[i], i, axis.Groupings.Count, isPrimary, hasSecondary, flag);
				}
			}
			this.ValidateExpansionState(axis.Expansion, axis.Groupings, isPrimary);
			this.ValidateSynchronization(axis.Synchronization, axis.Groupings, ref numberOfSyncBlocks);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001E978 File Offset: 0x0001CB78
		private void Visit(DataShapeBindingAxisGrouping grouping, int groupingIndex, int groupingsCount, bool isPrimary, bool hasSecondary, bool hasShowAll)
		{
			this.ValidateInstanceFilters(grouping.InstanceFilters, isPrimary);
			this.ValidateAxisGroupingAggregates(grouping, groupingIndex, groupingsCount, isPrimary, hasSecondary, hasShowAll);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001E997 File Offset: 0x0001CB97
		private void ValidateInstanceFilters(IList<FilterDefinition> instanceFilters, bool isPrimary)
		{
			if (instanceFilters.IsNullOrEmptyCollection<FilterDefinition>())
			{
				return;
			}
			if (!isPrimary)
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureOnContainer(EngineMessageSeverity.Error, "InstanceFilters", "Secondary Axis"));
				return;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		private void ValidateAxisGroupingAggregates(DataShapeBindingAxisGrouping grouping, int groupingIndex, int groupingsCount, bool isPrimary, bool hasSecondary, bool hasShowAll)
		{
			if (grouping.Aggregates == null)
			{
				return;
			}
			if (groupingIndex == groupingsCount - 1)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesOnLastGroupingMember()));
				return;
			}
			int? num = new int?(groupingIndex);
			IList<DataShapeBindingAggregate> aggregates = grouping.Aggregates;
			bool? flag = new bool?(isPrimary);
			SubtotalType? subtotal = grouping.Subtotal;
			SubtotalType subtotalType = SubtotalType.None;
			this.ValidateBindingAggregatesForScopedAggregates(num, groupingsCount, aggregates, flag, !((subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null)), hasSecondary, hasShowAll);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001EA34 File Offset: 0x0001CC34
		private void ValidateBindingAggregatesForScopedAggregates(int? groupingIndex, int primaryGroupingCount, IList<DataShapeBindingAggregate> aggregates, bool? isPrimary, bool hasSubtotal, bool hasSecondary, bool hasShowAll)
		{
			foreach (DataShapeBindingAggregate dataShapeBindingAggregate in aggregates)
			{
				if (!dataShapeBindingAggregate.Aggregations.IsNullOrEmptyCollection<DataShapeBindingAggregateContainer>())
				{
					foreach (DataShapeBindingAggregateContainer dataShapeBindingAggregateContainer in dataShapeBindingAggregate.Aggregations)
					{
						if (dataShapeBindingAggregateContainer.Scope != null)
						{
							if (!this._context.Model.Capabilities.SupportsScopedAggregates)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregates()));
								return;
							}
							if (isPrimary != null && !isPrimary.Value)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesNotOnDataShapeOrPrimaryAxis()));
								return;
							}
							if (hasSecondary)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithSecondaryAxis()));
								return;
							}
							if (!hasSubtotal)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithNoTotals()));
								return;
							}
							if (hasShowAll)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithShowAll()));
								return;
							}
							if (dataShapeBindingAggregateContainer.Scope.SecondaryDepth != null)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithSecondaryDepth()));
								return;
							}
							if (!dataShapeBindingAggregateContainer.RespectInstanceFilters)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithoutRespectingInstanceFilters()));
								return;
							}
							if (dataShapeBindingAggregateContainer.Median != null || dataShapeBindingAggregateContainer.Percentile != null)
							{
								this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedAggregatesForScopedAggregates()));
								return;
							}
							if (dataShapeBindingAggregateContainer.Scope.PrimaryDepth > 0 && dataShapeBindingAggregateContainer.Scope.PrimaryDepth <= primaryGroupingCount)
							{
								if (groupingIndex == null)
								{
									continue;
								}
								int primaryDepth = dataShapeBindingAggregateContainer.Scope.PrimaryDepth;
								int? num = groupingIndex + 1;
								if (!((primaryDepth < num.GetValueOrDefault()) & (num != null)))
								{
									continue;
								}
							}
							this._errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidScopedAggregatePrimaryDepth()));
							return;
						}
					}
				}
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001ECBC File Offset: 0x0001CEBC
		private void ValidateExpansionState(DataShapeBindingAxisExpansionState expansionState, IList<DataShapeBindingAxisGrouping> groupings, bool isPrimary)
		{
			if (expansionState == null)
			{
				return;
			}
			if (!isPrimary)
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureOnContainer(EngineMessageSeverity.Error, "Expansion", "Secondary Axis"));
				return;
			}
			this.ValidateExpansionInstance(expansionState.Instances);
			this.ValidateExpansionLevels(expansionState.Levels);
			if (groupings.Any(delegate(DataShapeBindingAxisGrouping group)
			{
				SubtotalType? subtotal = group.Subtotal;
				SubtotalType subtotalType = SubtotalType.None;
				return (subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null);
			}))
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedExpansionWithoutTotalsInSemanticQueryDataShapeCommand(EngineMessageSeverity.Error));
				return;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001ED44 File Offset: 0x0001CF44
		private void ValidateExpansionLevels(IList<DataShapeBindingAxisExpansionLevel> levels)
		{
			if (levels.IsNullOrEmpty<DataShapeBindingAxisExpansionLevel>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpansionState(DataShapeGenerationErrorCode.InvalidExpansionStateMissingExpansionLevels, DataShapeGenerationMessagePhrases.MissingExpansionLevels()));
				return;
			}
			for (int i = 0; i < levels.Count; i++)
			{
				if (levels[i].Expressions.IsNullOrEmpty<QueryExpressionContainer>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidExpansionState(DataShapeGenerationErrorCode.InvalidExpansionStateInvalidExpansionLevel, DataShapeGenerationMessagePhrases.InvalidExpansionLevel(i)));
				}
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		private void ValidateExpansionInstance(DataShapeBindingAxisExpansionInstance instances)
		{
			if (instances == null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpansionState(DataShapeGenerationErrorCode.InvalidExpansionStateMissingExpansionInstances, DataShapeGenerationMessagePhrases.MissingExpansionInstances()));
				return;
			}
			if (instances.Values.IsNullOrEmpty<QueryExpressionContainer>() && instances.Children.IsNullOrEmpty<DataShapeBindingAxisExpansionInstance>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpansionState(DataShapeGenerationErrorCode.InvalidExpansionStateEmptyExpansionInstances, DataShapeGenerationMessagePhrases.EmptyExpansionInstances()));
				return;
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001EE10 File Offset: 0x0001D010
		private void ValidateSynchronization(IList<DataShapeBindingAxisSynchronizedGroupingBlock> synchronization, IList<DataShapeBindingAxisGrouping> axisGroupings, ref int numberOfSyncBlocks)
		{
			if (synchronization.IsNullOrEmptyCollection<DataShapeBindingAxisSynchronizedGroupingBlock>())
			{
				return;
			}
			if (!this._context.Model.Capabilities.SupportsGroupSynchronization)
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedGroupSynchronization(EngineMessageSeverity.Error));
				return;
			}
			numberOfSyncBlocks += synchronization.Count;
			if (numberOfSyncBlocks > 10)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidGroupSynchronization(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedNumberOfSynchronizationBlocks(numberOfSyncBlocks, 10)));
				return;
			}
			int count = axisGroupings.Count;
			foreach (DataShapeBindingAxisSynchronizedGroupingBlock dataShapeBindingAxisSynchronizedGroupingBlock in synchronization)
			{
				int count2 = dataShapeBindingAxisSynchronizedGroupingBlock.Groupings.Count;
				if (count2 > 10)
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidGroupSynchronization(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedGroupSynchronizationNumberOfGroups(count2, 10)));
					break;
				}
				int? num = null;
				for (int i = 0; i < count2; i++)
				{
					int num2 = dataShapeBindingAxisSynchronizedGroupingBlock.Groupings[i];
					if (num2 == 0 || num2 >= count)
					{
						this._errorContext.Register(DataShapeGenerationMessages.InvalidGroupSynchronization(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedGroupSynchronizationIndex(num2)));
						return;
					}
					if (i != 0 && num.Value != num2 - 1)
					{
						this._errorContext.Register(DataShapeGenerationMessages.InvalidGroupSynchronization(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedGroupSynchronizationIndices(dataShapeBindingAxisSynchronizedGroupingBlock.Groupings)));
						return;
					}
					if (axisGroupings[num2].Subtotal != null)
					{
						SubtotalType? subtotal = axisGroupings[num2].Subtotal;
						SubtotalType subtotalType = SubtotalType.None;
						if (!((subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null)))
						{
							this._errorContext.Register(DataShapeGenerationMessages.InvalidGroupSynchronization(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedGroupSynchronizationWithTotals()));
							return;
						}
					}
					num = new int?(num2);
				}
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001EFDC File Offset: 0x0001D1DC
		private void ValidateScopedReductionInteractions(SemanticQueryDataShapeCommand command)
		{
			QueryDefinition query = command.Query;
			if (query != null && !query.GroupBy.IsNullOrEmpty<QueryExpressionContainer>())
			{
				DataShapeBinding binding = command.Binding;
				bool flag;
				if (binding == null)
				{
					flag = false;
				}
				else
				{
					DataReduction dataReduction = binding.DataReduction;
					bool? flag2 = ((dataReduction != null) ? new bool?(dataReduction.Scoped.IsNullOrEmptyCollection<ScopedDataReduction>()) : null);
					bool flag3 = false;
					flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				}
				if (flag)
				{
					this._errorContext.Register(DataShapeGenerationMessages.GroupByNotSupportedWithScopedDataReduction(EngineMessageSeverity.Error));
				}
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001F05D File Offset: 0x0001D25D
		private void ValidateHiddenProjections(DataShapeBinding binding)
		{
			if (binding.HiddenProjections.IsNullOrEmptyCollection<DataShapeBindingHiddenProjections>())
			{
				return;
			}
			if (!this._context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.VisualCalculations))
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedHiddenProjection(EngineMessageSeverity.Error));
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001F094 File Offset: 0x0001D294
		internal static bool TryAddIndices(DataShapeBindingAxis axis, HashSet<int> indices)
		{
			if (axis == null)
			{
				return true;
			}
			bool flag;
			SemanticQueryDataShapeCommandValidator.AddIndices(axis, indices, out flag, true);
			return !flag;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		internal static void AddIndices(DataShapeBindingAxis axis, HashSet<int> indices, out bool hasDuplicates, bool exitOnDuplicate)
		{
			hasDuplicates = false;
			if (axis == null)
			{
				return;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			if (groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return;
			}
			for (int i = 0; i < groupings.Count; i++)
			{
				IList<int> projections = groupings[i].Projections;
				if (!projections.IsNullOrEmptyCollection<int>())
				{
					for (int j = 0; j < projections.Count; j++)
					{
						if (!indices.Add(projections[j]))
						{
							hasDuplicates = true;
							if (exitOnDuplicate)
							{
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x0400041E RID: 1054
		private const int MaxNumberOfSynchronizationBlocksOrGroupsInSynchronization = 10;

		// Token: 0x0400041F RID: 1055
		private const string DefaultRootQueryName = null;

		// Token: 0x04000420 RID: 1056
		private readonly DataShapeGenerationContext _context;

		// Token: 0x04000421 RID: 1057
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000422 RID: 1058
		private readonly DataReductionValidator _dataReductionValidator;
	}
}
