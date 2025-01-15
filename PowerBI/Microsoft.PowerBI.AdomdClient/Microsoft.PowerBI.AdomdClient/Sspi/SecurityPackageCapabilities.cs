using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x0200011D RID: 285
	[Flags]
	internal enum SecurityPackageCapabilities
	{
		// Token: 0x04000A00 RID: 2560
		None = 0,
		// Token: 0x04000A01 RID: 2561
		Integrity = 1,
		// Token: 0x04000A02 RID: 2562
		Privacy = 2,
		// Token: 0x04000A03 RID: 2563
		TokenOnly = 4,
		// Token: 0x04000A04 RID: 2564
		Datagram = 8,
		// Token: 0x04000A05 RID: 2565
		Connection = 16,
		// Token: 0x04000A06 RID: 2566
		MultiRequired = 32,
		// Token: 0x04000A07 RID: 2567
		ClientOnly = 64,
		// Token: 0x04000A08 RID: 2568
		ExtendedError = 128,
		// Token: 0x04000A09 RID: 2569
		Impersonation = 256,
		// Token: 0x04000A0A RID: 2570
		AcceptWin32Name = 512,
		// Token: 0x04000A0B RID: 2571
		Stream = 1024,
		// Token: 0x04000A0C RID: 2572
		Negotiable = 2048,
		// Token: 0x04000A0D RID: 2573
		GssCompatible = 4096,
		// Token: 0x04000A0E RID: 2574
		Logon = 8192,
		// Token: 0x04000A0F RID: 2575
		AsciiBuffers = 16384,
		// Token: 0x04000A10 RID: 2576
		Fragment = 32768,
		// Token: 0x04000A11 RID: 2577
		MutualAuth = 65536,
		// Token: 0x04000A12 RID: 2578
		Delegation = 131072,
		// Token: 0x04000A13 RID: 2579
		ReadonlyWithChecksum = 262144,
		// Token: 0x04000A14 RID: 2580
		RestrictedTokens = 524288,
		// Token: 0x04000A15 RID: 2581
		NegoExtender = 1048576,
		// Token: 0x04000A16 RID: 2582
		Negotiable2 = 2097152,
		// Token: 0x04000A17 RID: 2583
		AppContainerPassthrough = 4194304,
		// Token: 0x04000A18 RID: 2584
		AppContainerChecks = 8388608,
		// Token: 0x04000A19 RID: 2585
		CredentialIsolationEnabled = 16777216
	}
}
