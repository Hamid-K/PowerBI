using System;
using System.Threading;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007F RID: 127
	public interface ICancellationTokenManager : IDisposable
	{
		// Token: 0x06000369 RID: 873
		CancellationToken RegisterRequest(Guid? jobId);

		// Token: 0x0600036A RID: 874
		void UnregisterRequest(Guid? jobId);

		// Token: 0x0600036B RID: 875
		void CancelRequest(Guid jobId);
	}
}
