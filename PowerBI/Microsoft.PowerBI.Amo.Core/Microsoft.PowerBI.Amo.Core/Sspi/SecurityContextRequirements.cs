using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010E RID: 270
	[Flags]
	internal enum SecurityContextRequirements
	{
		// Token: 0x04000984 RID: 2436
		None = 0,
		// Token: 0x04000985 RID: 2437
		Delegate = 1,
		// Token: 0x04000986 RID: 2438
		MutualAuth = 2,
		// Token: 0x04000987 RID: 2439
		ReplayDetect = 4,
		// Token: 0x04000988 RID: 2440
		SequenceDetect = 8,
		// Token: 0x04000989 RID: 2441
		Confidentiality = 16,
		// Token: 0x0400098A RID: 2442
		UseSessionKey = 32,
		// Token: 0x0400098B RID: 2443
		PromptForCreds = 64,
		// Token: 0x0400098C RID: 2444
		UsedCollectedCreds = 64,
		// Token: 0x0400098D RID: 2445
		SessionTicket = 64,
		// Token: 0x0400098E RID: 2446
		UseSuppliedCreds = 128,
		// Token: 0x0400098F RID: 2447
		UsedSuppliedCreds = 128,
		// Token: 0x04000990 RID: 2448
		AllocateMemory = 256,
		// Token: 0x04000991 RID: 2449
		UseDceStyle = 512,
		// Token: 0x04000992 RID: 2450
		UsedDceStyle = 512,
		// Token: 0x04000993 RID: 2451
		Datagram = 1024,
		// Token: 0x04000994 RID: 2452
		Connection = 2048,
		// Token: 0x04000995 RID: 2453
		CallLevel = 4096,
		// Token: 0x04000996 RID: 2454
		IntermediateReturn = 4096,
		// Token: 0x04000997 RID: 2455
		FragmentSupplied = 8192,
		// Token: 0x04000998 RID: 2456
		UsedCallLevel = 8192,
		// Token: 0x04000999 RID: 2457
		ExtendedError = 16384,
		// Token: 0x0400099A RID: 2458
		ThirdLegFailed = 16384,
		// Token: 0x0400099B RID: 2459
		Stream = 32768,
		// Token: 0x0400099C RID: 2460
		AcceptExtendedError = 32768,
		// Token: 0x0400099D RID: 2461
		Integrity = 65536,
		// Token: 0x0400099E RID: 2462
		AcceptStream = 65536,
		// Token: 0x0400099F RID: 2463
		Identity = 131072,
		// Token: 0x040009A0 RID: 2464
		AcceptIntegrity = 131072,
		// Token: 0x040009A1 RID: 2465
		NullSession = 262144,
		// Token: 0x040009A2 RID: 2466
		Licensing = 262144,
		// Token: 0x040009A3 RID: 2467
		ManualCredValidation = 524288,
		// Token: 0x040009A4 RID: 2468
		AcceptIdentity = 524288,
		// Token: 0x040009A5 RID: 2469
		Reserved1 = 1048576,
		// Token: 0x040009A6 RID: 2470
		AllowNullSession = 1048576,
		// Token: 0x040009A7 RID: 2471
		FragmentToFit = 2097152,
		// Token: 0x040009A8 RID: 2472
		FragmentOnly = 2097152,
		// Token: 0x040009A9 RID: 2473
		AllowNonUserLogons = 2097152,
		// Token: 0x040009AA RID: 2474
		ForwardCredentials = 4194304,
		// Token: 0x040009AB RID: 2475
		AllowContextReplay = 4194304,
		// Token: 0x040009AC RID: 2476
		NoIntegrity = 8388608,
		// Token: 0x040009AD RID: 2477
		AcceptFragmentToFit = 8388608,
		// Token: 0x040009AE RID: 2478
		AcceptFragmentOnly = 8388608,
		// Token: 0x040009AF RID: 2479
		UseHttpStyle = 16777216,
		// Token: 0x040009B0 RID: 2480
		UsedHttpStyle = 16777216,
		// Token: 0x040009B1 RID: 2481
		NoToken = 16777216,
		// Token: 0x040009B2 RID: 2482
		NoAdditionalToken = 33554432,
		// Token: 0x040009B3 RID: 2483
		ProxyBinding = 67108864,
		// Token: 0x040009B4 RID: 2484
		Reauthentication = 134217728,
		// Token: 0x040009B5 RID: 2485
		AllowMissingBindings = 268435456,
		// Token: 0x040009B6 RID: 2486
		UnverifiedTargetName = 536870912,
		// Token: 0x040009B7 RID: 2487
		ConfidentialityOnly = 1073741824
	}
}
