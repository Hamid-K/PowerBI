using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Broker
{
	// Token: 0x02000262 RID: 610
	internal interface ITokenRequestComponent
	{
		// Token: 0x06001846 RID: 6214
		Task<MsalTokenResponse> FetchTokensAsync(CancellationToken cancellationToken);
	}
}
