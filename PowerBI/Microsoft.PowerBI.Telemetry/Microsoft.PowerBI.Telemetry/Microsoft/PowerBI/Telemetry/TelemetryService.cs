using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000025 RID: 37
	public class TelemetryService : ITelemetryService
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000034E8 File Offset: 0x000016E8
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000034F0 File Offset: 0x000016F0
		private List<ILoggerService> LoggerServices { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000034F9 File Offset: 0x000016F9
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003501 File Offset: 0x00001701
		private HostData HostData { get; set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x0000350C File Offset: 0x0000170C
		public TelemetryService(ITelemetryConfiguration configuration, ITelemetryEvent rootEvent)
		{
			this.LoggerServices = configuration.Loggers;
			this.HostData = configuration.HostData;
			this.m_eventSummary = new Dictionary<string, EventAcumulatedSummary>(StringComparer.Ordinal);
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.Initialize(this.HostData);
			}
			if (rootEvent == null)
			{
				throw new ArgumentNullException("rootEvent");
			}
			this.Root = rootEvent;
			this.Log(this.Root);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000035C8 File Offset: 0x000017C8
		public TelemetryService(ITelemetryConfiguration configuration)
			: this(configuration, new PBIWinRootSession("00000000-0000-0000-0000-000000000000", false))
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000035DC File Offset: 0x000017DC
		public TelemetryService(ITelemetryConfiguration configuration, int summaryTime)
			: this(configuration)
		{
			this.m_eventSummaryPeriodMs = summaryTime;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000035EC File Offset: 0x000017EC
		static TelemetryService()
		{
			TelemetryService.DisableLogging();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003613 File Offset: 0x00001813
		public static TelemetryService Instance { get; private set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x0000361C File Offset: 0x0000181C
		public static void EnableLogging(ITelemetryConfiguration configuration)
		{
			object obj = TelemetryService.instanceLock;
			lock (obj)
			{
				TelemetryService.Instance = new TelemetryService(configuration);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003660 File Offset: 0x00001860
		public static void EnableLogging(ITelemetryConfiguration configuration, ITelemetryEvent rootEvent)
		{
			object obj = TelemetryService.instanceLock;
			lock (obj)
			{
				TelemetryService.Instance = new TelemetryService(configuration, rootEvent);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000036A8 File Offset: 0x000018A8
		public static void DisableLogging()
		{
			object obj = TelemetryService.instanceLock;
			lock (obj)
			{
				TelemetryService.Instance = TelemetryService.nullTelemetryService;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000036EC File Offset: 0x000018EC
		public bool UploadsRemain()
		{
			using (List<ILoggerService>.Enumerator enumerator = this.LoggerServices.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.UploadsRemaining())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003748 File Offset: 0x00001948
		public void Upload()
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.Upload();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003798 File Offset: 0x00001998
		public string SessionId
		{
			get
			{
				return this.HostData.sessionId;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000037A5 File Offset: 0x000019A5
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000037AD File Offset: 0x000019AD
		public ITelemetryEvent Root { get; private set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x000037B8 File Offset: 0x000019B8
		public void AccumulateSummaryBinForEvent(string eventName, int bin)
		{
			object eventSummaryLock = this.m_eventSummaryLock;
			lock (eventSummaryLock)
			{
				if (this.m_summaryTimer == null)
				{
					this.m_summaryTimer = new Timer(delegate(object state)
					{
						this.TraceAccumulatedSummary();
					}, null, this.m_eventSummaryPeriodMs, -1);
				}
				EventAcumulatedSummary eventAcumulatedSummary;
				if (!this.m_eventSummary.TryGetValue(eventName, out eventAcumulatedSummary))
				{
					eventAcumulatedSummary = new EventAcumulatedSummary();
					this.m_eventSummary.Add(eventName, eventAcumulatedSummary);
				}
				eventAcumulatedSummary.AccumulateSummaryBin(bin);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003844 File Offset: 0x00001A44
		public void TraceAccumulatedSummary()
		{
			object eventSummaryLock = this.m_eventSummaryLock;
			Dictionary<string, EventAcumulatedSummary> eventSummary;
			lock (eventSummaryLock)
			{
				if (this.m_summaryTimer != null)
				{
					this.m_summaryTimer.Dispose();
					this.m_summaryTimer = null;
				}
				if (this.m_eventSummary.Count == 0)
				{
					return;
				}
				eventSummary = this.m_eventSummary;
				this.m_eventSummary = new Dictionary<string, EventAcumulatedSummary>(StringComparer.Ordinal);
			}
			foreach (KeyValuePair<string, EventAcumulatedSummary> keyValuePair in eventSummary)
			{
				this.TraceInfo(keyValuePair.Value.ConvertToSummaryMessage(keyValuePair.Key), EventTarget.TelemetryAndLogs);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003910 File Offset: 0x00001B10
		public void TraceInfo(string message)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, EventTarget.LogsOnly);
			this.TraceInfo(telemetryTrace);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000392C File Offset: 0x00001B2C
		public void TraceVerbose(string message)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, EventTarget.LogsOnly);
			this.TraceVerbose(telemetryTrace);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003948 File Offset: 0x00001B48
		public void TraceWarning(string message)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, EventTarget.LogsOnly);
			this.TraceWarning(telemetryTrace);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003964 File Offset: 0x00001B64
		public void TraceError(string message)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, EventTarget.TelemetryAndLogs);
			this.TraceError(telemetryTrace);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003980 File Offset: 0x00001B80
		public void TraceInfo(string message, EventTarget eventTarget)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, eventTarget);
			this.TraceInfo(telemetryTrace);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000399C File Offset: 0x00001B9C
		public void TraceVerbose(string message, EventTarget eventTarget)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, eventTarget);
			this.TraceVerbose(telemetryTrace);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000039B8 File Offset: 0x00001BB8
		public void TraceWarning(string message, EventTarget eventTarget)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, eventTarget);
			this.TraceWarning(telemetryTrace);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000039D4 File Offset: 0x00001BD4
		public void TraceError(string message, EventTarget eventTarget)
		{
			TelemetryTrace telemetryTrace = new TelemetryTrace(message, eventTarget);
			this.TraceError(telemetryTrace);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000039F0 File Offset: 0x00001BF0
		public void Log(ITelemetryEvent telemetryEvent)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.Log(telemetryEvent);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003A44 File Offset: 0x00001C44
		public void StartTimedEvent(ITelemetryEvent telemetryEvent)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.StartTimedEvent(telemetryEvent);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003A98 File Offset: 0x00001C98
		public void EndTimedEvent(ITelemetryEvent telemetryEvent)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.EndTimedEvent(telemetryEvent);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003AEC File Offset: 0x00001CEC
		private void TraceInfo(ITelemetryTrace telemetryTrace)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.TraceInfo(telemetryTrace);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003B40 File Offset: 0x00001D40
		private void TraceVerbose(ITelemetryTrace telemetryTrace)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.TraceVerbose(telemetryTrace);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003B94 File Offset: 0x00001D94
		private void TraceWarning(ITelemetryTrace telemetryTrace)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.TraceWarning(telemetryTrace);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void TraceError(ITelemetryTrace telemetryTrace)
		{
			foreach (ILoggerService loggerService in this.LoggerServices)
			{
				loggerService.TraceError(telemetryTrace);
			}
		}

		// Token: 0x0400008D RID: 141
		private int m_eventSummaryPeriodMs = 600000;

		// Token: 0x0400008E RID: 142
		private Timer m_summaryTimer;

		// Token: 0x0400008F RID: 143
		private Dictionary<string, EventAcumulatedSummary> m_eventSummary;

		// Token: 0x04000090 RID: 144
		private readonly object m_eventSummaryLock = new object();

		// Token: 0x04000092 RID: 146
		private static object instanceLock = new object();

		// Token: 0x04000093 RID: 147
		private static TelemetryService nullTelemetryService = new TelemetryService(new EmptyTelemetryConfiguration());
	}
}
