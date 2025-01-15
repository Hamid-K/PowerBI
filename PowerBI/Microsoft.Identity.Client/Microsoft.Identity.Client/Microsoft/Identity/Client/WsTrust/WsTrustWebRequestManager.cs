using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001BE RID: 446
	internal class WsTrustWebRequestManager : IWsTrustWebRequestManager
	{
		// Token: 0x060013E8 RID: 5096 RVA: 0x00043830 File Offset: 0x00041A30
		public WsTrustWebRequestManager(IHttpManager httpManager)
		{
			this._httpManager = httpManager;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00043840 File Offset: 0x00041A40
		public async Task<MexDocument> GetMexDocumentAsync(string federationMetadataUrl, RequestContext requestContext, string federationMetadata = null)
		{
			MexDocument mexDocument2;
			if (!string.IsNullOrEmpty(federationMetadata))
			{
				MexDocument mexDocument = new MexDocument(federationMetadata);
				requestContext.Logger.Info(() => "MEX document fetched and parsed from provided federation metadata");
				mexDocument2 = mexDocument;
			}
			else
			{
				IDictionary<string, string> msalIdParameters = MsalIdHelper.GetMsalIdParameters(requestContext.Logger);
				UriBuilder uriBuilder = new UriBuilder(federationMetadataUrl);
				HttpResponse httpResponse = await this._httpManager.SendGetAsync(uriBuilder.Uri, msalIdParameters, requestContext.Logger, true, requestContext.UserCancellationToken).ConfigureAwait(false);
				if (httpResponse.StatusCode != HttpStatusCode.OK)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "Response status code does not indicate success: {0} ({1}). See https://aka.ms/msal-net-ropc for more information. ", (int)httpResponse.StatusCode, httpResponse.StatusCode);
					ILoggerAdapter logger = requestContext.Logger;
					string text2 = "=== Token Acquisition ({0}) failed:\n\tAuthority: {1}\n\tClientId: {2}.";
					ApiEvent apiEvent = requestContext.ApiEvent;
					string text3 = string.Format(text2, (apiEvent != null) ? apiEvent.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.CanonicalAuthority, requestContext.ServiceBundle.Config.ClientId);
					string text4 = "=== Token Acquisition ({0}) failed.\n\tHost: {1}.";
					ApiEvent apiEvent2 = requestContext.ApiEvent;
					logger.ErrorPii(text3, string.Format(text4, (apiEvent2 != null) ? apiEvent2.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.Host));
					throw MsalServiceExceptionFactory.FromHttpResponse("accessing_ws_metadata_exchange_failed", text, httpResponse, null);
				}
				MexDocument mexDocument3 = new MexDocument(httpResponse.Body);
				requestContext.Logger.InfoPii(() => "MEX document fetched and parsed from '" + federationMetadataUrl + "'", () => "Fetched and parsed MEX");
				mexDocument2 = mexDocument3;
			}
			return mexDocument2;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004389C File Offset: 0x00041A9C
		public async Task<WsTrustResponse> GetWsTrustResponseAsync(WsTrustEndpoint wsTrustEndpoint, string wsTrustRequest, RequestContext requestContext)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"SOAPAction",
				(wsTrustEndpoint.Version == WsTrustVersion.WsTrust2005) ? XmlNamespace.Issue2005.ToString() : XmlNamespace.Issue.ToString()
			} };
			StringContent stringContent = new StringContent(wsTrustRequest, Encoding.UTF8, "application/soap+xml");
			HttpResponse httpResponse = await this._httpManager.SendPostForceResponseAsync(wsTrustEndpoint.Uri, dictionary, stringContent, requestContext.Logger, requestContext.UserCancellationToken).ConfigureAwait(false);
			if (httpResponse.StatusCode != HttpStatusCode.OK)
			{
				string text = null;
				try
				{
					text = WsTrustResponse.ReadErrorResponse(XDocument.Parse(httpResponse.Body, LoadOptions.None));
				}
				catch (XmlException)
				{
					text = httpResponse.Body;
				}
				requestContext.Logger.ErrorPii("Ws-Trust request failed. See error message for more details." + string.Format("Status code: {0} \nError message: {1}", httpResponse.StatusCode, text), "Ws-Trust request failed. See error message for more details." + string.Format("Status code: {0}", httpResponse.StatusCode));
				throw MsalServiceExceptionFactory.FromHttpResponse("federated_service_returned_error", string.Format(CultureInfo.CurrentCulture, "Federated service at {0} returned error: {1} ", wsTrustEndpoint.Uri, text), httpResponse, null);
			}
			WsTrustResponse wsTrustResponse2;
			try
			{
				WsTrustResponse wsTrustResponse = WsTrustResponse.CreateFromResponse(httpResponse.Body, wsTrustEndpoint.Version);
				if (wsTrustResponse == null)
				{
					requestContext.Logger.ErrorPii("Token not found in the ws trust response. See response for more details: \n" + httpResponse.Body, "Token not found in WS-Trust response.");
					throw new MsalClientException("parsing_wstrust_response_failed", "There was an error parsing the WS-Trust response from the endpoint. \nThis may occur if there are issues with your ADFS configuration. See https://aka.ms/msal-net-iwa-troubleshooting for more details.\nEnable logging to see more details. See https://aka.ms/msal-net-logging.");
				}
				wsTrustResponse2 = wsTrustResponse;
			}
			catch (XmlException ex)
			{
				throw new MsalClientException("parsing_wstrust_response_failed", string.Format(CultureInfo.CurrentCulture, "Federated service at {0} parse error: Body {1} ", wsTrustEndpoint.Uri, httpResponse.Body), ex);
			}
			return wsTrustResponse2;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000438F8 File Offset: 0x00041AF8
		public async Task<UserRealmDiscoveryResponse> GetUserRealmAsync(string userRealmUriPrefix, string userName, RequestContext requestContext)
		{
			requestContext.Logger.Info("Sending request to userrealm endpoint. ");
			IDictionary<string, string> msalIdParameters = MsalIdHelper.GetMsalIdParameters(requestContext.Logger);
			Uri uri = new UriBuilder(userRealmUriPrefix + userName + "?api-version=1.0").Uri;
			HttpResponse httpResponse = await this._httpManager.SendGetAsync(uri, msalIdParameters, requestContext.Logger, true, requestContext.UserCancellationToken).ConfigureAwait(false);
			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				return JsonHelper.DeserializeFromJson<UserRealmDiscoveryResponse>(httpResponse.Body);
			}
			string text = string.Format(CultureInfo.CurrentCulture, "Response status code does not indicate success: {0} ({1}). ", (int)httpResponse.StatusCode, httpResponse.StatusCode);
			ILoggerAdapter logger = requestContext.Logger;
			string text2 = "=== Token Acquisition ({0}) failed:\n\tAuthority: {1}\n\tClientId: {2}.";
			ApiEvent apiEvent = requestContext.ApiEvent;
			string text3 = string.Format(text2, (apiEvent != null) ? apiEvent.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.CanonicalAuthority, requestContext.ServiceBundle.Config.ClientId);
			string text4 = "=== Token Acquisition ({0}) failed.\n\tHost: {1}.";
			ApiEvent apiEvent2 = requestContext.ApiEvent;
			logger.ErrorPii(text3, string.Format(text4, (apiEvent2 != null) ? apiEvent2.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.Host));
			throw MsalServiceExceptionFactory.FromHttpResponse("user_realm_discovery_failed", text, httpResponse, null);
		}

		// Token: 0x04000834 RID: 2100
		private readonly IHttpManager _httpManager;
	}
}
