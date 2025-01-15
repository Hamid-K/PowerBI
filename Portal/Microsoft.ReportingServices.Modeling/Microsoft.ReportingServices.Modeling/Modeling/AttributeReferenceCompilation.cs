using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200007F RID: 127
	[Flags]
	internal enum AttributeReferenceCompilation
	{
		// Token: 0x04000302 RID: 770
		Any = 0,
		// Token: 0x04000303 RID: 771
		ScalarOnly = 1,
		// Token: 0x04000304 RID: 772
		AggregateOnly = 2,
		// Token: 0x04000305 RID: 773
		FilterOnly = 4,
		// Token: 0x04000306 RID: 774
		VisibleOnly = 8
	}
}
