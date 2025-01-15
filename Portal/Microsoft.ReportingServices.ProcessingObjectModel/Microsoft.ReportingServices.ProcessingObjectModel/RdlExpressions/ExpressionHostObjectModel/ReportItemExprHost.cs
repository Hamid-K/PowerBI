using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200005F RID: 95
	public abstract class ReportItemExprHost : StyleExprHost, IVisibilityHiddenExprHost
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000303C File Offset: 0x0000123C
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003044 File Offset: 0x00001244
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00003047 File Offset: 0x00001247
		public virtual object BookmarkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000304A File Offset: 0x0000124A
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000304D File Offset: 0x0000124D
		public virtual object VisibilityHiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00003050 File Offset: 0x00001250
		public virtual object PageNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400009E RID: 158
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x0400009F RID: 159
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x040000A0 RID: 160
		public PageBreakExprHost PageBreakExprHost;
	}
}
