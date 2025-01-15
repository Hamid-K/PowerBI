using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B5 RID: 693
	internal sealed class CreateDataSetCacheEntry : CancelableLibraryStep
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x00064F4C File Offset: 0x0006314C
		internal CreateDataSetCacheEntry(DataSetCatalogItem item, JobType jobType)
			: base(UrlFriendlyUIDGenerator.Create(), item.ItemContext.OriginalItemPath, JobActionEnum.RefreshCache, jobType, item.Service.UserContext)
		{
			this.m_item = item;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00064F78 File Offset: 0x00063178
		protected override void Execute()
		{
			ProcessingContext.JobContext.ExecutionInfo.EventType = ReportEventType.Execute;
			ExecuteStandaloneDataSet executeStandaloneDataSet = new ExecuteStandaloneDataSet(this.m_item);
			executeStandaloneDataSet.CreateSnapshot();
			executeStandaloneDataSet.AddSnapshotToCache();
		}

		// Token: 0x04000916 RID: 2326
		private readonly DataSetCatalogItem m_item;
	}
}
