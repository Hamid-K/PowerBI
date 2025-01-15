using System;
using System.Text;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Logging
{
	// Token: 0x0200001F RID: 31
	internal sealed class RSLoggerService : ILoggerService
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003CDB File Offset: 0x00001EDB
		public RSLoggerService(IReportServerLogger reportServerLogger)
		{
			this._reportServerLogger = reportServerLogger;
			this._obfuscatePIInformation = this._reportServerLogger.LogLevel != LogLevel.Trace;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003D01 File Offset: 0x00001F01
		public void Initialize(HostData hostData)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003D04 File Offset: 0x00001F04
		public void Log(ITelemetryEvent telemetryEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(telemetryEvent.Name);
			stringBuilder.Append("|");
			foreach (string text in telemetryEvent.Properties.Keys)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(":");
				if (this._obfuscatePIInformation)
				{
					stringBuilder.Append(telemetryEvent.Properties[text].ScrubAndOrObfuscateTaggedInfo());
				}
				else
				{
					stringBuilder.Append(telemetryEvent.Properties[text]);
				}
				stringBuilder.Append(";");
			}
			switch (RSLoggerService.GetType(telemetryEvent))
			{
			case RSLoggerService.EventType.Info:
				this._reportServerLogger.Info(stringBuilder.ToString(), Array.Empty<object>());
				return;
			case RSLoggerService.EventType.Warn:
				this._reportServerLogger.Warning(stringBuilder.ToString(), Array.Empty<object>());
				return;
			case RSLoggerService.EventType.Error:
				this._reportServerLogger.Error(stringBuilder.ToString(), Array.Empty<object>());
				return;
			}
			this._reportServerLogger.Trace(stringBuilder.ToString(), Array.Empty<object>());
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003E44 File Offset: 0x00002044
		public void TraceInfo(ITelemetryTrace telemetryTrace)
		{
			this._reportServerLogger.Info(telemetryTrace.Message, Array.Empty<object>());
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003E5C File Offset: 0x0000205C
		public void TraceVerbose(ITelemetryTrace telemetryTrace)
		{
			this._reportServerLogger.Trace(telemetryTrace.Message, Array.Empty<object>());
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003E74 File Offset: 0x00002074
		public void TraceWarning(ITelemetryTrace telemetryTrace)
		{
			this._reportServerLogger.Warning(telemetryTrace.Message, Array.Empty<object>());
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003E8C File Offset: 0x0000208C
		public void TraceError(ITelemetryTrace telemetryTrace)
		{
			this._reportServerLogger.Error(telemetryTrace.Message, Array.Empty<object>());
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003EA4 File Offset: 0x000020A4
		private static RSLoggerService.EventType GetType(ITelemetryEvent telemetryEvent)
		{
			string text;
			telemetryEvent.Properties.TryGetValue("type", out text);
			string text2;
			telemetryEvent.Properties.TryGetValue("isError", out text2);
			string text3;
			telemetryEvent.Properties.TryGetValue("ExceptionType", out text3);
			if (string.Equals("Error", text, StringComparison.OrdinalIgnoreCase) || string.Equals("True", text2, StringComparison.OrdinalIgnoreCase) || !string.IsNullOrEmpty(text3))
			{
				return RSLoggerService.EventType.Error;
			}
			if (text != null)
			{
				string text4 = text.ToLowerInvariant();
				if (text4 == "information")
				{
					return RSLoggerService.EventType.Info;
				}
				if (text4 == "warning")
				{
					return RSLoggerService.EventType.Warn;
				}
			}
			return RSLoggerService.EventType.Trace;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003F3B File Offset: 0x0000213B
		public void StartTimedEvent(ITelemetryEvent telemetryEvent)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003F3D File Offset: 0x0000213D
		public void EndTimedEvent(ITelemetryEvent telemetryEvent)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F3F File Offset: 0x0000213F
		public bool UploadsRemaining()
		{
			return false;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F42 File Offset: 0x00002142
		public void Upload()
		{
		}

		// Token: 0x04000070 RID: 112
		private IReportServerLogger _reportServerLogger;

		// Token: 0x04000071 RID: 113
		private bool _obfuscatePIInformation;

		// Token: 0x02000035 RID: 53
		private enum EventType
		{
			// Token: 0x040000CF RID: 207
			Trace,
			// Token: 0x040000D0 RID: 208
			Info,
			// Token: 0x040000D1 RID: 209
			Warn,
			// Token: 0x040000D2 RID: 210
			Error
		}
	}
}
