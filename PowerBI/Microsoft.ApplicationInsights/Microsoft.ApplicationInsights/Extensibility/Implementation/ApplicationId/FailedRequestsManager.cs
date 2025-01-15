using System;
using System.Collections.Concurrent;
using System.Net;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.ApplicationId
{
	// Token: 0x020000C7 RID: 199
	internal class FailedRequestsManager
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00017A32 File Offset: 0x00015C32
		internal FailedRequestsManager()
		{
			this.retryWaitTime = TimeSpan.FromSeconds(30.0);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00017A59 File Offset: 0x00015C59
		internal FailedRequestsManager(TimeSpan retryWaitTime)
		{
			this.retryWaitTime = retryWaitTime;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017A73 File Offset: 0x00015C73
		public void RegisterFetchFailure(string instrumentationKey, HttpStatusCode httpStatusCode)
		{
			this.failingInstrumentationKeys.TryAdd(instrumentationKey, new FailedRequestsManager.FailedResult(this.retryWaitTime, httpStatusCode));
			CoreEventSource.Log.ApplicationIdProviderFetchApplicationIdFailedWithResponseCode(httpStatusCode.ToString(), "Incorrect");
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00017AAC File Offset: 0x00015CAC
		public void RegisterFetchFailure(string instrumentationKey, Exception ex)
		{
			AggregateException ex2;
			WebException ex3;
			HttpWebResponse httpWebResponse;
			if ((ex2 = ex as AggregateException) != null)
			{
				Exception innerException = ex2.Flatten().InnerException;
				if (innerException != null)
				{
					this.RegisterFetchFailure(instrumentationKey, innerException);
					return;
				}
			}
			else if ((ex3 = ex as WebException) != null && ex3.Response != null && (httpWebResponse = ex3.Response as HttpWebResponse) != null)
			{
				this.failingInstrumentationKeys.TryAdd(instrumentationKey, new FailedRequestsManager.FailedResult(this.retryWaitTime, httpWebResponse.StatusCode));
			}
			else
			{
				this.failingInstrumentationKeys.TryAdd(instrumentationKey, new FailedRequestsManager.FailedResult(this.retryWaitTime, HttpStatusCode.OK));
			}
			CoreEventSource.Log.ApplicationIdProviderFetchApplicationIdFailed(FailedRequestsManager.GetExceptionDetailString(ex), "Incorrect");
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017B50 File Offset: 0x00015D50
		public bool CanRetry(string instrumentationKey)
		{
			FailedRequestsManager.FailedResult failedResult;
			if (!this.failingInstrumentationKeys.TryGetValue(instrumentationKey, out failedResult))
			{
				return true;
			}
			if (failedResult.CanRetry())
			{
				FailedRequestsManager.FailedResult failedResult2;
				this.failingInstrumentationKeys.TryRemove(instrumentationKey, out failedResult2);
				return true;
			}
			return false;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017B8C File Offset: 0x00015D8C
		private static string GetExceptionDetailString(Exception ex)
		{
			AggregateException ex2;
			if ((ex2 = ex as AggregateException) != null)
			{
				return ex2.Flatten().InnerException.ToInvariantString();
			}
			return ex.ToInvariantString();
		}

		// Token: 0x0400029F RID: 671
		private const int DefaultRetryWaitTimeSeconds = 30;

		// Token: 0x040002A0 RID: 672
		private readonly TimeSpan retryWaitTime;

		// Token: 0x040002A1 RID: 673
		private ConcurrentDictionary<string, FailedRequestsManager.FailedResult> failingInstrumentationKeys = new ConcurrentDictionary<string, FailedRequestsManager.FailedResult>();

		// Token: 0x02000125 RID: 293
		private class FailedResult
		{
			// Token: 0x06000930 RID: 2352 RVA: 0x0001D964 File Offset: 0x0001BB64
			public FailedResult(TimeSpan retryAfter, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
			{
				this.retryAfterTime = DateTime.UtcNow + retryAfter;
				this.shouldRetry = httpStatusCode < HttpStatusCode.BadRequest || httpStatusCode >= HttpStatusCode.InternalServerError;
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
			public bool CanRetry()
			{
				return this.shouldRetry && DateTime.UtcNow > this.retryAfterTime;
			}

			// Token: 0x04000428 RID: 1064
			private readonly DateTime retryAfterTime;

			// Token: 0x04000429 RID: 1065
			private readonly bool shouldRetry;
		}
	}
}
