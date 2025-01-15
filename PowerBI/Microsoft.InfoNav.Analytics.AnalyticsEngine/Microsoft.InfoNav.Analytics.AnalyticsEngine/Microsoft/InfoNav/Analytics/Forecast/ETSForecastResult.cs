using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Analytics.ExponentialSmoothing;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000028 RID: 40
	internal sealed class ETSForecastResult : ForecastResult
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004B63 File Offset: 0x00002D63
		internal ETSForecastResult()
		{
			this.SeasonStats = null;
			this.ForecastStats = null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B79 File Offset: 0x00002D79
		internal ETSForecastResult(SeasonStats seasonStats, ForecastStats forecastStats, IReadOnlyList<ForecastPoint> points, double score, bool hasInfinity = false, bool hasNan = false)
			: base(points, score, hasInfinity, hasNan)
		{
			this.SeasonStats = seasonStats;
			this.ForecastStats = forecastStats;
		}

		// Token: 0x040000AA RID: 170
		internal static readonly ETSForecastResult Empty = new ETSForecastResult();

		// Token: 0x040000AB RID: 171
		internal readonly SeasonStats SeasonStats;

		// Token: 0x040000AC RID: 172
		internal readonly ForecastStats ForecastStats;
	}
}
