using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000133 RID: 307
	[OriginalName("PowerBIUserStatus")]
	public enum PowerBIUserStatus
	{
		// Token: 0x0400062A RID: 1578
		[OriginalName("SignedIn")]
		SignedIn = 1,
		// Token: 0x0400062B RID: 1579
		[OriginalName("SignedOut")]
		SignedOut,
		// Token: 0x0400062C RID: 1580
		[OriginalName("Expired")]
		Expired
	}
}
