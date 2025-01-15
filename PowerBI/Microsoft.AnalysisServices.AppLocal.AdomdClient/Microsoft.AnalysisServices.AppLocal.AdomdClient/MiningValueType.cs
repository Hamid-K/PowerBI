using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DA RID: 218
	public enum MiningValueType
	{
		// Token: 0x040007CD RID: 1997
		PreRenderedString = -1,
		// Token: 0x040007CE RID: 1998
		Missing = 1,
		// Token: 0x040007CF RID: 1999
		Existing,
		// Token: 0x040007D0 RID: 2000
		Continuous,
		// Token: 0x040007D1 RID: 2001
		Discrete,
		// Token: 0x040007D2 RID: 2002
		Discretized,
		// Token: 0x040007D3 RID: 2003
		Boolean,
		// Token: 0x040007D4 RID: 2004
		Coefficient,
		// Token: 0x040007D5 RID: 2005
		ScoreGain,
		// Token: 0x040007D6 RID: 2006
		RegressorStatistics,
		// Token: 0x040007D7 RID: 2007
		NodeUniqueName,
		// Token: 0x040007D8 RID: 2008
		Intercept,
		// Token: 0x040007D9 RID: 2009
		Periodicity,
		// Token: 0x040007DA RID: 2010
		AutoRegressiveOrder,
		// Token: 0x040007DB RID: 2011
		MovingAverageOrder,
		// Token: 0x040007DC RID: 2012
		DifferenceOrder,
		// Token: 0x040007DD RID: 2013
		Other
	}
}
