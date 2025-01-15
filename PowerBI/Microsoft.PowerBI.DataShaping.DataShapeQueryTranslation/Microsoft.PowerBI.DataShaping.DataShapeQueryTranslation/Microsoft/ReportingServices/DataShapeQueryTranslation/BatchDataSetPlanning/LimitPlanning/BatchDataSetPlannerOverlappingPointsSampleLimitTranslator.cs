using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x02000237 RID: 567
	internal sealed class BatchDataSetPlannerOverlappingPointsSampleLimitTranslator
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x0004ABE8 File Offset: 0x00048DE8
		private BatchDataSetPlannerOverlappingPointsSampleLimitTranslator(ICommonPlanningContext context, PlanDeclarationCollection declarations, WritableExpressionTable outputExpressionTable, DataShapeContext dsContext, PlanOperationContext input, Limit limit, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_dsContext = dsContext;
			this.m_input = input;
			this.m_limit = limit;
			this.m_limitMetadataBuilder = limitMetadataBuilder;
			this.m_limitInfoBuilder = limitInfoBuilder;
			this.m_namingContext = new NamingContext(null);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004AC44 File Offset: 0x00048E44
		public static PlanOperationContext Translate(ICommonPlanningContext context, PlanDeclarationCollection declarations, WritableExpressionTable outputExpressionTable, DataShapeContext dsContext, PlanOperationContext input, Limit limit, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			PlanOperation planOperation = new BatchDataSetPlannerOverlappingPointsSampleLimitTranslator(context, declarations, outputExpressionTable, dsContext, input, limit, limitMetadataBuilder, limitInfoBuilder).Translate();
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0004AC74 File Offset: 0x00048E74
		private PlanOperation Translate()
		{
			Dictionary<string, ExpressionId> dictionary;
			PlanOperation planOperation = this.BuildSampling(out dictionary);
			this.BuildLimitInfo(dictionary);
			return planOperation;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004AC90 File Offset: 0x00048E90
		private void BuildLimitInfo(Dictionary<string, ExpressionId> properties)
		{
			ExpressionNode expressionNode = this.m_input.CountRows();
			expressionNode = this.DeclareIfNotDeclared(expressionNode, PlanNames.IntersectionCount(this.m_dsContext.Id), "IntersectionCount");
			ExpressionId expressionId = this.m_limitMetadataBuilder.AddColumn(PlanNames.IntersectionCount(this.m_dsContext.Id), expressionNode, ObjectType.Limit, this.m_limit.Id);
			LimitOverride limitOverride = LimitOverride.OverrideLimit(this.m_limit.Id, null, new ExpressionId?(expressionId), new ExpressionId?(expressionId), properties, null);
			this.m_limitInfoBuilder.AddLimitOverride(limitOverride);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0004AD34 File Offset: 0x00048F34
		private PlanOperation BuildSampling(out Dictionary<string, ExpressionId> properties)
		{
			properties = null;
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = (OverlappingPointsSampleLimitOperator)this.m_limit.Operator;
			PlanExpression planExpression = BatchDataSetPlanningUtils.CreateLimitCountExpression(overlappingPointsSampleLimitOperator.Count.Value, this.m_limit.Id, this.m_context.ErrorContext);
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = ((overlappingPointsSampleLimitOperator.X != null) ? this.m_context.OutputExpressionTable.GetNodeAs<ResolvedCalculationReferenceExpressionNode>(overlappingPointsSampleLimitOperator.X.Key.ExpressionId.Value) : null);
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode2 = ((overlappingPointsSampleLimitOperator.Y != null) ? this.m_context.OutputExpressionTable.GetNodeAs<ResolvedCalculationReferenceExpressionNode>(overlappingPointsSampleLimitOperator.Y.Key.ExpressionId.Value) : null);
			AggregateGroupByTable aggregateGroupByTable = new AggregateGroupByTable(new AggregateReferenceTable(new TableReference(this.m_input, PlanNames.MinMax(this.m_dsContext.DataShape.Id), this.m_declarations, RowResultSetType.Unrestricted)), this.m_dsContext.DataShape, this.m_namingContext, true);
			PlanAggregateExpressionItem planAggregateExpressionItem;
			PlanAggregateExpressionItem planAggregateExpressionItem2;
			this.BuildMinMaxAggregateItems(aggregateGroupByTable, resolvedCalculationReferenceExpressionNode, overlappingPointsSampleLimitOperator.X, "X", out planAggregateExpressionItem, out planAggregateExpressionItem2);
			PlanAggregateExpressionItem planAggregateExpressionItem3;
			PlanAggregateExpressionItem planAggregateExpressionItem4;
			this.BuildMinMaxAggregateItems(aggregateGroupByTable, resolvedCalculationReferenceExpressionNode2, overlappingPointsSampleLimitOperator.Y, "Y", out planAggregateExpressionItem3, out planAggregateExpressionItem4);
			PlanOperation planOperation = null;
			if (aggregateGroupByTable.HasAggregates)
			{
				planOperation = aggregateGroupByTable.ToPlanOperation(this.m_context.Annotations, this.m_context.ScopeTree);
				planOperation = planOperation.DeclareIfNotDeclared(PlanNames.MinMax(this.m_limit.Operator.ObjectType.ToString()), this.m_declarations, false, false, null, false);
			}
			PlanExpression planExpression2 = this.GenerateAxisReferenceWithLog(planOperation, resolvedCalculationReferenceExpressionNode, overlappingPointsSampleLimitOperator.X, "X", planAggregateExpressionItem, planAggregateExpressionItem2, ref properties);
			PlanExpression planExpression3 = this.GenerateAxisReferenceWithLog(planOperation, resolvedCalculationReferenceExpressionNode2, overlappingPointsSampleLimitOperator.Y, "Y", planAggregateExpressionItem3, planAggregateExpressionItem4, ref properties);
			return this.m_input.Table.OverlappingPointsSample(planExpression2, planExpression3, planExpression);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004AF0D File Offset: 0x0004910D
		private ExpressionContext CreateExpressionContext(string propertyName)
		{
			return new ExpressionContext(this.m_context.ErrorContext, ObjectType.OverlappingPointsSampleLimitOperator, this.m_limit.Id, propertyName);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004AF30 File Offset: 0x00049130
		private PlanExpression GenerateAxisReferenceWithLog(PlanOperation aggPlan, ResolvedCalculationReferenceExpressionNode calcReference, LimitPlotAxis axis, string propertyName, PlanAggregateExpressionItem minItem, PlanAggregateExpressionItem maxItem, ref Dictionary<string, ExpressionId> properties)
		{
			if (calcReference == null || axis == null)
			{
				return null;
			}
			if (axis.Transform != DataReductionPlotAxisTransform.Log || aggPlan == null)
			{
				return new PlanExpression(calcReference, this.CreateExpressionContext(propertyName));
			}
			Identifier id = calcReference.Calculation.Id;
			ExpressionNode expressionNode = aggPlan.ExtractScalarFromSingleRowTable(minItem.PlanName, PlanNames.Min(id), this.m_declarations);
			ExpressionNode expressionNode2 = aggPlan.ExtractScalarFromSingleRowTable(maxItem.PlanName, PlanNames.Max(id), this.m_declarations);
			ExpressionNode expressionNode3 = expressionNode.GreaterThan(LiteralExpressionNode.ZeroInt64);
			ExpressionNode expressionNode4 = expressionNode2.GreaterThan(LiteralExpressionNode.ZeroInt64);
			ExpressionNode expressionNode5 = expressionNode3.And(expressionNode4);
			expressionNode5 = this.DeclareIfNotDeclared(expressionNode5, PlanNames.CanApplyLog(id), propertyName);
			ExpressionNode expressionNode6 = expressionNode.LessThan(LiteralExpressionNode.ZeroInt64);
			ExpressionNode expressionNode7 = expressionNode2.LessThan(LiteralExpressionNode.ZeroInt64);
			ExpressionNode expressionNode8 = expressionNode6.And(expressionNode7);
			expressionNode8 = this.DeclareIfNotDeclared(expressionNode8, PlanNames.CanApplyNegativeLog(id), propertyName);
			string text = PlanNames.TransformApplied(propertyName);
			ExpressionId expressionId = this.m_limitMetadataBuilder.AddColumn(text, expressionNode5.Or(expressionNode8), ObjectType.Limit, this.m_limit.Id);
			Util.AddToLazyDictionary<string, ExpressionId>(ref properties, text, expressionId, null);
			this.m_limitInfoBuilder.AddTelemetryItem(new LimitTelemetryItem(text, expressionId));
			return new PlanExpression(expressionNode5.If(ExprNodes.Log10(calcReference), expressionNode8.If(ExprNodes.Log10(calcReference.Minus()).Minus(), calcReference)).IfError(calcReference), this.CreateExpressionContext(propertyName));
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004B098 File Offset: 0x00049298
		private void BuildMinMaxAggregateItems(AggregateGroupByTable aggTable, ResolvedCalculationReferenceExpressionNode calcReference, LimitPlotAxis axis, string propertyName, out PlanAggregateExpressionItem aggregateItemMin, out PlanAggregateExpressionItem aggregateItemMax)
		{
			if (calcReference == null || axis == null || axis.Transform != DataReductionPlotAxisTransform.Log)
			{
				aggregateItemMax = null;
				aggregateItemMin = null;
				return;
			}
			ExpressionNode expressionNode = ExprNodes.CurrentGroup(calcReference);
			FunctionCallExpressionNode functionCallExpressionNode = ExprNodes.Max(new ExpressionNode[] { expressionNode });
			FunctionCallExpressionNode functionCallExpressionNode2 = ExprNodes.Min(new ExpressionNode[] { expressionNode });
			aggregateItemMax = aggTable.AddOrReuseAggregate(PlanNames.Argument("Max"), functionCallExpressionNode, this.m_outputExpressionTable, new ExpressionContext(this.m_context.ErrorContext, ObjectType.Calculation, "Max", propertyName), false);
			aggregateItemMin = aggTable.AddOrReuseAggregate(PlanNames.Argument("Min"), functionCallExpressionNode2, this.m_outputExpressionTable, new ExpressionContext(this.m_context.ErrorContext, ObjectType.Calculation, "Min", propertyName), false);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0004B156 File Offset: 0x00049356
		private ExpressionNode DeclareIfNotDeclared(ExpressionNode expr, string declarationName, Identifier objectId)
		{
			return expr.DeclareIfNotDeclared(this.m_namingContext.GenerateUniqueName(declarationName), this.m_declarations, this.m_context.ErrorContext, ObjectType.Limit, objectId);
		}

		// Token: 0x0400088A RID: 2186
		private readonly ICommonPlanningContext m_context;

		// Token: 0x0400088B RID: 2187
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400088C RID: 2188
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400088D RID: 2189
		private readonly DataShapeContext m_dsContext;

		// Token: 0x0400088E RID: 2190
		private readonly PlanOperationContext m_input;

		// Token: 0x0400088F RID: 2191
		private readonly Limit m_limit;

		// Token: 0x04000890 RID: 2192
		private readonly NamingContext m_namingContext;

		// Token: 0x04000891 RID: 2193
		private readonly LimitMetadataTableBuilder m_limitMetadataBuilder;

		// Token: 0x04000892 RID: 2194
		private readonly PlanLimitInfoBuilder m_limitInfoBuilder;
	}
}
