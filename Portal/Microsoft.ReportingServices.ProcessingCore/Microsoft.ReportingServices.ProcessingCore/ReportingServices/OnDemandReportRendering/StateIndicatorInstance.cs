using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000137 RID: 311
	public sealed class StateIndicatorInstance : GaugePanelItemInstance
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x00039FDD File Offset: 0x000381DD
		internal StateIndicatorInstance(StateIndicator defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00039FF0 File Offset: 0x000381F0
		public GaugeStateIndicatorStyles IndicatorStyle
		{
			get
			{
				if (this.m_indicatorStyle == null)
				{
					this.m_indicatorStyle = new GaugeStateIndicatorStyles?(((StateIndicator)this.m_defObject.GaugePanelItemDef).EvaluateIndicatorStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_indicatorStyle.Value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0003A050 File Offset: 0x00038250
		public double ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null)
				{
					this.m_scaleFactor = new double?(((StateIndicator)this.m_defObject.GaugePanelItemDef).EvaluateScaleFactor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_scaleFactor.Value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0003A0B4 File Offset: 0x000382B4
		public GaugeResizeModes ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null)
				{
					this.m_resizeMode = new GaugeResizeModes?(((StateIndicator)this.m_defObject.GaugePanelItemDef).EvaluateResizeMode(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_resizeMode.Value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0003A114 File Offset: 0x00038314
		public double Angle
		{
			get
			{
				if (this.m_angle == null)
				{
					this.m_angle = new double?(((StateIndicator)this.m_defObject.GaugePanelItemDef).EvaluateAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_angle.Value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0003A178 File Offset: 0x00038378
		public GaugeTransformationType TransformationType
		{
			get
			{
				if (this.m_transformationType == null)
				{
					this.m_transformationType = new GaugeTransformationType?(((StateIndicator)this.m_defObject.GaugePanelItemDef).EvaluateTransformationType(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_transformationType.Value;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003A1D8 File Offset: 0x000383D8
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_indicatorStyle = null;
			this.m_scaleFactor = null;
			this.m_resizeMode = null;
			this.m_angle = null;
			this.m_transformationType = null;
		}

		// Token: 0x0400063B RID: 1595
		private StateIndicator m_defObject;

		// Token: 0x0400063C RID: 1596
		private GaugeStateIndicatorStyles? m_indicatorStyle;

		// Token: 0x0400063D RID: 1597
		private double? m_scaleFactor;

		// Token: 0x0400063E RID: 1598
		private GaugeResizeModes? m_resizeMode;

		// Token: 0x0400063F RID: 1599
		private double? m_angle;

		// Token: 0x04000640 RID: 1600
		private GaugeTransformationType? m_transformationType;
	}
}
