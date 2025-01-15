using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000057 RID: 87
	public class WorkloadIdentityCredential : TokenCredential
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00009E51 File Offset: 0x00008051
		internal MsalConfidentialClient Client
		{
			get
			{
				ClientAssertionCredential clientAssertionCredential = this._clientAssertionCredential;
				if (clientAssertionCredential == null)
				{
					return null;
				}
				return clientAssertionCredential.Client;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00009E64 File Offset: 0x00008064
		internal string[] AdditionallyAllowedTenantIds
		{
			get
			{
				ClientAssertionCredential clientAssertionCredential = this._clientAssertionCredential;
				if (clientAssertionCredential == null)
				{
					return null;
				}
				return clientAssertionCredential.AdditionallyAllowedTenantIds;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009E77 File Offset: 0x00008077
		public WorkloadIdentityCredential()
			: this(null)
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009E80 File Offset: 0x00008080
		public WorkloadIdentityCredential(WorkloadIdentityCredentialOptions options)
		{
			options = options ?? new WorkloadIdentityCredentialOptions();
			if (!string.IsNullOrEmpty(options.TenantId) && !string.IsNullOrEmpty(options.ClientId) && !string.IsNullOrEmpty(options.TokenFilePath))
			{
				this._tokenFileCache = new FileContentsCache(options.TokenFilePath, null);
				ClientAssertionCredentialOptions clientAssertionCredentialOptions = options.Clone<ClientAssertionCredentialOptions>();
				clientAssertionCredentialOptions.Pipeline = options.Pipeline;
				clientAssertionCredentialOptions.MsalClient = options.MsalClient;
				this._clientAssertionCredential = new ClientAssertionCredential(options.TenantId, options.ClientId, new Func<CancellationToken, Task<string>>(this._tokenFileCache.GetTokenFileContentsAsync), clientAssertionCredentialOptions);
			}
			ClientAssertionCredential clientAssertionCredential = this._clientAssertionCredential;
			this._pipeline = ((clientAssertionCredential != null) ? clientAssertionCredential.Pipeline : null) ?? CredentialPipeline.GetInstance(null, false);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009F4B File Offset: 0x0000814B
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenCoreAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009F5C File Offset: 0x0000815C
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenCoreAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009FB0 File Offset: 0x000081B0
		private ValueTask<AccessToken> GetTokenCoreAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			WorkloadIdentityCredential.<GetTokenCoreAsync>d__12 <GetTokenCoreAsync>d__;
			<GetTokenCoreAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenCoreAsync>d__.<>4__this = this;
			<GetTokenCoreAsync>d__.async = async;
			<GetTokenCoreAsync>d__.requestContext = requestContext;
			<GetTokenCoreAsync>d__.cancellationToken = cancellationToken;
			<GetTokenCoreAsync>d__.<>1__state = -1;
			<GetTokenCoreAsync>d__.<>t__builder.Start<WorkloadIdentityCredential.<GetTokenCoreAsync>d__12>(ref <GetTokenCoreAsync>d__);
			return <GetTokenCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001EE RID: 494
		private const string UnavailableErrorMessage = "WorkloadIdentityCredential authentication unavailable. The workload options are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/workloadidentitycredential/troubleshoot";

		// Token: 0x040001EF RID: 495
		private readonly FileContentsCache _tokenFileCache;

		// Token: 0x040001F0 RID: 496
		private readonly ClientAssertionCredential _clientAssertionCredential;

		// Token: 0x040001F1 RID: 497
		private readonly CredentialPipeline _pipeline;
	}
}
