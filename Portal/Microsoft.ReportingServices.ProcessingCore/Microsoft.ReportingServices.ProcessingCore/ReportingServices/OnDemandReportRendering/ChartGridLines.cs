using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000242 RID: 578
	public sealed class ChartGridLines : IROMStyleDefinitionContainer
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x00059CB4 File Offset: 0x00057EB4
		internal ChartGridLines(ChartGridLines gridLinesDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_gridLinesDef = gridLinesDef;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00059CCA File Offset: 0x00057ECA
		internal ChartGridLines(GridLines renderGridLinesDef, object[] styleValues, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_renderGridLinesDef = renderGridLinesDef;
			this.m_styleValues = styleValues;
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00059CE8 File Offset: 0x00057EE8
		public ReportEnumProperty<ChartAutoBool> Enabled
		{
			get
			{
				if (this.m_enabled == null && !this.m_chart.IsOldSnapshot && this.m_gridLinesDef.Enabled != null)
				{
					this.m_enabled = new ReportEnumProperty<ChartAutoBool>(this.m_gridLinesDef.Enabled.IsExpression, this.m_gridLinesDef.Enabled.OriginalText, EnumTranslator.TranslateChartAutoBool(this.m_gridLinesDef.Enabled.StringValue, null));
				}
				return this.m_enabled;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00059D60 File Offset: 0x00057F60
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_renderGridLinesDef.StyleClass, this.m_styleValues, this.m_chart.RenderingContext);
					}
					else if (this.m_gridLinesDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_gridLinesDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00059DE6 File Offset: 0x00057FE6
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chart.IsOldSnapshot && this.m_gridLinesDef.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_gridLinesDef.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00059E26 File Offset: 0x00058026
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chart.IsOldSnapshot && this.m_gridLinesDef.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.m_gridLinesDef.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00059E68 File Offset: 0x00058068
		public ReportEnumProperty<ChartIntervalType> IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chart.IsOldSnapshot && this.m_gridLinesDef.IntervalType != null)
				{
					this.m_intervalType = new ReportEnumProperty<ChartIntervalType>(this.m_gridLinesDef.IntervalType.IsExpression, this.m_gridLinesDef.IntervalType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_gridLinesDef.IntervalType.StringValue, null));
				}
				return this.m_intervalType;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00059EE0 File Offset: 0x000580E0
		public ReportEnumProperty<ChartIntervalType> IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chart.IsOldSnapshot && this.m_gridLinesDef.IntervalOffsetType != null)
				{
					this.m_intervalOffsetType = new ReportEnumProperty<ChartIntervalType>(this.m_gridLinesDef.IntervalOffsetType.IsExpression, this.m_gridLinesDef.IntervalOffsetType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_gridLinesDef.IntervalOffsetType.StringValue, null));
				}
				return this.m_intervalOffsetType;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00059F56 File Offset: 0x00058156
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00059F5E File Offset: 0x0005815E
		internal ChartGridLines ChartGridLinesDef
		{
			get
			{
				return this.m_gridLinesDef;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00059F66 File Offset: 0x00058166
		public ChartGridLinesInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartGridLinesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00059F96 File Offset: 0x00058196
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000AB5 RID: 2741
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000AB6 RID: 2742
		private ChartGridLines m_gridLinesDef;

		// Token: 0x04000AB7 RID: 2743
		private GridLines m_renderGridLinesDef;

		// Token: 0x04000AB8 RID: 2744
		private object[] m_styleValues;

		// Token: 0x04000AB9 RID: 2745
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000ABA RID: 2746
		private ChartGridLinesInstance m_instance;

		// Token: 0x04000ABB RID: 2747
		private ReportEnumProperty<ChartAutoBool> m_enabled;

		// Token: 0x04000ABC RID: 2748
		private ReportDoubleProperty m_interval;

		// Token: 0x04000ABD RID: 2749
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x04000ABE RID: 2750
		private ReportEnumProperty<ChartIntervalType> m_intervalType;

		// Token: 0x04000ABF RID: 2751
		private ReportEnumProperty<ChartIntervalType> m_intervalOffsetType;
	}
}
