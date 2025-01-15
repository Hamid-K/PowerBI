using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C2 RID: 194
	internal sealed class ListChildrenAction : RSSoapAction<ListChildrenActionParameters>
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00021EE3 File Offset: 0x000200E3
		internal ListChildrenAction(RSService service)
			: base("ListChildrenAction", service)
		{
			this.m_requestInspector = service.RequestInspector;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00021F00 File Offset: 0x00020100
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
			CatalogItemList catalogItemList;
			if (base.ActionParameters.Recursive)
			{
				if (flag)
				{
					base.Service.EnsureMyReportsExists();
				}
				base.Service.Storage.FindObjectsRecursive(catalogItemContext.ItemPath, out catalogItemList, base.Service.SecMgr, base.Service, false);
			}
			else
			{
				base.Service.Storage.FindObjectsNonRecursive(catalogItemContext.ItemPath, out catalogItemList, base.Service.SecMgr, base.Service, flag);
			}
			base.ActionParameters.Children = catalogItemList;
		}

		// Token: 0x0400042B RID: 1067
		private readonly IRSRequestInspector m_requestInspector;
	}
}
