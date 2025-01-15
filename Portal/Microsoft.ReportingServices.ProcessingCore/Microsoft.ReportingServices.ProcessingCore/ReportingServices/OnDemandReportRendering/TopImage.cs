using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E6 RID: 230
	public sealed class TopImage : BaseGaugeImage
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003107C File Offset: 0x0002F27C
		internal TopImage(TopImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00031094 File Offset: 0x0002F294
		public ReportColorProperty HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					ExpressionInfo hueColor = this.TopImageDef.HueColor;
					if (hueColor != null)
					{
						this.m_hueColor = new ReportColorProperty(hueColor.IsExpression, hueColor.OriginalText, hueColor.IsExpression ? null : new ReportColor(hueColor.StringValue.Trim(), true), hueColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0003110C File Offset: 0x0002F30C
		internal TopImage TopImageDef
		{
			get
			{
				return (TopImage)this.m_defObject;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00031119 File Offset: 0x0002F319
		public new TopImageInstance Instance
		{
			get
			{
				return (TopImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00031126 File Offset: 0x0002F326
		internal override BaseGaugeImageInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new TopImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00031156 File Offset: 0x0002F356
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000498 RID: 1176
		private ReportColorProperty m_hueColor;
	}
}
