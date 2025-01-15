using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000051 RID: 81
	public class UsernamePasswordCredential : TokenCredential
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00009056 File Offset: 0x00007256
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000905E File Offset: 0x0000725E
		internal MsalPublicClient Client { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009066 File Offset: 0x00007266
		internal string DefaultScope { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000906E File Offset: 0x0000726E
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x060002D9 RID: 729 RVA: 0x00009076 File Offset: 0x00007276
		protected UsernamePasswordCredential()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000907E File Offset: 0x0000727E
		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId)
			: this(username, password, tenantId, clientId, null)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000908C File Offset: 0x0000728C
		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId, TokenCredentialOptions options)
			: this(username, password, tenantId, clientId, options, null, null)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000909D File Offset: 0x0000729D
		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId, UsernamePasswordCredentialOptions options)
			: this(username, password, tenantId, clientId, options, null, null)
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000090B0 File Offset: 0x000072B0
		internal UsernamePasswordCredential(string username, string password, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			Argument.AssertNotNull<string>(username, "username");
			Argument.AssertNotNull<string>(password, "password");
			Argument.AssertNotNull<string>(clientId, "clientId");
			this._tenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			this._username = username;
			this._password = password;
			this._clientId = clientId;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this.DefaultScope = AzureAuthorityHosts.GetDefaultScope(((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault());
			this.Client = client ?? new MsalPublicClient(this._pipeline, tenantId, clientId, null, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000091A1 File Offset: 0x000073A1
		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return this.Authenticate(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000091D8 File Offset: 0x000073D8
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await this.AuthenticateAsync(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009223 File Offset: 0x00007423
		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.AuthenticateImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AuthenticationResult>();
			return this._record;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000923C File Offset: 0x0000743C
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			await this.AuthenticateImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
			return this._record;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000928F File Offset: 0x0000748F
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000092A0 File Offset: 0x000074A0
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000092F4 File Offset: 0x000074F4
		private Task<AuthenticationResult> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			UsernamePasswordCredential.<AuthenticateImplAsync>d__31 <AuthenticateImplAsync>d__;
			<AuthenticateImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AuthenticationResult>.Create();
			<AuthenticateImplAsync>d__.<>4__this = this;
			<AuthenticateImplAsync>d__.async = async;
			<AuthenticateImplAsync>d__.requestContext = requestContext;
			<AuthenticateImplAsync>d__.cancellationToken = cancellationToken;
			<AuthenticateImplAsync>d__.<>1__state = -1;
			<AuthenticateImplAsync>d__.<>t__builder.Start<UsernamePasswordCredential.<AuthenticateImplAsync>d__31>(ref <AuthenticateImplAsync>d__);
			return <AuthenticateImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009350 File Offset: 0x00007550
		private Task<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			UsernamePasswordCredential.<GetTokenImplAsync>d__32 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<UsernamePasswordCredential.<GetTokenImplAsync>d__32>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001C2 RID: 450
		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";

		// Token: 0x040001C3 RID: 451
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/usernamepasswordcredential/troubleshoot";

		// Token: 0x040001C4 RID: 452
		private readonly string _clientId;

		// Token: 0x040001C5 RID: 453
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040001C6 RID: 454
		private readonly string _username;

		// Token: 0x040001C7 RID: 455
		private readonly string _password;

		// Token: 0x040001C8 RID: 456
		private AuthenticationRecord _record;

		// Token: 0x040001C9 RID: 457
		private readonly string _tenantId;
	}
}
