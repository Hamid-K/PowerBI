using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000227 RID: 551
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ChartSeries : ReportElementCollectionBase<ChartDataPoint>, IDataRegionRow, IROMStyleDefinitionContainer
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x000548CD File Offset: 0x00052ACD
		internal ChartSeries(Chart chart, int seriesIndex)
		{
			this.m_chart = chart;
			this.m_seriesIndex = seriesIndex;
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060014B6 RID: 5302
		public abstract string Name { get; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060014B7 RID: 5303
		public abstract Style Style { get; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060014B8 RID: 5304
		internal abstract ActionInfo ActionInfo { get; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060014B9 RID: 5305
		public abstract ReportEnumProperty<ChartSeriesType> Type { get; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060014BA RID: 5306
		public abstract ReportEnumProperty<ChartSeriesSubtype> Subtype { get; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060014BB RID: 5307
		public abstract ChartEmptyPoints EmptyPoints { get; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060014BC RID: 5308
		public abstract ChartSmartLabel SmartLabel { get; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060014BD RID: 5309
		public abstract ReportStringProperty LegendName { get; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060014BE RID: 5310
		internal abstract ReportStringProperty LegendText { get; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060014BF RID: 5311
		internal abstract ReportBoolProperty HideInLegend { get; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060014C0 RID: 5312
		public abstract ReportStringProperty ChartAreaName { get; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060014C1 RID: 5313
		public abstract ReportStringProperty ValueAxisName { get; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060014C2 RID: 5314
		public abstract ReportStringProperty CategoryAxisName { get; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060014C3 RID: 5315
		public abstract CustomPropertyCollection CustomProperties { get; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060014C4 RID: 5316
		public abstract ChartDataLabel DataLabel { get; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060014C5 RID: 5317
		public abstract ChartMarker Marker { get; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060014C6 RID: 5318
		internal abstract ReportStringProperty ToolTip { get; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060014C7 RID: 5319
		public abstract ReportBoolProperty Hidden { get; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060014C8 RID: 5320
		public abstract ChartItemInLegend ChartItemInLegend { get; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060014C9 RID: 5321
		public abstract ChartSeriesInstance Instance { get; }

		// Token: 0x060014CA RID: 5322 RVA: 0x000548E3 File Offset: 0x00052AE3
		IDataRegionCell IDataRegionRow.GetIfExists(int categoryIndex)
		{
			if (this.m_chartDataPoints != null && categoryIndex >= 0 && categoryIndex < this.Count)
			{
				return this.m_chartDataPoints[categoryIndex];
			}
			return null;
		}

		// Token: 0x060014CB RID: 5323
		internal abstract void SetNewContext();

		// Token: 0x040009C5 RID: 2501
		protected Chart m_chart;

		// Token: 0x040009C6 RID: 2502
		protected int m_seriesIndex;

		// Token: 0x040009C7 RID: 2503
		protected ChartDataPoint[] m_chartDataPoints;
	}
}
