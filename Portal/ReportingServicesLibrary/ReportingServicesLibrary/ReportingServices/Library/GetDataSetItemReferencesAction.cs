using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FC RID: 252
	internal sealed class GetDataSetItemReferencesAction : RSSoapAction<GetDataSetItemReferencesActionParameters>
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x00027629 File Offset: 0x00025829
		public GetDataSetItemReferencesAction(RSService service)
			: base("GetDataSetItemReferencesAction", service)
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00027638 File Offset: 0x00025838
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSet });
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadDatasource);
			DataSourceInfo theOnlyDataSource = ((DataSetCatalogItem)catalogItem).DataSources.GetTheOnlyDataSource();
			ItemReferenceData[] array = null;
			if (theOnlyDataSource.SecurityDescriptor == null || base.Service.SecMgr.CheckAccess(theOnlyDataSource.IsModel ? ItemType.Model : ItemType.DataSource, theOnlyDataSource.SecurityDescriptor, CommonOperation.ReadProperties, catalogItemContext.ItemPath))
			{
				array = new ItemReferenceData[1];
				string text = ((!string.IsNullOrEmpty(theOnlyDataSource.DataSourceReference)) ? base.Service.CatalogToExternal(theOnlyDataSource.DataSourceReference).Value : null);
				array[0] = new ItemReferenceData(theOnlyDataSource.OriginalName, text, ItemType.DataSource.ToString());
			}
			base.ActionParameters.ItemReferences = array;
		}
	}
}
