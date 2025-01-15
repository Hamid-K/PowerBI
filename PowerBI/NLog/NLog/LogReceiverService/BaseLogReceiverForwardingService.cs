using System;

namespace NLog.LogReceiverService
{
	// Token: 0x0200008D RID: 141
	public abstract class BaseLogReceiverForwardingService
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0001A0E8 File Offset: 0x000182E8
		protected BaseLogReceiverForwardingService()
			: this(null)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001A0F1 File Offset: 0x000182F1
		protected BaseLogReceiverForwardingService(LogFactory logFactory)
		{
			this._logFactory = logFactory;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001A100 File Offset: 0x00018300
		public void ProcessLogMessages(NLogEvents events)
		{
			DateTime dateTime = new DateTime(events.BaseTimeUtc, DateTimeKind.Utc);
			LogEventInfo[] array = new LogEventInfo[events.Events.Length];
			for (int i = 0; i < events.Events.Length; i++)
			{
				NLogEvent nlogEvent = events.Events[i];
				LogLevel logLevel = LogLevel.FromOrdinal(nlogEvent.LevelOrdinal);
				string text = events.Strings[nlogEvent.LoggerOrdinal];
				LogEventInfo logEventInfo = new LogEventInfo();
				logEventInfo.Level = logLevel;
				logEventInfo.LoggerName = text;
				logEventInfo.TimeStamp = dateTime.AddTicks(nlogEvent.TimeDelta).ToLocalTime();
				logEventInfo.Message = events.Strings[nlogEvent.MessageOrdinal];
				logEventInfo.Properties.Add("ClientName", events.ClientName);
				for (int j = 0; j < events.LayoutNames.Count; j++)
				{
					logEventInfo.Properties.Add(events.LayoutNames[j], events.Strings[nlogEvent.ValueIndexes[j]]);
				}
				array[i] = logEventInfo;
			}
			this.ProcessLogMessages(array);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001A228 File Offset: 0x00018428
		protected virtual void ProcessLogMessages(LogEventInfo[] logEvents)
		{
			ILogger logger = null;
			string text = string.Empty;
			foreach (LogEventInfo logEventInfo in logEvents)
			{
				if (logEventInfo.LoggerName != text)
				{
					if (this._logFactory != null)
					{
						logger = this._logFactory.GetLogger(logEventInfo.LoggerName);
					}
					else
					{
						logger = LogManager.GetLogger(logEventInfo.LoggerName);
					}
					text = logEventInfo.LoggerName;
				}
				if (logger != null)
				{
					logger.Log(logEventInfo);
				}
			}
		}

		// Token: 0x04000250 RID: 592
		private readonly LogFactory _logFactory;
	}
}
