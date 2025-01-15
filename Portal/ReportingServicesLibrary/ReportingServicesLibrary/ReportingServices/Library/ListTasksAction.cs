using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E3 RID: 483
	internal sealed class ListTasksAction : RSSoapAction<ListTasksActionParameters>
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x0003A2C5 File Offset: 0x000384C5
		internal ListTasksAction(RSService service)
			: base("ListTasksAction", service)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003A2D4 File Offset: 0x000384D4
		internal override void PerformActionNow()
		{
			bool flag;
			AuthzData.SecurityScope securityScope = Task.SoapTypeToSecurityScope(base.ActionParameters.Scope, out flag);
			base.ActionParameters.Tasks = base.Service.SecMgr.GetTaskList(flag, securityScope);
		}
	}
}
