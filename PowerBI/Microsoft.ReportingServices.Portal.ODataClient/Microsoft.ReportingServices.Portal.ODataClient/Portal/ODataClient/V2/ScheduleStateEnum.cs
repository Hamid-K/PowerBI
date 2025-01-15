using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x020000A4 RID: 164
	[OriginalName("ScheduleStateEnum")]
	public enum ScheduleStateEnum
	{
		// Token: 0x0400035E RID: 862
		[OriginalName("Ready")]
		Ready,
		// Token: 0x0400035F RID: 863
		[OriginalName("Running")]
		Running,
		// Token: 0x04000360 RID: 864
		[OriginalName("Paused")]
		Paused,
		// Token: 0x04000361 RID: 865
		[OriginalName("Expired")]
		Expired,
		// Token: 0x04000362 RID: 866
		[OriginalName("Failing")]
		Failing
	}
}
