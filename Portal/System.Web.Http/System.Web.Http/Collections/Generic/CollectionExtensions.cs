using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000188 RID: 392
	internal static class CollectionExtensions
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0001A120 File Offset: 0x00018320
		public static T[] AppendAndReallocate<T>(this T[] array, T value)
		{
			int num = array.Length;
			T[] array2 = new T[num + 1];
			array.CopyTo(array2, 0);
			array2[num] = value;
			return array2;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001A14C File Offset: 0x0001834C
		public static T[] AsArray<T>(this IEnumerable<T> values)
		{
			T[] array = values as T[];
			if (array == null)
			{
				array = values.ToArray<T>();
			}
			return array;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A16C File Offset: 0x0001836C
		public static Collection<T> AsCollection<T>(this IEnumerable<T> enumerable)
		{
			Collection<T> collection = enumerable as Collection<T>;
			if (collection != null)
			{
				return collection;
			}
			IList<T> list = enumerable as IList<T>;
			if (list == null)
			{
				list = new List<T>(enumerable);
			}
			return new Collection<T>(list);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001A19C File Offset: 0x0001839C
		public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001A1BC File Offset: 0x000183BC
		public static List<T> AsList<T>(this IEnumerable<T> enumerable)
		{
			List<T> list = enumerable as List<T>;
			if (list != null)
			{
				return list;
			}
			ListWrapperCollection<T> listWrapperCollection = enumerable as ListWrapperCollection<T>;
			if (listWrapperCollection != null)
			{
				return listWrapperCollection.ItemsList;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001A1EC File Offset: 0x000183EC
		public static void RemoveFrom<T>(this List<T> list, int start)
		{
			list.RemoveRange(start, list.Count - start);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001A200 File Offset: 0x00018400
		public static T SingleDefaultOrError<T, TArg1>(this IList<T> list, Action<TArg1> errorAction, TArg1 errorArg1)
		{
			int count = list.Count;
			if (count == 0)
			{
				return default(T);
			}
			if (count != 1)
			{
				errorAction(errorArg1);
				return default(T);
			}
			return list[0];
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001A240 File Offset: 0x00018440
		public static TMatch SingleOfTypeDefaultOrError<TInput, TMatch, TArg1>(this IList<TInput> list, Action<TArg1> errorAction, TArg1 errorArg1) where TMatch : class
		{
			TMatch tmatch = default(TMatch);
			for (int i = 0; i < list.Count; i++)
			{
				TMatch tmatch2 = list[i] as TMatch;
				if (tmatch2 != null)
				{
					if (tmatch != null)
					{
						errorAction(errorArg1);
						return default(TMatch);
					}
					tmatch = tmatch2;
				}
			}
			return tmatch;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public static T[] ToArrayWithoutNulls<T>(this ICollection<T> collection) where T : class
		{
			T[] array = new T[collection.Count];
			int num = 0;
			foreach (T t in collection)
			{
				if (t != null)
				{
					array[num] = t;
					num++;
				}
			}
			if (num == collection.Count)
			{
				return array;
			}
			T[] array2 = new T[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001A328 File Offset: 0x00018528
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
			foreach (TValue tvalue in array)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A364 File Offset: 0x00018564
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = list as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A38C File Offset: 0x0001858C
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = enumerable as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			IList<TValue> list = enumerable as IList<TValue>;
			if (list != null)
			{
				return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
			}
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(comparer);
			foreach (TValue tvalue in enumerable)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001A40C File Offset: 0x0001860C
		private static Dictionary<TKey, TValue> ToDictionaryFastNoCheck<TKey, TValue>(IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			int count = list.Count;
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(count, comparer);
			for (int i = 0; i < count; i++)
			{
				TValue tvalue = list[i];
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}
	}
}
