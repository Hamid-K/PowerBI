using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200021E RID: 542
	public static class FreeFunctions
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x00032518 File Offset: 0x00030718
		public static Dictionary<TKey, TValue> ToGenericDictionary<TKey, TValue>(this IDictionary dictionary)
		{
			Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary2.Add((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
			}
			return dictionary2;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00032590 File Offset: 0x00030790
		public static T FindIf<T>(ICollection<T> collection, Predicate<T> predicate)
		{
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000325EC File Offset: 0x000307EC
		public static int Accumulate<T>(ICollection<T> collection, Accumulator<T> accumulator)
		{
			int num = 0;
			foreach (T t in collection)
			{
				num += accumulator(t);
			}
			return num;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003263C File Offset: 0x0003083C
		public static bool Equal<T>(ICollection<T> c1, ICollection<T> c2, BinaryPredicate<T> predicate)
		{
			IEnumerator<T> enumerator = c1.GetEnumerator();
			IEnumerator<T> enumerator2 = c2.GetEnumerator();
			bool flag;
			bool flag3;
			do
			{
				flag = enumerator.MoveNext();
				bool flag2 = enumerator2.MoveNext();
				flag3 = flag == flag2;
				if (flag3 && flag)
				{
					flag3 = predicate(enumerator.Current, enumerator2.Current);
				}
			}
			while (flag3 && flag);
			return flag3;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003268C File Offset: 0x0003088C
		public static void Swap<T>(ref T t1, ref T t2)
		{
			T t3 = t1;
			t1 = t2;
			t2 = t3;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000326B4 File Offset: 0x000308B4
		public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				queue.Enqueue(t);
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000326FC File Offset: 0x000308FC
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				list.Add(t);
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00032744 File Offset: 0x00030944
		public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				hashSet.Add(t);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00032790 File Offset: 0x00030990
		public static void AddRange<K, V>(this IDictionary<K, V> dictionary, IEnumerable<KeyValuePair<K, V>> items)
		{
			foreach (KeyValuePair<K, V> keyValuePair in items)
			{
				try
				{
					dictionary.Add(keyValuePair);
				}
				catch (ArgumentException ex)
				{
					throw new ArgumentException("Unable to add item with key: '{0}' and value: '{1}' to the dictionary.".FormatWithInvariantCulture(new object[] { keyValuePair.Key, keyValuePair.Value }), ex);
				}
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003281C File Offset: 0x00030A1C
		public static void RemoveRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				hashSet.Remove(t);
			}
		}
	}
}
