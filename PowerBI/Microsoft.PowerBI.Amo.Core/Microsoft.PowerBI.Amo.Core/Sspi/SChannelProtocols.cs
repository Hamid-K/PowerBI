using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000109 RID: 265
	[Flags]
	internal enum SChannelProtocols : uint
	{
		// Token: 0x040008E3 RID: 2275
		None = 0U,
		// Token: 0x040008E4 RID: 2276
		Pct1Server = 1U,
		// Token: 0x040008E5 RID: 2277
		Pct1Client = 2U,
		// Token: 0x040008E6 RID: 2278
		Pct1 = 3U,
		// Token: 0x040008E7 RID: 2279
		Ssl2Server = 4U,
		// Token: 0x040008E8 RID: 2280
		Ssl2Client = 8U,
		// Token: 0x040008E9 RID: 2281
		Ssl2 = 12U,
		// Token: 0x040008EA RID: 2282
		Ssl3Server = 16U,
		// Token: 0x040008EB RID: 2283
		Ssl3Client = 32U,
		// Token: 0x040008EC RID: 2284
		Ssl3 = 48U,
		// Token: 0x040008ED RID: 2285
		Tls1Server = 64U,
		// Token: 0x040008EE RID: 2286
		Tls1Client = 128U,
		// Token: 0x040008EF RID: 2287
		Tls1 = 192U,
		// Token: 0x040008F0 RID: 2288
		Ssl3Tls1Clients = 160U,
		// Token: 0x040008F1 RID: 2289
		Ssl3Tls1Servers = 80U,
		// Token: 0x040008F2 RID: 2290
		Ssl3Tls1 = 240U,
		// Token: 0x040008F3 RID: 2291
		UniServer = 1073741824U,
		// Token: 0x040008F4 RID: 2292
		UniClient = 2147483648U,
		// Token: 0x040008F5 RID: 2293
		Uni = 3221225472U,
		// Token: 0x040008F6 RID: 2294
		Clients = 2147483818U,
		// Token: 0x040008F7 RID: 2295
		Servers = 1073741909U,
		// Token: 0x040008F8 RID: 2296
		Tls1Server_0 = 64U,
		// Token: 0x040008F9 RID: 2297
		Tls1Client_0 = 128U,
		// Token: 0x040008FA RID: 2298
		Tls1_0 = 192U,
		// Token: 0x040008FB RID: 2299
		Tls1Server_1 = 256U,
		// Token: 0x040008FC RID: 2300
		Tls1Client_1 = 512U,
		// Token: 0x040008FD RID: 2301
		Tls1_1 = 768U,
		// Token: 0x040008FE RID: 2302
		Tls1Server_2 = 1024U,
		// Token: 0x040008FF RID: 2303
		Tls1Client_2 = 2048U,
		// Token: 0x04000900 RID: 2304
		Tls1_2 = 3072U,
		// Token: 0x04000901 RID: 2305
		Tls1Server_3 = 4096U,
		// Token: 0x04000902 RID: 2306
		Tls1Client_3 = 8192U,
		// Token: 0x04000903 RID: 2307
		Tls1_3 = 12288U,
		// Token: 0x04000904 RID: 2308
		DtlsServer = 65536U,
		// Token: 0x04000905 RID: 2309
		DtlsClient = 131072U,
		// Token: 0x04000906 RID: 2310
		Dtls = 196608U,
		// Token: 0x04000907 RID: 2311
		Dtls1Server_0 = 65536U,
		// Token: 0x04000908 RID: 2312
		Dtls1Client_0 = 131072U,
		// Token: 0x04000909 RID: 2313
		Dtls1_0 = 196608U,
		// Token: 0x0400090A RID: 2314
		Dtls1Server_2 = 262144U,
		// Token: 0x0400090B RID: 2315
		Dtls1Client_2 = 524288U,
		// Token: 0x0400090C RID: 2316
		Dtls1_2 = 786432U,
		// Token: 0x0400090D RID: 2317
		Dtls1Server_X = 327680U,
		// Token: 0x0400090E RID: 2318
		Dtls1Client_X = 655360U,
		// Token: 0x0400090F RID: 2319
		Dtls1_X = 983040U,
		// Token: 0x04000910 RID: 2320
		Tls1Server_1Plus = 5376U,
		// Token: 0x04000911 RID: 2321
		Tls1Client_1Plus = 10752U,
		// Token: 0x04000912 RID: 2322
		Tls1_1Plus = 16128U,
		// Token: 0x04000913 RID: 2323
		Tls1Server_X = 5440U,
		// Token: 0x04000914 RID: 2324
		Tls1Client_X = 10880U,
		// Token: 0x04000915 RID: 2325
		Tls1_X = 16320U,
		// Token: 0x04000916 RID: 2326
		Ssl3Tls1Clients_X = 10912U,
		// Token: 0x04000917 RID: 2327
		Ssl3Tls1Servers_X = 5456U,
		// Token: 0x04000918 RID: 2328
		Ssl3Tls1_X = 16368U,
		// Token: 0x04000919 RID: 2329
		Clients_X = 2148149930U,
		// Token: 0x0400091A RID: 2330
		Servers_X = 1074074965U,
		// Token: 0x0400091B RID: 2331
		All = 4294967295U
	}
}
