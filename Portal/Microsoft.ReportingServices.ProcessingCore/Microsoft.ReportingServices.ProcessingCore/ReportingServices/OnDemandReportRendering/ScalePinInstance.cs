using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000130 RID: 304
	public sealed class ScalePinInstance : TickMarkStyleInstance
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00038E21 File Offset: 0x00037021
		internal ScalePinInstance(ScalePin defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00038E34 File Offset: 0x00037034
		public double Location
		{
			get
			{
				if (this.m_location == null)
				{
					this.m_location = new double?(((ScalePin)this.m_defObject.TickMarkStyleDef).EvaluateLocation(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_location.Value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00038E98 File Offset: 0x00037098
		public bool Enable
		{
			get
			{
				if (this.m_enable == null)
				{
					this.m_enable = new bool?(((ScalePin)this.m_defObject.TickMarkStyleDef).EvaluateEnable(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_enable.Value;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00038EF8 File Offset: 0x000370F8
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_location = null;
			this.m_enable = null;
		}

		// Token: 0x04000609 RID: 1545
		private double? m_location;

		// Token: 0x0400060A RID: 1546
		private bool? m_enable;
	}
}
