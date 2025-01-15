using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders.DynamicLimits
{
	// Token: 0x020001E6 RID: 486
	internal static class DynamicLimitsExprNodeBuilderExtensions
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x00046283 File Offset: 0x00044483
		internal static ExpressionNode ToLiteralExpr(this int value)
		{
			return ExprNodes.Literal((long)value);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00046291 File Offset: 0x00044491
		internal static ExpressionNode ToLiteralExpr(this Candidate<int> value)
		{
			return value.Value.ToLiteralExpr();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0004629E File Offset: 0x0004449E
		internal static ExpressionNode NthRoot(this ExpressionNode input, ExpressionNode n)
		{
			return ExprNodes.Ceiling(ExprNodes.Power(input, LiteralExpressionNode.OneInt64.Divide(n)), null);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000462B7 File Offset: 0x000444B7
		internal static ExpressionNode NthRoot(this ExpressionNode input, int n)
		{
			if (n == 1)
			{
				return input;
			}
			return ExprNodes.Power(input, ExprNodes.Literal(1.0 / (double)n));
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000462DB File Offset: 0x000444DB
		internal static ExpressionNode AddCoalesce(this ExpressionNode left, ExpressionNode right)
		{
			if (left == null)
			{
				return right;
			}
			return left.Add(right);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000462E9 File Offset: 0x000444E9
		internal static ExpressionNode MultiplyCoalesce(this ExpressionNode left, ExpressionNode right)
		{
			if (left == null)
			{
				return right;
			}
			return left.Multiply(right);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000462F7 File Offset: 0x000444F7
		internal static ExpressionNode DivideAndCeiling(this ExpressionNode dividend, ExpressionNode divisor)
		{
			return ExprNodes.Ceiling(dividend.Divide(divisor), null);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00046306 File Offset: 0x00044506
		internal static ExpressionNode DivideAndCeiling(this int dividend, ExpressionNode divisor)
		{
			return dividend.ToLiteralExpr().DivideAndCeiling(divisor);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00046314 File Offset: 0x00044514
		internal static ExpressionNode Subtract(this int left, ExpressionNode right)
		{
			return left.ToLiteralExpr().Subtract(right);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00046322 File Offset: 0x00044522
		internal static ExpressionNode LessThan(this ExpressionNode left, int right)
		{
			return left.LessThan(right.ToLiteralExpr());
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00046330 File Offset: 0x00044530
		internal static ExpressionNode LessThanOrEqualNoNan(this ExpressionNode left, ExpressionNode right)
		{
			if (left == right)
			{
				return LiteralExpressionNode.True;
			}
			return left.LessThanOrEqual(right);
		}
	}
}
