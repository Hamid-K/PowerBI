using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020A4 RID: 8356
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableHashSet
	{
		// Token: 0x060116C0 RID: 71360 RVA: 0x003BC352 File Offset: 0x003BA552
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		// Token: 0x060116C1 RID: 71361 RVA: 0x003BC359 File Offset: 0x003BA559
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		// Token: 0x060116C2 RID: 71362 RVA: 0x003BC366 File Offset: 0x003BA566
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		// Token: 0x060116C3 RID: 71363 RVA: 0x003BC373 File Offset: 0x003BA573
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		// Token: 0x060116C4 RID: 71364 RVA: 0x003BC386 File Offset: 0x003BA586
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060116C5 RID: 71365 RVA: 0x003BC393 File Offset: 0x003BA593
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060116C6 RID: 71366 RVA: 0x003BC386 File Offset: 0x003BA586
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060116C7 RID: 71367 RVA: 0x003BC393 File Offset: 0x003BA593
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, params T[] items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060116C8 RID: 71368 RVA: 0x003BC3A6 File Offset: 0x003BA5A6
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		// Token: 0x060116C9 RID: 71369 RVA: 0x003BC3B2 File Offset: 0x003BA5B2
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		// Token: 0x060116CA RID: 71370 RVA: 0x003BC3C0 File Offset: 0x003BA5C0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		// Token: 0x060116CB RID: 71371 RVA: 0x003BC3F0 File Offset: 0x003BA5F0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this ImmutableHashSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableHashSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060116CC RID: 71372 RVA: 0x003BC403 File Offset: 0x003BA603
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet(null);
		}
	}
}
