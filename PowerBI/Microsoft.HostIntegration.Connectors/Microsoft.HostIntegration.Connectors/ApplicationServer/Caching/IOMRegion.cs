using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F0 RID: 496
	internal interface IOMRegion
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600104F RID: 4175
		string RegionName { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001050 RID: 4176
		string CacheName { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001051 RID: 4177
		OMRegionStats Stats { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001052 RID: 4178
		IMemoryManager MemoryManager { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001053 RID: 4179
		ExpirationType ExpirationType { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001054 RID: 4180
		bool IsDeleted { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001055 RID: 4181
		object State { get; }
	}
}
