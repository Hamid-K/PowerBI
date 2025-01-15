using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026A RID: 618
	internal interface IOMCacheItem
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600149D RID: 5277
		object[] Tags { get; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600149E RID: 5278
		int Size { get; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600149F RID: 5279
		string RegionName { get; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060014A0 RID: 5280
		string CacheName { get; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060014A1 RID: 5281
		object Value { get; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060014A2 RID: 5282
		Key Key { get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060014A3 RID: 5283
		long TimeToLive { get; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060014A4 RID: 5284
		TimeSpan TimeBeforeExpiry { get; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060014A5 RID: 5285
		TimeSpan TimeToExtend { get; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060014A6 RID: 5286
		InternalCacheItemVersion Version { get; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060014A7 RID: 5287
		// (set) Token: 0x060014A8 RID: 5288
		IOMRegion Region { get; set; }
	}
}
