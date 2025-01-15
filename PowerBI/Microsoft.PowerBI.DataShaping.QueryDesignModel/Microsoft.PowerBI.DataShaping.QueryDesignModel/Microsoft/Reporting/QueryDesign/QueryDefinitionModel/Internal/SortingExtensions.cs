using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200011B RID: 283
	internal static class SortingExtensions
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0002C634 File Offset: 0x0002A834
		internal static IEnumerable<SortItem> OmitMissingGroups(this IEnumerable<SortItem> sortItems, IEnumerable<Group> groups, Rollup rollup)
		{
			SortingExtensions.<>c__DisplayClass0_0 CS$<>8__locals1 = new SortingExtensions.<>c__DisplayClass0_0();
			CS$<>8__locals1.groups = groups;
			SortingExtensions.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<RollupGroup> enumerable;
			if (rollup != null)
			{
				IEnumerable<RollupGroup> rollupGroups = rollup.RollupGroups;
				enumerable = rollupGroups;
			}
			else
			{
				enumerable = Enumerable.Empty<RollupGroup>();
			}
			CS$<>8__locals2.rollupGroups = enumerable;
			return sortItems.Where(delegate(SortItem item)
			{
				Func<GroupDetail, bool> <>9__3;
				return CS$<>8__locals1.groups.Any(delegate(Group g)
				{
					if (!item.RefersTo(g))
					{
						IEnumerable<GroupDetail> details = g.Details;
						Func<GroupDetail, bool> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (GroupDetail d) => item.RefersTo(d));
						}
						return details.Any(func);
					}
					return true;
				}) || CS$<>8__locals1.rollupGroups.Any((RollupGroup rg) => item.RefersTo(rg));
			});
		}
	}
}
