using System;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D7 RID: 983
	public interface IEventingServer
	{
		// Token: 0x06001E4C RID: 7756
		void SubmitEvent(WireEventBase evt);
	}
}
