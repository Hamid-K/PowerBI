using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200037A RID: 890
	internal static class ResamplingUtils
	{
		// Token: 0x06001D1A RID: 7450 RVA: 0x000762D4 File Offset: 0x000744D4
		private static double?[] TrimNullValues(double?[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				return new double?[0];
			}
			int num = 0;
			int num2 = rawData.Length - 1;
			while (rawData[num] == null && ++num != rawData.Length)
			{
			}
			if (num2 < num)
			{
				return new double?[0];
			}
			while (rawData[num2] == null && --num2 >= 0)
			{
			}
			int num3 = num2 - num + 1;
			double?[] array = new double?[num3];
			Array.Copy(rawData, num, array, 0, num3);
			return array;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0007634A File Offset: 0x0007454A
		internal static string[] LttbDownsampleToStrArray(double?[] rawData, int sampledRows)
		{
			double?[] array = ResamplingUtils.LttbDownsample(rawData, sampledRows);
			new string[array.Length];
			return array.Select(delegate(double? x)
			{
				if (x != null)
				{
					return x.Value.ToString("r", CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}).ToArray<string>();
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00076388 File Offset: 0x00074588
		internal static double?[] LttbDownsample(double?[] rawData, int sampledRows)
		{
			double?[] array = ResamplingUtils.TrimNullValues(rawData);
			int num = array.Length;
			if (sampledRows >= num)
			{
				return array;
			}
			if (sampledRows < 3)
			{
				throw new ArgumentOutOfRangeException("sampledRows");
			}
			double?[] array2 = new double?[sampledRows];
			double num2 = (double)(num - 2) / (double)(sampledRows - 2);
			ResamplingUtils.Coordinate[] array3 = new ResamplingUtils.Coordinate[sampledRows - 2];
			int num3 = 0;
			double num4 = 0.0;
			int num5 = 0;
			int num6 = 0;
			ResamplingUtils.Coordinate coordinate = new ResamplingUtils.Coordinate(0.0, 0.0);
			array2[num6++] = array[num3];
			int num7 = num;
			for (int i = sampledRows - 3; i >= 0; i--)
			{
				double num8 = 0.0;
				double num9 = 0.0;
				bool flag = false;
				int j = (int)(Math.Floor((double)(i + 1) * num2) + 1.0);
				int num10 = num7;
				num7 = j;
				int num11 = num10 - j;
				while (j < num10)
				{
					if (array[j] != null)
					{
						num8 += (double)j;
						num9 += array[j] ?? 0.0;
						flag = true;
					}
					j++;
				}
				if (flag)
				{
					coordinate = new ResamplingUtils.Coordinate(num8 / (double)num11, num9 / (double)num11);
				}
				array3[i] = coordinate;
			}
			ResamplingUtils.Coordinate coordinate2 = new ResamplingUtils.Coordinate(0.0, 0.0);
			int num12 = 1;
			for (int k = 0; k < sampledRows - 2; k++)
			{
				int l = num12;
				int num13 = (int)(Math.Floor((double)(k + 1) * num2) + 1.0);
				num12 = num13;
				if (array[num3] != null)
				{
					coordinate2 = new ResamplingUtils.Coordinate((double)num3, array[num3] ?? 0.0);
				}
				double num14 = -1.0;
				while (l < num13)
				{
					if (array[l] != null)
					{
						double num15 = Math.Abs((coordinate2.X - array3[k].X) * ((array[l] ?? 0.0) - coordinate2.Y) - (coordinate2.X - (double)l) * (array3[k].Y - coordinate2.Y)) * 0.5;
						if (num15 > num14)
						{
							num14 = num15;
							num4 = array[l] ?? 0.0;
							num5 = l;
						}
					}
					l++;
				}
				if (num14 < 0.0)
				{
					array2[num6++] = null;
				}
				else
				{
					array2[num6++] = new double?(num4);
					num3 = num5;
				}
			}
			array2[num6++] = array[num - 1];
			return array2;
		}

		// Token: 0x02000502 RID: 1282
		private struct Coordinate
		{
			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x060024FE RID: 9470 RVA: 0x00087648 File Offset: 0x00085848
			// (set) Token: 0x060024FF RID: 9471 RVA: 0x00087650 File Offset: 0x00085850
			public double X { get; private set; }

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x06002500 RID: 9472 RVA: 0x00087659 File Offset: 0x00085859
			// (set) Token: 0x06002501 RID: 9473 RVA: 0x00087661 File Offset: 0x00085861
			public double Y { get; private set; }

			// Token: 0x06002502 RID: 9474 RVA: 0x0008766A File Offset: 0x0008586A
			public Coordinate(double x, double y)
			{
				this = default(ResamplingUtils.Coordinate);
				this.X = x;
				this.Y = y;
			}
		}
	}
}
