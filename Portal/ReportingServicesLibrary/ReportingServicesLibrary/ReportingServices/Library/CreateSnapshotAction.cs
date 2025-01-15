using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B1 RID: 433
	internal sealed class CreateSnapshotAction : RSSoapAction<CreateSnapshotActionParameters>
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x000379B1 File Offset: 0x00035BB1
		public CreateSnapshotAction(RSService service)
			: base("CreateSnapshotAction", service)
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000379C0 File Offset: 0x00035BC0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateSnapshot, base.ActionParameters.ReportPath, "report", null, null, null, null, false, null, null);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00037A07 File Offset: 0x00035C07
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.JobType = JobType.UserJobType;
			this.PerformActionNow();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00037A30 File Offset: 0x00035C30
		internal override void PerformActionNow()
		{
			using (CreateHistorySnapshotCancelableStep createHistorySnapshotCancelableStep = new CreateHistorySnapshotCancelableStep(new CreateHistorySnapshotCancelableStep.CreateSnapshot(this.InternalCreateHistorySnapshot), new ExternalItemPath(base.ActionParameters.ReportPath), base.ActionParameters.JobType, base.Service.UserContext))
			{
				createHistorySnapshotCancelableStep.ExecuteWrapper();
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00037A98 File Offset: 0x00035C98
		private void InternalCreateHistorySnapshot()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "Report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.CreateSnapshot);
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.Execute);
			if (base.ActionParameters.JobType.Type == JobTypeEnum.User && !ExecutionOptions.IsManualHistoryEnabled(baseReportCatalogItem.ExecutionOptions))
			{
				throw new ReportSnapshotNotEnabledException();
			}
			CreateSnapshotExecutor createSnapshotExecutor = new CreateSnapshotExecutor(baseReportCatalogItem, true);
			createSnapshotExecutor.UsePermanentSnapshot = true;
			createSnapshotExecutor.CreateSnapshot();
			createSnapshotExecutor.AddSnapshotToHistory(false);
			base.ActionParameters.Warnings = Warning.ProcessingMessagesToWarningArray(createSnapshotExecutor.Messages);
			base.ActionParameters.HistoryID = Globals.ToSnapshotDateFormat(createSnapshotExecutor.ExecutionDateUtcCatalogPrecision);
		}
	}
}
