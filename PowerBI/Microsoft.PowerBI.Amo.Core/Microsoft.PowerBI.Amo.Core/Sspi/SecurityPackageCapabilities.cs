using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000112 RID: 274
	[Flags]
	internal enum SecurityPackageCapabilities
	{
		// Token: 0x040009C6 RID: 2502
		None = 0,
		// Token: 0x040009C7 RID: 2503
		Integrity = 1,
		// Token: 0x040009C8 RID: 2504
		Privacy = 2,
		// Token: 0x040009C9 RID: 2505
		TokenOnly = 4,
		// Token: 0x040009CA RID: 2506
		Datagram = 8,
		// Token: 0x040009CB RID: 2507
		Connection = 16,
		// Token: 0x040009CC RID: 2508
		MultiRequired = 32,
		// Token: 0x040009CD RID: 2509
		ClientOnly = 64,
		// Token: 0x040009CE RID: 2510
		ExtendedError = 128,
		// Token: 0x040009CF RID: 2511
		Impersonation = 256,
		// Token: 0x040009D0 RID: 2512
		AcceptWin32Name = 512,
		// Token: 0x040009D1 RID: 2513
		Stream = 1024,
		// Token: 0x040009D2 RID: 2514
		Negotiable = 2048,
		// Token: 0x040009D3 RID: 2515
		GssCompatible = 4096,
		// Token: 0x040009D4 RID: 2516
		Logon = 8192,
		// Token: 0x040009D5 RID: 2517
		AsciiBuffers = 16384,
		// Token: 0x040009D6 RID: 2518
		Fragment = 32768,
		// Token: 0x040009D7 RID: 2519
		MutualAuth = 65536,
		// Token: 0x040009D8 RID: 2520
		Delegation = 131072,
		// Token: 0x040009D9 RID: 2521
		ReadonlyWithChecksum = 262144,
		// Token: 0x040009DA RID: 2522
		RestrictedTokens = 524288,
		// Token: 0x040009DB RID: 2523
		NegoExtender = 1048576,
		// Token: 0x040009DC RID: 2524
		Negotiable2 = 2097152,
		// Token: 0x040009DD RID: 2525
		AppContainerPassthrough = 4194304,
		// Token: 0x040009DE RID: 2526
		AppContainerChecks = 8388608,
		// Token: 0x040009DF RID: 2527
		CredentialIsolationEnabled = 16777216
	}
}
