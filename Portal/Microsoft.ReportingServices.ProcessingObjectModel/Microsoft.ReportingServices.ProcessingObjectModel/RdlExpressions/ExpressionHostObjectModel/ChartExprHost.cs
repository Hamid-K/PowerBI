using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000096 RID: 150
	public abstract class ChartExprHost : DataRegionExprHost<ChartMemberExprHost, ChartDataPointExprHost>
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00003573 File Offset: 0x00001773
		internal IList<ChartSeriesExprHost> SeriesCollectionHostsRemotable
		{
			get
			{
				return this.m_seriesCollectionHostsRemotable;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000357B File Offset: 0x0000177B
		internal IList<ChartDerivedSeriesExprHost> ChartDerivedSeriesCollectionHostsRemotable
		{
			get
			{
				return this.m_derivedSeriesCollectionHostsRemotable;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00003583 File Offset: 0x00001783
		internal IList<ChartAreaExprHost> ChartAreasHostsRemotable
		{
			get
			{
				return this.m_chartAreasHostsRemotable;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000358B File Offset: 0x0000178B
		internal IList<ChartTitleExprHost> TitlesHostsRemotable
		{
			get
			{
				return this.m_titlesHostsRemotable;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00003593 File Offset: 0x00001793
		internal IList<ChartLegendExprHost> LegendsHostsRemotable
		{
			get
			{
				return this.m_legendsHostsRemotable;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000359B File Offset: 0x0000179B
		internal IList<DataValueExprHost> CodeParametersHostsRemotable
		{
			get
			{
				return this.m_codeParametersHostsRemotable;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000035A3 File Offset: 0x000017A3
		public virtual object DynamicHeightExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000035A6 File Offset: 0x000017A6
		public virtual object DynamicWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000035A9 File Offset: 0x000017A9
		public virtual object PaletteExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000035AC File Offset: 0x000017AC
		internal IList<ChartCustomPaletteColorExprHost> CustomPaletteColorHostsRemotable
		{
			get
			{
				return this.m_customPaletteColorHostsRemotable;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000035B4 File Offset: 0x000017B4
		public virtual object PaletteHatchBehaviorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000F8 RID: 248
		[CLSCompliant(false)]
		protected IList<ChartSeriesExprHost> m_seriesCollectionHostsRemotable;

		// Token: 0x040000F9 RID: 249
		[CLSCompliant(false)]
		protected IList<ChartDerivedSeriesExprHost> m_derivedSeriesCollectionHostsRemotable;

		// Token: 0x040000FA RID: 250
		[CLSCompliant(false)]
		protected IList<ChartAreaExprHost> m_chartAreasHostsRemotable;

		// Token: 0x040000FB RID: 251
		[CLSCompliant(false)]
		protected IList<ChartTitleExprHost> m_titlesHostsRemotable;

		// Token: 0x040000FC RID: 252
		[CLSCompliant(false)]
		protected IList<ChartLegendExprHost> m_legendsHostsRemotable;

		// Token: 0x040000FD RID: 253
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_codeParametersHostsRemotable;

		// Token: 0x040000FE RID: 254
		public ChartTitleExprHost NoDataMessageHost;

		// Token: 0x040000FF RID: 255
		public ChartBorderSkinExprHost BorderSkinHost;

		// Token: 0x04000100 RID: 256
		[CLSCompliant(false)]
		protected IList<ChartCustomPaletteColorExprHost> m_customPaletteColorHostsRemotable;
	}
}
