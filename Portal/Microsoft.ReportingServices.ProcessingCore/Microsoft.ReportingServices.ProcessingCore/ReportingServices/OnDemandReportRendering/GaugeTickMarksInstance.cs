using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012F RID: 303
	public sealed class GaugeTickMarksInstance : TickMarkStyleInstance
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00038D2C File Offset: 0x00036F2C
		internal GaugeTickMarksInstance(GaugeTickMarks defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00038D3C File Offset: 0x00036F3C
		public double Interval
		{
			get
			{
				if (this.m_interval == null)
				{
					this.m_interval = new double?(((GaugeTickMarks)this.m_defObject.TickMarkStyleDef).EvaluateInterval(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00038DA0 File Offset: 0x00036FA0
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null)
				{
					this.m_intervalOffset = new double?(((GaugeTickMarks)this.m_defObject.TickMarkStyleDef).EvaluateIntervalOffset(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00038E01 File Offset: 0x00037001
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_interval = null;
			this.m_intervalOffset = null;
		}

		// Token: 0x04000607 RID: 1543
		private double? m_interval;

		// Token: 0x04000608 RID: 1544
		private double? m_intervalOffset;
	}
}
