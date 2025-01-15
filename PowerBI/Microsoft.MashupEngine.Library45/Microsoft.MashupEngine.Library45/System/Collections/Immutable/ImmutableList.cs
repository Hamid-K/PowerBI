using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020A6 RID: 8358
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableList
	{
		// Token: 0x060116E0 RID: 71392 RVA: 0x003BC98A File Offset: 0x003BAB8A
		public static ImmutableList<T> Create<[Nullable(2)] T>()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x060116E1 RID: 71393 RVA: 0x003BC991 File Offset: 0x003BAB91
		public static ImmutableList<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		// Token: 0x060116E2 RID: 71394 RVA: 0x003BC99E File Offset: 0x003BAB9E
		public static ImmutableList<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x060116E3 RID: 71395 RVA: 0x003BC99E File Offset: 0x003BAB9E
		public static ImmutableList<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x060116E4 RID: 71396 RVA: 0x003BC9AB File Offset: 0x003BABAB
		public static ImmutableList<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		// Token: 0x060116E5 RID: 71397 RVA: 0x003BC9B8 File Offset: 0x003BABB8
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		// Token: 0x060116E6 RID: 71398 RVA: 0x003BC9DC File Offset: 0x003BABDC
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this ImmutableList<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableList<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060116E7 RID: 71399 RVA: 0x003BC9EF File Offset: 0x003BABEF
		public static IImmutableList<T> Replace<[Nullable(2)] T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x060116E8 RID: 71400 RVA: 0x003BCA09 File Offset: 0x003BAC09
		public static IImmutableList<T> Remove<[Nullable(2)] T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x060116E9 RID: 71401 RVA: 0x003BCA22 File Offset: 0x003BAC22
		public static IImmutableList<T> RemoveRange<[Nullable(2)] T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x060116EA RID: 71402 RVA: 0x003BCA3B File Offset: 0x003BAC3B
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x060116EB RID: 71403 RVA: 0x003BCA5B File Offset: 0x003BAC5B
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		// Token: 0x060116EC RID: 71404 RVA: 0x003BCA77 File Offset: 0x003BAC77
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x060116ED RID: 71405 RVA: 0x003BCA99 File Offset: 0x003BAC99
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x060116EE RID: 71406 RVA: 0x003BCAB4 File Offset: 0x003BACB4
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x060116EF RID: 71407 RVA: 0x003BCAE5 File Offset: 0x003BACE5
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		// Token: 0x060116F0 RID: 71408 RVA: 0x003BCB12 File Offset: 0x003BAD12
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x060116F1 RID: 71409 RVA: 0x003BCB3C File Offset: 0x003BAD3C
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}
