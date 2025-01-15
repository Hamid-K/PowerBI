using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E2 RID: 226
	internal sealed class ChangeDataSourceStateAction : RSSoapAction<ChangeDataSourceStateActionParameters>
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00025E5F File Offset: 0x0002405F
		public ChangeDataSourceStateAction(RSService service)
			: base("ChangeDataSourceStateAction", service)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00025E70 File Offset: 0x00024070
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.ChangeStateOfDataSource, base.ActionParameters.DataSourcePath, "name", null, null, null, null, base.ActionParameters.Enable, null, null);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00025EC1 File Offset: 0x000240C1
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.DataSourcePath = parameters.Item;
			base.ActionParameters.Enable = parameters.BoolParam;
			this.PerformActionNow();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00025EEC File Offset: 0x000240EC
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.DataSourcePath, "DataSource");
			DataSourceCatalogItem dataSourceCatalogItem = (DataSourceCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.DataSource, true);
			dataSourceCatalogItem.ThrowIfNoAccess(DatasourceOperation.UpdateContent);
			dataSourceCatalogItem.Enabled = base.ActionParameters.Enable;
			dataSourceCatalogItem.SaveEnabled();
		}
	}
}
