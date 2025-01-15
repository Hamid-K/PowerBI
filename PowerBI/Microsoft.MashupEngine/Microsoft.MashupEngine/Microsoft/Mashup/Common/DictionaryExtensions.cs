using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE8 RID: 7144
	public static class DictionaryExtensions
	{
		// Token: 0x0600B280 RID: 45696 RVA: 0x002457FC File Offset: 0x002439FC
		public static IDictionary<TKey2, TValue> TransformKeys<TKey, TKey2, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TKey2> transform)
		{
			Dictionary<TKey2, TValue> dictionary2 = new Dictionary<TKey2, TValue>(dictionary.Count);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				dictionary2[transform(keyValuePair.Key)] = keyValuePair.Value;
			}
			return dictionary2;
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x00245864 File Offset: 0x00243A64
		public static IDictionary<TKey, TValue2> TransformValues<TKey, TValue, TValue2>(this IDictionary<TKey, TValue> dictionary, Func<TValue, TValue2> transform)
		{
			Dictionary<TKey, TValue2> dictionary2 = new Dictionary<TKey, TValue2>(dictionary.Count);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				dictionary2[keyValuePair.Key] = transform(keyValuePair.Value);
			}
			return dictionary2;
		}
	}
}
