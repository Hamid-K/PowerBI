using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F9 RID: 249
	public sealed class NumericIndicatorCollection : GaugePanelObjectCollectionBase<NumericIndicator>
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00032572 File Offset: 0x00030772
		internal NumericIndicatorCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00032581 File Offset: 0x00030781
		protected override NumericIndicator CreateGaugePanelObject(int index)
		{
			return new NumericIndicator(this.m_gaugePanel.GaugePanelDef.NumericIndicators[index], this.m_gaugePanel);
		}

		// Token: 0x17000664 RID: 1636
		public NumericIndicator this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					NumericIndicator numericIndicator = this.m_gaugePanel.GaugePanelDef.NumericIndicators[i];
					if (string.CompareOrdinal(name, numericIndicator.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00032603 File Offset: 0x00030803
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.NumericIndicators != null)
				{
					return this.m_gaugePanel.GaugePanelDef.NumericIndicators.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004CE RID: 1230
		private GaugePanel m_gaugePanel;
	}
}
