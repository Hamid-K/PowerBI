using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200028C RID: 652
	public sealed class DelegateInformation
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x0003F67F File Offset: 0x0003E67F
		internal DelegateInformation(IList<DelegateUserResponse> delegateUserResponses, MeetingRequestsDeliveryScope meetingReqestsDeliveryScope)
		{
			this.delegateUserResponses = new Collection<DelegateUserResponse>(delegateUserResponses);
			this.meetingReqestsDeliveryScope = meetingReqestsDeliveryScope;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0003F69A File Offset: 0x0003E69A
		public Collection<DelegateUserResponse> DelegateUserResponses
		{
			get
			{
				return this.delegateUserResponses;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0003F6A2 File Offset: 0x0003E6A2
		public MeetingRequestsDeliveryScope MeetingRequestsDeliveryScope
		{
			get
			{
				return this.meetingReqestsDeliveryScope;
			}
		}

		// Token: 0x0400134B RID: 4939
		private Collection<DelegateUserResponse> delegateUserResponses;

		// Token: 0x0400134C RID: 4940
		private MeetingRequestsDeliveryScope meetingReqestsDeliveryScope;
	}
}
