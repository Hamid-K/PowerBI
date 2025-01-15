using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x02000288 RID: 648
	internal class HttpManager : IHttpManager
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00052406 File Offset: 0x00050606
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0005240E File Offset: 0x0005060E
		public long LastRequestDurationInMs { get; private set; }

		// Token: 0x060018EC RID: 6380 RVA: 0x00052417 File Offset: 0x00050617
		public HttpManager(IMsalHttpClientFactory httpClientFactory)
		{
			if (httpClientFactory == null)
			{
				throw new ArgumentNullException("httpClientFactory");
			}
			this._httpClientFactory = httpClientFactory;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00052435 File Offset: 0x00050635
		protected virtual HttpClient GetHttpClient()
		{
			return this._httpClientFactory.GetHttpClient();
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00052444 File Offset: 0x00050644
		public async Task<HttpResponse> SendPostAsync(Uri endpoint, IDictionary<string, string> headers, IDictionary<string, string> bodyParameters, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpContent httpContent = ((bodyParameters == null) ? null : new FormUrlEncodedContent(bodyParameters));
			return await this.SendPostAsync(endpoint, headers, httpContent, logger, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000524B4 File Offset: 0x000506B4
		public virtual Task<HttpResponse> SendPostAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, body, HttpMethod.Post, logger, false, false, cancellationToken);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000524D8 File Offset: 0x000506D8
		public virtual Task<HttpResponse> SendGetAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, null, HttpMethod.Get, logger, false, false, cancellationToken);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000524F8 File Offset: 0x000506F8
		public virtual Task<HttpResponse> SendGetForceResponseAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, null, HttpMethod.Get, logger, true, false, cancellationToken);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00052518 File Offset: 0x00050718
		public virtual Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, IDictionary<string, string> bodyParameters, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpContent httpContent = ((bodyParameters == null) ? null : new FormUrlEncodedContent(bodyParameters));
			return this.SendRequestAsync(uri, headers, httpContent, HttpMethod.Post, logger, true, false, cancellationToken);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00052548 File Offset: 0x00050748
		public virtual Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, StringContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(uri, headers, body, HttpMethod.Post, logger, true, false, cancellationToken);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005256C File Offset: 0x0005076C
		private static HttpRequestMessage CreateRequestMessage(Uri endpoint, IDictionary<string, string> headers)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage
			{
				RequestUri = endpoint
			};
			httpRequestMessage.Headers.Accept.Clear();
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					httpRequestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return httpRequestMessage;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000525E8 File Offset: 0x000507E8
		protected virtual async Task<HttpResponse> SendRequestAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, HttpMethod method, ILoggerAdapter logger, bool doNotThrow = false, bool retry = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpResponse response = null;
			try
			{
				HttpContent httpContent = body;
				if (body != null)
				{
					httpContent = await HttpManager.CloneHttpContentAsync(body).ConfigureAwait(false);
				}
				using (logger.LogBlockDuration("[HttpManager] ExecuteAsync", LogLevel.Verbose))
				{
					response = await this.ExecuteAsync(endpoint, headers, httpContent, method, logger, cancellationToken).ConfigureAwait(false);
				}
				DurationLogHelper durationLogHelper = null;
				if (response.StatusCode == HttpStatusCode.OK)
				{
					return response;
				}
				logger.Info(() => string.Format(CultureInfo.InvariantCulture, "Response status code does not indicate success: {0} ({1}). ", (int)response.StatusCode, response.StatusCode));
			}
			catch (TaskCanceledException ex)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					logger.Info("The HTTP request was cancelled. ");
					throw;
				}
				logger.Error("The HTTP request failed. " + ex.Message);
				throw new MsalServiceException("request_timeout", "Request to the endpoint timed out.", ex);
			}
			HttpResponse httpResponse;
			if (doNotThrow)
			{
				httpResponse = response;
			}
			else
			{
				if (this.IsRetryableStatusCode((int)response.StatusCode))
				{
					throw MsalServiceExceptionFactory.FromHttpResponse("service_not_available", "Service is unavailable to process the request", response, null);
				}
				httpResponse = response;
			}
			return httpResponse;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00052668 File Offset: 0x00050868
		protected async Task<HttpResponse> ExecuteAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, HttpMethod method, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpManager.<>c__DisplayClass15_0 CS$<>8__locals1 = new HttpManager.<>c__DisplayClass15_0();
			CS$<>8__locals1.method = method;
			CS$<>8__locals1.endpoint = endpoint;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			HttpManager.<>c__DisplayClass15_1 CS$<>8__locals2 = new HttpManager.<>c__DisplayClass15_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.requestMessage = HttpManager.CreateRequestMessage(CS$<>8__locals2.CS$<>8__locals1.endpoint, headers);
			HttpResponse httpResponse;
			try
			{
				CS$<>8__locals2.requestMessage.Method = CS$<>8__locals2.CS$<>8__locals1.method;
				CS$<>8__locals2.requestMessage.Content = body;
				logger.VerbosePii(() => string.Format("[HttpManager] Sending request. Method: {0}. URI: {1}. ", CS$<>8__locals2.CS$<>8__locals1.method, (CS$<>8__locals2.CS$<>8__locals1.endpoint == null) ? "NULL" : (CS$<>8__locals2.CS$<>8__locals1.endpoint.Scheme + "://" + CS$<>8__locals2.CS$<>8__locals1.endpoint.Authority + CS$<>8__locals2.CS$<>8__locals1.endpoint.AbsolutePath)), () => string.Format("[HttpManager] Sending request. Method: {0}. Host: {1}. ", CS$<>8__locals2.CS$<>8__locals1.method, (CS$<>8__locals2.CS$<>8__locals1.endpoint == null) ? "NULL" : (CS$<>8__locals2.CS$<>8__locals1.endpoint.Scheme + "://" + CS$<>8__locals2.CS$<>8__locals1.endpoint.Authority)));
				MeasureDurationResult<HttpResponseMessage> measureDurationResult = await StopwatchService.MeasureCodeBlockAsync<HttpResponseMessage>(delegate
				{
					HttpManager.<>c__DisplayClass15_1.<<ExecuteAsync>b__2>d <<ExecuteAsync>b__2>d;
					<<ExecuteAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
					<<ExecuteAsync>b__2>d.<>4__this = CS$<>8__locals2;
					<<ExecuteAsync>b__2>d.<>1__state = -1;
					<<ExecuteAsync>b__2>d.<>t__builder.Start<HttpManager.<>c__DisplayClass15_1.<<ExecuteAsync>b__2>d>(ref <<ExecuteAsync>b__2>d);
					return <<ExecuteAsync>b__2>d.<>t__builder.Task;
				}).ConfigureAwait(false);
				using (HttpResponseMessage responseMessage = measureDurationResult.Result)
				{
					this.LastRequestDurationInMs = measureDurationResult.Milliseconds;
					logger.Verbose(() => string.Format("[HttpManager] Received response. Status code: {0}. ", responseMessage.StatusCode));
					object obj = await HttpManager.CreateResponseAsync(responseMessage).ConfigureAwait(false);
					obj.UserAgent = CS$<>8__locals2.requestMessage.Headers.UserAgent.ToString();
					httpResponse = obj;
				}
			}
			finally
			{
				if (CS$<>8__locals2.requestMessage != null)
				{
					((IDisposable)CS$<>8__locals2.requestMessage).Dispose();
				}
			}
			return httpResponse;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000526E0 File Offset: 0x000508E0
		internal static async Task<HttpResponse> CreateResponseAsync(HttpResponseMessage response)
		{
			string text;
			if (response.Content == null)
			{
				text = null;
			}
			else
			{
				text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
			string text2 = text;
			return new HttpResponse
			{
				Headers = response.Headers,
				Body = text2,
				StatusCode = response.StatusCode
			};
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00052724 File Offset: 0x00050924
		protected static async Task<HttpContent> CloneHttpContentAsync(HttpContent httpContent)
		{
			MemoryStream temp = new MemoryStream();
			await httpContent.CopyToAsync(temp).ConfigureAwait(false);
			temp.Position = 0L;
			StreamContent streamContent = new StreamContent(temp);
			if (httpContent.Headers != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in httpContent.Headers)
				{
					streamContent.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return streamContent;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00052767 File Offset: 0x00050967
		protected virtual bool IsRetryableStatusCode(int statusCode)
		{
			return statusCode >= 500 && statusCode < 600;
		}

		// Token: 0x04000B46 RID: 2886
		protected readonly IMsalHttpClientFactory _httpClientFactory;
	}
}
