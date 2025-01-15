using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.DataShaping
{
	// Token: 0x02000008 RID: 8
	internal static class Util
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		internal static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		internal static T CheckEqual<T>(T arg1, T arg2)
		{
			if (!object.Equals(arg1, arg2))
			{
				throw new ArgumentException("The arguments must be equal", "arg1");
			}
			return arg1;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A0 File Offset: 0x000002A0
		internal static IList<T> Evaluate<T>(this IEnumerable<T> source)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				return list;
			}
			return source.ToList<T>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C0 File Offset: 0x000002C0
		internal static IReadOnlyList<T> EvaluateReadOnly<T>(this IEnumerable<T> source)
		{
			IReadOnlyList<T> readOnlyList = source as IReadOnlyList<T>;
			if (readOnlyList != null)
			{
				return readOnlyList;
			}
			return source.ToList<T>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static void ReplaceWith<T>(this ICollection<T> collection, IEnumerable<T> newItems)
		{
			collection.Clear();
			if (newItems != null)
			{
				foreach (T t in newItems)
				{
					collection.Add(t);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		internal static void ReplaceWith<T>(this ICollection<T> collection, T value)
		{
			collection.Clear();
			collection.Add(value);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		internal static T PeekOrDefault<T>(this Stack<T> stack)
		{
			if (stack.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			return stack.Peek();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216C File Offset: 0x0000036C
		internal static void Push<T>(this Stack<T> stack, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				stack.Push(t);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static IReadOnlyList<T> EmptyIfNull<T>(this IReadOnlyList<T> source)
		{
			return source ?? Util.EmptyReadOnlyList<T>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C0 File Offset: 0x000003C0
		internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return Util.EmptyReadOnlyList<T>();
			}
			return source;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DC File Offset: 0x000003DC
		internal static IEnumerable<T> ConcatNullable<T>(this IEnumerable<T> head, IEnumerable<T> tail)
		{
			IEnumerable<T> enumerable = head;
			if (tail != null)
			{
				enumerable = ((head != null) ? head.Concat(tail) : null) ?? tail;
			}
			return enumerable.EmptyIfNull<T>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		internal static IEnumerable<T> ConcatNullable<T>(this IEnumerable<T> head, T tail)
		{
			IEnumerable<T> enumerable = head;
			if (tail != null)
			{
				enumerable = ((head != null) ? head.Concat(new T[] { tail }) : null) ?? tail.AsList<T>();
			}
			return enumerable.EmptyIfNull<T>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000224C File Offset: 0x0000044C
		internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
		{
			if (items != null)
			{
				T[] array = items.ToArray<T>();
				if (array.Length != 0)
				{
					return Array.AsReadOnly<T>(array);
				}
			}
			return Util.EmptyCollectionsReadOnlyArray<T>.Instance;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002273 File Offset: 0x00000473
		internal static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IList<T> items)
		{
			if (items == null)
			{
				return Util.EmptyReadOnlyCollection<T>();
			}
			return new ReadOnlyCollection<T>(items);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002284 File Offset: 0x00000484
		internal static HashSet<T> ToSet<T>(this IEnumerable<T> items)
		{
			return new HashSet<T>(items);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000228C File Offset: 0x0000048C
		internal static HashSet<T> ToSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(items, comparer);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002295 File Offset: 0x00000495
		internal static IEnumerable<T> Concat<T>(this IEnumerable<T> source, params T[] values)
		{
			return source.Concat(values);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229E File Offset: 0x0000049E
		internal static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] values)
		{
			return source.Except(values);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022A7 File Offset: 0x000004A7
		internal static bool IsSupersetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !second.Except(first).Any<T>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B8 File Offset: 0x000004B8
		internal static bool IsSubsetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !first.Except(second).Any<T>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022C9 File Offset: 0x000004C9
		internal static IEnumerable<T> TakeAfter<T>(this IEnumerable<T> source, T value)
		{
			return source.TakeAfter(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D7 File Offset: 0x000004D7
		internal static IEnumerable<T> TakeAfter<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
		{
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (comparer.Equals(value, enumerator.Current))
					{
						IL_0088:
						while (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							yield return t;
						}
						goto JumpOutOfTryFinally-3;
					}
				}
				goto IL_0088;
			}
			JumpOutOfTryFinally-3:
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022F5 File Offset: 0x000004F5
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, T value)
		{
			return source.TakeUntil(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002304 File Offset: 0x00000504
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
		{
			return source.TakeUntil((T v) => comparer.Equals(value, v));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002337 File Offset: 0x00000537
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T item in source)
			{
				yield return item;
				if (predicate(item))
				{
					yield break;
				}
				item = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000234E File Offset: 0x0000054E
		internal static IEnumerable<T> WhereNonNull<T>(this IEnumerable<T> source) where T : class
		{
			return source.Where((T item) => item != null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002378 File Offset: 0x00000578
		internal static T Min<T>(this IEnumerable<T> source, Func<T, T, int> comparer)
		{
			T t3;
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ArgumentException("The argument value must be a non-empty collection.", "source");
				}
				T t = enumerator.Current;
				while (enumerator.MoveNext())
				{
					T t2 = enumerator.Current;
					if (comparer(t2, t) < 0)
					{
						t = t2;
					}
				}
				t3 = t;
			}
			return t3;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E8 File Offset: 0x000005E8
		internal static T[] EmptyArray<T>()
		{
			return Array.Empty<T>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023EF File Offset: 0x000005EF
		internal static ISet<T> EmptySet<T>()
		{
			return Util.EmptyCollectionsSet<T>.Instance;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023F6 File Offset: 0x000005F6
		internal static ReadOnlyCollection<T> EmptyReadOnlyCollection<T>()
		{
			return Util.EmptyCollectionsReadOnlyArray<T>.Instance;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023FD File Offset: 0x000005FD
		internal static IReadOnlyList<T> EmptyReadOnlyList<T>()
		{
			return Array.Empty<T>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002404 File Offset: 0x00000604
		internal static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T item)
		{
			return item.AsEnumerable<T>().Concat(source);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002414 File Offset: 0x00000614
		internal static IEnumerable<T> PrependWithNullCheck<T>(this IEnumerable<T> source, T item)
		{
			if (item == null)
			{
				return source;
			}
			IEnumerable<T> enumerable = item.AsEnumerable<T>();
			if (source == null)
			{
				return enumerable;
			}
			return enumerable.Concat(source);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000243E File Offset: 0x0000063E
		internal static IEnumerable<T> AsEnumerable<T>(this T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000244E File Offset: 0x0000064E
		internal static T Min<T>(this T value1, T value2) where T : IComparable<T>
		{
			if (value1.CompareTo(value2) > 0)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002464 File Offset: 0x00000664
		internal static T Max<T>(this T value1, T value2) where T : IComparable<T>
		{
			if (value1.CompareTo(value2) < 0)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247C File Offset: 0x0000067C
		internal static bool HasDuplicates<T>(this IEnumerable<T> source)
		{
			HashSet<T> hashSet = new HashSet<T>();
			foreach (T t in source)
			{
				if (!hashSet.Add(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D4 File Offset: 0x000006D4
		internal static bool SyncToListDistinctUnordered<T>(this IEnumerable<T> source, IList<T> target)
		{
			bool flag = false;
			HashSet<T> hashSet = new HashSet<T>(source);
			for (int i = target.Count - 1; i >= 0; i--)
			{
				if (!hashSet.Remove(target[i]))
				{
					target.RemoveAt(i);
					flag = true;
				}
			}
			foreach (T t in hashSet)
			{
				target.Add(t);
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000255C File Offset: 0x0000075C
		internal static bool SetEquals<T>(this IEnumerable<T> source, IEnumerable<T> other)
		{
			HashSet<T> hashSet = source as HashSet<T>;
			HashSet<T> hashSet2 = other as HashSet<T>;
			if (hashSet != null)
			{
				return hashSet.SetEquals(other);
			}
			if (hashSet2 != null)
			{
				return hashSet2.SetEquals(source);
			}
			return new HashSet<T>(source).SetEquals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000259C File Offset: 0x0000079C
		internal static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return true;
			}
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				return collection.Count == 0;
			}
			IReadOnlyCollection<T> readOnlyCollection = source as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection.Count == 0;
			}
			return !source.Any<T>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E0 File Offset: 0x000007E0
		internal static int CountItems<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return 0;
			}
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				return collection.Count;
			}
			IReadOnlyCollection<T> readOnlyCollection = source as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection.Count;
			}
			return source.Count<T>();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000261C File Offset: 0x0000081C
		internal static void InvokeTypeSpecificAction<TBase, TDerived1, TDerived2>(TBase arg, Action<TDerived1> action1, Action<TDerived2> action2) where TDerived1 : class, TBase where TDerived2 : class, TBase
		{
			TDerived1 tderived = arg as TDerived1;
			if (tderived != null)
			{
				action1(tderived);
				return;
			}
			TDerived2 tderived2 = arg as TDerived2;
			if (tderived2 != null)
			{
				action2(tderived2);
				return;
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002678 File Offset: 0x00000878
		internal static void InvokeTypeSpecificAction<TBase, TDerived1, TDerived2, TArgument>(TBase arg1, TArgument arg2, Action<TDerived1, TArgument> action1, Action<TDerived2, TArgument> action2) where TDerived1 : class, TBase where TDerived2 : class, TBase
		{
			if (arg1 is TDerived1)
			{
				action1((TDerived1)((object)arg1), arg2);
				return;
			}
			if (arg1 is TDerived2)
			{
				action2((TDerived2)((object)arg1), arg2);
				return;
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026D0 File Offset: 0x000008D0
		internal static TReturn InvokeTypeSpecificFunction<TReturn, TBase, TDerived1, TDerived2>(TBase arg, Func<TDerived1, TReturn> func1, Func<TDerived2, TReturn> func2) where TDerived1 : class, TBase where TDerived2 : class, TBase
		{
			TDerived1 tderived = arg as TDerived1;
			if (tderived != null)
			{
				return func1(tderived);
			}
			TDerived2 tderived2 = arg as TDerived2;
			if (tderived2 != null)
			{
				return func2(tderived2);
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000272C File Offset: 0x0000092C
		internal static TReturn InvokeTypeSpecificFunction<TArgument, TReturn, TBase, TDerived1, TDerived2>(TBase arg1, TArgument arg2, Func<TArgument, TDerived1, TReturn> func1, Func<TArgument, TDerived2, TReturn> func2) where TDerived1 : class, TBase where TDerived2 : class, TBase
		{
			if (arg1 is TDerived1)
			{
				return func1(arg2, (TDerived1)((object)arg1));
			}
			if (arg1 is TDerived2)
			{
				return func2(arg2, (TDerived2)((object)arg1));
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002784 File Offset: 0x00000984
		internal static TReturn InvokeStructSpecificFunction<TReturn, TBase, TDerived1, TDerived2>(TBase arg, Func<TDerived1, TReturn> func1, Func<TDerived2, TReturn> func2) where TDerived1 : struct, TBase where TDerived2 : struct, TBase
		{
			if (arg is TDerived1)
			{
				return func1((TDerived1)((object)arg));
			}
			if (arg is TDerived2)
			{
				return func2((TDerived2)((object)arg));
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027DC File Offset: 0x000009DC
		internal static TReturn InvokeStructSpecificFunction<TArgument, TReturn, TBase, TDerived1, TDerived2>(TBase arg1, TArgument arg2, Func<TArgument, TDerived1, TReturn> func1, Func<TArgument, TDerived2, TReturn> func2) where TDerived1 : struct, TBase where TDerived2 : struct, TBase
		{
			if (arg1 is TDerived1)
			{
				return func1(arg2, (TDerived1)((object)arg1));
			}
			if (arg1 is TDerived2)
			{
				return func2(arg2, (TDerived2)((object)arg1));
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002834 File Offset: 0x00000A34
		internal static TReturn InvokeStructSpecificFunction<TArgument, TReturn, TBase, TDerived1, TDerived2>(TBase arg1, TArgument arg2, Func<TDerived1, TArgument, TReturn> func1, Func<TDerived2, TArgument, TReturn> func2) where TDerived1 : struct, TBase where TDerived2 : struct, TBase
		{
			if (arg1 is TDerived1)
			{
				return func1((TDerived1)((object)arg1), arg2);
			}
			if (arg1 is TDerived2)
			{
				return func2((TDerived2)((object)arg1), arg2);
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000288C File Offset: 0x00000A8C
		internal static void InvokeStructSpecificAction<TBase, TDerived1, TDerived2>(TBase arg1, Action<TDerived1> action1, Action<TDerived2> action2) where TDerived1 : struct, TBase where TDerived2 : struct, TBase
		{
			if (arg1 is TDerived1)
			{
				action1((TDerived1)((object)arg1));
				return;
			}
			if (arg1 is TDerived2)
			{
				action2((TDerived2)((object)arg1));
				return;
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028E4 File Offset: 0x00000AE4
		internal static void InvokeStructSpecificAction<TBase, TDerived1, TDerived2, TArgument>(TBase arg1, TArgument arg2, Action<TDerived1, TArgument> action1, Action<TDerived2, TArgument> action2) where TDerived1 : struct, TBase where TDerived2 : struct, TBase
		{
			if (arg1 is TDerived1)
			{
				action1((TDerived1)((object)arg1), arg2);
				return;
			}
			if (arg1 is TDerived2)
			{
				action2((TDerived2)((object)arg1), arg2);
				return;
			}
			throw new NotImplementedException("Unknown subtype for type-specific operation.");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000293C File Offset: 0x00000B3C
		internal static IList<TElement> VisitList<TElement>(IList<TElement> list, Func<TElement, TElement> map)
		{
			if (list == null)
			{
				return null;
			}
			IList<TElement> list2 = list;
			List<TElement> list3 = null;
			for (int i = 0; i < list.Count; i++)
			{
				TElement telement = map(list[i]);
				if (list3 == null && list[i] != telement)
				{
					list3 = new List<TElement>(list);
					list2 = list3;
				}
				if (list3 != null)
				{
					list3[i] = telement;
				}
			}
			return list2;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029A0 File Offset: 0x00000BA0
		internal static IReadOnlyList<TElement> VisitReadOnlyList<TElement>(IReadOnlyList<TElement> list, Func<TElement, TElement> map) where TElement : class
		{
			if (list == null)
			{
				return null;
			}
			List<TElement> list2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				TElement telement = map(list[i]);
				if (list2 == null && list[i] != telement)
				{
					list2 = new List<TElement>(list.Count);
					for (int j = 0; j < i; j++)
					{
						list2.Add(list[j]);
					}
				}
				if (list2 != null)
				{
					list2.Add(telement);
				}
			}
			IReadOnlyList<TElement> readOnlyList = list2;
			return readOnlyList ?? list;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A24 File Offset: 0x00000C24
		internal static IReadOnlyList<TResult> CreateList<TInput, TResult>(this IReadOnlyList<TInput> input, Func<TInput, TResult> func)
		{
			if (input == null)
			{
				return null;
			}
			List<TResult> list = new List<TResult>(input.Count);
			int i = 0;
			int count = input.Count;
			while (i < count)
			{
				list.Add(func(input[i]));
				i++;
			}
			return list;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A6C File Offset: 0x00000C6C
		internal static IReadOnlyList<T> CreateList<T>(params IReadOnlyList<T>[] originalLists)
		{
			if (originalLists.Length == 0)
			{
				return Array.Empty<T>();
			}
			List<T> list2 = new List<T>(originalLists.Sum((IReadOnlyList<T> list) => list.Count));
			foreach (IReadOnlyList<T> readOnlyList in originalLists)
			{
				list2.AddRange(readOnlyList);
			}
			return list2;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ACC File Offset: 0x00000CCC
		internal static T SetAtExtendedIndex<T>(this IList<T> list, int index, T value)
		{
			List<T> list2 = list as List<T>;
			if (list2.Capacity < index)
			{
				list2.Capacity = index + 1;
			}
			for (int i = list.Count; i <= index; i++)
			{
				list.Add(default(T));
			}
			T t = list[index];
			list[index] = value;
			return t;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B21 File Offset: 0x00000D21
		internal static bool TryGetNonNullAtExtendedIndex<T>(this IReadOnlyList<T> list, int index, out T value)
		{
			value = default(T);
			if (list != null && list.Count > index && list[index] != null)
			{
				value = list[index];
				return true;
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B54 File Offset: 0x00000D54
		internal static void AddToLazyList<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B69 File Offset: 0x00000D69
		internal static void AddToLazyList<T>(ref List<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.AddRange(items);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B7E File Offset: 0x00000D7E
		internal static void AddToLazySortedList<TKey, TValue>(ref SortedList<TKey, TValue> list, TKey key, TValue value)
		{
			if (list == null)
			{
				list = new SortedList<TKey, TValue>();
			}
			list.Add(key, value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B94 File Offset: 0x00000D94
		internal static Dictionary<K, V> GetLazyDictionary<K, V>(ref Dictionary<K, V> dictionary, IEqualityComparer<K> comparer = null)
		{
			if (dictionary == null)
			{
				comparer = comparer ?? EqualityComparer<K>.Default;
				dictionary = new Dictionary<K, V>(comparer);
			}
			return dictionary;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BB0 File Offset: 0x00000DB0
		internal static bool TryGetFromLazyDictionary<K, V>(Dictionary<K, V> dictionary, K key, out V value)
		{
			if (dictionary == null)
			{
				value = default(V);
				return false;
			}
			return dictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BC6 File Offset: 0x00000DC6
		internal static void AddToLazyDictionary<K, V>(ref Dictionary<K, V> dictionary, K key, V value, IEqualityComparer<K> comparer = null)
		{
			Util.GetLazyDictionary<K, V>(ref dictionary, comparer);
			dictionary.Add(key, value);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public static void PushToLazyStack<T>(ref Stack<T> stack, T item)
		{
			if (stack == null)
			{
				stack = new Stack<T>();
			}
			stack.Push(item);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BEE File Offset: 0x00000DEE
		internal static bool AddToLazySet<T>(ref HashSet<T> set, T item, IEqualityComparer<T> comparer = null)
		{
			if (set == null)
			{
				set = new HashSet<T>(comparer ?? EqualityComparer<T>.Default);
			}
			return set.Add(item);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C10 File Offset: 0x00000E10
		internal static IReadOnlyList<T> RewriteList<T>(IReadOnlyList<T> items, IEqualityComparer<T> comparer, Func<T, T> rewrite)
		{
			if (items.IsNullOrEmpty<T>())
			{
				return items;
			}
			List<T> list = null;
			for (int i = 0; i < items.Count; i++)
			{
				T t = items[i];
				T t2 = rewrite(t);
				if (t != t2)
				{
					if (list == null)
					{
						list = new List<T>(items);
					}
					list[i] = t2;
				}
			}
			if (list.IsNullOrEmpty<T>())
			{
				return items;
			}
			return list;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C78 File Offset: 0x00000E78
		public static IDictionary<T, TValue> RewriteDictionaryKeys<T, TValue>(IDictionary<T, TValue> items, IEqualityComparer<T> comparer, Func<T, T> rewrite)
		{
			if (items == null || items.Count == 0)
			{
				return items;
			}
			Dictionary<T, TValue> dictionary = null;
			foreach (KeyValuePair<T, TValue> keyValuePair in items)
			{
				T key = keyValuePair.Key;
				T t = rewrite(key);
				if (!comparer.Equals(key, t))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<T, TValue>(items, comparer);
					}
					dictionary.Remove(key);
					dictionary[t] = keyValuePair.Value;
				}
			}
			if (dictionary.IsNullOrEmpty<KeyValuePair<T, TValue>>())
			{
				return items;
			}
			return dictionary;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D14 File Offset: 0x00000F14
		public static bool IsNumeric(this object value)
		{
			return !object.Equals(value, null) && (value is double || value is float || value is long || value is decimal || value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is ulong);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D88 File Offset: 0x00000F88
		public static void Add<TKey, TList, TValue>(this IDictionary<TKey, TList> dictionary, TKey key, TValue value) where TList : ICollection<TValue>, new()
		{
			TList tlist;
			if (!dictionary.TryGetValue(key, out tlist))
			{
				tlist = new TList();
				dictionary.Add(key, tlist);
			}
			tlist.Add(value);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static T Single<T>(this IEnumerable<T> collection, string failureMessage, params string[] parameters)
		{
			IList<T> list = collection as IList<T>;
			if (list != null)
			{
				if (list.Count == 1)
				{
					return list[0];
				}
			}
			else
			{
				IReadOnlyList<T> readOnlyList = collection as IReadOnlyList<T>;
				if (readOnlyList != null)
				{
					if (readOnlyList.Count == 1)
					{
						return readOnlyList[0];
					}
				}
				else
				{
					using (IEnumerator<T> enumerator = collection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							if (!enumerator.MoveNext())
							{
								return t;
							}
						}
					}
				}
			}
			throw new InvalidOperationException(StringUtil.FormatInvariant(failureMessage, parameters));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E54 File Offset: 0x00001054
		public static T Single<T>(this IEnumerable<T> collection, Func<T, bool> predicate, string failureMessage, params string[] parameters)
		{
			return collection.Where(predicate).Single(failureMessage, parameters);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E64 File Offset: 0x00001064
		public static IReadOnlyList<T> AsReadOnlyList<T>(this T item)
		{
			return new T[] { item };
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E74 File Offset: 0x00001074
		[DebuggerStepThrough]
		public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> items)
		{
			if (items == null)
			{
				return Util.EmptyReadOnlyList<T>();
			}
			IReadOnlyList<T> readOnlyList = items as IReadOnlyList<T>;
			if (readOnlyList != null)
			{
				if (readOnlyList.Count <= 0)
				{
					return Util.EmptyReadOnlyList<T>();
				}
				return readOnlyList;
			}
			else
			{
				ICollection<T> collection = items as ICollection<T>;
				if (collection != null && collection.Count == 0)
				{
					return Util.EmptyReadOnlyList<T>();
				}
				return items.ToList<T>();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EC3 File Offset: 0x000010C3
		public static List<T> AsList<T>(this T item)
		{
			return new List<T> { item };
		}

		// Token: 0x04000037 RID: 55
		internal const string UnknownSubTypeForTypeSpecificAction = "Unknown subtype for type-specific operation.";

		// Token: 0x02000028 RID: 40
		private static class EmptyCollectionsReadOnlyArray<T>
		{
			// Token: 0x04000082 RID: 130
			internal static readonly ReadOnlyCollection<T> Instance = Array.AsReadOnly<T>(Array.Empty<T>());
		}

		// Token: 0x02000029 RID: 41
		private static class EmptyCollectionsSet<T>
		{
			// Token: 0x04000083 RID: 131
			internal static readonly ISet<T> Instance = new HashSet<T>();
		}

		// Token: 0x0200002A RID: 42
		internal static class DevErrors
		{
			// Token: 0x04000084 RID: 132
			internal const string EmptyStringArgument = "The argument value must be a non-empty string.";

			// Token: 0x04000085 RID: 133
			internal const string WhiteSpaceStringArgument = "The argument value must be a non-empty string that does not consist solely of whitespace.";

			// Token: 0x04000086 RID: 134
			internal const string EmptyCollectionArgument = "The argument value must be a non-empty collection.";

			// Token: 0x04000087 RID: 135
			internal const string InvalidArgument = "The argument value is invalid.";

			// Token: 0x04000088 RID: 136
			internal const string ObjectEquals = "The arguments must be equal";
		}
	}
}
