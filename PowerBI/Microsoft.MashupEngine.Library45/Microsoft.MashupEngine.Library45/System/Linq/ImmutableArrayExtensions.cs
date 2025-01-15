using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02002062 RID: 8290
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArrayExtensions
	{
		// Token: 0x060113C4 RID: 70596 RVA: 0x003B54C4 File Offset: 0x003B36C4
		public static IEnumerable<TResult> Select<[Nullable(2)] T, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TResult> selector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Select(selector);
		}

		// Token: 0x060113C5 RID: 70597 RVA: 0x003B54DC File Offset: 0x003B36DC
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

		// Token: 0x060113C6 RID: 70598 RVA: 0x003B551D File Offset: 0x003B371D
		public static IEnumerable<T> Where<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Where(predicate);
		}

		// Token: 0x060113C7 RID: 70599 RVA: 0x003B5532 File Offset: 0x003B3732
		[NullableContext(2)]
		public static bool Any<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			return immutableArray.Length > 0;
		}

		// Token: 0x060113C8 RID: 70600 RVA: 0x003B5540 File Offset: 0x003B3740
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

		// Token: 0x060113C9 RID: 70601 RVA: 0x003B5588 File Offset: 0x003B3788
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

		// Token: 0x060113CA RID: 70602 RVA: 0x003B55D0 File Offset: 0x003B37D0
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

		// Token: 0x060113CB RID: 70603 RVA: 0x003B5658 File Offset: 0x003B3858
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

		// Token: 0x060113CC RID: 70604 RVA: 0x003B56EC File Offset: 0x003B38EC
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

		// Token: 0x060113CD RID: 70605 RVA: 0x003B5770 File Offset: 0x003B3970
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

		// Token: 0x060113CE RID: 70606 RVA: 0x003B57CC File Offset: 0x003B39CC
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

		// Token: 0x060113CF RID: 70607 RVA: 0x003B580D File Offset: 0x003B3A0D
		public static TResult Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] TResult, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Requires.NotNull<Func<TAccumulate, TResult>>(resultSelector, "resultSelector");
			return resultSelector(immutableArray.Aggregate(seed, func));
		}

		// Token: 0x060113D0 RID: 70608 RVA: 0x003B5828 File Offset: 0x003B3A28
		public static T ElementAt<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			return immutableArray[index];
		}

		// Token: 0x060113D1 RID: 70609 RVA: 0x003B5834 File Offset: 0x003B3A34
		[NullableContext(2)]
		public static T ElementAtOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			if (index < 0 || index >= immutableArray.Length)
			{
				return default(T);
			}
			return immutableArray[index];
		}

		// Token: 0x060113D2 RID: 70610 RVA: 0x003B5864 File Offset: 0x003B3A64
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

		// Token: 0x060113D3 RID: 70611 RVA: 0x003B58AE File Offset: 0x003B3AAE
		public static T First<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.First<T>();
			}
			return immutableArray[0];
		}

		// Token: 0x060113D4 RID: 70612 RVA: 0x003B58D0 File Offset: 0x003B3AD0
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.array.Length == 0)
			{
				return default(T);
			}
			return immutableArray.array[0];
		}

		// Token: 0x060113D5 RID: 70613 RVA: 0x003B58FC File Offset: 0x003B3AFC
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

		// Token: 0x060113D6 RID: 70614 RVA: 0x003B5945 File Offset: 0x003B3B45
		public static T Last<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.Last<T>();
			}
			return immutableArray[immutableArray.Length - 1];
		}

		// Token: 0x060113D7 RID: 70615 RVA: 0x003B5970 File Offset: 0x003B3B70
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

		// Token: 0x060113D8 RID: 70616 RVA: 0x003B59BF File Offset: 0x003B3BBF
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.LastOrDefault<T>();
		}

		// Token: 0x060113D9 RID: 70617 RVA: 0x003B59D4 File Offset: 0x003B3BD4
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

		// Token: 0x060113DA RID: 70618 RVA: 0x003B5A22 File Offset: 0x003B3C22
		public static T Single<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Single<T>();
		}

		// Token: 0x060113DB RID: 70619 RVA: 0x003B5A38 File Offset: 0x003B3C38
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

		// Token: 0x060113DC RID: 70620 RVA: 0x003B5AA4 File Offset: 0x003B3CA4
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.SingleOrDefault<T>();
		}

		// Token: 0x060113DD RID: 70621 RVA: 0x003B5AB8 File Offset: 0x003B3CB8
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

		// Token: 0x060113DE RID: 70622 RVA: 0x003B5B16 File Offset: 0x003B3D16
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector)
		{
			return immutableArray.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x060113DF RID: 70623 RVA: 0x003B5B24 File Offset: 0x003B3D24
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
		{
			return immutableArray.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x060113E0 RID: 70624 RVA: 0x003B5B34 File Offset: 0x003B3D34
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

		// Token: 0x060113E1 RID: 70625 RVA: 0x003B5B84 File Offset: 0x003B3D84
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

		// Token: 0x060113E2 RID: 70626 RVA: 0x003B5BE7 File Offset: 0x003B3DE7
		public static T[] ToArray<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (immutableArray.array.Length == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			return (T[])immutableArray.array.Clone();
		}

		// Token: 0x060113E3 RID: 70627 RVA: 0x003B5C14 File Offset: 0x003B3E14
		public static T First<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[0];
		}

		// Token: 0x060113E4 RID: 70628 RVA: 0x003B5C38 File Offset: 0x003B3E38
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

		// Token: 0x060113E5 RID: 70629 RVA: 0x003B5C69 File Offset: 0x003B3E69
		public static T Last<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x060113E6 RID: 70630 RVA: 0x003B5C94 File Offset: 0x003B3E94
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

		// Token: 0x060113E7 RID: 70631 RVA: 0x003B5CCC File Offset: 0x003B3ECC
		public static bool Any<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			return builder.Count > 0;
		}

		// Token: 0x060113E8 RID: 70632 RVA: 0x003B5CE2 File Offset: 0x003B3EE2
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
