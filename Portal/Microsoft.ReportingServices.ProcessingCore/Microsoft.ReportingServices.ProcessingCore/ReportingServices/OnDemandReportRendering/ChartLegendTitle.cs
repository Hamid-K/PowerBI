using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000239 RID: 569
	public sealed class ChartLegendTitle : IROMStyleDefinitionContainer
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x00057F67 File Offset: 0x00056167
		internal ChartLegendTitle(ChartLegendTitle chartLegendTitleDef, Chart chart)
		{
			this.m_chartLegendTitleDef = chartLegendTitleDef;
			this.m_chart = chart;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00057F80 File Offset: 0x00056180
		public Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot)
				{
					this.m_style = new Style(this.m_chart, this.m_chart, this.m_chartLegendTitleDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00057FD0 File Offset: 0x000561D0
		public ReportStringProperty Caption
		{
			get
			{
				if (this.m_caption == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendTitleDef.Caption != null)
				{
					this.m_caption = new ReportStringProperty(this.m_chartLegendTitleDef.Caption);
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00058010 File Offset: 0x00056210
		public ReportEnumProperty<ChartSeparators> TitleSeparator
		{
			get
			{
				if (this.m_titleSeparator == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendTitleDef.TitleSeparator != null)
				{
					this.m_titleSeparator = new ReportEnumProperty<ChartSeparators>(this.m_chartLegendTitleDef.TitleSeparator.IsExpression, this.m_chartLegendTitleDef.TitleSeparator.OriginalText, EnumTranslator.TranslateChartSeparator(this.m_chartLegendTitleDef.TitleSeparator.StringValue, null));
				}
				return this.m_titleSeparator;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00058086 File Offset: 0x00056286
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0005808E File Offset: 0x0005628E
		internal ChartLegendTitle ChartLegendTitleDef
		{
			get
			{
				return this.m_chartLegendTitleDef;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00058096 File Offset: 0x00056296
		public ChartLegendTitleInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendTitleInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000580C6 File Offset: 0x000562C6
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

		// Token: 0x04000A60 RID: 2656
		private Chart m_chart;

		// Token: 0x04000A61 RID: 2657
		private ChartLegendTitle m_chartLegendTitleDef;

		// Token: 0x04000A62 RID: 2658
		private ChartLegendTitleInstance m_instance;

		// Token: 0x04000A63 RID: 2659
		private Style m_style;

		// Token: 0x04000A64 RID: 2660
		private ReportStringProperty m_caption;

		// Token: 0x04000A65 RID: 2661
		private ReportEnumProperty<ChartSeparators> m_titleSeparator;
	}
}
