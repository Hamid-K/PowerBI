using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000314 RID: 788
	internal static class CacheStringSubstatus
	{
		// Token: 0x04000FE4 RID: 4068
		internal const string None = "ES0001";

		// Token: 0x04000FE5 RID: 4069
		internal const string NotPrimary = "ES0002";

		// Token: 0x04000FE6 RID: 4070
		internal const string NoWriteQuorum = "ES0003";

		// Token: 0x04000FE7 RID: 4071
		internal const string ReplicationQueueFull = "ES0004";

		// Token: 0x04000FE8 RID: 4072
		internal const string KeyLatched = "ES0005";

		// Token: 0x04000FE9 RID: 4073
		internal const string CacheServerUnavailable = "ES0006";

		// Token: 0x04000FEA RID: 4074
		internal const string Throttled = "ES0007";

		// Token: 0x04000FEB RID: 4075
		internal const string QuotaExceeded = "ES0009";

		// Token: 0x04000FEC RID: 4076
		internal const string ReadThroughKeyContention = "ES0008";

		// Token: 0x04000FED RID: 4077
		internal const string InternalError = "ES0010";

		// Token: 0x04000FEE RID: 4078
		internal const string ReplicationFailed = "ES0011";

		// Token: 0x04000FEF RID: 4079
		internal const string TagTooLarge = "ES0012";

		// Token: 0x04000FF0 RID: 4080
		internal const string KeyTooLarge = "ES0013";

		// Token: 0x04000FF1 RID: 4081
		internal const string RegionTooLarge = "ES0014";

		// Token: 0x04000FF2 RID: 4082
		internal const string CacheUnderReconfiguration = "ES0015";

		// Token: 0x04000FF3 RID: 4083
		internal const string CertificateRevocationServerOffline = "ES0016";

		// Token: 0x04000FF4 RID: 4084
		internal const string ErrorLoadingGlobalConfigSection = "ES0100";
	}
}
