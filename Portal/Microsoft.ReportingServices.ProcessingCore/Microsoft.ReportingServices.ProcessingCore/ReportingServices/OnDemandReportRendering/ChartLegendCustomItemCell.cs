using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024B RID: 587
	public sealed class ChartLegendCustomItemCell : ChartObjectCollectionItem<ChartLegendCustomItemCellInstance>, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x0005B067 File Offset: 0x00059267
		internal ChartLegendCustomItemCell(ChartLegendCustomItemCell chartLegendCustomItemCellDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartLegendCustomItemCellDef = chartLegendCustomItemCellDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0005B080 File Offset: 0x00059280
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartLegendCustomItemCellDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0005B0E0 File Offset: 0x000592E0
		public string UniqueName
		{
			get
			{
				return this.m_chart.ChartDef.UniqueName + "x" + this.m_chartLegendCustomItemCellDef.ID.ToString();
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005B11C File Offset: 0x0005931C
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chart, this.m_chartLegendCustomItemCellDef.Action, this.m_chart.ChartDef, this.m_chart, ObjectType.Chart, this.m_chartLegendCustomItemCellDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0005B197 File Offset: 0x00059397
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0005B19C File Offset: 0x0005939C
		public ReportEnumProperty<ChartCellType> CellType
		{
			get
			{
				if (this.m_cellType == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.CellType != null)
				{
					this.m_cellType = new ReportEnumProperty<ChartCellType>(this.m_chartLegendCustomItemCellDef.CellType.IsExpression, this.m_chartLegendCustomItemCellDef.CellType.OriginalText, EnumTranslator.TranslateChartCellType(this.m_chartLegendCustomItemCellDef.CellType.StringValue, null));
				}
				return this.m_cellType;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0005B212 File Offset: 0x00059412
		public ReportStringProperty Text
		{
			get
			{
				if (this.m_text == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.Text != null)
				{
					this.m_text = new ReportStringProperty(this.m_chartLegendCustomItemCellDef.Text);
				}
				return this.m_text;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0005B254 File Offset: 0x00059454
		public ReportIntProperty CellSpan
		{
			get
			{
				if (this.m_cellSpan == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.CellSpan != null)
				{
					this.m_cellSpan = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.CellSpan.IsExpression, this.m_chartLegendCustomItemCellDef.CellSpan.OriginalText, this.m_chartLegendCustomItemCellDef.CellSpan.IntValue, 1);
				}
				return this.m_cellSpan;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0005B2C5 File Offset: 0x000594C5
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartLegendCustomItemCellDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0005B308 File Offset: 0x00059508
		public ReportIntProperty ImageWidth
		{
			get
			{
				if (this.m_imageWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.ImageWidth != null)
				{
					this.m_imageWidth = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.ImageWidth.IsExpression, this.m_chartLegendCustomItemCellDef.ImageWidth.OriginalText, this.m_chartLegendCustomItemCellDef.ImageWidth.IntValue, 0);
				}
				return this.m_imageWidth;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0005B37C File Offset: 0x0005957C
		public ReportIntProperty ImageHeight
		{
			get
			{
				if (this.m_imageHeight == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.ImageHeight != null)
				{
					this.m_imageHeight = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.ImageHeight.IsExpression, this.m_chartLegendCustomItemCellDef.ImageHeight.OriginalText, this.m_chartLegendCustomItemCellDef.ImageHeight.IntValue, 0);
				}
				return this.m_imageHeight;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0005B3F0 File Offset: 0x000595F0
		public ReportIntProperty SymbolHeight
		{
			get
			{
				if (this.m_symbolHeight == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.SymbolHeight != null)
				{
					this.m_symbolHeight = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.SymbolHeight.IsExpression, this.m_chartLegendCustomItemCellDef.SymbolHeight.OriginalText, this.m_chartLegendCustomItemCellDef.SymbolHeight.IntValue, 0);
				}
				return this.m_symbolHeight;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0005B464 File Offset: 0x00059664
		public ReportIntProperty SymbolWidth
		{
			get
			{
				if (this.m_symbolWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.SymbolWidth != null)
				{
					this.m_symbolWidth = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.SymbolWidth.IsExpression, this.m_chartLegendCustomItemCellDef.SymbolWidth.OriginalText, this.m_chartLegendCustomItemCellDef.SymbolWidth.IntValue, 0);
				}
				return this.m_symbolWidth;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0005B4D8 File Offset: 0x000596D8
		public ReportEnumProperty<ChartCellAlignment> Alignment
		{
			get
			{
				if (this.m_alignment == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.Alignment != null)
				{
					this.m_alignment = new ReportEnumProperty<ChartCellAlignment>(this.m_chartLegendCustomItemCellDef.Alignment.IsExpression, this.m_chartLegendCustomItemCellDef.Alignment.OriginalText, EnumTranslator.TranslateChartCellAlignment(this.m_chartLegendCustomItemCellDef.Alignment.StringValue, null));
				}
				return this.m_alignment;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0005B550 File Offset: 0x00059750
		public ReportIntProperty TopMargin
		{
			get
			{
				if (this.m_topMargin == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.TopMargin != null)
				{
					this.m_topMargin = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.TopMargin.IsExpression, this.m_chartLegendCustomItemCellDef.TopMargin.OriginalText, this.m_chartLegendCustomItemCellDef.TopMargin.IntValue, 0);
				}
				return this.m_topMargin;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005B5C4 File Offset: 0x000597C4
		public ReportIntProperty BottomMargin
		{
			get
			{
				if (this.m_bottomMargin == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.BottomMargin != null)
				{
					this.m_bottomMargin = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.BottomMargin.IsExpression, this.m_chartLegendCustomItemCellDef.BottomMargin.OriginalText, this.m_chartLegendCustomItemCellDef.BottomMargin.IntValue, 0);
				}
				return this.m_bottomMargin;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005B638 File Offset: 0x00059838
		public ReportIntProperty LeftMargin
		{
			get
			{
				if (this.m_leftMargin == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.LeftMargin != null)
				{
					this.m_leftMargin = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.LeftMargin.IsExpression, this.m_chartLegendCustomItemCellDef.LeftMargin.OriginalText, this.m_chartLegendCustomItemCellDef.LeftMargin.IntValue, 0);
				}
				return this.m_leftMargin;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0005B6AC File Offset: 0x000598AC
		public ReportIntProperty RightMargin
		{
			get
			{
				if (this.m_rightMargin == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemCellDef.RightMargin != null)
				{
					this.m_rightMargin = new ReportIntProperty(this.m_chartLegendCustomItemCellDef.RightMargin.IsExpression, this.m_chartLegendCustomItemCellDef.RightMargin.OriginalText, this.m_chartLegendCustomItemCellDef.RightMargin.IntValue, 0);
				}
				return this.m_rightMargin;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005B71D File Offset: 0x0005991D
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0005B725 File Offset: 0x00059925
		internal ChartLegendCustomItemCell ChartLegendCustomItemCellDef
		{
			get
			{
				return this.m_chartLegendCustomItemCellDef;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005B72D File Offset: 0x0005992D
		public ChartLegendCustomItemCellInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendCustomItemCellInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005B75D File Offset: 0x0005995D
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

		// Token: 0x04000AF7 RID: 2807
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000AF8 RID: 2808
		private ChartLegendCustomItemCell m_chartLegendCustomItemCellDef;

		// Token: 0x04000AF9 RID: 2809
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000AFA RID: 2810
		private ActionInfo m_actionInfo;

		// Token: 0x04000AFB RID: 2811
		private ReportEnumProperty<ChartCellType> m_cellType;

		// Token: 0x04000AFC RID: 2812
		private ReportStringProperty m_text;

		// Token: 0x04000AFD RID: 2813
		private ReportIntProperty m_cellSpan;

		// Token: 0x04000AFE RID: 2814
		private ReportStringProperty m_toolTip;

		// Token: 0x04000AFF RID: 2815
		private ReportIntProperty m_imageWidth;

		// Token: 0x04000B00 RID: 2816
		private ReportIntProperty m_imageHeight;

		// Token: 0x04000B01 RID: 2817
		private ReportIntProperty m_symbolHeight;

		// Token: 0x04000B02 RID: 2818
		private ReportIntProperty m_symbolWidth;

		// Token: 0x04000B03 RID: 2819
		private ReportEnumProperty<ChartCellAlignment> m_alignment;

		// Token: 0x04000B04 RID: 2820
		private ReportIntProperty m_topMargin;

		// Token: 0x04000B05 RID: 2821
		private ReportIntProperty m_bottomMargin;

		// Token: 0x04000B06 RID: 2822
		private ReportIntProperty m_leftMargin;

		// Token: 0x04000B07 RID: 2823
		private ReportIntProperty m_rightMargin;
	}
}
