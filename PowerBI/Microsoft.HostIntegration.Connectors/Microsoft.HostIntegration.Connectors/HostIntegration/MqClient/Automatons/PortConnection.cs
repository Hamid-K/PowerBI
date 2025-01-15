using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFA RID: 2810
	internal class PortConnection
	{
		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x0600590A RID: 22794 RVA: 0x0016FFE0 File Offset: 0x0016E1E0
		// (set) Token: 0x0600590B RID: 22795 RVA: 0x0016FFE8 File Offset: 0x0016E1E8
		public int Port { get; private set; }

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x0016FFF1 File Offset: 0x0016E1F1
		// (set) Token: 0x0600590D RID: 22797 RVA: 0x0016FFF9 File Offset: 0x0016E1F9
		public HostConnection HostConnection { get; private set; }

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x00170002 File Offset: 0x0016E202
		// (set) Token: 0x0600590F RID: 22799 RVA: 0x0017000A File Offset: 0x0016E20A
		public bool UseSsl { get; private set; }

		// Token: 0x06005910 RID: 22800 RVA: 0x00170013 File Offset: 0x0016E213
		public PortConnection(int port, HostConnection parent, bool useSsl)
		{
			this.Port = port;
			this.ChannelToChannelQueueManagerCollections = new Dictionary<string, ChannelQueueManagerCollection>();
			this.HostConnection = parent;
			this.UseSsl = useSsl;
		}

		// Token: 0x040045F0 RID: 17904
		public Dictionary<string, ChannelQueueManagerCollection> ChannelToChannelQueueManagerCollections;
	}
}
