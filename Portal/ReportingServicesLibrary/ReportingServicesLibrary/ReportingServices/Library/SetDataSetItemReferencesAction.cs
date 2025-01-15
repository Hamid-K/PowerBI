using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FE RID: 254
	internal sealed class SetDataSetItemReferencesAction : RSSoapAction<SetDataSetItemReferencesActionParameters>
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00027752 File Offset: 0x00025952
		internal SetDataSetItemReferencesAction(RSService service)
			: base("SetDataSetItemReferencesAction", service)
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00027760 File Offset: 0x00025960
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(base.ActionParameters.ItemPath, true, "ItemPath");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			if (catalogItem.ThisItemType != ItemType.DataSet)
			{
				throw new WrongItemTypeException(catalogItemContext.OriginalItemPath.Value);
			}
			DataSetCatalogItem dataSetCatalogItem = catalogItem as DataSetCatalogItem;
			dataSetCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateDatasource);
			DataSourceInfoCollection dataSources = dataSetCatalogItem.DataSources;
			DataSourceInfo dataSourceInfo = new DataSourceInfo(base.ActionParameters.ItemReferences[0].Name, base.ActionParameters.ItemReferences[0].Reference, Guid.Empty);
			this.ResolveNewDataSource(dataSourceInfo);
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			dataSourceInfoCollection.Add(dataSourceInfo);
			DataSourceInfoCollection dataSourceInfoCollection2 = dataSources.CombineOnSetDataSources(dataSourceInfoCollection);
			dataSetCatalogItem.DataSources = dataSourceInfoCollection2;
			dataSetCatalogItem.SaveDataSources();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00027820 File Offset: 0x00025A20
		private void ResolveNewDataSource(DataSourceInfo dataSource)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, dataSource.DataSourceReference, DataSourceInfo.GetDataSourceReferenceXmlTag());
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			if (catalogItem.ThisItemType != ItemType.DataSource && catalogItem.ThisItemType != ItemType.Model)
			{
				throw new WrongItemTypeException(catalogItemContext.ItemPath.Value);
			}
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
			DataSourceInfoCollection dataSources = base.Service.Storage.GetDataSources(catalogItem.ItemID);
			dataSource.CopyFrom(dataSources.GetTheOnlyDataSource(), catalogItemContext.ItemPath.Value, catalogItem.ItemID, false);
		}
	}
}
