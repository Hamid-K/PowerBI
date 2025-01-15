using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000095 RID: 149
	[OriginalName("CredentialRetrievalType")]
	public enum CredentialRetrievalType
	{
		// Token: 0x040002F5 RID: 757
		[OriginalName("prompt")]
		Prompt,
		// Token: 0x040002F6 RID: 758
		[OriginalName("store")]
		Store,
		// Token: 0x040002F7 RID: 759
		[OriginalName("integrated")]
		Integrated,
		// Token: 0x040002F8 RID: 760
		[OriginalName("none")]
		None
	}
}
