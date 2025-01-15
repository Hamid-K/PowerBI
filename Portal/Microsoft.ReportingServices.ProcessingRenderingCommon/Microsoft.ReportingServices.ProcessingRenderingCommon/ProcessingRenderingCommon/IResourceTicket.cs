using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon
{
	// Token: 0x020000CF RID: 207
	public interface IResourceTicket : IDisposable
	{
		// Token: 0x06000709 RID: 1801
		void DelayUntilResourcesAvailableBlocking();

		// Token: 0x0600070A RID: 1802
		Task DelayUntilResourcesAvailableAsync();

		// Token: 0x0600070B RID: 1803
		Task DelayUntilResourcesAvailableAsync(CancellationToken cancellationToken);

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600070C RID: 1804
		long CumulativeWaitDurationMs { get; }
	}
}
