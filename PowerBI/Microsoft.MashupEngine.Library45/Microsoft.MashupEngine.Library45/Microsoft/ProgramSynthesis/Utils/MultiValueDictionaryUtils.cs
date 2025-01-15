using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004C5 RID: 1221
	public static class MultiValueDictionaryUtils
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x000521F3 File Offset: 0x000503F3
		public static MultiValueDictionary<TKey, TValue> ToMultiValueDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> enumerable, IEqualityComparer<TKey> comparer = null)
		{
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(comparer);
			multiValueDictionary.AddRange(enumerable);
			return multiValueDictionary;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00052204 File Offset: 0x00050404
		public static void AddRange<TKey, TValue>(this MultiValueDictionary<TKey, TValue> dictionary, IEnumerable<IGrouping<TKey, TValue>> enumerable)
		{
			foreach (IGrouping<TKey, TValue> grouping in enumerable)
			{
				dictionary.AddRange(grouping.Key, grouping);
			}
		}
	}
}
