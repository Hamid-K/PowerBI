using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000365 RID: 869
	internal static class CollectionUtil
	{
		// Token: 0x06001C9B RID: 7323 RVA: 0x00073618 File Offset: 0x00071818
		public static bool IsEmpty<T>(IEnumerable<T> items)
		{
			ICollection<T> collection = items as ICollection<T>;
			if (collection == null)
			{
				return !items.GetEnumerator().MoveNext();
			}
			return collection.Count > 0;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00073647 File Offset: 0x00071847
		public static bool ElementsEqual<T>(ICollection<T> items1, ICollection<T> items2)
		{
			return CollectionUtil.ElementsEqual<T>(items1, items2, null);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00073654 File Offset: 0x00071854
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

		// Token: 0x06001C9E RID: 7326 RVA: 0x00073710 File Offset: 0x00071910
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

		// Token: 0x06001C9F RID: 7327 RVA: 0x00073780 File Offset: 0x00071980
		public static void RemoveDuplicates<T>(IList<T> items)
		{
			CollectionUtil.RemoveDuplicates<T>(items, null);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007378C File Offset: 0x0007198C
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
