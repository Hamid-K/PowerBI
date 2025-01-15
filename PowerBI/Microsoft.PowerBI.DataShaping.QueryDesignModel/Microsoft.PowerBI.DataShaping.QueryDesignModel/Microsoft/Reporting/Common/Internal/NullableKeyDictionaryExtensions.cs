using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000289 RID: 649
	internal static class NullableKeyDictionaryExtensions
	{
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0004D852 File Offset: 0x0004BA52
		internal static NullableKeyDictionary<TKey, TSource> ToNullableKeyDictionary<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector) where TKey : class
		{
			return source.ToNullableKeyDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0004D860 File Offset: 0x0004BA60
		internal static NullableKeyDictionary<TKey, TSource> ToNullableKeyDictionary<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) where TKey : class
		{
			return new NullableKeyDictionary<TKey, TSource>(source, keySelector, source.Count, comparer);
		}
	}
}
