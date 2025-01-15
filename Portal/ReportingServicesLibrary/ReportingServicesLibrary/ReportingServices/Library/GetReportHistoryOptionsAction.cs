using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B4 RID: 436
	internal sealed class GetReportHistoryOptionsAction : RSSoapAction<ReportHistoryOptionsActionParameters>
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x00037E49 File Offset: 0x00036049
		public GetReportHistoryOptionsAction(RSService service)
			: base("GetReportHistoryOptionsAction", service)
		{
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00037E58 File Offset: 0x00036058
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadPolicy);
			base.ActionParameters.ManualCreationEnabled = ExecutionOptions.IsManualHistoryEnabled(baseReportCatalogItem.ExecutionOptions);
			base.ActionParameters.KeepExecutionSnapshots = ExecutionOptions.ExecutionSnapshotsKept(baseReportCatalogItem.ExecutionOptions);
			ScheduleDefinitionOrReference scheduleDefinitionOrReference;
			base.Service.SchedCoordinator.GetSnapshotSchedule(baseReportCatalogItem.ItemID, out scheduleDefinitionOrReference);
			base.ActionParameters.Schedule = scheduleDefinitionOrReference;
		}
	}
}
