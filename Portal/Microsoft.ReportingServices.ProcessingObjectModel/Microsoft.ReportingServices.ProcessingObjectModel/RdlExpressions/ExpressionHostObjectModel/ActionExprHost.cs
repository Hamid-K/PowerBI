using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200005D RID: 93
	public abstract class ActionExprHost : ReportObjectModelProxy
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000301D File Offset: 0x0000121D
		public virtual object HyperlinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00003020 File Offset: 0x00001220
		public virtual object DrillThroughReportNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00003023 File Offset: 0x00001223
		internal IList<ParamExprHost> DrillThroughParameterHostsRemotable
		{
			get
			{
				return this.m_drillThroughParameterHostsRemotable;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000302B File Offset: 0x0000122B
		public virtual object DrillThroughBookmarkLinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000302E File Offset: 0x0000122E
		public virtual object BookmarkLinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00003031 File Offset: 0x00001231
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400009D RID: 157
		[CLSCompliant(false)]
		protected IList<ParamExprHost> m_drillThroughParameterHostsRemotable;
	}
}
