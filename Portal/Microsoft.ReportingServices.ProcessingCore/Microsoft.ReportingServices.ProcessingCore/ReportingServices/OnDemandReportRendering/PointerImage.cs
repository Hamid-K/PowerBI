using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E2 RID: 226
	public sealed class PointerImage : BaseGaugeImage
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00030BC8 File Offset: 0x0002EDC8
		internal PointerImage(PointerImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00030BE0 File Offset: 0x0002EDE0
		public ReportColorProperty HueColor
		{
			get
			{
				if (this.m_hueColor == null && this.PointerImageDef.HueColor != null)
				{
					ExpressionInfo hueColor = this.PointerImageDef.HueColor;
					if (hueColor != null)
					{
						this.m_hueColor = new ReportColorProperty(hueColor.IsExpression, hueColor.OriginalText, hueColor.IsExpression ? null : new ReportColor(hueColor.StringValue.Trim(), true), hueColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00030C65 File Offset: 0x0002EE65
		public ReportDoubleProperty Transparency
		{
			get
			{
				if (this.m_transparency == null && this.PointerImageDef.Transparency != null)
				{
					this.m_transparency = new ReportDoubleProperty(this.PointerImageDef.Transparency);
				}
				return this.m_transparency;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00030C98 File Offset: 0x0002EE98
		public ReportSizeProperty OffsetX
		{
			get
			{
				if (this.m_offsetX == null && this.PointerImageDef.OffsetX != null)
				{
					this.m_offsetX = new ReportSizeProperty(this.PointerImageDef.OffsetX);
				}
				return this.m_offsetX;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00030CCB File Offset: 0x0002EECB
		public ReportSizeProperty OffsetY
		{
			get
			{
				if (this.m_offsetY == null && this.PointerImageDef.OffsetY != null)
				{
					this.m_offsetY = new ReportSizeProperty(this.PointerImageDef.OffsetY);
				}
				return this.m_offsetY;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00030CFE File Offset: 0x0002EEFE
		internal PointerImage PointerImageDef
		{
			get
			{
				return (PointerImage)this.m_defObject;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00030D0B File Offset: 0x0002EF0B
		public new PointerImageInstance Instance
		{
			get
			{
				return (PointerImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00030D18 File Offset: 0x0002EF18
		internal override BaseGaugeImageInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new PointerImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00030D48 File Offset: 0x0002EF48
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400048F RID: 1167
		private ReportColorProperty m_hueColor;

		// Token: 0x04000490 RID: 1168
		private ReportDoubleProperty m_transparency;

		// Token: 0x04000491 RID: 1169
		private ReportSizeProperty m_offsetX;

		// Token: 0x04000492 RID: 1170
		private ReportSizeProperty m_offsetY;
	}
}
