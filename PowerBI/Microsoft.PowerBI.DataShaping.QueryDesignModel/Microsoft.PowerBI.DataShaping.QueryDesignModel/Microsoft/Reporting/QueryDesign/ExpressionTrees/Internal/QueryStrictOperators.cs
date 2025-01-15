using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BB RID: 443
	internal static class QueryStrictOperators
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
		internal static QueryExpression GreaterThanStrict(QueryExpression left, QueryExpression right)
		{
			if (right.IsDaxLiteralBlank())
			{
				return left.GreaterThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.False, left.Equal(right)));
			}
			if (right is QueryLiteralExpression)
			{
				return left.GreaterThan(right);
			}
			return left.GreaterThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.False, right.IsNull().And(left.Equal(right))));
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0003D640 File Offset: 0x0003B840
		internal static QueryExpression GreaterThanOrEqualStrict(QueryExpression left, QueryExpression right)
		{
			if (right.IsDaxLiteralBlank())
			{
				return left.GreaterThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, left.Equal(right)));
			}
			if (right.IsDaxLiteralBlankEquivalent())
			{
				return left.GreaterThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, Literals.False));
			}
			if (right is QueryLiteralExpression)
			{
				return left.GreaterThanOrEqual(right);
			}
			return left.GreaterThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, right.IsNull().And(left.Equal(right))));
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0003D6D8 File Offset: 0x0003B8D8
		internal static QueryExpression LessThanStrict(QueryExpression left, QueryExpression right)
		{
			if (right.IsDaxLiteralBlankEquivalent())
			{
				return left.LessThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.False, left.Equal(right)));
			}
			if (right is QueryLiteralExpression || right is QueryNullExpression)
			{
				return left.LessThan(right);
			}
			return left.LessThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.False, right.IsNull().Not().And(left.Equal(right))));
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0003D754 File Offset: 0x0003B954
		internal static QueryExpression LessThanOrEqualStrict(QueryExpression left, QueryExpression right)
		{
			if (right.IsDaxLiteralBlank())
			{
				return left.LessThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, Literals.False));
			}
			if (right.IsDaxLiteralBlankEquivalent())
			{
				return left.LessThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, left.Equal(right)));
			}
			if (right is QueryLiteralExpression)
			{
				return left.LessThanOrEqual(right);
			}
			return left.LessThan(right).Or(QueryStrictOperators.Switch(left, right, Literals.True, right.IsNull().Not().And(left.Equal(right))));
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0003D7EE File Offset: 0x0003B9EE
		internal static QueryExpression EqualStrict(QueryExpression left, QueryExpression right)
		{
			if (!right.IsDaxLiteralBlank() && !right.IsDaxLiteralBlankEquivalent() && right is QueryLiteralExpression)
			{
				return left.Equal(right);
			}
			return QueryStrictOperators.Switch(left, right, Literals.True, Literals.False);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0003D824 File Offset: 0x0003BA24
		private static QuerySwitchExpression Switch(QueryExpression input, QueryExpression value1, QueryExpression result1, QueryExpression defaultResult)
		{
			List<QuerySwitchCase> list = new List<QuerySwitchCase>(1)
			{
				new QuerySwitchCase(value1, result1)
			};
			return input.Switch(list.AsReadOnly(), defaultResult);
		}
	}
}
