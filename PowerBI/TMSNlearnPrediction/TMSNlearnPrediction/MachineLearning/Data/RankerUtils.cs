using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001EF RID: 495
	internal static class RankerUtils
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0003AFBC File Offset: 0x000391BC
		public static double[] DiscountMap
		{
			get
			{
				if (RankerUtils._discountMap == null)
				{
					double[] array = new double[100];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = 1.0 / Math.Log((double)(2 + i));
					}
					Interlocked.CompareExchange<double[]>(ref RankerUtils._discountMap, array, null);
				}
				return RankerUtils._discountMap;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0003B014 File Offset: 0x00039214
		public static void QueryMaxDCG(double[] labelGains, int truncationLevel, List<short> queryLabels, List<float> queryOutputs, double[] groupMaxDcgCur)
		{
			int num = labelGains.Length;
			int[] array = new int[num];
			int num2 = Math.Min(truncationLevel, queryLabels.Count);
			if (num2 == 0)
			{
				for (int i = 0; i < truncationLevel; i++)
				{
					groupMaxDcgCur[i] = double.NaN;
				}
				return;
			}
			for (int j = 0; j < queryLabels.Count; j++)
			{
				array[(int)queryLabels[j]]++;
			}
			int num3 = labelGains.Length - 1;
			while (array[num3] == 0)
			{
				num3--;
			}
			groupMaxDcgCur[0] = labelGains[num3] * RankerUtils.DiscountMap[0];
			array[num3]--;
			for (int k = 1; k < num2; k++)
			{
				while (array[num3] == 0)
				{
					num3--;
				}
				groupMaxDcgCur[k] = groupMaxDcgCur[k - 1] + labelGains[num3] * RankerUtils.DiscountMap[k];
				array[num3]--;
			}
			for (int l = num2; l < truncationLevel; l++)
			{
				groupMaxDcgCur[l] = groupMaxDcgCur[l - 1];
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0003B128 File Offset: 0x00039328
		public static void QueryDCG(double[] labelGains, int truncationLevel, List<short> queryLabels, List<float> queryOutputs, double[] groupDcgCur)
		{
			int num = queryLabels.Count;
			int[] identityPermutation = Utils.GetIdentityPermutation(num);
			Array.Sort<int>(identityPermutation, RankerUtils.GetCompareItems(queryLabels, queryOutputs));
			if (num > truncationLevel)
			{
				num = truncationLevel;
			}
			double num2 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num2 += labelGains[(int)queryLabels[identityPermutation[i]]] * RankerUtils.DiscountMap[i];
				groupDcgCur[i] = num2;
			}
			for (int j = num; j < truncationLevel; j++)
			{
				groupDcgCur[j] = num2;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003B230 File Offset: 0x00039430
		private static Comparison<int> GetCompareItems(List<short> queryLabels, List<float> queryOutputs)
		{
			return delegate(int i, int j)
			{
				if (queryOutputs[i] > queryOutputs[j])
				{
					return -1;
				}
				if (queryOutputs[i] < queryOutputs[j])
				{
					return 1;
				}
				if (queryLabels[i] < queryLabels[j])
				{
					return -1;
				}
				if (queryLabels[i] > queryLabels[j])
				{
					return 1;
				}
				return i.CompareTo(j);
			};
		}

		// Token: 0x040005D4 RID: 1492
		private static volatile double[] _discountMap;
	}
}
