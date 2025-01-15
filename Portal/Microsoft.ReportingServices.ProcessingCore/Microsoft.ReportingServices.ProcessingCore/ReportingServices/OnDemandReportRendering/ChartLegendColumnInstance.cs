using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026A RID: 618
	public sealed class ChartLegendColumnInstance : BaseInstance
	{
		// Token: 0x060017FA RID: 6138 RVA: 0x00062204 File Offset: 0x00060404
		internal ChartLegendColumnInstance(ChartLegendColumn chartLegendColumnDef)
			: base(chartLegendColumnDef.ChartDef)
		{
			this.m_chartLegendColumnDef = chartLegendColumnDef;
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00062219 File Offset: 0x00060419
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartLegendColumnDef, this.m_chartLegendColumnDef.ChartDef, this.m_chartLegendColumnDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00062258 File Offset: 0x00060458
		public ChartColumnType ColumnType
		{
			get
			{
				if (this.m_columnType == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_columnType = new ChartColumnType?(this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateColumnType(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_columnType.Value;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x000622C8 File Offset: 0x000604C8
		public string Value
		{
			get
			{
				if (this.m_value == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_value = this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateValue(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00062328 File Offset: 0x00060528
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_toolTip = this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00062388 File Offset: 0x00060588
		public ReportSize MinimumWidth
		{
			get
			{
				if (this.m_minimumWidth == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_minimumWidth = new ReportSize(this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateMinimumWidth(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_minimumWidth;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x000623EC File Offset: 0x000605EC
		public ReportSize MaximumWidth
		{
			get
			{
				if (this.m_maximumWidth == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_maximumWidth = new ReportSize(this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateMaximumWidth(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_maximumWidth;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00062450 File Offset: 0x00060650
		public int SeriesSymbolWidth
		{
			get
			{
				if (this.m_seriesSymbolWidth == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_seriesSymbolWidth = new int?(this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateSeriesSymbolWidth(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_seriesSymbolWidth.Value;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x000624C0 File Offset: 0x000606C0
		public int SeriesSymbolHeight
		{
			get
			{
				if (this.m_seriesSymbolHeight == null && !this.m_chartLegendColumnDef.ChartDef.IsOldSnapshot)
				{
					this.m_seriesSymbolHeight = new int?(this.m_chartLegendColumnDef.ChartLegendColumnDef.EvaluateSeriesSymbolHeight(this.ReportScopeInstance, this.m_chartLegendColumnDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_seriesSymbolHeight.Value;
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00062530 File Offset: 0x00060730
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_columnType = null;
			this.m_value = null;
			this.m_toolTip = null;
			this.m_minimumWidth = null;
			this.m_maximumWidth = null;
			this.m_seriesSymbolWidth = null;
			this.m_seriesSymbolHeight = null;
		}

		// Token: 0x04000C10 RID: 3088
		private ChartLegendColumn m_chartLegendColumnDef;

		// Token: 0x04000C11 RID: 3089
		private StyleInstance m_style;

		// Token: 0x04000C12 RID: 3090
		private ChartColumnType? m_columnType;

		// Token: 0x04000C13 RID: 3091
		private string m_value;

		// Token: 0x04000C14 RID: 3092
		private string m_toolTip;

		// Token: 0x04000C15 RID: 3093
		private ReportSize m_minimumWidth;

		// Token: 0x04000C16 RID: 3094
		private ReportSize m_maximumWidth;

		// Token: 0x04000C17 RID: 3095
		private int? m_seriesSymbolWidth;

		// Token: 0x04000C18 RID: 3096
		private int? m_seriesSymbolHeight;
	}
}
