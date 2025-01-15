using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200003E RID: 62
	public class ClientSecretCredential : TokenCredential
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006BB2 File Offset: 0x00004DB2
		internal MsalConfidentialClient Client { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006BBA File Offset: 0x00004DBA
		internal string TenantId { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006BC2 File Offset: 0x00004DC2
		internal string ClientId { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006BCA File Offset: 0x00004DCA
		internal string ClientSecret { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006BD2 File Offset: 0x00004DD2
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x060001BB RID: 443 RVA: 0x00006BDA File Offset: 0x00004DDA
		protected ClientSecretCredential()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006BE2 File Offset: 0x00004DE2
		public ClientSecretCredential(string tenantId, string clientId, string clientSecret)
			: this(tenantId, clientId, clientSecret, null, null, null)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public ClientSecretCredential(string tenantId, string clientId, string clientSecret, ClientSecretCredentialOptions options)
			: this(tenantId, clientId, clientSecret, options, null, null)
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006BFF File Offset: 0x00004DFF
		public ClientSecretCredential(string tenantId, string clientId, string clientSecret, TokenCredentialOptions options)
			: this(tenantId, clientId, clientSecret, options, null, null)
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006C10 File Offset: 0x00004E10
		internal ClientSecretCredential(string tenantId, string clientId, string clientSecret, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
			Argument.AssertNotNull<string>(clientSecret, "clientSecret");
			this.TenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			if (clientId == null)
			{
				throw new ArgumentNullException("clientId");
			}
			this.ClientId = clientId;
			this.ClientSecret = clientSecret;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this.Client = client ?? new MsalConfidentialClient(this._pipeline, tenantId, clientId, clientSecret, null, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006CDC File Offset: 0x00004EDC
		public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			ClientSecretCredential.<GetTokenAsync>d__22 <GetTokenAsync>d__;
			<GetTokenAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenAsync>d__.<>4__this = this;
			<GetTokenAsync>d__.requestContext = requestContext;
			<GetTokenAsync>d__.cancellationToken = cancellationToken;
			<GetTokenAsync>d__.<>1__state = -1;
			<GetTokenAsync>d__.<>t__builder.Start<ClientSecretCredential.<GetTokenAsync>d__22>(ref <GetTokenAsync>d__);
			return <GetTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006D30 File Offset: 0x00004F30
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			AccessToken accessToken;
			using (CredentialDiagnosticScope credentialDiagnosticScope = this._pipeline.StartGetTokenScope("ClientSecretCredential.GetToken", requestContext))
			{
				try
				{
					string text = this.TenantIdResolver.Resolve(this.TenantId, requestContext, this.AdditionallyAllowedTenantIds);
					AuthenticationResult authenticationResult = this.Client.AcquireTokenForClientAsync(requestContext.Scopes, text, requestContext.Claims, requestContext.IsCaeEnabled, false, cancellationToken).EnsureCompleted<AuthenticationResult>();
					accessToken = credentialDiagnosticScope.Succeeded(new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn));
				}
				catch (Exception ex)
				{
					throw credentialDiagnosticScope.FailWrapAndThrow(ex, null, false);
				}
			}
			return accessToken;
		}

		// Token: 0x04000134 RID: 308
		private readonly CredentialPipeline _pipeline;

		// Token: 0x04000135 RID: 309
		internal readonly string[] AdditionallyAllowedTenantIds;
	}
}
