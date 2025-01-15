using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000250 RID: 592
	public sealed class ChartAxis : ChartObjectCollectionItem<ChartAxisInstance>, IROMStyleDefinitionContainer
	{
		// Token: 0x060016E0 RID: 5856 RVA: 0x0005C112 File Offset: 0x0005A312
		internal ChartAxis(ChartAxis axisDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_axisDef = axisDef;
			this.m_chart = chart;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005C128 File Offset: 0x0005A328
		internal ChartAxis(Axis renderAxisDef, AxisInstance renderAxisInstance, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart, bool isCategory)
		{
			this.m_renderAxisDef = renderAxisDef;
			this.m_renderAxisInstance = renderAxisInstance;
			this.m_chart = chart;
			this.m_isCategory = isCategory;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0005C150 File Offset: 0x0005A350
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_renderAxisDef.StyleClass, this.m_renderAxisInstance.StyleAttributeValues, this.m_chart.RenderingContext);
					}
					else if (this.m_axisDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_axisDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0005C1DC File Offset: 0x0005A3DC
		public ChartAxisTitle Title
		{
			get
			{
				if (this.m_title == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_title = new ChartAxisTitle(this.m_renderAxisDef.Title, this.m_renderAxisInstance.Title, this.m_chart);
					}
					else if (this.m_axisDef.Title != null)
					{
						this.m_title = new ChartAxisTitle(this.m_axisDef.Title, this.m_chart);
					}
				}
				return this.m_title;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0005C258 File Offset: 0x0005A458
		public string Name
		{
			get
			{
				if (!this.m_chart.IsOldSnapshot)
				{
					return this.m_axisDef.AxisName;
				}
				if (!this.m_isCategory)
				{
					return ChartAxis.Mode.ValueAxis.ToString();
				}
				return ChartAxis.Mode.CategoryAxis.ToString();
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0005C2A8 File Offset: 0x0005A4A8
		public ChartGridLines MajorGridLines
		{
			get
			{
				if (this.m_majorGridlines == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_axisDef.MajorGridLines != null)
						{
							this.m_majorGridlines = new ChartGridLines(this.m_renderAxisDef.MajorGridLines, this.m_renderAxisInstance.MajorGridLinesStyleAttributeValues, this.m_chart);
						}
					}
					else if (this.m_axisDef.MajorGridLines != null)
					{
						this.m_majorGridlines = new ChartGridLines(this.m_axisDef.MajorGridLines, this.m_chart);
					}
				}
				return this.m_majorGridlines;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0005C330 File Offset: 0x0005A530
		public ChartGridLines MinorGridLines
		{
			get
			{
				if (this.m_minorGridlines == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderAxisDef.MinorGridLines != null)
						{
							this.m_minorGridlines = new ChartGridLines(this.m_renderAxisDef.MinorGridLines, this.m_renderAxisInstance.MinorGridLinesStyleAttributeValues, this.m_chart);
						}
					}
					else if (this.m_axisDef.MinorGridLines != null)
					{
						this.m_minorGridlines = new ChartGridLines(this.m_axisDef.MinorGridLines, this.m_chart);
					}
				}
				return this.m_minorGridlines;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
		public ReportVariantProperty CrossAt
		{
			get
			{
				if (this.m_crossAt == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderAxisDef.CrossAt != null)
						{
							this.m_crossAt = new ReportVariantProperty(this.m_renderAxisDef.CrossAt);
						}
					}
					else if (this.m_axisDef.CrossAt != null)
					{
						this.m_crossAt = new ReportVariantProperty(this.m_axisDef.CrossAt);
					}
				}
				return this.m_crossAt;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005C428 File Offset: 0x0005A628
		public ChartStripLineCollection StripLines
		{
			get
			{
				if (this.m_chartStripLines == null && !this.m_chart.IsOldSnapshot && this.AxisDef.StripLines != null)
				{
					this.m_chartStripLines = new ChartStripLineCollection(this, this.m_chart);
				}
				return this.m_chartStripLines;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0005C464 File Offset: 0x0005A664
		public bool Scalar
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return this.m_renderAxisDef.Scalar;
				}
				return this.m_axisDef.Scalar;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005C48C File Offset: 0x0005A68C
		public ReportVariantProperty Minimum
		{
			get
			{
				if (this.m_min == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderAxisDef.Min != null)
						{
							this.m_min = new ReportVariantProperty(this.m_renderAxisDef.Min);
						}
					}
					else if (this.m_axisDef.Minimum != null)
					{
						this.m_min = new ReportVariantProperty(this.m_axisDef.Minimum);
					}
				}
				return this.m_min;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0005C4FC File Offset: 0x0005A6FC
		public ReportVariantProperty Maximum
		{
			get
			{
				if (this.m_max == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderAxisDef.Max != null)
						{
							this.m_max = new ReportVariantProperty(this.m_renderAxisDef.Max);
						}
					}
					else if (this.m_axisDef.Maximum != null)
					{
						this.m_max = new ReportVariantProperty(this.m_axisDef.Maximum);
					}
				}
				return this.m_max;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0005C56C File Offset: 0x0005A76C
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					this.m_customPropertiesReady = true;
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_customProperties = new CustomPropertyCollection(this.m_chart.RenderingContext, new CustomPropertyCollection(this.m_renderAxisDef.CustomProperties, this.m_renderAxisInstance.CustomPropertyInstances));
					}
					else
					{
						this.m_customProperties = new CustomPropertyCollection(this.m_chart.ReportScope.ReportScopeInstance, this.m_chart.RenderingContext, null, this.m_axisDef, ObjectType.Chart, this.m_chart.Name);
					}
				}
				else if (!this.m_customPropertiesReady)
				{
					this.m_customPropertiesReady = true;
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_customProperties.UpdateCustomProperties(new CustomPropertyCollection(this.m_renderAxisDef.CustomProperties, this.m_renderAxisInstance.CustomPropertyInstances));
					}
					else
					{
						this.m_customProperties.UpdateCustomProperties(this.m_chart.ReportScope.ReportScopeInstance, this.m_axisDef, this.m_chart.RenderingContext.OdpContext, ObjectType.Chart, this.m_chart.Name);
					}
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0005C698 File Offset: 0x0005A898
		public ReportEnumProperty<ChartAutoBool> Visible
		{
			get
			{
				if (this.m_visible == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_visible = new ReportEnumProperty<ChartAutoBool>(this.m_renderAxisDef.Visible ? ChartAutoBool.True : ChartAutoBool.False);
					}
					else if (this.m_axisDef.Visible != null)
					{
						this.m_visible = new ReportEnumProperty<ChartAutoBool>(this.m_axisDef.Visible.IsExpression, this.m_axisDef.Visible.OriginalText, this.m_axisDef.Visible.IsExpression ? ChartAutoBool.Auto : EnumTranslator.TranslateChartAutoBool(this.m_axisDef.Visible.StringValue, null));
					}
				}
				return this.m_visible;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005C744 File Offset: 0x0005A944
		public ReportEnumProperty<ChartAutoBool> Margin
		{
			get
			{
				if (this.m_margin == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_margin = new ReportEnumProperty<ChartAutoBool>(this.m_renderAxisDef.Margin ? ChartAutoBool.True : ChartAutoBool.False);
					}
					else if (this.m_axisDef.Margin != null)
					{
						this.m_margin = new ReportEnumProperty<ChartAutoBool>(this.m_axisDef.Margin.IsExpression, this.m_axisDef.Margin.OriginalText, this.m_axisDef.Margin.IsExpression ? ChartAutoBool.Auto : EnumTranslator.TranslateChartAutoBool(this.m_axisDef.Margin.StringValue, null));
					}
				}
				return this.m_margin;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0005C7F0 File Offset: 0x0005A9F0
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_axisDef.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0005C830 File Offset: 0x0005AA30
		public ReportEnumProperty<ChartIntervalType> IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.IntervalType != null)
				{
					this.m_intervalType = new ReportEnumProperty<ChartIntervalType>(this.m_axisDef.IntervalType.IsExpression, this.m_axisDef.IntervalType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_axisDef.IntervalType.StringValue, null));
				}
				return this.m_intervalType;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0005C8A6 File Offset: 0x0005AAA6
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.m_axisDef.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0005C8E8 File Offset: 0x0005AAE8
		public ReportEnumProperty<ChartIntervalType> IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.IntervalOffsetType != null)
				{
					this.m_intervalOffsetType = new ReportEnumProperty<ChartIntervalType>(this.m_axisDef.IntervalOffsetType.IsExpression, this.m_axisDef.IntervalOffsetType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_axisDef.IntervalOffsetType.StringValue, null));
				}
				return this.m_intervalOffsetType;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0005C95E File Offset: 0x0005AB5E
		public ReportDoubleProperty LabelInterval
		{
			get
			{
				if (this.m_labelInterval == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.LabelInterval != null)
				{
					this.m_labelInterval = new ReportDoubleProperty(this.m_axisDef.LabelInterval);
				}
				return this.m_labelInterval;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0005C9A0 File Offset: 0x0005ABA0
		public ReportEnumProperty<ChartIntervalType> LabelIntervalType
		{
			get
			{
				if (this.m_labelIntervalType == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.LabelIntervalType != null)
				{
					this.m_labelIntervalType = new ReportEnumProperty<ChartIntervalType>(this.m_axisDef.LabelIntervalType.IsExpression, this.m_axisDef.LabelIntervalType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_axisDef.LabelIntervalType.StringValue, null));
				}
				return this.m_labelIntervalType;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0005CA16 File Offset: 0x0005AC16
		public ReportDoubleProperty LabelIntervalOffset
		{
			get
			{
				if (this.m_labelIntervalOffset == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.LabelIntervalOffset != null)
				{
					this.m_labelIntervalOffset = new ReportDoubleProperty(this.m_axisDef.LabelIntervalOffset);
				}
				return this.m_labelIntervalOffset;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0005CA58 File Offset: 0x0005AC58
		public ReportEnumProperty<ChartIntervalType> LabelIntervalOffsetType
		{
			get
			{
				if (this.m_labelIntervalOffsetType == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.LabelIntervalOffsetType != null)
				{
					this.m_labelIntervalOffsetType = new ReportEnumProperty<ChartIntervalType>(this.m_axisDef.LabelIntervalOffsetType.IsExpression, this.m_axisDef.LabelIntervalOffsetType.OriginalText, EnumTranslator.TranslateChartIntervalType(this.m_axisDef.LabelIntervalOffsetType.StringValue, null));
				}
				return this.m_labelIntervalOffsetType;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0005CACE File Offset: 0x0005ACCE
		public ReportBoolProperty VariableAutoInterval
		{
			get
			{
				if (this.m_variableAutoInterval == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.VariableAutoInterval != null)
				{
					this.m_variableAutoInterval = new ReportBoolProperty(this.m_axisDef.VariableAutoInterval);
				}
				return this.m_variableAutoInterval;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0005CB10 File Offset: 0x0005AD10
		public ChartTickMarks MajorTickMarks
		{
			get
			{
				if (this.m_majorTickMarks == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_majorTickMarks = new ChartTickMarks(this.m_renderAxisDef.MajorTickMarks, this.m_chart);
					}
					else if (this.m_axisDef.MajorTickMarks != null)
					{
						this.m_majorTickMarks = new ChartTickMarks(this.m_axisDef.MajorTickMarks, this.m_chart);
					}
				}
				return this.m_majorTickMarks;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public ChartTickMarks MinorTickMarks
		{
			get
			{
				if (this.m_minorTickMarks == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_minorTickMarks = new ChartTickMarks(this.m_renderAxisDef.MinorTickMarks, this.m_chart);
					}
					else if (this.m_axisDef.MinorTickMarks != null)
					{
						this.m_minorTickMarks = new ChartTickMarks(this.m_axisDef.MinorTickMarks, this.m_chart);
					}
				}
				return this.m_minorTickMarks;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0005CBEF File Offset: 0x0005ADEF
		public ReportBoolProperty MarksAlwaysAtPlotEdge
		{
			get
			{
				if (this.m_marksAlwaysAtPlotEdge == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.MarksAlwaysAtPlotEdge != null)
				{
					this.m_marksAlwaysAtPlotEdge = new ReportBoolProperty(this.m_axisDef.MarksAlwaysAtPlotEdge);
				}
				return this.m_marksAlwaysAtPlotEdge;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0005CC30 File Offset: 0x0005AE30
		public ReportBoolProperty Reverse
		{
			get
			{
				if (this.m_reverse == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						return new ReportBoolProperty(new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo
						{
							BoolValue = this.m_renderAxisDef.Reverse
						});
					}
					if (this.m_axisDef.Reverse != null)
					{
						this.m_reverse = new ReportBoolProperty(this.m_axisDef.Reverse);
					}
				}
				return this.m_reverse;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0005CC98 File Offset: 0x0005AE98
		public ReportEnumProperty<ChartAxisLocation> Location
		{
			get
			{
				if (this.m_location == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.Location != null)
				{
					this.m_location = new ReportEnumProperty<ChartAxisLocation>(this.m_axisDef.Location.IsExpression, this.m_axisDef.Location.OriginalText, EnumTranslator.TranslateChartAxisLocation(this.m_axisDef.Location.StringValue, null));
				}
				return this.m_location;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0005CD10 File Offset: 0x0005AF10
		public ReportBoolProperty Interlaced
		{
			get
			{
				if (this.m_interlaced == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_interlaced = new ReportBoolProperty(new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo
						{
							BoolValue = this.m_renderAxisDef.Interlaced
						});
					}
					else if (this.m_axisDef.Interlaced != null)
					{
						this.m_interlaced = new ReportBoolProperty(this.m_axisDef.Interlaced);
					}
				}
				return this.m_interlaced;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x0005CD80 File Offset: 0x0005AF80
		public ReportColorProperty InterlacedColor
		{
			get
			{
				if (this.m_interlacedColor == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.InterlacedColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo interlacedColor = this.m_axisDef.InterlacedColor;
					this.m_interlacedColor = new ReportColorProperty(interlacedColor.IsExpression, interlacedColor.OriginalText, interlacedColor.IsExpression ? null : new ReportColor(interlacedColor.StringValue.Trim(), true), interlacedColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_interlacedColor;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0005CE10 File Offset: 0x0005B010
		public ReportBoolProperty LogScale
		{
			get
			{
				if (this.m_logScale == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_logScale = new ReportBoolProperty(new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo
						{
							BoolValue = this.m_renderAxisDef.LogScale
						});
					}
					else if (this.m_axisDef.LogScale != null)
					{
						this.m_logScale = new ReportBoolProperty(this.m_axisDef.LogScale);
					}
				}
				return this.m_logScale;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0005CE80 File Offset: 0x0005B080
		public ReportDoubleProperty LogBase
		{
			get
			{
				if (this.m_logBase == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.LogBase != null)
				{
					this.m_logBase = new ReportDoubleProperty(this.m_axisDef.LogBase);
				}
				return this.m_logBase;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
		public ReportBoolProperty HideLabels
		{
			get
			{
				if (this.m_hideLabels == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.HideLabels != null)
				{
					this.m_hideLabels = new ReportBoolProperty(this.m_axisDef.HideLabels);
				}
				return this.m_hideLabels;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0005CF00 File Offset: 0x0005B100
		public ReportDoubleProperty Angle
		{
			get
			{
				if (this.m_angle == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						return null;
					}
					if (this.m_axisDef.Angle != null)
					{
						this.m_angle = new ReportDoubleProperty(this.m_axisDef.Angle);
					}
				}
				return this.m_angle;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0005CF50 File Offset: 0x0005B150
		public ReportEnumProperty<ChartAxisArrow> Arrows
		{
			get
			{
				if (this.m_arrows == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.Arrows != null)
				{
					this.m_arrows = new ReportEnumProperty<ChartAxisArrow>(this.m_axisDef.Arrows.IsExpression, this.m_axisDef.Arrows.OriginalText, EnumTranslator.TranslateChartAxisArrow(this.m_axisDef.Arrows.StringValue, null));
				}
				return this.m_arrows;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0005CFC8 File Offset: 0x0005B1C8
		public ReportBoolProperty PreventFontShrink
		{
			get
			{
				if (this.m_preventFontShrink == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_preventFontShrink = new ReportBoolProperty();
					}
					else if (this.m_axisDef.PreventFontShrink != null)
					{
						this.m_preventFontShrink = new ReportBoolProperty(this.m_axisDef.PreventFontShrink);
					}
				}
				return this.m_preventFontShrink;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0005D020 File Offset: 0x0005B220
		public ReportBoolProperty PreventFontGrow
		{
			get
			{
				if (this.m_preventFontGrow == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_preventFontGrow = new ReportBoolProperty();
					}
					else if (this.m_axisDef.PreventFontGrow != null)
					{
						this.m_preventFontGrow = new ReportBoolProperty(this.m_axisDef.PreventFontGrow);
					}
				}
				return this.m_preventFontGrow;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0005D078 File Offset: 0x0005B278
		public ReportBoolProperty PreventLabelOffset
		{
			get
			{
				if (this.m_preventLabelOffset == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_preventLabelOffset = new ReportBoolProperty();
					}
					else if (this.m_axisDef.PreventLabelOffset != null)
					{
						this.m_preventLabelOffset = new ReportBoolProperty(this.m_axisDef.PreventLabelOffset);
					}
				}
				return this.m_preventLabelOffset;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		public ReportBoolProperty PreventWordWrap
		{
			get
			{
				if (this.m_preventWordWrap == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_preventWordWrap = new ReportBoolProperty();
					}
					else if (this.m_axisDef.PreventWordWrap != null)
					{
						this.m_preventWordWrap = new ReportBoolProperty(this.m_axisDef.PreventWordWrap);
					}
				}
				return this.m_preventWordWrap;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0005D128 File Offset: 0x0005B328
		public ReportEnumProperty<ChartAxisLabelRotation> AllowLabelRotation
		{
			get
			{
				if (this.m_allowLabelRotation == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.AllowLabelRotation != null)
				{
					this.m_allowLabelRotation = new ReportEnumProperty<ChartAxisLabelRotation>(this.m_axisDef.AllowLabelRotation.IsExpression, this.m_axisDef.AllowLabelRotation.OriginalText, EnumTranslator.TranslateChartAxisLabelRotation(this.m_axisDef.AllowLabelRotation.StringValue, null));
				}
				return this.m_allowLabelRotation;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0005D19E File Offset: 0x0005B39E
		public ReportBoolProperty IncludeZero
		{
			get
			{
				if (this.m_includeZero == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.IncludeZero != null)
				{
					this.m_includeZero = new ReportBoolProperty(this.m_axisDef.IncludeZero);
				}
				return this.m_includeZero;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		public ReportBoolProperty LabelsAutoFitDisabled
		{
			get
			{
				if (this.m_labelsAutoFitDisabled == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_labelsAutoFitDisabled = new ReportBoolProperty();
					}
					else if (this.m_axisDef.LabelsAutoFitDisabled != null)
					{
						this.m_labelsAutoFitDisabled = new ReportBoolProperty(this.m_axisDef.LabelsAutoFitDisabled);
					}
				}
				return this.m_labelsAutoFitDisabled;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0005D238 File Offset: 0x0005B438
		public ReportSizeProperty MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.MinFontSize != null)
				{
					this.m_minFontSize = new ReportSizeProperty(this.m_axisDef.MinFontSize);
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0005D278 File Offset: 0x0005B478
		public ReportSizeProperty MaxFontSize
		{
			get
			{
				if (this.m_maxFontSize == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.MaxFontSize != null)
				{
					this.m_maxFontSize = new ReportSizeProperty(this.m_axisDef.MaxFontSize);
				}
				return this.m_maxFontSize;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		public ReportBoolProperty OffsetLabels
		{
			get
			{
				if (this.m_offsetLabels == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.OffsetLabels != null)
				{
					this.m_offsetLabels = new ReportBoolProperty(this.m_axisDef.OffsetLabels);
				}
				return this.m_offsetLabels;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0005D2F8 File Offset: 0x0005B4F8
		public ReportBoolProperty HideEndLabels
		{
			get
			{
				if (this.m_hideEndLabels == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.HideEndLabels != null)
				{
					this.m_hideEndLabels = new ReportBoolProperty(this.m_axisDef.HideEndLabels);
				}
				return this.m_hideEndLabels;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0005D338 File Offset: 0x0005B538
		public ChartAxisScaleBreak AxisScaleBreak
		{
			get
			{
				if (this.m_axisScaleBreak == null && !this.m_chart.IsOldSnapshot && this.m_axisDef.AxisScaleBreak != null)
				{
					this.m_axisScaleBreak = new ChartAxisScaleBreak(this.m_axisDef.AxisScaleBreak, this.m_chart);
				}
				return this.m_axisScaleBreak;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0005D389 File Offset: 0x0005B589
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0005D391 File Offset: 0x0005B591
		internal ChartAxis AxisDef
		{
			get
			{
				return this.m_axisDef;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0005D399 File Offset: 0x0005B599
		internal AxisInstance RenderAxisInstance
		{
			get
			{
				return this.m_renderAxisInstance;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0005D3A1 File Offset: 0x0005B5A1
		public ChartAxisInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartAxisInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_majorGridlines != null)
			{
				this.m_majorGridlines.SetNewContext();
			}
			if (this.m_minorGridlines != null)
			{
				this.m_minorGridlines.SetNewContext();
			}
			if (this.m_title != null)
			{
				this.m_title.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_chartStripLines != null)
			{
				this.m_chartStripLines.SetNewContext();
			}
			if (this.m_majorTickMarks != null)
			{
				this.m_majorTickMarks.SetNewContext();
			}
			if (this.m_minorTickMarks != null)
			{
				this.m_minorTickMarks.SetNewContext();
			}
			if (this.m_axisScaleBreak != null)
			{
				this.m_axisScaleBreak.SetNewContext();
			}
			this.m_customPropertiesReady = false;
		}

		// Token: 0x04000B29 RID: 2857
		private ChartGridLines m_majorGridlines;

		// Token: 0x04000B2A RID: 2858
		private ChartGridLines m_minorGridlines;

		// Token: 0x04000B2B RID: 2859
		private ReportVariantProperty m_crossAt;

		// Token: 0x04000B2C RID: 2860
		private ReportVariantProperty m_min;

		// Token: 0x04000B2D RID: 2861
		private ReportVariantProperty m_max;

		// Token: 0x04000B2E RID: 2862
		private ChartAxisTitle m_title;

		// Token: 0x04000B2F RID: 2863
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000B30 RID: 2864
		private bool m_isCategory;

		// Token: 0x04000B31 RID: 2865
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000B32 RID: 2866
		private AxisInstance m_renderAxisInstance;

		// Token: 0x04000B33 RID: 2867
		private Axis m_renderAxisDef;

		// Token: 0x04000B34 RID: 2868
		private ChartAxis m_axisDef;

		// Token: 0x04000B35 RID: 2869
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000B36 RID: 2870
		private bool m_customPropertiesReady;

		// Token: 0x04000B37 RID: 2871
		private ChartStripLineCollection m_chartStripLines;

		// Token: 0x04000B38 RID: 2872
		private ReportEnumProperty<ChartAutoBool> m_visible;

		// Token: 0x04000B39 RID: 2873
		private ReportEnumProperty<ChartAutoBool> m_margin;

		// Token: 0x04000B3A RID: 2874
		private ReportDoubleProperty m_interval;

		// Token: 0x04000B3B RID: 2875
		private ReportEnumProperty<ChartIntervalType> m_intervalType;

		// Token: 0x04000B3C RID: 2876
		private ReportDoubleProperty m_intervalOffset;

		// Token: 0x04000B3D RID: 2877
		private ReportEnumProperty<ChartIntervalType> m_intervalOffsetType;

		// Token: 0x04000B3E RID: 2878
		private ReportDoubleProperty m_labelInterval;

		// Token: 0x04000B3F RID: 2879
		private ReportEnumProperty<ChartIntervalType> m_labelIntervalType;

		// Token: 0x04000B40 RID: 2880
		private ReportDoubleProperty m_labelIntervalOffset;

		// Token: 0x04000B41 RID: 2881
		private ReportEnumProperty<ChartIntervalType> m_labelIntervalOffsetType;

		// Token: 0x04000B42 RID: 2882
		private ReportBoolProperty m_variableAutoInterval;

		// Token: 0x04000B43 RID: 2883
		private ChartTickMarks m_majorTickMarks;

		// Token: 0x04000B44 RID: 2884
		private ChartTickMarks m_minorTickMarks;

		// Token: 0x04000B45 RID: 2885
		private ReportBoolProperty m_marksAlwaysAtPlotEdge;

		// Token: 0x04000B46 RID: 2886
		private ReportBoolProperty m_reverse;

		// Token: 0x04000B47 RID: 2887
		private ReportEnumProperty<ChartAxisLocation> m_location;

		// Token: 0x04000B48 RID: 2888
		private ReportBoolProperty m_interlaced;

		// Token: 0x04000B49 RID: 2889
		private ReportColorProperty m_interlacedColor;

		// Token: 0x04000B4A RID: 2890
		private ReportBoolProperty m_logScale;

		// Token: 0x04000B4B RID: 2891
		private ReportDoubleProperty m_logBase;

		// Token: 0x04000B4C RID: 2892
		private ReportBoolProperty m_hideLabels;

		// Token: 0x04000B4D RID: 2893
		private ReportDoubleProperty m_angle;

		// Token: 0x04000B4E RID: 2894
		private ReportBoolProperty m_preventFontShrink;

		// Token: 0x04000B4F RID: 2895
		private ReportBoolProperty m_preventFontGrow;

		// Token: 0x04000B50 RID: 2896
		private ReportBoolProperty m_preventLabelOffset;

		// Token: 0x04000B51 RID: 2897
		private ReportBoolProperty m_preventWordWrap;

		// Token: 0x04000B52 RID: 2898
		private ReportEnumProperty<ChartAxisLabelRotation> m_allowLabelRotation;

		// Token: 0x04000B53 RID: 2899
		private ReportBoolProperty m_includeZero;

		// Token: 0x04000B54 RID: 2900
		private ReportBoolProperty m_labelsAutoFitDisabled;

		// Token: 0x04000B55 RID: 2901
		private ReportSizeProperty m_minFontSize;

		// Token: 0x04000B56 RID: 2902
		private ReportSizeProperty m_maxFontSize;

		// Token: 0x04000B57 RID: 2903
		private ReportBoolProperty m_offsetLabels;

		// Token: 0x04000B58 RID: 2904
		private ReportBoolProperty m_hideEndLabels;

		// Token: 0x04000B59 RID: 2905
		private ReportEnumProperty<ChartAxisArrow> m_arrows;

		// Token: 0x04000B5A RID: 2906
		private ChartAxisScaleBreak m_axisScaleBreak;

		// Token: 0x0200093F RID: 2367
		public enum TickMarks
		{
			// Token: 0x04004020 RID: 16416
			None,
			// Token: 0x04004021 RID: 16417
			Inside,
			// Token: 0x04004022 RID: 16418
			Outside,
			// Token: 0x04004023 RID: 16419
			Cross
		}

		// Token: 0x02000940 RID: 2368
		public enum Locations
		{
			// Token: 0x04004025 RID: 16421
			Default,
			// Token: 0x04004026 RID: 16422
			Opposite
		}
	}
}
