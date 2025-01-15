using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008FE RID: 2302
	[Flags]
	public enum DataActions
	{
		// Token: 0x04003E5E RID: 15966
		None = 0,
		// Token: 0x04003E5F RID: 15967
		RecursiveAggregates = 1,
		// Token: 0x04003E60 RID: 15968
		PostSortAggregates = 2,
		// Token: 0x04003E61 RID: 15969
		UserSort = 4,
		// Token: 0x04003E62 RID: 15970
		AggregatesOfAggregates = 8,
		// Token: 0x04003E63 RID: 15971
		PostSortAggregatesOfAggregates = 16
	}
}
