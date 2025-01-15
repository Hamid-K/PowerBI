using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C6 RID: 454
	internal sealed class GetReportItemReferencesAction : RSSoapAction<GetReportItemReferencesActionParameters>
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00038A1D File Offset: 0x00036C1D
		public GetReportItemReferencesAction(RSService service)
			: base("GetReportItemReferencesAction", service)
		{
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00038A2C File Offset: 0x00036C2C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.RdlxReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			List<ItemReferenceData> list = new List<ItemReferenceData>();
			if (base.ActionParameters.ReferenceItemType == null || base.ActionParameters.ReferenceItemType.Value == ItemTypeEnum.DataSet)
			{
				baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
				foreach (DataSetInfo dataSetInfo in baseReportCatalogItem.SharedDataSets)
				{
					if (dataSetInfo.SecurityDescriptor != null)
					{
						base.Service.SecMgr.CheckAccess(ItemType.DataSet, dataSetInfo.SecurityDescriptor, CommonOperation.ReadProperties, catalogItemContext.ItemPath);
					}
					string text = ((dataSetInfo.AbsolutePath != null) ? base.Service.CatalogToExternal(dataSetInfo.AbsolutePath).Value : null);
					list.Add(new ItemReferenceData(dataSetInfo.DataSetName, text, ItemType.DataSet.ToString()));
				}
			}
			if (base.ActionParameters.ReferenceItemType == null || base.ActionParameters.ReferenceItemType.Value == ItemTypeEnum.DataSource)
			{
				baseReportCatalogItem.ThrowIfNoAccess(CommonOperation.ReadDatasource);
				foreach (object obj in baseReportCatalogItem.DataSources)
				{
					DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
					if (dataSourceInfo.IsReference)
					{
						if (dataSourceInfo.SecurityDescriptor != null)
						{
							base.Service.SecMgr.CheckAccess(dataSourceInfo.IsModel ? ItemType.Model : ItemType.DataSource, dataSourceInfo.SecurityDescriptor, CommonOperation.ReadProperties, catalogItemContext.ItemPath);
						}
						string text2 = ((dataSourceInfo.DataSourceReference != null) ? base.Service.CatalogToExternal(dataSourceInfo.DataSourceReference).Value : null);
						list.Add(new ItemReferenceData(dataSourceInfo.OriginalName, text2, ItemType.DataSource.ToString()));
					}
				}
			}
			base.ActionParameters.ItemReferences = list.ToArray();
		}
	}
}
