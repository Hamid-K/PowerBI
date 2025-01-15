using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013E RID: 318
	internal static class DaxOperators
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x00030A53 File Offset: 0x0002EC53
		internal static DaxExpression Equal(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("=", left, right);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00030A61 File Offset: 0x0002EC61
		internal static DaxExpression GreaterThan(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator(">", left, right);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00030A6F File Offset: 0x0002EC6F
		internal static DaxExpression GreaterThanOrEquals(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator(">=", left, right);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00030A7D File Offset: 0x0002EC7D
		internal static DaxExpression LessThan(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("<", left, right);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00030A8B File Offset: 0x0002EC8B
		internal static DaxExpression LessThanOrEquals(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("<=", left, right);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00030A99 File Offset: 0x0002EC99
		internal static DaxExpression NotEqual(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("<>", left, right);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00030AA7 File Offset: 0x0002ECA7
		internal static DaxExpression Plus(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("+", left, right);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00030AB5 File Offset: 0x0002ECB5
		internal static DaxExpression Minus(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("-", left, right);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00030AC3 File Offset: 0x0002ECC3
		internal static DaxExpression MinusSign(DaxExpression arg)
		{
			return DaxExpression.Scalar(string.Concat(new object[] { "-", "(", arg, ")" }));
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00030AF1 File Offset: 0x0002ECF1
		internal static DaxExpression Multiply(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("*", left, right);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00030AFF File Offset: 0x0002ECFF
		internal static DaxExpression Divide(DaxExpression left, DaxExpression right)
		{
			return DaxExpression.Scalar(string.Concat(new string[]
			{
				"DIVIDE(",
				left.ToString(),
				", ",
				right.ToString(),
				")"
			}));
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00030B3B File Offset: 0x0002ED3B
		internal static DaxExpression DivideRaw(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToBinaryExprWithOperator("/", left, right);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00030B49 File Offset: 0x0002ED49
		internal static DaxExpression In(DaxExpression left, DaxExpression right)
		{
			return DaxOperators.ToFlatOperator("IN", left, right);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00030B57 File Offset: 0x0002ED57
		internal static DaxExpression And(IReadOnlyList<DaxExpression> arguments)
		{
			return DaxOperators.InvokeOperator(DaxOperators.AndOperator, arguments);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00030B64 File Offset: 0x0002ED64
		internal static DaxExpression Concatenate(IReadOnlyList<DaxExpression> arguments)
		{
			return DaxOperators.InvokeOperator(DaxOperators.ConcatenateOperator, arguments);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00030B71 File Offset: 0x0002ED71
		private static DaxExpression InvokeOperator(DaxOperatorFormatter daxOperator, IReadOnlyList<DaxExpression> arguments)
		{
			return DaxExpression.Scalar(daxOperator.Invoke(arguments));
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00030B7F File Offset: 0x0002ED7F
		private static DaxExpression ToFlatOperator(string @operator, DaxExpression left, DaxExpression right)
		{
			return DaxExpression.Scalar(string.Concat(new string[]
			{
				left.ToString(),
				" ",
				@operator,
				" ",
				right.ToString()
			}));
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00030BB8 File Offset: 0x0002EDB8
		private static DaxExpression ToBinaryExprWithOperator(string @operator, DaxExpression left, DaxExpression right)
		{
			return DaxExpression.Scalar(string.Concat(new string[]
			{
				"(",
				left.ToString(),
				" ",
				@operator,
				" ",
				right.ToString(),
				")"
			}));
		}

		// Token: 0x04000AD3 RID: 2771
		private static readonly DaxOperatorFormatter AndOperator = new DaxOperatorFormatter("AND", " && ", false);

		// Token: 0x04000AD4 RID: 2772
		private static readonly DaxOperatorFormatter ConcatenateOperator = new DaxOperatorFormatter("Concatenate", " & ", false);
	}
}
