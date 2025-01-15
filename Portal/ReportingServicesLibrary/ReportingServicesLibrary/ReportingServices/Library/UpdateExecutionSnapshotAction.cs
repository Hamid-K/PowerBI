using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AD RID: 429
	internal sealed class UpdateExecutionSnapshotAction : RSSoapAction<UpdateExecutionSnapshotActionParameters>
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003767E File Offset: 0x0003587E
		public UpdateExecutionSnapshotAction(RSService service)
			: base("UpdateExecutionSnapshotAction", service)
		{
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003768C File Offset: 0x0003588C
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.UpdateSnapshot, base.ActionParameters.ReportPath, "report", null, null, null, null, false, null, null);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000376D3 File Offset: 0x000358D3
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.JobType = JobType.UserJobType;
			this.PerformActionNow();
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000376FC File Offset: 0x000358FC
		internal override void PerformActionNow()
		{
			using (UpdateExecutionSnapshotCancelableStep updateExecutionSnapshotCancelableStep = new UpdateExecutionSnapshotCancelableStep(new UpdateExecutionSnapshotCancelableStep.UpdateExecutionSnapshot(this.InternalUpdateSnapshot), new ExternalItemPath(base.ActionParameters.ReportPath), base.ActionParameters.JobType, base.Service.UserContext))
			{
				updateExecutionSnapshotCancelableStep.ExecuteWrapper();
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00037764 File Offset: 0x00035964
		private void InternalUpdateSnapshot()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "Report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.Execute);
			if (!ExecutionOptions.IsSnapshotExecution(baseReportCatalogItem.ExecutionOptions))
			{
				throw new ReportSnapshotNotEnabledException();
			}
			RSLocalCacheManager.Current.RemoveCachedReport(baseReportCatalogItem.ItemContext);
			bool flag = ExecutionOptions.ExecutionSnapshotsKept(baseReportCatalogItem.ExecutionOptions);
			CreateSnapshotExecutor createSnapshotExecutor = new CreateSnapshotExecutor(baseReportCatalogItem, flag);
			createSnapshotExecutor.UsePermanentSnapshot = true;
			createSnapshotExecutor.CreateSnapshot();
			createSnapshotExecutor.UpdateExecutionSnapshot();
			if (flag)
			{
				createSnapshotExecutor.AddSnapshotToHistory(true);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}
	}
}
