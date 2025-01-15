using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting
{
	// Token: 0x020000C5 RID: 197
	internal static class Util
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		internal static void RaiseEvent(this EventHandler eventHandler, object sender, EventArgs eventArgs)
		{
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(sender, eventArgs);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00020B06 File Offset: 0x0001ED06
		internal static void RaiseEvent(this EventHandler eventHandler, object sender)
		{
			eventHandler.RaiseEvent(sender, EventArgs.Empty);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00020B14 File Offset: 0x0001ED14
		internal static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
		{
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(sender, eventArgs);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00020B22 File Offset: 0x0001ED22
		internal static T CheckEqual<T>(T arg1, T arg2)
		{
			if (!object.Equals(arg1, arg2))
			{
				throw new ArgumentException("The arguments must be equal", "arg1");
			}
			return arg1;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00020B48 File Offset: 0x0001ED48
		internal static IList<T> Evaluate<T>(this IEnumerable<T> source)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				return list;
			}
			return source.ToList<T>();
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00020B68 File Offset: 0x0001ED68
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

		// Token: 0x06000C7B RID: 3195 RVA: 0x00020BBC File Offset: 0x0001EDBC
		internal static void ReplaceWith<T>(this ICollection<T> collection, T value)
		{
			collection.Clear();
			collection.Add(value);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00020BCB File Offset: 0x0001EDCB
		internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return Enumerable.Empty<T>();
			}
			return source;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00020BD8 File Offset: 0x0001EDD8
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
			return Util.EmptyCollections<T>.ReadOnlyCollectionInstance;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00020BFF File Offset: 0x0001EDFF
		internal static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IList<T> items)
		{
			if (items == null)
			{
				return Util.EmptyReadOnlyCollection<T>();
			}
			return new ReadOnlyCollection<T>(items);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00020C10 File Offset: 0x0001EE10
		internal static HashSet<T> ToSet<T>(this IEnumerable<T> items)
		{
			return new HashSet<T>(items);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00020C18 File Offset: 0x0001EE18
		internal static HashSet<T> ToSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(items, comparer);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00020C21 File Offset: 0x0001EE21
		internal static IEnumerable<T> Concat<T>(this IEnumerable<T> source, params T[] values)
		{
			return source.Concat(values);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00020C2A File Offset: 0x0001EE2A
		internal static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] values)
		{
			return source.Except(values);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00020C33 File Offset: 0x0001EE33
		internal static bool IsSupersetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !second.Except(first).Any<T>();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00020C44 File Offset: 0x0001EE44
		internal static bool IsSubsetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !first.Except(second).Any<T>();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00020C55 File Offset: 0x0001EE55
		internal static IEnumerable<T> TakeAfter<T>(this IEnumerable<T> source, T value)
		{
			return source.TakeAfter(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00020C63 File Offset: 0x0001EE63
		internal static IEnumerable<T> TakeAfter<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(source, "source");
			ArgumentValidation.CheckNotNull<IEqualityComparer<T>>(comparer, "comparer");
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (comparer.Equals(value, enumerator.Current))
					{
						IL_00AD:
						while (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							yield return t;
						}
						goto JumpOutOfTryFinally-3;
					}
				}
				goto IL_00AD;
			}
			JumpOutOfTryFinally-3:
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00020C81 File Offset: 0x0001EE81
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, T value)
		{
			return source.TakeUntil(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00020C90 File Offset: 0x0001EE90
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
		{
			return source.TakeUntil((T v) => comparer.Equals(value, v));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00020CC3 File Offset: 0x0001EEC3
		internal static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(source, "source");
			ArgumentValidation.CheckNotNull<Func<T, bool>>(predicate, "predicate");
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

		// Token: 0x06000C8A RID: 3210 RVA: 0x00020CDA File Offset: 0x0001EEDA
		internal static IEnumerable<T> WhereNonNull<T>(this IEnumerable<T> source) where T : class
		{
			return source.Where((T item) => item != null);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00020D04 File Offset: 0x0001EF04
		internal static T Min<T>(this IEnumerable<T> source, Func<T, T, int> comparer)
		{
			ArgumentValidation.CheckNotNull<Func<T, T, int>>(comparer, "comparer");
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

		// Token: 0x06000C8C RID: 3212 RVA: 0x00020D80 File Offset: 0x0001EF80
		internal static T[] EmptyArray<T>()
		{
			return Util.EmptyCollections<T>.ArrayInstance;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00020D87 File Offset: 0x0001EF87
		internal static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			return source == null || !source.Any<T>();
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00020D97 File Offset: 0x0001EF97
		internal static bool IsNullOrEmpty<T>(this IList<T> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00020DA7 File Offset: 0x0001EFA7
		internal static ReadOnlyCollection<T> EmptyReadOnlyCollection<T>()
		{
			return Util.EmptyCollections<T>.ReadOnlyCollectionInstance;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00020DAE File Offset: 0x0001EFAE
		internal static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T item)
		{
			return Util.AsEnumerable<T>(item).Concat(source);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00020DBC File Offset: 0x0001EFBC
		internal static IEnumerable<T> AsEnumerable<T>(T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00020DCC File Offset: 0x0001EFCC
		internal static T Min<T>(this T value1, T value2) where T : IComparable<T>
		{
			if (value1.CompareTo(value2) > 0)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00020DE2 File Offset: 0x0001EFE2
		internal static T Max<T>(this T value1, T value2) where T : IComparable<T>
		{
			if (value1.CompareTo(value2) < 0)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00020DF8 File Offset: 0x0001EFF8
		internal static bool TryFunc<TReturnType>(Func<TReturnType> func, out TReturnType returnValue, out Exception thrownException)
		{
			bool flag;
			try
			{
				returnValue = func();
				thrownException = null;
				flag = true;
			}
			catch (Exception ex)
			{
				returnValue = default(TReturnType);
				thrownException = ex;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00020E38 File Offset: 0x0001F038
		internal static bool HasDuplicates<T>(this IEnumerable<T> source)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(source, "source");
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

		// Token: 0x06000C96 RID: 3222 RVA: 0x00020E9C File Offset: 0x0001F09C
		internal static bool SyncToListDistinctUnordered<T>(this IEnumerable<T> source, IList<T> target)
		{
			bool flag = false;
			HashSet<T> hashSet = new HashSet<T>(source);
			int num = target.Count - 1;
			while (0 <= num)
			{
				if (!hashSet.Remove(target[num]))
				{
					target.RemoveAt(num);
					flag = true;
				}
				num--;
			}
			foreach (T t in hashSet)
			{
				target.Add(t);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00020F24 File Offset: 0x0001F124
		internal static bool SetEquals<T>(this IEnumerable<T> source, IEnumerable<T> other)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(source, "source");
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(other, "other");
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

		// Token: 0x06000C98 RID: 3224 RVA: 0x00020F7C File Offset: 0x0001F17C
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

		// Token: 0x06000C99 RID: 3225 RVA: 0x00020FD8 File Offset: 0x0001F1D8
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

		// Token: 0x06000C9A RID: 3226 RVA: 0x00021030 File Offset: 0x0001F230
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

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002108C File Offset: 0x0001F28C
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

		// Token: 0x06000C9C RID: 3228 RVA: 0x000210E4 File Offset: 0x0001F2E4
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

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002113C File Offset: 0x0001F33C
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

		// Token: 0x06000C9E RID: 3230 RVA: 0x00021194 File Offset: 0x0001F394
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

		// Token: 0x06000C9F RID: 3231 RVA: 0x000211EC File Offset: 0x0001F3EC
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

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00021244 File Offset: 0x0001F444
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

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002129C File Offset: 0x0001F49C
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

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00021300 File Offset: 0x0001F500
		internal static IReadOnlyList<TElement> VisitReadOnlyList<TElement>(IReadOnlyList<TElement> list, Func<TElement, TElement> map)
		{
			if (list == null)
			{
				return null;
			}
			IReadOnlyList<TElement> readOnlyList = list;
			List<TElement> list2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				TElement telement = map(list[i]);
				if (list2 == null && list[i] != telement)
				{
					list2 = new List<TElement>(list);
					readOnlyList = list2;
				}
				if (list2 != null)
				{
					list2[i] = telement;
				}
			}
			return readOnlyList;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00021364 File Offset: 0x0001F564
		internal static void SetAtExtendedIndex<T>(this IList<T> list, int index, T value)
		{
			for (int i = list.Count; i <= index; i++)
			{
				list.Add(default(T));
			}
			list[index] = value;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00021399 File Offset: 0x0001F599
		internal static void AddToLazyList<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000213AE File Offset: 0x0001F5AE
		internal static void AddToLazyList<T>(ref IList<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000213C3 File Offset: 0x0001F5C3
		internal static void AddToLazyList<T>(ref List<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.AddRange(items);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000213D8 File Offset: 0x0001F5D8
		internal static bool AddToLazySet<T>(ref HashSet<T> set, T item)
		{
			if (set == null)
			{
				set = new HashSet<T>();
			}
			return set.Add(item);
		}

		// Token: 0x0400096C RID: 2412
		internal const string UnknownSubTypeForTypeSpecificAction = "Unknown subtype for type-specific operation.";

		// Token: 0x020002D2 RID: 722
		private static class EmptyCollections<T>
		{
			// Token: 0x0400102C RID: 4140
			internal static readonly T[] ArrayInstance = new T[0];

			// Token: 0x0400102D RID: 4141
			internal static readonly ReadOnlyCollection<T> ReadOnlyCollectionInstance = Array.AsReadOnly<T>(new T[0]);
		}

		// Token: 0x020002D3 RID: 723
		internal static class DevErrors
		{
			// Token: 0x0400102E RID: 4142
			internal const string EmptyStringArgument = "The argument value must be a non-empty string.";

			// Token: 0x0400102F RID: 4143
			internal const string WhiteSpaceStringArgument = "The argument value must be a non-empty string that does not consist solely of whitespace.";

			// Token: 0x04001030 RID: 4144
			internal const string EmptyCollectionArgument = "The argument value must be a non-empty collection.";

			// Token: 0x04001031 RID: 4145
			internal const string InvalidArgument = "The argument value is invalid.";

			// Token: 0x04001032 RID: 4146
			internal const string ObjectEquals = "The arguments must be equal";
		}
	}
}
