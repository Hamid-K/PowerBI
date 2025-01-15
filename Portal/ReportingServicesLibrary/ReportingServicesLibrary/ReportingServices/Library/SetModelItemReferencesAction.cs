using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200015D RID: 349
	internal sealed class SetModelItemReferencesAction : RSSoapAction<SetModelItemReferencesActionParameters>
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x00030730 File Offset: 0x0002E930
		internal SetModelItemReferencesAction(RSService service)
			: base("SetModelItemReferencesAction", service)
		{
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00030740 File Offset: 0x0002E940
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(base.ActionParameters.ItemPath, true, "ItemPath");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.Model });
			ModelCatalogItem modelCatalogItem = catalogItem as ModelCatalogItem;
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.UpdateDatasource);
			DataSourceInfoCollection dataSources = modelCatalogItem.DataSources;
			DataSourceInfo dataSourceInfo = new DataSourceInfo(base.ActionParameters.ItemReferences[0].Name, base.ActionParameters.ItemReferences[0].Reference, Guid.Empty);
			this.ResolveNewDataSource(dataSourceInfo);
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			dataSourceInfoCollection.Add(dataSourceInfo);
			DataSourceInfoCollection dataSourceInfoCollection2 = dataSources.CombineOnSetDataSources(dataSourceInfoCollection);
			modelCatalogItem.DataSources = dataSourceInfoCollection2;
			modelCatalogItem.SaveDataSources();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000307F8 File Offset: 0x0002E9F8
		private void ResolveNewDataSource(DataSourceInfo dataSource)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, dataSource.DataSourceReference, DataSourceInfo.GetDataSourceReferenceXmlTag());
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			if (catalogItem.ThisItemType != ItemType.DataSource)
			{
				throw new WrongItemTypeException(catalogItemContext.ItemPath.Value);
			}
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
			DataSourceInfoCollection dataSources = base.Service.Storage.GetDataSources(catalogItem.ItemID);
			dataSource.CopyFrom(dataSources.GetTheOnlyDataSource(), catalogItemContext.ItemPath.Value, catalogItem.ItemID, true);
		}
	}
}
