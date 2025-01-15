using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000022 RID: 34
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OperationTelemetryExtensions
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00006E36 File Offset: 0x00005036
		public static void Start(this OperationTelemetry telemetry)
		{
			telemetry.Start(Stopwatch.GetTimestamp());
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006E43 File Offset: 0x00005043
		public static void Start(this OperationTelemetry telemetry, long timestamp)
		{
			telemetry.Timestamp = PreciseTimestamp.GetUtcNow();
			telemetry.BeginTimeInTicks = timestamp;
			RichPayloadEventSource.Log.ProcessOperationStart(telemetry);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006E62 File Offset: 0x00005062
		public static void Stop(this OperationTelemetry telemetry)
		{
			if (telemetry.BeginTimeInTicks != 0L)
			{
				OperationTelemetryExtensions.StopImpl(telemetry, Stopwatch.GetTimestamp());
				return;
			}
			OperationTelemetryExtensions.StopImpl(telemetry, TimeSpan.Zero);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006E83 File Offset: 0x00005083
		public static void Stop(this OperationTelemetry telemetry, long timestamp)
		{
			if (telemetry.BeginTimeInTicks != 0L)
			{
				OperationTelemetryExtensions.StopImpl(telemetry, timestamp);
				return;
			}
			OperationTelemetryExtensions.StopImpl(telemetry, TimeSpan.Zero);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static void GenerateOperationId(this OperationTelemetry telemetry)
		{
			telemetry.GenerateId();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006EA8 File Offset: 0x000050A8
		private static void StopImpl(OperationTelemetry telemetry, long timestamp)
		{
			double num = (double)(timestamp - telemetry.BeginTimeInTicks) * PreciseTimestamp.StopwatchTicksToTimeSpanTicks;
			OperationTelemetryExtensions.StopImpl(telemetry, TimeSpan.FromTicks((long)Math.Round(num)));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006ED7 File Offset: 0x000050D7
		private static void StopImpl(OperationTelemetry telemetry, TimeSpan duration)
		{
			telemetry.Duration = duration;
			if (telemetry.Timestamp == DateTimeOffset.MinValue)
			{
				telemetry.Timestamp = PreciseTimestamp.GetUtcNow();
			}
			RichPayloadEventSource.Log.ProcessOperationStop(telemetry);
		}
	}
}
