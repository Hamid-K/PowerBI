using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C1 RID: 449
	internal sealed class DeleteHistorySnapshotAction : RSSoapAction<DeleteHistorySnapshotActionParameters>
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x000386B4 File Offset: 0x000368B4
		public DeleteHistorySnapshotAction(RSService service)
			: base("DeleteHistorySnapshotAction", service)
		{
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000386C2 File Offset: 0x000368C2
		protected override void AddActionToBatch()
		{
			throw new NotImplementedException("SOAP should not call this action!");
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000386CE File Offset: 0x000368CE
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.HistoryId = parameters.Param;
			this.PerformActionNow();
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000386F8 File Offset: 0x000368F8
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
			if (!base.Service.Storage.DeleteHistoryRecord(baseReportCatalogItem.ItemID, base.ActionParameters.HistoryId))
			{
				throw new ReportHistoryNotFoundException(base.ActionParameters.ReportPath, base.ActionParameters.HistoryId);
			}
		}
	}
}
