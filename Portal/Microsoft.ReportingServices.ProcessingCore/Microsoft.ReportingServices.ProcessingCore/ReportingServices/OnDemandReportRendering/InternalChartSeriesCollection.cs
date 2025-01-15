using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000225 RID: 549
	internal sealed class InternalChartSeriesCollection : ChartSeriesCollection
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x00054640 File Offset: 0x00052840
		internal InternalChartSeriesCollection(Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ChartSeriesList seriesDefs)
			: base(owner)
		{
			this.m_seriesDefs = seriesDefs;
		}

		// Token: 0x17000B02 RID: 2818
		public override ChartSeries this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_chartSeriesCollection == null)
				{
					this.m_chartSeriesCollection = new ChartSeries[this.Count];
				}
				if (this.m_chartSeriesCollection[index] == null)
				{
					this.m_chartSeriesCollection[index] = new InternalChartSeries(this.m_owner, index, this.m_seriesDefs[index]);
				}
				return this.m_chartSeriesCollection[index];
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x000546E6 File Offset: 0x000528E6
		public override int Count
		{
			get
			{
				return this.m_seriesDefs.Count;
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000546F4 File Offset: 0x000528F4
		internal InternalChartSeries GetByName(string seriesName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				InternalChartSeries internalChartSeries = (InternalChartSeries)this[i];
				if (ReportProcessing.CompareWithInvariantCulture(seriesName, internalChartSeries.Name, false) == 0)
				{
					return internalChartSeries;
				}
			}
			return null;
		}

		// Token: 0x040009C3 RID: 2499
		private ChartSeriesList m_seriesDefs;
	}
}
