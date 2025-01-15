using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000183 RID: 387
	internal static class FilterUtils
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x00037C47 File Offset: 0x00035E47
		internal static BinaryFilterCondition CreateBooleanColumnFilterCondition(WritableExpressionTable expressionTable, string columnName, bool value)
		{
			return new BinaryFilterCondition
			{
				LeftExpression = FilterUtils.CreateColumnReferenceExpression(expressionTable, columnName),
				Operator = BinaryFilterOperator.Equal,
				RightExpression = FilterUtils.CreateLiteralExpression(expressionTable, value)
			};
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00037C7C File Offset: 0x00035E7C
		internal static FilterCondition CreateConditionForColumnReference(WritableExpressionTable expressionTable, string columnName, bool value)
		{
			UnaryFilterCondition unaryFilterCondition = new UnaryFilterCondition
			{
				Expression = FilterUtils.CreateColumnReferenceExpression(expressionTable, columnName)
			};
			if (!value)
			{
				unaryFilterCondition.Not = true;
			}
			return unaryFilterCondition;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00037CAC File Offset: 0x00035EAC
		internal static CompoundFilterCondition CreateCompoundConditionForColumnReferences(WritableExpressionTable expressionTable, string[] columnNames, bool[] values)
		{
			List<FilterCondition> list = new List<FilterCondition>();
			for (int i = 0; i < columnNames.Length; i++)
			{
				list.Add(FilterUtils.CreateConditionForColumnReference(expressionTable, columnNames[i], values[i]));
			}
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list
			};
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00037CF8 File Offset: 0x00035EF8
		internal static Expression CreateColumnReferenceExpression(WritableExpressionTable expressionTable, string columnName)
		{
			BatchColumnReferenceExpressionNode batchColumnReferenceExpressionNode = new BatchColumnReferenceExpressionNode(columnName);
			ExpressionId expressionId = expressionTable.Add(batchColumnReferenceExpressionNode);
			return new Expression(ExprNodes.Literal(PlanNames.Column(columnName)), new ExpressionId?(expressionId));
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00037D2C File Offset: 0x00035F2C
		internal static Expression CreateLiteralExpression(WritableExpressionTable expressionTable, object value)
		{
			LiteralExpressionNode literalExpressionNode = new LiteralExpressionNode(new ScalarValue(value));
			ExpressionId expressionId = expressionTable.Add(literalExpressionNode);
			return new Expression(ExprNodes.Literal(value), new ExpressionId?(expressionId));
		}
	}
}
