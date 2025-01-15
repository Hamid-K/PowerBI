using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004D RID: 77
	internal sealed class ChartDataPointCollection
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0001452D File Offset: 0x0001272D
		internal ChartDataPointCollection(Chart owner, int seriesCount, int categoryCount)
		{
			this.m_owner = owner;
			this.m_seriesCount = seriesCount;
			this.m_categoryCount = categoryCount;
		}

		// Token: 0x170004AC RID: 1196
		public ChartDataPoint this[int series, int category]
		{
			get
			{
				if (series < 0 || series >= this.m_seriesCount)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { series, 0, this.m_seriesCount });
				}
				if (category < 0 || category >= this.m_categoryCount)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { category, 0, this.m_categoryCount });
				}
				ChartDataPoint chartDataPoint = null;
				if (series == 0 && category == 0)
				{
					chartDataPoint = this.m_firstDataPoint;
				}
				else if (series == 0)
				{
					if (this.m_firstSeriesDataPoints != null)
					{
						chartDataPoint = this.m_firstSeriesDataPoints[category - 1];
					}
				}
				else if (category == 0)
				{
					if (this.m_firstCategoryDataPoints != null)
					{
						chartDataPoint = this.m_firstCategoryDataPoints[series - 1];
					}
				}
				else if (this.m_dataPoints != null && this.m_dataPoints[series - 1] != null)
				{
					chartDataPoint = this.m_dataPoints[series - 1][category - 1];
				}
				if (chartDataPoint == null)
				{
					chartDataPoint = new ChartDataPoint(this.m_owner, series, category);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (series == 0 && category == 0)
						{
							this.m_firstDataPoint = chartDataPoint;
						}
						else if (series == 0)
						{
							if (this.m_firstSeriesDataPoints == null)
							{
								this.m_firstSeriesDataPoints = new ChartSeriesDataPoints(this.m_categoryCount - 1);
							}
							this.m_firstSeriesDataPoints[category - 1] = chartDataPoint;
						}
						else if (category == 0)
						{
							if (this.m_firstCategoryDataPoints == null)
							{
								this.m_firstCategoryDataPoints = new ChartSeriesDataPoints(this.m_seriesCount - 1);
							}
							this.m_firstCategoryDataPoints[series - 1] = chartDataPoint;
						}
						else
						{
							if (this.m_dataPoints == null)
							{
								this.m_dataPoints = new ChartSeriesDataPoints[this.m_seriesCount - 1];
							}
							if (this.m_dataPoints[series - 1] == null)
							{
								this.m_dataPoints[series - 1] = new ChartSeriesDataPoints(this.m_categoryCount - 1);
							}
							this.m_dataPoints[series - 1][category - 1] = chartDataPoint;
						}
					}
				}
				return chartDataPoint;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00014733 File Offset: 0x00012933
		public int Count
		{
			get
			{
				return this.m_seriesCount * this.m_categoryCount;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00014742 File Offset: 0x00012942
		public int SeriesCount
		{
			get
			{
				return this.m_seriesCount;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001474A File Offset: 0x0001294A
		public int CategoryCount
		{
			get
			{
				return this.m_categoryCount;
			}
		}

		// Token: 0x04000173 RID: 371
		private Chart m_owner;

		// Token: 0x04000174 RID: 372
		private int m_categoryCount;

		// Token: 0x04000175 RID: 373
		private int m_seriesCount;

		// Token: 0x04000176 RID: 374
		private ChartDataPoint m_firstDataPoint;

		// Token: 0x04000177 RID: 375
		private ChartSeriesDataPoints m_firstCategoryDataPoints;

		// Token: 0x04000178 RID: 376
		private ChartSeriesDataPoints m_firstSeriesDataPoints;

		// Token: 0x04000179 RID: 377
		private ChartSeriesDataPoints[] m_dataPoints;
	}
}
