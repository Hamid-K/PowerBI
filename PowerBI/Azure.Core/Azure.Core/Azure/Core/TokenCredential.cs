using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000066 RID: 102
	public abstract class TokenCredential
	{
		// Token: 0x06000379 RID: 889
		[CallerShouldAudit("https://aka.ms/azsdk/callershouldaudit/identity")]
		public abstract ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken);

		// Token: 0x0600037A RID: 890
		[CallerShouldAudit("https://aka.ms/azsdk/callershouldaudit/identity")]
		public abstract AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken);
	}
}
