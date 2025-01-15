using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E4 RID: 228
	public sealed class IndicatorImage : BaseGaugeImage
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00030ECC File Offset: 0x0002F0CC
		internal IndicatorImage(IndicatorImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00030ED8 File Offset: 0x0002F0D8
		public ReportColorProperty HueColor
		{
			get
			{
				if (this.m_hueColor == null && this.IndicatorImageDef.HueColor != null)
				{
					ExpressionInfo hueColor = this.IndicatorImageDef.HueColor;
					if (hueColor != null)
					{
						this.m_hueColor = new ReportColorProperty(hueColor.IsExpression, this.IndicatorImageDef.HueColor.OriginalText, hueColor.IsExpression ? null : new ReportColor(hueColor.StringValue.Trim(), true), hueColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00030F67 File Offset: 0x0002F167
		public ReportDoubleProperty Transparency
		{
			get
			{
				if (this.m_transparency == null && this.IndicatorImageDef.Transparency != null)
				{
					this.m_transparency = new ReportDoubleProperty(this.IndicatorImageDef.Transparency);
				}
				return this.m_transparency;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00030F9A File Offset: 0x0002F19A
		internal IndicatorImage IndicatorImageDef
		{
			get
			{
				return (IndicatorImage)this.m_defObject;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00030FA7 File Offset: 0x0002F1A7
		public new IndicatorImageInstance Instance
		{
			get
			{
				return (IndicatorImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00030FB4 File Offset: 0x0002F1B4
		internal override BaseGaugeImageInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new IndicatorImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00030FE4 File Offset: 0x0002F1E4
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000496 RID: 1174
		private ReportColorProperty m_hueColor;

		// Token: 0x04000497 RID: 1175
		private ReportDoubleProperty m_transparency;
	}
}
