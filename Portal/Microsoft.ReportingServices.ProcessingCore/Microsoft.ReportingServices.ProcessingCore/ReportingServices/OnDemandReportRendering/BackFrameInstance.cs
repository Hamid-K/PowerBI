using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000DE RID: 222
	public sealed class BackFrameInstance : BaseInstance
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x000304EF File Offset: 0x0002E6EF
		internal BackFrameInstance(BackFrame defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00030504 File Offset: 0x0002E704
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

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00030540 File Offset: 0x0002E740
		public GaugeFrameStyles FrameStyle
		{
			get
			{
				if (this.m_frameStyle == null)
				{
					this.m_frameStyle = new GaugeFrameStyles?(this.m_defObject.BackFrameDef.EvaluateFrameStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_frameStyle.Value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0003059C File Offset: 0x0002E79C
		public GaugeFrameShapes FrameShape
		{
			get
			{
				if (this.m_frameShape == null)
				{
					this.m_frameShape = new GaugeFrameShapes?(this.m_defObject.BackFrameDef.EvaluateFrameShape(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_frameShape.Value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000305F8 File Offset: 0x0002E7F8
		public double FrameWidth
		{
			get
			{
				if (this.m_frameWidth == null)
				{
					this.m_frameWidth = new double?(this.m_defObject.BackFrameDef.EvaluateFrameWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_frameWidth.Value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00030654 File Offset: 0x0002E854
		public GaugeGlassEffects GlassEffect
		{
			get
			{
				if (this.m_glassEffect == null)
				{
					this.m_glassEffect = new GaugeGlassEffects?(this.m_defObject.BackFrameDef.EvaluateGlassEffect(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_glassEffect.Value;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000306B0 File Offset: 0x0002E8B0
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_frameStyle = null;
			this.m_frameShape = null;
			this.m_glassEffect = null;
			this.m_frameWidth = null;
		}

		// Token: 0x0400047D RID: 1149
		private BackFrame m_defObject;

		// Token: 0x0400047E RID: 1150
		private StyleInstance m_style;

		// Token: 0x0400047F RID: 1151
		private GaugeFrameStyles? m_frameStyle;

		// Token: 0x04000480 RID: 1152
		private GaugeFrameShapes? m_frameShape;

		// Token: 0x04000481 RID: 1153
		private double? m_frameWidth;

		// Token: 0x04000482 RID: 1154
		private GaugeGlassEffects? m_glassEffect;
	}
}
