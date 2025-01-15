using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000130 RID: 304
	[OriginalName("ScheduleStateEnum")]
	public enum ScheduleStateEnum
	{
		// Token: 0x0400061B RID: 1563
		[OriginalName("Ready")]
		Ready,
		// Token: 0x0400061C RID: 1564
		[OriginalName("Running")]
		Running,
		// Token: 0x0400061D RID: 1565
		[OriginalName("Paused")]
		Paused,
		// Token: 0x0400061E RID: 1566
		[OriginalName("Expired")]
		Expired,
		// Token: 0x0400061F RID: 1567
		[OriginalName("Failing")]
		Failing
	}
}
