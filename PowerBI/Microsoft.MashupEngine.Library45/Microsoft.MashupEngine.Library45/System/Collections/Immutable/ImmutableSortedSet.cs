using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020B8 RID: 8376
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedSet
	{
		// Token: 0x060118DA RID: 71898 RVA: 0x003C1523 File Offset: 0x003BF723
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		// Token: 0x060118DB RID: 71899 RVA: 0x003C152A File Offset: 0x003BF72A
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x060118DC RID: 71900 RVA: 0x003C1537 File Offset: 0x003BF737
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		// Token: 0x060118DD RID: 71901 RVA: 0x003C1544 File Offset: 0x003BF744
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		// Token: 0x060118DE RID: 71902 RVA: 0x003C1557 File Offset: 0x003BF757
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060118DF RID: 71903 RVA: 0x003C1564 File Offset: 0x003BF764
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060118E0 RID: 71904 RVA: 0x003C1557 File Offset: 0x003BF757
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060118E1 RID: 71905 RVA: 0x003C1564 File Offset: 0x003BF764
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060118E2 RID: 71906 RVA: 0x003C1577 File Offset: 0x003BF777
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		// Token: 0x060118E3 RID: 71907 RVA: 0x003C1583 File Offset: 0x003BF783
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		// Token: 0x060118E4 RID: 71908 RVA: 0x003C1590 File Offset: 0x003BF790
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		// Token: 0x060118E5 RID: 71909 RVA: 0x003C15C0 File Offset: 0x003BF7C0
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet(null);
		}

		// Token: 0x060118E6 RID: 71910 RVA: 0x003C15C9 File Offset: 0x003BF7C9
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this ImmutableSortedSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}
	}
}
