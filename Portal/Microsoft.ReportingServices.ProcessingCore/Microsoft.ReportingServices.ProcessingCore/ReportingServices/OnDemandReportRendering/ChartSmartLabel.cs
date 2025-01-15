using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023E RID: 574
	public sealed class ChartSmartLabel
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x00058E8A File Offset: 0x0005708A
		internal ChartSmartLabel(InternalChartSeries chartSeries, ChartSmartLabel chartSmartLabelDef, Chart chart)
		{
			this.m_chartSeries = chartSeries;
			this.m_chartSmartLabelDef = chartSmartLabelDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00058EA8 File Offset: 0x000570A8
		public ReportEnumProperty<ChartAllowOutsideChartArea> AllowOutSidePlotArea
		{
			get
			{
				if (this.m_allowOutSidePlotArea == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_allowOutSidePlotArea = new ReportEnumProperty<ChartAllowOutsideChartArea>(ChartAllowOutsideChartArea.False);
					}
					else if (this.m_chartSmartLabelDef.AllowOutSidePlotArea != null)
					{
						this.m_allowOutSidePlotArea = new ReportEnumProperty<ChartAllowOutsideChartArea>(this.m_chartSmartLabelDef.AllowOutSidePlotArea.IsExpression, this.m_chartSmartLabelDef.AllowOutSidePlotArea.OriginalText, EnumTranslator.TranslateChartAllowOutsideChartArea(this.m_chartSmartLabelDef.AllowOutSidePlotArea.StringValue, null));
					}
				}
				return this.m_allowOutSidePlotArea;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00058F2C File Offset: 0x0005712C
		public ReportColorProperty CalloutBackColor
		{
			get
			{
				if (this.m_calloutBackColor == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutBackColor != null)
				{
					ExpressionInfo calloutBackColor = this.m_chartSmartLabelDef.CalloutBackColor;
					this.m_calloutBackColor = new ReportColorProperty(calloutBackColor.IsExpression, calloutBackColor.OriginalText, calloutBackColor.IsExpression ? null : new ReportColor(calloutBackColor.StringValue.Trim(), true), calloutBackColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_calloutBackColor;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00058FBC File Offset: 0x000571BC
		public ReportEnumProperty<ChartCalloutLineAnchor> CalloutLineAnchor
		{
			get
			{
				if (this.m_calloutLineAnchor == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutLineAnchor != null)
				{
					this.m_calloutLineAnchor = new ReportEnumProperty<ChartCalloutLineAnchor>(this.m_chartSmartLabelDef.CalloutLineAnchor.IsExpression, this.m_chartSmartLabelDef.CalloutLineAnchor.OriginalText, EnumTranslator.TranslateChartCalloutLineAnchor(this.m_chartSmartLabelDef.CalloutLineAnchor.StringValue, null));
				}
				return this.m_calloutLineAnchor;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00059034 File Offset: 0x00057234
		public ReportColorProperty CalloutLineColor
		{
			get
			{
				if (this.m_calloutLineColor == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutLineColor != null)
				{
					ExpressionInfo calloutLineColor = this.m_chartSmartLabelDef.CalloutLineColor;
					this.m_calloutLineColor = new ReportColorProperty(calloutLineColor.IsExpression, calloutLineColor.OriginalText, calloutLineColor.IsExpression ? null : new ReportColor(calloutLineColor.StringValue.Trim(), true), calloutLineColor.IsExpression ? new ReportColor("", Color.Black, true) : null);
				}
				return this.m_calloutLineColor;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000590C4 File Offset: 0x000572C4
		public ReportEnumProperty<ChartCalloutLineStyle> CalloutLineStyle
		{
			get
			{
				if (this.m_calloutLineStyle == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutLineStyle != null)
				{
					this.m_calloutLineStyle = new ReportEnumProperty<ChartCalloutLineStyle>(this.m_chartSmartLabelDef.CalloutLineStyle.IsExpression, this.m_chartSmartLabelDef.CalloutLineStyle.OriginalText, EnumTranslator.TranslateChartCalloutLineStyle(this.m_chartSmartLabelDef.CalloutLineStyle.StringValue, null));
				}
				return this.m_calloutLineStyle;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0005913A File Offset: 0x0005733A
		public ReportSizeProperty CalloutLineWidth
		{
			get
			{
				if (this.m_calloutLineWidth == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutLineWidth != null)
				{
					this.m_calloutLineWidth = new ReportSizeProperty(this.m_chartSmartLabelDef.CalloutLineWidth);
				}
				return this.m_calloutLineWidth;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0005917C File Offset: 0x0005737C
		public ReportEnumProperty<ChartCalloutStyle> CalloutStyle
		{
			get
			{
				if (this.m_calloutStyle == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.CalloutStyle != null)
				{
					this.m_calloutStyle = new ReportEnumProperty<ChartCalloutStyle>(this.m_chartSmartLabelDef.CalloutStyle.IsExpression, this.m_chartSmartLabelDef.CalloutStyle.OriginalText, EnumTranslator.TranslateChartCalloutStyle(this.m_chartSmartLabelDef.CalloutStyle.StringValue, null));
				}
				return this.m_calloutStyle;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x000591F2 File Offset: 0x000573F2
		public ReportBoolProperty ShowOverlapped
		{
			get
			{
				if (this.m_showOverlapped == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.ShowOverlapped != null)
				{
					this.m_showOverlapped = new ReportBoolProperty(this.m_chartSmartLabelDef.ShowOverlapped);
				}
				return this.m_showOverlapped;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00059232 File Offset: 0x00057432
		public ReportBoolProperty MarkerOverlapping
		{
			get
			{
				if (this.m_markerOverlapping == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.MarkerOverlapping != null)
				{
					this.m_markerOverlapping = new ReportBoolProperty(this.m_chartSmartLabelDef.MarkerOverlapping);
				}
				return this.m_markerOverlapping;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00059272 File Offset: 0x00057472
		public ReportSizeProperty MaxMovingDistance
		{
			get
			{
				if (this.m_maxMovingDistance == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.MaxMovingDistance != null)
				{
					this.m_maxMovingDistance = new ReportSizeProperty(this.m_chartSmartLabelDef.MaxMovingDistance);
				}
				return this.m_maxMovingDistance;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x000592B2 File Offset: 0x000574B2
		public ReportSizeProperty MinMovingDistance
		{
			get
			{
				if (this.m_minMovingDistance == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.MinMovingDistance != null)
				{
					this.m_minMovingDistance = new ReportSizeProperty(this.m_chartSmartLabelDef.MinMovingDistance);
				}
				return this.m_minMovingDistance;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x000592F4 File Offset: 0x000574F4
		public ChartNoMoveDirections NoMoveDirections
		{
			get
			{
				if (this.m_noMoveDirections == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.NoMoveDirections != null)
				{
					this.m_noMoveDirections = new ChartNoMoveDirections(this.m_chartSeries, this.m_chartSmartLabelDef.NoMoveDirections, this.m_chart);
				}
				return this.m_noMoveDirections;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0005934B File Offset: 0x0005754B
		public ReportBoolProperty Disabled
		{
			get
			{
				if (this.m_disabled == null && !this.m_chart.IsOldSnapshot && this.m_chartSmartLabelDef.Disabled != null)
				{
					this.m_disabled = new ReportBoolProperty(this.m_chartSmartLabelDef.Disabled);
				}
				return this.m_disabled;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0005938B File Offset: 0x0005758B
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ReportScope;
				}
				return this.m_chart;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x000593A7 File Offset: 0x000575A7
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x000593AF File Offset: 0x000575AF
		internal ChartSmartLabel ChartSmartLabelDef
		{
			get
			{
				return this.m_chartSmartLabelDef;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x000593B7 File Offset: 0x000575B7
		public ChartSmartLabelInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartSmartLabelInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000593E7 File Offset: 0x000575E7
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_noMoveDirections != null)
			{
				this.m_noMoveDirections.SetNewContext();
			}
		}

		// Token: 0x04000A8D RID: 2701
		private Chart m_chart;

		// Token: 0x04000A8E RID: 2702
		private ChartSmartLabel m_chartSmartLabelDef;

		// Token: 0x04000A8F RID: 2703
		private ChartSmartLabelInstance m_instance;

		// Token: 0x04000A90 RID: 2704
		private ReportEnumProperty<ChartAllowOutsideChartArea> m_allowOutSidePlotArea;

		// Token: 0x04000A91 RID: 2705
		private ReportColorProperty m_calloutBackColor;

		// Token: 0x04000A92 RID: 2706
		private ReportEnumProperty<ChartCalloutLineAnchor> m_calloutLineAnchor;

		// Token: 0x04000A93 RID: 2707
		private ReportColorProperty m_calloutLineColor;

		// Token: 0x04000A94 RID: 2708
		private ReportEnumProperty<ChartCalloutLineStyle> m_calloutLineStyle;

		// Token: 0x04000A95 RID: 2709
		private ReportSizeProperty m_calloutLineWidth;

		// Token: 0x04000A96 RID: 2710
		private ReportEnumProperty<ChartCalloutStyle> m_calloutStyle;

		// Token: 0x04000A97 RID: 2711
		private ReportBoolProperty m_showOverlapped;

		// Token: 0x04000A98 RID: 2712
		private ReportBoolProperty m_markerOverlapping;

		// Token: 0x04000A99 RID: 2713
		private ReportSizeProperty m_maxMovingDistance;

		// Token: 0x04000A9A RID: 2714
		private ReportSizeProperty m_minMovingDistance;

		// Token: 0x04000A9B RID: 2715
		private ChartNoMoveDirections m_noMoveDirections;

		// Token: 0x04000A9C RID: 2716
		private ReportBoolProperty m_disabled;

		// Token: 0x04000A9D RID: 2717
		private InternalChartSeries m_chartSeries;
	}
}
