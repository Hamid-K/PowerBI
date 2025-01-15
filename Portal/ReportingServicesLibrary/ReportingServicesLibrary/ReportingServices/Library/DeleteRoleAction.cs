using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E9 RID: 489
	internal sealed class DeleteRoleAction : RSSoapAction<DeleteRoleActionParameters>
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x0003A620 File Offset: 0x00038820
		internal DeleteRoleAction(RSService service)
			: base("DeleteRoleAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.CustomRolesSecurity);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003A640 File Offset: 0x00038840
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteRole, base.ActionParameters.RoleName, "Name", null, null, null, null, false, null, null);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003A687 File Offset: 0x00038887
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.RoleName = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003A6A0 File Offset: 0x000388A0
		internal override void PerformActionNow()
		{
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.DeleteRoles, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			string text = base.ActionParameters.RoleName.Trim();
			if (Localization.CatalogCultureCompare(text, base.Service.MyReportsRole) == 0)
			{
				throw new ReservedRoleException(base.Service.MyReportsRole);
			}
			base.Service.SecMgr.DeleteRole(base.ActionParameters.RoleName);
		}
	}
}
