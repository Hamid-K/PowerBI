using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x02000236 RID: 566
	internal sealed class BatchDataSetPlannerBinnedLineSampleLimitTranslator
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x0004A779 File Offset: 0x00048979
		private BatchDataSetPlannerBinnedLineSampleLimitTranslator(ICommonPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, PlanOperationContext input, Limit limit)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_dsContext = dsContext;
			this.m_input = input;
			this.m_limit = limit;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004A7A8 File Offset: 0x000489A8
		public static PlanOperationContext Translate(ICommonPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, PlanOperationContext input, Limit limit, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			PlanOperation planOperation = new BatchDataSetPlannerBinnedLineSampleLimitTranslator(context, declarations, dsContext, input, limit).Translate(limitMetadataBuilder, limitInfoBuilder);
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004A7D4 File Offset: 0x000489D4
		private PlanOperation Translate(LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			this.BuildLimitInfo(limitMetadataBuilder, limitInfoBuilder);
			return this.BuildSampling();
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0004A7E4 File Offset: 0x000489E4
		private void BuildLimitInfo(LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			ExpressionNode expressionNode = this.m_input.CountRows();
			expressionNode = this.DeclareExpressionNodeIfNotDeclared(expressionNode, PlanNames.IntersectionCount(this.m_dsContext.Id), "IntersectionCount");
			ExpressionId expressionId = limitMetadataBuilder.AddColumn(PlanNames.IntersectionCount(this.m_dsContext.Id), expressionNode, ObjectType.Limit, this.m_limit.Id);
			Dictionary<string, ExpressionId> dictionary = null;
			if (this.m_dsContext.HasAnyPrimaryDynamic && this.m_dsContext.HasAnySecondaryDynamic)
			{
				ExpressionNode expressionNode2 = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(this.m_context.Annotations, this.m_input, this.m_dsContext.PrimaryDynamicsExcludingContextOnly, this.m_dsContext.PrimaryMembersExcludingContextOnly, this.m_dsContext.ScopeTree, SubtotalUsage.Output, false, false).CountRows();
				expressionNode2 = this.DeclareExpressionNodeIfNotDeclared(expressionNode2, PlanNames.PrimaryDbCount(this.m_dsContext.Id), "Primary");
				ExpressionNode expressionNode3 = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(this.m_context.Annotations, this.m_input, this.m_dsContext.SecondaryDynamicsExcludingContextOnly, this.m_dsContext.SecondaryMembersExcludingContextOnly, this.m_dsContext.ScopeTree, SubtotalUsage.Output, false, false).CountRows();
				expressionNode3 = this.DeclareExpressionNodeIfNotDeclared(expressionNode3, PlanNames.SecondaryDbCount(this.m_dsContext.Id), "Secondary");
				ExpressionId expressionId2 = limitMetadataBuilder.AddColumn(PlanNames.PrimaryDbCount(this.m_dsContext.Id), expressionNode2, ObjectType.Limit, this.m_limit.Id);
				ExpressionId expressionId3 = limitMetadataBuilder.AddColumn(PlanNames.SecondaryDbCount(this.m_dsContext.Id), expressionNode3, ObjectType.Limit, this.m_limit.Id);
				dictionary = new Dictionary<string, ExpressionId>(LimitPropertyConstants.NameComparer)
				{
					{ "DbPrimaryCount", expressionId2 },
					{ "DbSecondaryCount", expressionId3 }
				};
				limitInfoBuilder.AddTelemetryItem(new LimitTelemetryItem("PriDbCount", expressionId2));
				limitInfoBuilder.AddTelemetryItem(new LimitTelemetryItem("SecDbCount", expressionId3));
			}
			LimitOverride limitOverride = LimitOverride.OverrideLimit(this.m_limit.Id, null, new ExpressionId?(expressionId), new ExpressionId?(expressionId), dictionary, null);
			limitInfoBuilder.AddLimitOverride(limitOverride);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004AA00 File Offset: 0x00048C00
		private ExpressionNode DeclareExpressionNodeIfNotDeclared(ExpressionNode expr, string declarationName, Identifier objectId)
		{
			return expr.DeclareIfNotDeclared(declarationName, this.m_declarations, this.m_context.ErrorContext, ObjectType.Limit, objectId);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004AA20 File Offset: 0x00048C20
		private PlanOperation BuildSampling()
		{
			BinnedLineSampleLimitOperator binnedLineSampleLimitOperator = this.m_limit.Operator as BinnedLineSampleLimitOperator;
			Contract.RetailAssert(binnedLineSampleLimitOperator != null, "Limit operator should have been BinnedLineSampleLimitOperator");
			PlanExpression planExpression = this.CreateCountExpression(binnedLineSampleLimitOperator.Count.Value);
			PlanExpression planExpression2 = this.CreateCountExpression(binnedLineSampleLimitOperator.MinPointsPerSeries.Value);
			PlanExpression planExpression3 = this.CreateCountExpression(binnedLineSampleLimitOperator.MaxPointsPerSeries.Value);
			PlanExpression planExpression4 = this.CreateCountExpression(binnedLineSampleLimitOperator.MaxDynamicSeriesCount.Value);
			List<PlanBinnedLineSampleItem> list = new List<PlanBinnedLineSampleItem>(binnedLineSampleLimitOperator.Measures.Count);
			foreach (Expression expression in binnedLineSampleLimitOperator.Measures)
			{
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = this.m_context.OutputExpressionTable.GetNode(expression) as ResolvedCalculationReferenceExpressionNode;
				Contract.RetailAssert(resolvedCalculationReferenceExpressionNode != null, "Expected measure to be ResolvedCalculationReferenceExpressionNode");
				list.Add(resolvedCalculationReferenceExpressionNode.Calculation.ToBinnedLineSampleItem());
			}
			PlanBinnedLineSampleItem planBinnedLineSampleItem;
			if (binnedLineSampleLimitOperator.PrimaryScalarKey != null)
			{
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode2 = this.m_context.OutputExpressionTable.GetNode(binnedLineSampleLimitOperator.PrimaryScalarKey) as ResolvedCalculationReferenceExpressionNode;
				Contract.RetailAssert(resolvedCalculationReferenceExpressionNode2 != null, "Expected PrimaryScalarKey to be ResolvedCalculationReferenceExpressionNode");
				planBinnedLineSampleItem = resolvedCalculationReferenceExpressionNode2.Calculation.ToBinnedLineSampleItem();
			}
			else
			{
				planBinnedLineSampleItem = this.m_dsContext.PrimaryDynamicsExcludingContextOnly.Single("Expected one primary dynamic for binned line sample limit operator", Array.Empty<string>()).ToBinnedLineSampleItem();
			}
			IReadOnlyList<PlanBinnedLineSampleMember> readOnlyList = (this.m_dsContext.SecondaryDynamicsExcludingContextOnly.IsNullOrEmpty<DataMember>() ? null : this.m_dsContext.SecondaryDynamicsExcludingContextOnly.ToBinnedLineSampleItems());
			return this.m_input.Table.BinnedLineSample(planBinnedLineSampleItem, list, readOnlyList, planExpression, planExpression2, planExpression3, planExpression4);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004ABC8 File Offset: 0x00048DC8
		private PlanExpression CreateCountExpression(int count)
		{
			return BatchDataSetPlanningUtils.CreateLimitCountExpression(count, this.m_limit.Id, this.m_context.ErrorContext);
		}

		// Token: 0x04000885 RID: 2181
		private readonly ICommonPlanningContext m_context;

		// Token: 0x04000886 RID: 2182
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000887 RID: 2183
		private readonly DataShapeContext m_dsContext;

		// Token: 0x04000888 RID: 2184
		private readonly PlanOperationContext m_input;

		// Token: 0x04000889 RID: 2185
		private readonly Limit m_limit;
	}
}
