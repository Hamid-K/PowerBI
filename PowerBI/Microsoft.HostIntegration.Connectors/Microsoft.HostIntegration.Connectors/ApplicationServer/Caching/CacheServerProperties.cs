using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000037 RID: 55
	internal struct CacheServerProperties
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008C64 File Offset: 0x00006E64
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00008C6C File Offset: 0x00006E6C
		public CacheLookupTableTransfer InitialLookupTable { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008C75 File Offset: 0x00006E75
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00008C7D File Offset: 0x00006E7D
		public NamedCacheConfiguration CacheConfiguration { get; set; }
	}
}
