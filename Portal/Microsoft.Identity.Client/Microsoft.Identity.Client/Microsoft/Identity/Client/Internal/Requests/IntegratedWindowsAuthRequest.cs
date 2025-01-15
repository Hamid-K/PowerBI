using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.WsTrust;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000245 RID: 581
	internal class IntegratedWindowsAuthRequest : RequestBase
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x0004DEAE File Offset: 0x0004C0AE
		public IntegratedWindowsAuthRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByIntegratedWindowsAuthParameters integratedWindowsAuthParameters)
			: base(serviceBundle, authenticationRequestParameters, integratedWindowsAuthParameters)
		{
			this._integratedWindowsAuthParameters = integratedWindowsAuthParameters;
			this._commonNonInteractiveHandler = new CommonNonInteractiveHandler(authenticationRequestParameters.RequestContext, serviceBundle);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0004DED4 File Offset: 0x0004C0D4
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			await this.UpdateUsernameAsync().ConfigureAwait(false);
			UserAssertion userAssertion = await this.FetchAssertionFromWsTrustAsync().ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = await base.SendTokenRequestAsync(IntegratedWindowsAuthRequest.GetAdditionalBodyParameters(userAssertion), cancellationToken).ConfigureAwait(false);
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0004DF1F File Offset: 0x0004C11F
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			return base.GetCcsUpnHeader(this._integratedWindowsAuthParameters.Username);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0004DF34 File Offset: 0x0004C134
		private async Task<UserAssertion> FetchAssertionFromWsTrustAsync()
		{
			if (!base.AuthenticationRequestParameters.AuthorityInfo.IsWsTrustFlowSupported)
			{
				throw new MsalClientException("integrated_windows_authentication_failed", "Integrated windows authenticaiton is not supported when using WithAdfsAuthority() to specify the authority in ADFS on premises environments See https://aka.ms/msal-net-iwa for more details.");
			}
			UserRealmDiscoveryResponse userRealmDiscoveryResponse = await this._commonNonInteractiveHandler.QueryUserRealmDataAsync(base.AuthenticationRequestParameters.AuthorityInfo.UserRealmUriPrefix, this._integratedWindowsAuthParameters.Username).ConfigureAwait(false);
			if (userRealmDiscoveryResponse.IsFederated)
			{
				WsTrustResponse wsTrustResponse = await this._commonNonInteractiveHandler.PerformWsTrustMexExchangeAsync(userRealmDiscoveryResponse.FederationMetadataUrl, userRealmDiscoveryResponse.CloudAudienceUrn, UserAuthType.IntegratedAuth, this._integratedWindowsAuthParameters.Username, null, this._integratedWindowsAuthParameters.FederationMetadata).ConfigureAwait(false);
				return new UserAssertion(wsTrustResponse.Token, (wsTrustResponse.TokenType == "urn:oasis:names:tc:SAML:1.0:assertion") ? "urn:ietf:params:oauth:grant-type:saml1_1-bearer" : "urn:ietf:params:oauth:grant-type:saml2-bearer");
			}
			if (userRealmDiscoveryResponse.IsManaged)
			{
				throw new MsalClientException("integrated_windows_auth_not_supported_managed_user", "Integrated Windows Auth is not supported for managed users. See https://aka.ms/msal-net-iwa for details. ");
			}
			throw new MsalClientException("unknown_user_type", string.Format(CultureInfo.CurrentCulture, "Unsupported User Type '{0}'. Please see https://aka.ms/msal-net-up. ", userRealmDiscoveryResponse.AccountType));
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0004DF78 File Offset: 0x0004C178
		private async Task UpdateUsernameAsync()
		{
			if (string.IsNullOrWhiteSpace(this._integratedWindowsAuthParameters.Username))
			{
				string text = await this._commonNonInteractiveHandler.GetPlatformUserAsync().ConfigureAwait(false);
				this._integratedWindowsAuthParameters.Username = text;
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0004DFBC File Offset: 0x0004C1BC
		private static Dictionary<string, string> GetAdditionalBodyParameters(UserAssertion userAssertion)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (userAssertion != null)
			{
				dictionary["client_info"] = "1";
				dictionary["grant_type"] = userAssertion.AssertionType;
				dictionary["assertion"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(userAssertion.Assertion));
			}
			return dictionary;
		}

		// Token: 0x04000A4E RID: 2638
		private readonly CommonNonInteractiveHandler _commonNonInteractiveHandler;

		// Token: 0x04000A4F RID: 2639
		private readonly AcquireTokenByIntegratedWindowsAuthParameters _integratedWindowsAuthParameters;
	}
}
