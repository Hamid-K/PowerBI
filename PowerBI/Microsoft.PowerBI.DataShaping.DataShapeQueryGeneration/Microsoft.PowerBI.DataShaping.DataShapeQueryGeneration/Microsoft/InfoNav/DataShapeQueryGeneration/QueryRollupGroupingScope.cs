using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BE RID: 190
	internal sealed class QueryRollupGroupingScope
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00019CCB File Offset: 0x00017ECB
		private QueryRollupGroupingScope(QueryMemberBuilder builder)
		{
			this.Builders = new List<QueryMemberBuilder>();
			this.Builders.Add(builder);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00019CEA File Offset: 0x00017EEA
		internal List<QueryMemberBuilder> Builders { get; }

		// Token: 0x060006EA RID: 1770 RVA: 0x00019CF4 File Offset: 0x00017EF4
		internal bool TryAddGroupKeyExpressions(HashSet<ExpressionNode> uniqueExpressions)
		{
			bool flag = false;
			foreach (QueryMemberBuilder queryMemberBuilder in this.Builders)
			{
				flag |= queryMemberBuilder.Group.TryAddGroupKeyExpressions(uniqueExpressions);
			}
			return flag;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00019D54 File Offset: 0x00017F54
		internal void AddNonGroupKeyExpressions(HashSet<ExpressionNode> uniqueExpressions)
		{
			foreach (QueryMemberBuilder queryMemberBuilder in this.Builders)
			{
				queryMemberBuilder.Group.AddNonGroupKeyExpressions(uniqueExpressions);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019DAC File Offset: 0x00017FAC
		internal void SuppressAllRollupsIfNeeded(bool suppressSortByMeasureRollup)
		{
			foreach (QueryMemberBuilder queryMemberBuilder in this.Builders)
			{
				queryMemberBuilder.SuppressSubtotals();
				queryMemberBuilder.ClearInstanceFilters();
				if (suppressSortByMeasureRollup)
				{
					queryMemberBuilder.SupressSortByMeasureRollup();
				}
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00019E10 File Offset: 0x00018010
		internal static List<QueryRollupGroupingScope> CreateGroupingScopes(IList<QueryMemberBuilder> builders)
		{
			List<QueryRollupGroupingScope> list = new List<QueryRollupGroupingScope>();
			QueryRollupGroupingScope queryRollupGroupingScope = null;
			foreach (QueryMemberBuilder queryMemberBuilder in builders)
			{
				if (queryMemberBuilder.Group.HasSubtotal)
				{
					queryRollupGroupingScope = new QueryRollupGroupingScope(queryMemberBuilder);
					list.Add(queryRollupGroupingScope);
				}
				else if (queryRollupGroupingScope == null)
				{
					list.Add(new QueryRollupGroupingScope(queryMemberBuilder));
				}
				else
				{
					queryRollupGroupingScope.Builders.Add(queryMemberBuilder);
				}
			}
			return list;
		}
	}
}
