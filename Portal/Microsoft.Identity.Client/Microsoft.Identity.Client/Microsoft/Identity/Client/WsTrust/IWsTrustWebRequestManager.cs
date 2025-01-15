using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001B6 RID: 438
	internal interface IWsTrustWebRequestManager
	{
		// Token: 0x060013B8 RID: 5048
		Task<MexDocument> GetMexDocumentAsync(string federationMetadataUrl, RequestContext requestContext, string federationMetadata = null);

		// Token: 0x060013B9 RID: 5049
		Task<WsTrustResponse> GetWsTrustResponseAsync(WsTrustEndpoint wsTrustEndpoint, string wsTrustRequest, RequestContext requestContext);

		// Token: 0x060013BA RID: 5050
		Task<UserRealmDiscoveryResponse> GetUserRealmAsync(string userRealmUriPrefix, string userName, RequestContext requestContext);
	}
}
