using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011A RID: 282
	public sealed class PointerCap : IROMStyleDefinitionContainer
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00035D7B File Offset: 0x00033F7B
		internal PointerCap(PointerCap defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00035D91 File Offset: 0x00033F91
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

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00035DC9 File Offset: 0x00033FC9
		public CapImage CapImage
		{
			get
			{
				if (this.m_capImage == null && this.m_defObject.CapImage != null)
				{
					this.m_capImage = new CapImage(this.m_defObject.CapImage, this.m_gaugePanel);
				}
				return this.m_capImage;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00035E02 File Offset: 0x00034002
		public ReportBoolProperty OnTop
		{
			get
			{
				if (this.m_onTop == null && this.m_defObject.OnTop != null)
				{
					this.m_onTop = new ReportBoolProperty(this.m_defObject.OnTop);
				}
				return this.m_onTop;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00035E35 File Offset: 0x00034035
		public ReportBoolProperty Reflection
		{
			get
			{
				if (this.m_reflection == null && this.m_defObject.Reflection != null)
				{
					this.m_reflection = new ReportBoolProperty(this.m_defObject.Reflection);
				}
				return this.m_reflection;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00035E68 File Offset: 0x00034068
		public ReportEnumProperty<GaugeCapStyles> CapStyle
		{
			get
			{
				if (this.m_capStyle == null && this.m_defObject.CapStyle != null)
				{
					this.m_capStyle = new ReportEnumProperty<GaugeCapStyles>(this.m_defObject.CapStyle.IsExpression, this.m_defObject.CapStyle.OriginalText, EnumTranslator.TranslateGaugeCapStyles(this.m_defObject.CapStyle.StringValue, null));
				}
				return this.m_capStyle;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00035ED1 File Offset: 0x000340D1
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_defObject.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_defObject.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00035F04 File Offset: 0x00034104
		public ReportDoubleProperty Width
		{
			get
			{
				if (this.m_width == null && this.m_defObject.Width != null)
				{
					this.m_width = new ReportDoubleProperty(this.m_defObject.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00035F37 File Offset: 0x00034137
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00035F3F File Offset: 0x0003413F
		internal PointerCap PointerCapDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00035F47 File Offset: 0x00034147
		public PointerCapInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new PointerCapInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00035F77 File Offset: 0x00034177
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
			if (this.m_capImage != null)
			{
				this.m_capImage.SetNewContext();
			}
		}

		// Token: 0x0400056C RID: 1388
		private GaugePanel m_gaugePanel;

		// Token: 0x0400056D RID: 1389
		private PointerCap m_defObject;

		// Token: 0x0400056E RID: 1390
		private PointerCapInstance m_instance;

		// Token: 0x0400056F RID: 1391
		private Style m_style;

		// Token: 0x04000570 RID: 1392
		private CapImage m_capImage;

		// Token: 0x04000571 RID: 1393
		private ReportBoolProperty m_onTop;

		// Token: 0x04000572 RID: 1394
		private ReportBoolProperty m_reflection;

		// Token: 0x04000573 RID: 1395
		private ReportEnumProperty<GaugeCapStyles> m_capStyle;

		// Token: 0x04000574 RID: 1396
		private ReportBoolProperty m_hidden;

		// Token: 0x04000575 RID: 1397
		private ReportDoubleProperty m_width;
	}
}
