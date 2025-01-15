using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200015B RID: 347
	internal sealed class GetModelItemReferencesAction : RSSoapAction<GetModelItemReferencesActionParameters>
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x00030624 File Offset: 0x0002E824
		public GetModelItemReferencesAction(RSService service)
			: base("GetModelItemReferencesAction", service)
		{
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00030634 File Offset: 0x0002E834
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.Model });
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadDatasource);
			DataSourceInfo theOnlyDataSource = ((ModelCatalogItem)catalogItem).DataSources.GetTheOnlyDataSource();
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
