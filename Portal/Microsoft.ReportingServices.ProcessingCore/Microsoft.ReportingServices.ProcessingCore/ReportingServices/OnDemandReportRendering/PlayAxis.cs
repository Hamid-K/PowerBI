using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021B RID: 539
	public sealed class PlayAxis : Navigation
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0005399F File Offset: 0x00051B9F
		internal PlayAxis(BandLayoutOptions bandLayout)
			: base(bandLayout)
		{
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000539A8 File Offset: 0x00051BA8
		public Slider Slider
		{
			get
			{
				if (this.RIFPlayAxis.Slider == null)
				{
					return null;
				}
				return new Slider(this.RIFPlayAxis.Slider);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000539C9 File Offset: 0x00051BC9
		public DockingOption DockingOption
		{
			get
			{
				return this.RIFPlayAxis.DockingOption;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x000539D6 File Offset: 0x00051BD6
		private PlayAxis RIFPlayAxis
		{
			get
			{
				return this.m_navigation as PlayAxis;
			}
		}
	}
}
