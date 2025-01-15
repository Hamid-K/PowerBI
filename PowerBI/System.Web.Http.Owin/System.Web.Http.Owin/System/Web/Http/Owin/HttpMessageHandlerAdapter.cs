using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin.ExceptionHandling;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000010 RID: 16
	public class HttpMessageHandlerAdapter : OwinMiddleware, IDisposable
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002C78 File Offset: 0x00000E78
		public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandlerOptions options)
			: base(next)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this._messageHandler = options.MessageHandler;
			if (this._messageHandler == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"MessageHandler"
				}), "options");
			}
			this._messageInvoker = new HttpMessageInvoker(this._messageHandler);
			this._bufferPolicySelector = options.BufferPolicySelector;
			if (this._bufferPolicySelector == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"BufferPolicySelector"
				}), "options");
			}
			this._exceptionLogger = options.ExceptionLogger;
			if (this._exceptionLogger == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"ExceptionLogger"
				}), "options");
			}
			this._exceptionHandler = options.ExceptionHandler;
			if (this._exceptionHandler == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"ExceptionHandler"
				}), "options");
			}
			this._appDisposing = options.AppDisposing;
			if (this._appDisposing.CanBeCanceled)
			{
				this._appDisposing.Register(new Action(this.OnAppDisposing));
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E00 File Offset: 0x00001000
		[Obsolete("Use the HttpMessageHandlerAdapter(OwinMiddleware, HttpMessageHandlerOptions) constructor instead.")]
		public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector)
			: this(next, HttpMessageHandlerAdapter.CreateOptions(messageHandler, bufferPolicySelector))
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002E10 File Offset: 0x00001010
		public HttpMessageHandler MessageHandler
		{
			get
			{
				return this._messageHandler;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002E18 File Offset: 0x00001018
		public IHostBufferPolicySelector BufferPolicySelector
		{
			get
			{
				return this._bufferPolicySelector;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002E20 File Offset: 0x00001020
		public IExceptionLogger ExceptionLogger
		{
			get
			{
				return this._exceptionLogger;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002E28 File Offset: 0x00001028
		public IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._exceptionHandler;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002E30 File Offset: 0x00001030
		public CancellationToken AppDisposing
		{
			get
			{
				return this._appDisposing;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002E38 File Offset: 0x00001038
		public override Task Invoke(IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IOwinRequest request = context.Request;
			IOwinResponse response = context.Response;
			if (request == null)
			{
				throw Error.InvalidOperation(OwinResources.OwinContext_NullRequest, new object[0]);
			}
			if (response == null)
			{
				throw Error.InvalidOperation(OwinResources.OwinContext_NullResponse, new object[0]);
			}
			return this.InvokeCore(context, request, response);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002E94 File Offset: 0x00001094
		private async Task InvokeCore(IOwinContext context, IOwinRequest owinRequest, IOwinResponse owinResponse)
		{
			CancellationToken cancellationToken = owinRequest.CallCancelled;
			bool flag = this._bufferPolicySelector.UseBufferedInputStream(context);
			if (!flag)
			{
				owinRequest.DisableBuffering();
			}
			HttpContent httpContent;
			if (!owinRequest.Body.CanSeek && flag)
			{
				httpContent = await HttpMessageHandlerAdapter.CreateBufferedRequestContentAsync(owinRequest, cancellationToken);
			}
			else
			{
				httpContent = HttpMessageHandlerAdapter.CreateStreamedRequestContent(owinRequest);
			}
			HttpRequestMessage request = HttpMessageHandlerAdapter.CreateRequestMessage(owinRequest, httpContent);
			HttpMessageHandlerAdapter.MapRequestProperties(request, context);
			HttpMessageHandlerAdapter.SetPrincipal(owinRequest.User);
			HttpResponseMessage response = null;
			bool callNext;
			try
			{
				response = await this._messageInvoker.SendAsync(request, cancellationToken);
				if (response == null)
				{
					throw Error.InvalidOperation(OwinResources.SendAsync_ReturnedNull, new object[0]);
				}
				if (HttpMessageHandlerAdapter.IsSoftNotFound(request, response))
				{
					callNext = true;
				}
				else
				{
					callNext = false;
					bool flag2 = response.Content == null;
					if (!flag2)
					{
						flag2 = await this.ComputeContentLengthAsync(request, response, owinResponse, cancellationToken);
					}
					if (flag2)
					{
						if (!this._bufferPolicySelector.UseBufferedOutputStream(response))
						{
							owinResponse.DisableBuffering();
						}
						else if (response.Content != null)
						{
							response = await this.BufferResponseContentAsync(request, response, cancellationToken);
						}
						TaskAwaiter<bool> taskAwaiter = this.PrepareHeadersAsync(request, response, owinResponse, cancellationToken).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (taskAwaiter.GetResult())
						{
							await this.SendResponseMessageAsync(request, response, owinResponse, cancellationToken);
						}
					}
				}
			}
			finally
			{
				request.DisposeRequestResources();
				request.Dispose();
				if (response != null)
				{
					response.Dispose();
				}
			}
			if (callNext && base.Next != null)
			{
				await base.Next.Invoke(context);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002EF1 File Offset: 0x000010F1
		private static HttpContent CreateStreamedRequestContent(IOwinRequest owinRequest)
		{
			return new StreamContent(new NonOwnedStream(owinRequest.Body));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002F04 File Offset: 0x00001104
		private static async Task<HttpContent> CreateBufferedRequestContentAsync(IOwinRequest owinRequest, CancellationToken cancellationToken)
		{
			int? contentLength = owinRequest.GetContentLength();
			MemoryStream buffer;
			if (contentLength == null)
			{
				buffer = new MemoryStream();
			}
			else
			{
				buffer = new MemoryStream(contentLength.Value);
			}
			cancellationToken.ThrowIfCancellationRequested();
			using (StreamContent copier = new StreamContent(owinRequest.Body))
			{
				await copier.CopyToAsync(buffer);
			}
			StreamContent copier = null;
			buffer.Position = 0L;
			owinRequest.Body = buffer;
			return new ByteArrayContent(buffer.GetBuffer(), 0, (int)buffer.Length);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002F54 File Offset: 0x00001154
		private static HttpRequestMessage CreateRequestMessage(IOwinRequest owinRequest, HttpContent requestContent)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod(owinRequest.Method), owinRequest.Uri);
			try
			{
				httpRequestMessage.Content = requestContent;
				foreach (KeyValuePair<string, string[]> keyValuePair in owinRequest.Headers)
				{
					if (!httpRequestMessage.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
					{
						requestContent.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			catch
			{
				httpRequestMessage.Dispose();
				throw;
			}
			return httpRequestMessage;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003008 File Offset: 0x00001208
		private static void MapRequestProperties(HttpRequestMessage request, IOwinContext context)
		{
			request.SetOwinContext(context);
			HttpRequestContext httpRequestContext = new OwinHttpRequestContext(context, request);
			request.SetRequestContext(httpRequestContext);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000302B File Offset: 0x0000122B
		private static void SetPrincipal(IPrincipal user)
		{
			if (user != null)
			{
				Thread.CurrentPrincipal = user;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003038 File Offset: 0x00001238
		private static bool IsSoftNotFound(HttpRequestMessage request, HttpResponseMessage response)
		{
			bool flag;
			return response.StatusCode == HttpStatusCode.NotFound && (request.Properties.TryGetValue(HttpPropertyKeys.NoRouteMatched, out flag) && flag);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000306C File Offset: 0x0000126C
		private async Task<HttpResponseMessage> BufferResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				await response.Content.LoadIntoBufferAsync();
				return response;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(ex);
			}
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferContent, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage httpResponseMessage = await this._exceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			response.Dispose();
			HttpResponseMessage httpResponseMessage2;
			if (httpResponseMessage == null)
			{
				exceptionInfo.Throw();
				httpResponseMessage2 = null;
			}
			else
			{
				response = httpResponseMessage;
				cancellationToken.ThrowIfCancellationRequested();
				try
				{
					await response.Content.LoadIntoBufferAsync();
					return response;
				}
				catch (OperationCanceledException)
				{
					throw;
				}
				catch (Exception ex2)
				{
				}
				Exception ex2;
				ExceptionContext exceptionContext2 = new ExceptionContext(ex2, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferError, request, response);
				await this._exceptionLogger.LogAsync(exceptionContext2, cancellationToken);
				response.Dispose();
				httpResponseMessage2 = request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			return httpResponseMessage2;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000030CC File Offset: 0x000012CC
		private Task<bool> PrepareHeadersAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			HttpResponseHeaders headers = response.Headers;
			HttpContent content = response.Content;
			bool? transferEncodingChunked = headers.TransferEncodingChunked;
			bool flag = true;
			bool flag2 = (transferEncodingChunked.GetValueOrDefault() == flag) & (transferEncodingChunked != null);
			HttpHeaderValueCollection<TransferCodingHeaderValue> transferEncoding = headers.TransferEncoding;
			if (content != null)
			{
				HttpContentHeaders headers2 = content.Headers;
				if (!flag2)
				{
					return this.ComputeContentLengthAsync(request, response, owinResponse, cancellationToken);
				}
				headers2.ContentLength = null;
			}
			if (flag2 && transferEncoding.Count == 1)
			{
				transferEncoding.Clear();
			}
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003150 File Offset: 0x00001350
		private Task<bool> ComputeContentLengthAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			HttpResponseHeaders headers = response.Headers;
			HttpContentHeaders headers2 = response.Content.Headers;
			try
			{
				long? contentLength = headers2.ContentLength;
				return Task.FromResult<bool>(true);
			}
			catch (Exception ex)
			{
			}
			Exception ex;
			return this.HandleTryComputeLengthExceptionAsync(ex, request, response, owinResponse, cancellationToken);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000031A4 File Offset: 0x000013A4
		private async Task<bool> HandleTryComputeLengthExceptionAsync(Exception exception, HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			ExceptionContext exceptionContext = new ExceptionContext(exception, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterComputeContentLength, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			owinResponse.StatusCode = 500;
			HttpMessageHandlerAdapter.SetHeadersForEmptyResponse(owinResponse.Headers);
			return false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003214 File Offset: 0x00001414
		private Task SendResponseMessageAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			owinResponse.StatusCode = (int)response.StatusCode;
			owinResponse.ReasonPhrase = response.ReasonPhrase;
			IDictionary<string, string[]> headers = owinResponse.Headers;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in response.Headers)
			{
				headers[keyValuePair.Key] = keyValuePair.Value.AsArray<string>();
			}
			HttpContent content = response.Content;
			if (content == null)
			{
				HttpMessageHandlerAdapter.SetHeadersForEmptyResponse(headers);
				return TaskHelpers.Completed();
			}
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in content.Headers)
			{
				headers[keyValuePair2.Key] = keyValuePair2.Value.AsArray<string>();
			}
			return this.SendResponseContentAsync(request, response, owinResponse, cancellationToken);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003304 File Offset: 0x00001504
		private static void SetHeadersForEmptyResponse(IDictionary<string, string[]> headers)
		{
			headers["Content-Length"] = new string[] { "0" };
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003320 File Offset: 0x00001520
		private async Task SendResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			try
			{
				await response.Content.CopyToAsync(owinResponse.Body);
				return;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
			}
			Exception ex;
			ExceptionContext exceptionContext = new ExceptionContext(ex, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterStreamContent, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			await HttpMessageHandlerAdapter.AbortResponseAsync();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003386 File Offset: 0x00001586
		private static Task AbortResponseAsync()
		{
			return TaskHelpers.Canceled();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003390 File Offset: 0x00001590
		private static HttpMessageHandlerOptions CreateOptions(HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector)
		{
			if (messageHandler == null)
			{
				throw new ArgumentNullException("messageHandler");
			}
			if (bufferPolicySelector == null)
			{
				throw new ArgumentNullException("bufferPolicySelector");
			}
			return new HttpMessageHandlerOptions
			{
				MessageHandler = messageHandler,
				BufferPolicySelector = bufferPolicySelector,
				ExceptionLogger = new EmptyExceptionLogger(),
				ExceptionHandler = new DefaultExceptionHandler(),
				AppDisposing = CancellationToken.None
			};
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000033ED File Offset: 0x000015ED
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.OnAppDisposing();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000033F8 File Offset: 0x000015F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003407 File Offset: 0x00001607
		private void OnAppDisposing()
		{
			if (!this._disposed)
			{
				this._messageInvoker.Dispose();
				this._disposed = true;
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly HttpMessageHandler _messageHandler;

		// Token: 0x0400001B RID: 27
		private readonly HttpMessageInvoker _messageInvoker;

		// Token: 0x0400001C RID: 28
		private readonly IHostBufferPolicySelector _bufferPolicySelector;

		// Token: 0x0400001D RID: 29
		private readonly IExceptionLogger _exceptionLogger;

		// Token: 0x0400001E RID: 30
		private readonly IExceptionHandler _exceptionHandler;

		// Token: 0x0400001F RID: 31
		private readonly CancellationToken _appDisposing;

		// Token: 0x04000020 RID: 32
		private bool _disposed;
	}
}
