using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.UI;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200023B RID: 571
	internal class AuthCodeRequestComponent : IAuthCodeRequestComponent
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x0004C6A0 File Offset: 0x0004A8A0
		public AuthCodeRequestComponent(AuthenticationRequestParameters requestParams, AcquireTokenInteractiveParameters interactiveParameters)
		{
			if (requestParams == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._requestParams = requestParams;
			if (interactiveParameters == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._interactiveParameters = interactiveParameters;
			this._serviceBundle = this._requestParams.RequestContext.ServiceBundle;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0004C6F8 File Offset: 0x0004A8F8
		public async Task<Tuple<AuthorizationResult, string>> FetchAuthCodeAndPkceVerifierAsync(CancellationToken cancellationToken)
		{
			IWebUI webUI = this.CreateWebAuthenticationDialog();
			return await this.FetchAuthCodeAndPkceInternalAsync(webUI, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0004C744 File Offset: 0x0004A944
		public async Task<Uri> GetAuthorizationUriWithoutPkceAsync(CancellationToken cancellationToken)
		{
			string text = await this._requestParams.Authority.GetAuthorizationEndpointAsync(this._requestParams.RequestContext).ConfigureAwait(false);
			return this.CreateAuthorizationUri(text, false).Item1;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0004C788 File Offset: 0x0004A988
		public async Task<Uri> GetAuthorizationUriWithPkceAsync(string codeVerifier, CancellationToken cancellationToken)
		{
			string text = await this._requestParams.Authority.GetAuthorizationEndpointAsync(this._requestParams.RequestContext).ConfigureAwait(false);
			return this.CreateAuthorizationUriWithCodeChallenge(text, codeVerifier).Item1;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		private async Task<Tuple<AuthorizationResult, string>> FetchAuthCodeAndPkceInternalAsync(IWebUI webUi, CancellationToken cancellationToken)
		{
			RedirectUriHelper.Validate(this._requestParams.RedirectUri, false);
			this._requestParams.RedirectUri = webUi.UpdateRedirectUri(this._requestParams.RedirectUri);
			string text = await this._requestParams.Authority.GetAuthorizationEndpointAsync(this._requestParams.RequestContext).ConfigureAwait(false);
			Tuple<Uri, string, string> tuple = this.CreateAuthorizationUri(text, true);
			Uri item = tuple.Item1;
			string state = tuple.Item2;
			string codeVerifier = tuple.Item3;
			AuthorizationResult authorizationResult = await webUi.AcquireAuthorizationAsync(item, this._requestParams.RedirectUri, this._requestParams.RequestContext, cancellationToken).ConfigureAwait(false);
			this.VerifyAuthorizationResult(authorizationResult, state);
			return new Tuple<AuthorizationResult, string>(authorizationResult, codeVerifier);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0004C828 File Offset: 0x0004AA28
		private Tuple<Uri, string> CreateAuthorizationUriWithCodeChallenge(string authEndpoint, string codeVerifier)
		{
			IDictionary<string, string> dictionary = this.CreateAuthorizationRequestParameters(null);
			string text = this._serviceBundle.PlatformProxy.CryptographyManager.CreateBase64UrlEncodedSha256Hash(codeVerifier);
			dictionary["code_challenge"] = text;
			dictionary["code_challenge_method"] = "S256";
			return new Tuple<Uri, string>(this.CreateInteractiveRequestParameters(authEndpoint, dictionary).Uri, codeVerifier);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0004C884 File Offset: 0x0004AA84
		private Tuple<Uri, string, string> CreateAuthorizationUri(string authEndpoint, bool addPkceAndState = false)
		{
			IDictionary<string, string> dictionary = this.CreateAuthorizationRequestParameters(null);
			string text = null;
			string text2 = null;
			if (addPkceAndState)
			{
				text = this._serviceBundle.PlatformProxy.CryptographyManager.GenerateCodeVerifier();
				string text3 = this._serviceBundle.PlatformProxy.CryptographyManager.CreateBase64UrlEncodedSha256Hash(text);
				dictionary["code_challenge"] = text3;
				dictionary["code_challenge_method"] = "S256";
				text2 = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
				dictionary["state"] = text2;
			}
			dictionary["client_info"] = "1";
			return new Tuple<Uri, string, string>(this.CreateInteractiveRequestParameters(authEndpoint, dictionary).Uri, text2, text);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004C94C File Offset: 0x0004AB4C
		private UriBuilder CreateInteractiveRequestParameters(string authEndpoint, IDictionary<string, string> requestParameters)
		{
			if (this._interactiveParameters.Account != null)
			{
				if (!string.IsNullOrEmpty(this._interactiveParameters.Account.Username))
				{
					requestParameters["login_hint"] = this._interactiveParameters.Account.Username;
				}
				AccountId homeAccountId = this._interactiveParameters.Account.HomeAccountId;
				if (((homeAccountId != null) ? homeAccountId.ObjectId : null) != null)
				{
					requestParameters["login_req"] = this._interactiveParameters.Account.HomeAccountId.ObjectId;
				}
				AccountId homeAccountId2 = this._interactiveParameters.Account.HomeAccountId;
				if (!string.IsNullOrEmpty((homeAccountId2 != null) ? homeAccountId2.TenantId : null))
				{
					requestParameters["domain_req"] = this._interactiveParameters.Account.HomeAccountId.TenantId;
				}
			}
			AuthCodeRequestComponent.CheckForDuplicateQueryParameters(this._requestParams.ExtraQueryParameters, requestParameters);
			string text = requestParameters.ToQueryParameter();
			UriBuilder uriBuilder = new UriBuilder(authEndpoint);
			uriBuilder.AppendQueryParameters(text);
			return uriBuilder;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004CA44 File Offset: 0x0004AC44
		private Dictionary<string, string> CreateAuthorizationRequestParameters(Uri redirectUriOverride = null)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (!this._interactiveParameters.ExtraScopesToConsent.IsNullOrEmpty<string>())
			{
				hashSet = ScopeHelper.CreateScopeSet(this._interactiveParameters.ExtraScopesToConsent);
			}
			if (hashSet.Contains(this._requestParams.AppConfig.ClientId))
			{
				throw new ArgumentException("API does not accept client id as a user-provided scope");
			}
			HashSet<string> msalScopes = ScopeHelper.GetMsalScopes(new HashSet<string>(this._requestParams.Scope.Concat(hashSet)));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["scope"] = msalScopes.AsSingleString();
			dictionary["response_type"] = "code";
			dictionary["client_id"] = this._requestParams.AppConfig.ClientId;
			dictionary["redirect_uri"] = ((redirectUriOverride != null) ? redirectUriOverride.OriginalString : null) ?? this._requestParams.RedirectUri.OriginalString;
			Dictionary<string, string> dictionary2 = dictionary;
			if (!string.IsNullOrWhiteSpace(this._requestParams.ClaimsAndClientCapabilities))
			{
				dictionary2["claims"] = this._requestParams.ClaimsAndClientCapabilities;
			}
			if (!string.IsNullOrWhiteSpace(this._interactiveParameters.LoginHint) || this._requestParams.CcsRoutingHint != null)
			{
				string text;
				if (this._requestParams.CcsRoutingHint == null)
				{
					dictionary2["login_hint"] = this._interactiveParameters.LoginHint;
					text = CoreHelpers.GetCcsUpnHint(this._interactiveParameters.LoginHint);
				}
				else
				{
					dictionary2["login_hint"] = this._interactiveParameters.LoginHint;
					text = CoreHelpers.GetCcsClientInfoHint(this._requestParams.CcsRoutingHint.Value.Key, this._requestParams.CcsRoutingHint.Value.Value);
				}
				dictionary2["x-anchormailbox"] = text;
			}
			if (this._requestParams.RequestContext.CorrelationId != Guid.Empty)
			{
				dictionary2["client-request-id"] = this._requestParams.RequestContext.CorrelationId.ToString();
			}
			foreach (KeyValuePair<string, string> keyValuePair in MsalIdHelper.GetMsalIdParameters(this._requestParams.RequestContext.Logger))
			{
				dictionary2[keyValuePair.Key] = keyValuePair.Value;
			}
			if (this._interactiveParameters.Prompt == Prompt.NotSpecified)
			{
				dictionary2["prompt"] = Prompt.SelectAccount.PromptValue;
			}
			else if (this._interactiveParameters.Prompt.PromptValue != Prompt.NoPrompt.PromptValue)
			{
				dictionary2["prompt"] = this._interactiveParameters.Prompt.PromptValue;
			}
			return dictionary2;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0004CD3C File Offset: 0x0004AF3C
		private static void CheckForDuplicateQueryParameters(IDictionary<string, string> queryParamsDictionary, IDictionary<string, string> requestParameters)
		{
			foreach (KeyValuePair<string, string> keyValuePair in queryParamsDictionary)
			{
				if (requestParameters.ContainsKey(keyValuePair.Key))
				{
					throw new MsalClientException("duplicate_query_parameter", string.Format(CultureInfo.InvariantCulture, "Duplicate query parameter '{0}' in extraQueryParameters. ", keyValuePair.Key));
				}
				requestParameters[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004CDC4 File Offset: 0x0004AFC4
		private void VerifyAuthorizationResult(AuthorizationResult authorizationResult, string originalState)
		{
			if (authorizationResult.Status == AuthorizationStatus.Success && originalState != null && !originalState.Equals(authorizationResult.State, StringComparison.OrdinalIgnoreCase))
			{
				throw new MsalClientException("state_mismatch", string.Format(CultureInfo.InvariantCulture, "Returned state({0}) from authorize endpoint is not the same as the one sent({1}). See https://aka.ms/msal-statemismatcherror for more details. ", authorizationResult.State, originalState));
			}
			if (authorizationResult.Error == "login_required")
			{
				throw new MsalUiRequiredException("no_prompt_failed", "One of two conditions was encountered: 1. The Prompt.Never flag was passed, but the constraint could not be honored, because user interaction was required. 2. An error occurred during a silent web authentication that prevented the HTTP authentication flow from completing in a short enough time frame. ", null, UiRequiredExceptionClassification.PromptNeverFailed);
			}
			if (authorizationResult.Status == AuthorizationStatus.UserCancel)
			{
				this._requestParams.RequestContext.Logger.Info("Authorization result status returned user cancelled authentication. ");
				throw new MsalClientException(authorizationResult.Error, authorizationResult.ErrorDescription ?? "User canceled authentication.");
			}
			if (authorizationResult.Status != AuthorizationStatus.Success)
			{
				this._requestParams.RequestContext.Logger.ErrorPii("Authorization result was not successful. See error message for more details. " + authorizationResult.ErrorDescription, "Authorization result was not successful. See error message for more details. ");
				throw new MsalServiceException(authorizationResult.Error, (!string.IsNullOrEmpty(authorizationResult.ErrorDescription)) ? authorizationResult.ErrorDescription : "Unknown error");
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
		private IWebUI CreateWebAuthenticationDialog()
		{
			if (this._interactiveParameters.CustomWebUi != null)
			{
				return new CustomWebUiHandler(this._interactiveParameters.CustomWebUi);
			}
			CoreUIParent uiParent = this._interactiveParameters.UiParent;
			uiParent.UseHiddenBrowser = this._interactiveParameters.Prompt.Equals(Prompt.Never);
			return this._serviceBundle.PlatformProxy.GetWebUiFactory(this._requestParams.AppConfig).CreateAuthenticationDialog(uiParent, this._interactiveParameters.UseEmbeddedWebView, this._requestParams.RequestContext);
		}

		// Token: 0x04000A1B RID: 2587
		private readonly AuthenticationRequestParameters _requestParams;

		// Token: 0x04000A1C RID: 2588
		private readonly AcquireTokenInteractiveParameters _interactiveParameters;

		// Token: 0x04000A1D RID: 2589
		private readonly IServiceBundle _serviceBundle;
	}
}
