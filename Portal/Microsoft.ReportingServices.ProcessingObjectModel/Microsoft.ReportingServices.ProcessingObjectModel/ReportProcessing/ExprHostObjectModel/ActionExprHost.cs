using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200002E RID: 46
	public abstract class ActionExprHost : ReportObjectModelProxy
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002808 File Offset: 0x00000A08
		public virtual object HyperlinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000280B File Offset: 0x00000A0B
		public virtual object DrillThroughReportNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000280E File Offset: 0x00000A0E
		internal IList<ParamExprHost> DrillThroughParameterHostsRemotable
		{
			get
			{
				if (this.m_drillThroughParameterHostsRemotable == null && this.DrillThroughParameterHosts != null)
				{
					this.m_drillThroughParameterHostsRemotable = new RemoteArrayWrapper<ParamExprHost>(this.DrillThroughParameterHosts);
				}
				return this.m_drillThroughParameterHostsRemotable;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002837 File Offset: 0x00000A37
		public virtual object DrillThroughBookmarkLinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000283A File Offset: 0x00000A3A
		public virtual object BookmarkLinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000283D File Offset: 0x00000A3D
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000032 RID: 50
		protected ParamExprHost[] DrillThroughParameterHosts;

		// Token: 0x04000033 RID: 51
		[CLSCompliant(false)]
		protected IList<ParamExprHost> m_drillThroughParameterHostsRemotable;
	}
}
