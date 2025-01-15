using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200002C RID: 44
	public static class TelemetryUtils
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00004220 File Offset: 0x00002420
		public static ILoggerService GetAppInsightsConfiguredLogger(string appInsightsInstrKey, string appInsightsConnectionString, bool createFromDefaultConfiguration)
		{
			AllMatchesRule allMatchesRule = new AllMatchesRule(new IEventTransmissionRule[]
			{
				new CategoryMatchesRule(new TelemetryUse[] { TelemetryUse.Trace }),
				new PropertyMatchesRule("type", new HashSet<string>
				{
					TraceType.Error.ToString(),
					TraceType.ExpectedError.ToString(),
					TraceType.UnexpectedError.ToString(),
					TraceType.Fatal.ToString()
				})
			});
			AllMatchesRule allMatchesRule2 = new AllMatchesRule(new IEventTransmissionRule[]
			{
				new CategoryMatchesRule(new TelemetryUse[]
				{
					TelemetryUse.CustomerAction,
					TelemetryUse.CriticalError,
					TelemetryUse.Verbose,
					TelemetryUse.Internal
				}),
				new EventTargetNotMatchRule(EventTarget.LogsOnly)
			});
			AnyMatchesRule anyMatchesRule = new AnyMatchesRule(new IEventTransmissionRule[] { allMatchesRule2, allMatchesRule });
			TargetMatchRule targetMatchRule = new TargetMatchRule(EventTarget.TelemetryAndLogs);
			return new TransmissionControlledLogger(new AppInsightsService(new AppInsights(appInsightsInstrKey, appInsightsConnectionString, createFromDefaultConfiguration)), anyMatchesRule, targetMatchRule);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004316 File Offset: 0x00002516
		public static ILoggerService GetAppInsightsConfiguredLogger(string appInsightsInstrKey, string appInsightsConnectionString)
		{
			return TelemetryUtils.GetAppInsightsConfiguredLogger(appInsightsInstrKey, appInsightsConnectionString, false);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004320 File Offset: 0x00002520
		public static TraceType GetTraceType(TraceLevel traceLevel)
		{
			switch (traceLevel)
			{
			case TraceLevel.Error:
				return TraceType.Error;
			case TraceLevel.Warning:
				return TraceType.Warning;
			case TraceLevel.Info:
				return TraceType.Information;
			case TraceLevel.Verbose:
				return TraceType.Verbose;
			}
			return TraceType.Verbose;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004347 File Offset: 0x00002547
		public static bool IsReleaseBuild(string appVersion)
		{
			return !appVersion.Contains("(Main)");
		}
	}
}
