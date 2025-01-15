using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000298 RID: 664
	internal interface IDataRegionRow
	{
		// Token: 0x060019A3 RID: 6563
		IDataRegionCell GetIfExists(int index);

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060019A4 RID: 6564
		int Count { get; }
	}
}
