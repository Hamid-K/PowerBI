using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F7 RID: 503
	internal sealed class DeletePoliciesAction : RSSoapAction<DeletePoliciesActionParameters>
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0003AE93 File Offset: 0x00039093
		internal DeletePoliciesAction(RSService service)
			: base("DeletePoliciesAction", service)
		{
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003AEA4 File Offset: 0x000390A4
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeletePolicies, base.ActionParameters.ItemPath, "Item", null, null, null, null, false, null, null);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003AEEB File Offset: 0x000390EB
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003AF04 File Offset: 0x00039104
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
			if (!base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.UpdateDeleteAuthorizationPolicy, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			if (catalogItemContext.IsRoot)
			{
				throw new CannotDeleteRootPolicyException();
			}
			base.Service.SecMgr.DeletePolicy(catalogItemContext.ItemPath);
		}
	}
}
