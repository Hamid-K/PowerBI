using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200002D RID: 45
	public abstract class ActionInfoExprHost : StyleExprHost
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000027D7 File Offset: 0x000009D7
		internal IList<ActionExprHost> ActionItemHostsRemotable
		{
			get
			{
				if (this.m_actionItemHostsRemotable == null && this.ActionItemHosts != null)
				{
					this.m_actionItemHostsRemotable = new RemoteArrayWrapper<ActionExprHost>(this.ActionItemHosts);
				}
				return this.m_actionItemHostsRemotable;
			}
		}

		// Token: 0x04000030 RID: 48
		protected ActionExprHost[] ActionItemHosts;

		// Token: 0x04000031 RID: 49
		[CLSCompliant(false)]
		protected IList<ActionExprHost> m_actionItemHostsRemotable;
	}
}
