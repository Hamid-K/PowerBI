using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000114 RID: 276
	[Flags]
	internal enum SChannelProtocols : uint
	{
		// Token: 0x0400091D RID: 2333
		None = 0U,
		// Token: 0x0400091E RID: 2334
		Pct1Server = 1U,
		// Token: 0x0400091F RID: 2335
		Pct1Client = 2U,
		// Token: 0x04000920 RID: 2336
		Pct1 = 3U,
		// Token: 0x04000921 RID: 2337
		Ssl2Server = 4U,
		// Token: 0x04000922 RID: 2338
		Ssl2Client = 8U,
		// Token: 0x04000923 RID: 2339
		Ssl2 = 12U,
		// Token: 0x04000924 RID: 2340
		Ssl3Server = 16U,
		// Token: 0x04000925 RID: 2341
		Ssl3Client = 32U,
		// Token: 0x04000926 RID: 2342
		Ssl3 = 48U,
		// Token: 0x04000927 RID: 2343
		Tls1Server = 64U,
		// Token: 0x04000928 RID: 2344
		Tls1Client = 128U,
		// Token: 0x04000929 RID: 2345
		Tls1 = 192U,
		// Token: 0x0400092A RID: 2346
		Ssl3Tls1Clients = 160U,
		// Token: 0x0400092B RID: 2347
		Ssl3Tls1Servers = 80U,
		// Token: 0x0400092C RID: 2348
		Ssl3Tls1 = 240U,
		// Token: 0x0400092D RID: 2349
		UniServer = 1073741824U,
		// Token: 0x0400092E RID: 2350
		UniClient = 2147483648U,
		// Token: 0x0400092F RID: 2351
		Uni = 3221225472U,
		// Token: 0x04000930 RID: 2352
		Clients = 2147483818U,
		// Token: 0x04000931 RID: 2353
		Servers = 1073741909U,
		// Token: 0x04000932 RID: 2354
		Tls1Server_0 = 64U,
		// Token: 0x04000933 RID: 2355
		Tls1Client_0 = 128U,
		// Token: 0x04000934 RID: 2356
		Tls1_0 = 192U,
		// Token: 0x04000935 RID: 2357
		Tls1Server_1 = 256U,
		// Token: 0x04000936 RID: 2358
		Tls1Client_1 = 512U,
		// Token: 0x04000937 RID: 2359
		Tls1_1 = 768U,
		// Token: 0x04000938 RID: 2360
		Tls1Server_2 = 1024U,
		// Token: 0x04000939 RID: 2361
		Tls1Client_2 = 2048U,
		// Token: 0x0400093A RID: 2362
		Tls1_2 = 3072U,
		// Token: 0x0400093B RID: 2363
		Tls1Server_3 = 4096U,
		// Token: 0x0400093C RID: 2364
		Tls1Client_3 = 8192U,
		// Token: 0x0400093D RID: 2365
		Tls1_3 = 12288U,
		// Token: 0x0400093E RID: 2366
		DtlsServer = 65536U,
		// Token: 0x0400093F RID: 2367
		DtlsClient = 131072U,
		// Token: 0x04000940 RID: 2368
		Dtls = 196608U,
		// Token: 0x04000941 RID: 2369
		Dtls1Server_0 = 65536U,
		// Token: 0x04000942 RID: 2370
		Dtls1Client_0 = 131072U,
		// Token: 0x04000943 RID: 2371
		Dtls1_0 = 196608U,
		// Token: 0x04000944 RID: 2372
		Dtls1Server_2 = 262144U,
		// Token: 0x04000945 RID: 2373
		Dtls1Client_2 = 524288U,
		// Token: 0x04000946 RID: 2374
		Dtls1_2 = 786432U,
		// Token: 0x04000947 RID: 2375
		Dtls1Server_X = 327680U,
		// Token: 0x04000948 RID: 2376
		Dtls1Client_X = 655360U,
		// Token: 0x04000949 RID: 2377
		Dtls1_X = 983040U,
		// Token: 0x0400094A RID: 2378
		Tls1Server_1Plus = 5376U,
		// Token: 0x0400094B RID: 2379
		Tls1Client_1Plus = 10752U,
		// Token: 0x0400094C RID: 2380
		Tls1_1Plus = 16128U,
		// Token: 0x0400094D RID: 2381
		Tls1Server_X = 5440U,
		// Token: 0x0400094E RID: 2382
		Tls1Client_X = 10880U,
		// Token: 0x0400094F RID: 2383
		Tls1_X = 16320U,
		// Token: 0x04000950 RID: 2384
		Ssl3Tls1Clients_X = 10912U,
		// Token: 0x04000951 RID: 2385
		Ssl3Tls1Servers_X = 5456U,
		// Token: 0x04000952 RID: 2386
		Ssl3Tls1_X = 16368U,
		// Token: 0x04000953 RID: 2387
		Clients_X = 2148149930U,
		// Token: 0x04000954 RID: 2388
		Servers_X = 1074074965U,
		// Token: 0x04000955 RID: 2389
		All = 4294967295U
	}
}
