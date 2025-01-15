using System;
using Microsoft.BIServer.Telemetry.Interfaces;
using Owin;

namespace Microsoft.BIServer.Telemetry.Helpers
{
	// Token: 0x02000007 RID: 7
	internal static class TelemetryExtensions
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000022AB File Offset: 0x000004AB
		internal static void UseAppInsightsTelemetry(this IAppBuilder appBuilder, ITelemetryService telemetryService)
		{
			appBuilder.Use(typeof(TelemetryMiddleware), new object[] { telemetryService });
		}
	}
}
