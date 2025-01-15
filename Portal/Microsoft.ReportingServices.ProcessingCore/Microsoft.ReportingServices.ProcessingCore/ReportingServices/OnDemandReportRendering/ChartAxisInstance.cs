using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000271 RID: 625
	public sealed class ChartAxisInstance : BaseInstance
	{
		// Token: 0x06001838 RID: 6200 RVA: 0x0006382A File Offset: 0x00061A2A
		internal ChartAxisInstance(ChartAxis axisDef)
			: base(axisDef.ChartDef)
		{
			this.m_axisDef = axisDef;
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0006383F File Offset: 0x00061A3F
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_axisDef, this.m_axisDef.ChartDef, this.m_axisDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0006387C File Offset: 0x00061A7C
		public object CrossAt
		{
			get
			{
				if (this.m_crossAt == null)
				{
					if (this.m_axisDef.ChartDef.IsOldSnapshot)
					{
						this.m_crossAt = this.m_axisDef.RenderAxisInstance.CrossAtValue;
					}
					else
					{
						this.m_crossAt = this.m_axisDef.AxisDef.EvaluateCrossAt(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_crossAt;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x000638F4 File Offset: 0x00061AF4
		public object Minimum
		{
			get
			{
				if (this.m_min == null)
				{
					if (this.m_axisDef.ChartDef.IsOldSnapshot)
					{
						this.m_min = this.m_axisDef.RenderAxisInstance.MinValue;
					}
					else
					{
						this.m_min = this.m_axisDef.AxisDef.EvaluateMin(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_min;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0006396C File Offset: 0x00061B6C
		public object Maximum
		{
			get
			{
				if (this.m_max == null)
				{
					if (this.m_axisDef.ChartDef.IsOldSnapshot)
					{
						this.m_max = this.m_axisDef.RenderAxisInstance.MaxValue;
					}
					else
					{
						this.m_max = this.m_axisDef.AxisDef.EvaluateMax(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_max;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000639E4 File Offset: 0x00061BE4
		public ChartAutoBool Visible
		{
			get
			{
				if (this.m_visible == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					string text = this.m_axisDef.AxisDef.EvaluateVisible(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext);
					this.m_visible = new ChartAutoBool?(EnumTranslator.TranslateChartAutoBool(text, this.m_axisDef.ChartDef.RenderingContext.OdpContext.ReportRuntime));
				}
				return this.m_visible.Value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00063A74 File Offset: 0x00061C74
		public ChartAutoBool Margin
		{
			get
			{
				if (this.m_margin == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					string text = this.m_axisDef.AxisDef.EvaluateMargin(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext);
					this.m_margin = new ChartAutoBool?(EnumTranslator.TranslateChartAutoBool(text, this.m_axisDef.ChartDef.RenderingContext.OdpContext.ReportRuntime));
				}
				return this.m_margin.Value;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00063B04 File Offset: 0x00061D04
		public double Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_interval = new double?(this.m_axisDef.AxisDef.EvaluateInterval(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00063B74 File Offset: 0x00061D74
		public ChartIntervalType IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalType = new ChartIntervalType?(this.m_axisDef.AxisDef.EvaluateIntervalType(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalType.Value;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00063BE4 File Offset: 0x00061DE4
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffset = new double?(this.m_axisDef.AxisDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00063C54 File Offset: 0x00061E54
		public ChartIntervalType IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffsetType = new ChartIntervalType?(this.m_axisDef.AxisDef.EvaluateIntervalOffsetType(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffsetType.Value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00063CC4 File Offset: 0x00061EC4
		public double LabelInterval
		{
			get
			{
				if (this.m_labelInterval == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_labelInterval = new double?(this.m_axisDef.AxisDef.EvaluateLabelInterval(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_labelInterval.Value;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00063D34 File Offset: 0x00061F34
		public ChartIntervalType LabelIntervalType
		{
			get
			{
				if (this.m_labelIntervalType == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_labelIntervalType = new ChartIntervalType?(this.m_axisDef.AxisDef.EvaluateLabelIntervalType(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_labelIntervalType.Value;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00063DA4 File Offset: 0x00061FA4
		public double LabelIntervalOffset
		{
			get
			{
				if (this.m_labelIntervalOffset == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_labelIntervalOffset = new double?(this.m_axisDef.AxisDef.EvaluateLabelIntervalOffset(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_labelIntervalOffset.Value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00063E14 File Offset: 0x00062014
		public ChartIntervalType LabelIntervalOffsetType
		{
			get
			{
				if (this.m_labelIntervalOffsetType == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_labelIntervalOffsetType = new ChartIntervalType?(this.m_axisDef.AxisDef.EvaluateLabelIntervalOffsetType(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_labelIntervalOffsetType.Value;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00063E84 File Offset: 0x00062084
		public bool VariableAutoInterval
		{
			get
			{
				if (this.m_variableAutoInterval == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_variableAutoInterval = new bool?(this.m_axisDef.AxisDef.EvaluateVariableAutoInterval(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_variableAutoInterval.Value;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00063EF4 File Offset: 0x000620F4
		public bool MarksAlwaysAtPlotEdge
		{
			get
			{
				if (this.m_marksAlwaysAtPlotEdge == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_marksAlwaysAtPlotEdge = new bool?(this.m_axisDef.AxisDef.EvaluateMarksAlwaysAtPlotEdge(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_marksAlwaysAtPlotEdge.Value;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00063F64 File Offset: 0x00062164
		public bool Reverse
		{
			get
			{
				if (this.m_reverse == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_reverse = new bool?(this.m_axisDef.AxisDef.EvaluateReverse(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_reverse.Value;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00063FD4 File Offset: 0x000621D4
		public ChartAxisLocation Location
		{
			get
			{
				if (this.m_location == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_location = new ChartAxisLocation?(this.m_axisDef.AxisDef.EvaluateLocation(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_location.Value;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00064044 File Offset: 0x00062244
		public bool Interlaced
		{
			get
			{
				if (this.m_interlaced == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_interlaced = new bool?(this.m_axisDef.AxisDef.EvaluateInterlaced(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interlaced.Value;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x000640B4 File Offset: 0x000622B4
		public ReportColor InterlacedColor
		{
			get
			{
				if (this.m_interlacedColor == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_interlacedColor = new ReportColor(this.m_axisDef.AxisDef.EvaluateInterlacedColor(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_interlacedColor;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00064118 File Offset: 0x00062318
		public bool LogScale
		{
			get
			{
				if (this.m_logScale == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_logScale = new bool?(this.m_axisDef.AxisDef.EvaluateLogScale(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_logScale.Value;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00064188 File Offset: 0x00062388
		public double LogBase
		{
			get
			{
				if (this.m_logBase == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_logBase = new double?(this.m_axisDef.AxisDef.EvaluateLogBase(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_logBase.Value;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000641F8 File Offset: 0x000623F8
		public bool HideLabels
		{
			get
			{
				if (this.m_hideLabels == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_hideLabels = new bool?(this.m_axisDef.AxisDef.EvaluateHideLabels(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hideLabels.Value;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00064268 File Offset: 0x00062468
		public double Angle
		{
			get
			{
				if (this.m_angle == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_angle = new double?(this.m_axisDef.AxisDef.EvaluateAngle(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_angle.Value;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x000642D8 File Offset: 0x000624D8
		public ChartAxisArrow Arrows
		{
			get
			{
				if (this.m_arrows == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_arrows = new ChartAxisArrow?(this.m_axisDef.AxisDef.EvaluateArrows(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_arrows.Value;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00064348 File Offset: 0x00062548
		public bool PreventFontShrink
		{
			get
			{
				if (this.m_preventFontShrink == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_preventFontShrink = new bool?(this.m_axisDef.AxisDef.EvaluatePreventFontShrink(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_preventFontShrink.Value;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000643B8 File Offset: 0x000625B8
		public bool PreventFontGrow
		{
			get
			{
				if (this.m_preventFontGrow == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_preventFontGrow = new bool?(this.m_axisDef.AxisDef.EvaluatePreventFontGrow(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_preventFontGrow.Value;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00064428 File Offset: 0x00062628
		public bool PreventLabelOffset
		{
			get
			{
				if (this.m_preventLabelOffset == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_preventLabelOffset = new bool?(this.m_axisDef.AxisDef.EvaluatePreventLabelOffset(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_preventLabelOffset.Value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00064498 File Offset: 0x00062698
		public bool PreventWordWrap
		{
			get
			{
				if (this.m_preventWordWrap == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_preventWordWrap = new bool?(this.m_axisDef.AxisDef.EvaluatePreventWordWrap(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_preventWordWrap.Value;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00064508 File Offset: 0x00062708
		public ChartAxisLabelRotation AllowLabelRotation
		{
			get
			{
				if (this.m_allowLabelRotation == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_allowLabelRotation = new ChartAxisLabelRotation?(this.m_axisDef.AxisDef.EvaluateAllowLabelRotation(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_allowLabelRotation.Value;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00064578 File Offset: 0x00062778
		public bool IncludeZero
		{
			get
			{
				if (this.m_includeZero == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_includeZero = new bool?(this.m_axisDef.AxisDef.EvaluateIncludeZero(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_includeZero.Value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000645E8 File Offset: 0x000627E8
		public bool LabelsAutoFitDisabled
		{
			get
			{
				if (this.m_labelsAutoFitDisabled == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_labelsAutoFitDisabled = new bool?(this.m_axisDef.AxisDef.EvaluateLabelsAutoFitDisabled(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_labelsAutoFitDisabled.Value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x00064658 File Offset: 0x00062858
		public ReportSize MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_minFontSize = new ReportSize(this.m_axisDef.AxisDef.EvaluateMinFontSize(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000646BC File Offset: 0x000628BC
		public ReportSize MaxFontSize
		{
			get
			{
				if (this.m_maxFontSize == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_maxFontSize = new ReportSize(this.m_axisDef.AxisDef.EvaluateMaxFontSize(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_maxFontSize;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00064720 File Offset: 0x00062920
		public bool OffsetLabels
		{
			get
			{
				if (this.m_offsetLabels == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_offsetLabels = new bool?(this.m_axisDef.AxisDef.EvaluateOffsetLabels(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_offsetLabels.Value;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00064790 File Offset: 0x00062990
		public bool HideEndLabels
		{
			get
			{
				if (this.m_hideEndLabels == null && !this.m_axisDef.ChartDef.IsOldSnapshot)
				{
					this.m_hideEndLabels = new bool?(this.m_axisDef.AxisDef.EvaluateHideEndLabels(this.ReportScopeInstance, this.m_axisDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hideEndLabels.Value;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00064800 File Offset: 0x00062A00
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_min = null;
			this.m_max = null;
			this.m_crossAt = null;
			this.m_visible = null;
			this.m_margin = null;
			this.m_interval = null;
			this.m_intervalType = null;
			this.m_intervalOffset = null;
			this.m_intervalOffsetType = null;
			this.m_labelInterval = null;
			this.m_labelIntervalType = null;
			this.m_labelIntervalOffset = null;
			this.m_labelIntervalOffsetType = null;
			this.m_variableAutoInterval = null;
			this.m_marksAlwaysAtPlotEdge = null;
			this.m_reverse = null;
			this.m_location = null;
			this.m_interlaced = null;
			this.m_interlacedColor = null;
			this.m_logScale = null;
			this.m_logBase = null;
			this.m_hideLabels = null;
			this.m_angle = null;
			this.m_arrows = null;
			this.m_preventFontShrink = null;
			this.m_preventFontGrow = null;
			this.m_preventLabelOffset = null;
			this.m_preventWordWrap = null;
			this.m_allowLabelRotation = null;
			this.m_includeZero = null;
			this.m_labelsAutoFitDisabled = null;
			this.m_minFontSize = null;
			this.m_maxFontSize = null;
			this.m_offsetLabels = null;
			this.m_hideEndLabels = null;
		}

		// Token: 0x04000C47 RID: 3143
		private ChartAxis m_axisDef;

		// Token: 0x04000C48 RID: 3144
		private StyleInstance m_style;

		// Token: 0x04000C49 RID: 3145
		private object m_min;

		// Token: 0x04000C4A RID: 3146
		private object m_max;

		// Token: 0x04000C4B RID: 3147
		private object m_crossAt;

		// Token: 0x04000C4C RID: 3148
		private ChartAutoBool? m_visible;

		// Token: 0x04000C4D RID: 3149
		private ChartAutoBool? m_margin;

		// Token: 0x04000C4E RID: 3150
		private double? m_interval;

		// Token: 0x04000C4F RID: 3151
		private ChartIntervalType? m_intervalType;

		// Token: 0x04000C50 RID: 3152
		private double? m_intervalOffset;

		// Token: 0x04000C51 RID: 3153
		private ChartIntervalType? m_intervalOffsetType;

		// Token: 0x04000C52 RID: 3154
		private double? m_labelInterval;

		// Token: 0x04000C53 RID: 3155
		private ChartIntervalType? m_labelIntervalType;

		// Token: 0x04000C54 RID: 3156
		private double? m_labelIntervalOffset;

		// Token: 0x04000C55 RID: 3157
		private ChartIntervalType? m_labelIntervalOffsetType;

		// Token: 0x04000C56 RID: 3158
		private bool? m_variableAutoInterval;

		// Token: 0x04000C57 RID: 3159
		private bool? m_marksAlwaysAtPlotEdge;

		// Token: 0x04000C58 RID: 3160
		private bool? m_reverse;

		// Token: 0x04000C59 RID: 3161
		private ChartAxisLocation? m_location;

		// Token: 0x04000C5A RID: 3162
		private bool? m_interlaced;

		// Token: 0x04000C5B RID: 3163
		private ReportColor m_interlacedColor;

		// Token: 0x04000C5C RID: 3164
		private bool? m_logScale;

		// Token: 0x04000C5D RID: 3165
		private double? m_logBase;

		// Token: 0x04000C5E RID: 3166
		private bool? m_hideLabels;

		// Token: 0x04000C5F RID: 3167
		private double? m_angle;

		// Token: 0x04000C60 RID: 3168
		private bool? m_preventFontShrink;

		// Token: 0x04000C61 RID: 3169
		private bool? m_preventFontGrow;

		// Token: 0x04000C62 RID: 3170
		private bool? m_preventLabelOffset;

		// Token: 0x04000C63 RID: 3171
		private bool? m_preventWordWrap;

		// Token: 0x04000C64 RID: 3172
		private ChartAxisLabelRotation? m_allowLabelRotation;

		// Token: 0x04000C65 RID: 3173
		private bool? m_includeZero;

		// Token: 0x04000C66 RID: 3174
		private bool? m_labelsAutoFitDisabled;

		// Token: 0x04000C67 RID: 3175
		private ReportSize m_minFontSize;

		// Token: 0x04000C68 RID: 3176
		private ReportSize m_maxFontSize;

		// Token: 0x04000C69 RID: 3177
		private bool? m_offsetLabels;

		// Token: 0x04000C6A RID: 3178
		private bool? m_hideEndLabels;

		// Token: 0x04000C6B RID: 3179
		private ChartAxisArrow? m_arrows;
	}
}
