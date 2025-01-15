using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000044 RID: 68
	public class EnvironmentCredential : TokenCredential
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007AC4 File Offset: 0x00005CC4
		internal TokenCredential Credential { get; }

		// Token: 0x06000230 RID: 560 RVA: 0x00007ACC File Offset: 0x00005CCC
		public EnvironmentCredential()
			: this(CredentialPipeline.GetInstance(null, false), null)
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007ADC File Offset: 0x00005CDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public EnvironmentCredential(TokenCredentialOptions options)
			: this(CredentialPipeline.GetInstance(options, false), options)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007AEC File Offset: 0x00005CEC
		public EnvironmentCredential(EnvironmentCredentialOptions options)
			: this(CredentialPipeline.GetInstance(options, false), options)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal EnvironmentCredential(CredentialPipeline pipeline, TokenCredentialOptions options = null)
		{
			this._pipeline = pipeline;
			options = options ?? new EnvironmentCredentialOptions();
			EnvironmentCredentialOptions environmentCredentialOptions = (options as EnvironmentCredentialOptions) ?? options.Clone<EnvironmentCredentialOptions>();
			string tenantId = environmentCredentialOptions.TenantId;
			string clientId = environmentCredentialOptions.ClientId;
			string clientSecret = environmentCredentialOptions.ClientSecret;
			string clientCertificatePath = environmentCredentialOptions.ClientCertificatePath;
			string clientCertificatePassword = environmentCredentialOptions.ClientCertificatePassword;
			bool sendCertificateChain = environmentCredentialOptions.SendCertificateChain;
			string username = environmentCredentialOptions.Username;
			string password = environmentCredentialOptions.Password;
			if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(clientId))
			{
				if (!string.IsNullOrEmpty(clientSecret))
				{
					this.Credential = new ClientSecretCredential(tenantId, clientId, clientSecret, environmentCredentialOptions, this._pipeline, environmentCredentialOptions.MsalConfidentialClient);
					return;
				}
				if (!string.IsNullOrEmpty(clientCertificatePath))
				{
					ClientCertificateCredentialOptions clientCertificateCredentialOptions = environmentCredentialOptions.Clone<ClientCertificateCredentialOptions>();
					clientCertificateCredentialOptions.SendCertificateChain = sendCertificateChain;
					this.Credential = new ClientCertificateCredential(tenantId, clientId, clientCertificatePath, clientCertificatePassword, clientCertificateCredentialOptions, this._pipeline, environmentCredentialOptions.MsalConfidentialClient);
					return;
				}
				if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
				{
					this.Credential = new UsernamePasswordCredential(username, password, tenantId, clientId, environmentCredentialOptions, this._pipeline, environmentCredentialOptions.MsalPublicClient);
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007C14 File Offset: 0x00005E14
		internal EnvironmentCredential(CredentialPipeline pipeline, TokenCredential credential)
		{
			this._pipeline = pipeline;
			this.Credential = credential;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007C2A File Offset: 0x00005E2A
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007C3C File Offset: 0x00005E3C
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007C90 File Offset: 0x00005E90
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			EnvironmentCredential.<GetTokenImplAsync>d__12 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<EnvironmentCredential.<GetTokenImplAsync>d__12>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400016F RID: 367
		private const string UnavailableErrorMessage = "EnvironmentCredential authentication unavailable. Environment variables are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/environmentcredential/troubleshoot";

		// Token: 0x04000170 RID: 368
		private readonly CredentialPipeline _pipeline;
	}
}
