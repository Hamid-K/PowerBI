using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BF RID: 447
	internal sealed class DeleteSnapshotAction : RSSoapAction<DeleteSnapshotActionParameters>
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00038518 File Offset: 0x00036718
		public DeleteSnapshotAction(RSService service)
			: base("DeleteSnapshotAction", service)
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00038528 File Offset: 0x00036728
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteSnapshot, base.ActionParameters.ReportPath, "report", null, null, base.ActionParameters.SnapshotID, "historyID", false, null, null);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003857D File Offset: 0x0003677D
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.SnapshotID = parameters.Param;
			this.PerformActionNow();
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000385A8 File Offset: 0x000367A8
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
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.DeleteHistory);
			DateTime dateTime = Globals.ParseSnapshotDateParameter(base.ActionParameters.SnapshotID, false);
			if (!base.Service.Storage.DeleteHistoryRecord(baseReportCatalogItem.ItemID, dateTime))
			{
				throw new ReportHistoryNotFoundException(base.ActionParameters.ReportPath, base.ActionParameters.SnapshotID);
			}
		}
	}
}
