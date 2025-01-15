using System;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.TelemetryCore.OpenTelemetry
{
	// Token: 0x020001E7 RID: 487
	internal class NullOtelInstrumentation : IOtelInstrumentation
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x00045CEF File Offset: 0x00043EEF
		public void LogSuccessMetrics(string platform, ApiEvent.ApiIds apiId, CacheLevel cacheLevel, long totalDurationInUs, AuthenticationResultMetadata authResultMetadata, ILoggerAdapter logger)
		{
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00045CF1 File Offset: 0x00043EF1
		public void LogFailureMetrics(string platform, string errorCode, ApiEvent.ApiIds apiId, CacheRefreshReason cacheRefreshReason)
		{
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00045CF3 File Offset: 0x00043EF3
		void IOtelInstrumentation.IncrementSuccessCounter(string platform, ApiEvent.ApiIds apiId, TokenSource tokenSource, CacheRefreshReason cacheRefreshReason, CacheLevel cacheLevel, ILoggerAdapter logger)
		{
		}
	}
}
