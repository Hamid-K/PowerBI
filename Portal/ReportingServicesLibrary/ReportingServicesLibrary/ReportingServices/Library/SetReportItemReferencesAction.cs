using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C8 RID: 456
	internal sealed class SetReportItemReferencesAction : RSSoapAction<SetReportItemReferencesActionParameters>
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x00038D20 File Offset: 0x00036F20
		internal SetReportItemReferencesAction(RSService service)
			: base("SetReportItemReferencesAction", service)
		{
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00038D30 File Offset: 0x00036F30
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(base.ActionParameters.ItemPath, true, "ItemPath");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.RdlxReport
			});
			FullReportCatalogItem fullReportCatalogItem = catalogItem as FullReportCatalogItem;
			DataSetInfoCollection sharedDataSets = fullReportCatalogItem.SharedDataSets;
			DataSourceInfoCollection dataSources = fullReportCatalogItem.DataSources;
			DataSetInfoCollection dataSetInfoCollection = new DataSetInfoCollection();
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			foreach (ItemReference itemReference in base.ActionParameters.ItemReferences)
			{
				CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, itemReference.Reference, "ItemReference");
				CatalogItem catalogItem2 = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext2, true);
				catalogItem2.ThrowIfNoAccess(CommonOperation.ReadProperties);
				if (catalogItem2.ThisItemType == ItemType.DataSet)
				{
					fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
					dataSetInfoCollection.Add(new DataSetInfo(itemReference.Name, catalogItemContext2.ItemPath.Value));
				}
				else
				{
					if (catalogItem2.ThisItemType != ItemType.DataSource && catalogItem2.ThisItemType != ItemType.Model)
					{
						throw new WrongItemTypeException(catalogItemContext2.ItemPath.Value);
					}
					fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateDatasource);
					DataSourceInfoCollection dataSources2 = base.Service.Storage.GetDataSources(catalogItem2.ItemID);
					DataSourceInfo dataSourceInfo = new DataSourceInfo(itemReference.Name, itemReference.Name);
					dataSourceInfo.CopyFrom(dataSources2.GetTheOnlyDataSource(), catalogItemContext2.ItemPath.Value, catalogItem2.ItemID, false);
					dataSourceInfoCollection.Add(dataSourceInfo);
				}
			}
			RSTrace.CatalogTrace.Assert(fullReportCatalogItem.ExecuteOption != 0);
			SetItemDataSourcesAction.VerifyDataSources(dataSourceInfoCollection, fullReportCatalogItem);
			sharedDataSets.CombineOnSetDataSets(dataSetInfoCollection);
			dataSourceInfoCollection = dataSources.CombineOnSetDataSources(dataSourceInfoCollection);
			if (fullReportCatalogItem.ItemContext.ItemPath.IsEditSession)
			{
				fullReportCatalogItem.FlushDataCache();
			}
			fullReportCatalogItem.SharedDataSets = sharedDataSets;
			fullReportCatalogItem.DataSources = dataSourceInfoCollection;
			fullReportCatalogItem.SaveDataSets();
			fullReportCatalogItem.SaveDataSources();
		}
	}
}
