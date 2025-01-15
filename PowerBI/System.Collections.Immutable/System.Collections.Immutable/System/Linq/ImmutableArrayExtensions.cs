using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArrayExtensions
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002484 File Offset: 0x00000684
		public static IEnumerable<TResult> Select<[Nullable(2)] T, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TResult> selector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Select(selector);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000249C File Offset: 0x0000069C
		public static IEnumerable<TResult> SelectMany<[Nullable(2)] TSource, [Nullable(2)] TCollection, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (collectionSelector == null || resultSelector == null)
			{
				return immutableArray.SelectMany(collectionSelector, resultSelector);
			}
			if (immutableArray.Length != 0)
			{
				return immutableArray.SelectManyIterator(collectionSelector, resultSelector);
			}
			return Enumerable.Empty<TResult>();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024DD File Offset: 0x000006DD
		public static IEnumerable<T> Where<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Where(predicate);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024F2 File Offset: 0x000006F2
		[NullableContext(2)]
		public static bool Any<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			return immutableArray.Length > 0;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002500 File Offset: 0x00000700
		public static bool Any<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002548 File Offset: 0x00000748
		public static bool All<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002590 File Offset: 0x00000790
		[NullableContext(0)]
		public static bool SequenceEqual<TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			for (int i = 0; i < immutableArray.Length; i++)
			{
				if (!comparer.Equals(immutableArray.array[i], (TBase)((object)items.array[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002618 File Offset: 0x00000818
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, IEnumerable<TDerived> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			Requires.NotNull<IEnumerable<TDerived>>(items, "items");
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			int num = 0;
			int length = immutableArray.Length;
			foreach (TDerived tderived in items)
			{
				if (num == length)
				{
					return false;
				}
				if (!comparer.Equals(immutableArray[num], (TBase)((object)tderived)))
				{
					return false;
				}
				num++;
			}
			return num == length;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026AC File Offset: 0x000008AC
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items, Func<TBase, TBase, bool> predicate) where TDerived : TBase
		{
			Requires.NotNull<Func<TBase, TBase, bool>>(predicate, "predicate");
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			int i = 0;
			int length = immutableArray.Length;
			while (i < length)
			{
				if (!predicate(immutableArray[i], (TBase)((object)items[i])))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002730 File Offset: 0x00000930
		[NullableContext(2)]
		public static T Aggregate<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, T, T> func)
		{
			Requires.NotNull<Func<T, T, T>>(func, "func");
			if (immutableArray.Length == 0)
			{
				return default(T);
			}
			T t = immutableArray[0];
			int i = 1;
			int length = immutableArray.Length;
			while (i < length)
			{
				t = func(t, immutableArray[i]);
				i++;
			}
			return t;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000278C File Offset: 0x0000098C
		public static TAccumulate Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
		{
			Requires.NotNull<Func<TAccumulate, T, TAccumulate>>(func, "func");
			TAccumulate taccumulate = seed;
			foreach (T t in immutableArray.array)
			{
				taccumulate = func(taccumulate, t);
			}
			return taccumulate;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027CD File Offset: 0x000009CD
		public static TResult Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] TResult, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Requires.NotNull<Func<TAccumulate, TResult>>(resultSelector, "resultSelector");
			return resultSelector(immutableArray.Aggregate(seed, func));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027E8 File Offset: 0x000009E8
		public static T ElementAt<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			return immutableArray[index];
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027F4 File Offset: 0x000009F4
		[NullableContext(2)]
		public static T ElementAtOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			if (index < 0 || index >= immutableArray.Length)
			{
				return default(T);
			}
			return immutableArray[index];
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002824 File Offset: 0x00000A24
		public static T First<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return Enumerable.Empty<T>().First<T>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000286E File Offset: 0x00000A6E
		public static T First<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.First<T>();
			}
			return immutableArray[0];
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002890 File Offset: 0x00000A90
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.array.Length == 0)
			{
				return default(T);
			}
			return immutableArray.array[0];
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028BC File Offset: 0x00000ABC
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002905 File Offset: 0x00000B05
		public static T Last<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.Last<T>();
			}
			return immutableArray[immutableArray.Length - 1];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002930 File Offset: 0x00000B30
		public static T Last<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return Enumerable.Empty<T>().Last<T>();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000297F File Offset: 0x00000B7F
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.LastOrDefault<T>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002994 File Offset: 0x00000B94
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return default(T);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029E2 File Offset: 0x00000BE2
		public static T Single<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Single<T>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029F8 File Offset: 0x00000BF8
		public static T Single<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T t = default(T);
			foreach (T t2 in immutableArray.array)
			{
				if (predicate(t2))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					t = t2;
				}
			}
			if (flag)
			{
				Enumerable.Empty<T>().Single<T>();
			}
			return t;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A64 File Offset: 0x00000C64
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.SingleOrDefault<T>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A78 File Offset: 0x00000C78
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T t = default(T);
			foreach (T t2 in immutableArray.array)
			{
				if (predicate(t2))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					t = t2;
				}
			}
			return t;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector)
		{
			return immutableArray.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
		{
			return immutableArray.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Dictionary<TKey, T> dictionary = new Dictionary<TKey, T>(immutableArray.Length, comparer);
			foreach (T t in immutableArray)
			{
				dictionary.Add(keySelector(t), t);
			}
			return dictionary;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B44 File Offset: 0x00000D44
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<T, TElement>>(elementSelector, "elementSelector");
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(immutableArray.Length, comparer);
			foreach (T t in immutableArray.array)
			{
				dictionary.Add(keySelector(t), elementSelector(t));
			}
			return dictionary;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public static T[] ToArray<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (immutableArray.array.Length == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			return (T[])immutableArray.array.Clone();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static T First<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[0];
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BF8 File Offset: 0x00000DF8
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[0];
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C29 File Offset: 0x00000E29
		public static T Last<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C54 File Offset: 0x00000E54
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C8C File Offset: 0x00000E8C
		public static bool Any<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			return builder.Count > 0;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CA2 File Offset: 0x00000EA2
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource item in immutableArray.array)
			{
				foreach (TCollection tcollection in collectionSelector(item))
				{
					yield return resultSelector(item, tcollection);
				}
				IEnumerator<TCollection> enumerator = null;
				item = default(TSource);
			}
			TSource[] array = null;
			yield break;
			yield break;
		}
	}
}
