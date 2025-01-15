using System;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000262 RID: 610
	public sealed class NoRequestBlocking : IRequestBlocker<string>
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x0003734E File Offset: 0x0003554E
		public BlockingStatus<string> NotifyAndCheckBlockingStatus(string key, string clientIdentifier)
		{
			return new BlockingStatus<string>(key);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003734E File Offset: 0x0003554E
		public BlockingStatus<string> CheckBlockingStatus(string key, string clientIdentifier)
		{
			return new BlockingStatus<string>(key);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Block(string key)
		{
		}
	}
}
