using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Client.WsTrust;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x0200024C RID: 588
	internal class UsernamePasswordRequest : RequestBase
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0004F648 File Offset: 0x0004D848
		public UsernamePasswordRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByUsernamePasswordParameters usernamePasswordParameters)
			: base(serviceBundle, authenticationRequestParameters, usernamePasswordParameters)
		{
			this._usernamePasswordParameters = usernamePasswordParameters;
			this._requestParameters = authenticationRequestParameters;
			this._commonNonInteractiveHandler = new CommonNonInteractiveHandler(authenticationRequestParameters.RequestContext, serviceBundle);
			this._logger = this._requestParameters.RequestContext.Logger;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0004F694 File Offset: 0x0004D894
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			await this.UpdateUsernameAsync().ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse;
			try
			{
				msalTokenResponse = await this.GetTokenResponseAsync(cancellationToken).ConfigureAwait(false);
			}
			catch (JsonException ex)
			{
				throw new MsalServiceException("json_parse_failed", "There was an error parsing the response from the token endpoint, see inner exception for details. Verify that your app is configured correctly. If this is a B2C app, one possible cause is acquiring a token for Microsoft Graph, which is not supported. See https://aka.ms/msal-net-up", ex);
			}
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0004F6E0 File Offset: 0x0004D8E0
		private async Task<MsalTokenResponse> GetTokenResponseAsync(CancellationToken cancellationToken)
		{
			if (this._requestParameters.AppConfig.IsBrokerEnabled)
			{
				this._logger.Info("Broker is configured. Starting broker flow. ");
				IBroker broker = this._requestParameters.RequestContext.ServiceBundle.PlatformProxy.CreateBroker(this._requestParameters.RequestContext.ServiceBundle.Config, null);
				if (broker.IsBrokerInstalledAndInvokable(this._requestParameters.AuthorityInfo.AuthorityType))
				{
					this._logger.Info("Can invoke broker. Will attempt to acquire token with broker. ");
					MsalTokenResponse msalTokenResponse = await broker.AcquireTokenByUsernamePasswordAsync(this._requestParameters, this._usernamePasswordParameters).ConfigureAwait(false);
					if (msalTokenResponse != null)
					{
						this._logger.Info("Broker attempt completed successfully. ");
						Metrics.IncrementTotalAccessTokensFromBroker();
						return msalTokenResponse;
					}
					if (string.Equals(this._requestParameters.AuthenticationScheme.AccessTokenType, "pop"))
					{
						this._logger.Error("A broker application is required for Proof-of-Possesion, but one could not be found or communicated with. See https://aka.ms/msal-net-pop");
						throw new MsalClientException("broker_application_required", "MSAL cannot invoke the broker and it is required for Proof-of-Possession. WAM (Broker) may not be installed on the user's device or there was an error invoking the broker. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.Check logs for more details and see https://aka.ms/msal-net-pop. ");
					}
				}
				this._logger.Info("Broker request not attempted because the broker is not available.");
				cancellationToken.ThrowIfCancellationRequested();
			}
			UserAssertion userAssertion = await this.FetchAssertionFromWsTrustAsync().ConfigureAwait(false);
			return await base.SendTokenRequestAsync(this.GetAdditionalBodyParameters(userAssertion), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0004F72C File Offset: 0x0004D92C
		private async Task<UserAssertion> FetchAssertionFromWsTrustAsync()
		{
			UserAssertion userAssertion;
			if (!base.AuthenticationRequestParameters.AuthorityInfo.IsWsTrustFlowSupported)
			{
				userAssertion = null;
			}
			else
			{
				UserRealmDiscoveryResponse userRealmDiscoveryResponse = await this._commonNonInteractiveHandler.QueryUserRealmDataAsync(base.AuthenticationRequestParameters.AuthorityInfo.UserRealmUriPrefix, this._usernamePasswordParameters.Username).ConfigureAwait(false);
				if (userRealmDiscoveryResponse.IsFederated)
				{
					WsTrustResponse wsTrustResponse = await this._commonNonInteractiveHandler.PerformWsTrustMexExchangeAsync(userRealmDiscoveryResponse.FederationMetadataUrl, userRealmDiscoveryResponse.CloudAudienceUrn, UserAuthType.UsernamePassword, this._usernamePasswordParameters.Username, this._usernamePasswordParameters.Password, this._usernamePasswordParameters.FederationMetadata).ConfigureAwait(false);
					userAssertion = new UserAssertion(wsTrustResponse.Token, (wsTrustResponse.TokenType == "urn:oasis:names:tc:SAML:1.0:assertion") ? "urn:ietf:params:oauth:grant-type:saml1_1-bearer" : "urn:ietf:params:oauth:grant-type:saml2-bearer");
				}
				else
				{
					if (!userRealmDiscoveryResponse.IsManaged)
					{
						throw new MsalClientException("unknown_user_type", string.Format(CultureInfo.CurrentCulture, "Unsupported User Type '{0}'. Please see https://aka.ms/msal-net-up. ", userRealmDiscoveryResponse.AccountType));
					}
					if (this._usernamePasswordParameters.Password == null)
					{
						throw new MsalClientException("password_required_for_managed_user");
					}
					userAssertion = null;
				}
			}
			return userAssertion;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0004F770 File Offset: 0x0004D970
		private async Task UpdateUsernameAsync()
		{
			if (string.IsNullOrWhiteSpace(this._usernamePasswordParameters.Username))
			{
				string text = await this._commonNonInteractiveHandler.GetPlatformUserAsync().ConfigureAwait(false);
				this._usernamePasswordParameters.Username = text;
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0004F7B4 File Offset: 0x0004D9B4
		private Dictionary<string, string> GetAdditionalBodyParameters(UserAssertion userAssertion)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (userAssertion != null)
			{
				dictionary["grant_type"] = userAssertion.AssertionType;
				dictionary["assertion"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(userAssertion.Assertion));
			}
			else
			{
				dictionary["grant_type"] = "password";
				dictionary["username"] = this._usernamePasswordParameters.Username;
				dictionary["password"] = this._usernamePasswordParameters.Password;
			}
			ISet<string> set = new HashSet<string> { "openid", "offline_access", "profile" };
			set.UnionWith(base.AuthenticationRequestParameters.Scope);
			dictionary["scope"] = set.AsSingleString();
			dictionary["client_info"] = "1";
			return dictionary;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0004F896 File Offset: 0x0004DA96
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			return base.GetCcsUpnHeader(this._usernamePasswordParameters.Username);
		}

		// Token: 0x04000A6A RID: 2666
		private readonly CommonNonInteractiveHandler _commonNonInteractiveHandler;

		// Token: 0x04000A6B RID: 2667
		private readonly AcquireTokenByUsernamePasswordParameters _usernamePasswordParameters;

		// Token: 0x04000A6C RID: 2668
		private readonly AuthenticationRequestParameters _requestParameters;

		// Token: 0x04000A6D RID: 2669
		private readonly ILoggerAdapter _logger;
	}
}
