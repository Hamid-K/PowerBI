using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E7 RID: 231
	[Flags]
	internal enum AdvanceChanges
	{
		// Token: 0x04000410 RID: 1040
		NoChange = 0,
		// Token: 0x04000411 RID: 1041
		CASConfigChange = 1,
		// Token: 0x04000412 RID: 1042
		RoutingLookupChange = 2,
		// Token: 0x04000413 RID: 1043
		RequestRetryChange = 4,
		// Token: 0x04000414 RID: 1044
		RegionPropertiesChange = 8,
		// Token: 0x04000415 RID: 1045
		StorePropertiesChange = 16,
		// Token: 0x04000416 RID: 1046
		MemoryPressureMonitorChange = 32,
		// Token: 0x04000417 RID: 1047
		SecurityPropertiesChange = 64,
		// Token: 0x04000418 RID: 1048
		TransportPropertiesChange = 128,
		// Token: 0x04000419 RID: 1049
		QuotaPropertiesChange = 256,
		// Token: 0x0400041A RID: 1050
		UsagePropertiesChange = 512,
		// Token: 0x0400041B RID: 1051
		VersionPropertiesChange = 1024,
		// Token: 0x0400041C RID: 1052
		StoreVersionChange = 2048,
		// Token: 0x0400041D RID: 1053
		DNSDomainChange = 4096,
		// Token: 0x0400041E RID: 1054
		DiagnosticModeChange = 8192,
		// Token: 0x0400041F RID: 1055
		DiagnosticBufferChange = 16384,
		// Token: 0x04000420 RID: 1056
		ChangeAll = 8191
	}
}
