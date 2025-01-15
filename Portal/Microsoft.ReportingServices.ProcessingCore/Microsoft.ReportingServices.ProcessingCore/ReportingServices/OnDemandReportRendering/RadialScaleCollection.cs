using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FB RID: 251
	public sealed class RadialScaleCollection : GaugePanelObjectCollectionBase<RadialScale>
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x000326DE File Offset: 0x000308DE
		internal RadialScaleCollection(RadialGauge radialGauge, GaugePanel gaugePanel)
		{
			this.m_radialGauge = radialGauge;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000326F4 File Offset: 0x000308F4
		protected override RadialScale CreateGaugePanelObject(int index)
		{
			return new RadialScale(this.m_radialGauge.RadialGaugeDef.GaugeScales[index], this.m_gaugePanel);
		}

		// Token: 0x17000668 RID: 1640
		public RadialScale this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					RadialScale radialScale = this.m_radialGauge.RadialGaugeDef.GaugeScales[i];
					if (string.CompareOrdinal(name, radialScale.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00032777 File Offset: 0x00030977
		public override int Count
		{
			get
			{
				return this.m_radialGauge.RadialGaugeDef.GaugeScales.Count;
			}
		}

		// Token: 0x040004D1 RID: 1233
		private GaugePanel m_gaugePanel;

		// Token: 0x040004D2 RID: 1234
		private RadialGauge m_radialGauge;
	}
}
