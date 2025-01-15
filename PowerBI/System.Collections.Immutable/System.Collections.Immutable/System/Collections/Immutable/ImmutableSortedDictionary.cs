using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedDictionary
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00007892 File Offset: 0x00005A92
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007899 File Offset: 0x00005A99
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000078A6 File Offset: 0x00005AA6
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000078C1 File Offset: 0x00005AC1
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000078F4 File Offset: 0x00005AF4
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007901 File Offset: 0x00005B01
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007910 File Offset: 0x00005B10
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Select((TSource element) => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element))));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007980 File Offset: 0x00005B80
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>(this ImmutableSortedDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007993 File Offset: 0x00005B93
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000799F File Offset: 0x00005B9F
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000079AC File Offset: 0x00005BAC
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

		// Token: 0x060002A4 RID: 676 RVA: 0x000079E9 File Offset: 0x00005BE9
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keyComparer, null);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000079F3 File Offset: 0x00005BF3
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary(null, null);
		}
	}
}
