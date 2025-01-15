using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002083 RID: 8323
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArray
	{
		// Token: 0x060114ED RID: 70893 RVA: 0x003B79A4 File Offset: 0x003B5BA4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x060114EE RID: 70894 RVA: 0x003B79AC File Offset: 0x003B5BAC
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item)
		{
			T[] array = new T[] { item };
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114EF RID: 70895 RVA: 0x003B79D0 File Offset: 0x003B5BD0
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2)
		{
			T[] array = new T[] { item1, item2 };
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F0 RID: 70896 RVA: 0x003B79FC File Offset: 0x003B5BFC
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3)
		{
			T[] array = new T[] { item1, item2, item3 };
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F1 RID: 70897 RVA: 0x003B7A30 File Offset: 0x003B5C30
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3, T item4)
		{
			T[] array = new T[] { item1, item2, item3, item4 };
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F2 RID: 70898 RVA: 0x003B7A6C File Offset: 0x003B5C6C
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			IImmutableArray immutableArray = items as IImmutableArray;
			if (immutableArray != null)
			{
				Array array = immutableArray.Array;
				if (array == null)
				{
					throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
				}
				return new ImmutableArray<T>((T[])array);
			}
			else
			{
				int num;
				if (items.TryGetCount(out num))
				{
					return new ImmutableArray<T>(items.ToArray(num));
				}
				return new ImmutableArray<T>(items.ToArray<T>());
			}
		}

		// Token: 0x060114F3 RID: 70899 RVA: 0x003B7AD4 File Offset: 0x003B5CD4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[] { 2, 1 })] params T[] items)
		{
			if (items == null || items.Length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[items.Length];
			Array.Copy(items, array, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F4 RID: 70900 RVA: 0x003B7B08 File Offset: 0x003B5D08
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T[] items, int start, int length)
		{
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			T[] array = new T[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = items[start + i];
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F5 RID: 70901 RVA: 0x003B7B8C File Offset: 0x003B5D8C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int start, int length)
		{
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			if (start == 0 && length == items.Length)
			{
				return items;
			}
			T[] array = new T[length];
			Array.Copy(items.array, start, array, 0, length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060114F6 RID: 70902 RVA: 0x003B7C10 File Offset: 0x003B5E10
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TResult> selector)
		{
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060114F7 RID: 70903 RVA: 0x003B7C6C File Offset: 0x003B5E6C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TResult> selector)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060114F8 RID: 70904 RVA: 0x003B7CFC File Offset: 0x003B5EFC
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060114F9 RID: 70905 RVA: 0x003B7D58 File Offset: 0x003B5F58
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060114FA RID: 70906 RVA: 0x003B7DEC File Offset: 0x003B5FEC
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableArray.Create<T>().ToBuilder();
		}

		// Token: 0x060114FB RID: 70907 RVA: 0x003B7E06 File Offset: 0x003B6006
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>(int initialCapacity)
		{
			return new ImmutableArray<T>.Builder(initialCapacity);
		}

		// Token: 0x060114FC RID: 70908 RVA: 0x003B7E0E File Offset: 0x003B600E
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this IEnumerable<TSource> items)
		{
			if (items is ImmutableArray<TSource>)
			{
				return (ImmutableArray<TSource>)items;
			}
			return ImmutableArray.CreateRange<TSource>(items);
		}

		// Token: 0x060114FD RID: 70909 RVA: 0x003B7E25 File Offset: 0x003B6025
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this ImmutableArray<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060114FE RID: 70910 RVA: 0x003B7E38 File Offset: 0x003B6038
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.array, value);
		}

		// Token: 0x060114FF RID: 70911 RVA: 0x003B7E46 File Offset: 0x003B6046
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, value, comparer);
		}

		// Token: 0x06011500 RID: 70912 RVA: 0x003B7E55 File Offset: 0x003B6055
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.array, index, length, value);
		}

		// Token: 0x06011501 RID: 70913 RVA: 0x003B7E65 File Offset: 0x003B6065
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, int index, int length, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, index, length, value, comparer);
		}

		// Token: 0x040068D3 RID: 26835
		internal static readonly byte[] TwoElementArray = new byte[2];
	}
}
