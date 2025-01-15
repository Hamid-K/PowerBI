using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000A9 RID: 169
	public abstract class ChartSeriesExprHost : StyleExprHost
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000037B8 File Offset: 0x000019B8
		public virtual object TypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000037BB File Offset: 0x000019BB
		public virtual object SubtypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000037BE File Offset: 0x000019BE
		public virtual object LegendNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000037C1 File Offset: 0x000019C1
		public virtual object LegendTextExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000037C4 File Offset: 0x000019C4
		public virtual object ChartAreaNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000037C7 File Offset: 0x000019C7
		public virtual object ValueAxisNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000037CA File Offset: 0x000019CA
		public virtual object CategoryAxisNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000037CD File Offset: 0x000019CD
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000037D0 File Offset: 0x000019D0
		public virtual object HideInLegendExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000037D3 File Offset: 0x000019D3
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000037DB File Offset: 0x000019DB
		internal IList<ChartDerivedSeriesExprHost> ChartDerivedSeriesCollectionHostsRemotable
		{
			get
			{
				return this.m_derivedSeriesCollectionHostsRemotable;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000037E3 File Offset: 0x000019E3
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000115 RID: 277
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000116 RID: 278
		public ChartEmptyPointsExprHost EmptyPointsHost;

		// Token: 0x04000117 RID: 279
		public ChartSmartLabelExprHost SmartLabelHost;

		// Token: 0x04000118 RID: 280
		public ChartDataLabelExprHost DataLabelHost;

		// Token: 0x04000119 RID: 281
		public ChartMarkerExprHost ChartMarkerHost;

		// Token: 0x0400011A RID: 282
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x0400011B RID: 283
		[CLSCompliant(false)]
		protected IList<ChartDerivedSeriesExprHost> m_derivedSeriesCollectionHostsRemotable;

		// Token: 0x0400011C RID: 284
		public ChartDataPointInLegendExprHost DataPointInLegendHost;
	}
}
