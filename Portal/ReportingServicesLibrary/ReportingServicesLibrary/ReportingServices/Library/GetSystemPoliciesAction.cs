using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F1 RID: 497
	internal sealed class GetSystemPoliciesAction : RSSoapAction<GetSystemPoliciesActionParameters>
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x0003AB65 File Offset: 0x00038D65
		internal GetSystemPoliciesAction(RSService service)
			: base("GetSystemPoliciesAction", service)
		{
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003AB74 File Offset: 0x00038D74
		internal override void PerformActionNow()
		{
			CatalogOperation catalogOperation = CatalogOperation.ReadSystemSecurityPolicy;
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, catalogOperation, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.ActionParameters.Policies = base.Service.SecMgr.GetSystemPolicies();
		}
	}
}
