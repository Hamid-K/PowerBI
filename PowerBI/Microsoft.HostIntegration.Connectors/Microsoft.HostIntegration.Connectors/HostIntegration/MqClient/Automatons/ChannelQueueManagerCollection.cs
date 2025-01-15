using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD8 RID: 2776
	internal class ChannelQueueManagerCollection
	{
		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x0016C90D File Offset: 0x0016AB0D
		// (set) Token: 0x06005880 RID: 22656 RVA: 0x0016C915 File Offset: 0x0016AB15
		public string Channel { get; private set; }

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x0016C91E File Offset: 0x0016AB1E
		// (set) Token: 0x06005882 RID: 22658 RVA: 0x0016C926 File Offset: 0x0016AB26
		public PortConnection PortConnection { get; private set; }

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x0016C92F File Offset: 0x0016AB2F
		// (set) Token: 0x06005884 RID: 22660 RVA: 0x0016C937 File Offset: 0x0016AB37
		public List<ChannelQueueManager> ChannelQueueManagers { get; set; }

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x0016C940 File Offset: 0x0016AB40
		// (set) Token: 0x06005886 RID: 22662 RVA: 0x0016C948 File Offset: 0x0016AB48
		public int NumberOfConversationsPerSocket { get; set; }

		// Token: 0x06005887 RID: 22663 RVA: 0x0016C951 File Offset: 0x0016AB51
		internal ChannelQueueManagerCollection(string channel, PortConnection parent)
		{
			this.Channel = channel;
			this.ChannelQueueManagers = new List<ChannelQueueManager>();
			this.PortConnection = parent;
			this.NumberOfConversationsPerSocket = -1;
		}
	}
}
