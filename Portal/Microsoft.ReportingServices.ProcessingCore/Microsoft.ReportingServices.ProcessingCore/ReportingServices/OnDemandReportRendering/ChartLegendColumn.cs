using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000236 RID: 566
	public sealed class ChartLegendColumn : ChartObjectCollectionItem<ChartLegendColumnInstance>, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x000576D7 File Offset: 0x000558D7
		internal ChartLegendColumn(ChartLegendColumn chartLegendColumnDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartLegendColumnDef = chartLegendColumnDef;
			this.m_chart = chart;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000576F0 File Offset: 0x000558F0
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartLegendColumnDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00057750 File Offset: 0x00055950
		public string UniqueName
		{
			get
			{
				return this.m_chart.ChartDef.UniqueName + "x" + this.m_chartLegendColumnDef.ID.ToString();
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0005778C File Offset: 0x0005598C
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chart, this.m_chartLegendColumnDef.Action, this.m_chart.ChartDef, this.m_chart, ObjectType.Chart, this.m_chartLegendColumnDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00057807 File Offset: 0x00055A07
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0005780C File Offset: 0x00055A0C
		public ReportEnumProperty<ChartColumnType> ColumnType
		{
			get
			{
				if (this.m_columnType == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.ColumnType != null)
				{
					this.m_columnType = new ReportEnumProperty<ChartColumnType>(this.m_chartLegendColumnDef.ColumnType.IsExpression, this.m_chartLegendColumnDef.ColumnType.OriginalText, EnumTranslator.TranslateChartColumnType(this.m_chartLegendColumnDef.ColumnType.StringValue, null));
				}
				return this.m_columnType;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00057882 File Offset: 0x00055A82
		public ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.Value != null)
				{
					this.m_value = new ReportStringProperty(this.m_chartLegendColumnDef.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000578C2 File Offset: 0x00055AC2
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartLegendColumnDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00057902 File Offset: 0x00055B02
		public ReportSizeProperty MinimumWidth
		{
			get
			{
				if (this.m_minimumWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.MinimumWidth != null)
				{
					this.m_minimumWidth = new ReportSizeProperty(this.m_chartLegendColumnDef.MinimumWidth);
				}
				return this.m_minimumWidth;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00057942 File Offset: 0x00055B42
		public ReportSizeProperty MaximumWidth
		{
			get
			{
				if (this.m_maximumWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.MaximumWidth != null)
				{
					this.m_maximumWidth = new ReportSizeProperty(this.m_chartLegendColumnDef.MaximumWidth);
				}
				return this.m_maximumWidth;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00057984 File Offset: 0x00055B84
		public ReportIntProperty SeriesSymbolWidth
		{
			get
			{
				if (this.m_seriesSymbolWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.SeriesSymbolWidth != null)
				{
					this.m_seriesSymbolWidth = new ReportIntProperty(this.m_chartLegendColumnDef.SeriesSymbolWidth.IsExpression, this.m_chartLegendColumnDef.SeriesSymbolWidth.OriginalText, this.m_chartLegendColumnDef.SeriesSymbolWidth.IntValue, 200);
				}
				return this.m_seriesSymbolWidth;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x000579FC File Offset: 0x00055BFC
		public ReportIntProperty SeriesSymbolHeight
		{
			get
			{
				if (this.m_seriesSymbolHeight == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.SeriesSymbolHeight != null)
				{
					this.m_seriesSymbolHeight = new ReportIntProperty(this.m_chartLegendColumnDef.SeriesSymbolHeight.IsExpression, this.m_chartLegendColumnDef.SeriesSymbolHeight.OriginalText, this.m_chartLegendColumnDef.SeriesSymbolHeight.IntValue, 70);
				}
				return this.m_seriesSymbolHeight;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00057A70 File Offset: 0x00055C70
		public ChartLegendColumnHeader Header
		{
			get
			{
				if (this.m_header == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnDef.Header != null)
				{
					this.m_header = new ChartLegendColumnHeader(this.m_chartLegendColumnDef.Header, this.m_chart);
				}
				return this.m_header;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00057AC1 File Offset: 0x00055CC1
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00057AC9 File Offset: 0x00055CC9
		internal ChartLegendColumn ChartLegendColumnDef
		{
			get
			{
				return this.m_chartLegendColumnDef;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00057AD1 File Offset: 0x00055CD1
		public ChartLegendColumnInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendColumnInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00057B04 File Offset: 0x00055D04
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_header != null)
			{
				this.m_header.SetNewContext();
			}
		}

		// Token: 0x04000A49 RID: 2633
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000A4A RID: 2634
		private ChartLegendColumn m_chartLegendColumnDef;

		// Token: 0x04000A4B RID: 2635
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000A4C RID: 2636
		private ActionInfo m_actionInfo;

		// Token: 0x04000A4D RID: 2637
		private ReportEnumProperty<ChartColumnType> m_columnType;

		// Token: 0x04000A4E RID: 2638
		private ReportStringProperty m_value;

		// Token: 0x04000A4F RID: 2639
		private ReportStringProperty m_toolTip;

		// Token: 0x04000A50 RID: 2640
		private ReportSizeProperty m_minimumWidth;

		// Token: 0x04000A51 RID: 2641
		private ReportSizeProperty m_maximumWidth;

		// Token: 0x04000A52 RID: 2642
		private ReportIntProperty m_seriesSymbolWidth;

		// Token: 0x04000A53 RID: 2643
		private ReportIntProperty m_seriesSymbolHeight;

		// Token: 0x04000A54 RID: 2644
		private ChartLegendColumnHeader m_header;
	}
}
