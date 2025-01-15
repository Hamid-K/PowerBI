using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000243 RID: 579
	public sealed class ChartAxisScaleBreak : IROMStyleDefinitionContainer
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x00059FBE File Offset: 0x000581BE
		internal ChartAxisScaleBreak(ChartAxisScaleBreak chartAxisScaleBreakDef, Chart chart)
		{
			this.m_chartAxisScaleBreakDef = chartAxisScaleBreakDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00059FD4 File Offset: 0x000581D4
		public Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.StyleClass != null)
				{
					this.m_style = new Style(this.m_chart, this.m_chart, this.m_chartAxisScaleBreakDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005A031 File Offset: 0x00058231
		public ReportBoolProperty Enabled
		{
			get
			{
				if (this.m_enabled == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.Enabled != null)
				{
					this.m_enabled = new ReportBoolProperty(this.m_chartAxisScaleBreakDef.Enabled);
				}
				return this.m_enabled;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0005A074 File Offset: 0x00058274
		public ReportEnumProperty<ChartBreakLineType> BreakLineType
		{
			get
			{
				if (this.m_breakLineType == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.BreakLineType != null)
				{
					this.m_breakLineType = new ReportEnumProperty<ChartBreakLineType>(this.m_chartAxisScaleBreakDef.BreakLineType.IsExpression, this.m_chartAxisScaleBreakDef.BreakLineType.OriginalText, EnumTranslator.TranslateChartBreakLineType(this.m_chartAxisScaleBreakDef.BreakLineType.StringValue, null));
				}
				return this.m_breakLineType;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0005A0EC File Offset: 0x000582EC
		public ReportIntProperty CollapsibleSpaceThreshold
		{
			get
			{
				if (this.m_collapsibleSpaceThreshold == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.CollapsibleSpaceThreshold != null)
				{
					this.m_collapsibleSpaceThreshold = new ReportIntProperty(this.m_chartAxisScaleBreakDef.CollapsibleSpaceThreshold.IsExpression, this.m_chartAxisScaleBreakDef.CollapsibleSpaceThreshold.OriginalText, this.m_chartAxisScaleBreakDef.CollapsibleSpaceThreshold.IntValue, 25);
				}
				return this.m_collapsibleSpaceThreshold;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0005A160 File Offset: 0x00058360
		public ReportIntProperty MaxNumberOfBreaks
		{
			get
			{
				if (this.m_maxNumberOfBreaks == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.MaxNumberOfBreaks != null)
				{
					this.m_maxNumberOfBreaks = new ReportIntProperty(this.m_chartAxisScaleBreakDef.MaxNumberOfBreaks.IsExpression, this.m_chartAxisScaleBreakDef.MaxNumberOfBreaks.OriginalText, this.m_chartAxisScaleBreakDef.MaxNumberOfBreaks.IntValue, 5);
				}
				return this.m_maxNumberOfBreaks;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0005A1D1 File Offset: 0x000583D1
		public ReportDoubleProperty Spacing
		{
			get
			{
				if (this.m_spacing == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.Spacing != null)
				{
					this.m_spacing = new ReportDoubleProperty(this.m_chartAxisScaleBreakDef.Spacing);
				}
				return this.m_spacing;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005A214 File Offset: 0x00058414
		public ReportEnumProperty<ChartAutoBool> IncludeZero
		{
			get
			{
				if (this.m_includeZero == null && !this.m_chart.IsOldSnapshot && this.m_chartAxisScaleBreakDef.IncludeZero != null)
				{
					this.m_includeZero = new ReportEnumProperty<ChartAutoBool>(this.m_chartAxisScaleBreakDef.IncludeZero.IsExpression, this.m_chartAxisScaleBreakDef.IncludeZero.OriginalText, this.m_chartAxisScaleBreakDef.IncludeZero.IsExpression ? ChartAutoBool.Auto : EnumTranslator.TranslateChartAutoBool(this.m_chartAxisScaleBreakDef.IncludeZero.StringValue, null));
				}
				return this.m_includeZero;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005A29F File Offset: 0x0005849F
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005A2A7 File Offset: 0x000584A7
		internal ChartAxisScaleBreak ChartAxisScaleBreakDef
		{
			get
			{
				return this.m_chartAxisScaleBreakDef;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0005A2AF File Offset: 0x000584AF
		public ChartAxisScaleBreakInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartAxisScaleBreakInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0005A2DF File Offset: 0x000584DF
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

		// Token: 0x04000AC0 RID: 2752
		private Chart m_chart;

		// Token: 0x04000AC1 RID: 2753
		private ChartAxisScaleBreak m_chartAxisScaleBreakDef;

		// Token: 0x04000AC2 RID: 2754
		private ChartAxisScaleBreakInstance m_instance;

		// Token: 0x04000AC3 RID: 2755
		private Style m_style;

		// Token: 0x04000AC4 RID: 2756
		private ReportBoolProperty m_enabled;

		// Token: 0x04000AC5 RID: 2757
		private ReportEnumProperty<ChartBreakLineType> m_breakLineType;

		// Token: 0x04000AC6 RID: 2758
		private ReportIntProperty m_collapsibleSpaceThreshold;

		// Token: 0x04000AC7 RID: 2759
		private ReportIntProperty m_maxNumberOfBreaks;

		// Token: 0x04000AC8 RID: 2760
		private ReportDoubleProperty m_spacing;

		// Token: 0x04000AC9 RID: 2761
		private ReportEnumProperty<ChartAutoBool> m_includeZero;
	}
}
