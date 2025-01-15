using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.Internal.Requests.Silent;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x0200024B RID: 587
	internal class BrokerSilentStrategy : ISilentAuthRequestStrategy
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0004F44C File Offset: 0x0004D64C
		internal IBroker Broker { get; }

		// Token: 0x060017C2 RID: 6082 RVA: 0x0004F454 File Offset: 0x0004D654
		public BrokerSilentStrategy(SilentRequest request, IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters silentParameters, IBroker broker)
		{
			this._authenticationRequestParameters = authenticationRequestParameters;
			this._silentParameters = silentParameters;
			this._serviceBundle = serviceBundle;
			this._silentRequest = request;
			if (broker == null)
			{
				throw new ArgumentNullException("broker");
			}
			this.Broker = broker;
			this._logger = authenticationRequestParameters.RequestContext.Logger;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0004F4AC File Offset: 0x0004D6AC
		public async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult;
			if (!this.Broker.IsBrokerInstalledAndInvokable(this._authenticationRequestParameters.AuthorityInfo.AuthorityType))
			{
				if (this._authenticationRequestParameters.PopAuthenticationConfiguration != null)
				{
					throw new MsalClientException("broker_application_required", "MSAL cannot invoke the broker and it is required for Proof-of-Possession. WAM (Broker) may not be installed on the user's device or there was an error invoking the broker. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.Check logs for more details and see https://aka.ms/msal-net-pop. ");
				}
				this._logger.Warning("Broker is not installed or authority type is incorrect. Cannot respond to silent request.");
				authenticationResult = null;
			}
			else
			{
				MsalTokenResponse msalTokenResponse = await this.SendTokenRequestToBrokerAsync().ConfigureAwait(false);
				if (msalTokenResponse != null)
				{
					this.ValidateResponseFromBroker(msalTokenResponse);
					Metrics.IncrementTotalAccessTokensFromBroker();
					authenticationResult = await this._silentRequest.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
				}
				else
				{
					authenticationResult = null;
				}
			}
			return authenticationResult;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0004F4F0 File Offset: 0x0004D6F0
		private async Task<MsalTokenResponse> SendTokenRequestToBrokerAsync()
		{
			this._authenticationRequestParameters.RequestContext.Logger.Info("Can invoke broker. Will attempt to acquire token with broker. ");
			MsalTokenResponse msalTokenResponse;
			if (!PublicClientApplication.IsOperatingSystemAccount(this._authenticationRequestParameters.Account))
			{
				msalTokenResponse = await this.Broker.AcquireTokenSilentAsync(this._authenticationRequestParameters, this._silentParameters).ConfigureAwait(false);
			}
			else
			{
				msalTokenResponse = await this.Broker.AcquireTokenSilentDefaultUserAsync(this._authenticationRequestParameters, this._silentParameters).ConfigureAwait(false);
			}
			return msalTokenResponse;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0004F534 File Offset: 0x0004D734
		internal void ValidateResponseFromBroker(MsalTokenResponse msalTokenResponse)
		{
			this._logger.Info("Checking MsalTokenResponse returned from broker. ");
			if (msalTokenResponse.AccessToken != null)
			{
				this._logger.Info("Success. Response contains an access token. ");
				return;
			}
			if (msalTokenResponse.Error == null)
			{
				this._logger.Info("Unknown error returned in broker response. ");
				throw new MsalServiceException("broker_response_returned_error", "Broker response returned an error which does not contain an error or error description. See https://aka.ms/msal-brokers for details. ", null);
			}
			this._logger.Info(() => LogMessages.ErrorReturnedInBrokerResponse(msalTokenResponse.Error));
			if (msalTokenResponse.Error == "no_tokens_found" || msalTokenResponse.Error == "no_account_found" || msalTokenResponse.Error == "Broker refresh token is invalid")
			{
				throw new MsalUiRequiredException(msalTokenResponse.Error, msalTokenResponse.ErrorDescription);
			}
			throw MsalServiceExceptionFactory.FromBrokerResponse(msalTokenResponse, "Broker response returned error: " + msalTokenResponse.ErrorDescription);
		}

		// Token: 0x04000A64 RID: 2660
		internal AuthenticationRequestParameters _authenticationRequestParameters;

		// Token: 0x04000A65 RID: 2661
		protected IServiceBundle _serviceBundle;

		// Token: 0x04000A66 RID: 2662
		private readonly AcquireTokenSilentParameters _silentParameters;

		// Token: 0x04000A67 RID: 2663
		private readonly SilentRequest _silentRequest;

		// Token: 0x04000A69 RID: 2665
		private readonly ILoggerAdapter _logger;
	}
}
