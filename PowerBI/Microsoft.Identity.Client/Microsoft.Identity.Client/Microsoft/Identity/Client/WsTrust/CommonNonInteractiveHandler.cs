using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001B5 RID: 437
	internal class CommonNonInteractiveHandler
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x00042739 File Offset: 0x00040939
		public CommonNonInteractiveHandler(RequestContext requestContext, IServiceBundle serviceBundle)
		{
			this._requestContext = requestContext;
			this._serviceBundle = serviceBundle;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00042750 File Offset: 0x00040950
		public async Task<string> GetPlatformUserAsync()
		{
			string text = await this._serviceBundle.PlatformProxy.GetUserPrincipalNameAsync().ConfigureAwait(false);
			string platformUsername = text;
			if (string.IsNullOrWhiteSpace(platformUsername))
			{
				this._requestContext.Logger.Error("Could not find UPN for logged in user. ");
				throw new MsalClientException("unknown_user", "Could not identify the user logged into the OS. See http://aka.ms/msal-net-iwa for details. ");
			}
			this._requestContext.Logger.InfoPii(() => "Logged in user detected with user name '" + platformUsername + "'", () => "Logged in user detected. ");
			return platformUsername;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00042794 File Offset: 0x00040994
		public async Task<UserRealmDiscoveryResponse> QueryUserRealmDataAsync(string userRealmUriPrefix, string username)
		{
			UserRealmDiscoveryResponse userRealmDiscoveryResponse = await this._serviceBundle.WsTrustWebRequestManager.GetUserRealmAsync(userRealmUriPrefix, username, this._requestContext).ConfigureAwait(false);
			UserRealmDiscoveryResponse userRealmResponse = userRealmDiscoveryResponse;
			if (string.Equals(userRealmResponse.DomainName, "live.com"))
			{
				throw new MsalClientException("ropc_not_supported_for_msa", "ROPC does not support MSA accounts. See https://aka.ms/msal-net-ropc for details. ");
			}
			this._requestContext.Logger.InfoPii(() => string.Concat(new string[] { "User with user name '", username, "' detected as '", userRealmResponse.AccountType, "'. " }), () => "User detected as '" + userRealmResponse.AccountType + "'. ");
			return userRealmResponse;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000427E8 File Offset: 0x000409E8
		public async Task<WsTrustResponse> PerformWsTrustMexExchangeAsync(string federationMetadataUrl, string cloudAudienceUrn, UserAuthType userAuthType, string username, string password, string federationMetadataFilename)
		{
			MexDocument mexDocument;
			try
			{
				mexDocument = await this._serviceBundle.WsTrustWebRequestManager.GetMexDocumentAsync(federationMetadataUrl, this._requestContext, federationMetadataFilename).ConfigureAwait(false);
			}
			catch (XmlException ex)
			{
				throw new MsalClientException("parsing_ws_metadata_exchange_failed", "Parsing WS metadata exchange failed. ", ex);
			}
			WsTrustEndpoint wsTrustEndpoint = ((userAuthType == UserAuthType.IntegratedAuth) ? mexDocument.GetWsTrustWindowsTransportEndpoint() : mexDocument.GetWsTrustUsernamePasswordEndpoint());
			if (wsTrustEndpoint == null)
			{
				throw new MsalClientException("wstrust_endpoint_not_found", "WS-Trust endpoint not found in metadata document. ");
			}
			this._requestContext.Logger.VerbosePii(() => string.Format(CultureInfo.InvariantCulture, "WS-Trust endpoint '{0}' being used from MEX at '{1}'", wsTrustEndpoint.Uri, federationMetadataUrl), () => "Fetched and parsed MEX. ");
			WsTrustResponse wsTrustResponse = await this.GetWsTrustResponseAsync(userAuthType, cloudAudienceUrn, wsTrustEndpoint, username, password).ConfigureAwait(false);
			this._requestContext.Logger.Info(() => "Token of type '" + wsTrustResponse.TokenType + "' acquired from WS-Trust endpoint. ");
			return wsTrustResponse;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00042860 File Offset: 0x00040A60
		internal async Task<WsTrustResponse> GetWsTrustResponseAsync(UserAuthType userAuthType, string cloudAudienceUrn, WsTrustEndpoint endpoint, string username, string password)
		{
			string text = ((userAuthType == UserAuthType.IntegratedAuth) ? endpoint.BuildTokenRequestMessageWindowsIntegratedAuth(cloudAudienceUrn) : endpoint.BuildTokenRequestMessageUsernamePassword(cloudAudienceUrn, username, password));
			WsTrustResponse wsTrustResponse2;
			try
			{
				WsTrustResponse wsTrustResponse3 = await this._serviceBundle.WsTrustWebRequestManager.GetWsTrustResponseAsync(endpoint, text, this._requestContext).ConfigureAwait(false);
				WsTrustResponse wsTrustResponse = wsTrustResponse3;
				this._requestContext.Logger.Info(() => "Token of type '" + wsTrustResponse.TokenType + "' acquired from WS-Trust endpoint. ");
				wsTrustResponse2 = wsTrustResponse;
			}
			catch (Exception ex) when (!(ex is MsalClientException))
			{
				throw new MsalClientException("parsing_wstrust_response_failed", "There was an error parsing the WS-Trust response from the endpoint. \nThis may occur if there are issues with your ADFS configuration. See https://aka.ms/msal-net-iwa-troubleshooting for more details.\nEnable logging to see more details. See https://aka.ms/msal-net-logging. Error Message: " + ex.Message, ex);
			}
			return wsTrustResponse2;
		}

		// Token: 0x04000819 RID: 2073
		private readonly RequestContext _requestContext;

		// Token: 0x0400081A RID: 2074
		private readonly IServiceBundle _serviceBundle;
	}
}
