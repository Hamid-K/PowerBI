using System;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x02000006 RID: 6
	internal interface IFederationMetadataProvider
	{
		// Token: 0x0600000A RID: 10
		FederationMetadata GetMetadata(Uri metadataUrl);
	}
}
