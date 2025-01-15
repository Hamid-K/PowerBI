using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020B0 RID: 8368
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedDictionary
	{
		// Token: 0x06011816 RID: 71702 RVA: 0x003BF88D File Offset: 0x003BDA8D
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06011817 RID: 71703 RVA: 0x003BF894 File Offset: 0x003BDA94
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x06011818 RID: 71704 RVA: 0x003BF8A1 File Offset: 0x003BDAA1
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x06011819 RID: 71705 RVA: 0x003BF8AF File Offset: 0x003BDAAF
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x0601181A RID: 71706 RVA: 0x003BF8BC File Offset: 0x003BDABC
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x0601181B RID: 71707 RVA: 0x003BF8CF File Offset: 0x003BDACF
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x0601181C RID: 71708 RVA: 0x003BF8E3 File Offset: 0x003BDAE3
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x0601181D RID: 71709 RVA: 0x003BF8EF File Offset: 0x003BDAEF
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x0601181E RID: 71710 RVA: 0x003BF8FC File Offset: 0x003BDAFC
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x0601181F RID: 71711 RVA: 0x003BF90C File Offset: 0x003BDB0C
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Select((TSource element) => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element))));
		}

		// Token: 0x06011820 RID: 71712 RVA: 0x003BF97C File Offset: 0x003BDB7C
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>(this ImmutableSortedDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x06011821 RID: 71713 RVA: 0x003BF98F File Offset: 0x003BDB8F
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x06011822 RID: 71714 RVA: 0x003BF99B File Offset: 0x003BDB9B
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x06011823 RID: 71715 RVA: 0x003BF9A8 File Offset: 0x003BDBA8
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = source as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x06011824 RID: 71716 RVA: 0x003BF9E5 File Offset: 0x003BDBE5
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keyComparer, null);
		}

		// Token: 0x06011825 RID: 71717 RVA: 0x003BF9EF File Offset: 0x003BDBEF
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary(null, null);
		}
	}
}
