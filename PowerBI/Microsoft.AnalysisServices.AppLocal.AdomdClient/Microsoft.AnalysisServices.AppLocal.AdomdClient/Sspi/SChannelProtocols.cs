using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000114 RID: 276
	[Flags]
	internal enum SChannelProtocols : uint
	{
		// Token: 0x0400092A RID: 2346
		None = 0U,
		// Token: 0x0400092B RID: 2347
		Pct1Server = 1U,
		// Token: 0x0400092C RID: 2348
		Pct1Client = 2U,
		// Token: 0x0400092D RID: 2349
		Pct1 = 3U,
		// Token: 0x0400092E RID: 2350
		Ssl2Server = 4U,
		// Token: 0x0400092F RID: 2351
		Ssl2Client = 8U,
		// Token: 0x04000930 RID: 2352
		Ssl2 = 12U,
		// Token: 0x04000931 RID: 2353
		Ssl3Server = 16U,
		// Token: 0x04000932 RID: 2354
		Ssl3Client = 32U,
		// Token: 0x04000933 RID: 2355
		Ssl3 = 48U,
		// Token: 0x04000934 RID: 2356
		Tls1Server = 64U,
		// Token: 0x04000935 RID: 2357
		Tls1Client = 128U,
		// Token: 0x04000936 RID: 2358
		Tls1 = 192U,
		// Token: 0x04000937 RID: 2359
		Ssl3Tls1Clients = 160U,
		// Token: 0x04000938 RID: 2360
		Ssl3Tls1Servers = 80U,
		// Token: 0x04000939 RID: 2361
		Ssl3Tls1 = 240U,
		// Token: 0x0400093A RID: 2362
		UniServer = 1073741824U,
		// Token: 0x0400093B RID: 2363
		UniClient = 2147483648U,
		// Token: 0x0400093C RID: 2364
		Uni = 3221225472U,
		// Token: 0x0400093D RID: 2365
		Clients = 2147483818U,
		// Token: 0x0400093E RID: 2366
		Servers = 1073741909U,
		// Token: 0x0400093F RID: 2367
		Tls1Server_0 = 64U,
		// Token: 0x04000940 RID: 2368
		Tls1Client_0 = 128U,
		// Token: 0x04000941 RID: 2369
		Tls1_0 = 192U,
		// Token: 0x04000942 RID: 2370
		Tls1Server_1 = 256U,
		// Token: 0x04000943 RID: 2371
		Tls1Client_1 = 512U,
		// Token: 0x04000944 RID: 2372
		Tls1_1 = 768U,
		// Token: 0x04000945 RID: 2373
		Tls1Server_2 = 1024U,
		// Token: 0x04000946 RID: 2374
		Tls1Client_2 = 2048U,
		// Token: 0x04000947 RID: 2375
		Tls1_2 = 3072U,
		// Token: 0x04000948 RID: 2376
		Tls1Server_3 = 4096U,
		// Token: 0x04000949 RID: 2377
		Tls1Client_3 = 8192U,
		// Token: 0x0400094A RID: 2378
		Tls1_3 = 12288U,
		// Token: 0x0400094B RID: 2379
		DtlsServer = 65536U,
		// Token: 0x0400094C RID: 2380
		DtlsClient = 131072U,
		// Token: 0x0400094D RID: 2381
		Dtls = 196608U,
		// Token: 0x0400094E RID: 2382
		Dtls1Server_0 = 65536U,
		// Token: 0x0400094F RID: 2383
		Dtls1Client_0 = 131072U,
		// Token: 0x04000950 RID: 2384
		Dtls1_0 = 196608U,
		// Token: 0x04000951 RID: 2385
		Dtls1Server_2 = 262144U,
		// Token: 0x04000952 RID: 2386
		Dtls1Client_2 = 524288U,
		// Token: 0x04000953 RID: 2387
		Dtls1_2 = 786432U,
		// Token: 0x04000954 RID: 2388
		Dtls1Server_X = 327680U,
		// Token: 0x04000955 RID: 2389
		Dtls1Client_X = 655360U,
		// Token: 0x04000956 RID: 2390
		Dtls1_X = 983040U,
		// Token: 0x04000957 RID: 2391
		Tls1Server_1Plus = 5376U,
		// Token: 0x04000958 RID: 2392
		Tls1Client_1Plus = 10752U,
		// Token: 0x04000959 RID: 2393
		Tls1_1Plus = 16128U,
		// Token: 0x0400095A RID: 2394
		Tls1Server_X = 5440U,
		// Token: 0x0400095B RID: 2395
		Tls1Client_X = 10880U,
		// Token: 0x0400095C RID: 2396
		Tls1_X = 16320U,
		// Token: 0x0400095D RID: 2397
		Ssl3Tls1Clients_X = 10912U,
		// Token: 0x0400095E RID: 2398
		Ssl3Tls1Servers_X = 5456U,
		// Token: 0x0400095F RID: 2399
		Ssl3Tls1_X = 16368U,
		// Token: 0x04000960 RID: 2400
		Clients_X = 2148149930U,
		// Token: 0x04000961 RID: 2401
		Servers_X = 1074074965U,
		// Token: 0x04000962 RID: 2402
		All = 4294967295U
	}
}
