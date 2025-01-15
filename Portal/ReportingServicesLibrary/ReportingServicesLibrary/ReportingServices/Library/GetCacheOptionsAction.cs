using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A0 RID: 416
	internal sealed class GetCacheOptionsAction : RSSoapAction<GetCacheOptionsActionParameters>
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00036BA1 File Offset: 0x00034DA1
		public GetCacheOptionsAction(RSService service)
			: base("GetCacheOptionsAction", service)
		{
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00036BB0 File Offset: 0x00034DB0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.DataSet
			});
			(catalogItem as BaseExecutableCatalogItem).ThrowIfNoAccess(ReportOperation.ReadPolicy);
			bool flag;
			ExpirationDefinition expirationDefinition;
			base.Service.ExecCacheDb.GetCacheOptions(catalogItemContext.CatalogItemPath, out flag, out expirationDefinition);
			base.ActionParameters.CacheReport = flag;
			base.ActionParameters.Expiration = expirationDefinition;
		}
	}
}
