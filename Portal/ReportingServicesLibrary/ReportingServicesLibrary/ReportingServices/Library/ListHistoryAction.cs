using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BB RID: 443
	internal sealed class ListHistoryAction : RSSoapAction<ListHistoryActionParameters>
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0003830F File Offset: 0x0003650F
		public ListHistoryAction(RSService service)
			: base("ListHistoryAction", service)
		{
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00038320 File Offset: 0x00036520
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
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ListHistory);
			base.ActionParameters.ReportHistory = base.Service.Storage.ListHistory(baseReportCatalogItem.ItemID);
		}
	}
}
