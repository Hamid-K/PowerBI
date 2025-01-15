using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x0200002D RID: 45
	internal static class ForecasterFactory
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000540D File Offset: 0x0000360D
		internal static IForecaster CreateForecaster(ForecastStatistics forecastStatistics)
		{
			return new EnsembleForecaster(forecastStatistics);
		}
	}
}
