using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008FD RID: 2301
	[Flags]
	public enum AggregateUpdateFlags
	{
		// Token: 0x04003E59 RID: 15961
		None = 0,
		// Token: 0x04003E5A RID: 15962
		ScopedAggregates = 1,
		// Token: 0x04003E5B RID: 15963
		RowAggregates = 2,
		// Token: 0x04003E5C RID: 15964
		Both = 3
	}
}
