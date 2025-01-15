using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000008 RID: 8
	internal static class CollectionUtil
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000023C0 File Offset: 0x000005C0
		public static bool IsEmpty<T>(IEnumerable<T> items)
		{
			ICollection<T> collection = items as ICollection<T>;
			if (collection == null)
			{
				return !items.GetEnumerator().MoveNext();
			}
			return collection.Count > 0;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023EF File Offset: 0x000005EF
		public static bool ElementsEqual<T>(ICollection<T> items1, ICollection<T> items2)
		{
			return CollectionUtil.ElementsEqual<T>(items1, items2, null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023FC File Offset: 0x000005FC
		public static bool ElementsEqual<T>(ICollection<T> items1, ICollection<T> items2, IEqualityComparer<T> comparer)
		{
			if (items1 == null)
			{
				throw new ArgumentNullException("items1");
			}
			if (items2 == null)
			{
				throw new ArgumentNullException("items2");
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			if (items1.Count != items2.Count)
			{
				return false;
			}
			IList<T> list = items1 as IList<T>;
			IList<T> list2 = items2 as IList<T>;
			if (list != null && list2 != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (!comparer.Equals(list[i], list2[i]))
					{
						return false;
					}
				}
				return true;
			}
			IEnumerator<T> enumerator = items1.GetEnumerator();
			IEnumerator<T> enumerator2 = items2.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator2.MoveNext();
				if (!comparer.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024B8 File Offset: 0x000006B8
		public static bool ContainsAll<T>(ICollection<T> items1, ICollection<T> items2)
		{
			if (items1 == null)
			{
				throw new ArgumentNullException("items1");
			}
			if (items2 == null)
			{
				throw new ArgumentNullException("items2");
			}
			foreach (T t in items2)
			{
				if (!items1.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002528 File Offset: 0x00000728
		public static void RemoveDuplicates<T>(IList<T> items)
		{
			CollectionUtil.RemoveDuplicates<T>(items, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002534 File Offset: 0x00000734
		public static void RemoveDuplicates<T>(IList<T> items, IEqualityComparer<T> comparer)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			for (int i = 0; i < items.Count; i++)
			{
				for (int j = items.Count - 1; j > i; j--)
				{
					if (comparer.Equals(items[i], items[j]))
					{
						items.RemoveAt(j);
					}
				}
			}
		}
	}
}
