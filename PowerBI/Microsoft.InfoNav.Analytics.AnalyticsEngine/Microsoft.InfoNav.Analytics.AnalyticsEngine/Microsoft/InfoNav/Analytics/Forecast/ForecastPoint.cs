using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000030 RID: 48
	internal sealed class ForecastPoint
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000054AC File Offset: 0x000036AC
		internal ForecastPoint(double forecastValue, double lowerBoundValue, double upperboundValue)
		{
			if (!double.IsNaN(forecastValue) && !double.IsNaN(lowerBoundValue))
			{
				double.IsNaN(upperboundValue);
			}
			this.ForecastValue = ForecastPoint.ClipDouble(forecastValue);
			this.LowerBoundValue = ForecastPoint.ClipDouble(lowerBoundValue);
			this.UpperboundValue = ForecastPoint.ClipDouble(upperboundValue);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000054FC File Offset: 0x000036FC
		private static double ClipDouble(double value)
		{
			if (double.IsNaN(value))
			{
				value = 0.0;
			}
			else if (double.IsInfinity(value))
			{
				value = ((value > 0.0) ? double.MaxValue : double.MinValue);
			}
			return value;
		}

		// Token: 0x040000F1 RID: 241
		internal readonly double ForecastValue;

		// Token: 0x040000F2 RID: 242
		internal readonly double LowerBoundValue;

		// Token: 0x040000F3 RID: 243
		internal readonly double UpperboundValue;
	}
}
