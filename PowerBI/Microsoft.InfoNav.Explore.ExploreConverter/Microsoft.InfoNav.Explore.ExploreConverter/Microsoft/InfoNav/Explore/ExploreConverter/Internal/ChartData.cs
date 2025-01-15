using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200008E RID: 142
	internal sealed class ChartData
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000CCBE File Offset: 0x0000AEBE
		internal ChartData(List<ChartSeries> chartSeries)
		{
			this._chartSeries = chartSeries;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000CCCD File Offset: 0x0000AECD
		internal List<ChartSeries> ChartSeries
		{
			get
			{
				return this._chartSeries;
			}
		}

		// Token: 0x040001E1 RID: 481
		private readonly List<ChartSeries> _chartSeries;
	}
}
