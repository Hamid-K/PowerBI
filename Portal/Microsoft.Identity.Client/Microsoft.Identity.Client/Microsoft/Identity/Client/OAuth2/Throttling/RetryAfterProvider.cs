using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000215 RID: 533
	internal class RetryAfterProvider : IThrottlingProvider
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000493B8 File Offset: 0x000475B8
		internal ThrottlingCache ThrottlingCache { get; }

		// Token: 0x0600162B RID: 5675 RVA: 0x000493C0 File Offset: 0x000475C0
		public RetryAfterProvider()
		{
			this.ThrottlingCache = new ThrottlingCache(null);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000493E8 File Offset: 0x000475E8
		public void RecordException(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams, MsalServiceException ex)
		{
			TimeSpan retryAfterTimespan;
			if (RetryAfterProvider.TryGetRetryAfterValue(ex.Headers, out retryAfterTimespan))
			{
				retryAfterTimespan = RetryAfterProvider.GetSafeValue(retryAfterTimespan);
				ILoggerAdapter logger = requestParams.RequestContext.Logger;
				logger.Info(() => "[Throttling] Retry-After header detected, " + string.Format("value: {0} seconds", retryAfterTimespan.TotalSeconds));
				string text = requestParams.AuthorityInfo.CanonicalAuthority.ToString();
				IAccount account = requestParams.Account;
				string text2;
				if (account == null)
				{
					text2 = null;
				}
				else
				{
					AccountId homeAccountId = account.HomeAccountId;
					text2 = ((homeAccountId != null) ? homeAccountId.Identifier : null);
				}
				string requestStrictThumbprint = ThrottleCommon.GetRequestStrictThumbprint(bodyParams, text, text2);
				ThrottlingCacheEntry throttlingCacheEntry = new ThrottlingCacheEntry(ex, retryAfterTimespan);
				this.ThrottlingCache.AddAndCleanup(requestStrictThumbprint, throttlingCacheEntry, logger);
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004948D File Offset: 0x0004768D
		public void ResetCache()
		{
			this.ThrottlingCache.Clear();
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004949C File Offset: 0x0004769C
		public void TryThrottle(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams)
		{
			if (!this.ThrottlingCache.IsEmpty())
			{
				ILoggerAdapter logger = requestParams.RequestContext.Logger;
				string text = requestParams.AuthorityInfo.CanonicalAuthority.ToString();
				IAccount account = requestParams.Account;
				string text2;
				if (account == null)
				{
					text2 = null;
				}
				else
				{
					AccountId homeAccountId = account.HomeAccountId;
					text2 = ((homeAccountId != null) ? homeAccountId.Identifier : null);
				}
				ThrottleCommon.TryThrowServiceException(ThrottleCommon.GetRequestStrictThumbprint(bodyParams, text, text2), this.ThrottlingCache, logger, "RetryAfterProvider");
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00049508 File Offset: 0x00047708
		public static bool TryGetRetryAfterValue(HttpResponseHeaders headers, out TimeSpan retryAfterTimespan)
		{
			retryAfterTimespan = TimeSpan.Zero;
			DateTimeOffset? dateTimeOffset;
			if (headers == null)
			{
				dateTimeOffset = null;
			}
			else
			{
				RetryConditionHeaderValue retryAfter = headers.RetryAfter;
				dateTimeOffset = ((retryAfter != null) ? retryAfter.Date : null);
			}
			DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
			if (dateTimeOffset2 != null)
			{
				retryAfterTimespan = dateTimeOffset2.Value - DateTimeOffset.Now;
				return true;
			}
			TimeSpan? timeSpan;
			if (headers == null)
			{
				timeSpan = null;
			}
			else
			{
				RetryConditionHeaderValue retryAfter2 = headers.RetryAfter;
				timeSpan = ((retryAfter2 != null) ? retryAfter2.Delta : null);
			}
			TimeSpan? timeSpan2 = timeSpan;
			if (timeSpan2 != null)
			{
				retryAfterTimespan = timeSpan2.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000495AD File Offset: 0x000477AD
		private static TimeSpan GetSafeValue(TimeSpan headerValue)
		{
			if (headerValue > RetryAfterProvider.MaxRetryAfter)
			{
				return RetryAfterProvider.MaxRetryAfter;
			}
			return headerValue;
		}

		// Token: 0x04000969 RID: 2409
		internal static readonly TimeSpan MaxRetryAfter = TimeSpan.FromSeconds(3600.0);
	}
}
