using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025F RID: 607
	public sealed class ChartSmartLabelInstance : BaseInstance
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x0005FF8A File Offset: 0x0005E18A
		internal ChartSmartLabelInstance(ChartSmartLabel chartSmartLabelDef)
			: base(chartSmartLabelDef.ReportScope)
		{
			this.m_chartSmartLabelDef = chartSmartLabelDef;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0005FFA0 File Offset: 0x0005E1A0
		public ChartAllowOutsideChartArea AllowOutSidePlotArea
		{
			get
			{
				if (this.m_allowOutSidePlotArea == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_allowOutSidePlotArea = new ChartAllowOutsideChartArea?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateAllowOutSidePlotArea(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_allowOutSidePlotArea.Value;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00060010 File Offset: 0x0005E210
		public ReportColor CalloutBackColor
		{
			get
			{
				if (this.m_calloutBackColor == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutBackColor = new ReportColor(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutBackColor(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_calloutBackColor;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00060074 File Offset: 0x0005E274
		public ChartCalloutLineAnchor CalloutLineAnchor
		{
			get
			{
				if (this.m_calloutLineAnchor == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutLineAnchor = new ChartCalloutLineAnchor?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutLineAnchor(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_calloutLineAnchor.Value;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x000600E4 File Offset: 0x0005E2E4
		public ReportColor CalloutLineColor
		{
			get
			{
				if (this.m_calloutLineColor == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutLineColor = new ReportColor(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutLineColor(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_calloutLineColor;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00060148 File Offset: 0x0005E348
		public ChartCalloutLineStyle CalloutLineStyle
		{
			get
			{
				if (this.m_calloutLineStyle == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutLineStyle = new ChartCalloutLineStyle?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutLineStyle(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_calloutLineStyle.Value;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000601B8 File Offset: 0x0005E3B8
		public ReportSize CalloutLineWidth
		{
			get
			{
				if (this.m_calloutLineWidth == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutLineWidth = new ReportSize(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutLineWidth(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_calloutLineWidth;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0006021C File Offset: 0x0005E41C
		public ChartCalloutStyle CalloutStyle
		{
			get
			{
				if (this.m_calloutStyle == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_calloutStyle = new ChartCalloutStyle?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateCalloutStyle(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_calloutStyle.Value;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0006028C File Offset: 0x0005E48C
		public bool ShowOverlapped
		{
			get
			{
				if (this.m_showOverlapped == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_showOverlapped = new bool?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateShowOverlapped(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_showOverlapped.Value;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000602FC File Offset: 0x0005E4FC
		public bool MarkerOverlapping
		{
			get
			{
				if (this.m_markerOverlapping == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_markerOverlapping = new bool?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateMarkerOverlapping(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_markerOverlapping.Value;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0006036C File Offset: 0x0005E56C
		public bool Disabled
		{
			get
			{
				if (this.m_disabled == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_disabled = new bool?(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateDisabled(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_disabled.Value;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000603DC File Offset: 0x0005E5DC
		public ReportSize MaxMovingDistance
		{
			get
			{
				if (this.m_maxMovingDistance == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_maxMovingDistance = new ReportSize(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateMaxMovingDistance(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_maxMovingDistance;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00060440 File Offset: 0x0005E640
		public ReportSize MinMovingDistance
		{
			get
			{
				if (this.m_minMovingDistance == null && !this.m_chartSmartLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_minMovingDistance = new ReportSize(this.m_chartSmartLabelDef.ChartSmartLabelDef.EvaluateMinMovingDistance(this.ReportScopeInstance, this.m_chartSmartLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_minMovingDistance;
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000604A4 File Offset: 0x0005E6A4
		protected override void ResetInstanceCache()
		{
			this.m_allowOutSidePlotArea = null;
			this.m_calloutBackColor = null;
			this.m_calloutLineAnchor = null;
			this.m_calloutLineColor = null;
			this.m_calloutLineStyle = null;
			this.m_calloutLineWidth = null;
			this.m_calloutStyle = null;
			this.m_showOverlapped = null;
			this.m_markerOverlapping = null;
			this.m_maxMovingDistance = null;
			this.m_minMovingDistance = null;
			this.m_disabled = null;
		}

		// Token: 0x04000BB9 RID: 3001
		private ChartSmartLabel m_chartSmartLabelDef;

		// Token: 0x04000BBA RID: 3002
		private ChartAllowOutsideChartArea? m_allowOutSidePlotArea;

		// Token: 0x04000BBB RID: 3003
		private ReportColor m_calloutBackColor;

		// Token: 0x04000BBC RID: 3004
		private ChartCalloutLineAnchor? m_calloutLineAnchor;

		// Token: 0x04000BBD RID: 3005
		private ReportColor m_calloutLineColor;

		// Token: 0x04000BBE RID: 3006
		private ChartCalloutLineStyle? m_calloutLineStyle;

		// Token: 0x04000BBF RID: 3007
		private ReportSize m_calloutLineWidth;

		// Token: 0x04000BC0 RID: 3008
		private ChartCalloutStyle? m_calloutStyle;

		// Token: 0x04000BC1 RID: 3009
		private bool? m_showOverlapped;

		// Token: 0x04000BC2 RID: 3010
		private bool? m_markerOverlapping;

		// Token: 0x04000BC3 RID: 3011
		private ReportSize m_maxMovingDistance;

		// Token: 0x04000BC4 RID: 3012
		private ReportSize m_minMovingDistance;

		// Token: 0x04000BC5 RID: 3013
		private bool? m_disabled;
	}
}
