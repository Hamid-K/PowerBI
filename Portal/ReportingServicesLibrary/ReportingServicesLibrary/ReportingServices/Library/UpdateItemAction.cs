using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AA RID: 170
	internal abstract class UpdateItemAction<TParameter, TItem> : RSSoapAction<TParameter> where TParameter : UpdateItemActionParameters, new() where TItem : CatalogItem
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00020621 File Offset: 0x0001E821
		internal UpdateItemAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal virtual bool AllowEditSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002062C File Offset: 0x0001E82C
		internal override void PerformActionNow()
		{
			string itemPath = base.ActionParameters.ItemPath;
			bool allowEditSession = this.AllowEditSession;
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(itemPath, allowEditSession, "ItemPath");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			catalogItem.ModifiedBy = base.Service.UserName;
			catalogItem.ModificationDate = DateTime.Now;
			ItemType itemType = CatalogItem.ExtractCatalogTypeFromRuntimeType(typeof(TItem));
			catalogItem.ThrowIfWrongItemType(new ItemType[] { itemType });
			TItem titem = catalogItem as TItem;
			RSTrace.CatalogTrace.Assert(titem != null, "convertedItem");
			this.Update(titem);
			base.ActionParameters.ItemInfo = titem;
		}

		// Token: 0x060007E8 RID: 2024
		internal abstract void Update(TItem item);
	}
}
