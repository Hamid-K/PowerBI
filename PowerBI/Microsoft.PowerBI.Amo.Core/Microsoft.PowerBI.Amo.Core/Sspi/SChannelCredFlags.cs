using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000108 RID: 264
	[Flags]
	internal enum SChannelCredFlags
	{
		// Token: 0x040008CA RID: 2250
		None = 0,
		// Token: 0x040008CB RID: 2251
		NoSystemMapper = 2,
		// Token: 0x040008CC RID: 2252
		NoServernameCheck = 4,
		// Token: 0x040008CD RID: 2253
		ManualCredValidation = 8,
		// Token: 0x040008CE RID: 2254
		NoDefaultCreds = 16,
		// Token: 0x040008CF RID: 2255
		AutoCredValidation = 32,
		// Token: 0x040008D0 RID: 2256
		UseDeafultCreds = 64,
		// Token: 0x040008D1 RID: 2257
		DisableReconnects = 128,
		// Token: 0x040008D2 RID: 2258
		RevocationCheckEndCert = 256,
		// Token: 0x040008D3 RID: 2259
		RevocationCheckChain = 512,
		// Token: 0x040008D4 RID: 2260
		RevocationCheckChainExcludeRoot = 1024,
		// Token: 0x040008D5 RID: 2261
		IgnoreNoRevocationCheck = 2048,
		// Token: 0x040008D6 RID: 2262
		IgnoreRevocationOffline = 4096,
		// Token: 0x040008D7 RID: 2263
		RestrictedRoots = 8192,
		// Token: 0x040008D8 RID: 2264
		RevocationCheckCacheOnly = 16384,
		// Token: 0x040008D9 RID: 2265
		CacheOnlyUrlRetrieval = 32768,
		// Token: 0x040008DA RID: 2266
		MemoryStoreCert = 65536,
		// Token: 0x040008DB RID: 2267
		CacheOnlyUrlRetrievalNoCreate = 131072,
		// Token: 0x040008DC RID: 2268
		RootCert = 262144,
		// Token: 0x040008DD RID: 2269
		SniCredential = 524288,
		// Token: 0x040008DE RID: 2270
		SniEnableOcsp = 1048576,
		// Token: 0x040008DF RID: 2271
		SendAuxRecord = 2097152,
		// Token: 0x040008E0 RID: 2272
		UseStrongCrypto = 4194304,
		// Token: 0x040008E1 RID: 2273
		UsePresharedKeyOnly = 8388608
	}
}
