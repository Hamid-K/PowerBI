using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000011 RID: 17
	public static class DictionaryExtensions
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000035D0 File Offset: 0x000017D0
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> createValue)
		{
			TValue tvalue;
			if (dict.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = createValue();
			dict.Add(key, tvalue);
			return tvalue;
		}
	}
}
