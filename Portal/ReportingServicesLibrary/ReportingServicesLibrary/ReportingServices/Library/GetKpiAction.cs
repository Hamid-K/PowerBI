using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006D RID: 109
	internal sealed class GetKpiAction : RSSoapAction<GetKpiActionParameters>
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00012A08 File Offset: 0x00010C08
		internal GetKpiAction(RSService service)
			: base("GetKpiAction", service)
		{
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00012A18 File Offset: 0x00010C18
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Kpi");
			KpiCatalogItem kpiCatalogItem = (KpiCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Kpi);
			kpiCatalogItem.LoadProperties();
			base.ActionParameters.Item = kpiCatalogItem;
		}
	}
}
