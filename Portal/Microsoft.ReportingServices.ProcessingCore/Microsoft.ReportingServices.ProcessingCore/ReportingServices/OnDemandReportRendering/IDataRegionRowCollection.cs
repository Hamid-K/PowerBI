using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000297 RID: 663
	internal interface IDataRegionRowCollection
	{
		// Token: 0x060019A1 RID: 6561
		IDataRegionRow GetIfExists(int index);

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060019A2 RID: 6562
		int Count { get; }
	}
}
