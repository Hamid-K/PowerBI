using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000296 RID: 662
	internal interface IDataRegion
	{
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600199F RID: 6559
		bool HasDataCells { get; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060019A0 RID: 6560
		IDataRegionRowCollection RowCollection { get; }
	}
}
