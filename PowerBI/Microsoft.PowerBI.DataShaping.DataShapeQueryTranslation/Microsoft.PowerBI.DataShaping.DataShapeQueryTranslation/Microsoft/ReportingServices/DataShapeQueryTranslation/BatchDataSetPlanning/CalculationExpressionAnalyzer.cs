using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200017D RID: 381
	internal class CalculationExpressionAnalyzer : DataShapeVisitor
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00037683 File Offset: 0x00035883
		protected CalculationExpressionAnalyzer(WritableExpressionTable expressionTable)
		{
			this.m_expressionTable = expressionTable;
			this.m_calculationMap = new WritableCalculationExpressionMap();
			this.m_filterVisitor = new CalculationExpressionAnalyzer.FilterVisitor(delegate(DataShape dataShape, ObjectType filterConditionType)
			{
				this.Visit(dataShape);
			});
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000376B4 File Offset: 0x000358B4
		public static CalculationExpressionMap Analyze(DataShape dataShape, WritableExpressionTable expressionTable)
		{
			CalculationExpressionAnalyzer calculationExpressionAnalyzer = new CalculationExpressionAnalyzer(expressionTable);
			calculationExpressionAnalyzer.Visit(dataShape);
			return calculationExpressionAnalyzer.m_calculationMap;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000376C8 File Offset: 0x000358C8
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			this.m_filterVisitor.VisitFilter(filter);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000376D8 File Offset: 0x000358D8
		protected override void Visit(Calculation calculation)
		{
			ExpressionNode expressionNode = this.m_expressionTable.GetNode(calculation.Value);
			expressionNode = this.VisitNode(calculation, expressionNode, new ExpressionId?(calculation.Value.ExpressionId.Value));
			this.m_expressionTable.SetNode(calculation.Value, expressionNode);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003772C File Offset: 0x0003592C
		private ExpressionNode VisitNode(Calculation calc, ExpressionNode node, ExpressionId? expressionId)
		{
			FunctionCallExpressionNode functionCallExpressionNode = node as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.UsageKind == FunctionUsageKind.Processing)
			{
				if (string.IsNullOrEmpty(calc.NativeReferenceName))
				{
					ExpressionNode[] array = new ExpressionNode[functionCallExpressionNode.Arguments.Count];
					for (int i = 0; i < array.Length; i++)
					{
						ExpressionNode expressionNode = functionCallExpressionNode.Arguments[i];
						ExpressionNode expressionNode2 = this.VisitNode(calc, expressionNode, null);
						array[i] = expressionNode2;
					}
					return new FunctionCallExpressionNode(functionCallExpressionNode.Descriptor, functionCallExpressionNode.UsageKind, array);
				}
				Contract.RetailAssert(functionCallExpressionNode.Descriptor.CanBeHandledByQuery, "The functional call must be able to be handled by the query if it has a native reference name.");
			}
			LiteralExpressionNode literalExpressionNode = node as LiteralExpressionNode;
			if (literalExpressionNode != null)
			{
				return literalExpressionNode;
			}
			if (expressionId != null)
			{
				this.m_calculationMap.AddExpression(calc, calc.Value.ExpressionId.Value);
				return node;
			}
			SubExpressionNode subExpressionNode = this.m_expressionTable.CreateSubExpression(node);
			this.m_calculationMap.AddExpression(calc, subExpressionNode.ExpressionId);
			return subExpressionNode;
		}

		// Token: 0x0400069D RID: 1693
		protected readonly WritableExpressionTable m_expressionTable;

		// Token: 0x0400069E RID: 1694
		protected readonly WritableCalculationExpressionMap m_calculationMap;

		// Token: 0x0400069F RID: 1695
		private readonly CalculationExpressionAnalyzer.FilterVisitor m_filterVisitor;

		// Token: 0x020002F4 RID: 756
		private sealed class FilterVisitor : FilterVisitor<FilterCondition>
		{
			// Token: 0x060016DD RID: 5853 RVA: 0x0005212E File Offset: 0x0005032E
			internal FilterVisitor(VisitDataShapeDelegate visitDataShape)
				: base(visitDataShape)
			{
			}

			// Token: 0x060016DE RID: 5854 RVA: 0x00052137 File Offset: 0x00050337
			public void VisitFilter(Filter filter)
			{
				base.Visit(filter);
			}

			// Token: 0x060016DF RID: 5855 RVA: 0x00052141 File Offset: 0x00050341
			internal override FilterCondition Visit(UnaryFilterCondition condition)
			{
				return condition;
			}

			// Token: 0x060016E0 RID: 5856 RVA: 0x00052144 File Offset: 0x00050344
			internal override FilterCondition Visit(BinaryFilterCondition condition)
			{
				return condition;
			}

			// Token: 0x060016E1 RID: 5857 RVA: 0x00052147 File Offset: 0x00050347
			internal override FilterCondition Visit(CompoundFilterCondition condition)
			{
				this.Visit(condition.Conditions, condition.ObjectType, "Conditions");
				return condition;
			}
		}
	}
}
