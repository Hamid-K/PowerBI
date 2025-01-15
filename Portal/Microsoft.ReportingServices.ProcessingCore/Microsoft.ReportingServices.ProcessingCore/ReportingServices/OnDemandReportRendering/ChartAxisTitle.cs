using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000240 RID: 576
	public sealed class ChartAxisTitle : ChartObjectCollectionItem<ChartAxisTitleInstance>, IROMStyleDefinitionContainer
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x000594C1 File Offset: 0x000576C1
		internal ChartAxisTitle(ChartAxisTitle chartAxisTitleDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_chartAxisTitleDef = chartAxisTitleDef;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000594D7 File Offset: 0x000576D7
		internal ChartAxisTitle(Microsoft.ReportingServices.ReportProcessing.ChartTitle renderChartTitleDef, ChartTitleInstance renderChartTitleInstance, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_renderChartTitleDef = renderChartTitleDef;
			this.m_renderChartTitleInstance = renderChartTitleInstance;
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x000594F4 File Offset: 0x000576F4
		public ReportStringProperty Caption
		{
			get
			{
				if (this.m_caption == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderChartTitleDef.Caption != null)
						{
							this.m_caption = new ReportStringProperty(this.m_renderChartTitleDef.Caption);
						}
					}
					else if (this.m_chartAxisTitleDef.Caption != null)
					{
						this.m_caption = new ReportStringProperty(this.m_chartAxisTitleDef.Caption);
					}
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00059564 File Offset: 0x00057764
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_renderChartTitleDef.StyleClass, this.m_renderChartTitleInstance.StyleAttributeValues, this.m_chart.RenderingContext);
					}
					else if (this.m_chartAxisTitleDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartAxisTitleDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x000595F0 File Offset: 0x000577F0
		public ReportEnumProperty<ChartAxisTitlePositions> Position
		{
			get
			{
				if (this.m_position == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartAxisTitlePositions chartAxisTitlePositions = ChartAxisTitlePositions.Center;
						switch (this.m_renderChartTitleDef.Position)
						{
						case Microsoft.ReportingServices.ReportProcessing.ChartTitle.Positions.Center:
							chartAxisTitlePositions = ChartAxisTitlePositions.Center;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartTitle.Positions.Near:
							chartAxisTitlePositions = ChartAxisTitlePositions.Near;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartTitle.Positions.Far:
							chartAxisTitlePositions = ChartAxisTitlePositions.Far;
							break;
						}
						this.m_position = new ReportEnumProperty<ChartAxisTitlePositions>(chartAxisTitlePositions);
					}
					else if (this.m_chartAxisTitleDef.Position != null)
					{
						this.m_position = new ReportEnumProperty<ChartAxisTitlePositions>(this.m_chartAxisTitleDef.Position.IsExpression, this.m_chartAxisTitleDef.Position.OriginalText, EnumTranslator.TranslateChartAxisTitlePosition(this.m_chartAxisTitleDef.Position.StringValue, null));
					}
				}
				return this.m_position;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000596A4 File Offset: 0x000578A4
		public ReportEnumProperty<TextOrientations> TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisTitleDef.TextOrientation != null)
				{
					this.m_textOrientation = new ReportEnumProperty<TextOrientations>(this.m_chartAxisTitleDef.TextOrientation.IsExpression, this.m_chartAxisTitleDef.TextOrientation.OriginalText, EnumTranslator.TranslateTextOrientations(this.m_chartAxisTitleDef.TextOrientation.StringValue, null));
				}
				return this.m_textOrientation;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0005971A File Offset: 0x0005791A
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00059722 File Offset: 0x00057922
		internal ChartAxisTitle ChartAxisTitleDef
		{
			get
			{
				return this.m_chartAxisTitleDef;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0005972A File Offset: 0x0005792A
		internal ChartTitleInstance RenderChartTitleInstance
		{
			get
			{
				return this.m_renderChartTitleInstance;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x00059732 File Offset: 0x00057932
		public ChartAxisTitleInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartAxisTitleInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00059762 File Offset: 0x00057962
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000A9F RID: 2719
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000AA0 RID: 2720
		private ChartAxisTitle m_chartAxisTitleDef;

		// Token: 0x04000AA1 RID: 2721
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000AA2 RID: 2722
		private ReportStringProperty m_caption;

		// Token: 0x04000AA3 RID: 2723
		private Microsoft.ReportingServices.ReportProcessing.ChartTitle m_renderChartTitleDef;

		// Token: 0x04000AA4 RID: 2724
		private ChartTitleInstance m_renderChartTitleInstance;

		// Token: 0x04000AA5 RID: 2725
		private ReportEnumProperty<ChartAxisTitlePositions> m_position;

		// Token: 0x04000AA6 RID: 2726
		private ReportEnumProperty<TextOrientations> m_textOrientation;
	}
}
