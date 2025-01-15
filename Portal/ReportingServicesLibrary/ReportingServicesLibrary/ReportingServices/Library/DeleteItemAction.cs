using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B0 RID: 176
	internal sealed class DeleteItemAction : RSSoapAction<DeleteItemActionParameters>
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00020C41 File Offset: 0x0001EE41
		public DeleteItemAction(RSService service)
			: base("DeleteItemAction", service)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00020C50 File Offset: 0x0001EE50
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteItem, base.ActionParameters.ItemPath, "Item", null, null, null, null, false, null, null);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00020C96 File Offset: 0x0001EE96
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(base.ActionParameters.ItemPath, true, "Item");
			if (catalogItemContext.IsRoot)
			{
				throw new ReservedItemException(catalogItemContext.OriginalItemPath.Value);
			}
			if (base.Service.MyReportsEnabled && (ItemPathBase.CatalogCompare(catalogItemContext.OriginalItemPath, Global.VirtualMyReportsPath) == 0 || ItemPathBase.CatalogCompare(catalogItemContext.OriginalItemPath, Global.AllUsersFolderPath) == 0))
			{
				throw new ReservedItemException(catalogItemContext.OriginalItemPath.Value);
			}
			ItemType itemType;
			byte[] array;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			if (!base.ActionParameters.SkipSecurityCheck && !base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.Delete, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			if (!base.ActionParameters.SkipSecurityCheck && !base.Service.Storage.CheckChildrenBeforeDelete(catalogItemContext.ItemPath, base.Service.SecMgr))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			if (!base.Service.Storage.DeleteObject(catalogItemContext.ItemPath))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
		}
	}
}
