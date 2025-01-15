using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000099 RID: 153
	internal class QueryBindingProjectionGenerator
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x000154DD File Offset: 0x000136DD
		private QueryBindingProjectionGenerator(QueryProjectionsBuilder builder, QueryTranslationContext context, QuerySortGenerator sort)
		{
			this._builder = builder;
			this._context = context;
			this._sort = sort;
			this._hasProcessed = new BitArray(this._context.QueryDefinition.Select.Count);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001551A File Offset: 0x0001371A
		internal static bool TryGenerateProjections(QueryProjectionsBuilder builder, QueryTranslationContext context, QuerySortGenerator sort)
		{
			return new QueryBindingProjectionGenerator(builder, context, sort).TryGenerateProjections();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001552C File Offset: 0x0001372C
		private bool TryGenerateProjections()
		{
			DataShapeBinding dataShapeBinding = this._context.DataShapeBinding;
			QueryMeasureCalculationGenerator queryMeasureCalculationGenerator = QueryMeasureCalculationGenerator.Parse(dataShapeBinding);
			bool flag = dataShapeBinding.Primary.HasSubtotals() || dataShapeBinding.Secondary.HasSubtotals();
			bool flag2 = dataShapeBinding.ShouldGenerateRestartIdentities();
			return this.TryAddMeasuresFromAxes(queryMeasureCalculationGenerator, flag) && this.TryAddAxis(dataShapeBinding.Primary, queryMeasureCalculationGenerator, true, flag2) && this.TryAddAxis(dataShapeBinding.Secondary, queryMeasureCalculationGenerator, false, flag2) && this.TryValidateVisualShapeAndAddInactiveProjections(flag) && this.TryAddAggregates(dataShapeBinding.Primary, dataShapeBinding.Aggregates) && this.TryAddDataShapeProjections();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000155D0 File Offset: 0x000137D0
		private bool TryAddMeasuresFromAxes(QueryMeasureCalculationGenerator measureGen, bool hasSubtotal)
		{
			DataShapeBinding dataShapeBinding = this._context.DataShapeBinding;
			return this.TryAddMeasures(dataShapeBinding.Primary, measureGen, hasSubtotal, AllowedExpressionContent.TopLevelQuerySelect) && this.TryAddMeasures(dataShapeBinding.Secondary, measureGen, hasSubtotal, AllowedExpressionContent.TopLevelQuerySelect);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015618 File Offset: 0x00013818
		private bool TryAddMeasures(DataShapeBindingAxis axis, QueryMeasureCalculationGenerator measureGen, bool hasSubtotal, AllowedExpressionContentFlags allowedContent)
		{
			if (axis == null)
			{
				return true;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			DataShapeBinding dataShapeBinding = this._context.DataShapeBinding;
			IReadOnlyList<ResolvedQuerySelect> select = this._context.QueryDefinition.Select;
			for (int i = 0; i < groupings.Count; i++)
			{
				IList<int> projections = groupings[i].Projections;
				for (int j = 0; j < projections.Count; j++)
				{
					int num = projections[j];
					if (num < 0 || num >= select.Count)
					{
						this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, null));
						return false;
					}
					if (!this._hasProcessed[num])
					{
						ResolvedQuerySelect resolvedQuerySelect = select[num];
						bool flag = dataShapeBinding.IsSuppressedJoinPredicate(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.SuppressedJoinPredicatesByName);
						bool flag2 = dataShapeBinding.IsHiddenProjection(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.HiddenProjections);
						ProjectedDsqExpression projectedDsqExpression;
						IReadOnlyList<ResolvedQueryFilter> readOnlyList;
						if (SemanticQueryProjectionGenerator.TryExtractProjectionMeasure(this._context, new int?(num), resolvedQuerySelect.NativeReferenceName, resolvedQuerySelect.Expression, flag, flag2, allowedContent, out projectedDsqExpression, out readOnlyList) && !measureGen.IsGroupMeasureCalc(num))
						{
							SemanticQueryProjectionGenerator.PrepareMeasureForUseInQuery(resolvedQuerySelect.Expression, projectedDsqExpression, this._context.Expressions);
							if (hasSubtotal)
							{
								projectedDsqExpression.Aggregates.Add(DsqExpressionSubtotalAggregate.Instance);
							}
							if (!readOnlyList.IsNullOrEmpty<ResolvedQueryFilter>())
							{
								this._builder.AddFilters(readOnlyList, num);
							}
							this._builder.AddMeasure(projectedDsqExpression);
							this._sort.Rebind(projectedDsqExpression);
							this._hasProcessed[num] = true;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000157F8 File Offset: 0x000139F8
		private bool TryAddAxis(DataShapeBindingAxis axis, QueryMeasureCalculationGenerator measureGen, bool isPrimary, bool generateRestartIdentities)
		{
			if (axis == null)
			{
				return true;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			int count = groupings.Count;
			for (int i = 0; i < count; i++)
			{
				DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping = groupings[i];
				IList<int> projections = dataShapeBindingAxisGrouping.Projections;
				IList<int> list = dataShapeBindingAxisGrouping.ShowItemsWithNoData ?? Util.EmptyReadOnlyCollection<int>();
				QueryMemberBuilder queryMemberBuilder = new QueryMemberBuilder(this._context.Expressions, this._context.SharedContext.ErrorContext, this._sort, dataShapeBindingAxisGrouping.InstanceFilters, isPrimary, dataShapeBindingAxisGrouping.Subtotal.Value, this._context.SourceRefContext, new QueryGroupBuilderOptions(false, false, false, false, generateRestartIdentities, axis.ShouldTrackNonMeasureSortKeysForReferencing(i)), false);
				if (!this.TryAddProjections(queryMemberBuilder, dataShapeBindingAxisGrouping, measureGen, projections, list.AsReadOnlyList<int>(), AllowedExpressionContent.TopLevelQuerySelect))
				{
					return false;
				}
				if (!this.TryAddGroupBy(queryMemberBuilder, dataShapeBindingAxisGrouping))
				{
					return false;
				}
				if (queryMemberBuilder.HasValues)
				{
					if (isPrimary)
					{
						this._builder.AddPrimaryMember(queryMemberBuilder);
					}
					else
					{
						this._builder.AddSecondaryMember(queryMemberBuilder);
					}
				}
				else if (queryMemberBuilder.HasDetailIdentity)
				{
					this._context.SharedContext.Tracer.SanitizedTrace(TraceLevel.Warning, "Query has a GroupBy attached to a group with no projected columns.  The GroupBy is ignored.");
				}
			}
			return true;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00015928 File Offset: 0x00013B28
		private bool TryAddProjections(QueryMemberBuilder groupBuilder, DataShapeBindingAxisGrouping grouping, QueryMeasureCalculationGenerator measureGen, IList<int> projections, IReadOnlyList<int> showItemsWithNoData, AllowedExpressionContentFlags allowedContent)
		{
			DataShapeBinding dataShapeBinding = this._context.DataShapeBinding;
			IReadOnlyList<ResolvedQuerySelect> select = this._context.QueryDefinition.Select;
			for (int i = 0; i < projections.Count; i++)
			{
				int num = projections[i];
				if (!this._hasProcessed[num])
				{
					ResolvedQuerySelect resolvedQuerySelect = select[num];
					if (groupBuilder.TryAddProjection(resolvedQuerySelect.Expression, num, resolvedQuerySelect.NativeReferenceName, showItemsWithNoData.Contains(num)))
					{
						this._hasProcessed[num] = true;
					}
					else
					{
						bool flag = dataShapeBinding.IsSuppressedJoinPredicate(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.SuppressedJoinPredicatesByName);
						bool flag2 = dataShapeBinding.IsHiddenProjection(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.HiddenProjections);
						ProjectedDsqExpression projectedDsqExpression;
						IReadOnlyList<ResolvedQueryFilter> readOnlyList;
						if (!SemanticQueryProjectionGenerator.TryExtractProjectionMeasure(this._context, new int?(num), resolvedQuerySelect.NativeReferenceName, resolvedQuerySelect.Expression, flag, flag2, allowedContent, out projectedDsqExpression, out readOnlyList))
						{
							this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.CannotProcessSelectExpression(EngineMessageSeverity.Error, num));
							return false;
						}
						if (!readOnlyList.IsNullOrEmpty<ResolvedQueryFilter>())
						{
							this._builder.AddFilters(readOnlyList, num);
						}
						if (measureGen.ShouldGroupProjectMeasure(grouping, num))
						{
							SemanticQueryProjectionGenerator.PrepareMeasureForUseInQuery(resolvedQuerySelect.Expression, projectedDsqExpression, this._context.Expressions);
							groupBuilder.AddMeasureCalculation(projectedDsqExpression);
						}
						this._hasProcessed[num] = true;
					}
				}
			}
			return true;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015AC0 File Offset: 0x00013CC0
		private bool TryAddGroupBy(QueryMemberBuilder groupBuilder, DataShapeBindingAxisGrouping grouping)
		{
			IReadOnlyList<ResolvedQueryExpression> groupBy = this._context.QueryDefinition.GroupBy;
			IList<int> groupBy2 = grouping.GroupBy;
			if (groupBy2 != null)
			{
				if (this._context.Annotations.AnyQueryHasVisualCalculationExpressions())
				{
					this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualCalculationWithGroupBy(EngineMessageSeverity.Error));
					return false;
				}
				for (int i = 0; i < groupBy2.Count; i++)
				{
					int num = groupBy2[i];
					ResolvedQueryExpression resolvedQueryExpression = groupBy[num];
					if (!groupBuilder.TryAddGroupBy(resolvedQueryExpression, num))
					{
						this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedGroupByInSemanticQuery(EngineMessageSeverity.Error, num));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00015B64 File Offset: 0x00013D64
		private bool TryAddDataShapeProjections()
		{
			DataShapeBinding dataShapeBinding = this._context.DataShapeBinding;
			if (dataShapeBinding.Projections.IsNullOrEmptyCollection<int>())
			{
				return true;
			}
			IList<int> projections = dataShapeBinding.Projections;
			IReadOnlyList<ResolvedQuerySelect> select = this._context.QueryDefinition.Select;
			for (int i = 0; i < projections.Count; i++)
			{
				int num = projections[i];
				if (!this._hasProcessed[num])
				{
					ResolvedQuerySelect resolvedQuerySelect = select[num];
					bool flag = dataShapeBinding.IsSuppressedJoinPredicate(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.SuppressedJoinPredicatesByName);
					bool flag2 = dataShapeBinding.IsHiddenProjection(this._context.QueryDefinition.Name, num, this._context.QueryDefinition.Select, this._context.HiddenProjections);
					ProjectedDsqExpression projectedDsqExpression;
					IReadOnlyList<ResolvedQueryFilter> readOnlyList;
					if (!SemanticQueryProjectionGenerator.TryExtractProjectionMeasure(this._context, new int?(num), resolvedQuerySelect.NativeReferenceName, resolvedQuerySelect.Expression, flag, flag2, AllowedExpressionContent.TopLevelQuerySelect, out projectedDsqExpression, out readOnlyList))
					{
						this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedProjectionIndexInSemanticQuery(EngineMessageSeverity.Error, num));
						return false;
					}
					if (!readOnlyList.IsNullOrEmpty<ResolvedQueryFilter>())
					{
						this._builder.AddFilters(readOnlyList, num);
					}
					SemanticQueryProjectionGenerator.PrepareMeasureForUseInQuery(resolvedQuerySelect.Expression, projectedDsqExpression, this._context.Expressions);
					this._builder.AddDataShapeProjection(projectedDsqExpression);
					this._hasProcessed[num] = true;
				}
			}
			return true;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00015CE8 File Offset: 0x00013EE8
		private bool TryValidateVisualShapeAndAddInactiveProjections(bool hasExistingSubtotals)
		{
			if (!this._context.Annotations.QueryHasVisualCalculationsExpressions(this._context.QueryDefinition.Name))
			{
				return true;
			}
			if (!this._context.QueryDefinition.VisualShape.IsNullOrEmpty<ResolvedQueryAxis>())
			{
				Dictionary<ResolvedQueryExpression, int> dictionary = (from kvp in this._context.QueryDefinition.Select.Select((ResolvedQuerySelect @select, int index) => new global::System.ValueTuple<ResolvedQueryExpression, int>(@select.Expression, index))
					group kvp by kvp.Item1 into item
					select item.FirstOrDefault<global::System.ValueTuple<ResolvedQueryExpression, int>>()).ToDictionary(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "index" })] global::System.ValueTuple<ResolvedQueryExpression, int> kvp) => kvp.Item1, ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "index" })] global::System.ValueTuple<ResolvedQueryExpression, int> kvp) => kvp.Item2);
				bool flag = false;
				int num = 0;
				foreach (ResolvedQueryAxis resolvedQueryAxis in this._context.QueryDefinition.VisualShape)
				{
					int num2 = -1;
					QueryBindingProjectionGenerator.AxisMatchState axisMatchState = QueryBindingProjectionGenerator.AxisMatchState.None;
					bool flag2 = false;
					bool? flag3 = null;
					for (int i = 0; i < resolvedQueryAxis.Groups.Count; i++)
					{
						ResolvedQueryAxisGroup resolvedQueryAxisGroup = resolvedQueryAxis.Groups[i];
						int num3 = -1;
						if (flag3 == null)
						{
							flag3 = new bool?(resolvedQueryAxisGroup.Subtotal);
						}
						else
						{
							bool? flag4 = flag3;
							bool subtotal = resolvedQueryAxisGroup.Subtotal;
							if (!((flag4.GetValueOrDefault() == subtotal) & (flag4 != null)))
							{
								this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedInconsistentTotalsInVisualShapeAxis(EngineMessageSeverity.Error, resolvedQueryAxis.Name));
								return false;
							}
						}
						int num4 = ((num2 == -1) ? num2 : (num2 + 1));
						if (!flag2)
						{
							if ((axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.None || axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.Primary) && this.TryGetMemberIndexWithProjectedExpressions(this._builder.PrimaryMembers, resolvedQueryAxisGroup.Keys, num4, out num3))
							{
								axisMatchState = QueryBindingProjectionGenerator.AxisMatchState.Primary;
							}
							if ((axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.None || axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.Secondary) && this.TryGetMemberIndexWithProjectedExpressions(this._builder.SecondaryMembers, resolvedQueryAxisGroup.Keys, num4, out num3))
							{
								axisMatchState = QueryBindingProjectionGenerator.AxisMatchState.Secondary;
							}
						}
						if (num3 < 0)
						{
							if (i == 0)
							{
								this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedUnprojectedFirstVisualShapeAxisGroup(EngineMessageSeverity.Error));
								return false;
							}
							if (num2 + 1 != ((axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.Primary) ? this._builder.PrimaryMemberCount : this._builder.SecondaryMemberCount))
							{
								this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedIntermediateUnprojectedGroup(EngineMessageSeverity.Error));
								return false;
							}
							if (!this.TryInjectContextOnlyMember(resolvedQueryAxisGroup, axisMatchState, num2 + 1, resolvedQueryAxis.Name, dictionary))
							{
								return false;
							}
							num2++;
							num++;
							flag2 = true;
							flag = true;
						}
						else
						{
							QueryMemberBuilder queryMemberBuilder = ((axisMatchState == QueryBindingProjectionGenerator.AxisMatchState.Primary) ? this._builder.PrimaryMembers[num3] : this._builder.SecondaryMembers[num3]);
							if (!resolvedQueryAxisGroup.Subtotal && queryMemberBuilder.Group.HasSubtotal)
							{
								this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupTotals(EngineMessageSeverity.Error));
								return false;
							}
							if (resolvedQueryAxisGroup.Subtotal && !queryMemberBuilder.Group.HasSubtotal)
							{
								queryMemberBuilder.Group.SetIsSubtotalContextOnly();
							}
							num2 = num3;
							num++;
						}
					}
				}
				if (num != this._builder.PrimaryMemberCount + this._builder.SecondaryMemberCount)
				{
					this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeDataMemberMismatch(EngineMessageSeverity.Error));
					return false;
				}
				if (!hasExistingSubtotals && flag)
				{
					foreach (ProjectedDsqExpression projectedDsqExpression in this._builder.Measures)
					{
						projectedDsqExpression.Aggregates.Add(DsqExpressionSubtotalAggregate.Instance);
					}
				}
				return true;
			}
			if (this._builder.PrimaryMemberCount != 0 || this._builder.SecondaryMemberCount != 0)
			{
				this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedMissingVisualShape(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001616C File Offset: 0x0001436C
		private bool TryGetMemberIndexWithProjectedExpressions(IList<QueryMemberBuilder> members, IReadOnlyList<ResolvedQueryExpression> expressions, int expectedIndex, out int matchingMemberIndex)
		{
			matchingMemberIndex = -1;
			if (members.IsNullOrEmpty<QueryMemberBuilder>())
			{
				return false;
			}
			if (expectedIndex != -1)
			{
				if (expectedIndex >= members.Count)
				{
					return false;
				}
				if (members[expectedIndex].ProjectsAllExpressions(expressions))
				{
					matchingMemberIndex = expectedIndex;
					return true;
				}
			}
			else
			{
				for (int i = 0; i < members.Count; i++)
				{
					if (members[i].ProjectsAllExpressions(expressions))
					{
						matchingMemberIndex = i;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000161D4 File Offset: 0x000143D4
		private bool TryInjectContextOnlyMember(ResolvedQueryAxisGroup group, QueryBindingProjectionGenerator.AxisMatchState matchHierarchy, int insertIndex, string axisName, Dictionary<ResolvedQueryExpression, int> selectExpressionToIndex)
		{
			QueryMemberBuilder queryMemberBuilder = new QueryMemberBuilder(this._context.Expressions, this._context.SharedContext.ErrorContext, this._sort, null, matchHierarchy == QueryBindingProjectionGenerator.AxisMatchState.Primary, SubtotalType.After, this._context.SourceRefContext, QueryGroupBuilderOptions.AllDisabledOptions, true);
			foreach (ResolvedQueryExpression resolvedQueryExpression in group.Keys)
			{
				int num;
				if (!selectExpressionToIndex.TryGetValue(resolvedQueryExpression, out num))
				{
					this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupKeyWithoutCorrespondingSelect(EngineMessageSeverity.Error, axisName, resolvedQueryExpression));
					return false;
				}
				ResolvedQuerySelect resolvedQuerySelect = this._context.QueryDefinition.Select[num];
				if (!queryMemberBuilder.TryAddProjection(resolvedQuerySelect.Expression, num, resolvedQuerySelect.NativeReferenceName, false))
				{
					this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupKeyExpressionType(EngineMessageSeverity.Error, resolvedQuerySelect.Expression.GetType().Name));
					return false;
				}
			}
			if (matchHierarchy == QueryBindingProjectionGenerator.AxisMatchState.Primary)
			{
				this._builder.PrimaryMembers.Insert(insertIndex, queryMemberBuilder);
			}
			else
			{
				this._builder.SecondaryMembers.Insert(insertIndex, queryMemberBuilder);
			}
			DataShapeGenerationTelemetry telemetry = this._context.SharedContext.Telemetry;
			int numCollapsedGroups = telemetry.NumCollapsedGroups;
			telemetry.NumCollapsedGroups = numCollapsedGroups + 1;
			return true;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00016340 File Offset: 0x00014540
		private bool TryAddAggregates(DataShapeBindingAxis primaryAxis, IList<DataShapeBindingAggregate> dataShapeLevelAggregates)
		{
			IList<DataShapeBindingAxisGrouping> groupings = primaryAxis.Groupings;
			if (dataShapeLevelAggregates.IsNullOrEmptyCollection<DataShapeBindingAggregate>())
			{
				if (groupings.All((DataShapeBindingAxisGrouping g) => g.Aggregates == null))
				{
					return true;
				}
			}
			int count = groupings.Count;
			for (int i = 0; i < this._builder.MeasureCount; i++)
			{
				for (int j = 0; j < count; j++)
				{
					DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping = groupings[j];
					if (dataShapeBindingAxisGrouping.Aggregates != null && !this.TryApplyAggregates(this._builder.Measures[i], dataShapeBindingAxisGrouping.Aggregates, new int?(j)))
					{
						return false;
					}
				}
				if (!this.TryApplyAggregates(this._builder.Measures[i], dataShapeLevelAggregates, null))
				{
					return false;
				}
			}
			for (int k = 0; k < this._builder.PrimaryMemberCount; k++)
			{
				for (int l = 0; l < count; l++)
				{
					DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping2 = groupings[l];
					if (dataShapeBindingAxisGrouping2.Aggregates != null)
					{
						if (this._builder.PrimaryMembers[k].IsNonAggregatable)
						{
							this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAggregate(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedScopedAggregatesWithNonAggregatableColumn()));
							return false;
						}
						if (!this.TryApplyAggregates(this._builder.PrimaryMembers[k], dataShapeBindingAxisGrouping2.Aggregates, new int?(l)))
						{
							return false;
						}
					}
				}
				if (!this.TryApplyAggregates(this._builder.PrimaryMembers[k], dataShapeLevelAggregates, null))
				{
					return false;
				}
			}
			for (int m = 0; m < this._builder.SecondaryMemberCount; m++)
			{
				if (!this.TryApplyAggregates(this._builder.SecondaryMembers[m], dataShapeLevelAggregates, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00016524 File Offset: 0x00014724
		private bool TryApplyAggregates(QueryMemberBuilder group, IList<DataShapeBindingAggregate> aggregates, int? groupingIndex)
		{
			foreach (QueryGroupValueBuilder queryGroupValueBuilder in group.ValueBuilders)
			{
				if (!queryGroupValueBuilder.IsIdentityOnly && !this.TryApplyAggregates(queryGroupValueBuilder.GetProjectedDsqExpression(), aggregates, groupingIndex))
				{
					return false;
				}
			}
			foreach (ProjectedDsqExpression projectedDsqExpression in group.MeasureCalculations)
			{
				if (!this.TryApplyAggregates(projectedDsqExpression, aggregates, groupingIndex))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000165D0 File Offset: 0x000147D0
		private bool TryApplyAggregates(ProjectedDsqExpression expr, IList<DataShapeBindingAggregate> aggregates, int? groupingIndex)
		{
			for (int i = 0; i < aggregates.Count; i++)
			{
				DataShapeBindingAggregate dataShapeBindingAggregate = aggregates[i];
				if (QueryUtils.SelectIndexReferencesProjectedExpression(dataShapeBindingAggregate.Select, expr))
				{
					if (expr.IsContextOnly)
					{
						this._context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidAggregateOfHiddenProjection(EngineMessageSeverity.Error));
						return false;
					}
					QueryBindingProjectionGenerator.AddDsqExpressionAggregates(expr.Aggregates, dataShapeBindingAggregate.Aggregations, groupingIndex, dataShapeBindingAggregate.Select);
				}
			}
			return true;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00016644 File Offset: 0x00014844
		private static void AddDsqExpressionAggregates(DsqExpressionAggregates dsqExpressionAggregates, IList<DataShapeBindingAggregateContainer> aggregations, int? groupingIndex, int selectIndex)
		{
			foreach (DataShapeBindingAggregateContainer dataShapeBindingAggregateContainer in aggregations)
			{
				if (dataShapeBindingAggregateContainer.Average != null)
				{
					dsqExpressionAggregates.Add(new DsqExpressionAverageAggregate(dataShapeBindingAggregateContainer, new int?(selectIndex), groupingIndex));
				}
				else if (dataShapeBindingAggregateContainer.Max != null)
				{
					dsqExpressionAggregates.Add(new DsqExpressionMaxAggregate(dataShapeBindingAggregateContainer, new int?(selectIndex), groupingIndex));
				}
				else if (dataShapeBindingAggregateContainer.Min != null)
				{
					dsqExpressionAggregates.Add(new DsqExpressionMinAggregate(dataShapeBindingAggregateContainer, new int?(selectIndex), groupingIndex));
				}
				else if (dataShapeBindingAggregateContainer.Median != null)
				{
					dsqExpressionAggregates.Add(new DsqExpressionMedianAggregate(dataShapeBindingAggregateContainer, new int?(selectIndex), groupingIndex));
				}
				else if (dataShapeBindingAggregateContainer.Percentile != null)
				{
					dsqExpressionAggregates.Add(new DsqExpressionPercentileAggregate(dataShapeBindingAggregateContainer, new int?(selectIndex), groupingIndex));
				}
			}
		}

		// Token: 0x0400032B RID: 811
		private readonly QueryProjectionsBuilder _builder;

		// Token: 0x0400032C RID: 812
		private readonly QueryTranslationContext _context;

		// Token: 0x0400032D RID: 813
		private readonly QuerySortGenerator _sort;

		// Token: 0x0400032E RID: 814
		private BitArray _hasProcessed;

		// Token: 0x02000142 RID: 322
		private enum AxisMatchState
		{
			// Token: 0x04000517 RID: 1303
			None,
			// Token: 0x04000518 RID: 1304
			Primary,
			// Token: 0x04000519 RID: 1305
			Secondary
		}
	}
}
