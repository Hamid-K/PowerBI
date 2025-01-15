using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000247 RID: 583
	public sealed class ChartFormulaParameterCollection : ChartObjectCollectionBase<ChartFormulaParameter, ChartFormulaParameterInstance>
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x0005AD1B File Offset: 0x00058F1B
		internal ChartFormulaParameterCollection(ChartDerivedSeries derivedSeries, Chart chart)
		{
			this.m_derivedSeries = derivedSeries;
			this.m_chart = chart;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005AD31 File Offset: 0x00058F31
		protected override ChartFormulaParameter CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartFormulaParameter(this.m_derivedSeries, this.m_derivedSeries.ChartDerivedSeriesDef.FormulaParameters[index], this.m_chart);
		}

		// Token: 0x17000C83 RID: 3203
		public ChartFormulaParameter this[string name]
		{
			get
			{
				if (!this.m_chart.IsOldSnapshot)
				{
					for (int i = 0; i < this.Count; i++)
					{
						ChartFormulaParameter chartFormulaParameter = this.m_derivedSeries.ChartDerivedSeriesDef.FormulaParameters[i];
						if (string.CompareOrdinal(name, chartFormulaParameter.FormulaParameterName) == 0)
						{
							return base[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0005ADC5 File Offset: 0x00058FC5
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_derivedSeries.ChartDerivedSeriesDef.FormulaParameters.Count;
			}
		}

		// Token: 0x04000AEA RID: 2794
		private Chart m_chart;

		// Token: 0x04000AEB RID: 2795
		private ChartDerivedSeries m_derivedSeries;
	}
}
