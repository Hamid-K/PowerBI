using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum MashupDiscoveryOptions
	{
		// Token: 0x04000146 RID: 326
		None = 0,
		// Token: 0x04000147 RID: 327
		ReportExtensionDSR = 1,
		// Token: 0x04000148 RID: 328
		ReportOptionsRecord = 2,
		// Token: 0x04000149 RID: 329
		ReportDependencies = 4,
		// Token: 0x0400014A RID: 330
		ReportNavigationSteps = 8,
		// Token: 0x0400014B RID: 331
		IgnoreNativeQueries = 16,
		// Token: 0x0400014C RID: 332
		ReportMetadata = 32,
		// Token: 0x0400014D RID: 333
		MultipleNavigationSteps = 64
	}
}
