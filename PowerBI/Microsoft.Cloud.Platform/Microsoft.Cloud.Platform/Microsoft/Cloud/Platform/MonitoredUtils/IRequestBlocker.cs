using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200012E RID: 302
	public interface IRequestBlocker<TKey>
	{
		// Token: 0x060007FE RID: 2046
		BlockingStatus<TKey> NotifyAndCheckBlockingStatus(TKey key, string clientIdentifier);

		// Token: 0x060007FF RID: 2047
		BlockingStatus<TKey> CheckBlockingStatus(TKey key, string clientIdentifier);

		// Token: 0x06000800 RID: 2048
		void Block(TKey key);
	}
}
