using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200025C RID: 604
	[Flags]
	internal enum CacheEventType
	{
		// Token: 0x04000C23 RID: 3107
		AddEvent = 1,
		// Token: 0x04000C24 RID: 3108
		ReplaceEvent = 2,
		// Token: 0x04000C25 RID: 3109
		RemoveEvent = 4,
		// Token: 0x04000C26 RID: 3110
		CreateRegionEvent = 8,
		// Token: 0x04000C27 RID: 3111
		RemoveRegionEvent = 16,
		// Token: 0x04000C28 RID: 3112
		ClearRegionEvent = 32,
		// Token: 0x04000C29 RID: 3113
		NotificationMissEvent = 64,
		// Token: 0x04000C2A RID: 3114
		UnLockEvent = 128,
		// Token: 0x04000C2B RID: 3115
		ResetTimeEvent = 256,
		// Token: 0x04000C2C RID: 3116
		CompactPartitionEvent = 512,
		// Token: 0x04000C2D RID: 3117
		BulkEvictionEvent = 1024,
		// Token: 0x04000C2E RID: 3118
		PartitionClearCache = 2048,
		// Token: 0x04000C2F RID: 3119
		OpFailedEvent = 0,
		// Token: 0x04000C30 RID: 3120
		StopDispatchEvent = 4096,
		// Token: 0x04000C31 RID: 3121
		AllCacheEvents = 63,
		// Token: 0x04000C32 RID: 3122
		NotificationFailEvent = 64,
		// Token: 0x04000C33 RID: 3123
		DataLossEvent = 8192
	}
}
