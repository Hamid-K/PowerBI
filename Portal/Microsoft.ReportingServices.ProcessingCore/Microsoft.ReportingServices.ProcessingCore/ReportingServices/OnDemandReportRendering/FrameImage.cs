using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E1 RID: 225
	public sealed class FrameImage : BaseGaugeImage
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x00030A5E File Offset: 0x0002EC5E
		internal FrameImage(FrameImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00030A78 File Offset: 0x0002EC78
		public ReportColorProperty HueColor
		{
			get
			{
				if (this.m_hueColor == null && this.FrameImageDef.HueColor != null)
				{
					ExpressionInfo hueColor = this.FrameImageDef.HueColor;
					if (hueColor != null)
					{
						this.m_hueColor = new ReportColorProperty(hueColor.IsExpression, hueColor.OriginalText, hueColor.IsExpression ? null : new ReportColor(hueColor.StringValue.Trim(), true), hueColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00030AFD File Offset: 0x0002ECFD
		public ReportDoubleProperty Transparency
		{
			get
			{
				if (this.m_transparency == null && this.FrameImageDef.Transparency != null)
				{
					this.m_transparency = new ReportDoubleProperty(this.FrameImageDef.Transparency);
				}
				return this.m_transparency;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00030B30 File Offset: 0x0002ED30
		public ReportBoolProperty ClipImage
		{
			get
			{
				if (this.m_clipImage == null && this.FrameImageDef.ClipImage != null)
				{
					this.m_clipImage = new ReportBoolProperty(this.FrameImageDef.ClipImage);
				}
				return this.m_clipImage;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00030B63 File Offset: 0x0002ED63
		internal FrameImage FrameImageDef
		{
			get
			{
				return (FrameImage)this.m_defObject;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00030B70 File Offset: 0x0002ED70
		public new FrameImageInstance Instance
		{
			get
			{
				return (FrameImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00030B7D File Offset: 0x0002ED7D
		internal override BaseGaugeImageInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new FrameImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00030BAD File Offset: 0x0002EDAD
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400048C RID: 1164
		private ReportColorProperty m_hueColor;

		// Token: 0x0400048D RID: 1165
		private ReportDoubleProperty m_transparency;

		// Token: 0x0400048E RID: 1166
		private ReportBoolProperty m_clipImage;
	}
}
