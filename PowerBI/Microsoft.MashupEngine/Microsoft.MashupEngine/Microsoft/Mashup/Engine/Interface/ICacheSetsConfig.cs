using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000054 RID: 84
	[Obsolete]
	public interface ICacheSetsConfig
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015A RID: 346
		IPersistentCacheConfig Metadata { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015B RID: 347
		IPersistentCacheConfig Data { get; }
	}
}
