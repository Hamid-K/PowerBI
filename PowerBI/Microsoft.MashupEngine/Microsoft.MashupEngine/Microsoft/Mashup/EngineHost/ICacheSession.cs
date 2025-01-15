using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200196D RID: 6509
	public interface ICacheSession : IDisposable
	{
		// Token: 0x17002A32 RID: 10802
		// (get) Token: 0x0600A53A RID: 42298
		string CachePath { get; }
	}
}
