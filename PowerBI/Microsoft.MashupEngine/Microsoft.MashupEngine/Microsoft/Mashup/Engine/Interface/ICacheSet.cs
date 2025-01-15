using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000055 RID: 85
	public interface ICacheSet : IDisposable
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015C RID: 348
		IPersistentCache PersistentCache { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015D RID: 349
		IPersistentObjectCache PersistentObjectCache { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015E RID: 350
		IObjectCache ObjectCache { get; }
	}
}
