using System;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x0200017D RID: 381
	internal static class ListExtensions
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			List<T> list2 = list as List<T>;
			if (list2 != null)
			{
				list2.AddRange(items);
				return;
			}
			foreach (T t in items)
			{
				list.Add(t);
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000C758 File Offset: 0x0000A958
		public static int IndexOf<T>(this IList<T> list, Predicate<T> predicate)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000C788 File Offset: 0x0000A988
		public static IEnumerable<T> SubList<T>(this IList<T> list, int start, int count)
		{
			int num;
			for (int i = start; i < start + count; i = num + 1)
			{
				yield return list[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public static T[] ToArray<T>(this IList<T> list)
		{
			T[] array = new T[list.Count];
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public static T[] ToArrayInsertAt<T>(this IList<T> list, int pos, T element)
		{
			T[] array = new T[list.Count + 1];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < pos)
				{
					array[i] = list[i];
				}
				else if (i > pos)
				{
					array[i] = list[i - 1];
				}
				else
				{
					array[i] = element;
				}
			}
			return array;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000C828 File Offset: 0x0000AA28
		public static T[] ToArrayRemoveAt<T>(this IList<T> list, int pos)
		{
			T[] array = new T[list.Count - 1];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < pos)
				{
					array[i] = list[i];
				}
				else
				{
					array[i] = list[i + 1];
				}
			}
			return array;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000C878 File Offset: 0x0000AA78
		public static T[] ToArrayReplaceAt<T>(this IList<T> list, int pos, T element)
		{
			T[] array = new T[list.Count];
			list.CopyTo(array, 0);
			array[pos] = element;
			return array;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000C8A2 File Offset: 0x0000AAA2
		public static void CopyTo<T>(this IList<T> source, IList<T> destination)
		{
			source.CopyTo(0, destination, 0, source.Count);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public static void CopyTo<T>(this IList<T> source, int sourceIndex, IList<T> destination, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				destination[destinationIndex + i] = source[sourceIndex + i];
			}
		}
	}
}
