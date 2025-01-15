using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000047 RID: 71
	internal static class DictionaryExtensions
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000091D4 File Offset: 0x000073D4
		public static void Add<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key, TValue value)
		{
			if (!self.TryAdd(key, value))
			{
				throw new ArgumentException("Argument_AddingDuplicate");
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000091EB File Offset: 0x000073EB
		public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
		{
			return ((IDictionary<TKey, TValue>)self).Remove(key);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000091F4 File Offset: 0x000073F4
		internal static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValue)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = (dictionary[key] = createValue());
			}
			return tvalue;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000921C File Offset: 0x0000741C
		internal static void SetRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> valuesToCopy)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in valuesToCopy)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}
}
