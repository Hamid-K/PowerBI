using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200006A RID: 106
	public abstract class CellExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000311F File Offset: 0x0000131F
		internal IList<JoinConditionExprHost> JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}

		// Token: 0x040000B2 RID: 178
		[CLSCompliant(false)]
		protected IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;
	}
}
