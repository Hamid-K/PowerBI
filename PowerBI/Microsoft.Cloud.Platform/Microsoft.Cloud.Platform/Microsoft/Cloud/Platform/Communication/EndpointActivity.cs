using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CE RID: 1230
	public class EndpointActivity
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x0008565E File Offset: 0x0008385E
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x00085666 File Offset: 0x00083866
		public ClientActivityContextSource CorrelationIdsSource { get; private set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0008566F File Offset: 0x0008386F
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x00085677 File Offset: 0x00083877
		public ActivityType ActivityType { get; private set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x00085680 File Offset: 0x00083880
		// (set) Token: 0x06002579 RID: 9593 RVA: 0x00085688 File Offset: 0x00083888
		public string RootActivityIdHeaderName { get; private set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x00085691 File Offset: 0x00083891
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x00085699 File Offset: 0x00083899
		public string ClientActivityIdHeaderName { get; private set; }

		// Token: 0x0600257C RID: 9596 RVA: 0x000856A2 File Offset: 0x000838A2
		public EndpointActivity(ClientActivityContextSource source, ActivityType activityType, string rootActivityIdHeaderName, string clientActivityIdHeaderName)
		{
			this.CorrelationIdsSource = source;
			this.ActivityType = activityType;
			this.RootActivityIdHeaderName = rootActivityIdHeaderName;
			this.ClientActivityIdHeaderName = clientActivityIdHeaderName;
		}
	}
}
