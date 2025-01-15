using System;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x020001AD RID: 429
	public static class HashSetHelpers
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		public static T[] ToArray<T>(this HashSet<T> hashSet)
		{
			T[] array = new T[hashSet.Count];
			int num = 0;
			foreach (T t in hashSet)
			{
				array[num++] = t;
			}
			return array;
		}
	}
}
