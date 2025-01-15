using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F5 RID: 501
	internal sealed class SetSystemPoliciesAction : RSSoapAction<SetSystemPoliciesActionParameters>
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x0003AD97 File Offset: 0x00038F97
		internal SetSystemPoliciesAction(RSService service)
			: base("SetSystemPoliciesAction", service)
		{
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetPolicies, null, null, null, null, null, null, false, null, base.ActionParameters.Policies);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003ADEB File Offset: 0x00038FEB
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.Policies = parameters.Properties;
			this.PerformActionNow();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003AE04 File Offset: 0x00039004
		internal override void PerformActionNow()
		{
			CatalogOperation catalogOperation = CatalogOperation.UpdateSystemSecurityPolicy;
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, catalogOperation, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.Service.SecMgr.SetSystemPolicies(base.ActionParameters.Policies);
		}
	}
}
