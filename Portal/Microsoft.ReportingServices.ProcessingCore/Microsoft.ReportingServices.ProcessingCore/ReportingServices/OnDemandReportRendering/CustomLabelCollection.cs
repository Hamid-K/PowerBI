using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F2 RID: 242
	public sealed class CustomLabelCollection : GaugePanelObjectCollectionBase<CustomLabel>
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00032070 File Offset: 0x00030270
		internal CustomLabelCollection(GaugeScale gaugeScale, GaugePanel gaugePanel)
		{
			this.m_gaugeScale = gaugeScale;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00032086 File Offset: 0x00030286
		protected override CustomLabel CreateGaugePanelObject(int index)
		{
			return new CustomLabel(this.m_gaugeScale.GaugeScaleDef.CustomLabels[index], this.m_gaugePanel);
		}

		// Token: 0x17000656 RID: 1622
		public CustomLabel this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					CustomLabel customLabel = this.m_gaugeScale.GaugeScaleDef.CustomLabels[i];
					if (string.CompareOrdinal(name, customLabel.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0003210B File Offset: 0x0003030B
		public override int Count
		{
			get
			{
				return this.m_gaugeScale.GaugeScaleDef.CustomLabels.Count;
			}
		}

		// Token: 0x040004C4 RID: 1220
		private GaugePanel m_gaugePanel;

		// Token: 0x040004C5 RID: 1221
		private GaugeScale m_gaugeScale;
	}
}
