using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000119 RID: 281
	[Flags]
	internal enum SecurityContextRequirements
	{
		// Token: 0x040009CB RID: 2507
		None = 0,
		// Token: 0x040009CC RID: 2508
		Delegate = 1,
		// Token: 0x040009CD RID: 2509
		MutualAuth = 2,
		// Token: 0x040009CE RID: 2510
		ReplayDetect = 4,
		// Token: 0x040009CF RID: 2511
		SequenceDetect = 8,
		// Token: 0x040009D0 RID: 2512
		Confidentiality = 16,
		// Token: 0x040009D1 RID: 2513
		UseSessionKey = 32,
		// Token: 0x040009D2 RID: 2514
		PromptForCreds = 64,
		// Token: 0x040009D3 RID: 2515
		UsedCollectedCreds = 64,
		// Token: 0x040009D4 RID: 2516
		SessionTicket = 64,
		// Token: 0x040009D5 RID: 2517
		UseSuppliedCreds = 128,
		// Token: 0x040009D6 RID: 2518
		UsedSuppliedCreds = 128,
		// Token: 0x040009D7 RID: 2519
		AllocateMemory = 256,
		// Token: 0x040009D8 RID: 2520
		UseDceStyle = 512,
		// Token: 0x040009D9 RID: 2521
		UsedDceStyle = 512,
		// Token: 0x040009DA RID: 2522
		Datagram = 1024,
		// Token: 0x040009DB RID: 2523
		Connection = 2048,
		// Token: 0x040009DC RID: 2524
		CallLevel = 4096,
		// Token: 0x040009DD RID: 2525
		IntermediateReturn = 4096,
		// Token: 0x040009DE RID: 2526
		FragmentSupplied = 8192,
		// Token: 0x040009DF RID: 2527
		UsedCallLevel = 8192,
		// Token: 0x040009E0 RID: 2528
		ExtendedError = 16384,
		// Token: 0x040009E1 RID: 2529
		ThirdLegFailed = 16384,
		// Token: 0x040009E2 RID: 2530
		Stream = 32768,
		// Token: 0x040009E3 RID: 2531
		AcceptExtendedError = 32768,
		// Token: 0x040009E4 RID: 2532
		Integrity = 65536,
		// Token: 0x040009E5 RID: 2533
		AcceptStream = 65536,
		// Token: 0x040009E6 RID: 2534
		Identity = 131072,
		// Token: 0x040009E7 RID: 2535
		AcceptIntegrity = 131072,
		// Token: 0x040009E8 RID: 2536
		NullSession = 262144,
		// Token: 0x040009E9 RID: 2537
		Licensing = 262144,
		// Token: 0x040009EA RID: 2538
		ManualCredValidation = 524288,
		// Token: 0x040009EB RID: 2539
		AcceptIdentity = 524288,
		// Token: 0x040009EC RID: 2540
		Reserved1 = 1048576,
		// Token: 0x040009ED RID: 2541
		AllowNullSession = 1048576,
		// Token: 0x040009EE RID: 2542
		FragmentToFit = 2097152,
		// Token: 0x040009EF RID: 2543
		FragmentOnly = 2097152,
		// Token: 0x040009F0 RID: 2544
		AllowNonUserLogons = 2097152,
		// Token: 0x040009F1 RID: 2545
		ForwardCredentials = 4194304,
		// Token: 0x040009F2 RID: 2546
		AllowContextReplay = 4194304,
		// Token: 0x040009F3 RID: 2547
		NoIntegrity = 8388608,
		// Token: 0x040009F4 RID: 2548
		AcceptFragmentToFit = 8388608,
		// Token: 0x040009F5 RID: 2549
		AcceptFragmentOnly = 8388608,
		// Token: 0x040009F6 RID: 2550
		UseHttpStyle = 16777216,
		// Token: 0x040009F7 RID: 2551
		UsedHttpStyle = 16777216,
		// Token: 0x040009F8 RID: 2552
		NoToken = 16777216,
		// Token: 0x040009F9 RID: 2553
		NoAdditionalToken = 33554432,
		// Token: 0x040009FA RID: 2554
		ProxyBinding = 67108864,
		// Token: 0x040009FB RID: 2555
		Reauthentication = 134217728,
		// Token: 0x040009FC RID: 2556
		AllowMissingBindings = 268435456,
		// Token: 0x040009FD RID: 2557
		UnverifiedTargetName = 536870912,
		// Token: 0x040009FE RID: 2558
		ConfidentialityOnly = 1073741824
	}
}
