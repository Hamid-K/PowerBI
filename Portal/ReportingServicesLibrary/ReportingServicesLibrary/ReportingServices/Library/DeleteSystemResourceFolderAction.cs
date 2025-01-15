using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000255 RID: 597
	internal sealed class DeleteSystemResourceFolderAction : RSSoapAction<DeleteItemActionParameters>
	{
		// Token: 0x060015C8 RID: 5576 RVA: 0x000565AD File Offset: 0x000547AD
		public DeleteSystemResourceFolderAction(RSService service)
			: base("DeleteSystemResourceFolderAction", service)
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000565BC File Offset: 0x000547BC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteItem, base.ActionParameters.ItemPath, "Item", null, null, null, null, false, null, null);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00020C96 File Offset: 0x0001EE96
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00056604 File Offset: 0x00054804
		internal override void PerformActionNow()
		{
			string itemPath = base.ActionParameters.ItemPath;
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(itemPath, true, "Item");
			if (!catalogItemContext.ParentPath.Equals("/68f0607b-9378-4bbb-9e70-4da3d7d66838"))
			{
				throw new InvalidItemPathException(itemPath);
			}
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.UpdateSystemProperties, null))
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
