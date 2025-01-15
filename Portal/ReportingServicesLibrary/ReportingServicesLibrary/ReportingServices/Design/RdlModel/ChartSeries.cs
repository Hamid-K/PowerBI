using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B3 RID: 947
	public class ChartSeries
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x0007D992 File Offset: 0x0007BB92
		public ChartSeries()
		{
			this.PlotType = ChartSeries.PlotTypes.Auto;
			this.DataPoints = new List<DataPoint>();
		}

		// Token: 0x04000D39 RID: 3385
		public List<DataPoint> DataPoints;

		// Token: 0x04000D3A RID: 3386
		[DefaultValue(ChartSeries.PlotTypes.Auto)]
		public ChartSeries.PlotTypes PlotType;

		// Token: 0x0200050E RID: 1294
		public enum PlotTypes
		{
			// Token: 0x0400124B RID: 4683
			Auto,
			// Token: 0x0400124C RID: 4684
			Line
		}
	}
}
