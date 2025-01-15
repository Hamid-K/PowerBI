using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000088 RID: 136
	internal enum ReportScheduleActions
	{
		// Token: 0x04000307 RID: 775
		None,
		// Token: 0x04000308 RID: 776
		UpdateReportExecutionSnapshot,
		// Token: 0x04000309 RID: 777
		CreateReportHistorySnapshot,
		// Token: 0x0400030A RID: 778
		InvalidateCache,
		// Token: 0x0400030B RID: 779
		TimedSubscription,
		// Token: 0x0400030C RID: 780
		RefreshCache,
		// Token: 0x0400030D RID: 781
		SharedDatasetCacheUpdate,
		// Token: 0x0400030E RID: 782
		CommentAddedAlert,
		// Token: 0x0400030F RID: 783
		DataModelRefresh
	}
}
