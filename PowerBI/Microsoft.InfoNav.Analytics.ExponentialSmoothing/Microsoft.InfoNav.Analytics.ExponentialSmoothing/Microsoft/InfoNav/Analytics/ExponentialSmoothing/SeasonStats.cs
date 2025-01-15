using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000011 RID: 17
	public sealed class SeasonStats
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000066B9 File Offset: 0x000048B9
		public SeasonStats(int finalPeriod, IReadOnlyList<int> periods, IReadOnlyList<double> scores)
		{
			this.FinalPeriod = finalPeriod;
			this.Periods = periods;
			this.Scores = scores;
		}

		// Token: 0x04000082 RID: 130
		public readonly int FinalPeriod;

		// Token: 0x04000083 RID: 131
		public readonly IReadOnlyList<int> Periods;

		// Token: 0x04000084 RID: 132
		public readonly IReadOnlyList<double> Scores;
	}
}
