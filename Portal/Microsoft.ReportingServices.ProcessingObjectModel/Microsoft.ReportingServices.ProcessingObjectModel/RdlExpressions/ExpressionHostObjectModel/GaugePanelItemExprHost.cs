using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000081 RID: 129
	public abstract class GaugePanelItemExprHost : StyleExprHost
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000333B File Offset: 0x0000153B
		public virtual object TopExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000333E File Offset: 0x0000153E
		public virtual object LeftExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003341 File Offset: 0x00001541
		public virtual object HeightExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00003344 File Offset: 0x00001544
		public virtual object WidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00003347 File Offset: 0x00001547
		public virtual object ZIndexExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000334A File Offset: 0x0000154A
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000334D File Offset: 0x0000154D
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000D0 RID: 208
		public ActionInfoExprHost ActionInfoHost;
	}
}
