using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.MachineLearning.Dracula.FeatureInference
{
	// Token: 0x0200041E RID: 1054
	internal sealed class SlotSetStats
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x0007FEF4 File Offset: 0x0007E0F4
		public double GetEntropyGain()
		{
			if (this._cachedEntropyGain != null)
			{
				return this._cachedEntropyGain.Value;
			}
			float num = this._priorCounts.Sum();
			int n = this._settings.LabelCardinality;
			double num2 = 0.0;
			foreach (float[] array in this._counts)
			{
				float num3 = array[n];
				double num4 = (double)num3 / (double)num;
				double num5 = SlotSetStats.CalculateEntropy(array, n, num3);
				num2 += num4 * num5;
			}
			float num6 = this._counts.Sum((float[] c) => c[n]);
			num2 += this._priorEntropy * (double)(num - num6) / (double)num;
			this._cachedEntropyGain = new double?(this._startingEntropy - num2);
			return this._cachedEntropyGain.Value;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0007FFDD File Offset: 0x0007E1DD
		private SlotSetStats(FeatureInferenceSettings settings, HashSet<int> includedColumns, float[] priorCounts, double priorEntropy, double startingEntropy, float[][] counts)
		{
			this._settings = settings;
			this.IncludedColumns = includedColumns;
			this._priorCounts = priorCounts;
			this._priorEntropy = priorEntropy;
			this._startingEntropy = startingEntropy;
			this._counts = counts;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0008002C File Offset: 0x0007E22C
		public static SlotSetStats CreateFromSingleColumn(FeatureInferenceSettings settings, int columnIndex, ICountTable countTable)
		{
			List<float[]> list = new List<float[]>();
			float[] array = new float[settings.LabelCardinality];
			foreach (RawCountKey rawCountKey in countTable.AllRawCountKeys())
			{
				if (rawCountKey.HashId <= 0)
				{
					countTable.GetRawCounts(rawCountKey, array);
					float num = array.Sum();
					if (num >= settings.SignificanceThreshold)
					{
						float[] array2 = new float[settings.LabelCardinality + 1];
						Array.Copy(array, 0, array2, 0, settings.LabelCardinality);
						array2[settings.LabelCardinality] = num;
						list.Add(array2);
					}
				}
			}
			float[][] array3 = list.OrderByDescending((float[] x) => x[settings.LabelCardinality]).Take(1000).ToArray<float[]>();
			float[] array4 = new float[settings.LabelCardinality];
			float[] array5 = new float[settings.LabelCardinality];
			countTable.GetPriors(array4, array5);
			double num2 = SlotSetStats.CalculateEntropy(array4, settings.LabelCardinality, array4.Sum());
			return new SlotSetStats(settings, new HashSet<int> { columnIndex }, array4, num2, num2, array3);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000801A0 File Offset: 0x0007E3A0
		private static double CalculateEntropy(float[] counts, int n, float total)
		{
			double num = 0.0;
			for (int i = 0; i < n; i++)
			{
				float num2 = counts[i];
				if (num2 != 0f && num2 != total)
				{
					double num3 = (double)num2 / (double)total;
					num -= num3 * Math.Log(num3);
				}
			}
			return num;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000801F8 File Offset: 0x0007E3F8
		public static SlotSetStats CreateCombined(SlotSetStats s1, SlotSetStats s2)
		{
			List<float[]> list = new List<float[]>();
			int n = s1._settings.LabelCardinality;
			float num = s1._priorCounts.Sum();
			foreach (float[] array in s1._counts)
			{
				foreach (float[] array2 in s2._counts)
				{
					double num2 = (double)array[n] / (double)num * (double)array2[n];
					if (num2 < (double)s1._settings.SignificanceThreshold)
					{
						break;
					}
					float[] array3 = new float[n + 1];
					double num3 = 0.0;
					double num4 = num2 / (double)(array[n] + array2[n]);
					for (int k = 0; k < n; k++)
					{
						double num5 = (double)(array[k] + array2[k]) * num4;
						num3 += num5;
						array3[k] = (float)num5;
					}
					array3[n] = (float)num3;
					list.Add(array3);
				}
			}
			float[][] array4 = list.OrderByDescending((float[] x) => x[n]).Take(1000).ToArray<float[]>();
			return new SlotSetStats(s1._settings, new HashSet<int>(s1.IncludedColumns.Concat(s2.IncludedColumns)), s1._priorCounts, s1._priorEntropy, s1._priorEntropy - Math.Max(s1.GetEntropyGain(), s2.GetEntropyGain()), array4);
		}

		// Token: 0x04000D77 RID: 3447
		private const int NumBinsToKeep = 1000;

		// Token: 0x04000D78 RID: 3448
		private readonly FeatureInferenceSettings _settings;

		// Token: 0x04000D79 RID: 3449
		public readonly HashSet<int> IncludedColumns;

		// Token: 0x04000D7A RID: 3450
		private readonly float[][] _counts;

		// Token: 0x04000D7B RID: 3451
		private readonly float[] _priorCounts;

		// Token: 0x04000D7C RID: 3452
		private readonly double _priorEntropy;

		// Token: 0x04000D7D RID: 3453
		private readonly double _startingEntropy;

		// Token: 0x04000D7E RID: 3454
		private double? _cachedEntropyGain;
	}
}
