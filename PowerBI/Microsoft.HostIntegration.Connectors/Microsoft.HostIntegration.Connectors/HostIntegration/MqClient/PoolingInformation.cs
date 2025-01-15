using System;
using System.Globalization;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD6 RID: 3030
	public class PoolingInformation
	{
		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x00180FD2 File Offset: 0x0017F1D2
		// (set) Token: 0x06005E59 RID: 24153 RVA: 0x00180FDA File Offset: 0x0017F1DA
		public PoolingInformation.QueueManagerBehaviorInformation QueueManagerBehavior { get; private set; }

		// Token: 0x06005E5A RID: 24154 RVA: 0x00180FE3 File Offset: 0x0017F1E3
		public PoolingInformation(PoolingInformation.QueueManagerBehaviorInformation queueManagerBehavior)
		{
			this.QueueManagerBehavior = queueManagerBehavior;
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x00180FF2 File Offset: 0x0017F1F2
		public PoolingInformation()
		{
			this.QueueManagerBehavior = new PoolingInformation.QueueManagerBehaviorInformation(true, 60, true, true, 5);
		}

		// Token: 0x02000BD7 RID: 3031
		public class QueueManagerBehaviorInformation
		{
			// Token: 0x17001732 RID: 5938
			// (get) Token: 0x06005E5C RID: 24156 RVA: 0x0018100B File Offset: 0x0017F20B
			// (set) Token: 0x06005E5D RID: 24157 RVA: 0x00181013 File Offset: 0x0017F213
			public bool Pool { get; private set; }

			// Token: 0x17001733 RID: 5939
			// (get) Token: 0x06005E5E RID: 24158 RVA: 0x0018101C File Offset: 0x0017F21C
			// (set) Token: 0x06005E5F RID: 24159 RVA: 0x00181024 File Offset: 0x0017F224
			public int Timeout { get; private set; }

			// Token: 0x17001734 RID: 5940
			// (get) Token: 0x06005E60 RID: 24160 RVA: 0x0018102D File Offset: 0x0017F22D
			// (set) Token: 0x06005E61 RID: 24161 RVA: 0x00181035 File Offset: 0x0017F235
			public int QueueManagersPerConversation { get; private set; }

			// Token: 0x17001735 RID: 5941
			// (get) Token: 0x06005E62 RID: 24162 RVA: 0x0018103E File Offset: 0x0017F23E
			// (set) Token: 0x06005E63 RID: 24163 RVA: 0x00181046 File Offset: 0x0017F246
			public bool AllowDifferentChannels { get; private set; }

			// Token: 0x17001736 RID: 5942
			// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0018104F File Offset: 0x0017F24F
			// (set) Token: 0x06005E65 RID: 24165 RVA: 0x00181057 File Offset: 0x0017F257
			public bool DifferentUserDifferentConversation { get; private set; }

			// Token: 0x06005E66 RID: 24166 RVA: 0x00181060 File Offset: 0x0017F260
			internal QueueManagerBehaviorInformation(bool pool, int timeout, bool allowDifferentChannels, bool differentUserDifferentConversation, int queueManagersPerConversation)
			{
				this.Pool = pool;
				this.Timeout = timeout;
				this.QueueManagersPerConversation = queueManagersPerConversation;
				this.AllowDifferentChannels = allowDifferentChannels;
				this.DifferentUserDifferentConversation = differentUserDifferentConversation;
			}

			// Token: 0x06005E67 RID: 24167 RVA: 0x00181090 File Offset: 0x0017F290
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"Queue Manager Behavior: Pool ",
					this.Pool.ToString(),
					", Timeout ",
					this.Timeout.ToString(CultureInfo.InvariantCulture),
					", Different Channels ",
					this.AllowDifferentChannels.ToString(),
					", Different Conversations ",
					this.DifferentUserDifferentConversation.ToString(),
					", Queue Managers Per Conversation ",
					this.QueueManagersPerConversation.ToString(CultureInfo.InvariantCulture)
				});
			}
		}
	}
}
