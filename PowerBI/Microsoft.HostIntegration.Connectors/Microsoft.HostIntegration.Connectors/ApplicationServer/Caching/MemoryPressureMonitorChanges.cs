using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E4 RID: 228
	[Flags]
	internal enum MemoryPressureMonitorChanges
	{
		// Token: 0x040003F0 RID: 1008
		NoChange = 0,
		// Token: 0x040003F1 RID: 1009
		IsEnabledChange = 1,
		// Token: 0x040003F2 RID: 1010
		LowMemoryPercentChange = 2,
		// Token: 0x040003F3 RID: 1011
		LowMemoryReleasePercentChange = 4,
		// Token: 0x040003F4 RID: 1012
		IntervalChange = 8,
		// Token: 0x040003F5 RID: 1013
		ThrottleLowPercentChange = 16,
		// Token: 0x040003F6 RID: 1014
		ThrottleHighPercentChange = 32,
		// Token: 0x040003F7 RID: 1015
		ThrottleGCIntervalChange = 64,
		// Token: 0x040003F8 RID: 1016
		SyncGCIntervalChange = 128,
		// Token: 0x040003F9 RID: 1017
		IsMemoryManagerEnabledChange = 256,
		// Token: 0x040003FA RID: 1018
		BufferSizeChange = 512,
		// Token: 0x040003FB RID: 1019
		MinObjectPoolSizeChange = 1024,
		// Token: 0x040003FC RID: 1020
		MaxObjectPoolSizeChange = 2048,
		// Token: 0x040003FD RID: 1021
		InternalThrottleLowPercentChange = 4096,
		// Token: 0x040003FE RID: 1022
		InternalThrottleHighPercentChange = 8192,
		// Token: 0x040003FF RID: 1023
		PercentItemInFinalizerQueue = 16384,
		// Token: 0x04000400 RID: 1024
		MemoryManagerPauseThreshold = 32768,
		// Token: 0x04000401 RID: 1025
		AverageCacheItemSizeInBytes = 65536,
		// Token: 0x04000402 RID: 1026
		CacheUserDataSizePerNodeChange = 131072,
		// Token: 0x04000403 RID: 1027
		ChangeAll = 131071
	}
}
