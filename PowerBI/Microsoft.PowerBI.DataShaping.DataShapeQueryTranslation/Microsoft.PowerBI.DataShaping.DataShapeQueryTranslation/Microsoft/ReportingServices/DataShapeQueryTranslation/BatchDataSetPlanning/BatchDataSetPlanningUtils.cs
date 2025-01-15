using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000170 RID: 368
	internal static class BatchDataSetPlanningUtils
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x00036854 File Offset: 0x00034A54
		public static bool ContainsModelReference(IEnumerable<Calculation> calculations, ExpressionTable expressionTable, CalculationExpressionMap calculationMap)
		{
			foreach (Calculation calculation in calculations)
			{
				foreach (ExpressionId expressionId in calculationMap.GetExpressions(calculation))
				{
					if (ModelReferenceAnalyzer.ContainsModelReference(expressionTable.GetNode(expressionId)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000368E4 File Offset: 0x00034AE4
		internal static bool HasModelMeasureInCalculation(ExpressionTable expressionTable, CalculationExpressionMap calculationMap, IEnumerable<Calculation> calculations)
		{
			Func<ExpressionNode, bool> func;
			if ((func = BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure) == null)
			{
				func = (BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsModelMeasure));
			}
			return BatchDataSetPlanningUtils.HasExpressionFeatureInCalculation(expressionTable, calculationMap, calculations, func);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003690C File Offset: 0x00034B0C
		internal static bool HasExpressionFeatureInCalculation(ExpressionTable expressionTable, CalculationExpressionMap calculationMap, IEnumerable<Calculation> calculations, Func<ExpressionNode, bool> hasExpressionFeature)
		{
			foreach (Calculation calculation in calculations)
			{
				using (IEnumerator<ExpressionId> enumerator2 = calculationMap.GetExpressions(calculation).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (BatchDataSetPlanningUtils.HasExpressionFeatureInExpression(enumerator2.Current, expressionTable, hasExpressionFeature))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00036994 File Offset: 0x00034B94
		internal static bool HasModelMeasure(IEnumerable<Expression> expressions, ExpressionTable expressionTable)
		{
			Func<ExpressionNode, bool> func;
			if ((func = BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure) == null)
			{
				func = (BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsModelMeasure));
			}
			return BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(expressions, expressionTable, func);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000369B8 File Offset: 0x00034BB8
		internal static bool HasModelMeasure(IEnumerable<PlanDataTransformColumnMeasure> columns, ExpressionTable expressionTable)
		{
			IEnumerable<Expression> enumerable = columns.Select((PlanDataTransformColumnMeasure c) => c.Column.Value);
			Func<ExpressionNode, bool> func;
			if ((func = BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure) == null)
			{
				func = (BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsModelMeasure));
			}
			return BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(enumerable, expressionTable, func);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00036A0C File Offset: 0x00034C0C
		internal static bool HasExpressionFeatureInExpressions(IEnumerable<Expression> expressions, ExpressionTable expressionTable, Func<ExpressionNode, bool> hasExpressionFeature)
		{
			return expressions.Any((Expression e) => BatchDataSetPlanningUtils.HasExpressionFeatureInExpression(e.ExpressionId.Value, expressionTable, hasExpressionFeature));
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00036A3F File Offset: 0x00034C3F
		internal static bool HasModelMeasure(ExpressionId exprId, ExpressionTable expressionTable)
		{
			Func<ExpressionNode, bool> func;
			if ((func = BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure) == null)
			{
				func = (BatchDataSetPlanningUtils.<>O.<0>__IsModelMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsModelMeasure));
			}
			return BatchDataSetPlanningUtils.HasExpressionFeatureInExpression(exprId, expressionTable, func);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00036A64 File Offset: 0x00034C64
		internal static bool HasExpressionFeatureInExpression(ExpressionId exprId, ExpressionTable expressionTable, Func<ExpressionNode, bool> hasExpressionFeature)
		{
			ExpressionNode node = expressionTable.GetNode(exprId);
			return hasExpressionFeature(node);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00036A80 File Offset: 0x00034C80
		internal static IList<PlanOperation> ToPlanOperations(this IList<BatchGroupAndJoinBuilder> scopedTableBuilders, string telemetryName)
		{
			List<PlanOperation> list = new List<PlanOperation>(scopedTableBuilders.Count);
			for (int i = 0; i < scopedTableBuilders.Count; i++)
			{
				list.Add(scopedTableBuilders[i].ToPlanOperation(telemetryName));
			}
			return list;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00036ABE File Offset: 0x00034CBE
		public static PlanOperationContext ApplyGroupByHierarchy(DataShapeAnnotations annotations, PlanOperationContext table, IReadOnlyList<DataMember> dynamicMembers, IReadOnlyList<DataMember> allMembers, ScopeTree scopeTree, SubtotalUsage includedSubtotalKind, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys)
		{
			return BatchDataSetPlanningUtils.ApplyRegroupTable(annotations, table, allMembers, scopeTree, includedSubtotalKind, includeSortByMeasureKeysAtMeasureScope, excludeMeasureSortKeys);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00036AD0 File Offset: 0x00034CD0
		public static PlanOperationContext ApplyRegroupTable(DataShapeAnnotations annotations, PlanOperationContext table, IReadOnlyList<DataMember> members, ScopeTree scopeTree, SubtotalUsage includedSubtotalKind, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys)
		{
			List<Calculation> list2;
			List<DataMember> list3;
			List<PlanGroupByItem> list = BatchDataSetPlanningUtils.CreateGroupByItems(annotations, members, includedSubtotalKind, includeSortByMeasureKeysAtMeasureScope, excludeMeasureSortKeys, out list2, out list3);
			PlanOperation planOperation = table.Table.GroupBy(list);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(list3.ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, scopeTree.GetSpanningScopes(members.Where((DataMember m) => m.IsDynamic).ToList<DataMember>()).ToReadOnlyList<IScope>(), list2.AsReadOnly(), Util.EmptyReadOnlyCollection<DataMember>(), planOperationFilteringMetadata);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00036B50 File Offset: 0x00034D50
		private static List<PlanGroupByItem> CreateGroupByItems(DataShapeAnnotations annotations, IReadOnlyList<DataMember> dynamicMembers, SubtotalUsage includedSubtotalKind, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys, out List<Calculation> calculations, out List<DataMember> totals)
		{
			calculations = new List<Calculation>();
			totals = new List<DataMember>();
			List<PlanGroupByItem> list = new List<PlanGroupByItem>(dynamicMembers.Count);
			foreach (DataMember dataMember in dynamicMembers)
			{
				if (dataMember.IsDynamic)
				{
					PlanGroupByMember planGroupByMember = dataMember.ToGroupByItem(annotations, includedSubtotalKind, includeSortByMeasureKeysAtMeasureScope, excludeMeasureSortKeys, null);
					list.Add(planGroupByMember);
					if (planGroupByMember.RequiresRollupGroup)
					{
						totals.Add(dataMember);
					}
				}
				List<Calculation> calculations2 = dataMember.Calculations;
				if (calculations2 != null)
				{
					for (int i = 0; i < calculations2.Count; i++)
					{
						Calculation calculation = calculations2[i];
						if (!annotations.IsMeasure(calculation) && !annotations.IsSynchronizationIndex(calculation) && !annotations.IsVisualCalculation(calculation))
						{
							list.Add(calculation.ToGroupByItem());
							calculations.Add(calculation);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00036C44 File Offset: 0x00034E44
		public static PlanOperationContext ApplyInnermostIntersectionLimit(ICommonPlanningContext context, DataShapeContext dsContext, PlanOperationContext table, IEnumerable<PlanSortItem> sortItems, Identifier suppressedLimitId)
		{
			IScope innermostScopeInDataShape = context.ScopeTree.GetInnermostScopeInDataShape(dsContext.DataShape);
			if (innermostScopeInDataShape.ObjectType == ObjectType.DataIntersection)
			{
				Limit limit = context.Annotations.GetLimit((DataIntersection)innermostScopeInDataShape);
				if (limit != null && limit.Id != suppressedLimitId)
				{
					PlanOperation planOperation = BatchDataSetPlannerSimpleLimitTranslator.Translate(limit, table.Table, sortItems, limit.GetGroupScopesFromTargets(context.OutputExpressionTable), context.ErrorContext, context.Annotations.SubtotalAnnotations, true, null, null);
					table = table.ReplaceTable(planOperation, null, null, null);
				}
			}
			return table;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00036CCC File Offset: 0x00034ECC
		internal static PlanExpression CreateSegmentSizeExpression(int segmentSize, Identifier dataShapeId, TranslationErrorContext errorContext)
		{
			ExpressionNode expressionNode = ExprNodes.Literal(segmentSize);
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.DataShape, dataShapeId, "RequestedPrimaryLeafCount");
			return new PlanExpression(expressionNode, expressionContext);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00036CF9 File Offset: 0x00034EF9
		internal static PlanExpression CreateLimitCountExpression(int value, Identifier limitId, TranslationErrorContext errorContext)
		{
			return BatchDataSetPlanningUtils.CreateLimitCountExpression(ExprNodes.Literal(value), limitId, errorContext);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00036D10 File Offset: 0x00034F10
		internal static PlanExpression CreateLimitCountExpression(ExpressionNode countExpr, Identifier limitId, TranslationErrorContext errorContext)
		{
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.LimitOperator, limitId, "Count");
			return new PlanExpression(countExpr, expressionContext);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00036D34 File Offset: 0x00034F34
		internal static PlanExpression CreateLimitSkipExpression(long value, Identifier limitId, TranslationErrorContext errorContext)
		{
			ExpressionNode expressionNode = ExprNodes.Literal(value);
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.LimitOperator, limitId, "Skip");
			return new PlanExpression(expressionNode, expressionContext);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00036D64 File Offset: 0x00034F64
		public static PlanOperationContext RestoreScopedMeasureShowAllValues(DataShapeContext dsContext, IReadOnlyList<PlanOperationContext> coreTableFragments, PlanOperationContext table, PlanDeclarationCollection declarations, RowScopesMetadata coreTableRowScopes)
		{
			if (coreTableFragments.Count > 1 && dsContext.HasShowItemsWithNoData)
			{
				ScopeTree scopeTree = dsContext.ScopeTree;
				PlanOperationContext planOperationContext = table;
				foreach (PlanOperationContext planOperationContext2 in coreTableFragments)
				{
					if (table.RowScopes.IsPrefixOfScopes(planOperationContext2.RowScopes) && !planOperationContext2.RowScopes.Equals(coreTableRowScopes))
					{
						if (planOperationContext2.GetAllGroups().Any((DataMember m) => m.UsesShowItemsWithNoData()))
						{
							planOperationContext = planOperationContext.LeftOuterJoin(planOperationContext2, scopeTree);
						}
					}
				}
				if (table != planOperationContext)
				{
					table = planOperationContext.DeclareIfNotDeclared(PlanNames.Restored(table.RowScopes.InnermostScope.Id), declarations, false, null, false);
				}
			}
			return table;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00036E44 File Offset: 0x00035044
		public static bool TryGetCoreTableRowScope(DataShape rootDataShape, ScopeTree scopeTree, out IScope innermostScope)
		{
			bool flag = true;
			IScope innermostScopeInDataShape = scopeTree.GetInnermostScopeInDataShape(rootDataShape);
			innermostScope = innermostScopeInDataShape;
			IList<DataShape> list = scopeTree.GetChildScopes(innermostScopeInDataShape).OfType<DataShape>().Evaluate<DataShape>();
			if (scopeTree.AreSameScope(innermostScopeInDataShape, rootDataShape) && list.Count > 1)
			{
				innermostScope = null;
				return false;
			}
			if (list.Count > 0)
			{
				flag = BatchDataSetPlanningUtils.TryGetCoreTableRowScope(list.First<DataShape>(), scopeTree, out innermostScope);
			}
			return flag;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00036EA8 File Offset: 0x000350A8
		public static IList<DataMember> GetGroupsForCoreTableRowScope(this DataShape dataShape, ScopeTree scopeTree, DataMemberAnnotations dataMemberAnnotations, bool isPrimary)
		{
			IList<DataMember> dynamicMembers = (isPrimary ? dataShape.PrimaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>() : dataShape.SecondaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>());
			IScope scope = null;
			if (!BatchDataSetPlanningUtils.TryGetCoreTableRowScope(dataShape, scopeTree, out scope))
			{
				return new List<DataMember>();
			}
			List<DataMember> list = new List<DataMember>();
			if (dynamicMembers.Count > 0)
			{
				if (isPrimary)
				{
					list = (from d in scopeTree.GetAllParentScopes(scope).OfType<DataMember>()
						where dataMemberAnnotations.IsPrimaryMember(d) && scopeTree.IsSameOrParentScope(dynamicMembers[0], d)
						select d).ToList<DataMember>();
				}
				else
				{
					list = (from d in scopeTree.GetAllParentScopes(scope).OfType<DataMember>()
						where !dataMemberAnnotations.IsPrimaryMember(d) && scopeTree.IsSameOrParentScope(dynamicMembers[0], d)
						select d).ToList<DataMember>();
				}
			}
			return list;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00036F78 File Offset: 0x00035178
		public static bool AreEquivalentScopes(IScope scope1, IScope scope2, ScopeTree scopeTree)
		{
			IScope scope3 = scope1;
			IScope scope4 = scope2;
			if (scopeTree.AreSameScope(scope1, scope2))
			{
				return true;
			}
			if (scopeTree.IsParentScope(scope1, scope2))
			{
				scope3 = scope1;
				scope4 = scope2;
			}
			else if (scopeTree.IsParentScope(scope2, scope1))
			{
				scope3 = scope2;
				scope4 = scope1;
			}
			return scope4.ObjectType == ObjectType.DataShape && !scopeTree.GetAllParentScopes(scope4, scope3).Except(new IScope[] { scope3 }).OfType<DataMember>()
				.Any<DataMember>();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00036FE4 File Offset: 0x000351E4
		public static void ExtractExpressionFromEvaluateRollup(Expression expression, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable)
		{
			ExpressionNode expressionNode;
			if (ExpressionAnalysisUtils.IsFilterByRollupScopeMeasureExpression(inputExpressionTable.GetNode(expression), out expressionNode))
			{
				outputExpressionTable.SetNode(expression, expressionNode);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003700C File Offset: 0x0003520C
		public static IEnumerable<Calculation> GetMeasuresAtScope(IEnumerable<Calculation> calculations, IScope scope, DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			return from calc in calculations
				where annotations.IsMeasure(calc)
				where scopeTree.AreSameScope(scopeTree.GetContainingScope(calc), scope)
				select calc;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00037058 File Offset: 0x00035258
		public static bool UseEnhancedSampling(DataShapeContext dsContext)
		{
			if (!dsContext.HasPrimaryHierarchyLimit)
			{
				return false;
			}
			SampleLimitOperator sampleLimitOperator = dsContext.PrimaryHierarchyLimit.Operator as SampleLimitOperator;
			return sampleLimitOperator != null && sampleLimitOperator.PreserveKeyPoints.GetValueOrDefault<bool>();
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00037090 File Offset: 0x00035290
		public static Identifier FindSuppressedDynamicIntersectionLimit(DataShapeContext dsContext, ExpressionTable expressionTable)
		{
			Expression intersectionLimit = dsContext.DataShape.DynamicLimits.IntersectionLimit;
			if (intersectionLimit != null)
			{
				return expressionTable.GetNode<ResolvedLimitReferenceExpressionNode>(intersectionLimit.ExpressionId.Value).Limit.Id;
			}
			return null;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000370D1 File Offset: 0x000352D1
		internal static LiteralExpressionNode GetDynamicLimitPadding(Limit limit)
		{
			if (limit.Operator is SampleLimitOperator)
			{
				return ExprNodes.Literal(2L);
			}
			return LiteralExpressionNode.OneInt64;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000370F2 File Offset: 0x000352F2
		internal static ScopedMeasureShowAllRestorationMode DetermineScopedMeasureShowAllRestorationMode(DataShapeContext dsContext)
		{
			if (dsContext.HasDataShapeAggregatesAndProjections)
			{
				return ScopedMeasureShowAllRestorationMode.PreAggregates;
			}
			return ScopedMeasureShowAllRestorationMode.PostLimit;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00037100 File Offset: 0x00035300
		public static bool HasOutputTotal(this DataMember member, DataShapeAnnotations annotations)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			return annotations.TryGetBatchSubtotalAnnotation(member, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00037128 File Offset: 0x00035328
		public static IReadOnlyList<SubtotalColumnFilteringMetadata> ToTotalsMetadata(this IReadOnlyList<DataMember> totals)
		{
			if (totals.IsNullOrEmpty<DataMember>())
			{
				return null;
			}
			List<SubtotalColumnFilteringMetadata> list = new List<SubtotalColumnFilteringMetadata>(totals.Count);
			foreach (DataMember dataMember in totals)
			{
				list.Add(new SubtotalColumnFilteringMetadata(dataMember, null));
			}
			return list;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00037198 File Offset: 0x00035398
		internal static bool GetSubtotalIndicatorColumnFilteringValue(int memberIndex, int stopFilteringScopeMemberIndx)
		{
			return memberIndex > stopFilteringScopeMemberIndx;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000371A0 File Offset: 0x000353A0
		internal static PlanNewColumnProjectItem GetSubtotalIndicatorColumnProjectItem(this string subtotalIndicatorColumnName, LiteralExpressionNode value, TranslationErrorContext errorContext)
		{
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.DataMember, subtotalIndicatorColumnName, subtotalIndicatorColumnName);
			return new PlanNewColumnProjectItem(value, subtotalIndicatorColumnName, expressionContext, ColumnReuseKind.None);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000371C8 File Offset: 0x000353C8
		internal static bool ShouldIncludeCalculationInBaseQuery(this Calculation calculation, DataShapeContext dsContext, DataShapeAnnotations annotations, DataTransformReferenceMap transformReferenceMap)
		{
			Calculation calculation2;
			return !dsContext.DataShapeAggregatesAndProjections.Contains(calculation) && !annotations.CanBeHandledByProcessing(calculation) && !transformReferenceMap.HasDataTransformColumnReference(calculation) && !annotations.IsSynchronizationIndex(calculation) && !annotations.IsVisualCalculation(calculation) && (!annotations.IsSubtotal(calculation, out calculation2) || !annotations.IsVisualCalculation(calculation2));
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00037228 File Offset: 0x00035428
		internal static ExpressionNode RewriteAggregateInGroupBy(this FunctionCallExpressionNode node, ExpressionNode newArgument, AggregateGroupByTable aggregateTable, ExpressionContext expressionContext, WritableExpressionTable expressionTable, bool preferPlanName)
		{
			FunctionDescriptor functionDescriptor = ((node.Descriptor.Name == "SingleValue") ? FunctionDescriptorFactory.GetDescriptor("Min") : node.Descriptor);
			AggregatableCurrentGroupExpressionNode aggregatableCurrentGroupExpressionNode = new AggregatableCurrentGroupExpressionNode(newArgument);
			FunctionCallExpressionNode functionCallExpressionNode = new FunctionCallExpressionNode(functionDescriptor, node.UsageKind, new AggregatableCurrentGroupExpressionNode[] { aggregatableCurrentGroupExpressionNode });
			return new BatchColumnReferenceExpressionNode(aggregateTable.AddOrReuseAggregate(preferPlanName ? expressionContext.ObjectId.Value : PlanNames.Argument(expressionContext.ObjectId.Value), functionCallExpressionNode, expressionTable, expressionContext, preferPlanName).PlanName);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000372B2 File Offset: 0x000354B2
		internal static CoreTableTotalsTransforms.Context CreateCoreTableTransformContext(ScopeTree scopeTree, DataShapeAnnotations annotations, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, WritableExpressionTable outputExpressionTable)
		{
			return new CoreTableTotalsTransforms.Context(annotations, outputExpressionTable, sortByMeasureExpressionMappings, scopeTree);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000372C0 File Offset: 0x000354C0
		internal static IEnumerable<IScope> GetAllParentScopesWithoutTopDataShapes(this IScope startScope, ScopeTree scopeTree)
		{
			IEnumerable<IScope> allParentScopes = scopeTree.GetAllParentScopes(startScope);
			IEnumerable<IScope> enumerable = allParentScopes.SkipWhile((IScope s) => s is DataShape);
			if (enumerable.Any<IScope>())
			{
				return enumerable;
			}
			return allParentScopes;
		}

		// Token: 0x020002EE RID: 750
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000AE4 RID: 2788
			public static Func<ExpressionNode, bool> <0>__IsModelMeasure;
		}
	}
}
