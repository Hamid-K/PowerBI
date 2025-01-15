using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.TelemetryCore.OpenTelemetry;

namespace Microsoft.Identity.Client.Platforms.Features.OpenTelemetry
{
	// Token: 0x020001B4 RID: 436
	internal class OtelInstrumentation : IOtelInstrumentation
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00042222 File Offset: 0x00040422
		public OtelInstrumentation()
		{
			string version = OtelInstrumentation.Meter.Version;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00042238 File Offset: 0x00040438
		public void LogSuccessMetrics(string platform, ApiEvent.ApiIds apiId, CacheLevel cacheLevel, long totalDurationInUs, AuthenticationResultMetadata authResultMetadata, ILoggerAdapter logger)
		{
			this.IncrementSuccessCounter(platform, apiId, authResultMetadata.TokenSource, authResultMetadata.CacheRefreshReason, cacheLevel, logger);
			if (OtelInstrumentation.s_durationTotal.Value.Enabled)
			{
				OtelInstrumentation.s_durationTotal.Value.Record(authResultMetadata.DurationTotalInMs, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()),
					new KeyValuePair<string, object>("Platform", platform),
					new KeyValuePair<string, object>("ApiId", apiId),
					new KeyValuePair<string, object>("TokenSource", authResultMetadata.TokenSource),
					new KeyValuePair<string, object>("CacheLevel", cacheLevel),
					new KeyValuePair<string, object>("CacheRefreshReason", authResultMetadata.CacheRefreshReason)
				});
			}
			if (OtelInstrumentation.s_durationInL2Cache.Value.Enabled && cacheLevel == CacheLevel.L2Cache)
			{
				OtelInstrumentation.s_durationInL2Cache.Value.Record(authResultMetadata.DurationInCacheInMs, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()),
					new KeyValuePair<string, object>("Platform", platform),
					new KeyValuePair<string, object>("ApiId", apiId),
					new KeyValuePair<string, object>("CacheRefreshReason", authResultMetadata.CacheRefreshReason)
				});
			}
			if (OtelInstrumentation.s_durationInHttp.Value.Enabled && authResultMetadata.TokenSource == TokenSource.IdentityProvider)
			{
				OtelInstrumentation.s_durationInHttp.Value.Record(authResultMetadata.DurationInHttpInMs, new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()), new KeyValuePair<string, object>("Platform", platform), new KeyValuePair<string, object>("ApiId", apiId));
			}
			if (OtelInstrumentation.s_durationInL1CacheInUs.Value.Enabled && authResultMetadata.TokenSource == TokenSource.Cache && authResultMetadata.CacheLevel.Equals(CacheLevel.L1Cache))
			{
				OtelInstrumentation.s_durationInL1CacheInUs.Value.Record(totalDurationInUs, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()),
					new KeyValuePair<string, object>("Platform", platform),
					new KeyValuePair<string, object>("ApiId", apiId),
					new KeyValuePair<string, object>("TokenSource", authResultMetadata.TokenSource),
					new KeyValuePair<string, object>("CacheLevel", authResultMetadata.CacheLevel),
					new KeyValuePair<string, object>("CacheRefreshReason", authResultMetadata.CacheRefreshReason)
				});
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000424F4 File Offset: 0x000406F4
		public void IncrementSuccessCounter(string platform, ApiEvent.ApiIds apiId, TokenSource tokenSource, CacheRefreshReason cacheRefreshReason, CacheLevel cacheLevel, ILoggerAdapter logger)
		{
			if (OtelInstrumentation.s_successCounter.Value.Enabled)
			{
				OtelInstrumentation.s_successCounter.Value.Add(1L, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()),
					new KeyValuePair<string, object>("Platform", platform),
					new KeyValuePair<string, object>("ApiId", apiId),
					new KeyValuePair<string, object>("TokenSource", tokenSource),
					new KeyValuePair<string, object>("CacheRefreshReason", cacheRefreshReason),
					new KeyValuePair<string, object>("CacheLevel", cacheLevel)
				});
				logger.Verbose(() => "[OpenTelemetry] Completed incrementing to success counter.");
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000425D8 File Offset: 0x000407D8
		public void LogFailureMetrics(string platform, string errorCode, ApiEvent.ApiIds apiId, CacheRefreshReason cacheRefreshReason)
		{
			if (OtelInstrumentation.s_failureCounter.Value.Enabled)
			{
				OtelInstrumentation.s_failureCounter.Value.Add(1L, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("MsalVersion", MsalIdHelper.GetMsalVersion()),
					new KeyValuePair<string, object>("Platform", platform),
					new KeyValuePair<string, object>("ErrorCode", errorCode),
					new KeyValuePair<string, object>("ApiId", apiId),
					new KeyValuePair<string, object>("CacheRefreshReason", cacheRefreshReason)
				});
			}
		}

		// Token: 0x0400080B RID: 2059
		public const string MeterName = "MicrosoftIdentityClient_Common_Meter";

		// Token: 0x0400080C RID: 2060
		private const string SuccessCounterName = "MsalSuccess";

		// Token: 0x0400080D RID: 2061
		private const string FailedCounterName = "MsalFailure";

		// Token: 0x0400080E RID: 2062
		private const string TotalDurationHistogramName = "MsalTotalDuration.1A";

		// Token: 0x0400080F RID: 2063
		private const string DurationInL1CacheHistogramName = "MsalDurationInL1CacheInUs.1B";

		// Token: 0x04000810 RID: 2064
		private const string DurationInL2CacheHistogramName = "MsalDurationInL2Cache.1A";

		// Token: 0x04000811 RID: 2065
		private const string DurationInHttpHistogramName = "MsalDurationInHttp.1A";

		// Token: 0x04000812 RID: 2066
		internal static readonly Meter Meter = new Meter("MicrosoftIdentityClient_Common_Meter", "1.0.0");

		// Token: 0x04000813 RID: 2067
		internal static readonly Lazy<Counter<long>> s_successCounter = new Lazy<Counter<long>>(() => OtelInstrumentation.Meter.CreateCounter<long>("MsalSuccess", null, "Number of successful token acquisition calls"));

		// Token: 0x04000814 RID: 2068
		internal static readonly Lazy<Counter<long>> s_failureCounter = new Lazy<Counter<long>>(() => OtelInstrumentation.Meter.CreateCounter<long>("MsalFailure", null, "Number of failed token acquisition calls"));

		// Token: 0x04000815 RID: 2069
		internal static readonly Lazy<Histogram<long>> s_durationTotal = new Lazy<Histogram<long>>(() => OtelInstrumentation.Meter.CreateHistogram<long>("MsalTotalDuration.1A", "ms", "Performance of token acquisition calls total latency"));

		// Token: 0x04000816 RID: 2070
		internal static readonly Lazy<Histogram<long>> s_durationInL1CacheInUs = new Lazy<Histogram<long>>(() => OtelInstrumentation.Meter.CreateHistogram<long>("MsalDurationInL1CacheInUs.1B", "us", "Performance of token acquisition calls total latency in microseconds when L1 cache is used."));

		// Token: 0x04000817 RID: 2071
		internal static readonly Lazy<Histogram<long>> s_durationInL2Cache = new Lazy<Histogram<long>>(() => OtelInstrumentation.Meter.CreateHistogram<long>("MsalDurationInL2Cache.1A", "ms", "Performance of token acquisition calls cache latency"));

		// Token: 0x04000818 RID: 2072
		internal static readonly Lazy<Histogram<long>> s_durationInHttp = new Lazy<Histogram<long>>(() => OtelInstrumentation.Meter.CreateHistogram<long>("MsalDurationInHttp.1A", "ms", "Performance of token acquisition calls network latency"));
	}
}
