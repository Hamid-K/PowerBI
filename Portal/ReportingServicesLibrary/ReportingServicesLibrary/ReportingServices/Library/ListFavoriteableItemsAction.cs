using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000024 RID: 36
	internal sealed class ListFavoriteableItemsAction : RSSoapAction<ListFavoriteableItemsActionParameters>
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00005601 File Offset: 0x00003801
		internal ListFavoriteableItemsAction(RSService service)
			: base("ListFavoriteableItemsAction", service)
		{
			this.m_requestInspector = service.RequestInspector;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000561C File Offset: 0x0000381C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			ItemType itemType;
			byte[] array;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			RSService.EnsureItemType(itemType, catalogItemContext.OriginalItemPath.Value, new ItemType[]
			{
				ItemType.Folder,
				ItemType.Site
			});
			if (!base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			bool flag = base.Service.MyReportsEnabled && catalogItemContext.IsRoot && !this.m_requestInspector.IsAnonymous();
			FavoriteableCatalogItemList favoriteableCatalogItemList;
			if (base.ActionParameters.Recursive)
			{
				if (flag)
				{
					base.Service.EnsureMyReportsExists();
				}
				base.Service.Storage.FindFavoriteableItemsRecursive(catalogItemContext.ItemPath, out favoriteableCatalogItemList, base.Service.SecMgr, base.Service, false);
			}
			else
			{
				base.Service.Storage.FindFavoriteableItemsNonRecursive(catalogItemContext.ItemPath, out favoriteableCatalogItemList, base.Service.SecMgr, base.Service, flag);
			}
			base.ActionParameters.Items = favoriteableCatalogItemList;
		}

		// Token: 0x040000BB RID: 187
		private readonly IRSRequestInspector m_requestInspector;
	}
}
