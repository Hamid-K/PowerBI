using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F3 RID: 243
	public sealed class GaugeImageCollection : GaugePanelObjectCollectionBase<GaugeImage>
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00032122 File Offset: 0x00030322
		internal GaugeImageCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00032131 File Offset: 0x00030331
		protected override GaugeImage CreateGaugePanelObject(int index)
		{
			return new GaugeImage(this.m_gaugePanel.GaugePanelDef.GaugeImages[index], this.m_gaugePanel);
		}

		// Token: 0x17000658 RID: 1624
		public GaugeImage this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					GaugeImage gaugeImage = this.m_gaugePanel.GaugePanelDef.GaugeImages[i];
					if (string.CompareOrdinal(name, gaugeImage.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x000321B3 File Offset: 0x000303B3
		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.GaugeImages != null)
				{
					return this.m_gaugePanel.GaugePanelDef.GaugeImages.Count;
				}
				return 0;
			}
		}

		// Token: 0x040004C6 RID: 1222
		private GaugePanel m_gaugePanel;
	}
}
