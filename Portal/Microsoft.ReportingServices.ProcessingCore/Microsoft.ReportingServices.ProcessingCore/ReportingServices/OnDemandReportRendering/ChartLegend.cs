using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023A RID: 570
	public sealed class ChartLegend : ChartObjectCollectionItem<ChartLegendInstance>, IROMStyleDefinitionContainer
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x000580EE File Offset: 0x000562EE
		internal ChartLegend(ChartLegend legendDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_legendDef = legendDef;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00058104 File Offset: 0x00056304
		internal ChartLegend(Legend renderLegendDef, object[] styleValues, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_renderLegendDef = renderLegendDef;
			this.m_styleValues = styleValues;
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00058121 File Offset: 0x00056321
		public string Name
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return "Default";
				}
				return this.m_legendDef.LegendName;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00058144 File Offset: 0x00056344
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_hidden = new ReportBoolProperty(!this.m_renderLegendDef.Visible);
					}
					else
					{
						this.m_hidden = new ReportBoolProperty(this.m_legendDef.Hidden);
					}
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000581A0 File Offset: 0x000563A0
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_renderLegendDef.StyleClass, this.m_styleValues, this.m_chart.RenderingContext);
					}
					else if (this.m_legendDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_legendDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00058228 File Offset: 0x00056428
		public ReportEnumProperty<ChartLegendPositions> Position
		{
			get
			{
				if (this.m_position == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartLegendPositions chartLegendPositions = ChartLegendPositions.TopRight;
						switch (this.m_renderLegendDef.Position)
						{
						case Legend.Positions.RightTop:
							chartLegendPositions = ChartLegendPositions.RightTop;
							break;
						case Legend.Positions.TopLeft:
							chartLegendPositions = ChartLegendPositions.TopLeft;
							break;
						case Legend.Positions.TopCenter:
							chartLegendPositions = ChartLegendPositions.TopCenter;
							break;
						case Legend.Positions.TopRight:
							chartLegendPositions = ChartLegendPositions.TopRight;
							break;
						case Legend.Positions.LeftTop:
							chartLegendPositions = ChartLegendPositions.LeftTop;
							break;
						case Legend.Positions.LeftCenter:
							chartLegendPositions = ChartLegendPositions.LeftCenter;
							break;
						case Legend.Positions.LeftBottom:
							chartLegendPositions = ChartLegendPositions.LeftBottom;
							break;
						case Legend.Positions.RightCenter:
							chartLegendPositions = ChartLegendPositions.RightCenter;
							break;
						case Legend.Positions.RightBottom:
							chartLegendPositions = ChartLegendPositions.RightBottom;
							break;
						case Legend.Positions.BottomLeft:
							chartLegendPositions = ChartLegendPositions.BottomLeft;
							break;
						case Legend.Positions.BottomCenter:
							chartLegendPositions = ChartLegendPositions.BottomCenter;
							break;
						case Legend.Positions.BottomRight:
							chartLegendPositions = ChartLegendPositions.BottomRight;
							break;
						}
						this.m_position = new ReportEnumProperty<ChartLegendPositions>(chartLegendPositions);
					}
					else if (this.m_legendDef.Position != null)
					{
						this.m_position = new ReportEnumProperty<ChartLegendPositions>(this.m_legendDef.Position.IsExpression, this.m_legendDef.Position.OriginalText, EnumTranslator.TranslateChartLegendPositions(this.m_legendDef.Position.StringValue, null));
					}
				}
				return this.m_position;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005832C File Offset: 0x0005652C
		public ReportEnumProperty<ChartLegendLayouts> Layout
		{
			get
			{
				if (this.m_layout == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartLegendLayouts chartLegendLayouts = ChartLegendLayouts.AutoTable;
						switch (this.m_renderLegendDef.Layout)
						{
						case Legend.LegendLayout.Column:
							chartLegendLayouts = ChartLegendLayouts.Column;
							break;
						case Legend.LegendLayout.Row:
							chartLegendLayouts = ChartLegendLayouts.Row;
							break;
						case Legend.LegendLayout.Table:
							chartLegendLayouts = ChartLegendLayouts.AutoTable;
							break;
						}
						this.m_layout = new ReportEnumProperty<ChartLegendLayouts>(chartLegendLayouts);
					}
					else if (this.m_legendDef.Layout != null)
					{
						this.m_layout = new ReportEnumProperty<ChartLegendLayouts>(this.m_legendDef.Layout.IsExpression, this.m_legendDef.Layout.OriginalText, EnumTranslator.TranslateChartLegendLayout(this.m_legendDef.Layout.StringValue, null));
					}
				}
				return this.m_layout;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000583E0 File Offset: 0x000565E0
		public ReportBoolProperty DockOutsideChartArea
		{
			get
			{
				if (this.m_dockOutsideChartArea == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_dockOutsideChartArea = new ReportBoolProperty(!this.m_renderLegendDef.InsidePlotArea);
					}
					else if (this.m_legendDef.DockOutsideChartArea != null)
					{
						this.m_dockOutsideChartArea = new ReportBoolProperty(this.m_legendDef.DockOutsideChartArea);
					}
				}
				return this.m_dockOutsideChartArea;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00058446 File Offset: 0x00056646
		public ChartLegendCustomItemCollection LegendCustomItems
		{
			get
			{
				if (this.m_chartLegendCustomItems == null && !this.m_chart.IsOldSnapshot && this.ChartLegendDef.LegendCustomItems != null)
				{
					this.m_chartLegendCustomItems = new ChartLegendCustomItemCollection(this, this.m_chart);
				}
				return this.m_chartLegendCustomItems;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00058482 File Offset: 0x00056682
		public ChartLegendColumnCollection LegendColumns
		{
			get
			{
				if (this.m_chartLegendColumns == null && !this.m_chart.IsOldSnapshot && this.ChartLegendDef.LegendColumns != null)
				{
					this.m_chartLegendColumns = new ChartLegendColumnCollection(this, this.m_chart);
				}
				return this.m_chartLegendColumns;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000584C0 File Offset: 0x000566C0
		public ChartLegendTitle LegendTitle
		{
			get
			{
				if (this.m_legendTitle == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.LegendTitle != null)
				{
					this.m_legendTitle = new ChartLegendTitle(this.m_legendDef.LegendTitle, this.m_chart);
				}
				return this.m_legendTitle;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00058511 File Offset: 0x00056711
		public string DockToChartArea
		{
			get
			{
				if (!this.m_chart.IsOldSnapshot)
				{
					return this.m_legendDef.DockToChartArea;
				}
				if (!this.DockOutsideChartArea.Value)
				{
					return "Default";
				}
				return null;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00058540 File Offset: 0x00056740
		public ChartElementPosition ChartElementPosition
		{
			get
			{
				if (this.m_chartElementPosition == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.ChartElementPosition != null)
				{
					this.m_chartElementPosition = new ChartElementPosition(this.m_legendDef.ChartElementPosition, this.m_chart);
				}
				return this.m_chartElementPosition;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00058591 File Offset: 0x00056791
		public ReportBoolProperty AutoFitTextDisabled
		{
			get
			{
				if (this.m_autoFitTextDisabled == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.AutoFitTextDisabled != null)
				{
					this.m_autoFitTextDisabled = new ReportBoolProperty(this.m_legendDef.AutoFitTextDisabled);
				}
				return this.m_autoFitTextDisabled;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000585D1 File Offset: 0x000567D1
		public ReportSizeProperty MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.MinFontSize != null)
				{
					this.m_minFontSize = new ReportSizeProperty(this.m_legendDef.MinFontSize);
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00058614 File Offset: 0x00056814
		public ReportEnumProperty<ChartSeparators> HeaderSeparator
		{
			get
			{
				if (this.m_headerSeparator == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.HeaderSeparator != null)
				{
					this.m_headerSeparator = new ReportEnumProperty<ChartSeparators>(this.m_legendDef.HeaderSeparator.IsExpression, this.m_legendDef.HeaderSeparator.OriginalText, EnumTranslator.TranslateChartSeparator(this.m_legendDef.HeaderSeparator.StringValue, null));
				}
				return this.m_headerSeparator;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0005868C File Offset: 0x0005688C
		public ReportColorProperty HeaderSeparatorColor
		{
			get
			{
				if (this.m_headerSeparatorColor == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.HeaderSeparatorColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo headerSeparatorColor = this.m_legendDef.HeaderSeparatorColor;
					this.m_headerSeparatorColor = new ReportColorProperty(headerSeparatorColor.IsExpression, headerSeparatorColor.OriginalText, headerSeparatorColor.IsExpression ? null : new ReportColor(headerSeparatorColor.StringValue.Trim(), true), headerSeparatorColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_headerSeparatorColor;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0005871C File Offset: 0x0005691C
		public ReportEnumProperty<ChartSeparators> ColumnSeparator
		{
			get
			{
				if (this.m_columnSeparator == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.ColumnSeparator != null)
				{
					this.m_columnSeparator = new ReportEnumProperty<ChartSeparators>(this.m_legendDef.ColumnSeparator.IsExpression, this.m_legendDef.ColumnSeparator.OriginalText, EnumTranslator.TranslateChartSeparator(this.m_legendDef.ColumnSeparator.StringValue, null));
				}
				return this.m_columnSeparator;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00058794 File Offset: 0x00056994
		public ReportColorProperty ColumnSeparatorColor
		{
			get
			{
				if (this.m_columnSeparatorColor == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.ColumnSeparatorColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo columnSeparatorColor = this.m_legendDef.ColumnSeparatorColor;
					this.m_columnSeparatorColor = new ReportColorProperty(columnSeparatorColor.IsExpression, columnSeparatorColor.OriginalText, columnSeparatorColor.IsExpression ? null : new ReportColor(columnSeparatorColor.StringValue.Trim(), true), columnSeparatorColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_columnSeparatorColor;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00058824 File Offset: 0x00056A24
		public ReportIntProperty ColumnSpacing
		{
			get
			{
				if (this.m_columnSpacing == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.ColumnSpacing != null)
				{
					this.m_columnSpacing = new ReportIntProperty(this.m_legendDef.ColumnSpacing.IsExpression, this.m_legendDef.ColumnSpacing.OriginalText, this.m_legendDef.ColumnSpacing.IntValue, 50);
				}
				return this.m_columnSpacing;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00058896 File Offset: 0x00056A96
		public ReportBoolProperty InterlacedRows
		{
			get
			{
				if (this.m_interlacedRows == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.InterlacedRows != null)
				{
					this.m_interlacedRows = new ReportBoolProperty(this.m_legendDef.InterlacedRows);
				}
				return this.m_interlacedRows;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000588D8 File Offset: 0x00056AD8
		public ReportColorProperty InterlacedRowsColor
		{
			get
			{
				if (this.m_interlacedRowsColor == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.InterlacedRowsColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo interlacedRowsColor = this.m_legendDef.InterlacedRowsColor;
					this.m_interlacedRowsColor = new ReportColorProperty(interlacedRowsColor.IsExpression, interlacedRowsColor.OriginalText, interlacedRowsColor.IsExpression ? null : new ReportColor(interlacedRowsColor.StringValue.Trim(), true), interlacedRowsColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_interlacedRowsColor;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00058967 File Offset: 0x00056B67
		public ReportBoolProperty EquallySpacedItems
		{
			get
			{
				if (this.m_equallySpacedItems == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.EquallySpacedItems != null)
				{
					this.m_equallySpacedItems = new ReportBoolProperty(this.m_legendDef.EquallySpacedItems);
				}
				return this.m_equallySpacedItems;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000589A8 File Offset: 0x00056BA8
		public ReportEnumProperty<ChartAutoBool> Reversed
		{
			get
			{
				if (this.m_reversed == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.Reversed != null)
				{
					this.m_reversed = new ReportEnumProperty<ChartAutoBool>(this.m_legendDef.Reversed.IsExpression, this.m_legendDef.Reversed.OriginalText, EnumTranslator.TranslateChartAutoBool(this.m_legendDef.Reversed.StringValue, null));
				}
				return this.m_reversed;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00058A20 File Offset: 0x00056C20
		public ReportIntProperty MaxAutoSize
		{
			get
			{
				if (this.m_maxAutoSize == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.MaxAutoSize != null)
				{
					this.m_maxAutoSize = new ReportIntProperty(this.m_legendDef.MaxAutoSize.IsExpression, this.m_legendDef.MaxAutoSize.OriginalText, this.m_legendDef.MaxAutoSize.IntValue, 50);
				}
				return this.m_maxAutoSize;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00058A94 File Offset: 0x00056C94
		public ReportIntProperty TextWrapThreshold
		{
			get
			{
				if (this.m_textWrapThreshold == null && !this.m_chart.IsOldSnapshot && this.m_legendDef.TextWrapThreshold != null)
				{
					this.m_textWrapThreshold = new ReportIntProperty(this.m_legendDef.TextWrapThreshold.IsExpression, this.m_legendDef.TextWrapThreshold.OriginalText, this.m_legendDef.TextWrapThreshold.IntValue, 25);
				}
				return this.m_textWrapThreshold;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00058B06 File Offset: 0x00056D06
		internal ChartLegend ChartLegendDef
		{
			get
			{
				return this.m_legendDef;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00058B0E File Offset: 0x00056D0E
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00058B16 File Offset: 0x00056D16
		public ChartLegendInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00058B48 File Offset: 0x00056D48
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_chartLegendCustomItems != null)
			{
				this.m_chartLegendCustomItems.SetNewContext();
			}
			if (this.m_chartLegendColumns != null)
			{
				this.m_chartLegendColumns.SetNewContext();
			}
			if (this.m_legendTitle != null)
			{
				this.m_legendTitle.SetNewContext();
			}
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.SetNewContext();
			}
		}

		// Token: 0x04000A66 RID: 2662
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000A67 RID: 2663
		private Legend m_renderLegendDef;

		// Token: 0x04000A68 RID: 2664
		private object[] m_styleValues;

		// Token: 0x04000A69 RID: 2665
		private ChartLegend m_legendDef;

		// Token: 0x04000A6A RID: 2666
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000A6B RID: 2667
		private ChartLegendCustomItemCollection m_chartLegendCustomItems;

		// Token: 0x04000A6C RID: 2668
		private ChartLegendColumnCollection m_chartLegendColumns;

		// Token: 0x04000A6D RID: 2669
		private ChartLegendTitle m_legendTitle;

		// Token: 0x04000A6E RID: 2670
		private ReportEnumProperty<ChartLegendLayouts> m_layout;

		// Token: 0x04000A6F RID: 2671
		private ReportEnumProperty<ChartLegendPositions> m_position;

		// Token: 0x04000A70 RID: 2672
		private ReportBoolProperty m_hidden;

		// Token: 0x04000A71 RID: 2673
		private ReportBoolProperty m_dockOutsideChartArea;

		// Token: 0x04000A72 RID: 2674
		private ReportBoolProperty m_autoFitTextDisabled;

		// Token: 0x04000A73 RID: 2675
		private ReportSizeProperty m_minFontSize;

		// Token: 0x04000A74 RID: 2676
		private ReportEnumProperty<ChartSeparators> m_headerSeparator;

		// Token: 0x04000A75 RID: 2677
		private ReportColorProperty m_headerSeparatorColor;

		// Token: 0x04000A76 RID: 2678
		private ReportEnumProperty<ChartSeparators> m_columnSeparator;

		// Token: 0x04000A77 RID: 2679
		private ReportColorProperty m_columnSeparatorColor;

		// Token: 0x04000A78 RID: 2680
		private ReportIntProperty m_columnSpacing;

		// Token: 0x04000A79 RID: 2681
		private ReportBoolProperty m_interlacedRows;

		// Token: 0x04000A7A RID: 2682
		private ReportColorProperty m_interlacedRowsColor;

		// Token: 0x04000A7B RID: 2683
		private ReportBoolProperty m_equallySpacedItems;

		// Token: 0x04000A7C RID: 2684
		private ReportEnumProperty<ChartAutoBool> m_reversed;

		// Token: 0x04000A7D RID: 2685
		private ReportIntProperty m_maxAutoSize;

		// Token: 0x04000A7E RID: 2686
		private ReportIntProperty m_textWrapThreshold;

		// Token: 0x04000A7F RID: 2687
		private ChartElementPosition m_chartElementPosition;
	}
}
