using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x020000A5 RID: 165
	[OriginalName("PowerBIUserStatus")]
	public enum PowerBIUserStatus
	{
		// Token: 0x04000364 RID: 868
		[OriginalName("SignedIn")]
		SignedIn = 1,
		// Token: 0x04000365 RID: 869
		[OriginalName("SignedOut")]
		SignedOut,
		// Token: 0x04000366 RID: 870
		[OriginalName("Expired")]
		Expired
	}
}
