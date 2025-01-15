using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B9 RID: 441
	internal sealed class GetSnapshotLimitAction : RSSoapAction<GetSnapshotLimitActionParameters>
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x000381F7 File Offset: 0x000363F7
		public GetSnapshotLimitAction(RSService service)
			: base("GetSnapshotLimitAction", service)
		{
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00038208 File Offset: 0x00036408
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
			base.ActionParameters.UseSystem = false;
			base.ActionParameters.ScopedLimit = baseReportCatalogItem.HistorySnapshotLimit;
			base.ActionParameters.SystemLimit = base.Service.SystemSnapshotLimit;
			if (base.ActionParameters.ScopedLimit == -2)
			{
				base.ActionParameters.UseSystem = true;
				base.ActionParameters.ScopedLimit = base.ActionParameters.SystemLimit;
			}
		}
	}
}
