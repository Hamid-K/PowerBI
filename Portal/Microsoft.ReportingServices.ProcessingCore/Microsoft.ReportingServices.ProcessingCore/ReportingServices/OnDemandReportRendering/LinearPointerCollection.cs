using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F6 RID: 246
	public sealed class LinearPointerCollection : GaugePanelObjectCollectionBase<LinearPointer>
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x00032356 File Offset: 0x00030556
		internal LinearPointerCollection(LinearScale linearScale, GaugePanel gaugePanel)
		{
			this.m_linearScale = linearScale;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0003236C File Offset: 0x0003056C
		protected override LinearPointer CreateGaugePanelObject(int index)
		{
			return new LinearPointer(this.m_linearScale.LinearScaleDef.GaugePointers[index], this.m_gaugePanel);
		}

		// Token: 0x1700065E RID: 1630
		public LinearPointer this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					LinearPointer linearPointer = this.m_linearScale.LinearScaleDef.GaugePointers[i];
					if (string.CompareOrdinal(name, linearPointer.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000323EF File Offset: 0x000305EF
		public override int Count
		{
			get
			{
				return this.m_linearScale.LinearScaleDef.GaugePointers.Count;
			}
		}

		// Token: 0x040004C9 RID: 1225
		private GaugePanel m_gaugePanel;

		// Token: 0x040004CA RID: 1226
		private LinearScale m_linearScale;
	}
}
