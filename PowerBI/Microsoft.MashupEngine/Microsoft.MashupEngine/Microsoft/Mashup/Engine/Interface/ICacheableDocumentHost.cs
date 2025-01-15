using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000029 RID: 41
	public interface ICacheableDocumentHost : IDocumentHost
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A3 RID: 163
		object CacheIdentity { get; }
	}
}
