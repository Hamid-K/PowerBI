using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CF RID: 463
	internal sealed class GetResourceContentsAction : RSSoapAction<GetResourceContentsActionParameters>
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0003945C File Offset: 0x0003765C
		internal GetResourceContentsAction(RSService service)
			: base("GetResourceContentsAction", service)
		{
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003946C File Offset: 0x0003766C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Resource");
			ResourceCatalogItem resourceCatalogItem = (ResourceCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Resource);
			resourceCatalogItem.LoadContent();
			base.ActionParameters.MimeType = resourceCatalogItem.MimeType;
			if (!base.Service.IsTrustedFileType(resourceCatalogItem.ItemContext.ItemName) || !base.Service.IsTrustedContentType(resourceCatalogItem.MimeType))
			{
				base.ActionParameters.MimeType = "application/octet-stream";
			}
			base.ActionParameters.Content = resourceCatalogItem.Content;
		}
	}
}
