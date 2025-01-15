using System;

namespace System.Collections.Concurrent
{
	// Token: 0x02000002 RID: 2
	internal static class ConcurrentDictionaryExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static TValue GetOrAdd<TKey, TValue, TArg>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg arg)
		{
			TValue tvalue;
			while (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = valueFactory(key, arg);
				if (dictionary.TryAdd(key, tvalue))
				{
					return tvalue;
				}
			}
			return tvalue;
		}
	}
}
