using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200208A RID: 8330
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableDictionary
	{
		// Token: 0x060115B1 RID: 71089 RVA: 0x003B9C64 File Offset: 0x003B7E64
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x060115B2 RID: 71090 RVA: 0x003B9C6B File Offset: 0x003B7E6B
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x060115B3 RID: 71091 RVA: 0x003B9C78 File Offset: 0x003B7E78
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060115B4 RID: 71092 RVA: 0x003B9C86 File Offset: 0x003B7E86
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x060115B5 RID: 71093 RVA: 0x003B9C93 File Offset: 0x003B7E93
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x060115B6 RID: 71094 RVA: 0x003B9CA6 File Offset: 0x003B7EA6
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x060115B7 RID: 71095 RVA: 0x003B9CBA File Offset: 0x003B7EBA
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x060115B8 RID: 71096 RVA: 0x003B9CC6 File Offset: 0x003B7EC6
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x060115B9 RID: 71097 RVA: 0x003B9CD3 File Offset: 0x003B7ED3
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x060115BA RID: 71098 RVA: 0x003B9CE4 File Offset: 0x003B7EE4
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Select((TSource element) => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element))));
		}

		// Token: 0x060115BB RID: 71099 RVA: 0x003B9D54 File Offset: 0x003B7F54
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>(this ImmutableDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060115BC RID: 71100 RVA: 0x003B9D67 File Offset: 0x003B7F67
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x060115BD RID: 71101 RVA: 0x003B9D73 File Offset: 0x003B7F73
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, null, null);
		}

		// Token: 0x060115BE RID: 71102 RVA: 0x003B9D9D File Offset: 0x003B7F9D
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, keyComparer, null);
		}

		// Token: 0x060115BF RID: 71103 RVA: 0x003B9DC7 File Offset: 0x003B7FC7
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x060115C0 RID: 71104 RVA: 0x003B9DD4 File Offset: 0x003B7FD4
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableDictionary<TKey, TValue> immutableDictionary = source as ImmutableDictionary<TKey, TValue>;
			if (immutableDictionary != null)
			{
				return immutableDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x060115C1 RID: 71105 RVA: 0x003B9E11 File Offset: 0x003B8011
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keyComparer, null);
		}

		// Token: 0x060115C2 RID: 71106 RVA: 0x003B9E1B File Offset: 0x003B801B
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableDictionary(null, null);
		}

		// Token: 0x060115C3 RID: 71107 RVA: 0x003B9E25 File Offset: 0x003B8025
		public static bool Contains<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(map, "map");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return map.Contains(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x060115C4 RID: 71108 RVA: 0x003B9E4C File Offset: 0x003B804C
		[return: Nullable(2)]
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x060115C5 RID: 71109 RVA: 0x003B9E6C File Offset: 0x003B806C
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			TValue tvalue;
			if (dictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			return defaultValue;
		}
	}
}
