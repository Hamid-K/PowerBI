using System;

namespace Microsoft.Mashup.EngineHost.Interface
{
	// Token: 0x02001B65 RID: 7013
	public sealed class CacheSetsConfig
	{
		// Token: 0x17002BFB RID: 11259
		// (get) Token: 0x0600AF81 RID: 44929 RVA: 0x0023ECD0 File Offset: 0x0023CED0
		// (set) Token: 0x0600AF82 RID: 44930 RVA: 0x0023ECD8 File Offset: 0x0023CED8
		public CompoundCacheConfig Metadata { get; set; }

		// Token: 0x17002BFC RID: 11260
		// (get) Token: 0x0600AF83 RID: 44931 RVA: 0x0023ECE1 File Offset: 0x0023CEE1
		// (set) Token: 0x0600AF84 RID: 44932 RVA: 0x0023ECE9 File Offset: 0x0023CEE9
		public CompoundCacheConfig Data { get; set; }
	}
}
