using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F4 RID: 244
	public sealed class GaugeLabelCollection : GaugePanelObjectCollectionBase<GaugeLabel>
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x000321DE File Offset: 0x000303DE
		internal GaugeLabelCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000321ED File Offset: 0x000303ED
		protected override GaugeLabel CreateGaugePanelObject(int index)
		{
			return new GaugeLabel(this.m_gaugePanel.GaugePanelDef.GaugeLabels[index], this.m_gaugePanel);
		}

		// Token: 0x1700065A RID: 1626
		public GaugeLabel this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					GaugeLabel gaugeLabel = this.m_gaugePanel.GaugePanelDef.GaugeLabels[i];
					if (string.CompareOrdinal(name, gaugeLabel.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0003226F File Offset: 0x0003046F
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.GaugeLabels != null)
				{
					return this.m_gaugePanel.GaugePanelDef.GaugeLabels.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004C7 RID: 1223
		private GaugePanel m_gaugePanel;
	}
}
