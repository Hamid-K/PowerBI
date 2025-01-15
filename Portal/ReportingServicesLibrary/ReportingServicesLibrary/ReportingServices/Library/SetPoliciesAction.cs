using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F3 RID: 499
	internal sealed class SetPoliciesAction : RSSoapAction<SetPoliciesActionParameters>
	{
		// Token: 0x060010FB RID: 4347 RVA: 0x0003AC22 File Offset: 0x00038E22
		internal SetPoliciesAction(RSService service)
			: base("SetPoliciesAction", service)
		{
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003AC30 File Offset: 0x00038E30
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetPolicies, base.ActionParameters.ItemPath, "Item", null, null, null, null, false, null, base.ActionParameters.Policies);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003AC81 File Offset: 0x00038E81
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Policies = parameters.Properties;
			this.PerformActionNow();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003ACAC File Offset: 0x00038EAC
		internal override void PerformActionNow()
		{
			byte[] array = null;
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			ItemType itemType;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			if (!base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.UpdateDeleteAuthorizationPolicy, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.Service.SecMgr.SetCatalogItemPolicies(catalogItemContext.ItemPath, itemType, base.ActionParameters.Policies);
		}
	}
}
