using System;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.TelemetryCore.OpenTelemetry
{
	// Token: 0x020001E6 RID: 486
	internal interface IOtelInstrumentation
	{
		// Token: 0x060014AF RID: 5295
		void LogSuccessMetrics(string platform, ApiEvent.ApiIds apiId, CacheLevel cacheLevel, long totalDurationInUs, AuthenticationResultMetadata authResultMetadata, ILoggerAdapter logger);

		// Token: 0x060014B0 RID: 5296
		void IncrementSuccessCounter(string platform, ApiEvent.ApiIds apiId, TokenSource tokenSource, CacheRefreshReason cacheRefreshReason, CacheLevel cacheLevel, ILoggerAdapter logger);

		// Token: 0x060014B1 RID: 5297
		void LogFailureMetrics(string platform, string errorCode, ApiEvent.ApiIds apiId, CacheRefreshReason cacheRefreshReason);
	}
}
