using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NLog.Internal
{
	// Token: 0x0200010F RID: 271
	internal static class CollectionExtensions
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00024298 File Offset: 0x00022498
		[NotNull]
		public static IList<TItem> Filter<TItem, TState>([NotNull] this IList<TItem> items, TState state, Func<TItem, TState, bool> filter)
		{
			bool flag = false;
			IList<TItem> list = null;
			for (int i = 0; i < items.Count; i++)
			{
				TItem titem = items[i];
				if (filter(titem, state))
				{
					if (flag && list == null)
					{
						list = new List<TItem>();
					}
					if (list != null)
					{
						list.Add(titem);
					}
				}
				else
				{
					if (!flag && i > 0)
					{
						list = new List<TItem>();
						for (int j = 0; j < i; j++)
						{
							list.Add(items[j]);
						}
					}
					flag = true;
				}
			}
			IList<TItem> list2;
			if ((list2 = list) == null)
			{
				if (!flag)
				{
					return items;
				}
				IList<TItem> list3 = ArrayHelper.Empty<TItem>();
				list2 = list3;
			}
			return list2;
		}
	}
}
