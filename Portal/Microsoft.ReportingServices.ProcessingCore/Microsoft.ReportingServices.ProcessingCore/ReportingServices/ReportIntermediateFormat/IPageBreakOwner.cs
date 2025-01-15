using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D9 RID: 1241
	internal interface IPageBreakOwner
	{
		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x06003EA8 RID: 16040
		// (set) Token: 0x06003EA9 RID: 16041
		PageBreak PageBreak { get; set; }

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x06003EAA RID: 16042
		ObjectType ObjectType { get; }

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06003EAB RID: 16043
		string ObjectName { get; }

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06003EAC RID: 16044
		IInstancePath InstancePath { get; }
	}
}
