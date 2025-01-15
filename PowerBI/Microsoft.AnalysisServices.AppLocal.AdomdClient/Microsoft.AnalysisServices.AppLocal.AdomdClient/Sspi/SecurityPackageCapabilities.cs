using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x0200011D RID: 285
	[Flags]
	internal enum SecurityPackageCapabilities
	{
		// Token: 0x04000A0D RID: 2573
		None = 0,
		// Token: 0x04000A0E RID: 2574
		Integrity = 1,
		// Token: 0x04000A0F RID: 2575
		Privacy = 2,
		// Token: 0x04000A10 RID: 2576
		TokenOnly = 4,
		// Token: 0x04000A11 RID: 2577
		Datagram = 8,
		// Token: 0x04000A12 RID: 2578
		Connection = 16,
		// Token: 0x04000A13 RID: 2579
		MultiRequired = 32,
		// Token: 0x04000A14 RID: 2580
		ClientOnly = 64,
		// Token: 0x04000A15 RID: 2581
		ExtendedError = 128,
		// Token: 0x04000A16 RID: 2582
		Impersonation = 256,
		// Token: 0x04000A17 RID: 2583
		AcceptWin32Name = 512,
		// Token: 0x04000A18 RID: 2584
		Stream = 1024,
		// Token: 0x04000A19 RID: 2585
		Negotiable = 2048,
		// Token: 0x04000A1A RID: 2586
		GssCompatible = 4096,
		// Token: 0x04000A1B RID: 2587
		Logon = 8192,
		// Token: 0x04000A1C RID: 2588
		AsciiBuffers = 16384,
		// Token: 0x04000A1D RID: 2589
		Fragment = 32768,
		// Token: 0x04000A1E RID: 2590
		MutualAuth = 65536,
		// Token: 0x04000A1F RID: 2591
		Delegation = 131072,
		// Token: 0x04000A20 RID: 2592
		ReadonlyWithChecksum = 262144,
		// Token: 0x04000A21 RID: 2593
		RestrictedTokens = 524288,
		// Token: 0x04000A22 RID: 2594
		NegoExtender = 1048576,
		// Token: 0x04000A23 RID: 2595
		Negotiable2 = 2097152,
		// Token: 0x04000A24 RID: 2596
		AppContainerPassthrough = 4194304,
		// Token: 0x04000A25 RID: 2597
		AppContainerChecks = 8388608,
		// Token: 0x04000A26 RID: 2598
		CredentialIsolationEnabled = 16777216
	}
}
