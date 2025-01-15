using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000008 RID: 8
	public class AppInsightsService : ILoggerService
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002588 File Offset: 0x00000788
		public AppInsightsService(IAppInsightsWrapper appInsightsWrapper)
		{
			this.AppInsightsWrapper = appInsightsWrapper;
			this.AppInsightsHostData = new Dictionary<string, string>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025AC File Offset: 0x000007AC
		public void Initialize(HostData hostData)
		{
			this.AppInsightsWrapper.ApplicationVersion = hostData.build;
			this.AppInsightsWrapper.DeviceId = hostData.deviceId;
			this.AppInsightsWrapper.IsReturningUser = hostData.isReturningUser;
			this.AppInsightsWrapper.SessionId = hostData.sessionId;
			this.AppInsightsWrapper.UserId = hostData.userId;
			this.AppInsightsHostData = hostData.Properties;
			this.AppInsightsHostData.Remove("isReturningUser");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000262C File Offset: 0x0000082C
		public void TraceInfo(ITelemetryTrace telemetryTrace)
		{
			Dictionary<string, string> traceProperties = this.GetTraceProperties();
			this.AppInsightsWrapper.TrackTrace(TraceType.Information, telemetryTrace.Message.ScrubAndOrObfuscateTaggedInfo(), traceProperties);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002658 File Offset: 0x00000858
		public void TraceVerbose(ITelemetryTrace telemetryTrace)
		{
			Dictionary<string, string> traceProperties = this.GetTraceProperties();
			this.AppInsightsWrapper.TrackTrace(TraceType.Verbose, telemetryTrace.Message.ScrubAndOrObfuscateTaggedInfo(), traceProperties);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002684 File Offset: 0x00000884
		public void TraceWarning(ITelemetryTrace telemetryTrace)
		{
			Dictionary<string, string> traceProperties = this.GetTraceProperties();
			this.AppInsightsWrapper.TrackTrace(TraceType.Warning, telemetryTrace.Message.ScrubAndOrObfuscateTaggedInfo(), traceProperties);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026B0 File Offset: 0x000008B0
		public void TraceError(ITelemetryTrace telemetryTrace)
		{
			Dictionary<string, string> traceProperties = this.GetTraceProperties();
			this.AppInsightsWrapper.TrackTrace(TraceType.Error, telemetryTrace.Message.ScrubAndOrObfuscateTaggedInfo(), traceProperties);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026DC File Offset: 0x000008DC
		public void Log(ITelemetryEvent telemetryEvent)
		{
			Dictionary<string, string> eventProperties = this.GetEventProperties(telemetryEvent);
			this.AppInsightsWrapper.TrackEvent(telemetryEvent.Name, telemetryEvent.Time, eventProperties);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002709 File Offset: 0x00000909
		public void StartTimedEvent(ITelemetryEvent telemetryEvent)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000270C File Offset: 0x0000090C
		public void EndTimedEvent(ITelemetryEvent telemetryEvent)
		{
			Dictionary<string, string> eventProperties = this.GetEventProperties(telemetryEvent);
			eventProperties["end"] = DateTime.UtcNow.ToString("O");
			this.AppInsightsWrapper.TrackEvent(telemetryEvent.Name, telemetryEvent.Time, eventProperties);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002756 File Offset: 0x00000956
		public bool UploadsRemaining()
		{
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002759 File Offset: 0x00000959
		public void Upload()
		{
			this.AppInsightsWrapper.Flush();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002768 File Offset: 0x00000968
		private Dictionary<string, string> GetEventProperties(ITelemetryEvent telemetryEvent)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["id"] = telemetryEvent.Id;
			dictionary["category"] = telemetryEvent.Use.ToString();
			foreach (KeyValuePair<string, string> keyValuePair in this.AppInsightsHostData)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (string text in telemetryEvent.Properties.Keys)
			{
				dictionary[text] = telemetryEvent.Properties[text].ScrubAndOrObfuscateTaggedInfo();
			}
			dictionary["c"] = Interlocked.Increment(ref this.SessionLogsIndex).ToString();
			return dictionary;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000287C File Offset: 0x00000A7C
		private Dictionary<string, string> GetTraceProperties()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.AppInsightsHostData);
			dictionary["c"] = Interlocked.Increment(ref this.SessionLogsIndex).ToString();
			return dictionary;
		}

		// Token: 0x0400002F RID: 47
		private IAppInsightsWrapper AppInsightsWrapper;

		// Token: 0x04000030 RID: 48
		private Dictionary<string, string> AppInsightsHostData;

		// Token: 0x04000031 RID: 49
		private int SessionLogsIndex = -1;
	}
}
