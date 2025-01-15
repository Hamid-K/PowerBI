using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A9E RID: 2718
	internal static class WebRequestFactory
	{
		// Token: 0x06004C17 RID: 19479 RVA: 0x000FBC14 File Offset: 0x000F9E14
		public static WebRequest CreateWebRequest(this IEngineHost engineHost, IResource resource, Uri uri)
		{
			IHttpUriRewritingService httpUriRewritingService = engineHost.QueryService<IHttpUriRewritingService>();
			Uri uri2 = null;
			bool flag = httpUriRewritingService != null && httpUriRewritingService.TryRewriteRequestUri(uri, out uri2);
			WebRequest webRequest = ProxyWebRequest.CreateRequest(uri2 ?? uri);
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				MashupHttpWebRequest mashupHttpWebRequest = new WrappingHttpWebRequest(httpWebRequest);
				if (flag)
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(engineHost, "Engine/IO/Web/Request/WebRequestFactory/CreateWebRequest", TraceEventType.Information, null))
					{
						hostTrace.Add("OriginalUri", uri.AbsoluteUri, true);
						hostTrace.Add("RewrittenUri", uri2.AbsoluteUri, true);
						mashupHttpWebRequest = new WebRequestFactory.RewrittenHttpWebRequest(mashupHttpWebRequest, httpUriRewritingService, uri);
					}
				}
				if (RequestTracingService.IsTracePermitted(engineHost, resource))
				{
					mashupHttpWebRequest = new TracingHttpWebRequest(mashupHttpWebRequest, engineHost, resource);
				}
				return mashupHttpWebRequest;
			}
			return webRequest;
		}

		// Token: 0x02000A9F RID: 2719
		private sealed class RewrittenHttpWebRequest : DelegatingHttpWebRequest
		{
			// Token: 0x06004C18 RID: 19480 RVA: 0x000FBCD4 File Offset: 0x000F9ED4
			public RewrittenHttpWebRequest(MashupHttpWebRequest request, IHttpUriRewritingService uriRewritingService, Uri originalUri)
				: base(request)
			{
				this.uriRewritingService = uriRewritingService;
				this.originalUri = originalUri;
			}

			// Token: 0x170017F4 RID: 6132
			// (get) Token: 0x06004C19 RID: 19481 RVA: 0x000FBCEB File Offset: 0x000F9EEB
			public override Uri RequestUri
			{
				get
				{
					return this.originalUri;
				}
			}

			// Token: 0x06004C1A RID: 19482 RVA: 0x000FBCF4 File Offset: 0x000F9EF4
			public override Stream EndGetRequestStream(IAsyncResult asyncResult)
			{
				return this.WrapExceptionResponse<Stream>(() => this.<>n__0(asyncResult));
			}

			// Token: 0x06004C1B RID: 19483 RVA: 0x000FBD28 File Offset: 0x000F9F28
			public override Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
			{
				TransportContext innerContext = null;
				Stream stream = this.WrapExceptionResponse<Stream>(() => this.<>n__1(asyncResult, out innerContext));
				context = innerContext;
				return stream;
			}

			// Token: 0x06004C1C RID: 19484 RVA: 0x000FBD6C File Offset: 0x000F9F6C
			public override WebResponse EndGetResponse(IAsyncResult asyncResult)
			{
				return this.WrapResponse(() => this.<>n__2(asyncResult));
			}

			// Token: 0x06004C1D RID: 19485 RVA: 0x000FBD9F File Offset: 0x000F9F9F
			public override Stream GetRequestStream()
			{
				return this.WrapExceptionResponse<Stream>(() => base.GetRequestStream());
			}

			// Token: 0x06004C1E RID: 19486 RVA: 0x000FBDB4 File Offset: 0x000F9FB4
			public override Stream GetRequestStream(out TransportContext context)
			{
				TransportContext innerContext = null;
				Stream stream = this.WrapExceptionResponse<Stream>(() => this.<>n__3(out innerContext));
				context = innerContext;
				return stream;
			}

			// Token: 0x06004C1F RID: 19487 RVA: 0x000FBDEF File Offset: 0x000F9FEF
			public override WebResponse GetResponse()
			{
				return this.WrapResponse(new Func<WebResponse>(base.GetResponse));
			}

			// Token: 0x06004C20 RID: 19488 RVA: 0x000FBE04 File Offset: 0x000FA004
			private MashupHttpWebResponse WrapResponse(Func<WebResponse> getResponse)
			{
				return this.WrapExceptionResponse<MashupHttpWebResponse>(delegate
				{
					MashupHttpWebResponse mashupHttpWebResponse = (MashupHttpWebResponse)getResponse();
					Uri uri;
					if (this.uriRewritingService.TryRewriteResponseUri(mashupHttpWebResponse.ResponseUri, out uri))
					{
						return new WebRequestFactory.RewrittenHttpWebResponse(mashupHttpWebResponse, uri);
					}
					return mashupHttpWebResponse;
				});
			}

			// Token: 0x06004C21 RID: 19489 RVA: 0x000FBE38 File Offset: 0x000FA038
			private T WrapExceptionResponse<T>(Func<T> getValue)
			{
				T t;
				try
				{
					t = getValue();
				}
				catch (WebException ex)
				{
					MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
					Uri uri;
					if (mashupHttpWebResponse != null && this.uriRewritingService.TryRewriteResponseUri(mashupHttpWebResponse.ResponseUri, out uri))
					{
						mashupHttpWebResponse = new WebRequestFactory.RewrittenHttpWebResponse(mashupHttpWebResponse, uri);
						WebException ex2 = new WebException(ex.Message, ex.InnerException, ex.Status, mashupHttpWebResponse);
						foreach (object obj in ex.Data.Keys)
						{
							string text = (string)obj;
							ex2.Data[text] = ex.Data[text];
						}
						ex2.HelpLink = ex.HelpLink;
						ex2.Source = ex.Source;
						throw ex2;
					}
					throw;
				}
				return t;
			}

			// Token: 0x04002861 RID: 10337
			private readonly IHttpUriRewritingService uriRewritingService;

			// Token: 0x04002862 RID: 10338
			private readonly Uri originalUri;
		}

		// Token: 0x02000AA5 RID: 2725
		private sealed class RewrittenHttpWebResponse : DelegatingHttpWebResponse
		{
			// Token: 0x06004C31 RID: 19505 RVA: 0x000FBFFB File Offset: 0x000FA1FB
			public RewrittenHttpWebResponse(MashupHttpWebResponse response, Uri rewrittenUri)
				: base(response)
			{
				this.rewrittenUri = rewrittenUri;
			}

			// Token: 0x170017F5 RID: 6133
			// (get) Token: 0x06004C32 RID: 19506 RVA: 0x000FC00B File Offset: 0x000FA20B
			public override Uri ResponseUri
			{
				get
				{
					return this.rewrittenUri;
				}
			}

			// Token: 0x0400286E RID: 10350
			private readonly Uri rewrittenUri;
		}
	}
}
