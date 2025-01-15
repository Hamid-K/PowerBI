using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200003C RID: 60
	public class ClientCertificateCredential : TokenCredential
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000068AA File Offset: 0x00004AAA
		internal string TenantId { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000068B2 File Offset: 0x00004AB2
		internal string ClientId { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000068BA File Offset: 0x00004ABA
		internal IX509Certificate2Provider ClientCertificateProvider { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000068C2 File Offset: 0x00004AC2
		internal MsalConfidentialClient Client { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000068CA File Offset: 0x00004ACA
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x060001A1 RID: 417 RVA: 0x000068D2 File Offset: 0x00004AD2
		protected ClientCertificateCredential()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000068DA File Offset: 0x00004ADA
		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath)
			: this(tenantId, clientId, clientCertificatePath, null, null, null, null)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000068E9 File Offset: 0x00004AE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath, TokenCredentialOptions options)
			: this(tenantId, clientId, clientCertificatePath, null, options, null, null)
		{
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000068F9 File Offset: 0x00004AF9
		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath, ClientCertificateCredentialOptions options)
			: this(tenantId, clientId, clientCertificatePath, null, options, null, null)
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006909 File Offset: 0x00004B09
		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate)
			: this(tenantId, clientId, clientCertificate, null, null, null)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006917 File Offset: 0x00004B17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, TokenCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, options, null, null)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006926 File Offset: 0x00004B26
		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, ClientCertificateCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, options, null, null)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006935 File Offset: 0x00004B35
		internal ClientCertificateCredential(string tenantId, string clientId, string certificatePath, string certificatePassword, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			if (certificatePath == null)
			{
				throw new ArgumentNullException("certificatePath");
			}
			this..ctor(tenantId, clientId, new X509Certificate2FromFileProvider(certificatePath, certificatePassword), options, pipeline, client);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000695C File Offset: 0x00004B5C
		internal ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 certificate, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this..ctor(tenantId, clientId, new X509Certificate2FromObjectProvider(certificate), options, pipeline, client);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006984 File Offset: 0x00004B84
		internal ClientCertificateCredential(string tenantId, string clientId, IX509Certificate2Provider certificateProvider, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			this.TenantId = Validations.ValidateTenantId(tenantId, "tenantId", false);
			if (clientId == null)
			{
				throw new ArgumentNullException("clientId");
			}
			this.ClientId = clientId;
			this.ClientCertificateProvider = certificateProvider;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			ClientCertificateCredentialOptions clientCertificateCredentialOptions = options as ClientCertificateCredentialOptions;
			this.Client = client ?? new MsalConfidentialClient(this._pipeline, tenantId, clientId, certificateProvider, clientCertificateCredentialOptions != null && clientCertificateCredentialOptions.SendCertificateChain, options);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			TenantIdResolverBase tenantIdResolver = this.TenantIdResolver;
			ISupportsAdditionallyAllowedTenants supportsAdditionallyAllowedTenants = options as ISupportsAdditionallyAllowedTenants;
			this.AdditionallyAllowedTenantIds = tenantIdResolver.ResolveAddionallyAllowedTenantIds((supportsAdditionallyAllowedTenants != null) ? supportsAdditionallyAllowedTenants.AdditionallyAllowedTenants : null);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006A4C File Offset: 0x00004C4C
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			AccessToken accessToken;
			using (CredentialDiagnosticScope credentialDiagnosticScope = this._pipeline.StartGetTokenScope("ClientCertificateCredential.GetToken", requestContext))
			{
				try
				{
					string text = this.TenantIdResolver.Resolve(this.TenantId, requestContext, this.AdditionallyAllowedTenantIds);
					AuthenticationResult authenticationResult = this.Client.AcquireTokenForClientAsync(requestContext.Scopes, text, requestContext.Claims, requestContext.IsCaeEnabled, false, cancellationToken).EnsureCompleted<AuthenticationResult>();
					accessToken = credentialDiagnosticScope.Succeeded(new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn));
				}
				catch (Exception ex)
				{
					throw credentialDiagnosticScope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/clientcertificatecredential/troubleshoot", false);
				}
			}
			return accessToken;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006B08 File Offset: 0x00004D08
		public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			ClientCertificateCredential.<GetTokenAsync>d__29 <GetTokenAsync>d__;
			<GetTokenAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenAsync>d__.<>4__this = this;
			<GetTokenAsync>d__.requestContext = requestContext;
			<GetTokenAsync>d__.cancellationToken = cancellationToken;
			<GetTokenAsync>d__.<>1__state = -1;
			<GetTokenAsync>d__.<>t__builder.Start<ClientCertificateCredential.<GetTokenAsync>d__29>(ref <GetTokenAsync>d__);
			return <GetTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000128 RID: 296
		internal const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/clientcertificatecredential/troubleshoot";

		// Token: 0x0400012D RID: 301
		private readonly CredentialPipeline _pipeline;

		// Token: 0x0400012E RID: 302
		internal readonly string[] AdditionallyAllowedTenantIds;
	}
}
