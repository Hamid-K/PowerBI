using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200007C RID: 124
	public abstract class GaugeImageExprHost : GaugePanelItemExprHost
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000032CB File Offset: 0x000014CB
		public virtual object SourceExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000032CE File Offset: 0x000014CE
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}
	}
}
