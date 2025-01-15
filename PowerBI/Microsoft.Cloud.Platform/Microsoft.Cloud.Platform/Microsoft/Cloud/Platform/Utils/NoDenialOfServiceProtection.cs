using System;
using System.Net;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000261 RID: 609
	public sealed class NoDenialOfServiceProtection : IDenialOfServiceProtection<IPAddress>
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00037346 File Offset: 0x00035546
		public BlockingStatus<IPAddress> QueryBlockingStatus(IPAddress key, DateTime when)
		{
			return new BlockingStatus<IPAddress>(key);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00037346 File Offset: 0x00035546
		public BlockingStatus<IPAddress> ReportAndCheckRequestBlockingStatus(IPAddress key, DateTime when, string clientIdentifier = null)
		{
			return new BlockingStatus<IPAddress>(key);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Block(IPAddress key)
		{
		}
	}
}
