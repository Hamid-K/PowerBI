using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000113 RID: 275
	[Flags]
	internal enum SChannelCredFlags
	{
		// Token: 0x04000911 RID: 2321
		None = 0,
		// Token: 0x04000912 RID: 2322
		NoSystemMapper = 2,
		// Token: 0x04000913 RID: 2323
		NoServernameCheck = 4,
		// Token: 0x04000914 RID: 2324
		ManualCredValidation = 8,
		// Token: 0x04000915 RID: 2325
		NoDefaultCreds = 16,
		// Token: 0x04000916 RID: 2326
		AutoCredValidation = 32,
		// Token: 0x04000917 RID: 2327
		UseDeafultCreds = 64,
		// Token: 0x04000918 RID: 2328
		DisableReconnects = 128,
		// Token: 0x04000919 RID: 2329
		RevocationCheckEndCert = 256,
		// Token: 0x0400091A RID: 2330
		RevocationCheckChain = 512,
		// Token: 0x0400091B RID: 2331
		RevocationCheckChainExcludeRoot = 1024,
		// Token: 0x0400091C RID: 2332
		IgnoreNoRevocationCheck = 2048,
		// Token: 0x0400091D RID: 2333
		IgnoreRevocationOffline = 4096,
		// Token: 0x0400091E RID: 2334
		RestrictedRoots = 8192,
		// Token: 0x0400091F RID: 2335
		RevocationCheckCacheOnly = 16384,
		// Token: 0x04000920 RID: 2336
		CacheOnlyUrlRetrieval = 32768,
		// Token: 0x04000921 RID: 2337
		MemoryStoreCert = 65536,
		// Token: 0x04000922 RID: 2338
		CacheOnlyUrlRetrievalNoCreate = 131072,
		// Token: 0x04000923 RID: 2339
		RootCert = 262144,
		// Token: 0x04000924 RID: 2340
		SniCredential = 524288,
		// Token: 0x04000925 RID: 2341
		SniEnableOcsp = 1048576,
		// Token: 0x04000926 RID: 2342
		SendAuxRecord = 2097152,
		// Token: 0x04000927 RID: 2343
		UseStrongCrypto = 4194304,
		// Token: 0x04000928 RID: 2344
		UsePresharedKeyOnly = 8388608
	}
}
