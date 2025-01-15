using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD7 RID: 2775
	internal class ChannelQueueManager
	{
		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x0016C663 File Offset: 0x0016A863
		// (set) Token: 0x06005869 RID: 22633 RVA: 0x0016C66B File Offset: 0x0016A86B
		public int Instance { get; private set; }

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x0600586A RID: 22634 RVA: 0x0016C674 File Offset: 0x0016A874
		// (set) Token: 0x0600586B RID: 22635 RVA: 0x0016C67C File Offset: 0x0016A87C
		internal string Channel { get; private set; }

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x0600586C RID: 22636 RVA: 0x0016C685 File Offset: 0x0016A885
		// (set) Token: 0x0600586D RID: 22637 RVA: 0x0016C68D File Offset: 0x0016A88D
		internal Dictionary<string, NameQueueManager> NameToNameQueueManagers { get; private set; }

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x0600586E RID: 22638 RVA: 0x0016C696 File Offset: 0x0016A896
		// (set) Token: 0x0600586F RID: 22639 RVA: 0x0016C69E File Offset: 0x0016A89E
		internal PooledConnection Connection { get; set; }

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06005870 RID: 22640 RVA: 0x0016C6A7 File Offset: 0x0016A8A7
		// (set) Token: 0x06005871 RID: 22641 RVA: 0x0016C6AF File Offset: 0x0016A8AF
		internal ChannelQueueManagerCollection ChannelQueueManagerCollection { get; private set; }

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x0016C6B8 File Offset: 0x0016A8B8
		internal int ReferenceCount
		{
			get
			{
				object obj = this.lockObject;
				int num;
				lock (obj)
				{
					num = this.shareCount;
				}
				return num;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06005873 RID: 22643 RVA: 0x0016C6FC File Offset: 0x0016A8FC
		internal int AutomatonQmReferenceCount
		{
			get
			{
				object obj = this.lockObject;
				int num;
				lock (obj)
				{
					num = this.automatonQmShareCount;
				}
				return num;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0016C740 File Offset: 0x0016A940
		// (set) Token: 0x06005875 RID: 22645 RVA: 0x0016C748 File Offset: 0x0016A948
		internal bool AlreadyRemoved { get; set; }

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06005876 RID: 22646 RVA: 0x0016C751 File Offset: 0x0016A951
		// (set) Token: 0x06005877 RID: 22647 RVA: 0x0016C759 File Offset: 0x0016A959
		internal bool CreatedPooled { get; set; }

		// Token: 0x06005878 RID: 22648 RVA: 0x0016C762 File Offset: 0x0016A962
		internal ChannelQueueManager(string channel, ChannelQueueManagerCollection parent)
		{
			this.Channel = channel;
			this.NameToNameQueueManagers = new Dictionary<string, NameQueueManager>();
			this.ChannelQueueManagerCollection = parent;
			this.Instance = Interlocked.Increment(ref ChannelQueueManager.instance);
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0016C7A0 File Offset: 0x0016A9A0
		internal int AddReference()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.shareCount + 1;
				this.shareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0016C7F0 File Offset: 0x0016A9F0
		internal int Release()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.shareCount - 1;
				this.shareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x0016C840 File Offset: 0x0016AA40
		public int AddAutomatonQmReference()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.automatonQmShareCount + 1;
				this.automatonQmShareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0016C890 File Offset: 0x0016AA90
		internal int ReleaseAutomatonQm()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.automatonQmShareCount - 1;
				this.automatonQmShareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0016C8E0 File Offset: 0x0016AAE0
		public override string ToString()
		{
			return string.Format("Id: {0}, QMAs: {1}, QMCIs: {2}", this.Instance, this.AutomatonQmReferenceCount, this.ReferenceCount);
		}

		// Token: 0x040044B0 RID: 17584
		private static int instance;

		// Token: 0x040044B6 RID: 17590
		private object lockObject = new object();

		// Token: 0x040044B7 RID: 17591
		private int shareCount;

		// Token: 0x040044B8 RID: 17592
		private int automatonQmShareCount;
	}
}
