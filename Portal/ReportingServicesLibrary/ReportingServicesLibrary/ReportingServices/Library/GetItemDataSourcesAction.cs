using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E8 RID: 232
	internal sealed class GetItemDataSourcesAction : RSSoapAction<GetItemDataSourcesActionParameters>
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x000262C1 File Offset: 0x000244C1
		public GetItemDataSourcesAction(RSService service)
			: base("GetItemDataSourcesAction", service)
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000262D0 File Offset: 0x000244D0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.Model,
				ItemType.DataSet,
				ItemType.RdlxReport,
				ItemType.PowerBIReport
			});
			if (!base.ActionParameters.InternalUsePermissionForExecution)
			{
				catalogItem.ThrowIfNoAccess(CommonOperation.ReadDatasource);
			}
			DataSourceInfoCollection dataSourceInfoCollection = null;
			ItemType thisItemType = catalogItem.ThisItemType;
			if (thisItemType != ItemType.Report)
			{
				switch (thisItemType)
				{
				case ItemType.Model:
					dataSourceInfoCollection = ((ModelCatalogItem)catalogItem).DataSources;
					break;
				case ItemType.DataSet:
					dataSourceInfoCollection = ((DataSetCatalogItem)catalogItem).DataSources;
					break;
				case ItemType.RdlxReport:
					dataSourceInfoCollection = ((RdlxReportCatalogItem)catalogItem).DataSources;
					break;
				}
			}
			else
			{
				dataSourceInfoCollection = ((ProfessionalReportCatalogItem)catalogItem).DataSources;
			}
			GetItemDataSourcesAction.FixDataSourceReferences(dataSourceInfoCollection, base.Service, catalogItemContext, catalogItem);
			if (base.ActionParameters.FetchEncryptedCredentials)
			{
				base.ActionParameters.DataSources = DataSource.DataSourceInfoCollectionToThisArray(dataSourceInfoCollection, true, true);
				return;
			}
			base.ActionParameters.DataSources = DataSource.DataSourceInfoCollectionToThisArray(dataSourceInfoCollection, false, false);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000263DC File Offset: 0x000245DC
		internal static void FixDataSourceReferences(DataSourceInfoCollection dataSources, RSService service, CatalogItemContext itemContext, CatalogItem item)
		{
			if (dataSources == null)
			{
				return;
			}
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.SecurityDescriptor != null)
				{
					ItemType itemType;
					if (item.ThisItemType == ItemType.Model)
					{
						itemType = ItemType.DataSource;
					}
					else if (dataSourceInfo.IsModel)
					{
						itemType = ItemType.Model;
					}
					else
					{
						itemType = ItemType.DataSource;
					}
					if (!service.SecMgr.CheckAccess(itemType, dataSourceInfo.SecurityDescriptor, CommonOperation.ReadProperties, itemContext.ItemPath))
					{
						dataSourceInfo.DataSourceReference = null;
					}
				}
				if (dataSourceInfo.DataSourceReference != null)
				{
					dataSourceInfo.DataSourceReference = service.CatalogToExternal(dataSourceInfo.DataSourceReference).Value;
				}
			}
		}
	}
}
