using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E6 RID: 230
	internal sealed class GetDataSourceContentsAction : RSSoapAction<GetDataSourceContentsActionParameters>
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x000261D6 File Offset: 0x000243D6
		public GetDataSourceContentsAction(RSService service)
			: base("GetDataSourceContentsAction", service)
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000261E4 File Offset: 0x000243E4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.DataSourcePath, "DataSource");
			DataSourceCatalogItem dataSourceCatalogItem = (DataSourceCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.DataSource, true);
			if (!base.ActionParameters.InternalUsePermissionForExecution)
			{
				dataSourceCatalogItem.ThrowIfNoAccess(DatasourceOperation.ReadContent);
			}
			base.ActionParameters.DataSourceDefinition = DataSourceDefinition.DataSourceInfoToThis(dataSourceCatalogItem.DataSourceInfo, false, false);
		}
	}
}
