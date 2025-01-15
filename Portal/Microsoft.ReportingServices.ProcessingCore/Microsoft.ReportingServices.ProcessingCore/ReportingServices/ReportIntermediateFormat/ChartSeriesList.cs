using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000492 RID: 1170
	[Serializable]
	internal sealed class ChartSeriesList : RowList
	{
		// Token: 0x060037C2 RID: 14274 RVA: 0x000F2FE6 File Offset: 0x000F11E6
		public ChartSeriesList()
		{
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000F2FEE File Offset: 0x000F11EE
		internal ChartSeriesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001864 RID: 6244
		internal ChartSeries this[int index]
		{
			get
			{
				return (ChartSeries)base[index];
			}
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000F3008 File Offset: 0x000F1208
		internal ChartSeries GetByName(string seriesName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (ReportProcessing.CompareWithInvariantCulture(seriesName, chartSeries.Name, false) == 0)
				{
					return chartSeries;
				}
			}
			return null;
		}
	}
}
