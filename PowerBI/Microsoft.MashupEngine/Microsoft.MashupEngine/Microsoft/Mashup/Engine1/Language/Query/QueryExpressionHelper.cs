using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188E RID: 6286
	internal static class QueryExpressionHelper
	{
		// Token: 0x06009F7A RID: 40826 RVA: 0x0020EE58 File Offset: 0x0020D058
		public static bool TryGetColumnComparison(this QueryExpression expr, out int column, out BinaryOperator2 op, out Value value)
		{
			BinaryQueryExpression binaryQueryExpression = expr as BinaryQueryExpression;
			if (binaryQueryExpression != null)
			{
				if (binaryQueryExpression.Left.TryGetColumnAccess(out column) && binaryQueryExpression.Right.TryGetConstant(out value))
				{
					op = binaryQueryExpression.Operator;
					return true;
				}
				if (binaryQueryExpression.Right.TryGetColumnAccess(out column) && binaryQueryExpression.Left.TryGetConstant(out value))
				{
					op = binaryQueryExpression.Operator.SwapOperands();
					return true;
				}
			}
			column = 0;
			op = BinaryOperator2.Add;
			value = null;
			return false;
		}

		// Token: 0x06009F7B RID: 40827 RVA: 0x0020EECC File Offset: 0x0020D0CC
		public static bool TryGetInvocation(this QueryExpression expr, FunctionValue function, out QueryExpression argument0)
		{
			argument0 = null;
			InvocationQueryExpression invocationQueryExpression = expr as InvocationQueryExpression;
			if (invocationQueryExpression == null || invocationQueryExpression.Arguments.Count != 1)
			{
				return false;
			}
			ConstantQueryExpression constantQueryExpression = invocationQueryExpression.Function as ConstantQueryExpression;
			if (constantQueryExpression == null || !constantQueryExpression.Value.Equals(function))
			{
				return false;
			}
			argument0 = invocationQueryExpression.Arguments[0];
			return true;
		}

		// Token: 0x06009F7C RID: 40828 RVA: 0x0020EF24 File Offset: 0x0020D124
		public static bool TryGetInt32Constant(this QueryExpression expr, out int value)
		{
			Value value2;
			if (expr.TryGetConstant(out value2) && value2.IsNumber && value2.AsNumber.TryGetInt32(out value))
			{
				return true;
			}
			value = 0;
			return false;
		}
	}
}
