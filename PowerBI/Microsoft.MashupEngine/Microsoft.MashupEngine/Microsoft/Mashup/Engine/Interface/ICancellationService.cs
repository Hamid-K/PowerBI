using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000021 RID: 33
	public interface ICancellationService : ITrackingService<ICancellable>
	{
		// Token: 0x0600007E RID: 126
		int CancelAll();
	}
}
