using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000123 RID: 291
	[OriginalName("CredentialRetrievalType")]
	public enum CredentialRetrievalType
	{
		// Token: 0x040005BE RID: 1470
		[OriginalName("prompt")]
		Prompt,
		// Token: 0x040005BF RID: 1471
		[OriginalName("store")]
		Store,
		// Token: 0x040005C0 RID: 1472
		[OriginalName("integrated")]
		Integrated,
		// Token: 0x040005C1 RID: 1473
		[OriginalName("none")]
		None
	}
}
