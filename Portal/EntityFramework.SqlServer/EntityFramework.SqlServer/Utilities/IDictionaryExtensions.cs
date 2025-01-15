using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200001E RID: 30
	internal static class IDictionaryExtensions
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000EB18 File Offset: 0x0000CD18
		internal static void Add<TKey, TValue>(this IDictionary<TKey, IList<TValue>> map, TKey key, TValue value)
		{
			IList<TValue> list;
			if (!map.TryGetValue(key, out list))
			{
				list = new List<TValue>();
				map[key] = list;
			}
			list.Add(value);
		}
	}
}
