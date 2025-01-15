using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Broker
{
	// Token: 0x0200025D RID: 605
	internal class BrokerInteractiveRequestComponent : ITokenRequestComponent
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000508AE File Offset: 0x0004EAAE
		internal IBroker Broker { get; }

		// Token: 0x0600182B RID: 6187 RVA: 0x000508B8 File Offset: 0x0004EAB8
		public BrokerInteractiveRequestComponent(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenInteractiveParameters acquireTokenInteractiveParameters, IBroker broker, string optionalBrokerInstallUrl)
		{
			this._authenticationRequestParameters = authenticationRequestParameters;
			this._interactiveParameters = acquireTokenInteractiveParameters;
			this._serviceBundle = authenticationRequestParameters.RequestContext.ServiceBundle;
			this.Broker = broker;
			this._optionalBrokerInstallUrl = optionalBrokerInstallUrl;
			this._logger = this._authenticationRequestParameters.RequestContext.Logger;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00050910 File Offset: 0x0004EB10
		public async Task<MsalTokenResponse> FetchTokensAsync(CancellationToken cancellationToken)
		{
			if (this.Broker.IsBrokerInstalledAndInvokable(this._authenticationRequestParameters.AuthorityInfo.AuthorityType))
			{
				this._logger.Info("Can invoke broker. Will attempt to acquire token with broker. ");
			}
			else
			{
				if (string.IsNullOrEmpty(this._optionalBrokerInstallUrl))
				{
					this._logger.Info("Broker is required but is not installed or not available on the current platform. An app URI has not been provided. MSAL will fallback to use a browser.");
					return null;
				}
				this._logger.Info("Broker is required for authentication and broker is not installed on the device. Adding BrokerInstallUrl to broker payload. ");
				this.Broker.HandleInstallUrl(this._optionalBrokerInstallUrl);
			}
			MsalTokenResponse msalTokenResponse = await this.Broker.AcquireTokenInteractiveAsync(this._authenticationRequestParameters, this._interactiveParameters).ConfigureAwait(false);
			this.ValidateResponseFromBroker(msalTokenResponse);
			return msalTokenResponse;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00050954 File Offset: 0x0004EB54
		internal void ValidateResponseFromBroker(MsalTokenResponse msalTokenResponse)
		{
			this._logger.Info("Checking MsalTokenResponse returned from broker. ");
			if (!string.IsNullOrEmpty(msalTokenResponse.AccessToken))
			{
				this._logger.Info("Success. Broker response contains an access token. ");
				return;
			}
			if (msalTokenResponse.Error != null)
			{
				this._logger.Error(LogMessages.ErrorReturnedInBrokerResponse(msalTokenResponse.Error));
				throw MsalServiceExceptionFactory.FromBrokerResponse(msalTokenResponse, "Broker response returned error: " + msalTokenResponse.ErrorDescription);
			}
			this._logger.Error("Unknown error returned in broker response. ");
			throw new MsalServiceException("broker_response_returned_error", "Broker response returned an error which does not contain an error or error description. See https://aka.ms/msal-brokers for details. ", null);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000509E4 File Offset: 0x0004EBE4
		public static bool IsBrokerRequiredAuthCode(string authCode, out string installationUri)
		{
			if (authCode.StartsWith("msauth://", StringComparison.OrdinalIgnoreCase))
			{
				installationUri = BrokerInteractiveRequestComponent.ExtractAppLink(authCode);
				return installationUri != null;
			}
			installationUri = null;
			return false;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00050A08 File Offset: 0x0004EC08
		private static string ExtractAppLink(string authCode)
		{
			string text = new Uri(authCode).Query;
			if (text.StartsWith("?", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(1);
			}
			Dictionary<string, string> dictionary = CoreHelpers.ParseKeyValueList(text, '&', true, true, null);
			if (!dictionary.ContainsKey("app_link"))
			{
				return null;
			}
			return dictionary["app_link"];
		}

		// Token: 0x04000AA4 RID: 2724
		private readonly AcquireTokenInteractiveParameters _interactiveParameters;

		// Token: 0x04000AA5 RID: 2725
		private readonly string _optionalBrokerInstallUrl;

		// Token: 0x04000AA6 RID: 2726
		private readonly AuthenticationRequestParameters _authenticationRequestParameters;

		// Token: 0x04000AA7 RID: 2727
		private readonly IServiceBundle _serviceBundle;

		// Token: 0x04000AA8 RID: 2728
		private readonly ILoggerAdapter _logger;
	}
}
