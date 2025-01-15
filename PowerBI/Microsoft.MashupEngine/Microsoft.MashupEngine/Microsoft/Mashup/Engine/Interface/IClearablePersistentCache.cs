using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200005E RID: 94
	public interface IClearablePersistentCache : IPersistentCache, IDisposable
	{
		// Token: 0x0600018F RID: 399
		void Clear();
	}
}
