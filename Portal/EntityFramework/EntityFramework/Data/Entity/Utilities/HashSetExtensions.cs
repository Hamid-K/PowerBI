using System;
using System.Collections.Generic;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007F RID: 127
	internal static class HashSetExtensions
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0001006C File Offset: 0x0000E26C
		public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				set.Add(t);
			}
		}
	}
}
