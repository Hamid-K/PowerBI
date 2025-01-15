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
	// Token: 0x02000046 RID: 70
	public class InteractiveBrowserCredential : TokenCredential
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00007E2E File Offset: 0x0000602E
		internal string TenantId { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007E36 File Offset: 0x00006036
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007E3E File Offset: 0x0000603E
		internal string ClientId { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007E46 File Offset: 0x00006046
		internal string LoginHint { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00007E4E File Offset: 0x0000604E
		internal BrowserCustomizationOptions BrowserCustomization { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00007E56 File Offset: 0x00006056
		internal MsalPublicClient Client { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007E5E File Offset: 0x0000605E
		internal CredentialPipeline Pipeline { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00007E66 File Offset: 0x00006066
		internal bool DisableAutomaticAuthentication { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007E6E File Offset: 0x0000606E
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00007E76 File Offset: 0x00006076
		internal AuthenticationRecord Record { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00007E7F File Offset: 0x0000607F
		internal string DefaultScope { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00007E87 File Offset: 0x00006087
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007E8F File Offset: 0x0000608F
		internal bool UseOperatingSystemAccount { get; }

		// Token: 0x0600025E RID: 606 RVA: 0x00007E97 File Offset: 0x00006097
		public InteractiveBrowserCredential()
			: this(null, "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, null)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007EA8 File Offset: 0x000060A8
		public InteractiveBrowserCredential(InteractiveBrowserCredentialOptions options)
			: this((options != null) ? options.TenantId : null, ((options != null) ? options.ClientId : null) ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", options, null)
		{
			this.DisableAutomaticAuthentication = options != null && options.DisableAutomaticAuthentication;
			this.Record = ((options != null) ? options.AuthenticationRecord : null);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007F02 File Offset: 0x00006102
		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractiveBrowserCredential(string clientId)
			: this(null, clientId, null, null)
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007F0E File Offset: 0x0000610E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options = null)
			: this(Validations.ValidateTenantId(tenantId, "tenantId", true), clientId, options, null, null)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007F31 File Offset: 0x00006131
		internal InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(tenantId, clientId, options, pipeline, null)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007F4C File Offset: 0x0000614C
		internal InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			this.ClientId = clientId;
			this.TenantId = tenantId;
			this.Pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions = options as InteractiveBrowserCredentialOptions;
			this.LoginHint = ((interactiveBrowserCredentialOptions != null) ? interactiveBrowserCredentialOptions.LoginHint : null);
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions2 = options as InteractiveBrowserCredentialOptions;
			string text;
			if (interactiveBrowserCredentialOptions2 == null)
			{
				text = null;
			}
			else
			{
				Uri redirectUri = interactiveBrowserCredentialOptions2.RedirectUri;
				text = ((redirectUri != null) ? redirectUri.AbsoluteUri : null);
			}
			string text2 = text ?? "http://localhost";
			this.DefaultScope = AzureAuthorityHosts.GetDefaultScope(((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault());
			this.Client = client ?? new MsalPublicClient(this.Pipeline, tenantId, clientId, text2, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions3 = options as InteractiveBrowserCredentialOptions;
			this.Record = ((interactiveBrowserCredentialOptions3 != null) ? interactiveBrowserCredentialOptions3.AuthenticationRecord : null);
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions4 = options as InteractiveBrowserCredentialOptions;
			this.BrowserCustomization = ((interactiveBrowserCredentialOptions4 != null) ? interactiveBrowserCredentialOptions4.BrowserCustomization : null);
			IMsalPublicClientInitializerOptions msalPublicClientInitializerOptions = options as IMsalPublicClientInitializerOptions;
			this.UseOperatingSystemAccount = msalPublicClientInitializerOptions != null && msalPublicClientInitializerOptions.UseDefaultBrokerAccount;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008081 File Offset: 0x00006281
		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return this.Authenticate(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000080B8 File Offset: 0x000062B8
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await this.AuthenticateAsync(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008103 File Offset: 0x00006303
		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.AuthenticateImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AuthenticationRecord>();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008114 File Offset: 0x00006314
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.AuthenticateImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008167 File Offset: 0x00006367
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008178 File Offset: 0x00006378
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000081CC File Offset: 0x000063CC
		private Task<AuthenticationRecord> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			InteractiveBrowserCredential.<AuthenticateImplAsync>d__51 <AuthenticateImplAsync>d__;
			<AuthenticateImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AuthenticationRecord>.Create();
			<AuthenticateImplAsync>d__.<>4__this = this;
			<AuthenticateImplAsync>d__.async = async;
			<AuthenticateImplAsync>d__.requestContext = requestContext;
			<AuthenticateImplAsync>d__.cancellationToken = cancellationToken;
			<AuthenticateImplAsync>d__.<>1__state = -1;
			<AuthenticateImplAsync>d__.<>t__builder.Start<InteractiveBrowserCredential.<AuthenticateImplAsync>d__51>(ref <AuthenticateImplAsync>d__);
			return <AuthenticateImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008228 File Offset: 0x00006428
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			InteractiveBrowserCredential.<GetTokenImplAsync>d__52 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<InteractiveBrowserCredential.<GetTokenImplAsync>d__52>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008284 File Offset: 0x00006484
		private async Task<AccessToken> GetTokenViaBrowserLoginAsync(TokenRequestContext context, bool async, CancellationToken cancellationToken)
		{
			Prompt prompt;
			if (this.LoginHint == null)
			{
				prompt = Prompt.SelectAccount;
			}
			else
			{
				prompt = Prompt.NoPrompt;
			}
			Prompt prompt2 = prompt;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			string text;
			if ((text = this.TenantId) == null)
			{
				AuthenticationRecord record = this.Record;
				text = ((record != null) ? record.TenantId : null);
			}
			string text2 = tenantIdResolver.Resolve(text, context, this.AdditionallyAllowedTenantIds);
			AuthenticationResult authenticationResult = await this.Client.AcquireTokenInteractiveAsync(context.Scopes, context.Claims, prompt2, this.LoginHint, text2, context.IsCaeEnabled, this.BrowserCustomization, async, cancellationToken).ConfigureAwait(false);
			this.Record = new AuthenticationRecord(authenticationResult, this.ClientId);
			return new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn);
		}

		// Token: 0x0400018A RID: 394
		private const string AuthenticationRequiredMessage = "Interactive authentication is needed to acquire token. Call Authenticate to interactively authenticate.";

		// Token: 0x0400018B RID: 395
		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";
	}
}
