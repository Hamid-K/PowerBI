using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000103 RID: 259
	public sealed class LinearGaugeInstance : GaugeInstance
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x00032E55 File Offset: 0x00031055
		internal LinearGaugeInstance(LinearGauge defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00032E68 File Offset: 0x00031068
		public GaugeOrientations Orientation
		{
			get
			{
				if (this.m_orientation == null)
				{
					this.m_orientation = new GaugeOrientations?(((LinearGauge)this.m_defObject.GaugeDef).EvaluateOrientation(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_orientation.Value;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00032EC8 File Offset: 0x000310C8
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_orientation = null;
		}

		// Token: 0x040004E4 RID: 1252
		private LinearGauge m_defObject;

		// Token: 0x040004E5 RID: 1253
		private GaugeOrientations? m_orientation;
	}
}
