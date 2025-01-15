using System;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Platform;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000083 RID: 131
	public class TelemetryDebugWriter : IDebugOutput
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00012D9E File Offset: 0x00010F9E
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00012DA5 File Offset: 0x00010FA5
		public static bool IsTracingDisabled { get; set; }

		// Token: 0x06000434 RID: 1076 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public static void WriteTelemetry(ITelemetry telemetry, string filteredBy = null)
		{
			IDebugOutput debugOutput = PlatformSingleton.Current.GetDebugOutput();
			if (debugOutput.IsAttached() && debugOutput.IsLogging())
			{
				string text = "Application Insights Telemetry: ";
				if (string.IsNullOrEmpty(telemetry.Context.InstrumentationKey))
				{
					text = "Application Insights Telemetry (unconfigured): ";
				}
				if (!string.IsNullOrEmpty(filteredBy))
				{
					text = "Application Insights Telemetry (filtered by " + filteredBy + "): ";
				}
				string text2 = JsonSerializer.SerializeAsString(telemetry);
				debugOutput.WriteLine(text + text2);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00012E23 File Offset: 0x00011023
		void IDebugOutput.WriteLine(string message)
		{
			Debugger.Log(0, "category", message + Environment.NewLine);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00012E3B File Offset: 0x0001103B
		bool IDebugOutput.IsLogging()
		{
			return !TelemetryDebugWriter.IsTracingDisabled && Debugger.IsLogging();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00012E4B File Offset: 0x0001104B
		bool IDebugOutput.IsAttached()
		{
			return Debugger.IsAttached;
		}
	}
}
