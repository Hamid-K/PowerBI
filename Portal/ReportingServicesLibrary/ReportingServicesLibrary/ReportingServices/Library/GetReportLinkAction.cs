using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019B RID: 411
	internal sealed class GetReportLinkAction : RSSoapAction<GetReportLinkActionParameters>
	{
		// Token: 0x06000F07 RID: 3847 RVA: 0x000367EB File Offset: 0x000349EB
		public GetReportLinkAction(RSService service)
			: base("GetReportLinkAction", service)
		{
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000367FC File Offset: 0x000349FC
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "Report");
			LinkedReportCatalogItem linkedReportCatalogItem = (LinkedReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.LinkedReport);
			linkedReportCatalogItem.LoadLink();
			base.ActionParameters.LinkPath = ItemPathBase.SafeValue(linkedReportCatalogItem.LinkPath);
		}
	}
}
