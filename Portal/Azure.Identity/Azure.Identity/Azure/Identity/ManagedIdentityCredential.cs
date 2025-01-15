using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x0200004B RID: 75
	public class ManagedIdentityCredential : TokenCredential
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000083AE File Offset: 0x000065AE
		internal ManagedIdentityClient Client { get; }

		// Token: 0x06000288 RID: 648 RVA: 0x000083B6 File Offset: 0x000065B6
		protected ManagedIdentityCredential()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000083C0 File Offset: 0x000065C0
		public ManagedIdentityCredential(string clientId = null, TokenCredentialOptions options = null)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ClientId = clientId,
				Pipeline = CredentialPipeline.GetInstance(options, true),
				Options = options
			}))
		{
			bool? flag;
			if (options == null)
			{
				flag = null;
			}
			else
			{
				TokenCredentialDiagnosticsOptions diagnostics = options.Diagnostics;
				flag = ((diagnostics != null) ? new bool?(diagnostics.IsAccountIdentifierLoggingEnabled) : null);
			}
			bool? flag2 = flag;
			this._logAccountDetails = flag2.GetValueOrDefault();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008434 File Offset: 0x00006634
		public ManagedIdentityCredential(ResourceIdentifier resourceId, TokenCredentialOptions options = null)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ResourceIdentifier = resourceId,
				Pipeline = CredentialPipeline.GetInstance(options, true),
				Options = options
			}))
		{
			bool? flag;
			if (options == null)
			{
				flag = null;
			}
			else
			{
				TokenCredentialDiagnosticsOptions diagnostics = options.Diagnostics;
				flag = ((diagnostics != null) ? new bool?(diagnostics.IsAccountIdentifierLoggingEnabled) : null);
			}
			bool? flag2 = flag;
			this._logAccountDetails = flag2.GetValueOrDefault();
			this._clientId = resourceId.ToString();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000084B3 File Offset: 0x000066B3
		internal ManagedIdentityCredential(string clientId, CredentialPipeline pipeline, TokenCredentialOptions options = null, bool preserveTransport = false)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ClientId = clientId,
				PreserveTransport = preserveTransport,
				Options = options
			}))
		{
			this._clientId = clientId;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000084E9 File Offset: 0x000066E9
		internal ManagedIdentityCredential(ResourceIdentifier resourceId, CredentialPipeline pipeline, TokenCredentialOptions options, bool preserveTransport = false)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ResourceIdentifier = resourceId,
				PreserveTransport = preserveTransport,
				Options = options
			}))
		{
			this._clientId = resourceId.ToString();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008524 File Offset: 0x00006724
		internal ManagedIdentityCredential(ManagedIdentityClient client)
		{
			this._pipeline = client.Pipeline;
			this.Client = client;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008540 File Offset: 0x00006740
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008593 File Offset: 0x00006793
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000085A4 File Offset: 0x000067A4
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			ManagedIdentityCredential.<GetTokenImplAsync>d__16 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<ManagedIdentityCredential.<GetTokenImplAsync>d__16>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000196 RID: 406
		internal const string MsiUnavailableError = "No managed identity endpoint found.";

		// Token: 0x04000197 RID: 407
		private readonly CredentialPipeline _pipeline;

		// Token: 0x04000199 RID: 409
		private readonly string _clientId;

		// Token: 0x0400019A RID: 410
		private readonly bool _logAccountDetails;

		// Token: 0x0400019B RID: 411
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/managedidentitycredential/troubleshoot";
	}
}
