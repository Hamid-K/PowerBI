using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FC RID: 252
	public sealed class ScaleRangeCollection : GaugePanelObjectCollectionBase<ScaleRange>
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x0003278E File Offset: 0x0003098E
		internal ScaleRangeCollection(GaugeScale gaugeScale, GaugePanel gaugePanel)
		{
			this.m_gaugeScale = gaugeScale;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000327A4 File Offset: 0x000309A4
		protected override ScaleRange CreateGaugePanelObject(int index)
		{
			return new ScaleRange(this.m_gaugeScale.GaugeScaleDef.ScaleRanges[index], this.m_gaugePanel);
		}

		// Token: 0x1700066A RID: 1642
		public ScaleRange this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					ScaleRange scaleRange = this.m_gaugeScale.GaugeScaleDef.ScaleRanges[i];
					if (string.CompareOrdinal(name, scaleRange.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00032827 File Offset: 0x00030A27
		public override int Count
		{
			get
			{
				return this.m_gaugeScale.GaugeScaleDef.ScaleRanges.Count;
			}
		}

		// Token: 0x040004D3 RID: 1235
		private GaugePanel m_gaugePanel;

		// Token: 0x040004D4 RID: 1236
		private GaugeScale m_gaugeScale;
	}
}
