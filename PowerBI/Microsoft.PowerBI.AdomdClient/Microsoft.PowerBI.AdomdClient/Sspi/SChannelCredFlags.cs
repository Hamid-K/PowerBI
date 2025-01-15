using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000113 RID: 275
	[Flags]
	internal enum SChannelCredFlags
	{
		// Token: 0x04000904 RID: 2308
		None = 0,
		// Token: 0x04000905 RID: 2309
		NoSystemMapper = 2,
		// Token: 0x04000906 RID: 2310
		NoServernameCheck = 4,
		// Token: 0x04000907 RID: 2311
		ManualCredValidation = 8,
		// Token: 0x04000908 RID: 2312
		NoDefaultCreds = 16,
		// Token: 0x04000909 RID: 2313
		AutoCredValidation = 32,
		// Token: 0x0400090A RID: 2314
		UseDeafultCreds = 64,
		// Token: 0x0400090B RID: 2315
		DisableReconnects = 128,
		// Token: 0x0400090C RID: 2316
		RevocationCheckEndCert = 256,
		// Token: 0x0400090D RID: 2317
		RevocationCheckChain = 512,
		// Token: 0x0400090E RID: 2318
		RevocationCheckChainExcludeRoot = 1024,
		// Token: 0x0400090F RID: 2319
		IgnoreNoRevocationCheck = 2048,
		// Token: 0x04000910 RID: 2320
		IgnoreRevocationOffline = 4096,
		// Token: 0x04000911 RID: 2321
		RestrictedRoots = 8192,
		// Token: 0x04000912 RID: 2322
		RevocationCheckCacheOnly = 16384,
		// Token: 0x04000913 RID: 2323
		CacheOnlyUrlRetrieval = 32768,
		// Token: 0x04000914 RID: 2324
		MemoryStoreCert = 65536,
		// Token: 0x04000915 RID: 2325
		CacheOnlyUrlRetrievalNoCreate = 131072,
		// Token: 0x04000916 RID: 2326
		RootCert = 262144,
		// Token: 0x04000917 RID: 2327
		SniCredential = 524288,
		// Token: 0x04000918 RID: 2328
		SniEnableOcsp = 1048576,
		// Token: 0x04000919 RID: 2329
		SendAuxRecord = 2097152,
		// Token: 0x0400091A RID: 2330
		UseStrongCrypto = 4194304,
		// Token: 0x0400091B RID: 2331
		UsePresharedKeyOnly = 8388608
	}
}
