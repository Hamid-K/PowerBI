using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000244 RID: 580
	public sealed class ChartTickMarks : IROMStyleDefinitionContainer
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x0005A307 File Offset: 0x00058507
		internal ChartTickMarks(Axis.TickMarks type, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_type = new ReportEnumProperty<ChartTickMarksType>(this.GetTickMarksType(type));
			this.m_chart = chart;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0005A328 File Offset: 0x00058528
		internal ChartTickMarks(ChartTickMarks chartTickMarksDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartTickMarksDef = chartTickMarksDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0005A340 File Offset: 0x00058540
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartTickMarksDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0005A3A0 File Offset: 0x000585A0
		public ReportEnumProperty<ChartAutoBool> Enabled
		{
			get
			{
				if (this.m_enabled == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_type != null)
						{
							this.m_enabled = new ReportEnumProperty<ChartAutoBool>((this.m_type.Value != ChartTickMarksType.None) ? ChartAutoBool.True : ChartAutoBool.False);
						}
					}
					else if (this.m_chartTickMarksDef.Enabled != null)
					{
						this.m_enabled = new ReportEnumProperty<ChartAutoBool>(this.m_chartTickMarksDef.Enabled.IsExpression, this.m_chartTickMarksDef.Enabled.OriginalText, this.m_chartTickMarksDef.Enabled.IsExpression ? ChartAutoBool.Auto : EnumTranslator.TranslateChartAutoBool(this.m_chartTickMarksDef.Enabled.StringValue, null));
					}
				}
				return this.m_enabled;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0005A458 File Offset: 0x00058658
		public ReportEnumProperty<ChartTickMarksType> Type
		{
			get
			{
				if (this.m_type == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.Type != null)
				{
					this.m_type = new ReportEnumProperty<ChartTickMarksType>(this.m_chartTickMarksDef.Type.IsExpression, this.m_chartTickMarksDef.Type.OriginalText, EnumTranslator.TranslateChartTickMarksType(this.m_chartTickMarksDef.Type.StringValue, null));
				}
				return this.m_type;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0005A4CE File Offset: 0x000586CE
		public ReportDoubleProperty Length
		{
			get
			{
				if (this.m_length == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.Length != null)
				{
					this.m_length = new ReportDoubleProperty(this.m_chartTickMarksDef.Length);
				}
				return this.m_length;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0005A50E File Offset: 0x0005870E
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_chartTickMarksDef.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0005A550 File Offset: 0x00058750
		public ReportEnumProperty<ChartIntervalType> IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.IntervalType != null)
				{
					this.m_intervalType = new ReportEnumProperty<ChartIntervalType>(this.m_chartTickMarksDef.IntervalType.IsExpression, this.m_chartTickMarksDef.IntervalType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_chartTickMarksDef.IntervalType.StringValue, null));
				}
				return this.m_intervalType;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005A5C6 File Offset: 0x000587C6
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.m_chartTickMarksDef.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0005A608 File Offset: 0x00058808
		public ReportEnumProperty<ChartIntervalType> IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chart.IsOldSnapshot && this.m_chartTickMarksDef.IntervalOffsetType != null)
				{
					this.m_intervalOffsetType = new ReportEnumProperty<ChartIntervalType>(this.m_chartTickMarksDef.IntervalOffsetType.IsExpression, this.m_chartTickMarksDef.IntervalOffsetType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_chartTickMarksDef.IntervalOffsetType.StringValue, null));
				}
				return this.m_intervalOffsetType;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005A67E File Offset: 0x0005887E
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0005A686 File Offset: 0x00058886
		internal ChartTickMarks ChartTickMarksDef
		{
			get
			{
				return this.m_chartTickMarksDef;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0005A68E File Offset: 0x0005888E
		public ChartTickMarksInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartTickMarksInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0005A6BE File Offset: 0x000588BE
		private ChartTickMarksType GetTickMarksType(Axis.TickMarks tickMarks)
		{
			switch (tickMarks)
			{
			case Axis.TickMarks.Inside:
				return ChartTickMarksType.Inside;
			case Axis.TickMarks.Outside:
				return ChartTickMarksType.Outside;
			case Axis.TickMarks.Cross:
				return ChartTickMarksType.Cross;
			default:
				return ChartTickMarksType.None;
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0005A6DD File Offset: 0x000588DD
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

		// Token: 0x04000ACA RID: 2762
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000ACB RID: 2763
		private ChartTickMarks m_chartTickMarksDef;

		// Token: 0x04000ACC RID: 2764
		private ChartTickMarksInstance m_instance;

		// Token: 0x04000ACD RID: 2765
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000ACE RID: 2766
		private ReportEnumProperty<ChartAutoBool> m_enabled;

		// Token: 0x04000ACF RID: 2767
		private ReportEnumProperty<ChartTickMarksType> m_type;

		// Token: 0x04000AD0 RID: 2768
		private ReportDoubleProperty m_length;

		// Token: 0x04000AD1 RID: 2769
		private ReportDoubleProperty m_interval;

		// Token: 0x04000AD2 RID: 2770
		private ReportEnumProperty<ChartIntervalType> m_intervalType;

		// Token: 0x04000AD3 RID: 2771
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x04000AD4 RID: 2772
		private ReportEnumProperty<ChartIntervalType> m_intervalOffsetType;
	}
}
