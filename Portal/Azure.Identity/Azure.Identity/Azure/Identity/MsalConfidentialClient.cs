using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;

namespace Azure.Identity
{
	// Token: 0x02000076 RID: 118
	internal class MsalConfidentialClient : MsalClientBase<IConfidentialClientApplication>
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000BD2F File Offset: 0x00009F2F
		internal string RedirectUrl { get; }

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BD37 File Offset: 0x00009F37
		protected MsalConfidentialClient()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BD4A File Offset: 0x00009F4A
		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, string clientSecret, string redirectUrl, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this._clientSecret = clientSecret;
			this.RedirectUrl = redirectUrl;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BD72 File Offset: 0x00009F72
		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, IX509Certificate2Provider certificateProvider, bool includeX5CClaimHeader, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this._includeX5CClaimHeader = includeX5CClaimHeader;
			this._certificateProvider = certificateProvider;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BD9A File Offset: 0x00009F9A
		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<string> assertionCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this._assertionCallback = assertionCallback;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BDBA File Offset: 0x00009FBA
		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<CancellationToken, Task<string>> assertionCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this._asyncAssertionCallback = assertionCallback;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BDDA File Offset: 0x00009FDA
		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> appTokenProviderCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			this._appTokenProviderCallback = appTokenProviderCallback;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000BDFA File Offset: 0x00009FFA
		internal string RegionalAuthority { get; } = EnvironmentVariables.AzureRegionalAuthorityName;

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BE02 File Offset: 0x0000A002
		protected override ValueTask<IConfidentialClientApplication> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			return this.CreateClientCoreAsync(enableCae, async, cancellationToken);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BE10 File Offset: 0x0000A010
		protected virtual async ValueTask<IConfidentialClientApplication> CreateClientCoreAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			string[] array = (enableCae ? this.cp1Capabilities : Array.Empty<string>());
			BaseAbstractApplicationBuilder<ConfidentialClientApplicationBuilder> baseAbstractApplicationBuilder = ConfidentialClientApplicationBuilder.Create(base.ClientId).WithHttpClientFactory(new HttpPipelineClientFactory(base.Pipeline.HttpPipeline));
			LogCallback logCallback = new LogCallback(base.LogMsal);
			bool? flag = new bool?(base.IsSupportLoggingEnabled);
			ConfidentialClientApplicationBuilder confClientBuilder = baseAbstractApplicationBuilder.WithLogging(logCallback, null, flag, null);
			if (this._appTokenProviderCallback != null)
			{
				confClientBuilder.WithAppTokenProvider(this._appTokenProviderCallback).WithAuthority(base.AuthorityHost.AbsoluteUri, base.TenantId, false).WithInstanceDiscovery(false);
			}
			else
			{
				confClientBuilder.WithAuthority(base.AuthorityHost.AbsoluteUri, base.TenantId, true);
				if (base.DisableInstanceDiscovery)
				{
					confClientBuilder.WithInstanceDiscovery(false);
				}
			}
			if (array.Length != 0)
			{
				confClientBuilder.WithClientCapabilities(array);
			}
			if (this._clientSecret != null)
			{
				confClientBuilder.WithClientSecret(this._clientSecret);
			}
			if (this._assertionCallback != null)
			{
				if (this._asyncAssertionCallback != null)
				{
					throw new InvalidOperationException("Cannot set both _assertionCallback and _asyncAssertionCallback");
				}
				confClientBuilder.WithClientAssertion(this._assertionCallback);
			}
			if (this._asyncAssertionCallback != null)
			{
				if (this._assertionCallback != null)
				{
					throw new InvalidOperationException("Cannot set both _assertionCallback and _asyncAssertionCallback");
				}
				confClientBuilder.WithClientAssertion(this._asyncAssertionCallback);
			}
			if (this._certificateProvider != null)
			{
				X509Certificate2 x509Certificate = await this._certificateProvider.GetCertificateAsync(async, cancellationToken).ConfigureAwait(false);
				confClientBuilder.WithCertificate(x509Certificate);
			}
			if (this._appTokenProviderCallback == null && !string.IsNullOrEmpty(this.RegionalAuthority))
			{
				confClientBuilder.WithAzureRegion(this.RegionalAuthority);
			}
			if (!string.IsNullOrEmpty(this.RedirectUrl))
			{
				confClientBuilder.WithRedirectUri(this.RedirectUrl);
			}
			return confClientBuilder.Build();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public virtual async ValueTask<AuthenticationResult> AcquireTokenForClientAsync(string[] scopes, string tenantId, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenForClientCoreAsync(scopes, tenantId, claims, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public virtual async ValueTask<AuthenticationResult> AcquireTokenForClientCoreAsync(string[] scopes, string tenantId, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenForClientParameterBuilder acquireTokenForClientParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenForClient(scopes).WithSendX5C(this._includeX5CClaimHeader);
			if (!string.IsNullOrEmpty(tenantId))
			{
				acquireTokenForClientParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenForClientParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenForClientParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public virtual async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, AuthenticationAccount account, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenSilentCoreAsync(scopes, account, tenantId, redirectUri, claims, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, AuthenticationAccount account, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenSilent(scopes, account);
			if (!string.IsNullOrEmpty(tenantId))
			{
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C064 File Offset: 0x0000A264
		public virtual async ValueTask<AuthenticationResult> AcquireTokenByAuthorizationCodeAsync(string[] scopes, string code, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenByAuthorizationCodeCoreAsync(scopes, code, tenantId, redirectUri, claims, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public virtual async ValueTask<AuthenticationResult> AcquireTokenByAuthorizationCodeCoreAsync(string[] scopes, string code, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenByAuthorizationCodeParameterBuilder acquireTokenByAuthorizationCodeParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenByAuthorizationCode(scopes, code);
			if (!string.IsNullOrEmpty(tenantId))
			{
				acquireTokenByAuthorizationCodeParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByAuthorizationCodeParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenByAuthorizationCodeParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C16C File Offset: 0x0000A36C
		public virtual async ValueTask<AuthenticationResult> AcquireTokenOnBehalfOfAsync(string[] scopes, string tenantId, UserAssertion userAssertionValue, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await this.AcquireTokenOnBehalfOfCoreAsync(scopes, tenantId, userAssertionValue, claims, enableCae, async, cancellationToken).ConfigureAwait(false);
			base.LogAccountDetails(authenticationResult);
			return authenticationResult;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public virtual async ValueTask<AuthenticationResult> AcquireTokenOnBehalfOfCoreAsync(string[] scopes, string tenantId, UserAssertion userAssertionValue, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenOnBehalfOfParameterBuilder acquireTokenOnBehalfOfParameterBuilder = (await base.GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(false)).AcquireTokenOnBehalfOf(scopes, userAssertionValue).WithSendX5C(this._includeX5CClaimHeader);
			if (!string.IsNullOrEmpty(tenantId))
			{
				acquireTokenOnBehalfOfParameterBuilder.WithTenantIdFromAuthority(new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				}.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenOnBehalfOfParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenOnBehalfOfParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0400024F RID: 591
		internal readonly string _clientSecret;

		// Token: 0x04000250 RID: 592
		internal readonly bool _includeX5CClaimHeader;

		// Token: 0x04000251 RID: 593
		internal readonly IX509Certificate2Provider _certificateProvider;

		// Token: 0x04000252 RID: 594
		private readonly Func<string> _assertionCallback;

		// Token: 0x04000253 RID: 595
		private readonly Func<CancellationToken, Task<string>> _asyncAssertionCallback;

		// Token: 0x04000254 RID: 596
		private readonly Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> _appTokenProviderCallback;
	}
}
