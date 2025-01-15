using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AF RID: 1199
	public interface IRIFDataScope
	{
		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06003BC9 RID: 15305
		string Name { get; }

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06003BCA RID: 15306
		DataScopeInfo DataScopeInfo { get; }

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06003BCB RID: 15307
		ObjectType DataScopeObjectType { get; }
	}
}
