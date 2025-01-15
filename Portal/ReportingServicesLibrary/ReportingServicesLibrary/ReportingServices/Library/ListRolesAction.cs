using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E5 RID: 485
	internal sealed class ListRolesAction : RSSoapAction<ListRolesActionParameters>
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x0003A360 File Offset: 0x00038560
		internal ListRolesAction(RSService service)
			: base("ListRolesAction", service)
		{
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003A370 File Offset: 0x00038570
		internal override void PerformActionNow()
		{
			bool flag;
			AuthzData.SecurityScope securityScope = Microsoft.ReportingServices.Library.Soap.Task.SoapTypeToSecurityScope(base.ActionParameters.Scope, out flag);
			base.ActionParameters.Roles = base.Service.SecMgr.GetRoleList(securityScope, flag, new ExternalItemPath(base.ActionParameters.ItemPath));
		}
	}
}
