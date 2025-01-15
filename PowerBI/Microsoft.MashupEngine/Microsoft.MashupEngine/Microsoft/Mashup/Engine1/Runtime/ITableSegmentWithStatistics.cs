using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001560 RID: 5472
	internal interface ITableSegmentWithStatistics
	{
		// Token: 0x0600880C RID: 34828
		bool TryGetStatistics(int column, out ListStatistics statistics);
	}
}
