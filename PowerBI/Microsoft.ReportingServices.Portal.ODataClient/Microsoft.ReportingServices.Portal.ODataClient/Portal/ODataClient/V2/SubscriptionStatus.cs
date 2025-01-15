using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000093 RID: 147
	[OriginalName("SubscriptionStatus")]
	public enum SubscriptionStatus
	{
		// Token: 0x040002ED RID: 749
		[OriginalName("Completed")]
		Completed,
		// Token: 0x040002EE RID: 750
		[OriginalName("InProgress")]
		InProgress,
		// Token: 0x040002EF RID: 751
		[OriginalName("Failed")]
		Failed
	}
}
