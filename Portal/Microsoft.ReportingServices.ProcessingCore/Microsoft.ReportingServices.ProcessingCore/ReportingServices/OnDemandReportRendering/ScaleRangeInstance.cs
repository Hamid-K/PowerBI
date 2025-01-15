using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000134 RID: 308
	public sealed class ScaleRangeInstance : BaseInstance
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x00039528 File Offset: 0x00037728
		internal ScaleRangeInstance(ScaleRange defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0003953D File Offset: 0x0003773D
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.GaugePanelDef, this.m_defObject.GaugePanelDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0003957C File Offset: 0x0003777C
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.ScaleRangeDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x000395D8 File Offset: 0x000377D8
		public double StartWidth
		{
			get
			{
				if (this.m_startWidth == null)
				{
					this.m_startWidth = new double?(this.m_defObject.ScaleRangeDef.EvaluateStartWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_startWidth.Value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00039634 File Offset: 0x00037834
		public double EndWidth
		{
			get
			{
				if (this.m_endWidth == null)
				{
					this.m_endWidth = new double?(this.m_defObject.ScaleRangeDef.EvaluateEndWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_endWidth.Value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00039690 File Offset: 0x00037890
		public ReportColor InRangeBarPointerColor
		{
			get
			{
				if (this.m_inRangeBarPointerColor == null)
				{
					this.m_inRangeBarPointerColor = new ReportColor(this.m_defObject.ScaleRangeDef.EvaluateInRangeBarPointerColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_inRangeBarPointerColor;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000396E4 File Offset: 0x000378E4
		public ReportColor InRangeLabelColor
		{
			get
			{
				if (this.m_inRangeLabelColor == null)
				{
					this.m_inRangeLabelColor = new ReportColor(this.m_defObject.ScaleRangeDef.EvaluateInRangeLabelColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_inRangeLabelColor;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00039738 File Offset: 0x00037938
		public ReportColor InRangeTickMarksColor
		{
			get
			{
				if (this.m_inRangeTickMarksColor == null)
				{
					this.m_inRangeTickMarksColor = new ReportColor(this.m_defObject.ScaleRangeDef.EvaluateInRangeTickMarksColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_inRangeTickMarksColor;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0003978C File Offset: 0x0003798C
		public BackgroundGradientTypes BackgroundGradientType
		{
			get
			{
				if (this.m_backgroundGradientType == null)
				{
					this.m_backgroundGradientType = new BackgroundGradientTypes?(this.m_defObject.ScaleRangeDef.EvaluateBackgroundGradientType(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_backgroundGradientType.Value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000397E8 File Offset: 0x000379E8
		public ScaleRangePlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new ScaleRangePlacements?(this.m_defObject.ScaleRangeDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00039844 File Offset: 0x00037A44
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_defObject.ScaleRangeDef.EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00039890 File Offset: 0x00037A90
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.ScaleRangeDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000398EC File Offset: 0x00037AEC
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_distanceFromScale = null;
			this.m_startWidth = null;
			this.m_endWidth = null;
			this.m_inRangeBarPointerColor = null;
			this.m_inRangeLabelColor = null;
			this.m_inRangeTickMarksColor = null;
			this.m_placement = null;
			this.m_toolTip = null;
			this.m_hidden = null;
			this.m_backgroundGradientType = null;
		}

		// Token: 0x0400061C RID: 1564
		private ScaleRange m_defObject;

		// Token: 0x0400061D RID: 1565
		private StyleInstance m_style;

		// Token: 0x0400061E RID: 1566
		private double? m_distanceFromScale;

		// Token: 0x0400061F RID: 1567
		private double? m_startWidth;

		// Token: 0x04000620 RID: 1568
		private double? m_endWidth;

		// Token: 0x04000621 RID: 1569
		private ReportColor m_inRangeBarPointerColor;

		// Token: 0x04000622 RID: 1570
		private ReportColor m_inRangeLabelColor;

		// Token: 0x04000623 RID: 1571
		private ReportColor m_inRangeTickMarksColor;

		// Token: 0x04000624 RID: 1572
		private BackgroundGradientTypes? m_backgroundGradientType;

		// Token: 0x04000625 RID: 1573
		private ScaleRangePlacements? m_placement;

		// Token: 0x04000626 RID: 1574
		private string m_toolTip;

		// Token: 0x04000627 RID: 1575
		private bool? m_hidden;
	}
}
