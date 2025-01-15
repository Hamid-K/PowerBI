using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000031 RID: 49
	internal abstract class ForecastResult
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00005549 File Offset: 0x00003749
		protected ForecastResult()
		{
			this.Points = Util.EmptyReadOnlyCollection<ForecastPoint>();
			this.HasInfinity = false;
			this.HasNan = false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000556A File Offset: 0x0000376A
		protected ForecastResult(IReadOnlyList<ForecastPoint> points, double score, bool hasInfinity = false, bool hasNan = false)
		{
			this.Points = points;
			this.Score = score;
			this.HasInfinity = hasInfinity;
			this.HasNan = hasNan;
		}

		// Token: 0x040000F4 RID: 244
		internal readonly IReadOnlyList<ForecastPoint> Points;

		// Token: 0x040000F5 RID: 245
		internal readonly double Score;

		// Token: 0x040000F6 RID: 246
		internal readonly bool HasInfinity;

		// Token: 0x040000F7 RID: 247
		internal readonly bool HasNan;
	}
}
