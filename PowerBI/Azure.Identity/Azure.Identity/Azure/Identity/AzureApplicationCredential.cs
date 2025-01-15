using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000030 RID: 48
	internal class AzureApplicationCredential : TokenCredential
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000052BF File Offset: 0x000034BF
		public AzureApplicationCredential()
			: this(new AzureApplicationCredentialOptions(), null, null)
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000052CE File Offset: 0x000034CE
		public AzureApplicationCredential(AzureApplicationCredentialOptions options)
			: this(options ?? new AzureApplicationCredentialOptions(), null, null)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000052E2 File Offset: 0x000034E2
		internal AzureApplicationCredential(AzureApplicationCredentialOptions options, EnvironmentCredential environmentCredential = null, ManagedIdentityCredential managedIdentityCredential = null)
		{
			this._credential = new ChainedTokenCredential(new TokenCredential[]
			{
				environmentCredential ?? new EnvironmentCredential(options),
				managedIdentityCredential ?? new ManagedIdentityCredential(options.ManagedIdentityClientId, null)
			});
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000531D File Offset: 0x0000351D
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005330 File Offset: 0x00003530
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005384 File Offset: 0x00003584
		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			AccessToken accessToken;
			if (async)
			{
				accessToken = await this._credential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				accessToken = this._credential.GetToken(requestContext, cancellationToken);
			}
			return accessToken;
		}

		// Token: 0x040000C6 RID: 198
		private readonly ChainedTokenCredential _credential;
	}
}
