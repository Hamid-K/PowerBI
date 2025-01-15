using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009A RID: 154
	public sealed class DiagnosticsTelemetryModule : ITelemetryModule, IHeartbeatPropertyManager, IDisposable
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public DiagnosticsTelemetryModule()
		{
			this.Senders.Add(new PortalDiagnosticsQueueSender());
			this.EventListener = new DiagnosticsListener(this.Senders);
			this.heartbeatInterval = Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing.HeartbeatProvider.DefaultHeartbeatInterval;
			this.HeartbeatProvider = new HeartbeatProvider();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014B14 File Offset: 0x00012D14
		~DiagnosticsTelemetryModule()
		{
			this.Dispose(false);
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00014B44 File Offset: 0x00012D44
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00014B51 File Offset: 0x00012D51
		public bool IsHeartbeatEnabled
		{
			get
			{
				return this.HeartbeatProvider.IsHeartbeatEnabled;
			}
			set
			{
				this.HeartbeatProvider.IsHeartbeatEnabled = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00014B5F File Offset: 0x00012D5F
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00014B6C File Offset: 0x00012D6C
		public TimeSpan HeartbeatInterval
		{
			get
			{
				return this.HeartbeatProvider.HeartbeatInterval;
			}
			set
			{
				this.HeartbeatProvider.HeartbeatInterval = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00014B7A File Offset: 0x00012D7A
		public IList<string> ExcludedHeartbeatPropertyProviders
		{
			get
			{
				return this.HeartbeatProvider.ExcludedHeartbeatPropertyProviders;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00014B87 File Offset: 0x00012D87
		public IList<string> ExcludedHeartbeatProperties
		{
			get
			{
				return this.HeartbeatProvider.ExcludedHeartbeatProperties;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00014B94 File Offset: 0x00012D94
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00014BBC File Offset: 0x00012DBC
		public string Severity
		{
			get
			{
				return this.EventListener.LogLevel.ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && Enum.IsDefined(typeof(EventLevel), value))
				{
					EventLevel eventLevel = (EventLevel)Enum.Parse(typeof(EventLevel), value, true);
					this.EventListener.LogLevel = eventLevel;
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00014C06 File Offset: 0x00012E06
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00014C10 File Offset: 0x00012E10
		public string DiagnosticsInstrumentationKey
		{
			get
			{
				return this.instrumentationKey;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.instrumentationKey = value;
					foreach (PortalDiagnosticsSender portalDiagnosticsSender in this.Senders.OfType<PortalDiagnosticsSender>())
					{
						portalDiagnosticsSender.DiagnosticsInstrumentationKey = this.instrumentationKey;
					}
					this.HeartbeatProvider.InstrumentationKey = this.instrumentationKey;
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014C88 File Offset: 0x00012E88
		public void Initialize(TelemetryConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (!this.isInitialized)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (!this.isInitialized)
					{
						PortalDiagnosticsQueueSender portalDiagnosticsQueueSender = this.Senders.OfType<PortalDiagnosticsQueueSender>().First<PortalDiagnosticsQueueSender>();
						portalDiagnosticsQueueSender.IsDisabled = true;
						this.Senders.Remove(portalDiagnosticsQueueSender);
						PortalDiagnosticsSender portalDiagnosticsSender = new PortalDiagnosticsSender(configuration, new DiagnoisticsEventThrottlingManager<DiagnoisticsEventThrottling>(new DiagnoisticsEventThrottling(5), this.throttlingScheduler, 5U));
						portalDiagnosticsSender.DiagnosticsInstrumentationKey = this.DiagnosticsInstrumentationKey;
						this.Senders.Add(portalDiagnosticsSender);
						foreach (TraceEvent traceEvent in portalDiagnosticsQueueSender.EventData)
						{
							portalDiagnosticsSender.Send(traceEvent);
						}
						this.HeartbeatProvider.Initialize(configuration);
						this.isInitialized = true;
					}
				}
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00014D94 File Offset: 0x00012F94
		public bool AddHeartbeatProperty(string propertyName, string propertyValue, bool isHealthy)
		{
			return this.HeartbeatProvider.AddHeartbeatProperty(propertyName, false, propertyValue, isHealthy);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014DA5 File Offset: 0x00012FA5
		public bool SetHeartbeatProperty(string propertyName, string propertyValue = null, bool? isHealthy = null)
		{
			if (!string.IsNullOrEmpty(propertyName) && (propertyValue != null || isHealthy != null))
			{
				return this.HeartbeatProvider.SetHeartbeatProperty(propertyName, false, propertyValue, isHealthy);
			}
			CoreEventSource.Log.LogVerbose("Did not set a valid heartbeat property. Ensure you set a valid propertyName and one or both of the propertyValue and isHealthy parameters.", "Incorrect");
			return false;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00014DE0 File Offset: 0x00012FE0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00014DF0 File Offset: 0x00012FF0
		private void Dispose(bool managed)
		{
			if (managed && !this.disposed)
			{
				this.EventListener.Dispose();
				(this.throttlingScheduler as IDisposable).Dispose();
				foreach (IDisposable disposable in this.Senders.OfType<IDisposable>())
				{
					disposable.Dispose();
				}
				this.HeartbeatProvider.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x040001E7 RID: 487
		internal readonly IList<IDiagnosticsSender> Senders = new List<IDiagnosticsSender>();

		// Token: 0x040001E8 RID: 488
		internal readonly DiagnosticsListener EventListener;

		// Token: 0x040001E9 RID: 489
		internal readonly IHeartbeatProvider HeartbeatProvider;

		// Token: 0x040001EA RID: 490
		private readonly object lockObject = new object();

		// Token: 0x040001EB RID: 491
		private readonly IDiagnoisticsEventThrottlingScheduler throttlingScheduler = new DiagnoisticsEventThrottlingScheduler();

		// Token: 0x040001EC RID: 492
		private volatile bool disposed;

		// Token: 0x040001ED RID: 493
		private TimeSpan heartbeatInterval;

		// Token: 0x040001EE RID: 494
		private string instrumentationKey;

		// Token: 0x040001EF RID: 495
		private bool isInitialized;
	}
}
