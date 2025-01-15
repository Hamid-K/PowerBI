using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024E RID: 590
	public sealed class ChartStripLine : ChartObjectCollectionItem<ChartStripLineInstance>, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x0005BA87 File Offset: 0x00059C87
		internal ChartStripLine(ChartStripLine chartStripLineDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartStripLineDef = chartStripLineDef;
			this.m_chart = chart;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005BAA0 File Offset: 0x00059CA0
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartStripLineDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005BB00 File Offset: 0x00059D00
		public string UniqueName
		{
			get
			{
				return this.m_chart.ChartDef.UniqueName + "x" + this.m_chartStripLineDef.ID.ToString();
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005BB3C File Offset: 0x00059D3C
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chart, this.m_chartStripLineDef.Action, this.m_chart.ChartDef, this.m_chart, ObjectType.Chart, this.m_chartStripLineDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005BBB7 File Offset: 0x00059DB7
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005BBBA File Offset: 0x00059DBA
		public ReportStringProperty Title
		{
			get
			{
				if (this.m_title == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.Title != null)
				{
					this.m_title = new ReportStringProperty(this.m_chartStripLineDef.Title);
				}
				return this.m_title;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005BBFC File Offset: 0x00059DFC
		[Obsolete("Use TextOrientation instead.")]
		public ReportIntProperty TitleAngle
		{
			get
			{
				if (this.m_titleAngle == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.TitleAngle != null)
				{
					this.m_titleAngle = new ReportIntProperty(this.m_chartStripLineDef.TitleAngle.IsExpression, this.m_chartStripLineDef.TitleAngle.OriginalText, this.m_chartStripLineDef.TitleAngle.IntValue, 0);
				}
				return this.m_titleAngle;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005BC70 File Offset: 0x00059E70
		public ReportEnumProperty<TextOrientations> TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.TextOrientation != null)
				{
					this.m_textOrientation = new ReportEnumProperty<TextOrientations>(this.m_chartStripLineDef.TextOrientation.IsExpression, this.m_chartStripLineDef.TextOrientation.OriginalText, EnumTranslator.TranslateTextOrientations(this.m_chartStripLineDef.TextOrientation.StringValue, null));
				}
				return this.m_textOrientation;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0005BCE6 File Offset: 0x00059EE6
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartStripLineDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0005BD26 File Offset: 0x00059F26
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_chartStripLineDef.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0005BD68 File Offset: 0x00059F68
		public ReportEnumProperty<ChartIntervalType> IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.IntervalType != null)
				{
					this.m_intervalType = new ReportEnumProperty<ChartIntervalType>(this.m_chartStripLineDef.IntervalType.IsExpression, this.m_chartStripLineDef.IntervalType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_chartStripLineDef.IntervalType.StringValue, null));
				}
				return this.m_intervalType;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0005BDDE File Offset: 0x00059FDE
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.m_chartStripLineDef.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0005BE20 File Offset: 0x0005A020
		public ReportEnumProperty<ChartIntervalType> IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.IntervalOffsetType != null)
				{
					this.m_intervalOffsetType = new ReportEnumProperty<ChartIntervalType>(this.m_chartStripLineDef.IntervalOffsetType.IsExpression, this.m_chartStripLineDef.IntervalOffsetType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_chartStripLineDef.IntervalOffsetType.StringValue, null));
				}
				return this.m_intervalOffsetType;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0005BE96 File Offset: 0x0005A096
		public ReportDoubleProperty StripWidth
		{
			get
			{
				if (this.m_stripWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.StripWidth != null)
				{
					this.m_stripWidth = new ReportDoubleProperty(this.m_chartStripLineDef.StripWidth);
				}
				return this.m_stripWidth;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0005BED8 File Offset: 0x0005A0D8
		public ReportEnumProperty<ChartIntervalType> StripWidthType
		{
			get
			{
				if (this.m_stripWidthType == null && !this.m_chart.IsOldSnapshot && this.m_chartStripLineDef.StripWidthType != null)
				{
					this.m_stripWidthType = new ReportEnumProperty<ChartIntervalType>(this.m_chartStripLineDef.StripWidthType.IsExpression, this.m_chartStripLineDef.StripWidthType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_chartStripLineDef.StripWidthType.StringValue, null));
				}
				return this.m_stripWidthType;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0005BF4E File Offset: 0x0005A14E
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0005BF56 File Offset: 0x0005A156
		internal ChartStripLine ChartStripLineDef
		{
			get
			{
				return this.m_chartStripLineDef;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0005BF5E File Offset: 0x0005A15E
		public ChartStripLineInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartStripLineInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0005BF8E File Offset: 0x0005A18E
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
		}

		// Token: 0x04000B16 RID: 2838
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000B17 RID: 2839
		private ChartStripLine m_chartStripLineDef;

		// Token: 0x04000B18 RID: 2840
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000B19 RID: 2841
		private ActionInfo m_actionInfo;

		// Token: 0x04000B1A RID: 2842
		private ReportStringProperty m_title;

		// Token: 0x04000B1B RID: 2843
		private ReportEnumProperty<TextOrientations> m_textOrientation;

		// Token: 0x04000B1C RID: 2844
		private ReportIntProperty m_titleAngle;

		// Token: 0x04000B1D RID: 2845
		private ReportStringProperty m_toolTip;

		// Token: 0x04000B1E RID: 2846
		private ReportDoubleProperty m_interval;

		// Token: 0x04000B1F RID: 2847
		private ReportEnumProperty<ChartIntervalType> m_intervalType;

		// Token: 0x04000B20 RID: 2848
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x04000B21 RID: 2849
		private ReportEnumProperty<ChartIntervalType> m_intervalOffsetType;

		// Token: 0x04000B22 RID: 2850
		private ReportDoubleProperty m_stripWidth;

		// Token: 0x04000B23 RID: 2851
		private ReportEnumProperty<ChartIntervalType> m_stripWidthType;
	}
}
