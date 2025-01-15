using System;
using System.Globalization;
using System.Linq;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A3 RID: 163
	internal class PortalDiagnosticsSender : IDiagnosticsSender
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x00014F75 File Offset: 0x00013175
		public PortalDiagnosticsSender(TelemetryConfiguration configuration, IDiagnoisticsEventThrottlingManager throttlingManager)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (throttlingManager == null)
			{
				throw new ArgumentNullException("throttlingManager");
			}
			this.telemetryClient = new TelemetryClient(configuration);
			this.throttlingManager = throttlingManager;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014FAC File Offset: 0x000131AC
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00014FB4 File Offset: 0x000131B4
		public string DiagnosticsInstrumentationKey { get; set; }

		// Token: 0x060004FD RID: 1277 RVA: 0x00014FC0 File Offset: 0x000131C0
		public void Send(TraceEvent eventData)
		{
			try
			{
				if (eventData.MetaData != null && !string.IsNullOrEmpty(eventData.MetaData.MessageFormat) && !ThreadResourceLock.IsResourceLocked)
				{
					using (new ThreadResourceLock())
					{
						try
						{
							if (!this.throttlingManager.ThrottleEvent(eventData.MetaData.EventId, eventData.MetaData.Keywords))
							{
								this.InternalSendTraceTelemetry(eventData);
							}
						}
						catch (Exception ex)
						{
							CoreEventSource.Log.LogError("Failed to send traces to the portal: " + ex.ToInvariantString(), "Incorrect");
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001507C File Offset: 0x0001327C
		private void InternalSendTraceTelemetry(TraceEvent eventData)
		{
			if (this.telemetryClient.TelemetryConfiguration.TelemetryChannel == null)
			{
				return;
			}
			TraceTelemetry traceTelemetry = new TraceTelemetry();
			string text = ((eventData.Payload != null) ? string.Format(CultureInfo.CurrentCulture, eventData.MetaData.MessageFormat, eventData.Payload.ToArray<object>()) : eventData.MetaData.MessageFormat);
			if ((eventData.MetaData.Keywords & 1L) == 1L)
			{
				text = "AI: " + text;
			}
			else
			{
				string text2 = "[" + eventData.MetaData.EventSourceName + "] ";
				text = "AI (Internal): " + text2 + text;
			}
			traceTelemetry.Message = text;
			if (!string.IsNullOrEmpty(this.DiagnosticsInstrumentationKey))
			{
				traceTelemetry.Context.InstrumentationKey = this.DiagnosticsInstrumentationKey;
			}
			traceTelemetry.Context.Operation.SyntheticSource = "SDKTelemetry";
			this.telemetryClient.TrackTrace(traceTelemetry);
		}

		// Token: 0x040001FC RID: 508
		private const string AiPrefix = "AI: ";

		// Token: 0x040001FD RID: 509
		private const string AiNonUserActionable = "AI (Internal): ";

		// Token: 0x040001FE RID: 510
		private const string SdkTelemetrySyntheticSourceName = "SDKTelemetry";

		// Token: 0x040001FF RID: 511
		private readonly TelemetryClient telemetryClient;

		// Token: 0x04000200 RID: 512
		private readonly IDiagnoisticsEventThrottlingManager throttlingManager;
	}
}
