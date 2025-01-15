using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000280 RID: 640
	public interface IFlowStep
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600110F RID: 4367
		string Name { get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001110 RID: 4368
		bool CompletedSynchronously { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001111 RID: 4369
		bool ContinueRunning { get; }
	}
}
