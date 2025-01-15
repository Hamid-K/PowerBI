using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F7 RID: 247
	public sealed class RadialGaugeCollection : GaugePanelObjectCollectionBase<RadialGauge>
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x00032406 File Offset: 0x00030606
		internal RadialGaugeCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00032415 File Offset: 0x00030615
		protected override RadialGauge CreateGaugePanelObject(int index)
		{
			return new RadialGauge(this.m_gaugePanel.GaugePanelDef.RadialGauges[index], this.m_gaugePanel);
		}

		// Token: 0x17000660 RID: 1632
		public RadialGauge this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					RadialGauge radialGauge = this.m_gaugePanel.GaugePanelDef.RadialGauges[i];
					if (string.CompareOrdinal(name, radialGauge.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00032497 File Offset: 0x00030697
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.RadialGauges != null)
				{
					return this.m_gaugePanel.GaugePanelDef.RadialGauges.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004CB RID: 1227
		private GaugePanel m_gaugePanel;
	}
}
