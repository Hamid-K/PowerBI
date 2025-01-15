using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000214 RID: 532
	internal sealed class ChangeSubscriptionOwnerAction : RSSoapAction<ChangeSubscriptionOwnerActionParameters>
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x0004324C File Offset: 0x0004144C
		public ChangeSubscriptionOwnerAction(RSService service)
			: base("ChangeSubscriptionOwnerAction", service)
		{
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004325C File Offset: 0x0004145C
		internal override void PerformActionNow()
		{
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.UpdateSystemSecurityPolicy, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.Service.SubscriptionManager.ChangeSubscriptionOwner(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "SubscriptionID"), base.ActionParameters.NewOwner);
		}
	}
}
