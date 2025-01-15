using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000125 RID: 293
	public interface IDenialOfServiceProtection<TKey>
	{
		// Token: 0x060007DD RID: 2013
		BlockingStatus<TKey> QueryBlockingStatus(TKey key, DateTime when);

		// Token: 0x060007DE RID: 2014
		BlockingStatus<TKey> ReportAndCheckRequestBlockingStatus(TKey key, DateTime when, string clientIdentifier = null);

		// Token: 0x060007DF RID: 2015
		void Block(TKey key);
	}
}
