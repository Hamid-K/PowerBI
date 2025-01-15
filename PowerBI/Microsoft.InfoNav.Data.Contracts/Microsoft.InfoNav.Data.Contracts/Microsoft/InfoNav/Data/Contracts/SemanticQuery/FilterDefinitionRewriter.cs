using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.SemanticQuery
{
	// Token: 0x02000094 RID: 148
	internal static class FilterDefinitionRewriter
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00009790 File Offset: 0x00007990
		internal static FilterDefinition Rewrite(FilterDefinition filterDefinition, QueryExpressionRewriter expressionRewriter, Func<EntitySource, EntitySource> sourceRewriter = null)
		{
			if (filterDefinition == null)
			{
				return null;
			}
			List<EntitySource> list = ((sourceRewriter != null) ? filterDefinition.From.Rewrite(new Func<EntitySource, EntitySource>(sourceRewriter.Invoke)) : filterDefinition.From);
			List<QueryFilter> list2 = filterDefinition.Where.Rewrite((QueryFilter f) => QueryDefinitionRewriter.RewriteFilter(f, expressionRewriter));
			if (list == filterDefinition.From && list2 == filterDefinition.Where)
			{
				return filterDefinition;
			}
			return new FilterDefinition
			{
				Version = filterDefinition.Version,
				From = list,
				Where = list2
			};
		}
	}
}
