using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000245 RID: 581
	public sealed class ChartItemInLegend : IROMActionOwner
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0005A705 File Offset: 0x00058905
		internal ChartItemInLegend(InternalChartSeries chartSeries, ChartItemInLegend chartItemInLegendDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartSeries = chartSeries;
			this.m_chartItemInLegendDef = chartItemInLegendDef;
			this.m_chart = chart;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0005A722 File Offset: 0x00058922
		internal ChartItemInLegend(InternalChartDataPoint chartDataPoint, ChartItemInLegend chartItemInLegendDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_dataPoint = chartDataPoint;
			this.m_chartItemInLegendDef = chartItemInLegendDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0005A73F File Offset: 0x0005893F
		public string UniqueName
		{
			get
			{
				return this.InstancePath.UniqueName + "xInLegend";
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0005A758 File Offset: 0x00058958
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartItemInLegendDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.ReportScope, this.m_chartItemInLegendDef.Action, this.InstancePath, this.m_chart, ObjectType.Chart, this.m_chart.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0005A7CE File Offset: 0x000589CE
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0005A7D1 File Offset: 0x000589D1
		public ReportStringProperty LegendText
		{
			get
			{
				if (this.m_legendText == null && !this.m_chart.IsOldSnapshot && this.m_chartItemInLegendDef.LegendText != null)
				{
					this.m_legendText = new ReportStringProperty(this.m_chartItemInLegendDef.LegendText);
				}
				return this.m_legendText;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005A811 File Offset: 0x00058A11
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartItemInLegendDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartItemInLegendDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0005A851 File Offset: 0x00058A51
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_chartItemInLegendDef.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_chartItemInLegendDef.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0005A884 File Offset: 0x00058A84
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_dataPoint != null)
				{
					return this.m_dataPoint;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ReportScope;
				}
				return this.m_chart;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0005A8AF File Offset: 0x00058AAF
		private IInstancePath InstancePath
		{
			get
			{
				if (this.m_dataPoint != null)
				{
					return this.m_dataPoint.DataPointDef;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ChartSeriesDef;
				}
				return this.m_chart.ChartDef;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0005A8E4 File Offset: 0x00058AE4
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0005A8EC File Offset: 0x00058AEC
		internal ChartItemInLegend ChartItemInLegendDef
		{
			get
			{
				return this.m_chartItemInLegendDef;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		public ChartItemInLegendInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartItemInLegendInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005A924 File Offset: 0x00058B24
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000AD6 RID: 2774
		private ChartItemInLegend m_chartItemInLegendDef;

		// Token: 0x04000AD7 RID: 2775
		private ChartItemInLegendInstance m_instance;

		// Token: 0x04000AD8 RID: 2776
		private ActionInfo m_actionInfo;

		// Token: 0x04000AD9 RID: 2777
		private ReportStringProperty m_legendText;

		// Token: 0x04000ADA RID: 2778
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint m_dataPoint;

		// Token: 0x04000ADB RID: 2779
		private InternalChartSeries m_chartSeries;

		// Token: 0x04000ADC RID: 2780
		private ReportStringProperty m_toolTip;

		// Token: 0x04000ADD RID: 2781
		private ReportBoolProperty m_hidden;
	}
}
