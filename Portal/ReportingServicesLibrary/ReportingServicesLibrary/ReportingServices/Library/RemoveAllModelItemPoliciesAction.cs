using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000151 RID: 337
	internal sealed class RemoveAllModelItemPoliciesAction : RSSoapAction<RemoveAllModelItemPoliciesActionParameters>
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002FA4F File Offset: 0x0002DC4F
		internal RemoveAllModelItemPoliciesAction(RSService service)
			: base("RemoveAllModelItemPoliciesAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ModelItemSecurity);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002FA70 File Offset: 0x0002DC70
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.RemoveAllModelItemPolicies, base.ActionParameters.ItemPath, "Model", null, null, null, null, true, null, null);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			ItemType itemType;
			byte[] array;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			RSService.EnsureItemType(itemType, catalogItemContext.OriginalItemPath.Value, new ItemType[] { ItemType.Model });
			if (!base.Service.SecMgr.CheckAccess(itemType, array, ModelOperation.UpdateModelItemAuthorizationPolicies, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.Service.SecMgr.DeleteAllModelItemPolicies(catalogItemContext.CatalogItemPath);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002FB8B File Offset: 0x0002DD8B
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			this.PerformActionNow();
		}
	}
}
