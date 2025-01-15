using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AF RID: 431
	internal sealed class FlushCacheAction : RSSoapAction<FlushOptionsActionParameters>
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x00037851 File Offset: 0x00035A51
		public FlushCacheAction(RSService service)
			: base("FlushCacheAction", service)
		{
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00037860 File Offset: 0x00035A60
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.FlushCache, base.ActionParameters.ItemPath, "report", null, null, null, null, false, null, null);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000378A7 File Offset: 0x00035AA7
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000378C0 File Offset: 0x00035AC0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "ItemPath");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.DataSet
			});
			(catalogItem as BaseExecutableCatalogItem).ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			RSLocalCacheManager.Current.RemoveCachedReport(catalogItemContext);
			base.Service.ExecCacheDb.FlushCache(catalogItemContext.CatalogItemPath);
			base.Service.Storage.FlushContentCache(catalogItemContext.CatalogItemPath);
		}
	}
}
