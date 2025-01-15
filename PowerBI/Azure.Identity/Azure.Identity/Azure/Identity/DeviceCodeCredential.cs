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
	// Token: 0x02000042 RID: 66
	public class DeviceCodeCredential : TokenCredential
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007555 File Offset: 0x00005755
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000755D File Offset: 0x0000575D
		internal MsalPublicClient Client { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007566 File Offset: 0x00005766
		internal string ClientId { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000756E File Offset: 0x0000576E
		internal bool DisableAutomaticAuthentication { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007576 File Offset: 0x00005776
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000757E File Offset: 0x0000577E
		internal AuthenticationRecord Record { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007587 File Offset: 0x00005787
		internal Func<DeviceCodeInfo, CancellationToken, Task> DeviceCodeCallback { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000758F File Offset: 0x0000578F
		internal CredentialPipeline Pipeline { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00007597 File Offset: 0x00005797
		internal string DefaultScope { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000759F File Offset: 0x0000579F
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x0600020D RID: 525 RVA: 0x000075A7 File Offset: 0x000057A7
		public DeviceCodeCredential()
		{
			Func<DeviceCodeInfo, CancellationToken, Task> func;
			if ((func = DeviceCodeCredential.<>O.<0>__DefaultDeviceCodeHandler) == null)
			{
				func = (DeviceCodeCredential.<>O.<0>__DefaultDeviceCodeHandler = new Func<DeviceCodeInfo, CancellationToken, Task>(DeviceCodeCredential.DefaultDeviceCodeHandler));
			}
			this..ctor(func, null, "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, null);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000075D4 File Offset: 0x000057D4
		public DeviceCodeCredential(DeviceCodeCredentialOptions options)
		{
			Func<DeviceCodeInfo, CancellationToken, Task> func;
			if ((func = ((options != null) ? options.DeviceCodeCallback : null)) == null && (func = DeviceCodeCredential.<>O.<0>__DefaultDeviceCodeHandler) == null)
			{
				func = (DeviceCodeCredential.<>O.<0>__DefaultDeviceCodeHandler = new Func<DeviceCodeInfo, CancellationToken, Task>(DeviceCodeCredential.DefaultDeviceCodeHandler));
			}
			this..ctor(func, (options != null) ? options.TenantId : null, ((options != null) ? options.ClientId : null) ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", options, null);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007635 File Offset: 0x00005835
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string clientId, TokenCredentialOptions options = null)
			: this(deviceCodeCallback, null, clientId, options, null)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007642 File Offset: 0x00005842
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options = null)
			: this(deviceCodeCallback, Validations.ValidateTenantId(tenantId, "tenantId", true), clientId, options, null)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000765B File Offset: 0x0000585B
		internal DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(deviceCodeCallback, tenantId, clientId, options, pipeline, null)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000766C File Offset: 0x0000586C
		internal DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
			Argument.AssertNotNull<Func<DeviceCodeInfo, CancellationToken, Task>>(deviceCodeCallback, "deviceCodeCallback");
			this._tenantId = tenantId;
			this.ClientId = clientId;
			this.DeviceCodeCallback = deviceCodeCallback;
			DeviceCodeCredentialOptions deviceCodeCredentialOptions = options as DeviceCodeCredentialOptions;
			this.DisableAutomaticAuthentication = deviceCodeCredentialOptions != null && deviceCodeCredentialOptions.DisableAutomaticAuthentication;
			DeviceCodeCredentialOptions deviceCodeCredentialOptions2 = options as DeviceCodeCredentialOptions;
			this.Record = ((deviceCodeCredentialOptions2 != null) ? deviceCodeCredentialOptions2.AuthenticationRecord : null);
			this.Pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this.DefaultScope = AzureAuthorityHosts.GetDefaultScope(((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault());
			this.Client = client ?? new MsalPublicClient(this.Pipeline, tenantId, this.ClientId, AzureAuthorityHosts.GetDeviceCodeRedirectUri(((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault()).AbsoluteUri, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007794 File Offset: 0x00005994
		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return this.Authenticate(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000077C8 File Offset: 0x000059C8
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await this.AuthenticateAsync(new TokenRequestContext(new string[] { this.DefaultScope }, null, null, null, false), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007813 File Offset: 0x00005A13
		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.AuthenticateImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AuthenticationRecord>();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007824 File Offset: 0x00005A24
		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.AuthenticateImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007877 File Offset: 0x00005A77
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007888 File Offset: 0x00005A88
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000078DB File Offset: 0x00005ADB
		internal static Task DefaultDeviceCodeHandler(DeviceCodeInfo deviceCodeInfo, CancellationToken cancellationToken)
		{
			Console.WriteLine(deviceCodeInfo.Message);
			return Task.CompletedTask;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000078F0 File Offset: 0x00005AF0
		private Task<AuthenticationRecord> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			DeviceCodeCredential.<AuthenticateImplAsync>d__43 <AuthenticateImplAsync>d__;
			<AuthenticateImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AuthenticationRecord>.Create();
			<AuthenticateImplAsync>d__.<>4__this = this;
			<AuthenticateImplAsync>d__.async = async;
			<AuthenticateImplAsync>d__.requestContext = requestContext;
			<AuthenticateImplAsync>d__.cancellationToken = cancellationToken;
			<AuthenticateImplAsync>d__.<>1__state = -1;
			<AuthenticateImplAsync>d__.<>t__builder.Start<DeviceCodeCredential.<AuthenticateImplAsync>d__43>(ref <AuthenticateImplAsync>d__);
			return <AuthenticateImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000794C File Offset: 0x00005B4C
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			DeviceCodeCredential.<GetTokenImplAsync>d__44 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<DeviceCodeCredential.<GetTokenImplAsync>d__44>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000079A8 File Offset: 0x00005BA8
		private async Task<AccessToken> GetTokenViaDeviceCodeAsync(TokenRequestContext context, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.Client.AcquireTokenWithDeviceCodeAsync(context.Scopes, context.Claims, (DeviceCodeResult code) => this.DeviceCodeCallbackImpl(code, cancellationToken), context.IsCaeEnabled, async, cancellationToken).ConfigureAwait(false);
			this.Record = new AuthenticationRecord(authenticationResult, this.ClientId);
			return new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007A03 File Offset: 0x00005C03
		private Task DeviceCodeCallbackImpl(DeviceCodeResult deviceCode, CancellationToken cancellationToken)
		{
			return this.DeviceCodeCallback(new DeviceCodeInfo(deviceCode), cancellationToken);
		}

		// Token: 0x0400015B RID: 347
		private readonly string _tenantId;

		// Token: 0x0400015C RID: 348
		internal readonly string[] AdditionallyAllowedTenantIds;

		// Token: 0x04000165 RID: 357
		private const string AuthenticationRequiredMessage = "Interactive authentication is needed to acquire token. Call Authenticate to initiate the device code authentication.";

		// Token: 0x04000166 RID: 358
		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003C2 RID: 962
			public static Func<DeviceCodeInfo, CancellationToken, Task> <0>__DefaultDeviceCodeHandler;
		}
	}
}
