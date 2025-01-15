using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D0 RID: 464
	internal sealed class GroupByRegroupedAggregateExpressionRewriter : ExpressionNodeTreeTransform
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0004365E File Offset: 0x0004185E
		private GroupByRegroupedAggregateExpressionRewriter(WritableExpressionTable expressionTable, TranslationErrorContext errorContext, CalculationExpressionMap calculationMap, AggregateGroupByTable aggregateTable)
			: base(false)
		{
			this.m_errorContext = errorContext;
			this.m_expressionTable = expressionTable;
			this.m_calculationMap = calculationMap;
			this.m_aggregateTable = aggregateTable;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00043684 File Offset: 0x00041884
		public static void Rewrite(IReadOnlyList<Calculation> aggregates, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })] IReadOnlyList<global::System.ValueTuple<SortKey, string>> aggregateSortKeys, WritableExpressionTable expressionTable, TranslationErrorContext errorContext, CalculationExpressionMap calculationMap, AggregateGroupByTable aggregateTable)
		{
			GroupByRegroupedAggregateExpressionRewriter groupByRegroupedAggregateExpressionRewriter = new GroupByRegroupedAggregateExpressionRewriter(expressionTable, errorContext, calculationMap, aggregateTable);
			groupByRegroupedAggregateExpressionRewriter.Rewrite(aggregates);
			groupByRegroupedAggregateExpressionRewriter.Rewrite(aggregateSortKeys);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000436A0 File Offset: 0x000418A0
		private void Rewrite(IReadOnlyList<Calculation> aggregateCalcs)
		{
			foreach (Calculation calculation in aggregateCalcs)
			{
				this.m_expressionContext = new ExpressionContext(this.m_errorContext, calculation.ObjectType, calculation.Id, "Value");
				foreach (ExpressionId expressionId in this.m_calculationMap.GetExpressions(calculation))
				{
					this.RewriteExpression(expressionId);
				}
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00043748 File Offset: 0x00041948
		private void Rewrite([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })] IReadOnlyList<global::System.ValueTuple<SortKey, string>> aggregateSortKeys)
		{
			foreach (global::System.ValueTuple<SortKey, string> valueTuple in aggregateSortKeys)
			{
				SortKey item = valueTuple.Item1;
				this.m_expressionContext = new ExpressionContext(this.m_errorContext, item.ObjectType, valueTuple.Item2, "Value");
				this.RewriteExpression(item.Value.ExpressionId.Value);
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000437D0 File Offset: 0x000419D0
		private void RewriteExpression(ExpressionId exprId)
		{
			ExpressionNode expressionNode = this.m_expressionTable.GetNode(exprId);
			expressionNode = this.Visit(expressionNode);
			this.m_expressionTable.SetNode(exprId, expressionNode);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000437FF File Offset: 0x000419FF
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.FunctionCategory == FunctionCategory.Aggregate)
			{
				return this.VisitAggregate(node);
			}
			return base.Visit(node);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00043820 File Offset: 0x00041A20
		private ExpressionNode VisitAggregate(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.Name == "PercentileInc" || node.Descriptor.Name == "PercentileExc" || node.Descriptor.Name == "Median" || node.Descriptor.Name == "Any")
			{
				Contract.RetailFail("Aggregate type {0} is invalid for subquery regrouping", node.Descriptor.Name);
				return node;
			}
			return this.RewriteAggregateForGroupBy(node);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000438A8 File Offset: 0x00041AA8
		private ExpressionNode RewriteAggregateForGroupBy(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode = node.Arguments[0];
			ExpressionNode expressionNode2 = this.Visit(expressionNode);
			return node.RewriteAggregateInGroupBy(expressionNode2, this.m_aggregateTable, this.m_expressionContext, this.m_expressionTable, false);
		}

		// Token: 0x0400079A RID: 1946
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x0400079B RID: 1947
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400079C RID: 1948
		private readonly CalculationExpressionMap m_calculationMap;

		// Token: 0x0400079D RID: 1949
		private readonly AggregateGroupByTable m_aggregateTable;

		// Token: 0x0400079E RID: 1950
		private ExpressionContext m_expressionContext;
	}
}
