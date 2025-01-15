using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200023C RID: 572
	internal interface IAuthCodeRequestComponent
	{
		// Token: 0x06001735 RID: 5941
		Task<Tuple<AuthorizationResult, string>> FetchAuthCodeAndPkceVerifierAsync(CancellationToken cancellationToken);

		// Token: 0x06001736 RID: 5942
		Task<Uri> GetAuthorizationUriWithoutPkceAsync(CancellationToken cancellationToken);
	}
}
