using System;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x02000009 RID: 9
	internal static class ForecastMathUtils
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002FA5 File Offset: 0x000011A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void InterpolateValue(double pxnumLowHighRangeX, double pxnumLowX, double pxnumLowY, double pxnumHighY, double pxnumX, out double pxnumY)
		{
			pxnumY = (pxnumX - pxnumLowX) * (pxnumHighY - pxnumLowY) / pxnumLowHighRangeX + pxnumLowY;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002FB8 File Offset: 0x000011B8
		internal static void ZeroRounding(double pxnumAbsoluteSlope, ref double pxnumValue)
		{
			if (pxnumValue == 0.0)
			{
				return;
			}
			double num = Math.Abs(pxnumValue);
			if (num < 5E-324 && num < pxnumAbsoluteSlope)
			{
				num = Math.Floor(Math.Log(pxnumAbsoluteSlope / num));
				if (num > 2.0)
				{
					pxnumValue = 0.0;
				}
			}
		}

		// Token: 0x04000071 RID: 113
		private const uint c_nZeroRoundingThreshold = 2U;
	}
}
