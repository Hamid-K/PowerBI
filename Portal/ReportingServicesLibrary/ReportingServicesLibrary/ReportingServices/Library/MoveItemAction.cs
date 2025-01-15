using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B2 RID: 178
	internal class MoveItemAction : RSSoapAction<MoveItemActionParameters>
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00020F2D File Offset: 0x0001F12D
		public MoveItemAction(RSService service)
			: base("MoveItemAction", service)
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00020F3C File Offset: 0x0001F13C
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.MoveItem, base.ActionParameters.SourceItemPath, "Item", null, null, base.ActionParameters.TargetItemPath, "Target", false, null, null);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00020F90 File Offset: 0x0001F190
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.SourceItemPath = parameters.Item;
			base.ActionParameters.TargetItemPath = parameters.Param;
			this.PerformActionNow();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00020FBC File Offset: 0x0001F1BC
		internal override void PerformActionNow()
		{
			base.ActionParameters.Validate();
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.SourceItemPath, "Item");
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, base.ActionParameters.TargetItemPath, "Target");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!sourceContext.ItemPath.IsEditSession");
			if (base.Service.MyReportsEnabled && Localization.CatalogCultureCompare(catalogItemContext2.ParentPath, base.Service.PhysicalMyReportsPath) == 0)
			{
				base.Service.EnsureMyReportsExists();
			}
			if (catalogItemContext2.ItemPath.Value.StartsWith(catalogItemContext.ItemPath.Value + CatalogItemNameUtility.PathSeparatorString, Localization.CatalogStringComparison))
			{
				throw new InvalidMoveException(catalogItemContext.OriginalItemPath.Value, catalogItemContext2.OriginalItemPath.Value);
			}
			if (catalogItemContext2.IsRoot)
			{
				throw new ItemAlreadyExistsException("/");
			}
			RSTrace.CatalogTrace.Assert(!catalogItemContext.IsRoot);
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
			if (!base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.UpdateProperties, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			bool flag = true;
			ExternalItemPath externalItemPath = new ExternalItemPath(catalogItemContext2.ParentPath);
			ItemType itemType2;
			Guid guid;
			byte[] array2;
			if (!base.Service.Storage.ObjectExists(externalItemPath, out itemType2, out guid, out array2))
			{
				throw new ItemNotFoundException(catalogItemContext2.ParentPath, "Target");
			}
			if (catalogItemContext.ParentPath != catalogItemContext2.ParentPath)
			{
				flag = false;
				base.Service.EnsureAllowedAsSubitem(itemType2, itemType, array2, new ExternalItemPath(catalogItemContext2.ParentPath), catalogItemContext2.ItemName);
			}
			if (catalogItemContext.ItemPath != catalogItemContext2.ItemPath && !base.Service.Storage.MoveObject(catalogItemContext.CatalogItemPath, catalogItemContext2.ItemName, catalogItemContext2.CatalogItemPath, guid, flag))
			{
				throw new ItemAlreadyExistsException(catalogItemContext2.OriginalItemPath.Value);
			}
			ExternalItemPath externalItemPath2 = new ExternalItemPath(catalogItemContext.ParentPath);
			DateTime now = DateTime.Now;
			base.Service.Storage.SetLastModified(base.Service.ExternalToCatalogItemPath(externalItemPath2), base.Service.UserName, now);
			base.Service.Storage.SetLastModified(base.Service.ExternalToCatalogItemPath(externalItemPath), base.Service.UserName, now);
			base.Service.Storage.SetLastModified(catalogItemContext2.CatalogItemPath, base.Service.UserName, now);
		}
	}
}
