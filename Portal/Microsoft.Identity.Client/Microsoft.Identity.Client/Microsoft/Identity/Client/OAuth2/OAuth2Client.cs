using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Instance.Oidc;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000206 RID: 518
	internal class OAuth2Client
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x000488B8 File Offset: 0x00046AB8
		public OAuth2Client(ILoggerAdapter logger, IHttpManager httpManager)
		{
			this._headers = new Dictionary<string, string>(MsalIdHelper.GetMsalIdParameters(logger));
			if (httpManager == null)
			{
				throw new ArgumentNullException("httpManager");
			}
			this._httpManager = httpManager;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00048908 File Offset: 0x00046B08
		public void AddQueryParameter(string key, string value)
		{
			if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
			{
				this._queryParameters[key] = value;
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00048927 File Offset: 0x00046B27
		public void AddBodyParameter(string key, string value)
		{
			if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
			{
				this._bodyParameters[key] = value;
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00048946 File Offset: 0x00046B46
		internal void AddHeader(string key, string value)
		{
			this._headers[key] = value;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00048955 File Offset: 0x00046B55
		internal IReadOnlyDictionary<string, string> GetBodyParameters()
		{
			return new ReadOnlyDictionary<string, string>(this._bodyParameters);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00048962 File Offset: 0x00046B62
		public Task<InstanceDiscoveryResponse> DiscoverAadInstanceAsync(Uri endpoint, RequestContext requestContext)
		{
			return this.ExecuteRequestAsync<InstanceDiscoveryResponse>(endpoint, HttpMethod.Get, requestContext, false, true, null);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00048974 File Offset: 0x00046B74
		public Task<OidcMetadata> DiscoverOidcMetadataAsync(Uri endpoint, RequestContext requestContext)
		{
			return this.ExecuteRequestAsync<OidcMetadata>(endpoint, HttpMethod.Get, requestContext, false, true, null);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00048986 File Offset: 0x00046B86
		internal Task<MsalTokenResponse> GetTokenAsync(Uri endPoint, RequestContext requestContext, bool addCommonHeaders, Func<OnBeforeTokenRequestData, Task> onBeforePostRequestHandler)
		{
			return this.ExecuteRequestAsync<MsalTokenResponse>(endPoint, HttpMethod.Post, requestContext, false, addCommonHeaders, onBeforePostRequestHandler);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004899C File Offset: 0x00046B9C
		internal async Task<T> ExecuteRequestAsync<T>(Uri endPoint, HttpMethod method, RequestContext requestContext, bool expectErrorsOn200OK = false, bool addCommonHeaders = true, Func<OnBeforeTokenRequestData, Task> onBeforePostRequestData = null)
		{
			if (addCommonHeaders)
			{
				this.AddCommonHeaders(requestContext);
			}
			Uri endpointUri = this.AddExtraQueryParams(endPoint);
			HttpResponse httpResponse;
			using (requestContext.Logger.LogBlockDuration(string.Format("[Oauth2Client] Sending {0} request ", method), LogLevel.Verbose))
			{
				try
				{
					if (method == HttpMethod.Post)
					{
						if (onBeforePostRequestData != null)
						{
							OnBeforeTokenRequestData requestData = new OnBeforeTokenRequestData(this._bodyParameters, this._headers, endpointUri, requestContext.UserCancellationToken);
							await onBeforePostRequestData(requestData).ConfigureAwait(false);
							endpointUri = requestData.RequestUri;
							requestData = null;
						}
						httpResponse = await this._httpManager.SendPostAsync(endpointUri, this._headers, this._bodyParameters, requestContext.Logger, requestContext.UserCancellationToken).ConfigureAwait(false);
					}
					else
					{
						httpResponse = await this._httpManager.SendGetAsync(endpointUri, this._headers, requestContext.Logger, true, requestContext.UserCancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					if (ex is TaskCanceledException && requestContext.UserCancellationToken.IsCancellationRequested)
					{
						throw;
					}
					ILoggerAdapter logger = requestContext.Logger;
					string text = "=== Token Acquisition ({0}) failed:\n\tAuthority: {1}\n\tClientId: {2}.";
					ApiEvent apiEvent = requestContext.ApiEvent;
					string text2 = string.Format(text, (apiEvent != null) ? apiEvent.ApiIdString : null, endpointUri.Scheme + "://" + endpointUri.Host + endpointUri.AbsolutePath, requestContext.ServiceBundle.Config.ClientId);
					string text3 = "=== Token Acquisition ({0}) failed.\n\tHost: {1}.";
					ApiEvent apiEvent2 = requestContext.ApiEvent;
					logger.ErrorPii(text2, string.Format(text3, (apiEvent2 != null) ? apiEvent2.ApiIdString : null, endpointUri.Scheme + "://" + endpointUri.Host));
					requestContext.Logger.ErrorPii(ex);
					throw;
				}
			}
			DurationLogHelper durationLogHelper = null;
			if (requestContext.ApiEvent != null)
			{
				requestContext.ApiEvent.DurationInHttpInMs += this._httpManager.LastRequestDurationInMs;
			}
			if (httpResponse.StatusCode != HttpStatusCode.OK || expectErrorsOn200OK)
			{
				requestContext.Logger.Verbose(() => "[Oauth2Client] Processing error response ");
				try
				{
					if (!string.IsNullOrWhiteSpace(httpResponse.Body))
					{
						MsalTokenResponse msalTokenResponse = JsonHelper.DeserializeFromJson<MsalTokenResponse>(httpResponse.Body);
						if (httpResponse.StatusCode == HttpStatusCode.OK && expectErrorsOn200OK && !string.IsNullOrEmpty((msalTokenResponse != null) ? msalTokenResponse.Error : null))
						{
							OAuth2Client.ThrowServerException(httpResponse, requestContext);
						}
					}
				}
				catch (JsonException)
				{
				}
			}
			return OAuth2Client.CreateResponse<T>(httpResponse, requestContext);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00048A12 File Offset: 0x00046C12
		internal void AddBodyParameter(KeyValuePair<string, string> kvp)
		{
			this._bodyParameters.Add(kvp);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00048A20 File Offset: 0x00046C20
		private void AddCommonHeaders(RequestContext requestContext)
		{
			this._headers.Add("client-request-id", requestContext.CorrelationId.ToString());
			this._headers.Add("return-client-request-id", "true");
			if (!string.IsNullOrWhiteSpace(requestContext.Logger.ClientName))
			{
				this._headers.Add("x-app-name", requestContext.Logger.ClientName);
			}
			if (!string.IsNullOrWhiteSpace(requestContext.Logger.ClientVersion))
			{
				this._headers.Add("x-app-ver", requestContext.Logger.ClientVersion);
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00048AC0 File Offset: 0x00046CC0
		public static T CreateResponse<T>(HttpResponse response, RequestContext requestContext)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				OAuth2Client.ThrowServerException(response, requestContext);
			}
			OAuth2Client.VerifyCorrelationIdHeaderInResponse(response.HeadersAsDictionary, requestContext);
			T t;
			using (requestContext.Logger.LogBlockDuration("[OAuth2Client] Deserializing response", LogLevel.Verbose))
			{
				t = JsonHelper.DeserializeFromJson<T>(response.Body);
			}
			return t;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00048B28 File Offset: 0x00046D28
		private static void ThrowServerException(HttpResponse response, RequestContext requestContext)
		{
			bool flag = true;
			string text = string.Format(CultureInfo.InvariantCulture, "HttpStatusCode: {0}: {1}", (int)response.StatusCode, response.StatusCode.ToString());
			requestContext.Logger.Info(text);
			MsalServiceException ex;
			try
			{
				ex = OAuth2Client.ExtractErrorsFromTheResponse(response, ref flag);
			}
			catch (JsonException)
			{
				ex = MsalServiceExceptionFactory.FromHttpResponse("non_parsable_oauth_error", "An error response was returned by the OAuth2 server, but it could not be parsed. Please inspect the exception properties for details. ", response, null);
			}
			catch (Exception ex2)
			{
				ex = MsalServiceExceptionFactory.FromHttpResponse("unknown_error", response.Body, response, ex2);
			}
			if (ex == null)
			{
				ex = MsalServiceExceptionFactory.FromHttpResponse((response.StatusCode == HttpStatusCode.NotFound) ? "not_found" : "http_status_not_200", text, response, null);
			}
			if (flag)
			{
				ILoggerAdapter logger = requestContext.Logger;
				string text2 = "=== Token Acquisition ({0}) failed:\n\tAuthority: {1}\n\tClientId: {2}.";
				ApiEvent apiEvent = requestContext.ApiEvent;
				string text3 = string.Format(text2, (apiEvent != null) ? apiEvent.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.CanonicalAuthority, requestContext.ServiceBundle.Config.ClientId);
				string text4 = "=== Token Acquisition ({0}) failed.\n\tHost: {1}.";
				ApiEvent apiEvent2 = requestContext.ApiEvent;
				logger.ErrorPii(text3, string.Format(text4, (apiEvent2 != null) ? apiEvent2.ApiIdString : null, requestContext.ServiceBundle.Config.Authority.AuthorityInfo.Host));
				requestContext.Logger.ErrorPii(ex);
			}
			else
			{
				requestContext.Logger.InfoPii(ex);
			}
			throw ex;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00048C94 File Offset: 0x00046E94
		private static MsalServiceException ExtractErrorsFromTheResponse(HttpResponse response, ref bool shouldLogAsError)
		{
			if (string.IsNullOrWhiteSpace(response.Body))
			{
				return null;
			}
			MsalTokenResponse msalTokenResponse;
			try
			{
				msalTokenResponse = JsonHelper.DeserializeFromJson<MsalTokenResponse>(response.Body);
			}
			catch (JsonException)
			{
				if (response.StatusCode == (HttpStatusCode)429)
				{
					return MsalServiceExceptionFactory.FromThrottledAuthenticationResponse(response);
				}
				throw;
			}
			if (msalTokenResponse == null || msalTokenResponse.Error == null)
			{
				return null;
			}
			if (string.Compare(msalTokenResponse.Error, "authorization_pending", StringComparison.OrdinalIgnoreCase) == 0)
			{
				shouldLogAsError = false;
			}
			return MsalServiceExceptionFactory.FromHttpResponse(msalTokenResponse.Error, msalTokenResponse.ErrorDescription, response, null);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00048D20 File Offset: 0x00046F20
		private Uri AddExtraQueryParams(Uri endPoint)
		{
			UriBuilder uriBuilder = new UriBuilder(endPoint);
			string text = this._queryParameters.ToQueryParameter();
			uriBuilder.AppendQueryParameters(text);
			return uriBuilder.Uri;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00048D4C File Offset: 0x00046F4C
		private static void VerifyCorrelationIdHeaderInResponse(IDictionary<string, string> headers, RequestContext requestContext)
		{
			foreach (string text in headers.Keys)
			{
				string text2 = text.Trim();
				if (string.Compare(text2, "client-request-id", StringComparison.OrdinalIgnoreCase) == 0)
				{
					string text3 = headers[text2].Trim();
					if (string.Compare(text3, requestContext.CorrelationId.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
					{
						requestContext.Logger.WarningPii(string.Format(CultureInfo.InvariantCulture, "Returned correlation id '{0}' does not match the sent correlation id '{1}'", text3, requestContext.CorrelationId), "Returned correlation id does not match the sent correlation id");
						break;
					}
					break;
				}
			}
		}

		// Token: 0x0400091B RID: 2331
		private readonly Dictionary<string, string> _headers;

		// Token: 0x0400091C RID: 2332
		private readonly Dictionary<string, string> _queryParameters = new Dictionary<string, string>();

		// Token: 0x0400091D RID: 2333
		private readonly IDictionary<string, string> _bodyParameters = new Dictionary<string, string>();

		// Token: 0x0400091E RID: 2334
		private readonly IHttpManager _httpManager;
	}
}
