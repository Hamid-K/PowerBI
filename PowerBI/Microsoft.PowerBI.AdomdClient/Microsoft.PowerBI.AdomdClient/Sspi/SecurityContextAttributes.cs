using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000118 RID: 280
	internal enum SecurityContextAttributes
	{
		// Token: 0x04000978 RID: 2424
		Sizes,
		// Token: 0x04000979 RID: 2425
		Names,
		// Token: 0x0400097A RID: 2426
		Lifespan,
		// Token: 0x0400097B RID: 2427
		Info,
		// Token: 0x0400097C RID: 2428
		StreamSizes,
		// Token: 0x0400097D RID: 2429
		DatagramSizes = 4,
		// Token: 0x0400097E RID: 2430
		KeyInfo,
		// Token: 0x0400097F RID: 2431
		Authority,
		// Token: 0x04000980 RID: 2432
		ProtoInfo,
		// Token: 0x04000981 RID: 2433
		PasswordExpiry,
		// Token: 0x04000982 RID: 2434
		SessionKey,
		// Token: 0x04000983 RID: 2435
		PackageInfo,
		// Token: 0x04000984 RID: 2436
		UserFlags,
		// Token: 0x04000985 RID: 2437
		NegotiationInfo,
		// Token: 0x04000986 RID: 2438
		NativeNames,
		// Token: 0x04000987 RID: 2439
		Flags,
		// Token: 0x04000988 RID: 2440
		UseValidated,
		// Token: 0x04000989 RID: 2441
		CredentialName,
		// Token: 0x0400098A RID: 2442
		TargetInformation,
		// Token: 0x0400098B RID: 2443
		AccessToken,
		// Token: 0x0400098C RID: 2444
		Target,
		// Token: 0x0400098D RID: 2445
		AuthenticationId,
		// Token: 0x0400098E RID: 2446
		LogoffTime,
		// Token: 0x0400098F RID: 2447
		NegoKeys,
		// Token: 0x04000990 RID: 2448
		PromptingNeeded = 24,
		// Token: 0x04000991 RID: 2449
		UniqueBindings,
		// Token: 0x04000992 RID: 2450
		EndpointBindings,
		// Token: 0x04000993 RID: 2451
		ClientSpecifiedTarget,
		// Token: 0x04000994 RID: 2452
		LastClientTokenStatus = 30,
		// Token: 0x04000995 RID: 2453
		NegoPkgInfo,
		// Token: 0x04000996 RID: 2454
		NegoStatus,
		// Token: 0x04000997 RID: 2455
		ContextDeleted,
		// Token: 0x04000998 RID: 2456
		DtlsMtu,
		// Token: 0x04000999 RID: 2457
		ApplicationProtocol,
		// Token: 0x0400099A RID: 2458
		NegotiatedTlsExtensions,
		// Token: 0x0400099B RID: 2459
		IsLoopback,
		// Token: 0x0400099C RID: 2460
		IssuerList = 80,
		// Token: 0x0400099D RID: 2461
		RemoteCred,
		// Token: 0x0400099E RID: 2462
		LocalCred,
		// Token: 0x0400099F RID: 2463
		RemoteCertContext,
		// Token: 0x040009A0 RID: 2464
		LocalCertContext,
		// Token: 0x040009A1 RID: 2465
		RootStore,
		// Token: 0x040009A2 RID: 2466
		SupportedAlgs,
		// Token: 0x040009A3 RID: 2467
		CipherStrengths,
		// Token: 0x040009A4 RID: 2468
		SupportedProtocols,
		// Token: 0x040009A5 RID: 2469
		IssuerListEx,
		// Token: 0x040009A6 RID: 2470
		ConnectionInfo,
		// Token: 0x040009A7 RID: 2471
		EapKeyBlock,
		// Token: 0x040009A8 RID: 2472
		MappedCredAttr,
		// Token: 0x040009A9 RID: 2473
		SessionInfo,
		// Token: 0x040009AA RID: 2474
		AppData,
		// Token: 0x040009AB RID: 2475
		RemoteCertificates,
		// Token: 0x040009AC RID: 2476
		ClientCertPolicy,
		// Token: 0x040009AD RID: 2477
		CcPlicyResult,
		// Token: 0x040009AE RID: 2478
		UseNcrypt,
		// Token: 0x040009AF RID: 2479
		LocalCertInfo,
		// Token: 0x040009B0 RID: 2480
		CipherInfo,
		// Token: 0x040009B1 RID: 2481
		EapPrfInfo,
		// Token: 0x040009B2 RID: 2482
		SupportedSignatures,
		// Token: 0x040009B3 RID: 2483
		RemoteCertChain,
		// Token: 0x040009B4 RID: 2484
		UiInfo,
		// Token: 0x040009B5 RID: 2485
		EarlyStart,
		// Token: 0x040009B6 RID: 2486
		KeyingMaterialInfo,
		// Token: 0x040009B7 RID: 2487
		KeyingMaterial,
		// Token: 0x040009B8 RID: 2488
		SrtpParameters,
		// Token: 0x040009B9 RID: 2489
		TokenBinding,
		// Token: 0x040009BA RID: 2490
		ConnectionInfoEx,
		// Token: 0x040009BB RID: 2491
		KeyingMaterialTokenBinding,
		// Token: 0x040009BC RID: 2492
		SubjectSecurityAttributes = 128
	}
}
