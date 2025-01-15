using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E3 RID: 227
	public sealed class CapImage : BaseGaugeImage
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00030D63 File Offset: 0x0002EF63
		internal CapImage(CapImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00030D7C File Offset: 0x0002EF7C
		public ReportColorProperty HueColor
		{
			get
			{
				if (this.m_hueColor == null && this.CapImageDef.HueColor != null)
				{
					ExpressionInfo hueColor = this.CapImageDef.HueColor;
					if (hueColor != null)
					{
						this.m_hueColor = new ReportColorProperty(hueColor.IsExpression, hueColor.OriginalText, hueColor.IsExpression ? null : new ReportColor(hueColor.StringValue.Trim(), true), hueColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00030E01 File Offset: 0x0002F001
		public ReportSizeProperty OffsetX
		{
			get
			{
				if (this.m_offsetX == null && this.CapImageDef.OffsetX != null)
				{
					this.m_offsetX = new ReportSizeProperty(this.CapImageDef.OffsetX);
				}
				return this.m_offsetX;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00030E34 File Offset: 0x0002F034
		public ReportSizeProperty OffsetY
		{
			get
			{
				if (this.m_offsetY == null && this.CapImageDef.OffsetY != null)
				{
					this.m_offsetY = new ReportSizeProperty(this.CapImageDef.OffsetY);
				}
				return this.m_offsetY;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00030E67 File Offset: 0x0002F067
		internal CapImage CapImageDef
		{
			get
			{
				return (CapImage)this.m_defObject;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00030E74 File Offset: 0x0002F074
		public new CapImageInstance Instance
		{
			get
			{
				return (CapImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00030E81 File Offset: 0x0002F081
		internal override BaseGaugeImageInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new CapImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00030EB1 File Offset: 0x0002F0B1
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000493 RID: 1171
		private ReportColorProperty m_hueColor;

		// Token: 0x04000494 RID: 1172
		private ReportSizeProperty m_offsetX;

		// Token: 0x04000495 RID: 1173
		private ReportSizeProperty m_offsetY;
	}
}
