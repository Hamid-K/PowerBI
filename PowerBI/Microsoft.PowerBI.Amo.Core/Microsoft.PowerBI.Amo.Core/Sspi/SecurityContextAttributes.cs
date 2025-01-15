using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010D RID: 269
	internal enum SecurityContextAttributes
	{
		// Token: 0x0400093E RID: 2366
		Sizes,
		// Token: 0x0400093F RID: 2367
		Names,
		// Token: 0x04000940 RID: 2368
		Lifespan,
		// Token: 0x04000941 RID: 2369
		Info,
		// Token: 0x04000942 RID: 2370
		StreamSizes,
		// Token: 0x04000943 RID: 2371
		DatagramSizes = 4,
		// Token: 0x04000944 RID: 2372
		KeyInfo,
		// Token: 0x04000945 RID: 2373
		Authority,
		// Token: 0x04000946 RID: 2374
		ProtoInfo,
		// Token: 0x04000947 RID: 2375
		PasswordExpiry,
		// Token: 0x04000948 RID: 2376
		SessionKey,
		// Token: 0x04000949 RID: 2377
		PackageInfo,
		// Token: 0x0400094A RID: 2378
		UserFlags,
		// Token: 0x0400094B RID: 2379
		NegotiationInfo,
		// Token: 0x0400094C RID: 2380
		NativeNames,
		// Token: 0x0400094D RID: 2381
		Flags,
		// Token: 0x0400094E RID: 2382
		UseValidated,
		// Token: 0x0400094F RID: 2383
		CredentialName,
		// Token: 0x04000950 RID: 2384
		TargetInformation,
		// Token: 0x04000951 RID: 2385
		AccessToken,
		// Token: 0x04000952 RID: 2386
		Target,
		// Token: 0x04000953 RID: 2387
		AuthenticationId,
		// Token: 0x04000954 RID: 2388
		LogoffTime,
		// Token: 0x04000955 RID: 2389
		NegoKeys,
		// Token: 0x04000956 RID: 2390
		PromptingNeeded = 24,
		// Token: 0x04000957 RID: 2391
		UniqueBindings,
		// Token: 0x04000958 RID: 2392
		EndpointBindings,
		// Token: 0x04000959 RID: 2393
		ClientSpecifiedTarget,
		// Token: 0x0400095A RID: 2394
		LastClientTokenStatus = 30,
		// Token: 0x0400095B RID: 2395
		NegoPkgInfo,
		// Token: 0x0400095C RID: 2396
		NegoStatus,
		// Token: 0x0400095D RID: 2397
		ContextDeleted,
		// Token: 0x0400095E RID: 2398
		DtlsMtu,
		// Token: 0x0400095F RID: 2399
		ApplicationProtocol,
		// Token: 0x04000960 RID: 2400
		NegotiatedTlsExtensions,
		// Token: 0x04000961 RID: 2401
		IsLoopback,
		// Token: 0x04000962 RID: 2402
		IssuerList = 80,
		// Token: 0x04000963 RID: 2403
		RemoteCred,
		// Token: 0x04000964 RID: 2404
		LocalCred,
		// Token: 0x04000965 RID: 2405
		RemoteCertContext,
		// Token: 0x04000966 RID: 2406
		LocalCertContext,
		// Token: 0x04000967 RID: 2407
		RootStore,
		// Token: 0x04000968 RID: 2408
		SupportedAlgs,
		// Token: 0x04000969 RID: 2409
		CipherStrengths,
		// Token: 0x0400096A RID: 2410
		SupportedProtocols,
		// Token: 0x0400096B RID: 2411
		IssuerListEx,
		// Token: 0x0400096C RID: 2412
		ConnectionInfo,
		// Token: 0x0400096D RID: 2413
		EapKeyBlock,
		// Token: 0x0400096E RID: 2414
		MappedCredAttr,
		// Token: 0x0400096F RID: 2415
		SessionInfo,
		// Token: 0x04000970 RID: 2416
		AppData,
		// Token: 0x04000971 RID: 2417
		RemoteCertificates,
		// Token: 0x04000972 RID: 2418
		ClientCertPolicy,
		// Token: 0x04000973 RID: 2419
		CcPlicyResult,
		// Token: 0x04000974 RID: 2420
		UseNcrypt,
		// Token: 0x04000975 RID: 2421
		LocalCertInfo,
		// Token: 0x04000976 RID: 2422
		CipherInfo,
		// Token: 0x04000977 RID: 2423
		EapPrfInfo,
		// Token: 0x04000978 RID: 2424
		SupportedSignatures,
		// Token: 0x04000979 RID: 2425
		RemoteCertChain,
		// Token: 0x0400097A RID: 2426
		UiInfo,
		// Token: 0x0400097B RID: 2427
		EarlyStart,
		// Token: 0x0400097C RID: 2428
		KeyingMaterialInfo,
		// Token: 0x0400097D RID: 2429
		KeyingMaterial,
		// Token: 0x0400097E RID: 2430
		SrtpParameters,
		// Token: 0x0400097F RID: 2431
		TokenBinding,
		// Token: 0x04000980 RID: 2432
		ConnectionInfoEx,
		// Token: 0x04000981 RID: 2433
		KeyingMaterialTokenBinding,
		// Token: 0x04000982 RID: 2434
		SubjectSecurityAttributes = 128
	}
}
