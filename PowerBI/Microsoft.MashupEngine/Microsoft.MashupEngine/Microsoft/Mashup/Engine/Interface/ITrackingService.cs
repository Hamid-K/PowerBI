using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E7 RID: 231
	public interface ITrackingService<T>
	{
		// Token: 0x0600035C RID: 860
		void Register(T item);

		// Token: 0x0600035D RID: 861
		void Unregister(T item);
	}
}
