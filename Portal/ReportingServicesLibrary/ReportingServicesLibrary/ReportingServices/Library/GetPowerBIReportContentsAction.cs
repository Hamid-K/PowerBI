using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000161 RID: 353
	internal sealed class GetPowerBIReportContentsAction : RSSoapAction<GetPowerBIReportContentsActionParameters>
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x00030AAB File Offset: 0x0002ECAB
		internal GetPowerBIReportContentsAction(RSService service)
			: base("RSGetPowerBIReportContentsAction", service)
		{
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00030ABC File Offset: 0x0002ECBC
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "PowerBIReport");
			PowerBIReportCatalogItem powerBIReportCatalogItem = (PowerBIReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.PowerBIReport);
			powerBIReportCatalogItem.LoadContent();
			base.ActionParameters.Content = powerBIReportCatalogItem.Content;
		}
	}
}
