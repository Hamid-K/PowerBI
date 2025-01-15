using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedSet
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00008290 File Offset: 0x00006490
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008297 File Offset: 0x00006497
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000082A4 File Offset: 0x000064A4
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000082B1 File Offset: 0x000064B1
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000082C4 File Offset: 0x000064C4
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000082D1 File Offset: 0x000064D1
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000082E4 File Offset: 0x000064E4
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000082F1 File Offset: 0x000064F1
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008304 File Offset: 0x00006504
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00008310 File Offset: 0x00006510
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008320 File Offset: 0x00006520
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008350 File Offset: 0x00006550
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet(null);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008359 File Offset: 0x00006559
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this ImmutableSortedSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}
	}
}
