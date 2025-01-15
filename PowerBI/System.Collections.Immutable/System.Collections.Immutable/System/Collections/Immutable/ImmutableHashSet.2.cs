using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableHashSet
	{
		// Token: 0x060001ED RID: 493 RVA: 0x000064F0 File Offset: 0x000046F0
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000064F7 File Offset: 0x000046F7
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006504 File Offset: 0x00004704
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006511 File Offset: 0x00004711
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006524 File Offset: 0x00004724
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006531 File Offset: 0x00004731
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006544 File Offset: 0x00004744
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006551 File Offset: 0x00004751
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, params T[] items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006564 File Offset: 0x00004764
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006570 File Offset: 0x00004770
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006580 File Offset: 0x00004780
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000065B0 File Offset: 0x000047B0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this ImmutableHashSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableHashSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000065C3 File Offset: 0x000047C3
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet(null);
		}
	}
}
