using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000749 RID: 1865
	internal class ODataRequest
	{
		// Token: 0x06003730 RID: 14128 RVA: 0x000B011C File Offset: 0x000AE31C
		public ODataRequest(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, string contentType, bool throwOnBadRequest, bool catch404, IEngineHost host, ODataSettingsBase settings, ODataUserSettings userSettings, ODataServerVersion serverVersion, bool handleExceptions = true)
		{
			this.currentResource = resource;
			this.serviceUri = serviceUri;
			this.uri = uri;
			this.credentials = credentials;
			this.contentType = contentType;
			this.throwOnBadRequest = throwOnBadRequest;
			this.catch404 = catch404;
			this.host = host;
			this.settings = settings;
			this.userSettings = userSettings;
			this.serverVersion = serverVersion;
			this.handleExceptions = handleExceptions;
			this.persistentCache = host.GetPersistentCache();
			this.headers = ODataRequest.GetAllHeaders(this.host, this.userSettings, this.serverVersion, headers, ODataRequest.IsMetadataUri(uri));
			this.usedCredentials = credentials;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000B01C8 File Offset: 0x000AE3C8
		private static Value GetAllHeaders(IEngineHost host, ODataUserSettings userSettings, ODataServerVersion serverVersion, Value headers, bool forMetadataUri = false)
		{
			Value value = headers;
			if (headers.IsNull)
			{
				value = userSettings.Headers;
			}
			else if (!userSettings.Headers.IsNull)
			{
				value = headers.Concatenate(userSettings.Headers);
			}
			if (userSettings.ODataVersion == ODataServerVersion.V3)
			{
				value = (value.IsNull ? ODataRequest.V3Header : value.Concatenate(ODataRequest.V3Header));
			}
			else if (userSettings.ODataVersion == ODataServerVersion.V4)
			{
				value = (value.IsNull ? ODataRequest.V4Header : value.Concatenate(ODataRequest.V4Header));
			}
			else
			{
				if (serverVersion != ODataServerVersion.V4)
				{
					value = (value.IsNull ? ODataRequest.V3Header : value.Concatenate(ODataRequest.V3Header));
				}
				if (serverVersion == ODataServerVersion.V4 || serverVersion == ODataServerVersion.All)
				{
					value = (value.IsNull ? ODataRequest.V4Header : value.Concatenate(ODataRequest.V4Header));
				}
			}
			string text = (forMetadataUri ? userSettings.IncludeMetadataAnnotations : userSettings.IncludeAnnotations);
			if (text != null)
			{
				value = ODataRequest.AddToPreferHeader(value, "odata.include-annotations", "\"" + text + "\"");
			}
			if (userSettings.OmitValues != null)
			{
				value = ODataRequest.AddToPreferHeader(value, "omit-values", userSettings.OmitValues);
			}
			return value;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000B02E0 File Offset: 0x000AE4E0
		public string GetCacheKey(string other = null)
		{
			Value value = this.headers;
			Value value2 = RecordValue.New(Keys.New("Accept"), new Value[] { TextValue.New(this.contentType) });
			value = (value.IsRecord ? value.Concatenate(value2) : value2);
			if (this.userSettings.ExcludedFromCacheKey.IsList)
			{
				value = Library.Record.RemoveFields.Invoke(value, this.userSettings.ExcludedFromCacheKey);
			}
			Uri uri = this.userSettings.ApplyQueryOptions(this.uri);
			return this.settings.Cache.GetCacheKey(this.credentials, value, uri, other);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000B0380 File Offset: 0x000AE580
		public HttpResponseData GetResponseStream()
		{
			Tracer tracer = new Tracer(this.host, "Engine/IO/OData/GetResponseStream/", this.currentResource.Resource, null, null);
			HttpResponseData httpResponseData;
			using (IHostTrace hostTrace = tracer.CreateTrace("Get", TraceEventType.Information))
			{
				hostTrace.Add("RequestUri", this.uri.AbsoluteUri, true);
				hostTrace.Add("ContentType", this.contentType, false);
				hostTrace.Add("ThrowOnBadrequest", this.throwOnBadRequest, false);
				try
				{
					string cacheKey = this.GetCacheKey(null);
					Stream responseStream;
					if (!this.persistentCache.TryGetValue(cacheKey, out responseStream))
					{
						this.settings.Cache.OnCacheMissed();
						responseStream = this.GetResponseStream(tracer, hostTrace, cacheKey, false);
					}
					httpResponseData = new HttpResponseData(responseStream);
				}
				catch (Exception ex)
				{
					SafeExceptions.TraceIsSafeException(hostTrace, ex);
					throw;
				}
			}
			return httpResponseData;
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000B0474 File Offset: 0x000AE674
		public MashupHttpWebRequest BuildWebRequest(IResource origin, Uri uri, out IResource resource)
		{
			if (ODataRequest.IsMetadataUri(uri))
			{
				int num = uri.AbsoluteUri.LastIndexOf("$metadata", StringComparison.OrdinalIgnoreCase);
				this.currentResource = this.currentResource.NewUrl(uri.AbsoluteUri.Remove(num));
			}
			else
			{
				this.currentResource = this.currentResource.NewUrl(uri.AbsoluteUri);
			}
			resource = this.currentResource.Resource;
			ResourceCredentialCollection resourceCredentialCollection;
			if (HostResourcePermissionService.InsecureRedirects(this.host))
			{
				resourceCredentialCollection = this.credentials;
			}
			else if (!this.passedInCredentialsUsed && this.currentResource.IsCompatibleWith(this.credentials.Resource))
			{
				this.passedInCredentialsUsed = true;
				if (!HostResourcePermissionService.IsResourceAccessPermitted(this.host, resource, out resourceCredentialCollection))
				{
					resourceCredentialCollection = this.credentials;
				}
			}
			else
			{
				resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, origin, resource, null);
			}
			this.usedCredentials = resourceCredentialCollection;
			return HttpRequestBuilder.BuildWebRequest(this.currentResource, this.serviceUri, uri, this.headers, resourceCredentialCollection, this.settings, this.contentType, this.host, this.userSettings, this.serverVersion);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000B0584 File Offset: 0x000AE784
		private Stream GetResponseStream(Tracer tracer, IHostTrace trace, string key, bool tokenRefreshed = false)
		{
			IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.currentResource.Kind, this.uri.Host);
			Stream stream2;
			using (new ProgressRequest(hostProgress))
			{
				try
				{
					WebRequest lastRequest;
					MashupHttpWebResponse mashupHttpWebResponse = this.settings.RetryPolicy.Execute<MashupHttpWebResponse>(this.host, this.currentResource.Resource, () => (MashupHttpWebResponse)Request.ManualRedirect(this.host, this.currentResource.Kind, this.uri, new Request.CreateWebRequestDelegate(this.BuildWebRequest), new Func<WebRequest, WebResponse>(this.GetResponse), out lastRequest, null), tracer);
					if (mashupHttpWebResponse.ContentType.StartsWith("text/html;", StringComparison.Ordinal) && this.credentials.IsOAuth())
					{
						trace.TracePossibleJwtExpiration(mashupHttpWebResponse, this.credentials);
						Stream stream;
						if (this.TryRequestWithNewToken(tokenRefreshed, key, out stream))
						{
							return stream;
						}
					}
					trace.Add("ResponseContentLength", mashupHttpWebResponse.ContentLength, false);
					stream2 = this.persistentCache.Add(key, HttpResponseData.Serialize(mashupHttpWebResponse, hostProgress));
				}
				catch (WebException ex)
				{
					trace.Add(ex, true);
					trace.TracePossibleJwtExpiration(ex, this.credentials);
					MashupHttpWebResponse mashupHttpWebResponse2 = ex.Response as MashupHttpWebResponse;
					if (mashupHttpWebResponse2 != null)
					{
						Stream stream3;
						if (mashupHttpWebResponse2.StatusCode == HttpStatusCode.Unauthorized && this.TryRequestWithNewToken(tokenRefreshed, key, out stream3))
						{
							return stream3;
						}
						HttpServices.ThrowIfAuthorizationError(this.host, ex, this.currentResource.Resource);
						if (this.catch404 && mashupHttpWebResponse2 != null && mashupHttpWebResponse2.StatusCode == HttpStatusCode.NotFound)
						{
							return HttpResponseData.Serialize(mashupHttpWebResponse2, hostProgress);
						}
					}
					if (!this.handleExceptions || (this.throwOnBadRequest && HttpResponseHandler.IsBadRequestError(ex)))
					{
						throw;
					}
					throw ODataCommonErrors.RequestFailed(this.host, ex, (ex.Response == null) ? this.uri : ex.Response.ResponseUri, this.currentResource);
				}
			}
			return stream2;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000B0788 File Offset: 0x000AE988
		private MashupHttpWebResponse GetResponse(WebRequest webRequest)
		{
			MashupHttpWebResponse mashupHttpWebResponse2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/OData/GetResponse", TraceEventType.Information, this.currentResource.Resource))
			{
				hostTrace.Add("RequestUri", webRequest.RequestUri, true);
				hostTrace.Add("RequestMethod", webRequest.Method, false);
				try
				{
					MashupHttpWebResponse mashupHttpWebResponse = (MashupHttpWebResponse)webRequest.GetResponse();
					hostTrace.Add("ResponseUri", mashupHttpWebResponse.ResponseUri, true);
					hostTrace.Add("ResponseStatusCode", mashupHttpWebResponse.StatusCode, false);
					this.ThrowIfRedirectedToLoginPage(mashupHttpWebResponse);
					mashupHttpWebResponse2 = mashupHttpWebResponse;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return mashupHttpWebResponse2;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000B0848 File Offset: 0x000AEA48
		private void ThrowIfRedirectedToLoginPage(MashupHttpWebResponse response)
		{
			if (Request.IsRedirectStatusCode(response.StatusCode))
			{
				string text = response.Headers["WWW-Authenticate"];
				if (!string.IsNullOrEmpty(text) && text.Contains("error=invalid_token"))
				{
					HttpResource httpResource = this.currentResource.NewUrl(response.ResponseUri.AbsoluteUri);
					HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, httpResource.Resource, null);
					throw DataSourceException.NewAccessAuthorizationError(this.host, httpResource.Resource, null, null, null);
				}
			}
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000B08C8 File Offset: 0x000AEAC8
		private bool HasSameOrigin(string resource1, string resource2)
		{
			Uri uri = new Uri(resource1);
			Uri uri2 = new Uri(resource2);
			return uri.Scheme.Equals(uri2.Scheme, StringComparison.OrdinalIgnoreCase) && uri.Host.Equals(uri2.Host, StringComparison.OrdinalIgnoreCase) && uri.Port == uri2.Port;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000B091C File Offset: 0x000AEB1C
		private bool TryRequestWithNewToken(bool tokenRefreshed, string key, out Stream responseStream)
		{
			if (!tokenRefreshed && this.usedCredentials.HasRefreshableCredential())
			{
				ICredentialService credentialService = this.host.QueryService<ICredentialService>();
				Tracer tracer = new Tracer(this.host, "Engine/IO/OData/TryRefreshOAuthToken/", this.currentResource.Resource, null, null);
				using (IHostTrace hostTrace = tracer.CreateTrace("Get", TraceEventType.Information))
				{
					try
					{
						ResourceCredentialCollection resourceCredentialCollection = credentialService.RefreshCredential(this.usedCredentials.Resource, true);
						hostTrace.Add("RefreshSuccessful", true, false);
						this.credentials = resourceCredentialCollection;
						this.passedInCredentialsUsed = false;
						responseStream = this.GetResponseStream(tracer, hostTrace, key, true);
						return true;
					}
					catch (RuntimeException ex)
					{
						hostTrace.Add("RefreshSuccessful", false, false);
						hostTrace.Add(ex, true);
						responseStream = null;
						return false;
					}
				}
			}
			responseStream = null;
			return false;
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000B0A0C File Offset: 0x000AEC0C
		private static bool IsMetadataUri(Uri uri)
		{
			return uri.Segments[uri.Segments.Length - 1].Equals("$metadata", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000B0A2C File Offset: 0x000AEC2C
		private static Value AddToPreferHeader(Value headersValue, string key, string value)
		{
			Value value2 = TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}={1}", key, value));
			RecordValue recordValue = headersValue as RecordValue;
			int num;
			if (!recordValue.IsNull && recordValue.Keys.TryGetKeyIndex("Prefer", out num))
			{
				Value value3 = recordValue[num];
				value2 = ((!value3.IsNull) ? value3.Concatenate(TextValue.New(";")).Concatenate(value2) : value2);
			}
			RecordValue recordValue2 = RecordValue.New(Keys.New("Prefer"), new Value[] { value2 });
			if (!headersValue.IsNull)
			{
				return headersValue.Concatenate(recordValue2);
			}
			return recordValue2;
		}

		// Token: 0x04001C69 RID: 7273
		private static readonly RecordValue V3Header = RecordValue.New(Keys.New("MaxDataServiceVersion"), new Value[] { TextValue.New("3.0") });

		// Token: 0x04001C6A RID: 7274
		private static readonly RecordValue V4Header = RecordValue.New(Keys.New("OData-MaxVersion"), new Value[] { TextValue.New("4.0") });

		// Token: 0x04001C6B RID: 7275
		private readonly Uri serviceUri;

		// Token: 0x04001C6C RID: 7276
		private readonly Uri uri;

		// Token: 0x04001C6D RID: 7277
		private readonly Value headers;

		// Token: 0x04001C6E RID: 7278
		private readonly string contentType;

		// Token: 0x04001C6F RID: 7279
		private readonly bool throwOnBadRequest;

		// Token: 0x04001C70 RID: 7280
		private readonly bool catch404;

		// Token: 0x04001C71 RID: 7281
		private readonly IEngineHost host;

		// Token: 0x04001C72 RID: 7282
		private readonly ODataSettingsBase settings;

		// Token: 0x04001C73 RID: 7283
		private readonly ODataUserSettings userSettings;

		// Token: 0x04001C74 RID: 7284
		private readonly ODataServerVersion serverVersion;

		// Token: 0x04001C75 RID: 7285
		private readonly bool handleExceptions;

		// Token: 0x04001C76 RID: 7286
		private readonly IPersistentCache persistentCache;

		// Token: 0x04001C77 RID: 7287
		private bool passedInCredentialsUsed;

		// Token: 0x04001C78 RID: 7288
		private HttpResource currentResource;

		// Token: 0x04001C79 RID: 7289
		private ResourceCredentialCollection credentials;

		// Token: 0x04001C7A RID: 7290
		private ResourceCredentialCollection usedCredentials;
	}
}
