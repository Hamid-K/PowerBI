using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000247 RID: 583
	internal class InteractiveRequest : RequestBase
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
		public InteractiveRequest(AuthenticationRequestParameters requestParams, AcquireTokenInteractiveParameters interactiveParameters, IAuthCodeRequestComponent authCodeRequestComponentOverride = null, ITokenRequestComponent authCodeExchangeComponentOverride = null, ITokenRequestComponent brokerExchangeComponentOverride = null)
		{
			IServiceBundle serviceBundle;
			if (requestParams == null)
			{
				serviceBundle = null;
			}
			else
			{
				RequestContext requestContext = requestParams.RequestContext;
				serviceBundle = ((requestContext != null) ? requestContext.ServiceBundle : null);
			}
			base..ctor(serviceBundle, requestParams, interactiveParameters);
			if (requestParams == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._requestParams = requestParams;
			if (interactiveParameters == null)
			{
				throw new ArgumentNullException("interactiveParameters");
			}
			this._interactiveParameters = interactiveParameters;
			this._authCodeRequestComponentOverride = authCodeRequestComponentOverride;
			this._authCodeExchangeComponentOverride = authCodeExchangeComponentOverride;
			this._brokerInteractiveComponent = brokerExchangeComponentOverride;
			this._serviceBundle = requestParams.RequestContext.ServiceBundle;
			this._logger = requestParams.RequestContext.Logger;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0004E248 File Offset: 0x0004C448
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			MsalTokenResponse msalTokenResponse = await this.GetTokenResponseAsync(cancellationToken).ConfigureAwait(false);
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0004E294 File Offset: 0x0004C494
		private async Task<MsalTokenResponse> FetchTokensFromBrokerAsync(string brokerInstallUrl, CancellationToken cancellationToken)
		{
			IBroker broker = this._serviceBundle.PlatformProxy.CreateBroker(this._serviceBundle.Config, this._interactiveParameters.UiParent);
			return await (this._brokerInteractiveComponent ?? new BrokerInteractiveRequestComponent(this._requestParams, this._interactiveParameters, broker, brokerInstallUrl)).FetchTokensAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0004E2E8 File Offset: 0x0004C4E8
		private async Task<MsalTokenResponse> GetTokenResponseAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this._requestParams.AppConfig.IsBrokerEnabled)
			{
				this._logger.Info("Broker is configured. Starting broker flow without knowing the broker installation app link. ");
				MsalTokenResponse msalTokenResponse = await this.FetchTokensFromBrokerAsync(null, cancellationToken).ConfigureAwait(false);
				if (msalTokenResponse != null)
				{
					this._logger.Info("Broker attempt completed successfully. ");
					Metrics.IncrementTotalAccessTokensFromBroker();
					return msalTokenResponse;
				}
				if (string.Equals(this._requestParams.AuthenticationScheme.AccessTokenType, "pop"))
				{
					this._logger.Error("A broker application is required for Proof-of-Possesion, but one could not be found or communicated with. See https://aka.ms/msal-net-pop");
					throw new MsalClientException("broker_application_required", "MSAL cannot invoke the broker and it is required for Proof-of-Possession. WAM (Broker) may not be installed on the user's device or there was an error invoking the broker. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.Check logs for more details and see https://aka.ms/msal-net-pop. ");
				}
				this._logger.Info("Broker attempt did not complete, most likely because the broker is not installed. Attempting to use a browser / web UI. ");
				cancellationToken.ThrowIfCancellationRequested();
			}
			if (this._requestParams.AppConfig.MultiCloudSupportEnabled)
			{
				this._logger.Info("Instance Aware was configured.");
				this._requestParams.AppConfig.ExtraQueryParameters["instance_aware"] = "true";
			}
			Tuple<AuthorizationResult, string> tuple = await (this._authCodeRequestComponentOverride ?? new AuthCodeRequestComponent(this._requestParams, this._interactiveParameters)).FetchAuthCodeAndPkceVerifierAsync(cancellationToken).ConfigureAwait(false);
			this._logger.Info("An authorization code was retrieved from the /authorize endpoint. ");
			AuthorizationResult authResult = tuple.Item1;
			string authCode = authResult.Code;
			string pkceCodeVerifier = tuple.Item2;
			string text;
			MsalTokenResponse msalTokenResponse2;
			if (BrokerInteractiveRequestComponent.IsBrokerRequiredAuthCode(authCode, out text))
			{
				msalTokenResponse2 = await this.RunBrokerWithInstallUriAsync(text, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				if (this._requestParams.AppConfig.MultiCloudSupportEnabled && !string.IsNullOrEmpty(authResult.CloudInstanceHost))
				{
					this._logger.Info("Updating the authority to the cloud specific authority.");
					this._requestParams.AuthorityManager = new AuthorityManager(this._requestParams.RequestContext, Authority.CreateAuthorityWithEnvironment(this._requestParams.Authority.AuthorityInfo, authResult.CloudInstanceHost));
					await base.ResolveAuthorityAsync().ConfigureAwait(false);
				}
				this._logger.Info("Exchanging the auth code for tokens. ");
				MsalTokenResponse msalTokenResponse3 = await (this._authCodeExchangeComponentOverride ?? new AuthCodeExchangeComponent(this._requestParams, this._interactiveParameters, authCode, pkceCodeVerifier, authResult.ClientInfo)).FetchTokensAsync(cancellationToken).ConfigureAwait(false);
				Metrics.IncrementTotalAccessTokensFromIdP();
				msalTokenResponse2 = msalTokenResponse3;
			}
			return msalTokenResponse2;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0004E334 File Offset: 0x0004C534
		private async Task<MsalTokenResponse> RunBrokerWithInstallUriAsync(string brokerInstallUri, CancellationToken cancellationToken)
		{
			this._logger.Info(() => "Based on the auth code, the broker flow is required. Starting broker flow knowing the broker installation app link. ");
			cancellationToken.ThrowIfCancellationRequested();
			MsalTokenResponse msalTokenResponse = await this.FetchTokensFromBrokerAsync(brokerInstallUri, cancellationToken).ConfigureAwait(false);
			MsalTokenResponse tokenResponse = msalTokenResponse;
			this._logger.Info(() => "Broker attempt completed successfully " + (tokenResponse != null).ToString());
			Metrics.IncrementTotalAccessTokensFromBroker();
			return tokenResponse;
		}

		// Token: 0x04000A56 RID: 2646
		private readonly AuthenticationRequestParameters _requestParams;

		// Token: 0x04000A57 RID: 2647
		private readonly AcquireTokenInteractiveParameters _interactiveParameters;

		// Token: 0x04000A58 RID: 2648
		private readonly IServiceBundle _serviceBundle;

		// Token: 0x04000A59 RID: 2649
		private readonly ILoggerAdapter _logger;

		// Token: 0x04000A5A RID: 2650
		private const string InstanceAwareParam = "instance_aware";

		// Token: 0x04000A5B RID: 2651
		private readonly IAuthCodeRequestComponent _authCodeRequestComponentOverride;

		// Token: 0x04000A5C RID: 2652
		private readonly ITokenRequestComponent _authCodeExchangeComponentOverride;

		// Token: 0x04000A5D RID: 2653
		private readonly ITokenRequestComponent _brokerInteractiveComponent;
	}
}
