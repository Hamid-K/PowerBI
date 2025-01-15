using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025A RID: 602
	public sealed class ChartDataPointValuesInstance : BaseInstance
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x0005EE5A File Offset: 0x0005D05A
		internal ChartDataPointValuesInstance(ChartDataPointValues chartDataPointValuesDef)
			: base(chartDataPointValuesDef.ChartDataPoint)
		{
			this.m_chartDataPointValuesDef = chartDataPointValuesDef;
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0005EE70 File Offset: 0x0005D070
		public object X
		{
			get
			{
				if (this.m_x == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("X");
						if (dataValue != null)
						{
							this.m_x = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_x = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateX(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_x;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0005EEFC File Offset: 0x0005D0FC
		public object Y
		{
			get
			{
				if (this.m_y == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("Y");
						if (dataValue != null)
						{
							this.m_y = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_y = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateY(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_y;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005EF88 File Offset: 0x0005D188
		public object Size
		{
			get
			{
				if (this.m_size == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("Size");
						if (dataValue != null)
						{
							this.m_size = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_size = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateSize(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_size;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0005F014 File Offset: 0x0005D214
		public object High
		{
			get
			{
				if (this.m_high == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("High");
						if (dataValue != null)
						{
							this.m_high = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_high = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateHigh(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_high;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005F0A0 File Offset: 0x0005D2A0
		public object Low
		{
			get
			{
				if (this.m_low == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("Low");
						if (dataValue != null)
						{
							this.m_low = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_low = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateLow(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_low;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0005F12C File Offset: 0x0005D32C
		public object Start
		{
			get
			{
				if (this.m_start == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("Open");
						if (dataValue != null)
						{
							this.m_start = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_start = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateStart(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_start;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0005F1B8 File Offset: 0x0005D3B8
		public object End
		{
			get
			{
				if (this.m_end == null)
				{
					if (this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
					{
						DataValue dataValue = this.m_chartDataPointValuesDef.GetDataValue("Close");
						if (dataValue != null)
						{
							this.m_end = dataValue.Instance.Value;
						}
					}
					else
					{
						this.m_end = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateEnd(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
					}
				}
				return this.m_end;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0005F244 File Offset: 0x0005D444
		public object Mean
		{
			get
			{
				if (this.m_mean == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_mean = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateMean(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_mean;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
		public object Median
		{
			get
			{
				if (this.m_median == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_median = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateMedian(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_median;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0005F30C File Offset: 0x0005D50C
		public object HighlightX
		{
			get
			{
				if (this.m_highlightX == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_highlightX = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateHighlightX(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_highlightX;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0005F370 File Offset: 0x0005D570
		public object HighlightY
		{
			get
			{
				if (this.m_highlightY == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_highlightY = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateHighlightY(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_highlightY;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0005F3D4 File Offset: 0x0005D5D4
		public object HighlightSize
		{
			get
			{
				if (this.m_highlightSize == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_highlightSize = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateHighlightSize(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_highlightSize;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0005F438 File Offset: 0x0005D638
		public string FormatX
		{
			get
			{
				if (this.m_formatX == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_formatX = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateFormatX(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_formatX;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0005F498 File Offset: 0x0005D698
		public string FormatY
		{
			get
			{
				if (this.m_formatY == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_formatY = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateFormatY(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_formatY;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		public string FormatSize
		{
			get
			{
				if (this.m_formatSize == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_formatSize = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateFormatSize(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_formatSize;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0005F558 File Offset: 0x0005D758
		public string CurrencyLanguageX
		{
			get
			{
				if (this.m_currencyLanguageX == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_currencyLanguageX = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateCurrencyLanguageX(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_currencyLanguageX;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0005F5B8 File Offset: 0x0005D7B8
		public string CurrencyLanguageY
		{
			get
			{
				if (this.m_currencyLanguageY == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_currencyLanguageY = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateCurrencyLanguageY(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_currencyLanguageY;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005F618 File Offset: 0x0005D818
		public string CurrencyLanguageSize
		{
			get
			{
				if (this.m_currencyLanguageSize == null && !this.m_chartDataPointValuesDef.ChartDef.IsOldSnapshot)
				{
					this.m_currencyLanguageSize = this.m_chartDataPointValuesDef.ChartDataPointValuesDef.EvaluateCurrencyLanguageSize(this.ReportScopeInstance, this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_currencyLanguageSize;
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0005F678 File Offset: 0x0005D878
		protected override void ResetInstanceCache()
		{
			this.m_x = null;
			this.m_y = null;
			this.m_size = null;
			this.m_high = null;
			this.m_low = null;
			this.m_start = null;
			this.m_end = null;
			this.m_mean = null;
			this.m_median = null;
			this.m_highlightX = null;
			this.m_highlightY = null;
			this.m_highlightSize = null;
			this.m_formatX = null;
			this.m_formatY = null;
			this.m_formatSize = null;
			this.m_currencyLanguageX = null;
			this.m_currencyLanguageY = null;
			this.m_currencyLanguageSize = null;
			this.m_fieldsUsedInValuesEvaluated = false;
			this.m_fieldsUsedInValues = null;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0005F714 File Offset: 0x0005D914
		internal List<string> GetFieldsUsedInValues()
		{
			if (!this.m_fieldsUsedInValuesEvaluated)
			{
				this.m_fieldsUsedInValuesEvaluated = true;
				ChartDataPoint dataPointDef = this.m_chartDataPointValuesDef.ChartDataPoint.DataPointDef;
				if (dataPointDef.Action != null && dataPointDef.Action.TrackFieldsUsedInValueExpression)
				{
					this.m_fieldsUsedInValues = new List<string>();
					ObjectModelImpl reportObjectModel = this.m_chartDataPointValuesDef.ChartDef.RenderingContext.OdpContext.ReportObjectModel;
					reportObjectModel.ResetFieldsUsedInExpression();
					ReportVariantProperty reportVariantProperty = this.m_chartDataPointValuesDef.X;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object x = this.X;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Y;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object y = this.Y;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Size;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object size = this.Size;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.High;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object high = this.High;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Low;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object low = this.Low;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Start;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object start = this.Start;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.End;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object end = this.End;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Mean;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object mean = this.Mean;
					}
					reportVariantProperty = this.m_chartDataPointValuesDef.Median;
					if (reportVariantProperty != null && reportVariantProperty.IsExpression)
					{
						object median = this.Median;
					}
					reportObjectModel.AddFieldsUsedInExpression(this.m_fieldsUsedInValues);
				}
			}
			return this.m_fieldsUsedInValues;
		}

		// Token: 0x04000B91 RID: 2961
		private ChartDataPointValues m_chartDataPointValuesDef;

		// Token: 0x04000B92 RID: 2962
		private object m_x;

		// Token: 0x04000B93 RID: 2963
		private object m_y;

		// Token: 0x04000B94 RID: 2964
		private object m_size;

		// Token: 0x04000B95 RID: 2965
		private object m_high;

		// Token: 0x04000B96 RID: 2966
		private object m_low;

		// Token: 0x04000B97 RID: 2967
		private object m_start;

		// Token: 0x04000B98 RID: 2968
		private object m_end;

		// Token: 0x04000B99 RID: 2969
		private object m_mean;

		// Token: 0x04000B9A RID: 2970
		private object m_median;

		// Token: 0x04000B9B RID: 2971
		private object m_highlightX;

		// Token: 0x04000B9C RID: 2972
		private object m_highlightY;

		// Token: 0x04000B9D RID: 2973
		private object m_highlightSize;

		// Token: 0x04000B9E RID: 2974
		private string m_formatX;

		// Token: 0x04000B9F RID: 2975
		private string m_formatY;

		// Token: 0x04000BA0 RID: 2976
		private string m_formatSize;

		// Token: 0x04000BA1 RID: 2977
		private string m_currencyLanguageX;

		// Token: 0x04000BA2 RID: 2978
		private string m_currencyLanguageY;

		// Token: 0x04000BA3 RID: 2979
		private string m_currencyLanguageSize;

		// Token: 0x04000BA4 RID: 2980
		private List<string> m_fieldsUsedInValues;

		// Token: 0x04000BA5 RID: 2981
		private bool m_fieldsUsedInValuesEvaluated;
	}
}
