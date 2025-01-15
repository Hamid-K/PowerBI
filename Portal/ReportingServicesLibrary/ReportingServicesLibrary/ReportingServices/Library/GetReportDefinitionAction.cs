using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000199 RID: 409
	internal sealed class GetReportDefinitionAction : RSSoapAction<GetReportDefinitionActionParameters>
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00036723 File Offset: 0x00034923
		public GetReportDefinitionAction(RSService service)
			: base("GetReportDefinitionAction", service)
		{
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00036734 File Offset: 0x00034934
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Report");
			FullReportCatalogItem fullReportCatalogItem = (FullReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, base.ActionParameters.ItemType);
			fullReportCatalogItem.LoadDefinition();
			base.ActionParameters.ReportDefinition = fullReportCatalogItem.Content;
		}
	}
}
