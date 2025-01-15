using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000184 RID: 388
	internal interface ISpatialElementCollection
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06000FFE RID: 4094
		int Count { get; }

		// Token: 0x06000FFF RID: 4095
		MapSpatialElement GetItem(int index);
	}
}
