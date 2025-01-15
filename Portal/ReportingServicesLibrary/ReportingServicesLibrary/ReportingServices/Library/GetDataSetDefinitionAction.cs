using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F8 RID: 248
	internal sealed class GetDataSetDefinitionAction : RSSoapAction<GetDataSetDefinitionActionParameters>
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0002748A File Offset: 0x0002568A
		public GetDataSetDefinitionAction(RSService service)
			: base("GetDataSetDefinitionAction", service)
		{
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00027498 File Offset: 0x00025698
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "DataSet");
			DataSetCatalogItem dataSetCatalogItem = (DataSetCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.DataSet);
			dataSetCatalogItem.InternalUsePermissionForExecution = base.ActionParameters.InternalUsePermissionForExecution;
			dataSetCatalogItem.LoadDefinition();
			base.ActionParameters.DataSetDefinition = dataSetCatalogItem.Content;
		}
	}
}
