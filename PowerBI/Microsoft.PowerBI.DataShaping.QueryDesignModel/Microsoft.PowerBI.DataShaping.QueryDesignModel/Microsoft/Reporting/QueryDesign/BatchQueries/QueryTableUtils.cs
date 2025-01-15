using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.QueryDesignModel.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027E RID: 638
	internal static class QueryTableUtils
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x0004CC64 File Offset: 0x0004AE64
		internal static QueryTableGroupBuilder FindGroupBuilderForDetail(this IList<QueryTableGroupBuilder> groupBuilders, int endIndex, QueryExpression expression)
		{
			for (int i = endIndex; i >= 0; i--)
			{
				if (groupBuilders[i].CanAddGroupDetail(expression))
				{
					return groupBuilders[i];
				}
			}
			throw new InvalidOperationException("Did not find compatible group builder for query expression.");
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0004CC9E File Offset: 0x0004AE9E
		internal static IEnumerable<QueryTableColumn> GetSubtotalIndicatorColumns(this IEnumerable<QueryTableGroupBuilderBase> builders)
		{
			return builders.OfType<QueryTableRollupBuilder>().SelectMany((QueryTableRollupBuilder b) => b.GroupBuilders.Select((QueryTableGroupBuilder gb) => gb.SubtotalIndicatorColumn));
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0004CCCC File Offset: 0x0004AECC
		internal static void RemoveDuplicateKeys<TGroupingKey>(List<TGroupingKey> keys, HashSet<TGroupingKey> existingKeys)
		{
			for (int i = 0; i < keys.Count; i++)
			{
				TGroupingKey tgroupingKey = keys[i];
				if (!existingKeys.Add(tgroupingKey))
				{
					keys.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0004CD08 File Offset: 0x0004AF08
		internal static void RemoveDuplicatedColumnsAndEmptyGroups<TGroupBuilder, TGroupingKey>(this List<TGroupBuilder> groupBuilders, HashSet<TGroupingKey> existingKeys = null) where TGroupBuilder : IGroupBuilder<TGroupingKey>
		{
			if (existingKeys == null)
			{
				existingKeys = new HashSet<TGroupingKey>();
			}
			groupBuilders.RemoveDuplicatedKeysAndEmptyGroups(delegate(TGroupBuilder groupBuilder)
			{
				groupBuilder.RemoveDuplicateKeys(existingKeys);
			});
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0004CD48 File Offset: 0x0004AF48
		internal static void RemoveDuplicatedKeysAndEmptyGroups<TGroupBuilder>(this List<TGroupBuilder> groupBuilders, Action<TGroupBuilder> removeDuplicatedKeys) where TGroupBuilder : IGroupBuilder
		{
			for (int i = 0; i < groupBuilders.Count; i++)
			{
				TGroupBuilder tgroupBuilder = groupBuilders[i];
				if (removeDuplicatedKeys != null)
				{
					removeDuplicatedKeys(tgroupBuilder);
				}
				TGroupBuilder tgroupBuilder2 = groupBuilders[i];
				if (tgroupBuilder2.IsEmpty)
				{
					groupBuilders.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0004CD9A File Offset: 0x0004AF9A
		internal static bool IsNonEmptyTable(QueryExpression expression)
		{
			return expression is QueryNewTableExpression;
		}
	}
}
