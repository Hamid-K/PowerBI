using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF0 RID: 2800
	internal class NameQueueManager
	{
		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x0016CBD3 File Offset: 0x0016ADD3
		// (set) Token: 0x060058A9 RID: 22697 RVA: 0x0016CBDB File Offset: 0x0016ADDB
		public string Name { get; private set; }

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x0016CBE4 File Offset: 0x0016ADE4
		// (set) Token: 0x060058AB RID: 22699 RVA: 0x0016CBEC File Offset: 0x0016ADEC
		public List<WrappedPooledQueueManager> QueueManagers { get; private set; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060058AC RID: 22700 RVA: 0x0016CBF5 File Offset: 0x0016ADF5
		// (set) Token: 0x060058AD RID: 22701 RVA: 0x0016CBFD File Offset: 0x0016ADFD
		public ChannelQueueManager ChannelQueueManager { get; private set; }

		// Token: 0x060058AE RID: 22702 RVA: 0x0016CC06 File Offset: 0x0016AE06
		public NameQueueManager(string name, ChannelQueueManager parent)
		{
			this.Name = name;
			this.QueueManagers = new List<WrappedPooledQueueManager>();
			this.ChannelQueueManager = parent;
		}
	}
}
