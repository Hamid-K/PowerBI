using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A5E RID: 2654
	internal sealed class HttpRequest : Request
	{
		// Token: 0x06004A0A RID: 18954 RVA: 0x000F6A60 File Offset: 0x000F4C60
		public HttpRequest(IEngineHost host, string resourceKind, string resourcePath, Uri uri, TextValue url, Value query, Value content, string webApiKey, Value headers, Value timeout, RetryPolicy retryPolicy, int[] nonErrors, bool dynamicResourcePath, Value credentialQuery, Action<MashupHttpWebResponse, IHostTrace> responseErrorHandler)
			: base(host, resourceKind, uri, url, query, content, webApiKey, headers, timeout.IsNull ? HttpRequest.DefaultTimeout : timeout, retryPolicy, nonErrors, credentialQuery)
		{
			base.Method = (content.IsNull ? "GET" : "POST");
			if (resourcePath == null && dynamicResourcePath)
			{
				IResource resource = Resource.New(resourceKind, uri.AbsoluteUri);
				ResourceCredentialCollection resourceCredentialCollection;
				if (HostResourcePermissionService.IsResourceAccessPermitted(host, resource, out resourceCredentialCollection))
				{
					resourcePath = resource.NonNormalizedPath;
				}
			}
			this.resourcePath = resourcePath ?? base.InitialUri.String;
			this.responseErrorHandler = responseErrorHandler;
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x000F6034 File Offset: 0x000F4234
		public override string ProgressDataSource
		{
			get
			{
				return base.Uri.Host;
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x000F6AFC File Offset: 0x000F4CFC
		public override string ResourcePath
		{
			get
			{
				return this.resourcePath;
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x000F6B04 File Offset: 0x000F4D04
		protected override Response CreateResponse(WebRequest webRequest, WebResponse webResponse, WebException webException = null, ResourceCredentialCollection credentials = null)
		{
			MashupHttpWebResponse currentWebResponse = ((webResponse != null || webException == null) ? ((MashupHttpWebResponse)webResponse) : ((MashupHttpWebResponse)webException.Response));
			return new HttpResponse((MashupHttpWebRequest)webRequest, currentWebResponse, base.KeepCompressed, delegate(Exception exception, long position)
			{
				this.LogResponseError(currentWebResponse, exception, position);
			}, base.Host, (credentials == null || webRequest.Method != "GET") ? null : new Func<HttpResponse>(delegate
			{
				HttpResponse httpResponse = (HttpResponse)this.CreateResponse(credentials);
				currentWebResponse = httpResponse.HttpWebResponse;
				return httpResponse;
			}), webException);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x000F6B98 File Offset: 0x000F4D98
		protected override IResource CreateResource()
		{
			return Resource.New(base.ResourceKind, this.resourcePath);
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x000F6BAC File Offset: 0x000F4DAC
		protected override void ApplyCredentialsToRequest(WebRequest webRequest, ResourceCredentialCollection credentials)
		{
			string text = ((base.OAuthResource == null) ? null : base.OAuthResource.AsString);
			HttpServices.ApplyCredentials((MashupHttpWebRequest)webRequest, base.ResourceKind, base.Uri, credentials, base.Host, text);
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x000F6BF0 File Offset: 0x000F4DF0
		public override Uri ApplyCredentialsToUri(Uri uri, ResourceCredentialCollection credentials)
		{
			uri = UriHelper.AddQueryRecord(uri, base.CredentialQuery);
			UriBuilder uriBuilder = new UriBuilder(uri);
			if (!HttpResourceCredentialDispatcher.ApplyCredentialsToUri(uriBuilder, base.ApiKeyName, credentials, base.Host))
			{
				throw DataSourceException.NewInvalidCredentialsError(base.Host, base.RequestResource, null, null, null);
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000F6C40 File Offset: 0x000F4E40
		protected override bool TryCreateSecurityException(WebException exception, out ResourceSecurityException resourceSecurityException)
		{
			MashupHttpWebResponse mashupHttpWebResponse = exception.Response as MashupHttpWebResponse;
			if (mashupHttpWebResponse != null && exception.Status == WebExceptionStatus.ProtocolError && (base.NonErrors == null || !base.NonErrors.Contains((int)mashupHttpWebResponse.StatusCode)))
			{
				return HttpServices.TryGetResourceSecurityException(base.Host, (int)mashupHttpWebResponse.StatusCode, base.RequestResource, mashupHttpWebResponse.Headers, out resourceSecurityException);
			}
			resourceSecurityException = null;
			return false;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x000F6CA3 File Offset: 0x000F4EA3
		public override void VerifyPermissionAndGetCredentials(out ResourceCredentialCollection credentials)
		{
			HttpServices.VerifyPermissionAndGetCredentials(base.Host, base.RequestResource, base.ApiKeyName != null, out credentials);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x000F6CC0 File Offset: 0x000F4EC0
		public override bool IsFailedStatusCode(Response response)
		{
			return (response.StatusCode >= 300 || response.StatusCode < 200) && (base.NonErrors == null || !base.NonErrors.Contains(response.StatusCode));
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x000F6CFC File Offset: 0x000F4EFC
		protected override bool TryRefreshOAuthToken(WebException exception, ResourceCredentialCollection credentials, out ResourceCredentialCollection refreshedCredentials)
		{
			if (exception.Response is MashupHttpWebResponse && credentials.HasRefreshableCredential())
			{
				ICredentialService credentialService = base.Host.QueryService<ICredentialService>();
				using (IHostTrace hostTrace = TracingService.CreateTrace(base.Host, "Engine/IO/Web/Request/TryRefreshOAuthToken", TraceEventType.Information, base.RequestResource))
				{
					try
					{
						refreshedCredentials = credentialService.RefreshCredential(credentials.Resource, true);
						hostTrace.Add("RefreshSuccessful", true, false);
						return true;
					}
					catch (RuntimeException ex)
					{
						hostTrace.Add("RefreshSuccessful", false, false);
						hostTrace.Add(ex, true);
						refreshedCredentials = null;
						return false;
					}
				}
			}
			refreshedCredentials = null;
			return false;
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x000F6DB8 File Offset: 0x000F4FB8
		private void LogResponseError(MashupHttpWebResponse response, Exception exception, long position)
		{
			if (this.responseErrorHandler == null)
			{
				base.LogError(exception, position);
				return;
			}
			base.LogError(exception, delegate(IHostTrace trace)
			{
				this.responseErrorHandler(response, trace);
			}, position);
		}

		// Token: 0x0400276F RID: 10095
		private static readonly Value DefaultTimeout = new DurationValue(TimeSpan.FromSeconds(100.0));

		// Token: 0x04002770 RID: 10096
		private readonly string resourcePath;

		// Token: 0x04002771 RID: 10097
		private readonly Action<MashupHttpWebResponse, IHostTrace> responseErrorHandler;
	}
}
