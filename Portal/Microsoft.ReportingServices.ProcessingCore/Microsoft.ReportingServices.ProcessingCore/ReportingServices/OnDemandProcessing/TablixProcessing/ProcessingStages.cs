using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008FB RID: 2299
	public enum ProcessingStages
	{
		// Token: 0x04003E4E RID: 15950
		Grouping = 1,
		// Token: 0x04003E4F RID: 15951
		SortAndFilter,
		// Token: 0x04003E50 RID: 15952
		PreparePeerGroupRunningValues,
		// Token: 0x04003E51 RID: 15953
		RunningValues,
		// Token: 0x04003E52 RID: 15954
		UserSortFilter,
		// Token: 0x04003E53 RID: 15955
		UpdateAggregates,
		// Token: 0x04003E54 RID: 15956
		CreateGroupTree
	}
}
