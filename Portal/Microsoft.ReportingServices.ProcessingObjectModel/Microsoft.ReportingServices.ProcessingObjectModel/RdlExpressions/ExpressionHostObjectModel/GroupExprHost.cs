using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000063 RID: 99
	public abstract class GroupExprHost : IndexedExprHost
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000030B3 File Offset: 0x000012B3
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000030B6 File Offset: 0x000012B6
		public virtual object PageNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000030B9 File Offset: 0x000012B9
		internal IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				return this.m_filterHostsRemotable;
			}
		}

		// Token: 0x040000A5 RID: 165
		public IndexedExprHost ParentExpressionsHost;

		// Token: 0x040000A6 RID: 166
		public IndexedExprHost ReGroupExpressionsHost;

		// Token: 0x040000A7 RID: 167
		public IndexedExprHost VariableValueHosts;

		// Token: 0x040000A8 RID: 168
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x040000A9 RID: 169
		public IndexedExprHost UserSortExpressionsHost;

		// Token: 0x040000AA RID: 170
		public PageBreakExprHost PageBreakExprHost;
	}
}
