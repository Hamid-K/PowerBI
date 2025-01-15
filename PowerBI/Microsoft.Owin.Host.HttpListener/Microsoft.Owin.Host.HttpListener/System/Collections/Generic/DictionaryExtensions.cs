using System;

namespace System.Collections.Generic
{
	// Token: 0x02000002 RID: 2
	internal static class DictionaryExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static T Get<T>(this IDictionary<string, object> dictionary, string key)
		{
			object value;
			if (!dictionary.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		internal static T Get<T>(this IDictionary<string, object> dictionary, string subDictionaryKey, string key)
		{
			IDictionary<string, object> subDictionary = dictionary.Get(subDictionaryKey);
			if (subDictionary == null)
			{
				return default(T);
			}
			return subDictionary.Get(key);
		}
	}
}
