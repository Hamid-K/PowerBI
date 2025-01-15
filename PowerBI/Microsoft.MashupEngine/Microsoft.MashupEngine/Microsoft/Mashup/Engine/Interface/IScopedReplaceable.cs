using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200005D RID: 93
	public interface IScopedReplaceable<T>
	{
		// Token: 0x0600018E RID: 398
		IDisposable ReplaceWith(T value);
	}
}
