using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022E RID: 558
	public sealed class ChartDataPointValues
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x00055FDE File Offset: 0x000541DE
		internal ChartDataPointValues(ChartDataPoint dataPoint, ChartDataPointValues chartDataPointValuesDef, Chart chart)
		{
			this.m_dataPoint = dataPoint;
			this.m_chartDataPointValuesDef = chartDataPointValuesDef;
			this.m_chart = chart;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00055FFB File Offset: 0x000541FB
		internal ChartDataPointValues(ChartDataPoint dataPoint, Chart chart)
		{
			this.m_dataPoint = dataPoint;
			this.m_chart = chart;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00056014 File Offset: 0x00054214
		public ReportVariantProperty X
		{
			get
			{
				if (this.m_x == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("X");
						if (dataValue != null)
						{
							this.m_x = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.X != null)
					{
						this.m_x = new ReportVariantProperty(this.m_chartDataPointValuesDef.X);
					}
				}
				return this.m_x;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0005607C File Offset: 0x0005427C
		public ReportVariantProperty Y
		{
			get
			{
				if (this.m_y == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("Y");
						if (dataValue != null)
						{
							this.m_y = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.Y != null)
					{
						this.m_y = new ReportVariantProperty(this.m_chartDataPointValuesDef.Y);
					}
				}
				return this.m_y;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000560E4 File Offset: 0x000542E4
		public ReportVariantProperty Size
		{
			get
			{
				if (this.m_size == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("Size");
						if (dataValue != null)
						{
							this.m_size = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.Size != null)
					{
						this.m_size = new ReportVariantProperty(this.m_chartDataPointValuesDef.Size);
					}
				}
				return this.m_size;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005614C File Offset: 0x0005434C
		public ReportVariantProperty High
		{
			get
			{
				if (this.m_high == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("High");
						if (dataValue != null)
						{
							this.m_high = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.High != null)
					{
						this.m_high = new ReportVariantProperty(this.m_chartDataPointValuesDef.High);
					}
				}
				return this.m_high;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000561B4 File Offset: 0x000543B4
		public ReportVariantProperty Low
		{
			get
			{
				if (this.m_low == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("Low");
						if (dataValue != null)
						{
							this.m_low = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.Low != null)
					{
						this.m_low = new ReportVariantProperty(this.m_chartDataPointValuesDef.Low);
					}
				}
				return this.m_low;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0005621C File Offset: 0x0005441C
		public ReportVariantProperty Start
		{
			get
			{
				if (this.m_start == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("Open");
						if (dataValue != null)
						{
							this.m_start = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.Start != null)
					{
						this.m_start = new ReportVariantProperty(this.m_chartDataPointValuesDef.Start);
					}
				}
				return this.m_start;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00056284 File Offset: 0x00054484
		public ReportVariantProperty End
		{
			get
			{
				if (this.m_end == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						DataValue dataValue = this.GetDataValue("Close");
						if (dataValue != null)
						{
							this.m_end = dataValue.Value;
						}
					}
					else if (this.m_chartDataPointValuesDef.End != null)
					{
						this.m_end = new ReportVariantProperty(this.m_chartDataPointValuesDef.End);
					}
				}
				return this.m_end;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x000562EC File Offset: 0x000544EC
		public ReportVariantProperty Mean
		{
			get
			{
				if (this.m_mean == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.Mean != null)
				{
					this.m_mean = new ReportVariantProperty(this.m_chartDataPointValuesDef.Mean);
				}
				return this.m_mean;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0005632C File Offset: 0x0005452C
		public ReportVariantProperty Median
		{
			get
			{
				if (this.m_median == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.Median != null)
				{
					this.m_median = new ReportVariantProperty(this.m_chartDataPointValuesDef.Median);
				}
				return this.m_median;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0005636C File Offset: 0x0005456C
		public ReportVariantProperty HighlightX
		{
			get
			{
				if (this.m_highlightX == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.HighlightX != null)
				{
					this.m_highlightX = new ReportVariantProperty(this.m_chartDataPointValuesDef.HighlightX);
				}
				return this.m_highlightX;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000563AC File Offset: 0x000545AC
		public ReportVariantProperty HighlightY
		{
			get
			{
				if (this.m_highlightY == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.HighlightY != null)
				{
					this.m_highlightY = new ReportVariantProperty(this.m_chartDataPointValuesDef.HighlightY);
				}
				return this.m_highlightY;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000563EC File Offset: 0x000545EC
		public ReportVariantProperty HighlightSize
		{
			get
			{
				if (this.m_highlightSize == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.HighlightSize != null)
				{
					this.m_highlightSize = new ReportVariantProperty(this.m_chartDataPointValuesDef.HighlightSize);
				}
				return this.m_highlightSize;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005642C File Offset: 0x0005462C
		public ReportStringProperty FormatX
		{
			get
			{
				if (this.m_formatX == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.FormatX != null)
				{
					this.m_formatX = new ReportStringProperty(this.m_chartDataPointValuesDef.FormatX);
				}
				return this.m_formatX;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0005646C File Offset: 0x0005466C
		public ReportStringProperty FormatY
		{
			get
			{
				if (this.m_formatY == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.FormatY != null)
				{
					this.m_formatY = new ReportStringProperty(this.m_chartDataPointValuesDef.FormatY);
				}
				return this.m_formatY;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000564AC File Offset: 0x000546AC
		public ReportStringProperty FormatSize
		{
			get
			{
				if (this.m_formatSize == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.FormatSize != null)
				{
					this.m_formatSize = new ReportStringProperty(this.m_chartDataPointValuesDef.FormatSize);
				}
				return this.m_formatSize;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000564EC File Offset: 0x000546EC
		public ReportStringProperty CurrencyLanguageX
		{
			get
			{
				if (this.m_currencyLanguageX == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.CurrencyLanguageX != null)
				{
					this.m_currencyLanguageX = new ReportStringProperty(this.m_chartDataPointValuesDef.CurrencyLanguageX);
				}
				return this.m_currencyLanguageX;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0005652C File Offset: 0x0005472C
		public ReportStringProperty CurrencyLanguageY
		{
			get
			{
				if (this.m_currencyLanguageY == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.CurrencyLanguageY != null)
				{
					this.m_currencyLanguageY = new ReportStringProperty(this.m_chartDataPointValuesDef.CurrencyLanguageY);
				}
				return this.m_currencyLanguageY;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0005656C File Offset: 0x0005476C
		public ReportStringProperty CurrencyLanguageSize
		{
			get
			{
				if (this.m_currencyLanguageSize == null && !this.m_chart.IsOldSnapshot && this.m_chartDataPointValuesDef.CurrencyLanguageSize != null)
				{
					this.m_currencyLanguageSize = new ReportStringProperty(this.m_chartDataPointValuesDef.CurrencyLanguageSize);
				}
				return this.m_currencyLanguageSize;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000565AC File Offset: 0x000547AC
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x000565B4 File Offset: 0x000547B4
		internal ChartDataPoint ChartDataPoint
		{
			get
			{
				return this.m_dataPoint;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000565BC File Offset: 0x000547BC
		internal ChartDataPointValues ChartDataPointValuesDef
		{
			get
			{
				return this.m_chartDataPointValuesDef;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000565C4 File Offset: 0x000547C4
		public ChartDataPointValuesInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartDataPointValuesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000565F4 File Offset: 0x000547F4
		internal DataValue GetDataValue(string propertyName)
		{
			DataValueCollection dataValues = ((ShimChartDataPoint)this.m_dataPoint).DataValues;
			DataValue dataValue;
			try
			{
				dataValue = dataValues[propertyName];
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				dataValue = null;
			}
			return dataValue;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005663C File Offset: 0x0005483C
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000A06 RID: 2566
		private ChartDataPointValues m_chartDataPointValuesDef;

		// Token: 0x04000A07 RID: 2567
		private Chart m_chart;

		// Token: 0x04000A08 RID: 2568
		private ChartDataPointValuesInstance m_instance;

		// Token: 0x04000A09 RID: 2569
		private ReportVariantProperty m_x;

		// Token: 0x04000A0A RID: 2570
		private ReportVariantProperty m_y;

		// Token: 0x04000A0B RID: 2571
		private ReportVariantProperty m_size;

		// Token: 0x04000A0C RID: 2572
		private ReportVariantProperty m_high;

		// Token: 0x04000A0D RID: 2573
		private ReportVariantProperty m_low;

		// Token: 0x04000A0E RID: 2574
		private ReportVariantProperty m_start;

		// Token: 0x04000A0F RID: 2575
		private ReportVariantProperty m_end;

		// Token: 0x04000A10 RID: 2576
		private ReportVariantProperty m_mean;

		// Token: 0x04000A11 RID: 2577
		private ReportVariantProperty m_median;

		// Token: 0x04000A12 RID: 2578
		private ReportVariantProperty m_highlightX;

		// Token: 0x04000A13 RID: 2579
		private ReportVariantProperty m_highlightY;

		// Token: 0x04000A14 RID: 2580
		private ReportVariantProperty m_highlightSize;

		// Token: 0x04000A15 RID: 2581
		private ReportStringProperty m_formatX;

		// Token: 0x04000A16 RID: 2582
		private ReportStringProperty m_formatY;

		// Token: 0x04000A17 RID: 2583
		private ReportStringProperty m_formatSize;

		// Token: 0x04000A18 RID: 2584
		private ReportStringProperty m_currencyLanguageX;

		// Token: 0x04000A19 RID: 2585
		private ReportStringProperty m_currencyLanguageY;

		// Token: 0x04000A1A RID: 2586
		private ReportStringProperty m_currencyLanguageSize;

		// Token: 0x04000A1B RID: 2587
		private ChartDataPoint m_dataPoint;
	}
}
