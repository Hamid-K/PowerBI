using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200005A RID: 90
	public interface ICacheClock
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000184 RID: 388
		CacheVersion Current { get; }

		// Token: 0x06000185 RID: 389
		CacheVersion Increment();
	}
}
