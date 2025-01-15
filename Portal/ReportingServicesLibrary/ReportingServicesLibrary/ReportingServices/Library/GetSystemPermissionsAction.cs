using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001FB RID: 507
	internal sealed class GetSystemPermissionsAction : RSSoapAction<GetSystemPermissionsActionParameters>
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x0003B0BF File Offset: 0x000392BF
		internal GetSystemPermissionsAction(RSService service)
			: base("GetSystemPermissionsAction", service)
		{
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003B0CD File Offset: 0x000392CD
		internal override void PerformActionNow()
		{
			base.ActionParameters.Operations = base.Service.SecMgr.GetSystemPermissions();
		}
	}
}
