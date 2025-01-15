using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200009C RID: 156
	public abstract class ChartAreaExprHost : StyleExprHost
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00003648 File Offset: 0x00001848
		internal IList<ChartAxisExprHost> CategoryAxesHostsRemotable
		{
			get
			{
				return this.m_categoryAxesHostsRemotable;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00003650 File Offset: 0x00001850
		internal IList<ChartAxisExprHost> ValueAxesHostsRemotable
		{
			get
			{
				return this.m_valueAxesHostsRemotable;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00003658 File Offset: 0x00001858
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000365B File Offset: 0x0000185B
		public virtual object AlignOrientationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000365E File Offset: 0x0000185E
		public virtual object EquallySizedAxesFontExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00003661 File Offset: 0x00001861
		public virtual object CursorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00003664 File Offset: 0x00001864
		public virtual object AxesViewExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00003667 File Offset: 0x00001867
		public virtual object ChartAlignTypePositionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000366A File Offset: 0x0000186A
		public virtual object InnerPlotPositionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000107 RID: 263
		[CLSCompliant(false)]
		protected IList<ChartAxisExprHost> m_categoryAxesHostsRemotable;

		// Token: 0x04000108 RID: 264
		[CLSCompliant(false)]
		protected IList<ChartAxisExprHost> m_valueAxesHostsRemotable;

		// Token: 0x04000109 RID: 265
		public Chart3DPropertiesExprHost Chart3DPropertiesHost;

		// Token: 0x0400010A RID: 266
		public ChartElementPositionExprHost ChartElementPositionHost;

		// Token: 0x0400010B RID: 267
		public ChartElementPositionExprHost ChartInnerPlotPositionHost;
	}
}
