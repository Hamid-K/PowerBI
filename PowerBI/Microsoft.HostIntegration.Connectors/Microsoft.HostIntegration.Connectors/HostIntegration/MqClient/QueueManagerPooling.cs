using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B5A RID: 2906
	public class QueueManagerPooling
	{
		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06005C3F RID: 23615 RVA: 0x0017CEB4 File Offset: 0x0017B0B4
		// (set) Token: 0x06005C40 RID: 23616 RVA: 0x0017CEC0 File Offset: 0x0017B0C0
		public bool Pool
		{
			get
			{
				return QueueManagerPooling.iPooling.Pool;
			}
			set
			{
				QueueManagerPooling.iPooling.Pool = value;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x0017CECD File Offset: 0x0017B0CD
		// (set) Token: 0x06005C42 RID: 23618 RVA: 0x0017CED9 File Offset: 0x0017B0D9
		public int Timeout
		{
			get
			{
				return QueueManagerPooling.iPooling.Timeout;
			}
			set
			{
				QueueManagerPooling.iPooling.Timeout = value;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0017CEE6 File Offset: 0x0017B0E6
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0017CEF2 File Offset: 0x0017B0F2
		public int QueueManagersPerConversation
		{
			get
			{
				return QueueManagerPooling.iPooling.QueueManagersPerConversation;
			}
			set
			{
				QueueManagerPooling.iPooling.QueueManagersPerConversation = value;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x0017CEFF File Offset: 0x0017B0FF
		// (set) Token: 0x06005C46 RID: 23622 RVA: 0x0017CF0B File Offset: 0x0017B10B
		public bool AllowDifferentChannels
		{
			get
			{
				return QueueManagerPooling.iPooling.AllowDifferentChannels;
			}
			set
			{
				QueueManagerPooling.iPooling.AllowDifferentChannels = value;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x0017CF18 File Offset: 0x0017B118
		// (set) Token: 0x06005C48 RID: 23624 RVA: 0x0017CF24 File Offset: 0x0017B124
		public bool OneUserPerConversation
		{
			get
			{
				return QueueManagerPooling.iPooling.OneUserPerConversation;
			}
			set
			{
				QueueManagerPooling.iPooling.OneUserPerConversation = value;
			}
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x0017CF40 File Offset: 0x0017B140
		internal QueueManagerPooling(PoolingInformation.QueueManagerBehaviorInformation behavior)
		{
			this.Pool = behavior.Pool;
			this.Timeout = behavior.Timeout;
			this.AllowDifferentChannels = behavior.AllowDifferentChannels;
			this.OneUserPerConversation = behavior.DifferentUserDifferentConversation;
			this.QueueManagersPerConversation = behavior.QueueManagersPerConversation;
		}

		// Token: 0x0400484B RID: 18507
		private static IPooling iPooling = Globals.GetIPooling();
	}
}
