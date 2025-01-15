using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200008D RID: 141
	public interface IRoleChangeUpdater
	{
		// Token: 0x06000519 RID: 1305
		void Subscribe(IRoleChangeUpdateSubscriber subscriber);

		// Token: 0x0600051A RID: 1306
		void Unsubscribe(IRoleChangeUpdateSubscriber subscriber);
	}
}
