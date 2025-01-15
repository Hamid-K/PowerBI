using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000254 RID: 596
	public sealed class ChartSeriesInstance : BaseInstance
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x0005E0E5 File Offset: 0x0005C2E5
		internal ChartSeriesInstance(InternalChartSeries chartSeriesDef)
			: base(chartSeriesDef.ReportScope)
		{
			this.m_chartSeriesDef = chartSeriesDef;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0005E0FA File Offset: 0x0005C2FA
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartSeriesDef, this.m_chartSeriesDef.ChartDef, this.m_chartSeriesDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005E138 File Offset: 0x0005C338
		public ChartSeriesType Type
		{
			get
			{
				if (this.m_type == null)
				{
					if (this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
					{
						this.m_type = new ChartSeriesType?(this.m_chartSeriesDef.Type.Value);
					}
					else
					{
						this.m_type = new ChartSeriesType?(this.m_chartSeriesDef.ChartSeriesDef.EvaluateType(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_type.Value;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005E1C4 File Offset: 0x0005C3C4
		public ChartSeriesSubtype Subtype
		{
			get
			{
				if (this.m_subtype == null)
				{
					if (this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
					{
						this.m_subtype = new ChartSeriesSubtype?(this.m_chartSeriesDef.Subtype.Value);
					}
					else
					{
						this.m_subtype = new ChartSeriesSubtype?(this.m_chartSeriesDef.ChartSeriesDef.EvaluateSubtype(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_subtype.Value;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0005E250 File Offset: 0x0005C450
		public string LegendName
		{
			get
			{
				if (this.m_legendName == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_legendName = this.m_chartSeriesDef.ChartSeriesDef.EvaluateLegendName(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_legendName;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		internal string LegendText
		{
			get
			{
				if (this.m_legendText == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_legendText = this.m_chartSeriesDef.ChartSeriesDef.EvaluateLegendText(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_legendText;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0005E310 File Offset: 0x0005C510
		internal bool HideInLegend
		{
			get
			{
				if (this.m_hideInLegend == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_hideInLegend = new bool?(this.m_chartSeriesDef.ChartSeriesDef.EvaluateHideInLegend(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hideInLegend.Value;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0005E380 File Offset: 0x0005C580
		public string ChartAreaName
		{
			get
			{
				if (this.m_chartAreaName == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_chartAreaName = this.m_chartSeriesDef.ChartSeriesDef.EvaluateChartAreaName(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_chartAreaName;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0005E3E0 File Offset: 0x0005C5E0
		public string ValueAxisName
		{
			get
			{
				if (this.m_valueAxisName == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_valueAxisName = this.m_chartSeriesDef.ChartSeriesDef.EvaluateValueAxisName(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_valueAxisName;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0005E440 File Offset: 0x0005C640
		public string CategoryAxisName
		{
			get
			{
				if (this.m_categoryAxisName == null && !this.m_chartSeriesDef.ChartDef.IsOldSnapshot)
				{
					this.m_categoryAxisName = this.m_chartSeriesDef.ChartSeriesDef.EvaluateCategoryAxisName(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_categoryAxisName;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0005E4A0 File Offset: 0x0005C6A0
		internal string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_chartSeriesDef.ChartSeriesDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0005E4EC File Offset: 0x0005C6EC
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_chartSeriesDef.ChartSeriesDef.EvaluateHidden(this.ReportScopeInstance, this.m_chartSeriesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005E548 File Offset: 0x0005C748
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_type = null;
			this.m_subtype = null;
			this.m_legendName = null;
			this.m_legendText = null;
			this.m_hideInLegend = null;
			this.m_chartAreaName = null;
			this.m_valueAxisName = null;
			this.m_categoryAxisName = null;
			this.m_toolTip = null;
			this.m_hidden = null;
		}

		// Token: 0x04000B6D RID: 2925
		private InternalChartSeries m_chartSeriesDef;

		// Token: 0x04000B6E RID: 2926
		private StyleInstance m_style;

		// Token: 0x04000B6F RID: 2927
		private ChartSeriesType? m_type;

		// Token: 0x04000B70 RID: 2928
		private ChartSeriesSubtype? m_subtype;

		// Token: 0x04000B71 RID: 2929
		private string m_legendName;

		// Token: 0x04000B72 RID: 2930
		private string m_legendText;

		// Token: 0x04000B73 RID: 2931
		private bool? m_hideInLegend;

		// Token: 0x04000B74 RID: 2932
		private string m_chartAreaName;

		// Token: 0x04000B75 RID: 2933
		private string m_valueAxisName;

		// Token: 0x04000B76 RID: 2934
		private string m_categoryAxisName;

		// Token: 0x04000B77 RID: 2935
		private string m_toolTip;

		// Token: 0x04000B78 RID: 2936
		private bool? m_hidden;
	}
}
