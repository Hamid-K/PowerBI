using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000ED RID: 237
	public sealed class TopImageInstance : BaseGaugeImageInstance
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x000317C4 File Offset: 0x0002F9C4
		internal TopImageInstance(TopImage defObject)
			: base(defObject)
		{
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x000317D0 File Offset: 0x0002F9D0
		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((TopImage)this.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00031827 File Offset: 0x0002FA27
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
		}

		// Token: 0x040004A9 RID: 1193
		private ReportColor m_hueColor;
	}
}
