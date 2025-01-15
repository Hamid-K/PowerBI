using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BD RID: 445
	internal sealed class ListHistorySnapshotsAction : RSSoapAction<ListHistorySnapshotsActionParameters>
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x000383E4 File Offset: 0x000365E4
		public ListHistorySnapshotsAction(RSService service)
			: base("ListHistorySnapshotsAction", service)
		{
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000383F4 File Offset: 0x000365F4
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
			if (StaticConfig.Current.GetOrDefault(ConfigSwitches.EnableListHistorySnapshotsSize.ToString(), true))
			{
				base.ActionParameters.ReportHistory = base.Service.Storage.ListHistorySnapshots(baseReportCatalogItem.ItemID);
				return;
			}
			base.ActionParameters.ReportHistory = base.Service.Storage.ListHistorySnapshotsNoSize(baseReportCatalogItem.ItemID);
		}
	}
}
