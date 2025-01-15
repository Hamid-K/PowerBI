using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000088 RID: 136
	public interface IPersistentCacheEntry
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F7 RID: 503
		string CacheKey { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F8 RID: 504
		bool IsCached { get; }
	}
}
