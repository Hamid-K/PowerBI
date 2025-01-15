using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000035 RID: 53
	[Flags]
	internal enum ExtendedProtectionScenario
	{
		// Token: 0x040000BF RID: 191
		Proxy = 0,
		// Token: 0x040000C0 RID: 192
		Direct = 1,
		// Token: 0x040000C1 RID: 193
		Any = 2,
		// Token: 0x040000C2 RID: 194
		Invalid = 4
	}
}
