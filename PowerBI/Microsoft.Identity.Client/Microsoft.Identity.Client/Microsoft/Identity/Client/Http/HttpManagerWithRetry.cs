using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Logger;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x0200028B RID: 651
	internal class HttpManagerWithRetry : HttpManager
	{
		// Token: 0x060018FE RID: 6398 RVA: 0x000527E1 File Offset: 0x000509E1
		public HttpManagerWithRetry(IMsalHttpClientFactory httpClientFactory)
			: base(httpClientFactory)
		{
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000527EC File Offset: 0x000509EC
		public override Task<HttpResponse> SendPostAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, body, HttpMethod.Post, logger, false, true, cancellationToken);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00052810 File Offset: 0x00050A10
		public override Task<HttpResponse> SendGetAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, null, HttpMethod.Get, logger, false, true, cancellationToken);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00052830 File Offset: 0x00050A30
		public override Task<HttpResponse> SendGetForceResponseAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(endpoint, headers, null, HttpMethod.Get, logger, true, true, cancellationToken);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00052850 File Offset: 0x00050A50
		public override Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, IDictionary<string, string> bodyParameters, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpContent httpContent = ((bodyParameters == null) ? null : new FormUrlEncodedContent(bodyParameters));
			return this.SendRequestAsync(uri, headers, httpContent, HttpMethod.Post, logger, true, true, cancellationToken);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00052880 File Offset: 0x00050A80
		public override Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, StringContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SendRequestAsync(uri, headers, body, HttpMethod.Post, logger, true, true, cancellationToken);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000528A1 File Offset: 0x00050AA1
		protected override HttpClient GetHttpClient()
		{
			return this._httpClientFactory.GetHttpClient();
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000528B0 File Offset: 0x00050AB0
		protected override async Task<HttpResponse> SendRequestAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, HttpMethod method, ILoggerAdapter logger, bool doNotThrow = false, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			Exception timeoutException = null;
			bool isRetriableStatusCode = false;
			HttpResponse response = null;
			bool flag;
			try
			{
				HttpContent httpContent = body;
				if (body != null)
				{
					httpContent = await HttpManager.CloneHttpContentAsync(body).ConfigureAwait(false);
				}
				using (logger.LogBlockDuration("[HttpManager] ExecuteAsync", LogLevel.Verbose))
				{
					response = await base.ExecuteAsync(endpoint, headers, httpContent, method, logger, cancellationToken).ConfigureAwait(false);
				}
				DurationLogHelper durationLogHelper = null;
				if (response.StatusCode == HttpStatusCode.OK)
				{
					return response;
				}
				logger.Info(() => string.Format(CultureInfo.InvariantCulture, "Response status code does not indicate success: {0} ({1}). ", (int)response.StatusCode, response.StatusCode));
				isRetriableStatusCode = this.IsRetryableStatusCode((int)response.StatusCode);
				flag = isRetriableStatusCode && !HttpManagerWithRetry.HasRetryAfterHeader(response);
			}
			catch (TaskCanceledException ex)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					logger.Info("The HTTP request was cancelled. ");
					throw;
				}
				logger.Error("The HTTP request failed. " + ex.Message);
				flag = true;
				timeoutException = ex;
			}
			HttpResponse httpResponse;
			if (flag && retry)
			{
				logger.Info("Retrying one more time..");
				await Task.Delay(TimeSpan.FromSeconds(1.0), cancellationToken).ConfigureAwait(false);
				httpResponse = await this.SendRequestAsync(endpoint, headers, body, method, logger, doNotThrow, false, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				logger.Warning("Request retry failed.");
				if (timeoutException != null)
				{
					throw new MsalServiceException("request_timeout", "Request to the endpoint timed out.", timeoutException);
				}
				if (doNotThrow)
				{
					httpResponse = response;
				}
				else
				{
					if (isRetriableStatusCode)
					{
						throw MsalServiceExceptionFactory.FromHttpResponse("service_not_available", "Service is unavailable to process the request", response, null);
					}
					httpResponse = response;
				}
			}
			return httpResponse;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00052938 File Offset: 0x00050B38
		private static bool HasRetryAfterHeader(HttpResponse response)
		{
			RetryConditionHeaderValue retryConditionHeaderValue;
			if (response == null)
			{
				retryConditionHeaderValue = null;
			}
			else
			{
				HttpResponseHeaders headers = response.Headers;
				retryConditionHeaderValue = ((headers != null) ? headers.RetryAfter : null);
			}
			RetryConditionHeaderValue retryConditionHeaderValue2 = retryConditionHeaderValue;
			return retryConditionHeaderValue2 != null && (retryConditionHeaderValue2.Delta != null || retryConditionHeaderValue2.Date != null);
		}
	}
}
