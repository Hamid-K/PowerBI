using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000068 RID: 104
	internal static class DsqSortKeyExtensions
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00011354 File Offset: 0x0000F554
		internal static bool Contains(this IReadOnlyList<DsqSortKey> orderBy, ExpressionNode expression)
		{
			return orderBy.FirstOrDefault(expression) != null;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011360 File Offset: 0x0000F560
		internal static DsqSortKey FirstOrDefault(this IReadOnlyList<DsqSortKey> orderBy, ExpressionNode expression)
		{
			for (int i = 0; i < orderBy.Count; i++)
			{
				DsqSortKey dsqSortKey = orderBy[i];
				if (dsqSortKey.MatchesExpression(expression))
				{
					return dsqSortKey;
				}
			}
			return null;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011392 File Offset: 0x0000F592
		internal static bool Contains(this IEnumerable<QueryGroupKey> keys, DsqSortKey orderBy)
		{
			return keys.Select((QueryGroupKey k) => k.Expression).Contains(orderBy);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000113C0 File Offset: 0x0000F5C0
		internal static bool Contains(this IEnumerable<QueryGroupValue> values, DsqSortKey orderBy)
		{
			using (IEnumerator<QueryGroupValue> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MatchesExpression(orderBy.Expression))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011414 File Offset: 0x0000F614
		internal static bool Contains(this IEnumerable<ProjectedDsqExpression> expressions, DsqSortKey orderBy)
		{
			return expressions.Select((ProjectedDsqExpression e) => e.Value.DsqExpression).Contains(orderBy);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011444 File Offset: 0x0000F644
		internal static bool Contains(this IEnumerable<ExpressionNode> expressions, DsqSortKey orderBy)
		{
			foreach (ExpressionNode expressionNode in expressions)
			{
				if (orderBy.MatchesExpression(expressionNode))
				{
					return true;
				}
			}
			return false;
		}
	}
}
