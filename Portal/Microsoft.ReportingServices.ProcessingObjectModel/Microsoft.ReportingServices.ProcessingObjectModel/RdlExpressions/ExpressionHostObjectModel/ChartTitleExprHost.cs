using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000098 RID: 152
	public abstract class ChartTitleExprHost : ChartTitleBaseExprHost
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000035CD File Offset: 0x000017CD
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000035D0 File Offset: 0x000017D0
		public virtual object DockingExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000035D3 File Offset: 0x000017D3
		public virtual object DockingOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000035D6 File Offset: 0x000017D6
		public virtual object DockOutsideChartAreaExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000035D9 File Offset: 0x000017D9
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000035DC File Offset: 0x000017DC
		public virtual object TextOrientationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000101 RID: 257
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000102 RID: 258
		public ChartElementPositionExprHost ChartElementPositionHost;
	}
}
