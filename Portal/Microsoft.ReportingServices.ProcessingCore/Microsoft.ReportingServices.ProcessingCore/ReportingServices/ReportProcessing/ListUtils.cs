using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000661 RID: 1633
	internal class ListUtils
	{
		// Token: 0x06005AA7 RID: 23207 RVA: 0x0017524C File Offset: 0x0017344C
		public static void AdjustLength<T>(List<T> instances, int indexInCollection) where T : class
		{
			for (int i = instances.Count; i <= indexInCollection; i++)
			{
				instances.Add(default(T));
			}
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0017527C File Offset: 0x0017347C
		public static bool ContainsWithOrdinalComparer(string item, List<string> list)
		{
			if (item == null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < list.Count; j++)
			{
				if (string.CompareOrdinal(list[j], item) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x001752D0 File Offset: 0x001734D0
		public static bool AreSameOrSuffix<T>(List<T> list1, List<T> list2, IComparer<T> comparer)
		{
			if (list1 == null || list2 == null)
			{
				return list1 == list2;
			}
			int num = Math.Min(list1.Count, list2.Count);
			int num2 = list1.Count - num;
			int num3 = list2.Count - num;
			for (int i = 0; i < num; i++)
			{
				T t = list1[num2 + i];
				T t2 = list2[num3 + i];
				if (comparer.Compare(t, t2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x00175340 File Offset: 0x00173540
		public static bool IsSubSequenceAllowExtras<T>(List<T> super, List<T> subCandidate, IComparer<T> comparer)
		{
			if (super == null || subCandidate == null)
			{
				return super == subCandidate;
			}
			if (subCandidate.Count > super.Count)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < super.Count && num < subCandidate.Count)
			{
				T t = super[num2];
				T t2 = subCandidate[num];
				if (comparer.Compare(t, t2) == 0)
				{
					num++;
				}
				num2++;
			}
			return num == subCandidate.Count;
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x001753AC File Offset: 0x001735AC
		public static bool Contains<T>(List<T> list, T item, IComparer<T> comparer)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Compare(list[i], item) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x001753E0 File Offset: 0x001735E0
		public static bool IsSubset<T>(List<T> super, List<T> subCandidate, IComparer<T> comparer)
		{
			for (int i = 0; i < subCandidate.Count; i++)
			{
				if (!ListUtils.Contains<T>(super, subCandidate[i], comparer))
				{
					return false;
				}
			}
			return true;
		}
	}
}
