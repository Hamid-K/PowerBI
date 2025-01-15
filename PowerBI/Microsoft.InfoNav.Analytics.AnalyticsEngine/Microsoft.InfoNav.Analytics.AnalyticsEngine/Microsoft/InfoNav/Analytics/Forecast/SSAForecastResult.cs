using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000038 RID: 56
	internal sealed class SSAForecastResult : ForecastResult
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00006563 File Offset: 0x00004763
		internal SSAForecastResult()
		{
			this.Features = null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006572 File Offset: 0x00004772
		internal SSAForecastResult(SSAFeatures features, IReadOnlyList<ForecastPoint> points, double score, bool hasInfinity = false, bool hasNan = false)
			: base(points, score, hasInfinity, hasNan)
		{
			this.Features = features;
		}

		// Token: 0x04000121 RID: 289
		internal static readonly SSAForecastResult Empty = new SSAForecastResult();

		// Token: 0x04000122 RID: 290
		internal readonly SSAFeatures Features;
	}
}
