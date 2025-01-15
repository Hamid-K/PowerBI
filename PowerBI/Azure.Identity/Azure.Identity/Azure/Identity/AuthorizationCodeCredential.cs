using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200002E RID: 46
	public class AuthorizationCodeCredential : TokenCredential
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004FFE File Offset: 0x000031FE
		internal MsalConfidentialClient Client { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005006 File Offset: 0x00003206
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x06000117 RID: 279 RVA: 0x0000500E File Offset: 0x0000320E
		protected AuthorizationCodeCredential()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005016 File Offset: 0x00003216
		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode)
			: this(tenantId, clientId, clientSecret, authorizationCode, null)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005024 File Offset: 0x00003224
		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, AuthorizationCodeCredentialOptions options)
			: this(tenantId, clientId, clientSecret, authorizationCode, options, null, null)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005035 File Offset: 0x00003235
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, TokenCredentialOptions options)
			: this(tenantId, clientId, clientSecret, authorizationCode, options, null, null)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005048 File Offset: 0x00003248
		internal AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, TokenCredentialOptions options, MsalConfidentialClient client, CredentialPipeline pipeline = null)
		{
			Validations.ValidateTenantId(tenantId, "tenantId", false);
			this._tenantId = tenantId;
			Argument.AssertNotNull<string>(clientSecret, "clientSecret");
			Argument.AssertNotNull<string>(clientId, "clientId");
			Argument.AssertNotNull<string>(authorizationCode, "authorizationCode");
			this._clientId = clientId;
			this._authCode = authorizationCode;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options ?? new TokenCredentialOptions(), false);
			AuthorizationCodeCredentialOptions authorizationCodeCredentialOptions = options as AuthorizationCodeCredentialOptions;
			string text;
			if (authorizationCodeCredentialOptions != null)
			{
				Uri redirectUri = authorizationCodeCredentialOptions.RedirectUri;
				text = ((redirectUri != null) ? redirectUri.AbsoluteUri : null);
			}
			else
			{
				text = null;
			}
			this._redirectUri = text;
			this.Client = client ?? new MsalConfidentialClient(this._pipeline, tenantId, clientId, clientSecret, this._redirectUri, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005149 File Offset: 0x00003349
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000515C File Offset: 0x0000335C
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000051B0 File Offset: 0x000033B0
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			AuthorizationCodeCredential.<GetTokenImplAsync>d__20 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<AuthorizationCodeCredential.<GetTokenImplAsync>d__20>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000520C File Offset: 0x0000340C
		private async Task<AccessToken> AcquireTokenWithCode(bool async, TokenRequestContext requestContext, AccessToken token, string tenantId, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.Client.AcquireTokenByAuthorizationCodeAsync(requestContext.Scopes, this._authCode, tenantId, this._redirectUri, requestContext.Claims, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(false);
			this._record = new AuthenticationRecord(authenticationResult, this._clientId);
			return new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn);
		}

		// Token: 0x040000BA RID: 186
		private readonly string _authCode;

		// Token: 0x040000BB RID: 187
		private readonly string _clientId;

		// Token: 0x040000BC RID: 188
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040000BD RID: 189
		private AuthenticationRecord _record;

		// Token: 0x040000BF RID: 191
		private readonly string _redirectUri;

		// Token: 0x040000C0 RID: 192
		private readonly string _tenantId;

		// Token: 0x040000C1 RID: 193
		internal readonly string[] AdditionallyAllowedTenantIds;
	}
}
