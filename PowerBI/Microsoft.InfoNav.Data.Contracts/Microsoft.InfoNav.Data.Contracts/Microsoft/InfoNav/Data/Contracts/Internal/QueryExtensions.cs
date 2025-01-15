using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B3 RID: 691
	public static class QueryExtensions
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x000290A0 File Offset: 0x000272A0
		public static bool IsPodQuery(this QueryDefinition query)
		{
			List<EntitySource> from = query.From;
			if (from != null)
			{
				for (int i = 0; i < from.Count; i++)
				{
					if (from[i].Type == EntitySourceType.Pod)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
