using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200021F RID: 543
	public enum MeetingRequestsDeliveryScope
	{
		// Token: 0x04000ED4 RID: 3796
		DelegatesOnly,
		// Token: 0x04000ED5 RID: 3797
		DelegatesAndMe,
		// Token: 0x04000ED6 RID: 3798
		DelegatesAndSendInformationToMe,
		// Token: 0x04000ED7 RID: 3799
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		NoForward
	}
}
