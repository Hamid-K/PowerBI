using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.Kerberos;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000212 RID: 530
	internal class TokenClient
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00048EB0 File Offset: 0x000470B0
		public TokenClient(AuthenticationRequestParameters requestParams)
		{
			if (requestParams == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._requestParams = requestParams;
			this._serviceBundle = this._requestParams.RequestContext.ServiceBundle;
			this._oAuth2Client = new OAuth2Client(this._serviceBundle.ApplicationLogger, this._serviceBundle.HttpManager);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00048F10 File Offset: 0x00047110
		public async Task<MsalTokenResponse> SendTokenRequestAsync(IDictionary<string, string> additionalBodyParameters, string scopeOverride = null, string tokenEndpointOverride = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			MsalTokenResponse msalTokenResponse2;
			using (this._requestParams.RequestContext.Logger.LogMethodDuration(LogLevel.Verbose, "SendTokenRequestAsync", "/_/src/client/Microsoft.Identity.Client/OAuth2/TokenClient.cs"))
			{
				cancellationToken.ThrowIfCancellationRequested();
				string tokenEndpoint = tokenEndpointOverride;
				if (tokenEndpoint == null)
				{
					string text = await this._requestParams.Authority.GetTokenEndpointAsync(this._requestParams.RequestContext).ConfigureAwait(false);
					tokenEndpoint = text;
				}
				this._requestParams.RequestContext.ApiEvent.TokenEndpoint = tokenEndpoint;
				string text2 = ((!string.IsNullOrEmpty(scopeOverride)) ? scopeOverride : TokenClient.GetDefaultScopes(this._requestParams.Scope));
				await this.AddBodyParamsAndHeadersAsync(additionalBodyParameters, text2, cancellationToken).ConfigureAwait(false);
				this.AddThrottlingHeader();
				this._serviceBundle.ThrottlingManager.TryThrottle(this._requestParams, this._oAuth2Client.GetBodyParameters());
				MsalTokenResponse msalTokenResponse;
				try
				{
					msalTokenResponse = await this.SendHttpAndClearTelemetryAsync(tokenEndpoint, this._requestParams.RequestContext.Logger).ConfigureAwait(false);
				}
				catch (MsalServiceException ex)
				{
					this._serviceBundle.ThrottlingManager.RecordException(this._requestParams, this._oAuth2Client.GetBodyParameters(), ex);
					throw;
				}
				if (string.IsNullOrEmpty(msalTokenResponse.Scope))
				{
					msalTokenResponse.Scope = this._requestParams.Scope.AsSingleString();
					this._requestParams.RequestContext.Logger.Info("ScopeSet was missing from the token response, so using developer provided scopes in the result. ");
				}
				if (string.IsNullOrEmpty(msalTokenResponse.TokenType))
				{
					throw new MsalClientException("token_type_missing", "The response from the token endpoint does not contain the token_type parameter. This happens if the identity provider (AAD, B2C, ADFS, etc.) did not include the access token type in the token response. Verify the configuration of the identity provider. ");
				}
				if (!string.Equals(msalTokenResponse.TokenType, this._requestParams.AuthenticationScheme.AccessTokenType, StringComparison.OrdinalIgnoreCase))
				{
					throw new MsalClientException("token_type_mismatch", MsalErrorMessage.TokenTypeMismatch(this._requestParams.AuthenticationScheme.AccessTokenType, msalTokenResponse.TokenType));
				}
				msalTokenResponse2 = msalTokenResponse;
			}
			return msalTokenResponse2;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00048F74 File Offset: 0x00047174
		private void AddThrottlingHeader()
		{
			this._oAuth2Client.AddHeader("x-ms-lib-capability", "retry-after, h429");
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00048F8C File Offset: 0x0004718C
		private async Task AddBodyParamsAndHeadersAsync(IDictionary<string, string> additionalBodyParameters, string scopes, CancellationToken cancellationToken)
		{
			this._oAuth2Client.AddBodyParameter("client_id", this._requestParams.AppConfig.ClientId);
			if (this._serviceBundle.Config.ClientCredential != null)
			{
				this._requestParams.RequestContext.Logger.Verbose(() => "[TokenClient] Before adding the client assertion / secret");
				string text = await this._requestParams.Authority.GetTokenEndpointAsync(this._requestParams.RequestContext).ConfigureAwait(false);
				bool isSha2CredentialSupported = this._requestParams.AuthorityManager.Authority.AuthorityInfo.IsSha2CredentialSupported;
				await this._serviceBundle.Config.ClientCredential.AddConfidentialClientParametersAsync(this._oAuth2Client, this._requestParams.RequestContext.Logger, this._serviceBundle.PlatformProxy.CryptographyManager, this._requestParams.AppConfig.ClientId, text, this._requestParams.SendX5C, isSha2CredentialSupported, cancellationToken).ConfigureAwait(false);
				this._requestParams.RequestContext.Logger.Verbose(() => "[TokenClient] After adding the client assertion / secret");
			}
			this._oAuth2Client.AddBodyParameter("scope", scopes);
			this.AddClaims();
			foreach (KeyValuePair<string, string> keyValuePair in additionalBodyParameters)
			{
				this._oAuth2Client.AddBodyParameter(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in this._requestParams.AuthenticationScheme.GetTokenRequestParams())
			{
				this._oAuth2Client.AddBodyParameter(keyValuePair2.Key, keyValuePair2.Value);
			}
			this._oAuth2Client.AddHeader("x-client-current-telemetry", this._serviceBundle.HttpTelemetryManager.GetCurrentRequestHeader(this._requestParams.RequestContext.ApiEvent));
			if (!this._requestInProgress)
			{
				this._requestInProgress = true;
				this._oAuth2Client.AddHeader("x-client-last-telemetry", this._serviceBundle.HttpTelemetryManager.GetLastRequestHeader());
			}
			if (DeviceAuthHelper.CanOSPerformPKeyAuth())
			{
				this._oAuth2Client.AddHeader("x-ms-PKeyAuth", "1.0");
			}
			this.AddExtraHttpHeaders();
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00048FE8 File Offset: 0x000471E8
		private void AddClaims()
		{
			string kerberosTicketClaim = KerberosSupplementalTicketManager.GetKerberosTicketClaim(this._requestParams.RequestContext.ServiceBundle.Config.KerberosServicePrincipalName, this._requestParams.RequestContext.ServiceBundle.Config.TicketContainer);
			string resolvedClaims;
			if (string.IsNullOrEmpty(kerberosTicketClaim))
			{
				resolvedClaims = this._requestParams.ClaimsAndClientCapabilities;
			}
			else if (!string.IsNullOrEmpty(this._requestParams.ClaimsAndClientCapabilities))
			{
				JObject jobject = JsonHelper.ParseIntoJsonObject(this._requestParams.ClaimsAndClientCapabilities);
				JObject jobject2 = ClaimsHelper.MergeClaimsIntoCapabilityJson(kerberosTicketClaim, jobject);
				resolvedClaims = JsonHelper.JsonObjectToString(jobject2);
				this._requestParams.RequestContext.Logger.Verbose(() => "Adding kerberos claim + Claims/ClientCapabilities to request: " + resolvedClaims);
			}
			else
			{
				resolvedClaims = kerberosTicketClaim;
				this._requestParams.RequestContext.Logger.Verbose(() => "Adding kerberos claim to request: " + resolvedClaims);
			}
			this._oAuth2Client.AddBodyParameter("claims", resolvedClaims);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000490EC File Offset: 0x000472EC
		private void AddExtraHttpHeaders()
		{
			if (this._requestParams.ExtraHttpHeaders != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this._requestParams.ExtraHttpHeaders)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key) && !string.IsNullOrEmpty(keyValuePair.Value))
					{
						this._oAuth2Client.AddHeader(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004917C File Offset: 0x0004737C
		public void AddHeaderToClient(string name, string value)
		{
			this._oAuth2Client.AddHeader(name, value);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0004918C File Offset: 0x0004738C
		private async Task<MsalTokenResponse> SendHttpAndClearTelemetryAsync(string tokenEndpoint, ILoggerAdapter logger)
		{
			UriBuilder uriBuilder = new UriBuilder(tokenEndpoint);
			uriBuilder.AppendQueryParameters(this._requestParams.ExtraQueryParameters);
			Uri tokenEndpointWithQueryParams = uriBuilder.Uri;
			try
			{
				int num = 0;
				try
				{
					logger.Verbose(() => "[Token Client] Fetching MsalTokenResponse .... ");
					MsalTokenResponse msalTokenResponse = await this._oAuth2Client.GetTokenAsync(tokenEndpointWithQueryParams, this._requestParams.RequestContext, true, this._requestParams.OnBeforeTokenRequestHandler).ConfigureAwait(false);
					this._serviceBundle.HttpTelemetryManager.ResetPreviousUnsentData();
					return msalTokenResponse;
				}
				catch (MsalServiceException obj)
				{
					num = 1;
				}
				if (num == 1)
				{
					object obj;
					MsalServiceException ex = (MsalServiceException)obj;
					if (!ex.IsRetryable)
					{
						this._serviceBundle.HttpTelemetryManager.ResetPreviousUnsentData();
					}
					string text;
					if (ex.StatusCode == 401 && this._serviceBundle.DeviceAuthManager.TryCreateDeviceAuthChallengeResponse(ex.Headers, new Uri(tokenEndpoint), out text))
					{
						this._oAuth2Client.AddHeader("Authorization", text);
						return await this._oAuth2Client.GetTokenAsync(tokenEndpointWithQueryParams, this._requestParams.RequestContext, false, this._requestParams.OnBeforeTokenRequestHandler).ConfigureAwait(false);
					}
					Exception ex2 = obj as Exception;
					if (ex2 == null)
					{
						throw obj;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
			}
			finally
			{
				this._requestInProgress = false;
			}
			tokenEndpointWithQueryParams = null;
			MsalTokenResponse msalTokenResponse2;
			return msalTokenResponse2;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000491DF File Offset: 0x000473DF
		private static string GetDefaultScopes(ISet<string> inputScope)
		{
			SortedSet<string> sortedSet = new SortedSet<string>(inputScope, StringComparer.OrdinalIgnoreCase);
			sortedSet.UnionWith(OAuth2Value.ReservedScopes);
			return sortedSet.AsSingleString();
		}

		// Token: 0x04000962 RID: 2402
		private readonly AuthenticationRequestParameters _requestParams;

		// Token: 0x04000963 RID: 2403
		private readonly IServiceBundle _serviceBundle;

		// Token: 0x04000964 RID: 2404
		private readonly OAuth2Client _oAuth2Client;

		// Token: 0x04000965 RID: 2405
		private volatile bool _requestInProgress;
	}
}
