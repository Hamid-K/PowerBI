using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000056 RID: 86
	internal sealed class OWCChartColumnCollection
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00018D0D File Offset: 0x00016F0D
		internal OWCChartColumnCollection(OWCChart chartDef, OWCChartInstance chartInstance, ReportItem owner)
		{
			this.m_owner = owner;
			this.m_chartInstance = chartInstance;
			this.m_chartDef = chartDef;
		}

		// Token: 0x170004D2 RID: 1234
		public OWCChartColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				OWCChartColumn owcchartColumn;
				if (this.m_chartData == null || this.m_chartData[index] == null)
				{
					ArrayList arrayList = null;
					if (this.m_chartInstance != null)
					{
						arrayList = ((OWCChartInstanceInfo)this.m_owner.InstanceInfo)[index];
					}
					owcchartColumn = new OWCChartColumn(this.m_chartDef.ChartData[index], arrayList);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (this.m_chartData == null)
						{
							this.m_chartData = new OWCChartColumn[this.m_chartDef.ChartData.Count];
						}
						this.m_chartData[index] = owcchartColumn;
					}
				}
				else
				{
					owcchartColumn = this.m_chartData[index];
				}
				return owcchartColumn;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00018E0E File Offset: 0x0001700E
		public int Count
		{
			get
			{
				return this.m_chartDef.ChartData.Count;
			}
		}

		// Token: 0x040001A3 RID: 419
		private ReportItem m_owner;

		// Token: 0x040001A4 RID: 420
		private OWCChart m_chartDef;

		// Token: 0x040001A5 RID: 421
		private OWCChartInstance m_chartInstance;

		// Token: 0x040001A6 RID: 422
		private OWCChartColumn[] m_chartData;
	}
}
