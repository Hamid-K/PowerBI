using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001DC RID: 476
	internal interface IWebUI
	{
		// Token: 0x0600149B RID: 5275
		Task<AuthorizationResult> AcquireAuthorizationAsync(Uri authorizationUri, Uri redirectUri, RequestContext requestContext, CancellationToken cancellationToken);

		// Token: 0x0600149C RID: 5276
		Uri UpdateRedirectUri(Uri redirectUri);
	}
}
