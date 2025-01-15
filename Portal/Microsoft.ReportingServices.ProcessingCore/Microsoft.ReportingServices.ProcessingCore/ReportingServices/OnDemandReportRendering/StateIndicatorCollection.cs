using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FD RID: 253
	public sealed class StateIndicatorCollection : GaugePanelObjectCollectionBase<StateIndicator>
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0003283E File Offset: 0x00030A3E
		internal StateIndicatorCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003284D File Offset: 0x00030A4D
		protected override StateIndicator CreateGaugePanelObject(int index)
		{
			return new StateIndicator(this.m_gaugePanel.GaugePanelDef.StateIndicators[index], this.m_gaugePanel);
		}

		// Token: 0x1700066C RID: 1644
		public StateIndicator this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					StateIndicator stateIndicator = this.m_gaugePanel.GaugePanelDef.StateIndicators[i];
					if (string.CompareOrdinal(name, stateIndicator.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000328CF File Offset: 0x00030ACF
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.StateIndicators != null)
				{
					return this.m_gaugePanel.GaugePanelDef.StateIndicators.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004D5 RID: 1237
		private GaugePanel m_gaugePanel;
	}
}
