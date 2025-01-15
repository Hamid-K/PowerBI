using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200005C RID: 92
	public abstract class ActionInfoExprHost : StyleExprHost
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000300D File Offset: 0x0000120D
		internal IList<ActionExprHost> ActionItemHostsRemotable
		{
			get
			{
				return this.m_actionItemHostsRemotable;
			}
		}

		// Token: 0x0400009C RID: 156
		[CLSCompliant(false)]
		protected IList<ActionExprHost> m_actionItemHostsRemotable;
	}
}
