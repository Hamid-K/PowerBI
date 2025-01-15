using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser
{
	// Token: 0x02000182 RID: 386
	internal class HttpListenerInterceptor : IUriInterceptor
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0003F4F3 File Offset: 0x0003D6F3
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0003F4FB File Offset: 0x0003D6FB
		public Action TestBeforeTopLevelCall { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0003F504 File Offset: 0x0003D704
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x0003F50C File Offset: 0x0003D70C
		public Action<string> TestBeforeStart { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0003F515 File Offset: 0x0003D715
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0003F51D File Offset: 0x0003D71D
		public Action TestBeforeGetContext { get; set; }

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003F526 File Offset: 0x0003D726
		public HttpListenerInterceptor(ILoggerAdapter logger)
		{
			this._logger = logger;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003F538 File Offset: 0x0003D738
		public async Task<Uri> ListenToSingleRequestAndRespondAsync(int port, string path, Func<Uri, MessageAndHttpCode> responseProducer, CancellationToken cancellationToken)
		{
			Action testBeforeTopLevelCall = this.TestBeforeTopLevelCall;
			if (testBeforeTopLevelCall != null)
			{
				testBeforeTopLevelCall();
			}
			cancellationToken.ThrowIfCancellationRequested();
			HttpListener httpListener = null;
			string urlToListenTo = string.Empty;
			Uri url;
			try
			{
				if (string.IsNullOrEmpty(path))
				{
					path = "/";
				}
				else
				{
					path = (path.StartsWith("/") ? path : ("/" + path));
				}
				urlToListenTo = "http://localhost:" + port.ToString() + path;
				if (!urlToListenTo.EndsWith("/"))
				{
					urlToListenTo += "/";
				}
				httpListener = new HttpListener();
				httpListener.Prefixes.Add(urlToListenTo);
				Action<string> testBeforeStart = this.TestBeforeStart;
				if (testBeforeStart != null)
				{
					testBeforeStart(urlToListenTo);
				}
				httpListener.Start();
				this._logger.Info(() => "Listening for authorization code on " + urlToListenTo);
				using (cancellationToken.Register(delegate
				{
					this._logger.Warning("HttpListener stopped because cancellation was requested.");
					HttpListenerInterceptor.TryStopListening(httpListener);
				}))
				{
					Action testBeforeGetContext = this.TestBeforeGetContext;
					if (testBeforeGetContext != null)
					{
						testBeforeGetContext();
					}
					HttpListenerContext httpListenerContext = await httpListener.GetContextAsync().ConfigureAwait(false);
					cancellationToken.ThrowIfCancellationRequested();
					this.Respond(responseProducer, httpListenerContext);
					this._logger.Verbose(() => "HttpListner received a message on " + urlToListenTo);
					url = httpListenerContext.Request.Url;
				}
			}
			catch (Exception ex) when (ex is HttpListenerException || ex is ObjectDisposedException)
			{
				this._logger.Info(() => "HttpListenerException - cancellation requested? " + cancellationToken.IsCancellationRequested.ToString());
				cancellationToken.ThrowIfCancellationRequested();
				if (ex is HttpListenerException)
				{
					throw new MsalClientException("http_listener_error", "An HttpListenerException occurred while listening on " + urlToListenTo + " for the system browser to complete the login. Possible cause and mitigation: the app is unable to listen on the specified URL; run 'netsh http add iplisten 127.0.0.1' from the Admin command prompt.", ex);
				}
				throw;
			}
			finally
			{
				HttpListenerInterceptor.TryStopListening(httpListener);
			}
			return url;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003F59C File Offset: 0x0003D79C
		private static void TryStopListening(HttpListener httpListener)
		{
			try
			{
				if (httpListener != null)
				{
					httpListener.Abort();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003F5C8 File Offset: 0x0003D7C8
		private void Respond(Func<Uri, MessageAndHttpCode> responseProducer, HttpListenerContext context)
		{
			MessageAndHttpCode messageAndCode = responseProducer(context.Request.Url);
			this._logger.Info(() => "Processing a response message to the browser. HttpStatus:" + messageAndCode.HttpCode.ToString());
			HttpStatusCode httpCode = messageAndCode.HttpCode;
			if (httpCode != HttpStatusCode.OK)
			{
				if (httpCode != HttpStatusCode.Found)
				{
					throw new NotImplementedException("HttpCode not supported" + messageAndCode.HttpCode.ToString());
				}
				context.Response.StatusCode = 302;
				context.Response.RedirectLocation = messageAndCode.Message;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(messageAndCode.Message);
				context.Response.ContentLength64 = (long)bytes.Length;
				context.Response.OutputStream.Write(bytes, 0, bytes.Length);
			}
			context.Response.OutputStream.Close();
		}

		// Token: 0x040006EF RID: 1775
		private ILoggerAdapter _logger;
	}
}
