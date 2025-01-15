using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FE RID: 254
	public sealed class IndicatorStateCollection : GaugePanelObjectCollectionBase<IndicatorState>
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x000328FA File Offset: 0x00030AFA
		internal IndicatorStateCollection(StateIndicator stateIndicator, GaugePanel gaugePanel)
		{
			this.m_stateIndicator = stateIndicator;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00032910 File Offset: 0x00030B10
		protected override IndicatorState CreateGaugePanelObject(int index)
		{
			return new IndicatorState(this.m_stateIndicator.StateIndicatorDef.IndicatorStates[index], this.m_gaugePanel);
		}

		// Token: 0x1700066E RID: 1646
		public IndicatorState this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					IndicatorState indicatorState = this.m_stateIndicator.StateIndicatorDef.IndicatorStates[i];
					if (string.CompareOrdinal(name, indicatorState.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00032993 File Offset: 0x00030B93
		public override int Count
		{
			get
			{
				if (this.m_stateIndicator.StateIndicatorDef.IndicatorStates != null)
				{
					return this.m_stateIndicator.StateIndicatorDef.IndicatorStates.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004D6 RID: 1238
		private StateIndicator m_stateIndicator;

		// Token: 0x040004D7 RID: 1239
		private GaugePanel m_gaugePanel;
	}
}
