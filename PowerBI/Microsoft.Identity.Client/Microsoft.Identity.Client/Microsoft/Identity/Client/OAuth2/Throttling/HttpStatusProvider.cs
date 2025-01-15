using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000213 RID: 531
	internal class HttpStatusProvider : IThrottlingProvider
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x000491FC File Offset: 0x000473FC
		internal ThrottlingCache ThrottlingCache { get; }

		// Token: 0x06001621 RID: 5665 RVA: 0x00049204 File Offset: 0x00047404
		public HttpStatusProvider()
		{
			this.ThrottlingCache = new ThrottlingCache(null);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004922C File Offset: 0x0004742C
		public void RecordException(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams, MsalServiceException ex)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			TimeSpan timeSpan;
			if (HttpStatusProvider.IsRequestSupported(requestParams) && (ex.StatusCode == 429 || (ex.StatusCode >= 500 && ex.StatusCode < 600)) && !RetryAfterProvider.TryGetRetryAfterValue(ex.Headers, out timeSpan))
			{
				logger.Info(() => string.Format("[Throttling] HTTP status code {0} encountered - ", ex.StatusCode) + string.Format("throttling for {0} seconds. ", HttpStatusProvider.s_throttleDuration.TotalSeconds));
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
				ThrottlingCacheEntry throttlingCacheEntry = new ThrottlingCacheEntry(ex, HttpStatusProvider.s_throttleDuration);
				this.ThrottlingCache.AddAndCleanup(requestStrictThumbprint, throttlingCacheEntry, logger);
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00049312 File Offset: 0x00047512
		public void ResetCache()
		{
			this.ThrottlingCache.Clear();
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00049320 File Offset: 0x00047520
		public void TryThrottle(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams)
		{
			if (!this.ThrottlingCache.IsEmpty() && HttpStatusProvider.IsRequestSupported(requestParams))
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
				ThrottleCommon.TryThrowServiceException(ThrottleCommon.GetRequestStrictThumbprint(bodyParams, text, text2), this.ThrottlingCache, logger, "HttpStatusProvider");
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00049393 File Offset: 0x00047593
		private static bool IsRequestSupported(AuthenticationRequestParameters requestParameters)
		{
			return !requestParameters.AppConfig.IsConfidentialClient;
		}

		// Token: 0x04000966 RID: 2406
		internal static readonly TimeSpan s_throttleDuration = TimeSpan.FromSeconds(60.0);
	}
}
