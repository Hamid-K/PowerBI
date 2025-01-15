using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FA RID: 250
	public sealed class RadialPointerCollection : GaugePanelObjectCollectionBase<RadialPointer>
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x0003262E File Offset: 0x0003082E
		internal RadialPointerCollection(RadialScale radialScale, GaugePanel gaugePanel)
		{
			this.m_radialScale = radialScale;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00032644 File Offset: 0x00030844
		protected override RadialPointer CreateGaugePanelObject(int index)
		{
			return new RadialPointer(this.m_radialScale.RadialScaleDef.GaugePointers[index], this.m_gaugePanel);
		}

		// Token: 0x17000666 RID: 1638
		public RadialPointer this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					RadialPointer radialPointer = this.m_radialScale.RadialScaleDef.GaugePointers[i];
					if (string.CompareOrdinal(name, radialPointer.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000326C7 File Offset: 0x000308C7
		public override int Count
		{
			get
			{
				return this.m_radialScale.RadialScaleDef.GaugePointers.Count;
			}
		}

		// Token: 0x040004CF RID: 1231
		private GaugePanel m_gaugePanel;

		// Token: 0x040004D0 RID: 1232
		private RadialScale m_radialScale;
	}
}
