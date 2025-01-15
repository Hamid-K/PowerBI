using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000223 RID: 547
	public sealed class ChartData
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x00054559 File Offset: 0x00052759
		internal ChartData(Chart owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00054568 File Offset: 0x00052768
		internal bool HasSeriesCollection
		{
			get
			{
				return this.m_seriesCollection != null;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00054574 File Offset: 0x00052774
		public ChartSeriesCollection SeriesCollection
		{
			get
			{
				if (this.m_seriesCollection == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						this.m_seriesCollection = new ShimChartSeriesCollection(this.m_owner);
					}
					else
					{
						this.m_seriesCollection = new InternalChartSeriesCollection(this.m_owner, this.m_owner.ChartDef.ChartSeriesCollection);
					}
				}
				return this.m_seriesCollection;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000545D0 File Offset: 0x000527D0
		public ChartDerivedSeriesCollection DerivedSeriesCollection
		{
			get
			{
				if (this.m_chartDerivedSeriesCollection == null && !this.m_owner.IsOldSnapshot && this.m_owner.ChartDef.DerivedSeriesCollection != null)
				{
					this.m_chartDerivedSeriesCollection = new ChartDerivedSeriesCollection(this.m_owner);
				}
				return this.m_chartDerivedSeriesCollection;
			}
		}

		// Token: 0x040009BE RID: 2494
		private Chart m_owner;

		// Token: 0x040009BF RID: 2495
		private ChartSeriesCollection m_seriesCollection;

		// Token: 0x040009C0 RID: 2496
		private ChartDerivedSeriesCollection m_chartDerivedSeriesCollection;
	}
}
