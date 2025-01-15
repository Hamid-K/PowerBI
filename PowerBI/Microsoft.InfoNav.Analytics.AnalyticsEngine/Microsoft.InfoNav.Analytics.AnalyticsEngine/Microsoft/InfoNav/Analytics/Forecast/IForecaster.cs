using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000034 RID: 52
	internal interface IForecaster
	{
		// Token: 0x060000E5 RID: 229
		ForecastResult Forecast(ForecastContext context, DataPreprocessResult preprocessedResult);
	}
}
