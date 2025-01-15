using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000030 RID: 48
	public abstract class ReportItemExprHost : StyleExprHost, IVisibilityHiddenExprHost
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002848 File Offset: 0x00000A48
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				if (this.m_customPropertyHostsRemotable == null && this.CustomPropertyHosts != null)
				{
					this.m_customPropertyHostsRemotable = new RemoteArrayWrapper<DataValueExprHost>(this.CustomPropertyHosts);
				}
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002871 File Offset: 0x00000A71
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002874 File Offset: 0x00000A74
		public virtual object BookmarkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002877 File Offset: 0x00000A77
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000287A File Offset: 0x00000A7A
		public virtual object VisibilityHiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000034 RID: 52
		public ActionExprHost ActionHost;

		// Token: 0x04000035 RID: 53
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000036 RID: 54
		protected DataValueExprHost[] CustomPropertyHosts;

		// Token: 0x04000037 RID: 55
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;
	}
}
