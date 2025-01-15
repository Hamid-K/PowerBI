using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000019 RID: 25
	internal static class CollectionExtensions
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static T[] AppendAndReallocate<T>(this T[] array, T value)
		{
			int num = array.Length;
			T[] array2 = new T[num + 1];
			array.CopyTo(array2, 0);
			array2[num] = value;
			return array2;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000039FC File Offset: 0x00001BFC
		public static T[] AsArray<T>(this IEnumerable<T> values)
		{
			T[] array = values as T[];
			if (array == null)
			{
				array = values.ToArray<T>();
			}
			return array;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003A1C File Offset: 0x00001C1C
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

		// Token: 0x060000C7 RID: 199 RVA: 0x00003A4C File Offset: 0x00001C4C
		public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A6C File Offset: 0x00001C6C
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00003A9C File Offset: 0x00001C9C
		public static void RemoveFrom<T>(this List<T> list, int start)
		{
			list.RemoveRange(start, list.Count - start);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003AB0 File Offset: 0x00001CB0
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

		// Token: 0x060000CB RID: 203 RVA: 0x00003AF0 File Offset: 0x00001CF0
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

		// Token: 0x060000CC RID: 204 RVA: 0x00003B54 File Offset: 0x00001D54
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

		// Token: 0x060000CD RID: 205 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
			foreach (TValue tvalue in array)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003C14 File Offset: 0x00001E14
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = list as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003C3C File Offset: 0x00001E3C
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00003CBC File Offset: 0x00001EBC
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
