using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000313 RID: 787
	public static class DataCacheErrorSubStatus
	{
		// Token: 0x04000FD2 RID: 4050
		public const int None = -1;

		// Token: 0x04000FD3 RID: 4051
		public const int NotPrimary = 1;

		// Token: 0x04000FD4 RID: 4052
		public const int NoWriteQuorum = 2;

		// Token: 0x04000FD5 RID: 4053
		public const int ReplicationQueueFull = 3;

		// Token: 0x04000FD6 RID: 4054
		public const int KeyLatched = 4;

		// Token: 0x04000FD7 RID: 4055
		public const int CacheServerUnavailable = 5;

		// Token: 0x04000FD8 RID: 4056
		public const int Throttled = 6;

		// Token: 0x04000FD9 RID: 4057
		public const int QuotaExceeded = 9;

		// Token: 0x04000FDA RID: 4058
		public const int ReadThroughKeyContention = 7;

		// Token: 0x04000FDB RID: 4059
		public const int ServiceMemoryShortage = 8;

		// Token: 0x04000FDC RID: 4060
		public const int InternalError = 10;

		// Token: 0x04000FDD RID: 4061
		public const int ReplicationFailed = 11;

		// Token: 0x04000FDE RID: 4062
		public const int TagTooLarge = 12;

		// Token: 0x04000FDF RID: 4063
		public const int KeyTooLarge = 13;

		// Token: 0x04000FE0 RID: 4064
		public const int RegionTooLarge = 14;

		// Token: 0x04000FE1 RID: 4065
		public const int CacheUnderReconfiguration = 15;

		// Token: 0x04000FE2 RID: 4066
		public const int CertificateRevocationServerOffline = 16;

		// Token: 0x04000FE3 RID: 4067
		internal const int ErrorLoadingGlobalConfigSection = 100;
	}
}
