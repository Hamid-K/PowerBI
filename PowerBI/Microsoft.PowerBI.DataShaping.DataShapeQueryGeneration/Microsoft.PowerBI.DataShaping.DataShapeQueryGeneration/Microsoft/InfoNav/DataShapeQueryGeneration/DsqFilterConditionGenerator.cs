using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005D RID: 93
	[ImmutableObject(false)]
	internal sealed class DsqFilterConditionGenerator : DefaultResolvedQueryExpressionVisitor<GeneratedFilterCondition>, IDsqFilterConditionGenerator
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000F148 File Offset: 0x0000D348
		internal DsqFilterConditionGenerator(DsqExpressionGenerator exprBuilder, IFeatureSwitchProvider featureSwitchProvider, DataShapeGenerationErrorContext errorContext, DataShapeGenerationTelemetry telemetry, DsqFilterConditionGenerationOptions generationOptions, QuerySourceExpressionReferenceContext sourceRefContext)
		{
			this._exprBuilder = exprBuilder;
			this._featureSwitchProvider = featureSwitchProvider;
			this._errorContext = errorContext;
			this._generationOptions = generationOptions;
			this._parentExpressions = new Stack<ResolvedQueryExpression>();
			this._nonCollapsibleConditions = new List<GeneratedFilterCondition>();
			this._telemetry = telemetry;
			this._sourceRefContext = sourceRefContext;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public List<GeneratedFilterCondition> Generate(ResolvedQueryExpression expr, ExpressionContext expressionContext, IReadOnlyList<ResolvedQueryExpression> targets, out FilterUsageKind filterUsageKind)
		{
			this._expressionContent = ExpressionContent.None;
			this._expressionContext = expressionContext;
			this._currentTargets = targets ?? Util.EmptyReadOnlyCollection<ResolvedQueryExpression>();
			this._implicitTargetsEncountered = null;
			GeneratedFilterCondition generatedFilterCondition = this.VisitExpression(expr);
			generatedFilterCondition = this.ApplySlicerTargets(targets, generatedFilterCondition);
			this.PreserveCorrelation(generatedFilterCondition, expr);
			List<GeneratedFilterCondition> list = new List<GeneratedFilterCondition>();
			list.Add(generatedFilterCondition);
			if (this._nonCollapsibleConditions.Count > 0)
			{
				list.AddRange(this._nonCollapsibleConditions);
				this._nonCollapsibleConditions.Clear();
			}
			this._implicitTargetsEncountered = null;
			this._currentTargets = null;
			this._expressionContext = null;
			filterUsageKind = this.AssignFilterUsageKind();
			return list;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F23C File Offset: 0x0000D43C
		private FilterUsageKind AssignFilterUsageKind()
		{
			FilterUsageKind filterUsageKind = FilterUsageKind.Default;
			if (this._expressionContent.HasFlag(ExpressionContent.ModelReference) && this._expressionContent.HasFlag(ExpressionContent.SubqueryReference))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilterSourceReference(EngineMessageSeverity.Error));
			}
			else if ((this._expressionContent & ExpressionContent.ModelReference) == ExpressionContent.ModelReference)
			{
				filterUsageKind = FilterUsageKind.Default;
			}
			else if ((this._expressionContent & ExpressionContent.SubqueryReference) == ExpressionContent.SubqueryReference)
			{
				filterUsageKind = FilterUsageKind.SubqueryOutput;
			}
			return filterUsageKind;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		private GeneratedFilterCondition ApplySlicerTargets(IReadOnlyList<ResolvedQueryExpression> targets, GeneratedFilterCondition filterCondition)
		{
			if (filterCondition.ConversionStatus == FilterConversionStatus.Succeeded && !targets.IsNullOrEmpty<ResolvedQueryExpression>())
			{
				DsqFilterType? dsqFilterType = filterCondition.FilterType;
				DsqFilterType dsqFilterType2 = DsqFilterType.Context;
				if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
				{
					dsqFilterType = filterCondition.FilterType;
					dsqFilterType2 = DsqFilterType.Scope;
					if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
					{
						dsqFilterType = filterCondition.FilterType;
						dsqFilterType2 = DsqFilterType.Exist;
						if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
						{
							dsqFilterType = filterCondition.FilterType;
							dsqFilterType2 = DsqFilterType.AnyValue;
							if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
							{
								dsqFilterType = filterCondition.FilterType;
								dsqFilterType2 = DsqFilterType.AnyValueDefaultValueOverridesAncestors;
								if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
								{
									dsqFilterType = filterCondition.FilterType;
									dsqFilterType2 = DsqFilterType.DefaultValue;
									if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2) & (dsqFilterType != null)))
									{
										TargetMatchStatus? targetMatchStatus = null;
										if (!this._implicitTargetsEncountered.IsNullOrEmptyCollection<ResolvedQueryExpression>())
										{
											targetMatchStatus = new TargetMatchStatus?(this.CompareTargets(targets, this._implicitTargetsEncountered));
										}
										DataShapeGenerationTelemetry telemetry = this._telemetry;
										SlicerTargetsTelemetry slicerTargetsTelemetry = new SlicerTargetsTelemetry();
										slicerTargetsTelemetry.ExplicitTargetCount = targets.Count;
										HashSet<ResolvedQueryExpression> implicitTargetsEncountered = this._implicitTargetsEncountered;
										slicerTargetsTelemetry.ImplicitTargetCount = ((implicitTargetsEncountered != null) ? implicitTargetsEncountered.Count : 0);
										slicerTargetsTelemetry.FilterType = filterCondition.FilterType;
										slicerTargetsTelemetry.ExplicitTargetMatchStatus = targetMatchStatus;
										telemetry.AddSlicerTargetsTelemetry(slicerTargetsTelemetry);
									}
								}
							}
						}
					}
				}
			}
			return filterCondition;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000F408 File Offset: 0x0000D608
		private TargetMatchStatus CompareTargets(IReadOnlyList<ResolvedQueryExpression> targets, ISet<ResolvedQueryExpression> implicitTargetsEncountered)
		{
			if (targets.Any((ResolvedQueryExpression t) => t is ResolvedQuerySourceRefExpression))
			{
				Func<ResolvedQueryExpression, ResolvedQueryExpression> func;
				if ((func = DsqFilterConditionGenerator.<>O.<0>__GetSourceRefExpressionOrDefault) == null)
				{
					func = (DsqFilterConditionGenerator.<>O.<0>__GetSourceRefExpressionOrDefault = new Func<ResolvedQueryExpression, ResolvedQueryExpression>(DsqFilterConditionGenerator.GetSourceRefExpressionOrDefault));
				}
				ReadOnlySet<ResolvedQueryExpression> readOnlySet = implicitTargetsEncountered.Select(func).ToReadOnlySet(null);
				return this.CompareTargetsImpl(targets, readOnlySet);
			}
			return this.CompareTargetsImpl(targets, implicitTargetsEncountered);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F475 File Offset: 0x0000D675
		private TargetMatchStatus CompareTargetsImpl(IReadOnlyList<ResolvedQueryExpression> targets, ICollection<ResolvedQueryExpression> implicitTargetsEncountered)
		{
			if (targets.SetEquals(implicitTargetsEncountered))
			{
				return TargetMatchStatus.FullMatch;
			}
			if (targets.IsSubsetOf(implicitTargetsEncountered))
			{
				return TargetMatchStatus.Subset;
			}
			if (targets.IsSupersetOf(implicitTargetsEncountered))
			{
				return TargetMatchStatus.Superset;
			}
			if (targets.Intersect(implicitTargetsEncountered).Any<ResolvedQueryExpression>())
			{
				return TargetMatchStatus.DisjointSet;
			}
			return TargetMatchStatus.NoMatch;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F4A9 File Offset: 0x0000D6A9
		private static ResolvedQueryExpression GetSourceRefExpressionOrDefault(ResolvedQueryExpression t)
		{
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = t as ResolvedQueryColumnExpression;
			return (((resolvedQueryColumnExpression != null) ? resolvedQueryColumnExpression.Expression : null) as ResolvedQuerySourceRefExpression) ?? t;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public override GeneratedFilterCondition Visit(ResolvedQueryNotExpression expression)
		{
			ResolvedQueryExpression childExpression = expression.Expression;
			ResolvedQueryAndExpression resolvedQueryAndExpression = childExpression as ResolvedQueryAndExpression;
			ResolvedQueryOrExpression resolvedQueryOrExpression = childExpression as ResolvedQueryOrExpression;
			ResolvedQueryInExpression inExpression = childExpression as ResolvedQueryInExpression;
			GeneratedFilterCondition filterCondition;
			if (resolvedQueryAndExpression != null || resolvedQueryOrExpression != null)
			{
				this._parentExpressions.Push(expression);
				if (resolvedQueryAndExpression != null)
				{
					filterCondition = this.CreateBinaryLogicalOperation(resolvedQueryAndExpression, CompoundFilterOperator.NotAll);
				}
				else
				{
					Contract.RetailAssert(resolvedQueryOrExpression != null, "Either andExpression or orExpression must not be null.");
					filterCondition = this.CreateBinaryLogicalOperation(resolvedQueryOrExpression, CompoundFilterOperator.NotAny);
				}
				this._parentExpressions.Pop();
				return filterCondition;
			}
			if (inExpression != null)
			{
				filterCondition = GeneratedFilterCondition.Empty;
				this.GenerateWithParentTracking<FilterConversionStatus>(expression, delegate
				{
					filterCondition = this.Visit(inExpression, true);
					return filterCondition.ConversionStatus;
				});
				return filterCondition;
			}
			ResolvedQueryNotExpression resolvedQueryNotExpression = childExpression as ResolvedQueryNotExpression;
			if (resolvedQueryNotExpression != null)
			{
				childExpression = resolvedQueryNotExpression.Expression;
			}
			filterCondition = this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.VisitExpression(childExpression));
			if (filterCondition.ConversionStatus != FilterConversionStatus.Succeeded)
			{
				return filterCondition;
			}
			if (resolvedQueryNotExpression != null)
			{
				return filterCondition;
			}
			return this.Negate(filterCondition);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F620 File Offset: 0x0000D820
		private GeneratedFilterCondition Negate(GeneratedFilterCondition singleChildCondition)
		{
			if (singleChildCondition.Condition == null)
			{
				return singleChildCondition;
			}
			BinaryFilterCondition binaryFilterCondition = singleChildCondition.Condition as BinaryFilterCondition;
			UnaryFilterCondition unaryFilterCondition = singleChildCondition.Condition as UnaryFilterCondition;
			if (binaryFilterCondition != null)
			{
				binaryFilterCondition.Not = this.Invert(binaryFilterCondition.Not);
				return singleChildCondition;
			}
			if (unaryFilterCondition != null)
			{
				unaryFilterCondition.Not = this.Invert(unaryFilterCondition.Not);
				return singleChildCondition;
			}
			return new GeneratedFilterCondition(new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.NotAny,
				Conditions = new List<FilterCondition>(1) { singleChildCondition.Condition }
			}, singleChildCondition.ConversionStatus, singleChildCondition.FilterType);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private void PreserveCorrelation(GeneratedFilterCondition generatedCondition, ResolvedQueryExpression expression)
		{
			if (generatedCondition.Condition == null)
			{
				return;
			}
			CompoundFilterCondition compoundFilterCondition = generatedCondition.Condition as CompoundFilterCondition;
			if (compoundFilterCondition == null)
			{
				return;
			}
			List<FilterCondition> conditions = compoundFilterCondition.Conditions;
			if ((conditions == null || conditions.Count != 1) && this._implicitTargetsEncountered != null && this.HasMultipleEntityReferences(this._implicitTargetsEncountered))
			{
				if (!(expression is ResolvedQueryAndExpression))
				{
					if (expression is ResolvedQueryNotExpression && compoundFilterCondition.Operator == CompoundFilterOperator.All)
					{
						this._telemetry.RecordFilterCorrelation(true);
					}
					return;
				}
				this._telemetry.RecordFilterCorrelation(false);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F757 File Offset: 0x0000D957
		private Candidate<bool> Invert(Candidate<bool> candidate)
		{
			return candidate != true;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F76C File Offset: 0x0000D96C
		public override GeneratedFilterCondition Visit(ResolvedQueryAndExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryLogicalOperation(expression, CompoundFilterOperator.All));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public override GeneratedFilterCondition Visit(ResolvedQueryOrExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryLogicalOperation(expression, CompoundFilterOperator.Any));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public override GeneratedFilterCondition Visit(ResolvedQueryComparisonExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryFilterCondition(expression, this.ConvertOperator(expression.ComparisonKind)));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F820 File Offset: 0x0000DA20
		public override GeneratedFilterCondition Visit(ResolvedQueryStartsWithExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryFilterCondition(expression, BinaryFilterOperator.StartsWith));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public override GeneratedFilterCondition Visit(ResolvedQueryEndsWithExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryFilterCondition(expression, BinaryFilterOperator.EndsWith));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000F898 File Offset: 0x0000DA98
		public override GeneratedFilterCondition Visit(ResolvedQueryContainsExpression expression)
		{
			return this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateBinaryFilterCondition(expression, BinaryFilterOperator.Contains));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		public override GeneratedFilterCondition Visit(ResolvedQueryInExpression expression)
		{
			return this.Visit(expression, false);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F8DB File Offset: 0x0000DADB
		private GeneratedFilterCondition Visit(ResolvedQueryInExpression expression, bool negate)
		{
			if (expression.HasValues)
			{
				return this.GenerateInValuesFilter(expression, negate);
			}
			return this.GenerateInTableFilter(expression, negate);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		private GeneratedFilterCondition GenerateInValuesFilter(ResolvedQueryInExpression expression, bool negate)
		{
			if (expression.Values.Count > 30000)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilterExceedsMaxNumberOfValuesForInFilter(EngineMessageSeverity.Error, expression.Values.Count, 30000));
				return GeneratedFilterCondition.Empty;
			}
			if (this.ShouldFlatten(expression))
			{
				if (expression.Values.Count > 500)
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidFilterExceedsMaxNumberOfValuesForInFilter(EngineMessageSeverity.Error, expression.Values.Count, 500));
					return GeneratedFilterCondition.Empty;
				}
				bool useIdentityEqualsComparisons = this._useIdentityEqualsComparisons;
				QueryEqualitySemanticsKind? equalityKind = expression.EqualityKind;
				QueryEqualitySemanticsKind queryEqualitySemanticsKind = QueryEqualitySemanticsKind.Identity;
				this._useIdentityEqualsComparisons = (equalityKind.GetValueOrDefault() == queryEqualitySemanticsKind) & (equalityKind != null);
				GeneratedFilterCondition generatedFilterCondition = this.VisitExpression(this.FlattenInExpression(expression, negate));
				this._useIdentityEqualsComparisons = useIdentityEqualsComparisons;
				return generatedFilterCondition;
			}
			else
			{
				GeneratedFilterCondition generatedFilterCondition2 = this.GenerateWithParentTracking<GeneratedFilterCondition>(expression, () => this.CreateInFilterCondition(expression));
				if (!negate)
				{
					return generatedFilterCondition2;
				}
				return this.Negate(generatedFilterCondition2);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000FA20 File Offset: 0x0000DC20
		private GeneratedFilterCondition GenerateInTableFilter(ResolvedQueryInExpression expression, bool negate)
		{
			ResolvedQueryExpressionSourceRefExpression resolvedQueryExpressionSourceRefExpression = expression.Table as ResolvedQueryExpressionSourceRefExpression;
			if (resolvedQueryExpressionSourceRefExpression == null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidTableArgumentToInOperatorInFilter(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			IIntermediateTableSchema intermediateTableSchema;
			if (this._sourceRefContext == null || !this._sourceRefContext.TryGetSourceSchema(resolvedQueryExpressionSourceRefExpression.SourceName, out intermediateTableSchema))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidTableArgumentToInOperatorInFilter(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			IntermediateDataShapeTableSchema intermediateDataShapeTableSchema = intermediateTableSchema as IntermediateDataShapeTableSchema;
			if (intermediateDataShapeTableSchema != null)
			{
				return this.GenerateInTableFilterAsApplyFilter(expression, intermediateDataShapeTableSchema);
			}
			IntermediateExpressionTableSchema intermediateExpressionTableSchema = intermediateTableSchema as IntermediateExpressionTableSchema;
			if (intermediateExpressionTableSchema == null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidTableArgumentToInOperatorInFilter(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			GeneratedFilterCondition generatedFilterCondition = this.GenerateInTableFilter(expression, intermediateExpressionTableSchema);
			if (!negate)
			{
				return generatedFilterCondition;
			}
			return this.Negate(generatedFilterCondition);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000FADC File Offset: 0x0000DCDC
		private GeneratedFilterCondition GenerateInTableFilter(ResolvedQueryInExpression expression, IntermediateExpressionTableSchema tableSchema)
		{
			if (expression.Expressions.Count != tableSchema.Type.RowType.Columns.Count)
			{
				this._errorContext.Register(DataShapeGenerationMessages.MismatchedArgumentCountsToInOperatorTableType(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			bool flag;
			List<Expression> list;
			if (!this.TryGenerateInExpressions(expression, out flag, out list))
			{
				return GeneratedFilterCondition.Empty;
			}
			if (!flag && this.HasMultipleEntityReferences(expression.Expressions))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidInTableTypeFilterConditionMultipleEntities(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			if (!flag && this._generationOptions.SuppressSlicersAndApplyFilters)
			{
				return GeneratedFilterCondition.Ignored;
			}
			InFilterCondition inFilterCondition = new InFilterCondition
			{
				Expressions = list,
				Table = tableSchema.ReferenceExpression,
				IdentityComparison = true
			};
			return this.BuildFilterCondition(flag, inFilterCondition);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		private bool IsValidSchemaForInTableApplyFilter<T>(ResolvedQueryInExpression expression, IReadOnlyList<T> schemaItems, Func<T, IConceptualColumn> getSchemaItemColumn)
		{
			IReadOnlyList<ResolvedQueryExpression> expressions = expression.Expressions;
			if (schemaItems == null || expressions.Count > schemaItems.Count)
			{
				this._errorContext.Register(DataShapeGenerationMessages.MismatchedArgumentCountsToInOperator(EngineMessageSeverity.Error));
				return false;
			}
			for (int i = 0; i < expressions.Count; i++)
			{
				ResolvedQueryColumnExpression resolvedQueryColumnExpression = expressions[i] as ResolvedQueryColumnExpression;
				IConceptualColumn conceptualColumn = getSchemaItemColumn(schemaItems[i]);
				if (resolvedQueryColumnExpression == null || conceptualColumn == null)
				{
					this._errorContext.Register(DataShapeGenerationMessages.NonColumnArgumentToInOperatorInFilter(EngineMessageSeverity.Error));
					return false;
				}
				if (resolvedQueryColumnExpression.Column != conceptualColumn)
				{
					this._errorContext.Register(DataShapeGenerationMessages.MismatchedColumnArgumentsToInOperatorInFilter(EngineMessageSeverity.Error));
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000FC40 File Offset: 0x0000DE40
		private GeneratedFilterCondition GenerateInTableFilterAsApplyFilter(ResolvedQueryInExpression expression, IntermediateDataShapeTableSchema schema)
		{
			if (!this.IsValidSchemaForInTableApplyFilter<IntermediateTableSchemaColumn>(expression, schema.Columns, (IntermediateTableSchemaColumn schemaColumn) => ((schemaColumn != null) ? schemaColumn.LineageProperty : null) as IConceptualColumn))
			{
				return GeneratedFilterCondition.Empty;
			}
			if (this._generationOptions.SuppressSlicersAndApplyFilters)
			{
				return GeneratedFilterCondition.Ignored;
			}
			return new GeneratedFilterCondition(new ApplyFilterCondition
			{
				DataShapeReference = schema.ReferenceExpression
			}, FilterConversionStatus.Succeeded, new DsqFilterType?(DsqFilterType.Apply));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		private bool ShouldFlatten(ResolvedQueryInExpression expression)
		{
			if (!this._generationOptions.FlattenNegatedTupleInFilters)
			{
				return false;
			}
			if (!this.HasMultipleEntityReferences(expression.Expressions))
			{
				return false;
			}
			if (this.IsContainedInNegation())
			{
				InFilterFlatteningTelemetry inTelemetry = this.InTelemetry;
				int multiEntityNegated = inTelemetry.MultiEntityNegated;
				inTelemetry.MultiEntityNegated = multiEntityNegated + 1;
				return true;
			}
			return false;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000FD04 File Offset: 0x0000DF04
		private InFilterFlatteningTelemetry InTelemetry
		{
			get
			{
				if (this._telemetry.InFlattening == null)
				{
					this._telemetry.InFlattening = new InFilterFlatteningTelemetry();
				}
				return this._telemetry.InFlattening;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FD30 File Offset: 0x0000DF30
		private bool HasMultipleEntityReferences(IEnumerable<ResolvedQueryExpression> expressions)
		{
			HashSet<IConceptualEntity> hashSet = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			foreach (ResolvedQueryExpression resolvedQueryExpression in expressions)
			{
				IConceptualEntity entitySource = this.GetEntitySource(resolvedQueryExpression);
				if (entitySource != null)
				{
					hashSet.Add(entitySource);
				}
				if (hashSet.Count > 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		private IConceptualEntity GetEntitySource(ResolvedQueryExpression expression)
		{
			IConceptualEntity conceptualEntity;
			if (QueryExpressionResolver.TryGetSourceEntity(expression, out conceptualEntity))
			{
				return conceptualEntity;
			}
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = expression as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression != null)
			{
				return resolvedQueryPropertyExpression.Property.Entity;
			}
			return null;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000FDDA File Offset: 0x0000DFDA
		private bool IsContainedInNegation()
		{
			if (this._parentExpressions.Count == 0)
			{
				return false;
			}
			return this._parentExpressions.Count((ResolvedQueryExpression p) => p is ResolvedQueryNotExpression) % 2 == 1;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000FE1C File Offset: 0x0000E01C
		private GeneratedFilterCondition CreateBinaryFilterCondition(ResolvedQueryBinaryExpression expression, BinaryFilterOperator @operator)
		{
			GeneratedDsqExpression generatedDsqExpression;
			if (!this.TryGenerateExpression(expression.Left, out generatedDsqExpression))
			{
				return GeneratedFilterCondition.Empty;
			}
			ExpressionNode expression2 = generatedDsqExpression.Expression;
			bool hasAggregate = generatedDsqExpression.HasAggregate;
			Util.AddToLazySet<ResolvedQueryExpression>(ref this._implicitTargetsEncountered, expression.Left, null);
			if (DsqFilterConditionGenerator.IsAnyOrDefaultFilterCondition(expression))
			{
				return this.CreateAnyOrDefaultValueFilterCondition(expression, expression2, @operator);
			}
			GeneratedDsqExpression generatedDsqExpression2;
			if (!this.TryGenerateExpression(expression.Right, out generatedDsqExpression2))
			{
				this.RegisterFilterComparisonError(expression.Left, expression.Right, new Action<IConceptualProperty, ResolvedQueryExpression>(this.RegisterBinaryFilterError));
				return GeneratedFilterCondition.Empty;
			}
			ExpressionNode expression3 = generatedDsqExpression2.Expression;
			bool hasAggregate2 = generatedDsqExpression2.HasAggregate;
			bool flag = hasAggregate || hasAggregate2;
			BinaryFilterCondition binaryFilterCondition = new BinaryFilterCondition
			{
				Operator = @operator,
				LeftExpression = expression2,
				RightExpression = expression3
			};
			return this.BuildFilterCondition(flag, binaryFilterCondition);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		private void RegisterFilterComparisonError(ResolvedQueryExpression targetExpression, ResolvedQueryExpression otherExpression, Action<IConceptualProperty, ResolvedQueryExpression> defaultErrorAction)
		{
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = targetExpression as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression != null)
			{
				IConceptualProperty property = resolvedQueryPropertyExpression.Property;
				if (otherExpression is ResolvedQueryDateSpanExpression || otherExpression is ResolvedQueryDateAddExpression)
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidFilterComparisonIncompatibleExpressions(EngineMessageSeverity.Error, property, otherExpression));
					return;
				}
				defaultErrorAction(property, otherExpression);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FF44 File Offset: 0x0000E144
		private void RegisterBinaryFilterError(IConceptualProperty target, ResolvedQueryExpression otherExpression)
		{
			this._errorContext.Register(DataShapeGenerationMessages.InvalidBinaryFilterConditionExpression(EngineMessageSeverity.Error, target, otherExpression));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000FF5C File Offset: 0x0000E15C
		private ResolvedQueryExpression FlattenInExpression(ResolvedQueryInExpression expression, bool negate)
		{
			IReadOnlyList<ResolvedQueryExpression> expressions = expression.Expressions;
			ResolvedQueryExpression resolvedQueryExpression = null;
			for (int i = 0; i < expression.Values.Count; i++)
			{
				ResolvedQueryExpression resolvedQueryExpression2 = null;
				IReadOnlyList<ResolvedQueryExpression> readOnlyList = expression.Values[i];
				for (int j = 0; j < expressions.Count; j++)
				{
					ResolvedQueryComparisonExpression resolvedQueryComparisonExpression = expressions[j].Comparison(readOnlyList[j], QueryComparisonKind.Equal);
					if (j == 0)
					{
						resolvedQueryExpression2 = resolvedQueryComparisonExpression;
					}
					else
					{
						resolvedQueryExpression2 = resolvedQueryExpression2.And(resolvedQueryComparisonExpression);
					}
				}
				if (i == 0)
				{
					resolvedQueryExpression = resolvedQueryExpression2;
				}
				else
				{
					resolvedQueryExpression = resolvedQueryExpression.Or(resolvedQueryExpression2);
				}
			}
			if (!negate)
			{
				return resolvedQueryExpression;
			}
			return new ResolvedQueryNotExpression(resolvedQueryExpression);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		private GeneratedFilterCondition CreateInFilterCondition(ResolvedQueryInExpression expression)
		{
			bool flag;
			List<Expression> list;
			if (!this.TryGenerateInExpressions(expression, out flag, out list))
			{
				return GeneratedFilterCondition.Empty;
			}
			List<List<Expression>> list2 = new List<List<Expression>>(expression.Values.Count);
			for (int i = 0; i < expression.Values.Count; i++)
			{
				IReadOnlyList<ResolvedQueryExpression> readOnlyList = expression.Values[i];
				List<Expression> list3 = new List<Expression>(readOnlyList.Count);
				foreach (ResolvedQueryExpression resolvedQueryExpression in readOnlyList)
				{
					GeneratedDsqExpression generatedDsqExpression;
					if (!this.TryGenerateExpression(resolvedQueryExpression, out generatedDsqExpression))
					{
						return GeneratedFilterCondition.Empty;
					}
					list3.Add(generatedDsqExpression.Expression);
					flag |= generatedDsqExpression.HasAggregate;
				}
				list2.Add(list3);
			}
			InFilterCondition inFilterCondition = new InFilterCondition();
			inFilterCondition.Expressions = list;
			inFilterCondition.Values = list2;
			QueryEqualitySemanticsKind? equalityKind = expression.EqualityKind;
			QueryEqualitySemanticsKind queryEqualitySemanticsKind = QueryEqualitySemanticsKind.Identity;
			inFilterCondition.IdentityComparison = (equalityKind.GetValueOrDefault() == queryEqualitySemanticsKind) & (equalityKind != null);
			InFilterCondition inFilterCondition2 = inFilterCondition;
			return this.BuildFilterCondition(flag, inFilterCondition2);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00010118 File Offset: 0x0000E318
		private bool TryGenerateInExpressions(ResolvedQueryInExpression expression, out bool isScopeFilter, out List<Expression> generatedExpressions)
		{
			isScopeFilter = false;
			generatedExpressions = new List<Expression>(expression.Expressions.Count);
			foreach (ResolvedQueryExpression resolvedQueryExpression in expression.Expressions)
			{
				GeneratedDsqExpression generatedDsqExpression;
				if (!this.TryGenerateExpression(resolvedQueryExpression, out generatedDsqExpression))
				{
					return false;
				}
				Util.AddToLazySet<ResolvedQueryExpression>(ref this._implicitTargetsEncountered, resolvedQueryExpression, null);
				bool hasAggregate = generatedDsqExpression.HasAggregate;
				ExpressionNode expression2 = generatedDsqExpression.Expression;
				generatedExpressions.Add(expression2);
				isScopeFilter = isScopeFilter || hasAggregate;
			}
			return true;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000101B8 File Offset: 0x0000E3B8
		private GeneratedFilterCondition BuildFilterCondition(bool isScopeFilter, FilterCondition condition)
		{
			DsqFilterType dsqFilterType = (isScopeFilter ? DsqFilterType.Scope : DsqFilterType.DataShape);
			return GeneratedFilterCondition.CreateSucceeded(condition, new DsqFilterType?(dsqFilterType));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000101D9 File Offset: 0x0000E3D9
		private static bool IsAnyOrDefaultFilterCondition(ResolvedQueryBinaryExpression expression)
		{
			return expression.Right is ResolvedQueryAnyValueExpression || expression.Right is ResolvedQueryDefaultValueExpression;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000101F8 File Offset: 0x0000E3F8
		private GeneratedFilterCondition CreateAnyOrDefaultValueFilterCondition(ResolvedQueryBinaryExpression expression, ExpressionNode generatedTarget, BinaryFilterOperator @operator)
		{
			Contract.Check(DsqFilterConditionGenerator.IsAnyOrDefaultFilterCondition(expression), "Cannot a create AnyValue or DefaultValue FilterCondition with specified parameters");
			if (this._parentExpressions.Count != 1)
			{
				if (this._parentExpressions.Except(expression).Any((ResolvedQueryExpression e) => !(e is ResolvedQueryAndExpression)))
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidAnyValueOrDefaultValue(EngineMessageSeverity.Error));
					return GeneratedFilterCondition.Empty;
				}
			}
			if (@operator != BinaryFilterOperator.Equal)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidBinaryOperatorForAnyValueOrDefaultValue(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = expression.Left as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression == null || !(resolvedQueryPropertyExpression.Property is IConceptualColumn))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilterTargetForAnyValueOrDefaultValue(EngineMessageSeverity.Error));
				return GeneratedFilterCondition.Empty;
			}
			FilterConversionStatus filterConversionStatus = FilterConversionStatus.Ignored;
			if (expression.Right is ResolvedQueryAnyValueExpression)
			{
				return this.GenerateAnyValueFilterCondition((ResolvedQueryAnyValueExpression)expression.Right, expression.Left, generatedTarget);
			}
			if (expression.Right is ResolvedQueryDefaultValueExpression)
			{
				DefaultValueFilterCondition defaultValueFilterCondition = null;
				if (this._defaultValueBuilder == null)
				{
					defaultValueFilterCondition = new DefaultValueFilterCondition();
					this._defaultValueBuilder = new DefaultValueFilterConditionBuilder<IDsqFilterConditionGenerator>(this, defaultValueFilterCondition);
					filterConversionStatus = FilterConversionStatus.Succeeded;
				}
				this._defaultValueBuilder.WithTarget(generatedTarget);
				return new GeneratedFilterCondition(defaultValueFilterCondition, filterConversionStatus, new DsqFilterType?(DsqFilterType.DefaultValue));
			}
			return GeneratedFilterCondition.Empty;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010338 File Offset: 0x0000E538
		private GeneratedFilterCondition GenerateAnyValueFilterCondition(ResolvedQueryAnyValueExpression anyValueExpression, ResolvedQueryExpression target, ExpressionNode generatedTarget)
		{
			AnyValueFilterCondition anyValueFilterCondition = null;
			DsqFilterType? dsqFilterType = null;
			FilterConversionStatus filterConversionStatus = FilterConversionStatus.Ignored;
			if (anyValueExpression.DefaultValueOverridesAncestors)
			{
				if (this._anyValueWithAncestorOverridingBuilder == null)
				{
					anyValueFilterCondition = new AnyValueFilterCondition
					{
						DefaultValueOverridesAncestors = true
					};
					this._anyValueWithAncestorOverridingBuilder = new AnyValueFilterConditionBuilder<IDsqFilterConditionGenerator>(this, anyValueFilterCondition);
					filterConversionStatus = FilterConversionStatus.Succeeded;
				}
				this._anyValueWithAncestorOverridingBuilder.WithTarget(generatedTarget);
				dsqFilterType = new DsqFilterType?(DsqFilterType.AnyValueDefaultValueOverridesAncestors);
				QueryMemberBuilder queryMemberBuilder = new QueryMemberBuilder(this._exprBuilder, this._errorContext, QuerySortGenerator.CreateEmptySortGenerator(), null, true, SubtotalType.None, this._sourceRefContext, QueryGroupBuilderOptions.AllDisabledOptions, false);
				queryMemberBuilder.TryAddProjection(target, 0, null, false);
				using (IEnumerator<QueryGroupKey> enumerator = queryMemberBuilder.ToMember().Group.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QueryGroupKey queryGroupKey = enumerator.Current;
						if (!generatedTarget.Equals(queryGroupKey.Expression))
						{
							this._anyValueWithAncestorOverridingBuilder.WithTarget(queryGroupKey.Expression);
						}
					}
					goto IL_0116;
				}
			}
			if (this._anyValueBuilder == null)
			{
				anyValueFilterCondition = new AnyValueFilterCondition();
				this._anyValueBuilder = new AnyValueFilterConditionBuilder<IDsqFilterConditionGenerator>(this, anyValueFilterCondition);
				filterConversionStatus = FilterConversionStatus.Succeeded;
			}
			this._anyValueBuilder.WithTarget(generatedTarget);
			dsqFilterType = new DsqFilterType?(DsqFilterType.AnyValue);
			IL_0116:
			return new GeneratedFilterCondition(anyValueFilterCondition, filterConversionStatus, dsqFilterType);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010474 File Offset: 0x0000E674
		private BinaryFilterOperator ConvertOperator(QueryComparisonKind comparisonKind)
		{
			BinaryFilterOperator binaryFilterOperator = BinaryFilterOperator.Equal;
			switch (comparisonKind)
			{
			case QueryComparisonKind.Equal:
				binaryFilterOperator = (this._useIdentityEqualsComparisons ? BinaryFilterOperator.EqualIdentity : BinaryFilterOperator.Equal);
				break;
			case QueryComparisonKind.GreaterThan:
				binaryFilterOperator = BinaryFilterOperator.GreaterThan;
				break;
			case QueryComparisonKind.GreaterThanOrEqual:
				binaryFilterOperator = BinaryFilterOperator.GreaterThanOrEqual;
				break;
			case QueryComparisonKind.LessThan:
				binaryFilterOperator = BinaryFilterOperator.LessThan;
				break;
			case QueryComparisonKind.LessThanOrEqual:
				binaryFilterOperator = BinaryFilterOperator.LessThanOrEqual;
				break;
			}
			return binaryFilterOperator;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000104C0 File Offset: 0x0000E6C0
		private GeneratedFilterCondition CreateBinaryLogicalOperation(ResolvedQueryBinaryExpression expression, CompoundFilterOperator @operator)
		{
			ResolvedQueryExpression resolvedQueryExpression = expression.Left;
			ResolvedQueryExpression resolvedQueryExpression2 = expression.Right;
			if (@operator == CompoundFilterOperator.NotAll || @operator == CompoundFilterOperator.NotAny)
			{
				@operator = ((@operator == CompoundFilterOperator.NotAll) ? CompoundFilterOperator.Any : CompoundFilterOperator.All);
				resolvedQueryExpression = new ResolvedQueryNotExpression(resolvedQueryExpression);
				resolvedQueryExpression2 = new ResolvedQueryNotExpression(resolvedQueryExpression2);
			}
			GeneratedFilterCondition generatedFilterCondition = this.VisitExpression(resolvedQueryExpression);
			if (generatedFilterCondition.ConversionStatus == FilterConversionStatus.Failed)
			{
				return generatedFilterCondition;
			}
			GeneratedFilterCondition generatedFilterCondition2 = this.VisitExpression(resolvedQueryExpression2);
			if (generatedFilterCondition2.ConversionStatus == FilterConversionStatus.Failed)
			{
				return generatedFilterCondition2;
			}
			GeneratedFilterCondition generatedFilterCondition3;
			if (this.CompoundConditionAlreadyHandled(generatedFilterCondition, generatedFilterCondition2, out generatedFilterCondition3))
			{
				return generatedFilterCondition3;
			}
			DsqFilterType? dsqFilterType;
			if (!this.TryGetFilterType(generatedFilterCondition.FilterType, generatedFilterCondition2.FilterType, out dsqFilterType))
			{
				return GeneratedFilterCondition.Empty;
			}
			CompoundFilterCondition compoundFilterCondition = generatedFilterCondition.Condition as CompoundFilterCondition;
			CompoundFilterCondition compoundFilterCondition2 = generatedFilterCondition2.Condition as CompoundFilterCondition;
			bool flag = DsqFilterConditionGenerator.CanCollapseInnerCompoundCondition(generatedFilterCondition, compoundFilterCondition, @operator, dsqFilterType);
			bool flag2 = DsqFilterConditionGenerator.CanCollapseInnerCompoundCondition(generatedFilterCondition2, compoundFilterCondition2, @operator, dsqFilterType);
			if (flag)
			{
				if (flag2)
				{
					compoundFilterCondition.Conditions.AddRange(compoundFilterCondition2.Conditions);
				}
				else
				{
					compoundFilterCondition.Conditions.Add(generatedFilterCondition2.Condition);
				}
				return generatedFilterCondition;
			}
			if (flag2)
			{
				compoundFilterCondition2.Conditions.Insert(0, generatedFilterCondition.Condition);
				return generatedFilterCondition2;
			}
			return GeneratedFilterCondition.CreateSucceeded(new CompoundFilterCondition
			{
				Operator = @operator,
				Conditions = new List<FilterCondition>(2) { generatedFilterCondition.Condition, generatedFilterCondition2.Condition }
			}, dsqFilterType);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001060C File Offset: 0x0000E80C
		private bool CompoundConditionAlreadyHandled(GeneratedFilterCondition leftCondition, GeneratedFilterCondition rightCondition, out GeneratedFilterCondition result)
		{
			result = GeneratedFilterCondition.Ignored;
			bool flag = this.IsCollapsibleCondition(leftCondition);
			bool flag2 = this.IsCollapsibleCondition(rightCondition);
			if (flag && flag2)
			{
				return false;
			}
			if (flag)
			{
				result = leftCondition;
			}
			if (flag2)
			{
				result = rightCondition;
			}
			return true;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010650 File Offset: 0x0000E850
		private static bool CanCollapseInnerCompoundCondition(GeneratedFilterCondition condition, CompoundFilterCondition compoundCondition, CompoundFilterOperator @operator, DsqFilterType? combinedType)
		{
			if (compoundCondition != null && compoundCondition.Operator == @operator)
			{
				DsqFilterType? filterType = condition.FilterType;
				DsqFilterType? dsqFilterType = combinedType;
				return (filterType.GetValueOrDefault() == dsqFilterType.GetValueOrDefault()) & (filterType != null == (dsqFilterType != null));
			}
			return false;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000106A0 File Offset: 0x0000E8A0
		private bool IsCollapsibleCondition(GeneratedFilterCondition condition)
		{
			if (condition.ConversionStatus == FilterConversionStatus.Ignored)
			{
				return false;
			}
			if (condition.ConversionStatus == FilterConversionStatus.Succeeded && condition.Condition != null && (condition.Condition is AnyValueFilterCondition || condition.Condition is DefaultValueFilterCondition))
			{
				this._nonCollapsibleConditions.Add(condition);
				return false;
			}
			return true;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public override GeneratedFilterCondition Visit(ResolvedQueryBetweenExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression;
			if (!this.TryGenerateExpression(expression.Expression, out generatedDsqExpression))
			{
				return GeneratedFilterCondition.Empty;
			}
			GeneratedDsqExpression generatedDsqExpression2;
			if (!this.TryGenerateExpression(expression.LowerBound, out generatedDsqExpression2))
			{
				this.RegisterFilterComparisonError(expression.Expression, expression.LowerBound, new Action<IConceptualProperty, ResolvedQueryExpression>(this.RegisterBetweenFilterError));
				return GeneratedFilterCondition.Empty;
			}
			GeneratedDsqExpression generatedDsqExpression3;
			if (!this.TryGenerateExpression(expression.UpperBound, out generatedDsqExpression3))
			{
				this.RegisterFilterComparisonError(expression.Expression, expression.UpperBound, new Action<IConceptualProperty, ResolvedQueryExpression>(this.RegisterBetweenFilterError));
				return GeneratedFilterCondition.Empty;
			}
			bool flag = generatedDsqExpression.HasAggregate || generatedDsqExpression2.HasAggregate || generatedDsqExpression3.HasAggregate;
			CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = new List<FilterCondition>(2)
				{
					new BinaryFilterCondition
					{
						Operator = BinaryFilterOperator.GreaterThanOrEqual,
						LeftExpression = generatedDsqExpression.Expression,
						RightExpression = generatedDsqExpression2.Expression
					},
					new BinaryFilterCondition
					{
						Operator = BinaryFilterOperator.LessThanOrEqual,
						LeftExpression = generatedDsqExpression.Expression,
						RightExpression = generatedDsqExpression3.Expression
					}
				}
			};
			return this.BuildFilterCondition(flag, compoundFilterCondition);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00010837 File Offset: 0x0000EA37
		private void RegisterBetweenFilterError(IConceptualProperty target, ResolvedQueryExpression otherExpression)
		{
			this._errorContext.Register(DataShapeGenerationMessages.InvalidBetweenFilterConditionExpression(EngineMessageSeverity.Error, target, otherExpression));
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001084C File Offset: 0x0000EA4C
		public override GeneratedFilterCondition Visit(ResolvedQueryExistsExpression expression)
		{
			if (this._generationOptions.SuppressExistsFilters)
			{
				return GeneratedFilterCondition.Ignored;
			}
			GeneratedDsqExpression generatedDsqExpression;
			if (!this.TryGenerateExpression(expression.Expression, out generatedDsqExpression))
			{
				return GeneratedFilterCondition.Empty;
			}
			ExistsFilterCondition existsFilterCondition = null;
			if (this._exists == null)
			{
				existsFilterCondition = new ExistsFilterCondition();
				this._exists = new ExistsFilterConditionBuilder<IDsqFilterConditionGenerator>(this, existsFilterCondition);
			}
			ExistsFilterItemBuilder<ExistsFilterConditionBuilder<IDsqFilterConditionGenerator>> existsFilterItemBuilder = this._exists.WithExistsItem().WithExists(generatedDsqExpression.Expression);
			foreach (ResolvedQueryExpression resolvedQueryExpression in this._currentTargets)
			{
				GeneratedDsqExpression generatedDsqExpression2;
				if (!this.TryGenerateExpression(resolvedQueryExpression, out generatedDsqExpression2))
				{
					return GeneratedFilterCondition.Empty;
				}
				existsFilterItemBuilder.WithTarget(generatedDsqExpression2.Expression);
			}
			return GeneratedFilterCondition.CreateSucceeded(existsFilterCondition, new DsqFilterType?(DsqFilterType.Exist));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001092C File Offset: 0x0000EB2C
		protected override GeneratedFilterCondition VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			return GeneratedFilterCondition.Empty;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010934 File Offset: 0x0000EB34
		private bool TryGetFilterType(DsqFilterType? x, DsqFilterType? y, out DsqFilterType? combined)
		{
			if (x == null)
			{
				combined = y;
				return true;
			}
			if (y != null)
			{
				DsqFilterType? dsqFilterType = x;
				DsqFilterType? dsqFilterType2 = y;
				if (!((dsqFilterType.GetValueOrDefault() == dsqFilterType2.GetValueOrDefault()) & (dsqFilterType != null == (dsqFilterType2 != null))))
				{
					combined = null;
					return false;
				}
			}
			combined = x;
			return true;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010994 File Offset: 0x0000EB94
		private T GenerateWithParentTracking<T>(ResolvedQueryExpression expression, Func<T> action)
		{
			this._parentExpressions.Push(expression);
			T t = action();
			this._parentExpressions.Pop();
			return t;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000109B4 File Offset: 0x0000EBB4
		private bool TryGenerateExpression(ResolvedQueryExpression target, out GeneratedDsqExpression generatedExpression)
		{
			if (!ResolvedQueryExpressionValidator.Validate(target, this._errorContext, AllowedExpressionContent.WhereExpression, this._expressionContext))
			{
				generatedExpression = default(GeneratedDsqExpression);
				return false;
			}
			bool flag = this._exprBuilder.TryGenerate(target, out generatedExpression);
			if (flag)
			{
				this._expressionContent |= generatedExpression.ExpressionContent;
			}
			return flag;
		}

		// Token: 0x04000248 RID: 584
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000249 RID: 585
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x0400024A RID: 586
		private readonly DsqExpressionGenerator _exprBuilder;

		// Token: 0x0400024B RID: 587
		private readonly DsqFilterConditionGenerationOptions _generationOptions;

		// Token: 0x0400024C RID: 588
		private readonly DataShapeGenerationTelemetry _telemetry;

		// Token: 0x0400024D RID: 589
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x0400024E RID: 590
		private IReadOnlyList<ResolvedQueryExpression> _currentTargets;

		// Token: 0x0400024F RID: 591
		private HashSet<ResolvedQueryExpression> _implicitTargetsEncountered;

		// Token: 0x04000250 RID: 592
		private ExistsFilterConditionBuilder<IDsqFilterConditionGenerator> _exists;

		// Token: 0x04000251 RID: 593
		private AnyValueFilterConditionBuilder<IDsqFilterConditionGenerator> _anyValueWithAncestorOverridingBuilder;

		// Token: 0x04000252 RID: 594
		private AnyValueFilterConditionBuilder<IDsqFilterConditionGenerator> _anyValueBuilder;

		// Token: 0x04000253 RID: 595
		private DefaultValueFilterConditionBuilder<IDsqFilterConditionGenerator> _defaultValueBuilder;

		// Token: 0x04000254 RID: 596
		private Stack<ResolvedQueryExpression> _parentExpressions;

		// Token: 0x04000255 RID: 597
		private IList<GeneratedFilterCondition> _nonCollapsibleConditions;

		// Token: 0x04000256 RID: 598
		private ExpressionContent _expressionContent;

		// Token: 0x04000257 RID: 599
		private ExpressionContext _expressionContext;

		// Token: 0x04000258 RID: 600
		private bool _useIdentityEqualsComparisons;

		// Token: 0x02000129 RID: 297
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004D6 RID: 1238
			public static Func<ResolvedQueryExpression, ResolvedQueryExpression> <0>__GetSourceRefExpressionOrDefault;
		}
	}
}
