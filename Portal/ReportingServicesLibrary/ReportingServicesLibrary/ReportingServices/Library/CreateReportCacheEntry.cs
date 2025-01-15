using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B4 RID: 692
	internal sealed class CreateReportCacheEntry : CancelableLibraryStep
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x00064EEF File Offset: 0x000630EF
		internal CreateReportCacheEntry(BaseReportCatalogItem item, JobType jobType)
			: base(UrlFriendlyUIDGenerator.Create(), item.ItemContext.OriginalItemPath, JobActionEnum.RefreshCache, jobType, item.Service.UserContext)
		{
			this.m_item = item;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00064F1B File Offset: 0x0006311B
		protected override void Execute()
		{
			ProcessingContext.JobContext.ExecutionInfo.EventType = ReportEventType.Execute;
			CreateSnapshotExecutor createSnapshotExecutor = new CreateSnapshotExecutor(this.m_item, false);
			createSnapshotExecutor.UsePermanentSnapshot = false;
			createSnapshotExecutor.CreateSnapshot();
			createSnapshotExecutor.AddSnapshotToCache();
		}

		// Token: 0x04000915 RID: 2325
		private readonly BaseReportCatalogItem m_item;
	}
}
