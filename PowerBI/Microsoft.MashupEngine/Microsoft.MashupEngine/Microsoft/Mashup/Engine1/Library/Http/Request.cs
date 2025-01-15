using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A72 RID: 2674
	internal abstract class Request
	{
		// Token: 0x06004AD4 RID: 19156 RVA: 0x000F897C File Offset: 0x000F6B7C
		protected Request(IEngineHost host, string resourceKind, Uri uri, TextValue initialUri, Value query, Value content, string webApiKey, Value headers, Value timeout, RetryPolicy retryPolicy, int[] nonErrors = null, Value credentialQuery = null)
		{
			this.host = host;
			this.resourceKind = resourceKind;
			this.uri = uri;
			this.initialUri = initialUri;
			this.query = query;
			this.content = content;
			this.apiKeyName = webApiKey;
			this.headers = headers;
			this.isMetadata = false;
			this.useCache = true;
			this.useBuffer = true;
			this.keepCompressed = false;
			this.timeout = timeout;
			this.nonErrors = nonErrors;
			this.credentialQuery = credentialQuery ?? Value.Null;
			this.contentLength = -1L;
			this.retryPolicy = retryPolicy ?? this.GetDefaultRetryPolicy();
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x000F8A23 File Offset: 0x000F6C23
		private RetryPolicy GetDefaultRetryPolicy()
		{
			return new RetryPolicy(3, new Func<Exception, RetryHandlerResult>(RetryPolicy.IsTransient), this.nonErrors, RetryDelayAlgorithm.ExponentialBackoff, 6);
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x000F8A3F File Offset: 0x000F6C3F
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x000F8A47 File Offset: 0x000F6C47
		// (set) Token: 0x06004AD8 RID: 19160 RVA: 0x000F8A4F File Offset: 0x000F6C4F
		public string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x000F8A58 File Offset: 0x000F6C58
		// (set) Token: 0x06004ADA RID: 19162 RVA: 0x000F8A60 File Offset: 0x000F6C60
		public long ContentLength
		{
			get
			{
				return this.contentLength;
			}
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06004ADB RID: 19163 RVA: 0x000F8A69 File Offset: 0x000F6C69
		public string ResourceKind
		{
			get
			{
				return this.resourceKind;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x000F8A71 File Offset: 0x000F6C71
		public virtual string ResourcePath
		{
			get
			{
				return this.RequestResource.NonNormalizedPath;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x000F8A7E File Offset: 0x000F6C7E
		public Value Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x000F8A86 File Offset: 0x000F6C86
		public Value CredentialQuery
		{
			get
			{
				return this.credentialQuery;
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06004ADF RID: 19167 RVA: 0x000F8A8E File Offset: 0x000F6C8E
		public Value Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x000F8A96 File Offset: 0x000F6C96
		public string ApiKeyName
		{
			get
			{
				return this.apiKeyName;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x000F8A9E File Offset: 0x000F6C9E
		public Value Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x000F8AA6 File Offset: 0x000F6CA6
		public Value Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x000F8AAE File Offset: 0x000F6CAE
		public IResource RequestResource
		{
			get
			{
				if (this.requestResource == null)
				{
					this.requestResource = this.CreateResource();
				}
				return this.requestResource;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x000F8ACA File Offset: 0x000F6CCA
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x000F8AD2 File Offset: 0x000F6CD2
		public TextValue InitialUri
		{
			get
			{
				return this.initialUri;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x000F8ADA File Offset: 0x000F6CDA
		// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x000F8AE2 File Offset: 0x000F6CE2
		public bool IsMetadata
		{
			get
			{
				return this.isMetadata;
			}
			set
			{
				this.isMetadata = value;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x000F8AEB File Offset: 0x000F6CEB
		// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x000F8AF3 File Offset: 0x000F6CF3
		public bool UseCache
		{
			get
			{
				return this.useCache;
			}
			set
			{
				this.useCache = value;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x000F8AFC File Offset: 0x000F6CFC
		// (set) Token: 0x06004AEB RID: 19179 RVA: 0x000F8B04 File Offset: 0x000F6D04
		public bool UseBuffer
		{
			get
			{
				return this.useBuffer;
			}
			set
			{
				this.useBuffer = value;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x000F8B0D File Offset: 0x000F6D0D
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x000F8B15 File Offset: 0x000F6D15
		public bool KeepCompressed
		{
			get
			{
				return this.keepCompressed;
			}
			set
			{
				this.keepCompressed = value;
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x000F8B1E File Offset: 0x000F6D1E
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x000F8B26 File Offset: 0x000F6D26
		public bool IsRetry
		{
			get
			{
				return this.isRetry;
			}
			set
			{
				this.isRetry = value;
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x000F8B2F File Offset: 0x000F6D2F
		// (set) Token: 0x06004AF1 RID: 19185 RVA: 0x000F8B37 File Offset: 0x000F6D37
		public Value ExcludedHeaders
		{
			get
			{
				return this.excludedHeaders;
			}
			set
			{
				this.excludedHeaders = value;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x000F8B40 File Offset: 0x000F6D40
		// (set) Token: 0x06004AF3 RID: 19187 RVA: 0x000F8B48 File Offset: 0x000F6D48
		public TextValue OAuthResource
		{
			get
			{
				return this.oAuthResource;
			}
			set
			{
				this.oAuthResource = value;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x000F8B51 File Offset: 0x000F6D51
		public int[] NonErrors
		{
			get
			{
				return this.nonErrors;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x000F8B59 File Offset: 0x000F6D59
		// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x000F8B61 File Offset: 0x000F6D61
		public X509Certificate2 ClientCertificate { get; set; }

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x000F8B6A File Offset: 0x000F6D6A
		// (set) Token: 0x06004AF8 RID: 19192 RVA: 0x000F8B72 File Offset: 0x000F6D72
		public bool AllowUnpermittedRedirects
		{
			get
			{
				return this.allowUnpermittedRedirects;
			}
			set
			{
				this.allowUnpermittedRedirects = value;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x000F8B7B File Offset: 0x000F6D7B
		// (set) Token: 0x06004AFA RID: 19194 RVA: 0x000F8B83 File Offset: 0x000F6D83
		public IList<string> SafeRequestHeaders { get; set; }

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x000F8B8C File Offset: 0x000F6D8C
		// (set) Token: 0x06004AFC RID: 19196 RVA: 0x000F8B94 File Offset: 0x000F6D94
		public IList<string> SafeResponseHeaders { get; set; }

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x000F8B9D File Offset: 0x000F6D9D
		// (set) Token: 0x06004AFE RID: 19198 RVA: 0x000F8BA5 File Offset: 0x000F6DA5
		public RecordValue TraceData { get; set; }

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06004AFF RID: 19199
		public abstract string ProgressDataSource { get; }

		// Token: 0x06004B00 RID: 19200
		public abstract void VerifyPermissionAndGetCredentials(out ResourceCredentialCollection credentials);

		// Token: 0x06004B01 RID: 19201 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsFailedStatusCode(Response response)
		{
			return false;
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x000F8BAE File Offset: 0x000F6DAE
		public static bool TryCreateSecurityException(Request request, WebException webException, out ResourceSecurityException resourceSecurityException)
		{
			return request.TryCreateSecurityException(webException, out resourceSecurityException);
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004B03 RID: 19203 RVA: 0x000F8BB8 File Offset: 0x000F6DB8
		private Tracer Tracer
		{
			get
			{
				if (this.tracer == null)
				{
					this.tracer = new Tracer(this.host, "Engine/IO/Web/Request/", this.RequestResource, null, null);
				}
				return this.tracer;
			}
		}

		// Token: 0x06004B04 RID: 19204
		protected abstract IResource CreateResource();

		// Token: 0x06004B05 RID: 19205
		protected abstract Response CreateResponse(WebRequest webRequest, WebResponse webResponse, WebException exception = null, ResourceCredentialCollection credentials = null);

		// Token: 0x06004B06 RID: 19206
		protected abstract bool TryCreateSecurityException(WebException exception, out ResourceSecurityException resourceSecurityException);

		// Token: 0x06004B07 RID: 19207
		protected abstract void ApplyCredentialsToRequest(WebRequest webRequest, ResourceCredentialCollection credentials);

		// Token: 0x06004B08 RID: 19208 RVA: 0x000912D6 File Offset: 0x0008F4D6
		protected virtual bool TryRefreshOAuthToken(WebException exception, ResourceCredentialCollection credentials, out ResourceCredentialCollection refreshedCredentials)
		{
			refreshedCredentials = null;
			return false;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x000F8BE6 File Offset: 0x000F6DE6
		public virtual Uri ApplyCredentialsToUri(Uri uri, ResourceCredentialCollection credentials)
		{
			return UriHelper.AddQueryRecord(uri, this.CredentialQuery);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x000F8BF4 File Offset: 0x000F6DF4
		public virtual string GetCacheKey(ResourceCredentialCollection credentials)
		{
			Value value = this.Headers;
			if (value.IsRecord && this.ExcludedHeaders != null && this.ExcludedHeaders.IsList)
			{
				value = Library.Record.RemoveFields.Invoke(value, this.ExcludedHeaders);
			}
			return PersistentCacheKey.SchemeInvoke.Qualify(credentials.GetHash(), UriHelper.AddQueryRecord(this.Uri, this.CredentialQuery).AbsoluteUri, this.Method, this.Content.IsNull ? string.Empty : Convert.ToBase64String(this.Content.AsBinary.AsBytes), value.IsNull ? string.Empty : value.AsRecord.CreateCacheKey(), this.ApiKeyName ?? string.Empty);
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x000F8CB8 File Offset: 0x000F6EB8
		public string GetHeaderString()
		{
			if (this.headers.IsNull)
			{
				return null;
			}
			RecordValue asRecord = this.headers.AsRecord;
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			List<string> list = asRecord.Keys.ToList<string>();
			list.Sort();
			foreach (string text in list)
			{
				string asString = asRecord[text].AsString;
				try
				{
					webHeaderCollection.Add(text, asString);
				}
				catch (ArgumentException ex)
				{
					throw ValueException.NewDataFormatError(ex.Message, asRecord[text], ex);
				}
			}
			return webHeaderCollection.ToString();
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x000F8D74 File Offset: 0x000F6F74
		public Response GetResponse(ResourceCredentialCollection credentials, Request.SecurityExceptionCreator securityExceptionCreator = null, bool tokenRefreshed = false)
		{
			Response response2;
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("GetResponse", TraceEventType.Information))
			{
				this.AddTraces(hostTrace);
				try
				{
					Response response = this.retryPolicy.Execute<Response>(this.host, this.RequestResource, () => this.GetResponseCore(credentials), this.Tracer);
					hostTrace.Add("ResponseStatusCode", response.StatusCode, false);
					hostTrace.Add("ResponseContentLength", response.ContentLength, false);
					this.TraceResponseHeaders(hostTrace, response.Headers);
					response2 = response;
				}
				catch (WebException ex)
				{
					MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
					if (mashupHttpWebResponse != null)
					{
						hostTrace.Add("StatusCode", mashupHttpWebResponse.StatusCode, false);
						hostTrace.Add("StatusDescription", mashupHttpWebResponse.StatusDescription, true);
						this.TraceResponseHeaders(hostTrace, mashupHttpWebResponse.Headers);
					}
					hostTrace.Add(ex, true);
					hostTrace.TracePossibleJwtExpiration(ex, credentials);
					securityExceptionCreator = securityExceptionCreator ?? new Request.SecurityExceptionCreator(Request.TryCreateSecurityException);
					ResourceSecurityException ex2;
					if (securityExceptionCreator(this, ex, out ex2))
					{
						ResourceCredentialCollection resourceCredentialCollection;
						if (tokenRefreshed || !(ex2 is ResourceAccessAuthorizationException) || !this.TryRefreshOAuthToken(ex, credentials, out resourceCredentialCollection))
						{
							throw ex2;
						}
						response2 = this.GetResponse(resourceCredentialCollection, securityExceptionCreator, true);
					}
					else
					{
						if (ex.Response == null)
						{
							throw new ResponseException(ex);
						}
						response2 = this.CreateResponse(this.CreateWebRequest(this.uri, credentials), ex.Response, ex, null);
					}
				}
				catch (ArgumentException ex3)
				{
					hostTrace.Add(ex3, true);
					throw ValueException.NewDataFormatError<Message1>(Strings.WebResponseMalformed(this.Uri.OriginalString), Value.Null, ex3);
				}
				catch (IOException ex4)
				{
					hostTrace.Add(ex4, true);
					throw new ResponseException(ex4);
				}
			}
			return response2;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x000F8FAC File Offset: 0x000F71AC
		protected void TraceRequestHeaders(IHostTrace trace)
		{
			IList<string> safeRequestHeaders = this.SafeRequestHeaders;
			if (safeRequestHeaders != null && safeRequestHeaders.Count > 0 && this.Headers.IsRecord && this.Headers.AsRecord.Count > 0)
			{
				foreach (string text in this.SafeRequestHeaders)
				{
					Value value;
					if (this.Headers.TryGetValue(text, out value) && value.IsText)
					{
						trace.Add("Sent_" + text, value.AsString, false);
					}
				}
			}
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x000F905C File Offset: 0x000F725C
		protected void TraceResponseHeaders(IHostTrace trace, WebHeaderCollection headers)
		{
			IList<string> safeResponseHeaders = this.SafeResponseHeaders;
			if (safeResponseHeaders != null && safeResponseHeaders.Count > 0 && headers.Count > 0)
			{
				foreach (string text in this.SafeResponseHeaders)
				{
					string text2 = headers[text];
					if (text2 != null)
					{
						trace.Add("Rcvd_" + text, text2, false);
					}
				}
			}
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x000F90E0 File Offset: 0x000F72E0
		protected void TraceAdditionalData(IHostTrace trace)
		{
			RecordValue traceData = this.TraceData;
			if (traceData != null && traceData.Count > 0)
			{
				for (int i = 0; i < this.TraceData.Count; i++)
				{
					string text = this.TraceData.Keys[i];
					Value value = this.TraceData[i];
					bool flag = ValueException2.IsPii(value);
					object obj;
					try
					{
						obj = ValueMarshaller.MarshalToClr(value);
					}
					catch (ValueException ex)
					{
						obj = ex.ToString();
					}
					trace.Add(text, obj, flag);
				}
			}
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x000F9170 File Offset: 0x000F7370
		private Response GetResponseCore(ResourceCredentialCollection credentials)
		{
			ICacheSets cacheSets = this.host.QueryService<ICacheSets>();
			IPersistentCache persistentCache = (this.IsMetadata ? cacheSets.Metadata.PersistentCache : cacheSets.Data.PersistentCache);
			IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.resourceKind, this.ProgressDataSource);
			if (this.UseCache)
			{
				using (new ProgressRequest(hostProgress))
				{
					string cacheKey = this.GetCacheKey(credentials);
					Stream stream;
					if (this.IsRetry || !persistentCache.TryGetValue(cacheKey, out stream))
					{
						Response response = this.CreateResponse(credentials);
						if (response.ContentLength > 0L && response.ContentLength < 4194304L)
						{
							stream = persistentCache.BeginAdd();
							using (Stream stream2 = Response.Serialize(response))
							{
								stream2.CopyTo(stream);
							}
							stream = persistentCache.EndAdd(cacheKey, stream);
						}
						else
						{
							stream = persistentCache.Add(cacheKey, Response.Serialize(response));
						}
					}
					return Response.Deserialize(stream);
				}
			}
			return new ProgressResponse(this.CreateResponse(credentials), hostProgress);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x000F92AC File Offset: 0x000F74AC
		protected Response CreateResponse(ResourceCredentialCollection credentials)
		{
			int num;
			bool flag = this.CallerHandlesRedirect(out num);
			bool flag2 = HostResourcePermissionService.InsecureRedirects(this.Host);
			bool flag3 = this.Host.QueryService<IExtensibilityService>() != null;
			if (flag && flag3)
			{
				flag2 = false;
			}
			if (!flag2 || this.allowUnpermittedRedirects)
			{
				Request.CreateWebRequestDelegate createWebRequestDelegate;
				if (this.allowUnpermittedRedirects)
				{
					createWebRequestDelegate = delegate(IResource origin, Uri uri, out IResource resource)
					{
						return this.CreateUnverifiedWebRequest(uri, credentials, out resource);
					};
				}
				else
				{
					createWebRequestDelegate = delegate(IResource origin, Uri uri, out IResource resource)
					{
						return this.CreateWebRequest(credentials, origin, uri, out resource);
					};
				}
				WebRequest webRequest;
				WebResponse webResponse = Request.ManualRedirect(this.Host, this.ResourceKind, this.Uri, createWebRequestDelegate, new Func<WebRequest, WebResponse>(this.GetWebResponse), out webRequest, this.nonErrors);
				return this.CreateResponse(webRequest, webResponse, null, credentials);
			}
			if (flag)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.WebContentsManualStatusHandlingInvalidStatus(num), null, null);
			}
			WebRequest webRequest2 = this.CreateWebRequest(this.Uri, credentials);
			WebResponse webResponse2 = this.GetWebResponse(webRequest2);
			return this.CreateResponse(webRequest2, webResponse2, null, credentials);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x000F93B4 File Offset: 0x000F75B4
		private bool CallerHandlesRedirect(out int handledRedirect)
		{
			if (this.nonErrors != null)
			{
				foreach (int num in this.nonErrors)
				{
					if (Request.IsRedirectStatusCode((HttpStatusCode)num))
					{
						handledRedirect = num;
						return true;
					}
				}
			}
			handledRedirect = 0;
			return false;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x000F93F3 File Offset: 0x000F75F3
		private WebRequest CreateUnverifiedWebRequest(Uri uri, ResourceCredentialCollection credentials, out IResource resource)
		{
			WebRequest webRequest = this.CreateWebRequest(uri, credentials);
			resource = credentials.Resource;
			return webRequest;
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x000F9408 File Offset: 0x000F7608
		private WebRequest CreateWebRequest(ResourceCredentialCollection initialCredentials, IResource origin, Uri uri, out IResource resource)
		{
			resource = Resource.New(this.ResourceKind, uri.AbsoluteUri);
			ResourceCredentialCollection resourceCredentialCollection;
			if (initialCredentials == null || origin != null)
			{
				resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.Host, origin, resource, null);
			}
			else
			{
				resourceCredentialCollection = initialCredentials;
			}
			return this.CreateWebRequest(uri, resourceCredentialCollection);
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x000F944C File Offset: 0x000F764C
		private WebRequest CreateWebRequest(Uri uri, ResourceCredentialCollection credentials)
		{
			WebRequest webRequest = this.Host.CreateWebRequest(credentials.Resource, this.ApplyCredentialsToUri(uri, credentials));
			if (webRequest == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.UriInvalidArgument, this.InitialUri, null);
			}
			webRequest.CachePolicy = Request.WebRequestReloadPolicy;
			MashupHttpWebRequest mashupHttpWebRequest = webRequest as MashupHttpWebRequest;
			if (mashupHttpWebRequest != null)
			{
				mashupHttpWebRequest.AllowWriteStreamBuffering = this.UseBuffer;
				mashupHttpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				if (!this.Timeout.IsNull)
				{
					double num = Math.Round(this.Timeout.AsDuration.AsClrTimeSpan.TotalMilliseconds);
					mashupHttpWebRequest.Timeout = ((num <= 2147483647.0) ? ((int)num) : (-1));
					if (mashupHttpWebRequest.ReadWriteTimeout < mashupHttpWebRequest.Timeout)
					{
						mashupHttpWebRequest.ReadWriteTimeout = mashupHttpWebRequest.Timeout;
					}
				}
				if (this.ClientCertificate != null)
				{
					mashupHttpWebRequest.ClientCertificates.Add(this.ClientCertificate);
				}
			}
			RequestHeaders.Create(webRequest).ApplyHeaders(this.Headers);
			webRequest.Method = this.Method;
			if (this.ContentLength != -1L)
			{
				webRequest.ContentLength = this.ContentLength;
			}
			this.ApplyCredentialsToRequest(webRequest, credentials);
			webRequest.AdjustForCompression();
			return webRequest;
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x000F956C File Offset: 0x000F776C
		private WebResponse GetWebResponse(WebRequest webRequest)
		{
			if (!this.Content.IsNull)
			{
				using (Stream stream = this.Content.AsBinary.Open())
				{
					using (Stream requestStream = webRequest.GetRequestStream())
					{
						stream.CopyTo(requestStream);
					}
				}
			}
			return webRequest.GetResponse();
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x000F95E0 File Offset: 0x000F77E0
		protected void LogError(Exception exception, long position)
		{
			this.LogError(exception, null, position);
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x000F95EC File Offset: 0x000F77EC
		protected void LogError(Exception exception, Action<IHostTrace> extraErrorTraceHandler, long position)
		{
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("LogError", TraceEventType.Information))
			{
				this.AddTraces(hostTrace);
				hostTrace.Add("Position", position, false);
				if (extraErrorTraceHandler != null)
				{
					extraErrorTraceHandler(hostTrace);
				}
				hostTrace.Add(exception, true);
			}
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x000F9654 File Offset: 0x000F7854
		private void AddTraces(IHostTrace trace)
		{
			trace.Add("RequestMethod", this.Method, false);
			trace.Add("RequestUri", this.Uri, true);
			trace.Add("RequestHasContent", !this.Content.IsNull, false);
			trace.Add("RequestHasHeaders", !this.Headers.IsNull, false);
			trace.Add("RequestHasTimeout", !this.Timeout.IsNull, false);
			trace.Add("UseCache", this.UseCache, false);
			trace.Add("UseBuffer", this.UseBuffer, false);
			Value value;
			if (this.Headers.IsRecord && (this.Headers.TryGetValue("Range", out value) || this.Headers.TryGetValue("x-ms-range", out value)) && value.IsText)
			{
				trace.Add("Range", value.AsString, false);
			}
			this.TraceRequestHeaders(trace);
			this.TraceAdditionalData(trace);
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x000F9770 File Offset: 0x000F7970
		public static Request Create(IEngineHost host, string resourceKind, string resourcePath, TextValue url, Value query = null, Value content = null, string webApiKey = null, Value headers = null, Value timeout = null, int[] nonErrors = null, Value relativePath = null, Value credentialQuery = null, Action<MashupHttpWebResponse, IHostTrace> responseErrorHandler = null, RetryPolicy retryPolicy = null)
		{
			query = query ?? Value.Null;
			credentialQuery = credentialQuery ?? Value.Null;
			content = content ?? Value.Null;
			headers = headers ?? Value.Null;
			timeout = timeout ?? Value.Null;
			relativePath = relativePath ?? Value.Null;
			bool flag = false;
			Uri uri = UriHelper.CreateAbsoluteUriFromValue(url);
			if (!relativePath.IsNull)
			{
				uri = UriHelper.Combine(uri, relativePath.AsText);
				flag = true;
			}
			if (!query.IsNull && query.IsRecord)
			{
				uri = UriHelper.AddQueryRecord(uri, query.AsRecord);
			}
			if (UriHelper.IsFileUri(uri))
			{
				return new FileRequest(host, uri, url, query, content, webApiKey, headers, timeout, retryPolicy);
			}
			if (UriHelper.IsFtpUri(uri))
			{
				return new FtpRequest(host, uri, url, query, content, webApiKey, headers, timeout, retryPolicy);
			}
			if (UriHelper.IsWebUri(uri))
			{
				return new HttpRequest(host, resourceKind, resourcePath, uri, url, query, content, webApiKey, headers, timeout, retryPolicy, nonErrors, flag, credentialQuery, responseErrorHandler);
			}
			throw ValueException.NewExpressionError<Message1>(Strings.WebContentsSchemeUnsupported(uri.Scheme), url, null);
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x000F987F File Offset: 0x000F7A7F
		public static bool IsRedirectStatusCode(HttpStatusCode statusCode)
		{
			return statusCode == HttpStatusCode.MultipleChoices || statusCode == HttpStatusCode.MovedPermanently || statusCode == HttpStatusCode.Found || statusCode == HttpStatusCode.SeeOther || statusCode == HttpStatusCode.TemporaryRedirect;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x000F98AC File Offset: 0x000F7AAC
		public static WebResponse ManualRedirect(IEngineHost host, string resourceKind, Uri uri, Request.CreateWebRequestDelegate createRequest, Func<WebRequest, WebResponse> createResponse, out WebRequest webRequest, int[] nonErrors = null)
		{
			int num = 0;
			IResource resource;
			webRequest = createRequest(null, uri, out resource);
			WebResponse webResponse;
			for (;;)
			{
				MashupHttpWebRequest mashupHttpWebRequest = webRequest as MashupHttpWebRequest;
				if (mashupHttpWebRequest != null)
				{
					mashupHttpWebRequest.AllowAutoRedirect = false;
				}
				IDisposable disposable = null;
				ImpersonationCredential impersonationCredential = webRequest.Credentials as ImpersonationCredential;
				if (impersonationCredential != null)
				{
					webRequest.UseDefaultCredentials = true;
					disposable = impersonationCredential.Impersonate();
				}
				using (disposable)
				{
					webResponse = createResponse(webRequest);
				}
				MashupHttpWebResponse mashupHttpWebResponse = webResponse as MashupHttpWebResponse;
				if (mashupHttpWebResponse == null || !Request.IsRedirectStatusCode(mashupHttpWebResponse.StatusCode) || (nonErrors != null && nonErrors.Contains((int)mashupHttpWebResponse.StatusCode)))
				{
					break;
				}
				num++;
				if (num > ((MashupHttpWebRequest)webRequest).MaximumAutomaticRedirections)
				{
					goto Block_7;
				}
				string text = webResponse.Headers[HttpResponseHeader.Location];
				if (text == null)
				{
					goto Block_8;
				}
				try
				{
					uri = new Uri(uri, text);
				}
				catch (UriFormatException)
				{
					throw new WebException(Strings.RequestRedirectionProtocolError, null, WebExceptionStatus.ProtocolError, webResponse);
				}
				if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
				{
					goto Block_11;
				}
				string text2 = webRequest.Method;
				switch (mashupHttpWebResponse.StatusCode)
				{
				case HttpStatusCode.MovedPermanently:
				case HttpStatusCode.Found:
					if (text2.Equals("POST"))
					{
						text2 = "GET";
					}
					break;
				case HttpStatusCode.SeeOther:
				case HttpStatusCode.NotModified:
				case HttpStatusCode.UseProxy:
				case HttpStatusCode.Unused:
					goto IL_0196;
				case HttpStatusCode.TemporaryRedirect:
					break;
				default:
					goto IL_0196;
				}
				IL_019D:
				webRequest.Abort();
				webResponse.Close();
				webRequest = createRequest(resource, uri, out resource);
				webRequest.Method = text2;
				continue;
				IL_0196:
				text2 = "GET";
				goto IL_019D;
			}
			return webResponse;
			Block_7:
			throw new WebException(Strings.RequestRedirectionMaximumReach, null, WebExceptionStatus.ProtocolError, webResponse);
			Block_8:
			throw new WebException(Strings.RequestRedirectLocationMissing, null, WebExceptionStatus.ProtocolError, webResponse);
			Block_11:
			throw new WebException(Strings.RequestRedirectionProtocolError, null, WebExceptionStatus.ProtocolError, webResponse);
		}

		// Token: 0x040027A7 RID: 10151
		public const string DeleteMethodKey = "DELETE";

		// Token: 0x040027A8 RID: 10152
		public const string GetMethodKey = "GET";

		// Token: 0x040027A9 RID: 10153
		public const string HeadMethodKey = "HEAD";

		// Token: 0x040027AA RID: 10154
		public const string PatchMethodKey = "PATCH";

		// Token: 0x040027AB RID: 10155
		public const string PostMethodKey = "POST";

		// Token: 0x040027AC RID: 10156
		public const string PutMethodKey = "PUT";

		// Token: 0x040027AD RID: 10157
		private const long CacheOnceSize = 4194304L;

		// Token: 0x040027AE RID: 10158
		private static readonly RequestCachePolicy WebRequestReloadPolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x040027AF RID: 10159
		private readonly Uri uri;

		// Token: 0x040027B0 RID: 10160
		private readonly IEngineHost host;

		// Token: 0x040027B1 RID: 10161
		private readonly string resourceKind;

		// Token: 0x040027B2 RID: 10162
		private readonly TextValue initialUri;

		// Token: 0x040027B3 RID: 10163
		private readonly Value query;

		// Token: 0x040027B4 RID: 10164
		private readonly Value content;

		// Token: 0x040027B5 RID: 10165
		private readonly string apiKeyName;

		// Token: 0x040027B6 RID: 10166
		private readonly Value headers;

		// Token: 0x040027B7 RID: 10167
		private readonly Value timeout;

		// Token: 0x040027B8 RID: 10168
		private readonly int[] nonErrors;

		// Token: 0x040027B9 RID: 10169
		private readonly RetryPolicy retryPolicy;

		// Token: 0x040027BA RID: 10170
		private readonly Value credentialQuery;

		// Token: 0x040027BB RID: 10171
		private IResource requestResource;

		// Token: 0x040027BC RID: 10172
		private bool isMetadata;

		// Token: 0x040027BD RID: 10173
		private bool useCache;

		// Token: 0x040027BE RID: 10174
		private bool useBuffer;

		// Token: 0x040027BF RID: 10175
		private Value excludedHeaders;

		// Token: 0x040027C0 RID: 10176
		private bool isRetry;

		// Token: 0x040027C1 RID: 10177
		private bool allowUnpermittedRedirects;

		// Token: 0x040027C2 RID: 10178
		private TextValue oAuthResource;

		// Token: 0x040027C3 RID: 10179
		private bool keepCompressed;

		// Token: 0x040027C4 RID: 10180
		private Tracer tracer;

		// Token: 0x040027C5 RID: 10181
		protected string method;

		// Token: 0x040027C6 RID: 10182
		protected long contentLength;

		// Token: 0x02000A73 RID: 2675
		// (Invoke) Token: 0x06004B1F RID: 19231
		public delegate WebRequest CreateWebRequestDelegate(IResource origin, Uri uri, out IResource resource);

		// Token: 0x02000A74 RID: 2676
		// (Invoke) Token: 0x06004B23 RID: 19235
		public delegate bool SecurityExceptionCreator(Request request, WebException e, out ResourceSecurityException resourceSecurityException);
	}
}
