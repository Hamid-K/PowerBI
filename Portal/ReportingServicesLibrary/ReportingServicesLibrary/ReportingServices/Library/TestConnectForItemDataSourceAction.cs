using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F0 RID: 240
	internal sealed class TestConnectForItemDataSourceAction : TestConnectAction<RSTestConnectForItemDataSourceActionParameters>
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00026D07 File Offset: 0x00024F07
		public TestConnectForItemDataSourceAction(RSService service)
			: base("TestConnectForItemDataSourceAction", service)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00026D18 File Offset: 0x00024F18
		protected override void InitDataSourceInfo()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			switch (catalogItem.ThisItemType)
			{
			case ItemType.Report:
			case ItemType.DataSet:
			case ItemType.RdlxReport:
			{
				BaseExecutableCatalogItem baseExecutableCatalogItem = (BaseExecutableCatalogItem)catalogItem;
				baseExecutableCatalogItem.ThrowIfNoAccess(ReportOperation.ReadDatasource);
				base.ActionParameters.DSInfo = this.GetRequestedDataSource(baseExecutableCatalogItem.DataSources);
				return;
			}
			case ItemType.DataSource:
			{
				DataSourceCatalogItem dataSourceCatalogItem = (DataSourceCatalogItem)catalogItem;
				dataSourceCatalogItem.ThrowIfNoAccess(DatasourceOperation.ReadContent);
				base.ActionParameters.DSInfo = dataSourceCatalogItem.DataSourceInfo;
				return;
			}
			case ItemType.Model:
			{
				ModelCatalogItem modelCatalogItem = (ModelCatalogItem)catalogItem;
				modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadDatasource);
				DataSourceInfoCollection dataSources = modelCatalogItem.DataSources;
				base.ActionParameters.DSInfo = dataSources.GetTheOnlyDataSource();
				return;
			}
			}
			throw new WrongItemTypeException(catalogItemContext.OriginalItemPath.Value);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00026E0C File Offset: 0x0002500C
		private DataSourceInfo GetRequestedDataSource(DataSourceInfoCollection datasourceInfoCollection)
		{
			if (base.ActionParameters.DataSourceName == null)
			{
				throw new MissingParameterException("DataSourceName");
			}
			foreach (object obj in datasourceInfoCollection)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.OriginalName == base.ActionParameters.DataSourceName)
				{
					if (dataSourceInfo.IsReference)
					{
						CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, new CatalogItemPath(dataSourceInfo.DataSourceReference), "");
						CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
						if (dataSourceInfo.IsModel)
						{
							((ModelCatalogItem)catalogItem).ThrowIfNoAccess(ModelOperation.ReadDatasource);
						}
						else
						{
							((DataSourceCatalogItem)catalogItem).ThrowIfNoAccess(DatasourceOperation.ReadContent);
						}
					}
					return dataSourceInfo;
				}
			}
			throw new InvalidParameterException("DataSourceName");
		}
	}
}
