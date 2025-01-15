using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200004C RID: 76
	public class OnBehalfOfCredential : TokenCredential
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000085FF File Offset: 0x000067FF
		internal MsalConfidentialClient Client { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008607 File Offset: 0x00006807
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000860F File Offset: 0x0000680F
		protected OnBehalfOfCredential()
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008617 File Offset: 0x00006817
		public OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, string userAssertion)
			: this(tenantId, clientId, clientCertificate, userAssertion, null, null, null)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008627 File Offset: 0x00006827
		public OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, string userAssertion, OnBehalfOfCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, userAssertion, options, null, null)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008638 File Offset: 0x00006838
		public OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion)
			: this(tenantId, clientId, clientSecret, userAssertion, null, null, null)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008648 File Offset: 0x00006848
		public OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion, OnBehalfOfCredentialOptions options)
			: this(tenantId, clientId, clientSecret, userAssertion, options, null, null)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008659 File Offset: 0x00006859
		internal OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 certificate, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this..ctor(tenantId, clientId, new X509Certificate2FromObjectProvider(certificate), userAssertion, options, pipeline, client);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008680 File Offset: 0x00006880
		internal OnBehalfOfCredential(string tenantId, string clientId, IX509Certificate2Provider certificateProvider, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			this._tenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			if (clientId == null)
			{
				throw new ArgumentNullException("clientId");
			}
			this._clientId = clientId;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			if (options == null)
			{
				options = new OnBehalfOfCredentialOptions();
			}
			this._userAssertion = new UserAssertion(userAssertion);
			this.Client = client ?? new MsalConfidentialClient(this._pipeline, tenantId, clientId, certificateProvider, options.SendCertificateChain, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			OnBehalfOfCredentialOptions onBehalfOfCredentialOptions = options;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((onBehalfOfCredentialOptions != null) ? ((ISupportsAdditionallyAllowedTenants)onBehalfOfCredentialOptions).AdditionallyAllowedTenants : null);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008748 File Offset: 0x00006948
		internal OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			Argument.AssertNotNull<string>(clientId, "clientId");
			Argument.AssertNotNull<string>(clientSecret, "clientSecret");
			if (options == null)
			{
				options = new OnBehalfOfCredentialOptions();
			}
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this._tenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			this._clientId = clientId;
			this._clientSecret = clientSecret;
			this._userAssertion = new UserAssertion(userAssertion);
			this.Client = client ?? new MsalConfidentialClient(this._pipeline, this._tenantId, this._clientId, this._clientSecret, null, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? options.AdditionallyAllowedTenants : null);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008825 File Offset: 0x00006A25
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return this.GetTokenInternalAsync(requestContext, false, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008835 File Offset: 0x00006A35
		public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return this.GetTokenInternalAsync(requestContext, true, cancellationToken);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008840 File Offset: 0x00006A40
		internal ValueTask<AccessToken> GetTokenInternalAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			OnBehalfOfCredential.<GetTokenInternalAsync>d__22 <GetTokenInternalAsync>d__;
			<GetTokenInternalAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenInternalAsync>d__.<>4__this = this;
			<GetTokenInternalAsync>d__.requestContext = requestContext;
			<GetTokenInternalAsync>d__.async = async;
			<GetTokenInternalAsync>d__.cancellationToken = cancellationToken;
			<GetTokenInternalAsync>d__.<>1__state = -1;
			<GetTokenInternalAsync>d__.<>t__builder.Start<OnBehalfOfCredential.<GetTokenInternalAsync>d__22>(ref <GetTokenInternalAsync>d__);
			return <GetTokenInternalAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400019D RID: 413
		private readonly string _tenantId;

		// Token: 0x0400019E RID: 414
		private readonly CredentialPipeline _pipeline;

		// Token: 0x0400019F RID: 415
		private readonly string _clientId;

		// Token: 0x040001A0 RID: 416
		private readonly string _clientSecret;

		// Token: 0x040001A1 RID: 417
		private readonly UserAssertion _userAssertion;

		// Token: 0x040001A2 RID: 418
		internal readonly string[] AdditionallyAllowedTenantIds;
	}
}
