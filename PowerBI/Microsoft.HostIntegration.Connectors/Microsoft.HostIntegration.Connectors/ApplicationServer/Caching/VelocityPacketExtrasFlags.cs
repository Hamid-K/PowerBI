using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000178 RID: 376
	[Flags]
	internal enum VelocityPacketExtrasFlags
	{
		// Token: 0x04000880 RID: 2176
		None = 0,
		// Token: 0x04000881 RID: 2177
		Version = 1,
		// Token: 0x04000882 RID: 2178
		ExpiryTTL = 2,
		// Token: 0x04000883 RID: 2179
		LockHandle = 4,
		// Token: 0x04000884 RID: 2180
		PartitionKey = 8,
		// Token: 0x04000885 RID: 2181
		CacheItemMask = 16
	}
}
