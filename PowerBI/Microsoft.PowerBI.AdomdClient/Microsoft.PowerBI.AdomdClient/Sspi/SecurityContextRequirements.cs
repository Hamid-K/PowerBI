using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000119 RID: 281
	[Flags]
	internal enum SecurityContextRequirements
	{
		// Token: 0x040009BE RID: 2494
		None = 0,
		// Token: 0x040009BF RID: 2495
		Delegate = 1,
		// Token: 0x040009C0 RID: 2496
		MutualAuth = 2,
		// Token: 0x040009C1 RID: 2497
		ReplayDetect = 4,
		// Token: 0x040009C2 RID: 2498
		SequenceDetect = 8,
		// Token: 0x040009C3 RID: 2499
		Confidentiality = 16,
		// Token: 0x040009C4 RID: 2500
		UseSessionKey = 32,
		// Token: 0x040009C5 RID: 2501
		PromptForCreds = 64,
		// Token: 0x040009C6 RID: 2502
		UsedCollectedCreds = 64,
		// Token: 0x040009C7 RID: 2503
		SessionTicket = 64,
		// Token: 0x040009C8 RID: 2504
		UseSuppliedCreds = 128,
		// Token: 0x040009C9 RID: 2505
		UsedSuppliedCreds = 128,
		// Token: 0x040009CA RID: 2506
		AllocateMemory = 256,
		// Token: 0x040009CB RID: 2507
		UseDceStyle = 512,
		// Token: 0x040009CC RID: 2508
		UsedDceStyle = 512,
		// Token: 0x040009CD RID: 2509
		Datagram = 1024,
		// Token: 0x040009CE RID: 2510
		Connection = 2048,
		// Token: 0x040009CF RID: 2511
		CallLevel = 4096,
		// Token: 0x040009D0 RID: 2512
		IntermediateReturn = 4096,
		// Token: 0x040009D1 RID: 2513
		FragmentSupplied = 8192,
		// Token: 0x040009D2 RID: 2514
		UsedCallLevel = 8192,
		// Token: 0x040009D3 RID: 2515
		ExtendedError = 16384,
		// Token: 0x040009D4 RID: 2516
		ThirdLegFailed = 16384,
		// Token: 0x040009D5 RID: 2517
		Stream = 32768,
		// Token: 0x040009D6 RID: 2518
		AcceptExtendedError = 32768,
		// Token: 0x040009D7 RID: 2519
		Integrity = 65536,
		// Token: 0x040009D8 RID: 2520
		AcceptStream = 65536,
		// Token: 0x040009D9 RID: 2521
		Identity = 131072,
		// Token: 0x040009DA RID: 2522
		AcceptIntegrity = 131072,
		// Token: 0x040009DB RID: 2523
		NullSession = 262144,
		// Token: 0x040009DC RID: 2524
		Licensing = 262144,
		// Token: 0x040009DD RID: 2525
		ManualCredValidation = 524288,
		// Token: 0x040009DE RID: 2526
		AcceptIdentity = 524288,
		// Token: 0x040009DF RID: 2527
		Reserved1 = 1048576,
		// Token: 0x040009E0 RID: 2528
		AllowNullSession = 1048576,
		// Token: 0x040009E1 RID: 2529
		FragmentToFit = 2097152,
		// Token: 0x040009E2 RID: 2530
		FragmentOnly = 2097152,
		// Token: 0x040009E3 RID: 2531
		AllowNonUserLogons = 2097152,
		// Token: 0x040009E4 RID: 2532
		ForwardCredentials = 4194304,
		// Token: 0x040009E5 RID: 2533
		AllowContextReplay = 4194304,
		// Token: 0x040009E6 RID: 2534
		NoIntegrity = 8388608,
		// Token: 0x040009E7 RID: 2535
		AcceptFragmentToFit = 8388608,
		// Token: 0x040009E8 RID: 2536
		AcceptFragmentOnly = 8388608,
		// Token: 0x040009E9 RID: 2537
		UseHttpStyle = 16777216,
		// Token: 0x040009EA RID: 2538
		UsedHttpStyle = 16777216,
		// Token: 0x040009EB RID: 2539
		NoToken = 16777216,
		// Token: 0x040009EC RID: 2540
		NoAdditionalToken = 33554432,
		// Token: 0x040009ED RID: 2541
		ProxyBinding = 67108864,
		// Token: 0x040009EE RID: 2542
		Reauthentication = 134217728,
		// Token: 0x040009EF RID: 2543
		AllowMissingBindings = 268435456,
		// Token: 0x040009F0 RID: 2544
		UnverifiedTargetName = 536870912,
		// Token: 0x040009F1 RID: 2545
		ConfidentialityOnly = 1073741824
	}
}
