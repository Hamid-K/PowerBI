using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000061 RID: 97
	internal static class CollectionExtensions
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public static T[] AppendAndReallocate<T>(this T[] array, T value)
		{
			int num = array.Length;
			T[] array2 = new T[num + 1];
			array.CopyTo(array2, 0);
			array2[num] = value;
			return array2;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public static T[] AsArray<T>(this IEnumerable<T> values)
		{
			T[] array = values as T[];
			if (array == null)
			{
				array = values.ToArray<T>();
			}
			return array;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000CB80 File Offset: 0x0000AD80
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

		// Token: 0x0600038C RID: 908 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
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

		// Token: 0x0600038E RID: 910 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public static void RemoveFrom<T>(this List<T> list, int start)
		{
			list.RemoveRange(start, list.Count - start);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000CC14 File Offset: 0x0000AE14
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

		// Token: 0x06000390 RID: 912 RVA: 0x0000CC54 File Offset: 0x0000AE54
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

		// Token: 0x06000391 RID: 913 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
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

		// Token: 0x06000392 RID: 914 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
			foreach (TValue tvalue in array)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = list as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
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

		// Token: 0x06000395 RID: 917 RVA: 0x0000CE20 File Offset: 0x0000B020
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
