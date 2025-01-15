using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000022 RID: 34
	public sealed class PowerBITelemetryConfiguration : TelemetryConfiguration
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00002DCD File Offset: 0x00000FCD
		public PowerBITelemetryConfiguration(string appVersion, bool isSSRS)
			: this(appVersion, null, null, isSSRS ? "Power BI Desktop SSRS" : "Power BI Desktop")
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public PowerBITelemetryConfiguration(string appVersion, string appDirName)
			: this(appVersion, null, appDirName, null)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002DF3 File Offset: 0x00000FF3
		public PowerBITelemetryConfiguration(string appVersion, ILoggerService traceLogger)
			: this(appVersion, traceLogger, "Power BI Desktop", null)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002E03 File Offset: 0x00001003
		public PowerBITelemetryConfiguration(string appVersion, ILoggerService traceLogger, string appDirName, string fullPath = null)
			: this(appVersion, traceLogger, appDirName, TelemetryUtils.IsReleaseBuild(appVersion) ? "02064c4b-fb9f-4feb-9adc-60e5fd3f72e7" : "ba67f694-fef4-4021-beec-626fe54ec363", fullPath, null)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002E28 File Offset: 0x00001028
		public PowerBITelemetryConfiguration(string appVersion, ILoggerService traceLogger, string appDirName, string appInsightsInstrKey, string fullPath = null, string appInsightsConnectionString = null)
			: base(appVersion, new List<ILoggerService>(), appDirName, "WinDesktop", fullPath, true)
		{
			this.traceLogger = traceLogger;
			this.AppInsightsInstrumentationKey = appInsightsInstrKey;
			this.AppInsightsConnectionString = appInsightsConnectionString;
			this.AuthenticatedUserId = "Unknown";
			this.ApplicationType = AppType.Unknown;
			this.TenantId = "Unknown";
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002E7E File Offset: 0x0000107E
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002E86 File Offset: 0x00001086
		public bool TelemetryEnabled
		{
			get
			{
				return this.telemetryEnabled;
			}
			set
			{
				if (this.telemetryEnabled != value)
				{
					this.telemetryEnabled = value;
					this.UpdateLoggers();
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002E9E File Offset: 0x0000109E
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002EA6 File Offset: 0x000010A6
		public bool TracingEnabled
		{
			get
			{
				return this.tracingEnabled;
			}
			set
			{
				if (this.tracingEnabled != value)
				{
					this.tracingEnabled = value;
					this.UpdateLoggers();
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002EC6 File Offset: 0x000010C6
		public string AuthenticatedUserId { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002ED7 File Offset: 0x000010D7
		public AppType ApplicationType { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002EE8 File Offset: 0x000010E8
		public string TenantId { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002EF1 File Offset: 0x000010F1
		public string AppInsightsInstrumentationKey { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002EF9 File Offset: 0x000010F9
		public string AppInsightsConnectionString { get; }

		// Token: 0x060000AD RID: 173 RVA: 0x00002F01 File Offset: 0x00001101
		protected override string GetAuthenticatedUserId()
		{
			return this.AuthenticatedUserId;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002F09 File Offset: 0x00001109
		protected override AppType GetApplicationType()
		{
			return this.ApplicationType;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002F11 File Offset: 0x00001111
		protected override string GetTenantId()
		{
			return this.TenantId;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002F1C File Offset: 0x0000111C
		private void UpdateLoggers()
		{
			List<ILoggerService> list = new List<ILoggerService>();
			if (this.tracingEnabled && this.traceLogger != null)
			{
				list.Add(this.traceLogger);
			}
			if (this.telemetryEnabled)
			{
				if (this.appInsightsLogger == null)
				{
					this.appInsightsLogger = TelemetryUtils.GetAppInsightsConfiguredLogger(this.AppInsightsInstrumentationKey, this.AppInsightsConnectionString);
				}
				list.Add(this.appInsightsLogger);
			}
			base.Loggers = list;
		}

		// Token: 0x04000052 RID: 82
		internal const string defaultAppInsightsInstrKey = "02064c4b-fb9f-4feb-9adc-60e5fd3f72e7";

		// Token: 0x04000053 RID: 83
		internal const string defaultAppInsightsPPEInstrKey = "ba67f694-fef4-4021-beec-626fe54ec363";

		// Token: 0x04000054 RID: 84
		private ILoggerService traceLogger;

		// Token: 0x04000055 RID: 85
		private ILoggerService appInsightsLogger;

		// Token: 0x04000056 RID: 86
		private bool telemetryEnabled;

		// Token: 0x04000057 RID: 87
		private bool tracingEnabled;
	}
}
