using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200006F RID: 111
	public abstract class DataRegionExprHost<TMemberType, TCellType> : ReportItemExprHost where TMemberType : MemberNodeExprHost<TMemberType> where TCellType : CellExprHost
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000031A9 File Offset: 0x000013A9
		public virtual object NoRowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000031AC File Offset: 0x000013AC
		internal IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				return this.m_filterHostsRemotable;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000031B4 File Offset: 0x000013B4
		internal SortExprHost SortHost
		{
			get
			{
				return this.m_sortHost;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000031BC File Offset: 0x000013BC
		internal IList<IMemberNode> MemberTreeHostsRemotable
		{
			get
			{
				return this.m_memberTreeHostsRemotable;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000031C4 File Offset: 0x000013C4
		internal IList<TCellType> CellHostsRemotable
		{
			get
			{
				return this.m_cellHostsRemotable;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000031CC File Offset: 0x000013CC
		internal IList<JoinConditionExprHost> JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}

		// Token: 0x040000BC RID: 188
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x040000BD RID: 189
		protected SortExprHost m_sortHost;

		// Token: 0x040000BE RID: 190
		[CLSCompliant(false)]
		protected IList<IMemberNode> m_memberTreeHostsRemotable;

		// Token: 0x040000BF RID: 191
		[CLSCompliant(false)]
		protected IList<TCellType> m_cellHostsRemotable;

		// Token: 0x040000C0 RID: 192
		public IndexedExprHost UserSortExpressionsHost;

		// Token: 0x040000C1 RID: 193
		[CLSCompliant(false)]
		protected IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;
	}
}
