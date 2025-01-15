using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200042B RID: 1067
	public static class ConfigurationDictionaryExtensions
	{
		// Token: 0x060020EE RID: 8430 RVA: 0x0007BE54 File Offset: 0x0007A054
		public static ConfigurationDictionary<TKey, TElement> ToConfigurationDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return new ConfigurationDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
		}
	}
}
