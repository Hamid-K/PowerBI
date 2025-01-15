using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav
{
	// Token: 0x02000025 RID: 37
	internal static class RangeExtensions
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00005F68 File Offset: 0x00004168
		internal static IList<TRange> GetLongestRanges<TRange>(this IList<TRange> sortedRanges) where TRange : IRange
		{
			int count = sortedRanges.Count;
			if (count <= 1)
			{
				return sortedRanges;
			}
			int num = int.MinValue;
			List<TRange> list = new List<TRange>(count);
			for (int i = 0; i < count; i++)
			{
				int num2 = i;
				TRange trange;
				while (i < count - 1)
				{
					trange = sortedRanges[i];
					int firstIndex = trange.FirstIndex;
					trange = sortedRanges[i + 1];
					if (firstIndex != trange.FirstIndex)
					{
						break;
					}
					trange = sortedRanges[i + 1];
					int lastIndex = trange.LastIndex;
					trange = sortedRanges[i];
					if (lastIndex > trange.LastIndex)
					{
						num2 = i + 1;
					}
					i++;
				}
				trange = sortedRanges[num2];
				if (trange.LastIndex > num)
				{
					list.Add(sortedRanges[num2]);
					trange = sortedRanges[num2];
					num = trange.LastIndex;
				}
			}
			return list;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006058 File Offset: 0x00004258
		internal static IList<TRange> GetLongestNonOverlappingRanges<TRange>(this IList<TRange> sortedRanges) where TRange : IRange
		{
			int count = sortedRanges.Count;
			if (count <= 1)
			{
				return sortedRanges;
			}
			int num = int.MinValue;
			int num2 = int.MinValue;
			int num3 = int.MinValue;
			List<TRange> list = new List<TRange>(count);
			for (int i = 0; i < count; i++)
			{
				TRange trange = sortedRanges[i];
				if (trange.FirstIndex > num2)
				{
					if (num != -2147483648)
					{
						list.Add(sortedRanges[num]);
					}
					num = i;
					trange = sortedRanges[i];
					num2 = trange.LastIndex;
					trange = sortedRanges[i];
					int lastIndex = trange.LastIndex;
					trange = sortedRanges[i];
					num3 = lastIndex - trange.FirstIndex;
				}
				else
				{
					trange = sortedRanges[i];
					int lastIndex2 = trange.LastIndex;
					trange = sortedRanges[i];
					if (lastIndex2 - trange.FirstIndex > num3)
					{
						num = i;
						trange = sortedRanges[i];
						num2 = trange.LastIndex;
						trange = sortedRanges[i];
						int lastIndex3 = trange.LastIndex;
						trange = sortedRanges[i];
						num3 = lastIndex3 - trange.FirstIndex;
					}
				}
			}
			list.Add(sortedRanges[num]);
			return list;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000061AC File Offset: 0x000043AC
		internal static IList<IList<TRange>> GenerateNonOverlappingSequences<TRange>(this IList<TRange> items, int sequenceLimit) where TRange : IRange
		{
			return items.GenerateNonOverlappingSequences(sequenceLimit, 0);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000061B8 File Offset: 0x000043B8
		private static IList<IList<TRange>> GenerateNonOverlappingSequences<TRange>(this IList<TRange> items, int sequenceLimit, int currentIndex) where TRange : IRange
		{
			List<IList<TRange>> list = new List<IList<TRange>>();
			if (currentIndex >= items.Count)
			{
				return list;
			}
			int num = RangeExtensions.FindAlternateItems<TRange>(items, currentIndex);
			int num2 = currentIndex;
			while (num2 < num && list.Count < sequenceLimit)
			{
				TRange trange = items[num2];
				int num3 = RangeExtensions.FindNextNonOverlappingItem<TRange>(items, num2);
				if (num3 < items.Count)
				{
					IList<IList<TRange>> list2 = items.GenerateNonOverlappingSequences(sequenceLimit, num3);
					for (int i = 0; i < list2.Count; i++)
					{
						if (list.Count >= sequenceLimit)
						{
							break;
						}
						IList<TRange> list3 = list2[i];
						TRange[] array = new TRange[list3.Count + 1];
						array[0] = trange;
						for (int j = 0; j < list3.Count; j++)
						{
							array[j + 1] = list3[j];
						}
						list.Add(array);
					}
				}
				else
				{
					list.Add(new TRange[] { trange });
				}
				num2++;
			}
			return list;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000062B0 File Offset: 0x000044B0
		private static int FindAlternateItems<TRange>(IList<TRange> items, int index) where TRange : IRange
		{
			TRange trange = items[index];
			int num = trange.LastIndex;
			int i;
			for (i = index + 1; i < items.Count; i++)
			{
				trange = items[i];
				if (trange.FirstIndex > num)
				{
					break;
				}
				trange = items[i];
				if (trange.LastIndex <= num)
				{
					trange = items[i];
					num = trange.LastIndex;
				}
			}
			return i;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006330 File Offset: 0x00004530
		private static int FindNextNonOverlappingItem<TRange>(IList<TRange> items, int index) where TRange : IRange
		{
			TRange trange = items[index];
			int i;
			for (i = index + 1; i < items.Count; i++)
			{
				TRange trange2 = items[i];
				if (trange2.FirstIndex > trange.LastIndex)
				{
					break;
				}
			}
			return i;
		}
	}
}
