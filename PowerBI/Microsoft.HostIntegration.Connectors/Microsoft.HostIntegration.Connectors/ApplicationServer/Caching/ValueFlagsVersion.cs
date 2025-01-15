using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D6 RID: 470
	[Flags]
	internal enum ValueFlagsVersion
	{
		// Token: 0x04000A88 RID: 2696
		LegacyWcfType = 1,
		// Token: 0x04000A89 RID: 2697
		WireProtocolType = 2,
		// Token: 0x04000A8A RID: 2698
		EitherType = 3
	}
}
