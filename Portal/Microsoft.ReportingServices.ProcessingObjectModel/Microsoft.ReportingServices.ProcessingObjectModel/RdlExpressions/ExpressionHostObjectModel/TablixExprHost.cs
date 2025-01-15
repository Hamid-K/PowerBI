using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000071 RID: 113
	public abstract class TablixExprHost : DataRegionExprHost<TablixMemberExprHost, TablixCellExprHost>
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000031E4 File Offset: 0x000013E4
		public virtual object TopMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000031E7 File Offset: 0x000013E7
		public virtual object BottomMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000031EA File Offset: 0x000013EA
		public virtual object LeftMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000031ED File Offset: 0x000013ED
		public virtual object RightMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000031F0 File Offset: 0x000013F0
		internal IList<TablixCellExprHost> CornerCellHostsRemotable
		{
			get
			{
				return this.m_cornerCellHostsRemotable;
			}
		}

		// Token: 0x040000C2 RID: 194
		[CLSCompliant(false)]
		protected IList<TablixCellExprHost> m_cornerCellHostsRemotable;
	}
}
