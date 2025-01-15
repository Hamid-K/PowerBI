using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableList
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00006B4A File Offset: 0x00004D4A
		public static ImmutableList<T> Create<[Nullable(2)] T>()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006B51 File Offset: 0x00004D51
		public static ImmutableList<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006B5E File Offset: 0x00004D5E
		public static ImmutableList<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006B6B File Offset: 0x00004D6B
		public static ImmutableList<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006B78 File Offset: 0x00004D78
		public static ImmutableList<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006B84 File Offset: 0x00004D84
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this ImmutableList<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableList<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006BBB File Offset: 0x00004DBB
		public static IImmutableList<T> Replace<[Nullable(2)] T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006BD5 File Offset: 0x00004DD5
		public static IImmutableList<T> Remove<[Nullable(2)] T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006BEE File Offset: 0x00004DEE
		public static IImmutableList<T> RemoveRange<[Nullable(2)] T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006C07 File Offset: 0x00004E07
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006C27 File Offset: 0x00004E27
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006C43 File Offset: 0x00004E43
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006C65 File Offset: 0x00004E65
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006C80 File Offset: 0x00004E80
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006CB1 File Offset: 0x00004EB1
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006CDE File Offset: 0x00004EDE
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006D08 File Offset: 0x00004F08
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}
