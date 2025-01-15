using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000217 RID: 535
	internal static class ThrottleCommon
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0004972C File Offset: 0x0004792C
		public static string GetRequestStrictThumbprint(IReadOnlyDictionary<string, string> bodyParams, string authority, string homeAccountId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (bodyParams.TryGetValue("client_id", out text))
			{
				stringBuilder.Append(text);
				stringBuilder.Append('.');
			}
			stringBuilder.Append(authority);
			stringBuilder.Append('.');
			string text2;
			if (bodyParams.TryGetValue("scope", out text2))
			{
				stringBuilder.Append(text2);
				stringBuilder.Append('.');
			}
			stringBuilder.Append(homeAccountId);
			stringBuilder.Append('.');
			return stringBuilder.ToString();
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000497A8 File Offset: 0x000479A8
		public static void TryThrowServiceException(string thumbprint, ThrottlingCache cache, ILoggerAdapter logger, string providerName)
		{
			MsalServiceException ex;
			if (cache.TryGetOrRemoveExpired(thumbprint, logger, out ex))
			{
				logger.WarningPii("[Throttling] Exception thrown because of throttling rule " + providerName + " - thumbprint: " + thumbprint, "[Throttling] Exception thrown because of throttling rule " + providerName);
				throw new MsalThrottledServiceException(ex);
			}
		}

		// Token: 0x0400096C RID: 2412
		public const string ThrottleRetryAfterHeaderName = "x-ms-lib-capability";

		// Token: 0x0400096D RID: 2413
		public const string ThrottleRetryAfterHeaderValue = "retry-after, h429";

		// Token: 0x0400096E RID: 2414
		internal const char KeyDelimiter = '.';
	}
}
