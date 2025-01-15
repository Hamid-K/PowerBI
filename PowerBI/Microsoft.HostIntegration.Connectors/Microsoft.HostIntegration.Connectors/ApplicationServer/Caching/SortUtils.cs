using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000392 RID: 914
	internal static class SortUtils
	{
		// Token: 0x0600202E RID: 8238 RVA: 0x00061EC5 File Offset: 0x000600C5
		public static void SelectSorted<T>(IList<T> list, int sortFirstNElements, Comparison<T> comparer)
		{
			SortUtils.SelectSorted<T, T>(list, sortFirstNElements, (T x) => x, comparer);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00061EDB File Offset: 0x000600DB
		public static void SelectSorted<T>(IList<T> list, int sortFirstNElements)
		{
			SortUtils.SelectSorted<T>(list, sortFirstNElements, new Comparison<T>(Comparer<T>.Default.Compare));
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00061EF5 File Offset: 0x000600F5
		public static void SelectSorted<T, TSort>(IList<T> list, int sortFirstNElements, Converter<T, TSort> conv)
		{
			SortUtils.SelectSorted<T, TSort>(list, sortFirstNElements, conv, new Comparison<TSort>(Comparer<TSort>.Default.Compare));
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00061F10 File Offset: 0x00060110
		public static void SelectSorted<T, TSort>(IList<T> list, int sortFirstNElements, Converter<T, TSort> conv, Comparison<TSort> comparer)
		{
			SortUtils.QuickSort<T, TSort>(list, 0, list.Count - 1, conv, comparer, sortFirstNElements);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00061F24 File Offset: 0x00060124
		private static void QuickSort<T, TSort>(IList<T> list, int left, int right, Converter<T, TSort> conv, Comparison<TSort> comparer, int first)
		{
			if (right <= left || first <= 0)
			{
				return;
			}
			SortUtils.MedianOf3<T, TSort>(list, left, right, left + (right - left >> 1), conv, comparer);
			TSort tsort = conv(list[right]);
			int i = left;
			int num = right - 1;
			while (i <= num)
			{
				while (i <= num)
				{
					if (comparer(conv(list[i]), tsort) >= 0)
					{
						break;
					}
					i++;
				}
				while (i <= num && comparer(tsort, conv(list[num])) < 0)
				{
					num--;
				}
				if (i <= num)
				{
					SortUtils.Swap<T>(list, i++, num--);
				}
			}
			SortUtils.Swap<T>(list, i, right);
			SortUtils.QuickSort<T, TSort>(list, left, i - 1, conv, comparer, first);
			SortUtils.QuickSort<T, TSort>(list, i + 1, right, conv, comparer, first + left - i - 1);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00061FEC File Offset: 0x000601EC
		private static void MedianOf3<T, TSort>(IList<T> list, int left, int right, int median, Converter<T, TSort> conv, Comparison<TSort> comparer)
		{
			TSort tsort = conv(list[left]);
			TSort tsort2 = conv(list[right]);
			TSort tsort3 = conv(list[median]);
			bool flag = comparer(tsort, tsort2) > 0;
			bool flag2 = comparer(tsort, tsort3) > 0;
			bool flag3 = comparer(tsort2, tsort3) > 0;
			if (flag != flag2)
			{
				SortUtils.Swap<T>(list, left, right);
				return;
			}
			if (flag2 != flag3)
			{
				SortUtils.Swap<T>(list, median, right);
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0006206C File Offset: 0x0006026C
		private static void Swap<T>(IList<T> list, int first, int second)
		{
			T t = list[first];
			list[first] = list[second];
			list[second] = t;
		}
	}
}
