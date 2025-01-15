using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021C RID: 540
	public sealed class Slider
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x000539E3 File Offset: 0x00051BE3
		internal Slider(Slider slider)
		{
			this.m_slider = slider;
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x000539F2 File Offset: 0x00051BF2
		public bool Hidden
		{
			get
			{
				return this.m_slider.Hidden;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000539FF File Offset: 0x00051BFF
		public LabelData LabelData
		{
			get
			{
				if (this.m_slider.LabelData == null)
				{
					return null;
				}
				return new LabelData(this.m_slider.LabelData);
			}
		}

		// Token: 0x040009A7 RID: 2471
		private readonly Slider m_slider;
	}
}
