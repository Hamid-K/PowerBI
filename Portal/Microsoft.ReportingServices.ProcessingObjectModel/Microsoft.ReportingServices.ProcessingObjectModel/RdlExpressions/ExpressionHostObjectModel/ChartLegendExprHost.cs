using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200009B RID: 155
	public abstract class ChartLegendExprHost : StyleExprHost
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000035FD File Offset: 0x000017FD
		internal IList<ChartLegendCustomItemExprHost> ChartLegendCustomItemsHostsRemotable
		{
			get
			{
				return this.m_legendCustomItemsHostsRemotable;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00003605 File Offset: 0x00001805
		internal IList<ChartLegendColumnExprHost> ChartLegendColumnsHostsRemotable
		{
			get
			{
				return this.m_legendColumnsHostsRemotable;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000360D File Offset: 0x0000180D
		public virtual object ChartLegendPositionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00003610 File Offset: 0x00001810
		public virtual object LayoutExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00003613 File Offset: 0x00001813
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00003616 File Offset: 0x00001816
		public virtual object DockOutsideChartAreaExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00003619 File Offset: 0x00001819
		public virtual object AutoFitTextDisabledExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000361C File Offset: 0x0000181C
		public virtual object MinFontSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000361F File Offset: 0x0000181F
		public virtual object HeaderSeparatorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00003622 File Offset: 0x00001822
		public virtual object HeaderSeparatorColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00003625 File Offset: 0x00001825
		public virtual object ColumnSeparatorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00003628 File Offset: 0x00001828
		public virtual object ColumnSeparatorColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000362B File Offset: 0x0000182B
		public virtual object ColumnSpacingExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000362E File Offset: 0x0000182E
		public virtual object InterlacedRowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00003631 File Offset: 0x00001831
		public virtual object InterlacedRowsColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00003634 File Offset: 0x00001834
		public virtual object EquallySpacedItemsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00003637 File Offset: 0x00001837
		public virtual object ReversedExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000363A File Offset: 0x0000183A
		public virtual object MaxAutoSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000363D File Offset: 0x0000183D
		public virtual object TextWrapThresholdExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000103 RID: 259
		[CLSCompliant(false)]
		protected IList<ChartLegendCustomItemExprHost> m_legendCustomItemsHostsRemotable;

		// Token: 0x04000104 RID: 260
		[CLSCompliant(false)]
		protected IList<ChartLegendColumnExprHost> m_legendColumnsHostsRemotable;

		// Token: 0x04000105 RID: 261
		public ChartLegendTitleExprHost TitleExprHost;

		// Token: 0x04000106 RID: 262
		public ChartElementPositionExprHost ChartElementPositionHost;
	}
}
