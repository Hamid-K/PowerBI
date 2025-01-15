using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000149 RID: 329
	public sealed class ConfidentialClientApplication : ClientApplicationBase, IConfidentialClientApplication, IClientApplicationBase, IApplicationBase, IConfidentialClientApplicationWithCertificate, IByRefreshToken, ILongRunningWebApi
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x0003B434 File Offset: 0x00039634
		internal ConfidentialClientApplication(ApplicationConfiguration configuration)
			: base(configuration)
		{
			ApplicationBase.GuardMobileFrameworks();
			this.AppTokenCacheInternal = configuration.AppTokenCacheInternalForTest ?? new TokenCache(base.ServiceBundle, true, null);
			this.Certificate = configuration.ClientCredentialCertificate;
			base.ServiceBundle.ApplicationLogger.Verbose(() => string.Format("ConfidentialClientApplication {0} created", configuration.GetHashCode()));
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003B4AE File Offset: 0x000396AE
		public AcquireTokenByAuthorizationCodeParameterBuilder AcquireTokenByAuthorizationCode(IEnumerable<string> scopes, string authorizationCode)
		{
			return AcquireTokenByAuthorizationCodeParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes, authorizationCode);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003B4BD File Offset: 0x000396BD
		public AcquireTokenForClientParameterBuilder AcquireTokenForClient(IEnumerable<string> scopes)
		{
			return AcquireTokenForClientParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003B4CB File Offset: 0x000396CB
		public AcquireTokenOnBehalfOfParameterBuilder AcquireTokenOnBehalfOf(IEnumerable<string> scopes, UserAssertion userAssertion)
		{
			if (userAssertion == null)
			{
				base.ServiceBundle.ApplicationLogger.Error("User assertion for OBO request should not be null");
				throw new MsalClientException("user_assertion_null");
			}
			return AcquireTokenOnBehalfOfParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes, userAssertion);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003B500 File Offset: 0x00039700
		public AcquireTokenOnBehalfOfParameterBuilder InitiateLongRunningProcessInWebApi(IEnumerable<string> scopes, string userToken, ref string longRunningProcessSessionKey)
		{
			if (string.IsNullOrEmpty(userToken))
			{
				throw new ArgumentNullException("userToken");
			}
			UserAssertion userAssertion = new UserAssertion(userToken);
			if (string.IsNullOrEmpty(longRunningProcessSessionKey))
			{
				longRunningProcessSessionKey = userAssertion.AssertionHash;
			}
			return AcquireTokenOnBehalfOfParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes, userAssertion, longRunningProcessSessionKey);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003B547 File Offset: 0x00039747
		public AcquireTokenOnBehalfOfParameterBuilder AcquireTokenInLongRunningProcess(IEnumerable<string> scopes, string longRunningProcessSessionKey)
		{
			if (string.IsNullOrEmpty(longRunningProcessSessionKey))
			{
				throw new ArgumentNullException("longRunningProcessSessionKey");
			}
			return AcquireTokenOnBehalfOfParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes, longRunningProcessSessionKey);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003B56C File Offset: 0x0003976C
		public async Task<bool> StopLongRunningProcessInWebApiAsync(string longRunningProcessSessionKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(longRunningProcessSessionKey))
			{
				throw new ArgumentNullException("longRunningProcessSessionKey");
			}
			Guid guid = Guid.NewGuid();
			RequestContext requestContext = base.CreateRequestContext(guid, cancellationToken);
			requestContext.ApiEvent = new ApiEvent(guid);
			requestContext.ApiEvent.ApiId = ApiEvent.ApiIds.RemoveOboTokens;
			Authority authority = await Microsoft.Identity.Client.Instance.Authority.CreateAuthorityForRequestAsync(requestContext, null, null).ConfigureAwait(false);
			AuthenticationRequestParameters authenticationRequestParameters = new AuthenticationRequestParameters(base.ServiceBundle, base.UserTokenCacheInternal, new AcquireTokenCommonParameters
			{
				ApiId = requestContext.ApiEvent.ApiId
			}, requestContext, authority, null);
			bool flag;
			if (base.UserTokenCacheInternal != null)
			{
				flag = await base.UserTokenCacheInternal.StopLongRunningOboProcessAsync(longRunningProcessSessionKey, authenticationRequestParameters).ConfigureAwait(false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003B5BF File Offset: 0x000397BF
		public GetAuthorizationRequestUrlParameterBuilder GetAuthorizationRequestUrl(IEnumerable<string> scopes)
		{
			return GetAuthorizationRequestUrlParameterBuilder.Create(ClientExecutorFactory.CreateConfidentialClientExecutor(this), scopes);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003B5CD File Offset: 0x000397CD
		AcquireTokenByRefreshTokenParameterBuilder IByRefreshToken.AcquireTokenByRefreshToken(IEnumerable<string> scopes, string refreshToken)
		{
			return AcquireTokenByRefreshTokenParameterBuilder.Create(ClientExecutorFactory.CreateClientApplicationBaseExecutor(this), scopes, refreshToken);
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0003B5DC File Offset: 0x000397DC
		public ITokenCache AppTokenCache
		{
			get
			{
				return this.AppTokenCacheInternal;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0003B5E4 File Offset: 0x000397E4
		public X509Certificate2 Certificate { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0003B5EC File Offset: 0x000397EC
		internal ITokenCacheInternal AppTokenCacheInternal { get; }

		// Token: 0x06001085 RID: 4229 RVA: 0x0003B5F4 File Offset: 0x000397F4
		internal override async Task<AuthenticationRequestParameters> CreateRequestParametersAsync(AcquireTokenCommonParameters commonParameters, RequestContext requestContext, ITokenCacheInternal cache)
		{
			return await base.CreateRequestParametersAsync(commonParameters, requestContext, cache).ConfigureAwait(false);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003B64F File Offset: 0x0003984F
		[Obsolete("Use ConfidentialClientApplicationBuilder instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ConfidentialClientApplication(string clientId, string redirectUri, ClientCredential clientCredential, TokenCache userTokenCache, TokenCache appTokenCache)
			: this(ConfidentialClientApplicationBuilder.Create(clientId).BuildConfiguration())
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003B667 File Offset: 0x00039867
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ConfidentialClientApplicationBuilder instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public ConfidentialClientApplication(string clientId, string authority, string redirectUri, ClientCredential clientCredential, TokenCache userTokenCache, TokenCache appTokenCache)
			: this(ConfidentialClientApplicationBuilder.Create(clientId).BuildConfiguration())
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0003B67F File Offset: 0x0003987F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenOnBehalfOfAsync(IEnumerable<string> scopes, UserAssertion userAssertion)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003B686 File Offset: 0x00039886
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenOnBehalfOfAsync(IEnumerable<string> scopes, UserAssertion userAssertion, string authority)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003B68D File Offset: 0x0003988D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IConfidentialClientApplicationWithCertificate.AcquireTokenOnBehalfOfWithCertificateAsync(IEnumerable<string> scopes, UserAssertion userAssertion)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0003B694 File Offset: 0x00039894
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IConfidentialClientApplicationWithCertificate.AcquireTokenOnBehalfOfWithCertificateAsync(IEnumerable<string> scopes, UserAssertion userAssertion, string authority)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0003B69B File Offset: 0x0003989B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByAuthorizationCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenByAuthorizationCodeAsync(string authorizationCode, IEnumerable<string> scopes)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003B6A2 File Offset: 0x000398A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenForClientAsync(IEnumerable<string> scopes)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003B6A9 File Offset: 0x000398A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<AuthenticationResult> AcquireTokenForClientAsync(IEnumerable<string> scopes, bool forceRefresh)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003B6B0 File Offset: 0x000398B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IConfidentialClientApplicationWithCertificate.AcquireTokenForClientWithCertificateAsync(IEnumerable<string> scopes)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003B6B7 File Offset: 0x000398B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IConfidentialClientApplicationWithCertificate.AcquireTokenForClientWithCertificateAsync(IEnumerable<string> scopes, bool forceRefresh)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003B6BE File Offset: 0x000398BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByRefreshToken instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> IByRefreshToken.AcquireTokenByRefreshTokenAsync(IEnumerable<string> scopes, string refreshToken)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003B6C5 File Offset: 0x000398C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAuthorizationRequestUrl instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<Uri> GetAuthorizationRequestUrlAsync(IEnumerable<string> scopes, string loginHint, string extraQueryParameters)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003B6CC File Offset: 0x000398CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAuthorizationRequestUrl instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public Task<Uri> GetAuthorizationRequestUrlAsync(IEnumerable<string> scopes, string redirectUri, string loginHint, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x04000508 RID: 1288
		public const string AttemptRegionDiscovery = "TryAutoDetect";
	}
}
