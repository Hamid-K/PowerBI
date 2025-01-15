using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000DC RID: 220
	public sealed class BackFrame : IROMStyleDefinitionContainer
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0003016B File Offset: 0x0002E36B
		internal BackFrame(BackFrame defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00030181 File Offset: 0x0002E381
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000301BC File Offset: 0x0002E3BC
		public ReportEnumProperty<GaugeFrameStyles> FrameStyle
		{
			get
			{
				if (this.m_frameStyle == null && this.m_defObject.FrameStyle != null)
				{
					this.m_frameStyle = new ReportEnumProperty<GaugeFrameStyles>(this.m_defObject.FrameStyle.IsExpression, this.m_defObject.FrameStyle.OriginalText, EnumTranslator.TranslateGaugeFrameStyles(this.m_defObject.FrameStyle.StringValue, null));
				}
				return this.m_frameStyle;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00030228 File Offset: 0x0002E428
		public ReportEnumProperty<GaugeFrameShapes> FrameShape
		{
			get
			{
				if (this.m_frameShape == null && this.m_defObject.FrameShape != null)
				{
					this.m_frameShape = new ReportEnumProperty<GaugeFrameShapes>(this.m_defObject.FrameShape.IsExpression, this.m_defObject.FrameShape.OriginalText, EnumTranslator.TranslateGaugeFrameShapes(this.m_defObject.FrameShape.StringValue, null));
				}
				return this.m_frameShape;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00030291 File Offset: 0x0002E491
		public ReportDoubleProperty FrameWidth
		{
			get
			{
				if (this.m_frameWidth == null && this.m_defObject.FrameWidth != null)
				{
					this.m_frameWidth = new ReportDoubleProperty(this.m_defObject.FrameWidth);
				}
				return this.m_frameWidth;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000302C4 File Offset: 0x0002E4C4
		public ReportEnumProperty<GaugeGlassEffects> GlassEffect
		{
			get
			{
				if (this.m_glassEffect == null && this.m_defObject.GlassEffect != null)
				{
					this.m_glassEffect = new ReportEnumProperty<GaugeGlassEffects>(this.m_defObject.GlassEffect.IsExpression, this.m_defObject.GlassEffect.OriginalText, EnumTranslator.TranslateGaugeGlassEffects(this.m_defObject.GlassEffect.StringValue, null));
				}
				return this.m_glassEffect;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0003032D File Offset: 0x0002E52D
		public FrameBackground FrameBackground
		{
			get
			{
				if (this.m_frameBackground == null && this.m_defObject.FrameBackground != null)
				{
					this.m_frameBackground = new FrameBackground(this.m_defObject.FrameBackground, this.m_gaugePanel);
				}
				return this.m_frameBackground;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00030366 File Offset: 0x0002E566
		public FrameImage FrameImage
		{
			get
			{
				if (this.m_frameImage == null && this.m_defObject.FrameImage != null)
				{
					this.m_frameImage = new FrameImage(this.m_defObject.FrameImage, this.m_gaugePanel);
				}
				return this.m_frameImage;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0003039F File Offset: 0x0002E59F
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000303A7 File Offset: 0x0002E5A7
		internal BackFrame BackFrameDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000303AF File Offset: 0x0002E5AF
		public BackFrameInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new BackFrameInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000303E0 File Offset: 0x0002E5E0
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_frameBackground != null)
			{
				this.m_frameBackground.SetNewContext();
			}
			if (this.m_frameImage != null)
			{
				this.m_frameImage.SetNewContext();
			}
		}

		// Token: 0x0400046F RID: 1135
		private GaugePanel m_gaugePanel;

		// Token: 0x04000470 RID: 1136
		private BackFrame m_defObject;

		// Token: 0x04000471 RID: 1137
		private BackFrameInstance m_instance;

		// Token: 0x04000472 RID: 1138
		private Style m_style;

		// Token: 0x04000473 RID: 1139
		private ReportEnumProperty<GaugeFrameStyles> m_frameStyle;

		// Token: 0x04000474 RID: 1140
		private ReportEnumProperty<GaugeFrameShapes> m_frameShape;

		// Token: 0x04000475 RID: 1141
		private ReportDoubleProperty m_frameWidth;

		// Token: 0x04000476 RID: 1142
		private ReportEnumProperty<GaugeGlassEffects> m_glassEffect;

		// Token: 0x04000477 RID: 1143
		private FrameBackground m_frameBackground;

		// Token: 0x04000478 RID: 1144
		private FrameImage m_frameImage;
	}
}
