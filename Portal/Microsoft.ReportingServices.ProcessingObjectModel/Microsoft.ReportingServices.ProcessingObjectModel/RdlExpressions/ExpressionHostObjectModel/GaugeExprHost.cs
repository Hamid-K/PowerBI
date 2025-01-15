using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200007D RID: 125
	public abstract class GaugeExprHost : GaugePanelItemExprHost
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000032D9 File Offset: 0x000014D9
		public virtual object ClipContentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000032DC File Offset: 0x000014DC
		public virtual object AspectRatioExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000CD RID: 205
		public BackFrameExprHost BackFrameHost;

		// Token: 0x040000CE RID: 206
		public TopImageExprHost TopImageHost;
	}
}
