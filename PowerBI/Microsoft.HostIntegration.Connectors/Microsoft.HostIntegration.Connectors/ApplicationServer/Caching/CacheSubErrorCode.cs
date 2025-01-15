using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A0 RID: 928
	internal enum CacheSubErrorCode
	{
		// Token: 0x0400138B RID: 5003
		None,
		// Token: 0x0400138C RID: 5004
		CacheServerUnavailable,
		// Token: 0x0400138D RID: 5005
		KeyLatched,
		// Token: 0x0400138E RID: 5006
		ReplicationQueueFull,
		// Token: 0x0400138F RID: 5007
		ServiceMemoryShortage,
		// Token: 0x04001390 RID: 5008
		ReadThroughKeyContention,
		// Token: 0x04001391 RID: 5009
		Throttled,
		// Token: 0x04001392 RID: 5010
		QuotaExceeded,
		// Token: 0x04001393 RID: 5011
		NoWriteQuorum,
		// Token: 0x04001394 RID: 5012
		NotPrimary,
		// Token: 0x04001395 RID: 5013
		CacheUnderReconfiguration,
		// Token: 0x04001396 RID: 5014
		CertificateRevocationServerOffline
	}
}
