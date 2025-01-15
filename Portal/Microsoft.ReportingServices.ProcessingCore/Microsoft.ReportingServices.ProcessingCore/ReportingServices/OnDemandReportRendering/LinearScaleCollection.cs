using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F8 RID: 248
	public sealed class LinearScaleCollection : GaugePanelObjectCollectionBase<LinearScale>
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x000324C2 File Offset: 0x000306C2
		internal LinearScaleCollection(LinearGauge linearGauge, GaugePanel gaugePanel)
		{
			this.m_linearGauge = linearGauge;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000324D8 File Offset: 0x000306D8
		protected override LinearScale CreateGaugePanelObject(int index)
		{
			return new LinearScale(this.m_linearGauge.LinearGaugeDef.GaugeScales[index], this.m_gaugePanel);
		}

		// Token: 0x17000662 RID: 1634
		public LinearScale this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					LinearScale linearScale = this.m_linearGauge.LinearGaugeDef.GaugeScales[i];
					if (string.CompareOrdinal(name, linearScale.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0003255B File Offset: 0x0003075B
		public override int Count
		{
			get
			{
				return this.m_linearGauge.LinearGaugeDef.GaugeScales.Count;
			}
		}

		// Token: 0x040004CC RID: 1228
		private GaugePanel m_gaugePanel;

		// Token: 0x040004CD RID: 1229
		private LinearGauge m_linearGauge;
	}
}
