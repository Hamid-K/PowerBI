using System;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders.DynamicLimits;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C7 RID: 455
	internal sealed class DynamicLimitsTablesBuilder
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x000415AC File Offset: 0x0003F7AC
		internal static DynamicLimitCounts ApplyDynamicLimits(DataShapeAnnotations annotations, TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShapeContext dsContext, PlanOperationContext coreTableOnlyOutputTotals, PlanOperationContext primaryTableBase, PlanOperationContext secondaryTableBase, out PlanOperationContext primaryTable, out PlanOperationContext secondaryTable)
		{
			DynamicLimitCounts dynamicLimitCounts = DynamicLimitsTablesBuilder.DetermineDynamicLimitCounts(errorContext, declarations, dsContext, coreTableOnlyOutputTotals, primaryTableBase, secondaryTableBase);
			LiteralExpressionNode dynamicLimitPadding = BatchDataSetPlanningUtils.GetDynamicLimitPadding(dsContext.PrimaryHierarchyLimit);
			ExpressionNode expressionNode = dynamicLimitCounts.TargetPrimaryCount.Add(dynamicLimitPadding);
			primaryTable = LimitApplier.ApplyTotalHierarchyLimit(annotations, errorContext, dsContext, primaryTableBase, dsContext.PrimaryDynamicsExcludingContextOnly, expressionNode);
			primaryTable = primaryTable.DeclareIfNotDeclared(PlanNames.Primary(dsContext.Id), declarations, false, null, false);
			LiteralExpressionNode dynamicLimitPadding2 = BatchDataSetPlanningUtils.GetDynamicLimitPadding(dsContext.SecondaryHierarchyLimit);
			ExpressionNode expressionNode2 = dynamicLimitCounts.TargetSecondaryCount.Add(dynamicLimitPadding2);
			secondaryTable = LimitApplier.ApplyTotalHierarchyLimit(annotations, errorContext, dsContext, secondaryTableBase, dsContext.SecondaryDynamicsExcludingContextOnly, expressionNode2);
			secondaryTable = secondaryTable.DeclareIfNotDeclared(PlanNames.Secondary(dsContext.Id), declarations, false, null, false);
			return dynamicLimitCounts;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004165C File Offset: 0x0003F85C
		private static DynamicLimitCounts DetermineDynamicLimitCounts(TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShapeContext dsContext, PlanOperationContext coreTableOnlyOutputTotals, PlanOperationContext primaryTable, PlanOperationContext secondaryTable)
		{
			DynamicLimits dynamicLimits = dsContext.DataShape.DynamicLimits;
			ExpressionNode expressionNode = coreTableOnlyOutputTotals.CountRows();
			expressionNode = expressionNode.DeclareIfNotDeclared(PlanNames.IntersectionCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode2 = primaryTable.CountRows();
			expressionNode2 = expressionNode2.DeclareIfNotDeclared(PlanNames.PrimaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode3 = secondaryTable.CountRows();
			expressionNode3 = expressionNode3.DeclareIfNotDeclared(PlanNames.SecondaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			LiteralExpressionNode literalExpressionNode = ExprNodes.Literal((long)dynamicLimits.TargetIntersectionCount.Value);
			ExpressionNode expressionNode4 = expressionNode2.Multiply(expressionNode3);
			expressionNode4 = expressionNode4.DeclareIfNotDeclared(PlanNames.SpaceCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode5;
			ExpressionNode expressionNode6;
			if (dsContext.HasShowItemsWithNoData)
			{
				expressionNode5 = literalExpressionNode;
				expressionNode6 = expressionNode4.LessThanOrEqualNoNan(literalExpressionNode);
			}
			else
			{
				LiteralExpressionNode literalExpressionNode2 = ExprNodes.Literal(5L);
				ExpressionNode expressionNode7 = expressionNode4.Divide(expressionNode);
				ExpressionNode expressionNode8 = ExprNodes.MinValue(new ExpressionNode[] { literalExpressionNode2, expressionNode7 });
				expressionNode8 = expressionNode8.DeclareIfNotDeclared(PlanNames.SparseFactor(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
				expressionNode5 = ExprNodes.Ceiling(literalExpressionNode.Multiply(expressionNode8), null);
				expressionNode5 = expressionNode5.DeclareIfNotDeclared(PlanNames.TargetIntersectionCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
				expressionNode6 = expressionNode.LessThanOrEqualNoNan(literalExpressionNode);
			}
			ExpressionNode expressionNode9;
			ExpressionNode expressionNode10;
			DynamicLimitsTablesBuilder.DetermineBalancedCounts(errorContext, declarations, dsContext, dynamicLimits.Primary, dynamicLimits.Secondary, expressionNode2, expressionNode3, literalExpressionNode, expressionNode5, expressionNode6, expressionNode4, out expressionNode9, out expressionNode10);
			return new DynamicLimitCounts(expressionNode, expressionNode2, expressionNode9, expressionNode3, expressionNode10);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000417F0 File Offset: 0x0003F9F0
		internal static void DetermineBalancedCounts(TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicLimitRecommendation primaryRecommendation, DynamicLimitRecommendation secondaryRecommendation, ExpressionNode actualPrimaryCount, ExpressionNode actualSecondaryCount, ExpressionNode requestedIntersectionCount, ExpressionNode targetCount, ExpressionNode canFitAllPoints, ExpressionNode spaceCount, out ExpressionNode targetPrimary, out ExpressionNode targetSecondary)
		{
			if (primaryRecommendation.IsMandatoryConstraint && secondaryRecommendation.IsMandatoryConstraint)
			{
				targetPrimary = primaryRecommendation.Max.ToLiteralExpr();
				targetSecondary = secondaryRecommendation.Max.ToLiteralExpr();
				return;
			}
			if (primaryRecommendation.IsMandatoryConstraint)
			{
				global::System.ValueTuple<ExpressionNode, ExpressionNode> valueTuple = DynamicLimitsTablesBuilder.DetermineCountsForOneMandatory(errorContext, declarations, dsContext, targetCount, primaryRecommendation, actualPrimaryCount, secondaryRecommendation, PlanNames.TargetSecondaryCount(dsContext.Id));
				targetPrimary = valueTuple.Item1;
				targetSecondary = valueTuple.Item2;
				return;
			}
			if (secondaryRecommendation.IsMandatoryConstraint)
			{
				global::System.ValueTuple<ExpressionNode, ExpressionNode> valueTuple = DynamicLimitsTablesBuilder.DetermineCountsForOneMandatory(errorContext, declarations, dsContext, targetCount, secondaryRecommendation, actualSecondaryCount, primaryRecommendation, PlanNames.TargetPrimaryCount(dsContext.Id));
				targetSecondary = valueTuple.Item1;
				targetPrimary = valueTuple.Item2;
				return;
			}
			DynamicLimitsTablesBuilder.DetermineBalancedCountsAroundRecommendations(errorContext, declarations, dsContext, primaryRecommendation, secondaryRecommendation, actualPrimaryCount, actualSecondaryCount, requestedIntersectionCount, targetCount, canFitAllPoints, spaceCount, out targetPrimary, out targetSecondary);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000418B4 File Offset: 0x0003FAB4
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "mandatoryCount", "dependentCount" })]
		private static global::System.ValueTuple<ExpressionNode, ExpressionNode> DetermineCountsForOneMandatory(TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShapeContext dsContext, ExpressionNode combinedTargetCount, DynamicLimitRecommendation mandatoryRecommendation, ExpressionNode mandatoryActualCount, DynamicLimitRecommendation dependentRecomendation, string dependentCountPlanName)
		{
			ExpressionNode expressionNode = mandatoryRecommendation.Max.ToLiteralExpr();
			ExpressionNode expressionNode2 = ExprNodes.MaxValue(new ExpressionNode[]
			{
				ExprNodes.MinValue(new ExpressionNode[] { mandatoryActualCount, expressionNode }),
				LiteralExpressionNode.OneInt64
			});
			ExpressionNode expressionNode3 = ExprNodes.MinValue(new ExpressionNode[]
			{
				combinedTargetCount.DivideAndCeiling(expressionNode2),
				dependentRecomendation.Max.ToLiteralExpr()
			});
			expressionNode3 = expressionNode3.DeclareIfNotDeclared(dependentCountPlanName, declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			return new global::System.ValueTuple<ExpressionNode, ExpressionNode>(expressionNode, expressionNode3);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00041938 File Offset: 0x0003FB38
		private static void DetermineBalancedCountsAroundRecommendations(TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicLimitRecommendation primaryRecommendation, DynamicLimitRecommendation secondaryRecommendation, ExpressionNode actualPrimaryCount, ExpressionNode actualSecondaryCount, ExpressionNode requestedIntersectionCount, ExpressionNode targetCount, ExpressionNode canFitAllPoints, ExpressionNode spaceCount, out ExpressionNode targetPrimary, out ExpressionNode targetSecondary)
		{
			LiteralExpressionNode literalExpressionNode = ExprNodes.Literal((long)primaryRecommendation.Min.Value);
			LiteralExpressionNode literalExpressionNode2 = ExprNodes.Literal((long)primaryRecommendation.Max.Value);
			LiteralExpressionNode literalExpressionNode3 = ExprNodes.Literal((long)secondaryRecommendation.Min.Value);
			LiteralExpressionNode literalExpressionNode4 = ExprNodes.Literal((long)secondaryRecommendation.Max.Value);
			ExpressionNode expressionNode = actualPrimaryCount.GreaterThan(literalExpressionNode2);
			ExpressionNode expressionNode2 = actualSecondaryCount.GreaterThan(literalExpressionNode4);
			ExpressionNode expressionNode3 = ExprNodes.Floor(ExprNodes.Sqrt(targetCount.Multiply(actualPrimaryCount.Divide(actualSecondaryCount))), LiteralExpressionNode.OneInt64);
			ExpressionNode expressionNode4 = ExprNodes.Floor(targetCount.Divide(expressionNode3), LiteralExpressionNode.OneInt64);
			ExpressionNode expressionNode5 = expressionNode2.If(actualPrimaryCount, ExprNodes.Ceiling(targetCount.Divide(actualSecondaryCount), null));
			ExpressionNode expressionNode6 = canFitAllPoints.If(spaceCount.LessThanOrEqualNoNan(targetCount).If(requestedIntersectionCount, expressionNode3), expressionNode.And(expressionNode2).If(ExprNodes.Ceiling(targetCount.Divide(literalExpressionNode4), null), expressionNode5));
			expressionNode6 = expressionNode6.DeclareIfNotDeclared(PlanNames.InitTargetPrimaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode7 = expressionNode2.If(ExprNodes.Ceiling(targetCount.Divide(actualPrimaryCount), null), actualSecondaryCount);
			ExpressionNode expressionNode8 = canFitAllPoints.If(spaceCount.LessThanOrEqualNoNan(targetCount).If(requestedIntersectionCount, expressionNode4), expressionNode.And(expressionNode2).If(literalExpressionNode4, expressionNode7));
			expressionNode8 = expressionNode8.DeclareIfNotDeclared(PlanNames.InitTargetSecondaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode9 = ExprNodes.MinValue(new ExpressionNode[] { literalExpressionNode, actualPrimaryCount });
			expressionNode9 = expressionNode9.DeclareIfNotDeclared(PlanNames.MinPrimaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode10 = ExprNodes.MinValue(new ExpressionNode[] { literalExpressionNode3, actualSecondaryCount });
			expressionNode10 = expressionNode10.DeclareIfNotDeclared(PlanNames.MinSecondaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			ExpressionNode expressionNode11 = expressionNode6.LessThan(expressionNode9);
			ExpressionNode expressionNode12 = expressionNode8.LessThan(expressionNode10);
			targetPrimary = expressionNode11.If(expressionNode9, expressionNode12.If(ExprNodes.Ceiling(targetCount.Divide(expressionNode10), null), expressionNode6));
			targetPrimary = targetPrimary.DeclareIfNotDeclared(PlanNames.TargetPrimaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
			targetSecondary = expressionNode11.If(ExprNodes.Ceiling(targetCount.Divide(expressionNode9), null), expressionNode12.If(expressionNode10, expressionNode8));
			targetSecondary = targetSecondary.DeclareIfNotDeclared(PlanNames.TargetSecondaryCount(dsContext.Id), declarations, errorContext, ObjectType.DynamicLimits, dsContext.Id);
		}
	}
}
