using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200004D RID: 77
	internal static class ReadOnlyDictionaryExtensions
	{
		// Token: 0x06000323 RID: 803 RVA: 0x000092B3 File Offset: 0x000074B3
		public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}
			return new ReadOnlyDictionary<TKey, TValue>(dictionary);
		}
	}
}
