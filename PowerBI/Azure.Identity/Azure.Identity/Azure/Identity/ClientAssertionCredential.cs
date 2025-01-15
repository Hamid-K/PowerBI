using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200003A RID: 58
	public class ClientAssertionCredential : TokenCredential
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006547 File Offset: 0x00004747
		internal string TenantId { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000654F File Offset: 0x0000474F
		internal string ClientId { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006557 File Offset: 0x00004757
		internal MsalConfidentialClient Client { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000655F File Offset: 0x0000475F
		internal CredentialPipeline Pipeline { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006567 File Offset: 0x00004767
		internal bool AllowMultiTenantAuthentication { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000656F File Offset: 0x0000476F
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x0600018E RID: 398 RVA: 0x00006577 File Offset: 0x00004777
		protected ClientAssertionCredential()
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006580 File Offset: 0x00004780
		public ClientAssertionCredential(string tenantId, string clientId, Func<CancellationToken, Task<string>> assertionCallback, ClientAssertionCredentialOptions options = null)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
			this.TenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			this.ClientId = clientId;
			MsalConfidentialClient msalConfidentialClient;
			if ((msalConfidentialClient = ((options != null) ? options.MsalClient : null)) == null)
			{
				msalConfidentialClient = new MsalConfidentialClient(((options != null) ? options.Pipeline : null) ?? CredentialPipeline.GetInstance(options, false), tenantId, clientId, assertionCallback, options);
			}
			this.Client = msalConfidentialClient;
			CredentialPipeline credentialPipeline;
			if ((credentialPipeline = ((options != null) ? options.Pipeline : null)) == null)
			{
				credentialPipeline = ((options != null) ? options.Pipeline : null) ?? CredentialPipeline.GetInstance(options, false);
			}
			this.Pipeline = credentialPipeline;
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006664 File Offset: 0x00004864
		public ClientAssertionCredential(string tenantId, string clientId, Func<string> assertionCallback, ClientAssertionCredentialOptions options = null)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
			this.TenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			this.ClientId = clientId;
			MsalConfidentialClient msalConfidentialClient;
			if ((msalConfidentialClient = ((options != null) ? options.MsalClient : null)) == null)
			{
				msalConfidentialClient = new MsalConfidentialClient(((options != null) ? options.Pipeline : null) ?? CredentialPipeline.GetInstance(options, false), tenantId, clientId, assertionCallback, options);
			}
			this.Client = msalConfidentialClient;
			CredentialPipeline credentialPipeline;
			if ((credentialPipeline = ((options != null) ? options.Pipeline : null)) == null)
			{
				credentialPipeline = ((options != null) ? options.Pipeline : null) ?? CredentialPipeline.GetInstance(options, false);
			}
			this.Pipeline = credentialPipeline;
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006748 File Offset: 0x00004948
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			AccessToken accessToken;
			using (CredentialDiagnosticScope credentialDiagnosticScope = this.Pipeline.StartGetTokenScope("ClientAssertionCredential.GetToken", requestContext))
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

		// Token: 0x06000192 RID: 402 RVA: 0x00006800 File Offset: 0x00004A00
		public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			ClientAssertionCredential.<GetTokenAsync>d__23 <GetTokenAsync>d__;
			<GetTokenAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenAsync>d__.<>4__this = this;
			<GetTokenAsync>d__.requestContext = requestContext;
			<GetTokenAsync>d__.cancellationToken = cancellationToken;
			<GetTokenAsync>d__.<>1__state = -1;
			<GetTokenAsync>d__.<>t__builder.Start<ClientAssertionCredential.<GetTokenAsync>d__23>(ref <GetTokenAsync>d__);
			return <GetTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400011D RID: 285
		internal readonly string[] AdditionallyAllowedTenantIds;
	}
}
