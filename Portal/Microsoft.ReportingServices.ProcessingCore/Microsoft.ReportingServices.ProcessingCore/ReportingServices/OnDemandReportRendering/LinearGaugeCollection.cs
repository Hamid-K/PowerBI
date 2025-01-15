using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F5 RID: 245
	public sealed class LinearGaugeCollection : GaugePanelObjectCollectionBase<LinearGauge>
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x0003229A File Offset: 0x0003049A
		internal LinearGaugeCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000322A9 File Offset: 0x000304A9
		protected override LinearGauge CreateGaugePanelObject(int index)
		{
			return new LinearGauge(this.m_gaugePanel.GaugePanelDef.LinearGauges[index], this.m_gaugePanel);
		}

		// Token: 0x1700065C RID: 1628
		public LinearGauge this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					LinearGauge linearGauge = this.m_gaugePanel.GaugePanelDef.LinearGauges[i];
					if (string.CompareOrdinal(name, linearGauge.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0003232B File Offset: 0x0003052B
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.LinearGauges != null)
				{
					return this.m_gaugePanel.GaugePanelDef.LinearGauges.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004C8 RID: 1224
		private GaugePanel m_gaugePanel;
	}
}
