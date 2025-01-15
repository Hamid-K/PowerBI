using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000229 RID: 553
	internal sealed class ShimChartSeries : ChartSeries
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x000550C8 File Offset: 0x000532C8
		internal ShimChartSeries(Chart owner, int seriesIndex, ShimChartMember seriesParentMember)
			: base(owner, seriesIndex)
		{
			this.m_cells = new List<ShimChartDataPoint>();
			this.m_plotAsLine = seriesParentMember.CurrentRenderChartMember.IsPlotTypeLine();
			this.GenerateChartDataPoints(seriesParentMember, null, owner.CategoryHierarchy.MemberCollection as ShimChartMemberCollection);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00055108 File Offset: 0x00053308
		private void GenerateChartDataPoints(ShimChartMember seriesParentMember, ShimChartMember categoryParentMember, ShimChartMemberCollection categoryMembers)
		{
			if (categoryMembers == null)
			{
				this.m_cells.Add(new ShimChartDataPoint(this.m_chart, this.m_seriesIndex, this.m_cells.Count, seriesParentMember, categoryParentMember));
				this.TranslateChartType(this.m_chart.RenderChartDef.Type, this.m_chart.RenderChartDef.SubType);
				return;
			}
			int count = categoryMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimChartMember shimChartMember = categoryMembers[i] as ShimChartMember;
				this.GenerateChartDataPoints(seriesParentMember, shimChartMember, shimChartMember.Children as ShimChartMemberCollection);
			}
		}

		// Token: 0x17000B36 RID: 2870
		public override ChartDataPoint this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_cells[index];
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x000551F3 File Offset: 0x000533F3
		public override int Count
		{
			get
			{
				return this.m_cells.Count;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00055200 File Offset: 0x00053400
		public override string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00055203 File Offset: 0x00053403
		public override Style Style
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00055206 File Offset: 0x00053406
		internal override ActionInfo ActionInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00055209 File Offset: 0x00053409
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0005520C File Offset: 0x0005340C
		public override ReportEnumProperty<ChartSeriesType> Type
		{
			get
			{
				return this.m_chartSeriesType;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00055214 File Offset: 0x00053414
		public override ReportEnumProperty<ChartSeriesSubtype> Subtype
		{
			get
			{
				return this.m_chartSeriesSubtype;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0005521C File Offset: 0x0005341C
		public override ChartSmartLabel SmartLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0005521F File Offset: 0x0005341F
		public override ChartEmptyPoints EmptyPoints
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00055222 File Offset: 0x00053422
		public override ReportStringProperty LegendName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00055225 File Offset: 0x00053425
		internal override ReportStringProperty LegendText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00055228 File Offset: 0x00053428
		internal override ReportBoolProperty HideInLegend
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0005522B File Offset: 0x0005342B
		public override ReportStringProperty ChartAreaName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0005522E File Offset: 0x0005342E
		public override ReportStringProperty ValueAxisName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00055231 File Offset: 0x00053431
		public override ReportStringProperty CategoryAxisName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00055234 File Offset: 0x00053434
		public override ChartDataLabel DataLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00055237 File Offset: 0x00053437
		public override ChartMarker Marker
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0005523A File Offset: 0x0005343A
		internal override ReportStringProperty ToolTip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0005523D File Offset: 0x0005343D
		public override ReportBoolProperty Hidden
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00055240 File Offset: 0x00053440
		public override ChartItemInLegend ChartItemInLegend
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00055243 File Offset: 0x00053443
		public override ChartSeriesInstance Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00055248 File Offset: 0x00053448
		private void TranslateChartType(Chart.ChartTypes chartType, Chart.ChartSubTypes chartSubType)
		{
			ChartSeriesType chartSeriesType = ChartSeriesType.Column;
			ChartSeriesSubtype chartSeriesSubtype = ChartSeriesSubtype.Plain;
			if (this.m_plotAsLine && chartType != Chart.ChartTypes.Line)
			{
				chartSeriesType = ChartSeriesType.Line;
				chartSeriesSubtype = ChartSeriesSubtype.Plain;
			}
			else
			{
				switch (chartType)
				{
				case Chart.ChartTypes.Column:
					chartSeriesType = ChartSeriesType.Column;
					chartSeriesSubtype = this.TranslateChartSubType(chartSubType);
					break;
				case Chart.ChartTypes.Bar:
					chartSeriesType = ChartSeriesType.Bar;
					chartSeriesSubtype = this.TranslateChartSubType(chartSubType);
					break;
				case Chart.ChartTypes.Line:
					chartSeriesType = ChartSeriesType.Line;
					chartSeriesSubtype = this.TranslateChartSubType(chartSubType);
					break;
				case Chart.ChartTypes.Pie:
					chartSeriesType = ChartSeriesType.Shape;
					if (chartSubType == Chart.ChartSubTypes.Exploded)
					{
						chartSeriesSubtype = ChartSeriesSubtype.ExplodedPie;
					}
					else
					{
						chartSeriesSubtype = ChartSeriesSubtype.Pie;
					}
					break;
				case Chart.ChartTypes.Scatter:
					if (chartSubType == Chart.ChartSubTypes.Plain)
					{
						chartSeriesType = ChartSeriesType.Scatter;
						chartSeriesSubtype = ChartSeriesSubtype.Plain;
					}
					else
					{
						chartSeriesType = ChartSeriesType.Line;
						if (chartSubType == Chart.ChartSubTypes.Line)
						{
							chartSeriesSubtype = ChartSeriesSubtype.Plain;
						}
						else
						{
							chartSeriesSubtype = ChartSeriesSubtype.Smooth;
						}
					}
					break;
				case Chart.ChartTypes.Bubble:
					chartSeriesType = ChartSeriesType.Scatter;
					chartSeriesSubtype = ChartSeriesSubtype.Bubble;
					break;
				case Chart.ChartTypes.Area:
					chartSeriesType = ChartSeriesType.Area;
					chartSeriesSubtype = this.TranslateChartSubType(chartSubType);
					break;
				case Chart.ChartTypes.Doughnut:
					chartSeriesType = ChartSeriesType.Shape;
					if (chartSubType == Chart.ChartSubTypes.Exploded)
					{
						chartSeriesSubtype = ChartSeriesSubtype.ExplodedDoughnut;
					}
					else
					{
						chartSeriesSubtype = ChartSeriesSubtype.Doughnut;
					}
					break;
				case Chart.ChartTypes.Stock:
					chartSeriesType = ChartSeriesType.Range;
					chartSeriesSubtype = this.TranslateChartSubType(chartSubType);
					break;
				}
			}
			this.m_chartSeriesType = new ReportEnumProperty<ChartSeriesType>(chartSeriesType);
			this.m_chartSeriesSubtype = new ReportEnumProperty<ChartSeriesSubtype>(chartSeriesSubtype);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00055328 File Offset: 0x00053528
		private ChartSeriesSubtype TranslateChartSubType(Chart.ChartSubTypes chartSubTypes)
		{
			switch (chartSubTypes)
			{
			case Chart.ChartSubTypes.Stacked:
				return ChartSeriesSubtype.Stacked;
			case Chart.ChartSubTypes.PercentStacked:
				return ChartSeriesSubtype.PercentStacked;
			case Chart.ChartSubTypes.Smooth:
				return ChartSeriesSubtype.Smooth;
			case Chart.ChartSubTypes.HighLowClose:
			case Chart.ChartSubTypes.OpenHighLowClose:
				return ChartSeriesSubtype.Stock;
			case Chart.ChartSubTypes.Candlestick:
				return ChartSeriesSubtype.Candlestick;
			}
			return ChartSeriesSubtype.Plain;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00055376 File Offset: 0x00053576
		internal override void SetNewContext()
		{
		}

		// Token: 0x040009E0 RID: 2528
		private List<ShimChartDataPoint> m_cells;

		// Token: 0x040009E1 RID: 2529
		private bool m_plotAsLine;

		// Token: 0x040009E2 RID: 2530
		private ReportEnumProperty<ChartSeriesType> m_chartSeriesType;

		// Token: 0x040009E3 RID: 2531
		private ReportEnumProperty<ChartSeriesSubtype> m_chartSeriesSubtype;
	}
}
