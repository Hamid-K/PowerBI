using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000118 RID: 280
	internal enum SecurityContextAttributes
	{
		// Token: 0x04000985 RID: 2437
		Sizes,
		// Token: 0x04000986 RID: 2438
		Names,
		// Token: 0x04000987 RID: 2439
		Lifespan,
		// Token: 0x04000988 RID: 2440
		Info,
		// Token: 0x04000989 RID: 2441
		StreamSizes,
		// Token: 0x0400098A RID: 2442
		DatagramSizes = 4,
		// Token: 0x0400098B RID: 2443
		KeyInfo,
		// Token: 0x0400098C RID: 2444
		Authority,
		// Token: 0x0400098D RID: 2445
		ProtoInfo,
		// Token: 0x0400098E RID: 2446
		PasswordExpiry,
		// Token: 0x0400098F RID: 2447
		SessionKey,
		// Token: 0x04000990 RID: 2448
		PackageInfo,
		// Token: 0x04000991 RID: 2449
		UserFlags,
		// Token: 0x04000992 RID: 2450
		NegotiationInfo,
		// Token: 0x04000993 RID: 2451
		NativeNames,
		// Token: 0x04000994 RID: 2452
		Flags,
		// Token: 0x04000995 RID: 2453
		UseValidated,
		// Token: 0x04000996 RID: 2454
		CredentialName,
		// Token: 0x04000997 RID: 2455
		TargetInformation,
		// Token: 0x04000998 RID: 2456
		AccessToken,
		// Token: 0x04000999 RID: 2457
		Target,
		// Token: 0x0400099A RID: 2458
		AuthenticationId,
		// Token: 0x0400099B RID: 2459
		LogoffTime,
		// Token: 0x0400099C RID: 2460
		NegoKeys,
		// Token: 0x0400099D RID: 2461
		PromptingNeeded = 24,
		// Token: 0x0400099E RID: 2462
		UniqueBindings,
		// Token: 0x0400099F RID: 2463
		EndpointBindings,
		// Token: 0x040009A0 RID: 2464
		ClientSpecifiedTarget,
		// Token: 0x040009A1 RID: 2465
		LastClientTokenStatus = 30,
		// Token: 0x040009A2 RID: 2466
		NegoPkgInfo,
		// Token: 0x040009A3 RID: 2467
		NegoStatus,
		// Token: 0x040009A4 RID: 2468
		ContextDeleted,
		// Token: 0x040009A5 RID: 2469
		DtlsMtu,
		// Token: 0x040009A6 RID: 2470
		ApplicationProtocol,
		// Token: 0x040009A7 RID: 2471
		NegotiatedTlsExtensions,
		// Token: 0x040009A8 RID: 2472
		IsLoopback,
		// Token: 0x040009A9 RID: 2473
		IssuerList = 80,
		// Token: 0x040009AA RID: 2474
		RemoteCred,
		// Token: 0x040009AB RID: 2475
		LocalCred,
		// Token: 0x040009AC RID: 2476
		RemoteCertContext,
		// Token: 0x040009AD RID: 2477
		LocalCertContext,
		// Token: 0x040009AE RID: 2478
		RootStore,
		// Token: 0x040009AF RID: 2479
		SupportedAlgs,
		// Token: 0x040009B0 RID: 2480
		CipherStrengths,
		// Token: 0x040009B1 RID: 2481
		SupportedProtocols,
		// Token: 0x040009B2 RID: 2482
		IssuerListEx,
		// Token: 0x040009B3 RID: 2483
		ConnectionInfo,
		// Token: 0x040009B4 RID: 2484
		EapKeyBlock,
		// Token: 0x040009B5 RID: 2485
		MappedCredAttr,
		// Token: 0x040009B6 RID: 2486
		SessionInfo,
		// Token: 0x040009B7 RID: 2487
		AppData,
		// Token: 0x040009B8 RID: 2488
		RemoteCertificates,
		// Token: 0x040009B9 RID: 2489
		ClientCertPolicy,
		// Token: 0x040009BA RID: 2490
		CcPlicyResult,
		// Token: 0x040009BB RID: 2491
		UseNcrypt,
		// Token: 0x040009BC RID: 2492
		LocalCertInfo,
		// Token: 0x040009BD RID: 2493
		CipherInfo,
		// Token: 0x040009BE RID: 2494
		EapPrfInfo,
		// Token: 0x040009BF RID: 2495
		SupportedSignatures,
		// Token: 0x040009C0 RID: 2496
		RemoteCertChain,
		// Token: 0x040009C1 RID: 2497
		UiInfo,
		// Token: 0x040009C2 RID: 2498
		EarlyStart,
		// Token: 0x040009C3 RID: 2499
		KeyingMaterialInfo,
		// Token: 0x040009C4 RID: 2500
		KeyingMaterial,
		// Token: 0x040009C5 RID: 2501
		SrtpParameters,
		// Token: 0x040009C6 RID: 2502
		TokenBinding,
		// Token: 0x040009C7 RID: 2503
		ConnectionInfoEx,
		// Token: 0x040009C8 RID: 2504
		KeyingMaterialTokenBinding,
		// Token: 0x040009C9 RID: 2505
		SubjectSecurityAttributes = 128
	}
}
