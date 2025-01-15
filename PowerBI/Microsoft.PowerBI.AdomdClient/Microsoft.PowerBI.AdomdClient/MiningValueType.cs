using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DA RID: 218
	public enum MiningValueType
	{
		// Token: 0x040007C0 RID: 1984
		PreRenderedString = -1,
		// Token: 0x040007C1 RID: 1985
		Missing = 1,
		// Token: 0x040007C2 RID: 1986
		Existing,
		// Token: 0x040007C3 RID: 1987
		Continuous,
		// Token: 0x040007C4 RID: 1988
		Discrete,
		// Token: 0x040007C5 RID: 1989
		Discretized,
		// Token: 0x040007C6 RID: 1990
		Boolean,
		// Token: 0x040007C7 RID: 1991
		Coefficient,
		// Token: 0x040007C8 RID: 1992
		ScoreGain,
		// Token: 0x040007C9 RID: 1993
		RegressorStatistics,
		// Token: 0x040007CA RID: 1994
		NodeUniqueName,
		// Token: 0x040007CB RID: 1995
		Intercept,
		// Token: 0x040007CC RID: 1996
		Periodicity,
		// Token: 0x040007CD RID: 1997
		AutoRegressiveOrder,
		// Token: 0x040007CE RID: 1998
		MovingAverageOrder,
		// Token: 0x040007CF RID: 1999
		DifferenceOrder,
		// Token: 0x040007D0 RID: 2000
		Other
	}
}
