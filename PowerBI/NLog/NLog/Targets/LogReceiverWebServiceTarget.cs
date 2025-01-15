using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;
using NLog.LogReceiverService;

namespace NLog.Targets
{
	// Token: 0x02000044 RID: 68
	[Target("LogReceiverService")]
	public class LogReceiverWebServiceTarget : Target
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x000109CD File Offset: 0x0000EBCD
		public LogReceiverWebServiceTarget()
		{
			this.Parameters = new List<MethodCallParameter>();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000109F6 File Offset: 0x0000EBF6
		public LogReceiverWebServiceTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00010A05 File Offset: 0x0000EC05
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00010A0D File Offset: 0x0000EC0D
		[RequiredParameter]
		public virtual string EndpointAddress { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00010A16 File Offset: 0x0000EC16
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00010A1E File Offset: 0x0000EC1E
		public string EndpointConfigurationName { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00010A27 File Offset: 0x0000EC27
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x00010A2F File Offset: 0x0000EC2F
		public bool UseBinaryEncoding { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00010A38 File Offset: 0x0000EC38
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x00010A40 File Offset: 0x0000EC40
		public bool UseOneWayContract { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00010A49 File Offset: 0x0000EC49
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00010A51 File Offset: 0x0000EC51
		public Layout ClientId { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00010A5A File Offset: 0x0000EC5A
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00010A62 File Offset: 0x0000EC62
		[ArrayParameter(typeof(MethodCallParameter), "parameter")]
		public IList<MethodCallParameter> Parameters { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00010A6B File Offset: 0x0000EC6B
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00010A73 File Offset: 0x0000EC73
		public bool IncludeEventProperties { get; set; }

		// Token: 0x060006C5 RID: 1733 RVA: 0x00010A7C File Offset: 0x0000EC7C
		protected internal virtual bool OnSend(NLogEvents events, IEnumerable<AsyncLogEventInfo> asyncContinuations)
		{
			return true;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00010A7F File Offset: 0x0000EC7F
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[] { logEvent });
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00010A95 File Offset: 0x0000EC95
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			if (this.inCall)
			{
				for (int i = 0; i < logEvents.Count; i++)
				{
					base.PrecalculateVolatileLayouts(logEvents[i].LogEvent);
					this.buffer.Append(logEvents[i]);
				}
				return;
			}
			AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEvents.Count];
			logEvents.CopyTo(array, 0);
			NLogEvents nlogEvents = this.TranslateLogEvents(array);
			this.Send(nlogEvents, array, null);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00010B14 File Offset: 0x0000ED14
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this.SendBufferedEvents(asyncContinuation);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00010B20 File Offset: 0x0000ED20
		private static int AddValueAndGetStringOrdinal(NLogEvents context, Dictionary<string, int> stringTable, string value)
		{
			int count;
			if (value == null || !stringTable.TryGetValue(value, out count))
			{
				count = context.Strings.Count;
				if (value != null)
				{
					stringTable.Add(value, count);
				}
				context.Strings.Add(value);
			}
			return count;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00010B60 File Offset: 0x0000ED60
		private NLogEvents TranslateLogEvents(IList<AsyncLogEventInfo> logEvents)
		{
			if (logEvents.Count == 0 && !LogManager.ThrowExceptions)
			{
				InternalLogger.Error<string>("LogReceiverServiceTarget(Name={0}): LogEvents array is empty, sending empty event...", base.Name);
				return new NLogEvents();
			}
			string text = string.Empty;
			if (this.ClientId != null)
			{
				text = this.ClientId.Render(logEvents[0].LogEvent);
			}
			NLogEvents nlogEvents = new NLogEvents
			{
				ClientName = text,
				LayoutNames = new StringCollection(),
				Strings = new StringCollection(),
				BaseTimeUtc = logEvents[0].LogEvent.TimeStamp.ToUniversalTime().Ticks
			};
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				nlogEvents.LayoutNames.Add(this.Parameters[i].Name);
			}
			if (this.IncludeEventProperties)
			{
				LogReceiverWebServiceTarget.AddEventProperties(logEvents, nlogEvents);
			}
			nlogEvents.Events = new NLogEvent[logEvents.Count];
			for (int j = 0; j < logEvents.Count; j++)
			{
				AsyncLogEventInfo asyncLogEventInfo = logEvents[j];
				nlogEvents.Events[j] = this.TranslateEvent(asyncLogEventInfo.LogEvent, nlogEvents, dictionary);
			}
			return nlogEvents;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		private static void AddEventProperties(IList<AsyncLogEventInfo> logEvents, NLogEvents networkLogEvents)
		{
			for (int i = 0; i < logEvents.Count; i++)
			{
				LogEventInfo logEvent = logEvents[i].LogEvent;
				if (logEvent.HasProperties)
				{
					foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
					{
						string text;
						if ((text = keyValuePair.Key as string) != null && !networkLogEvents.LayoutNames.Contains(text))
						{
							networkLogEvents.LayoutNames.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00010D40 File Offset: 0x0000EF40
		private void Send(NLogEvents events, IList<AsyncLogEventInfo> asyncContinuations, AsyncContinuation flushContinuations)
		{
			if (!this.OnSend(events, asyncContinuations))
			{
				if (flushContinuations != null)
				{
					flushContinuations(null);
				}
				return;
			}
			IWcfLogReceiverClient wcfLogReceiverClient = this.CreateLogReceiver();
			wcfLogReceiverClient.ProcessLogMessagesCompleted += delegate(object sender, AsyncCompletedEventArgs e)
			{
				if (e.Error != null)
				{
					InternalLogger.Error(e.Error, "LogReceiverServiceTarget(Name={0}): Error while sending", new object[] { this.Name });
				}
				for (int i = 0; i < asyncContinuations.Count; i++)
				{
					asyncContinuations[i].Continuation(e.Error);
				}
				if (flushContinuations != null)
				{
					flushContinuations(e.Error);
				}
				this.SendBufferedEvents(null);
			};
			this.inCall = true;
			wcfLogReceiverClient.ProcessLogMessagesAsync(events);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		[Obsolete("Use CreateLogReceiver instead. Marked obsolete before v4.3.11 and it may be removed in a future release.")]
		protected virtual WcfLogReceiverClient CreateWcfLogReceiverClient()
		{
			WcfLogReceiverClient wcfLogReceiverClient;
			if (string.IsNullOrEmpty(this.EndpointConfigurationName))
			{
				Binding binding;
				if (this.UseBinaryEncoding)
				{
					binding = new CustomBinding(new BindingElement[]
					{
						new BinaryMessageEncodingBindingElement(),
						new HttpTransportBindingElement()
					});
				}
				else
				{
					binding = new BasicHttpBinding();
				}
				wcfLogReceiverClient = new WcfLogReceiverClient(this.UseOneWayContract, binding, new EndpointAddress(this.EndpointAddress));
			}
			else
			{
				wcfLogReceiverClient = new WcfLogReceiverClient(this.UseOneWayContract, this.EndpointConfigurationName, new EndpointAddress(this.EndpointAddress));
			}
			wcfLogReceiverClient.ProcessLogMessagesCompleted += this.ClientOnProcessLogMessagesCompleted;
			return wcfLogReceiverClient;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00010E44 File Offset: 0x0000F044
		protected virtual IWcfLogReceiverClient CreateLogReceiver()
		{
			return this.CreateWcfLogReceiverClient();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00010E4C File Offset: 0x0000F04C
		private void ClientOnProcessLogMessagesCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			IWcfLogReceiverClient wcfLogReceiverClient = sender as IWcfLogReceiverClient;
			if (wcfLogReceiverClient != null && wcfLogReceiverClient.State == CommunicationState.Opened)
			{
				try
				{
					wcfLogReceiverClient.Close();
				}
				catch
				{
					wcfLogReceiverClient.Abort();
				}
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00010E90 File Offset: 0x0000F090
		private void SendBufferedEvents(AsyncContinuation flushContinuation)
		{
			try
			{
				object syncRoot = base.SyncRoot;
				lock (syncRoot)
				{
					AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
					if (eventsAndClear.Length != 0)
					{
						NLogEvents nlogEvents = this.TranslateLogEvents(eventsAndClear);
						this.Send(nlogEvents, eventsAndClear, flushContinuation);
					}
					else
					{
						this.inCall = false;
						if (flushContinuation != null)
						{
							flushContinuation(null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (flushContinuation != null)
				{
					InternalLogger.Error(ex, "LogReceiverServiceTarget(Name={0}): Error in flush async", new object[] { base.Name });
					if (ex.MustBeRethrown())
					{
						throw;
					}
					flushContinuation(ex);
				}
				else
				{
					InternalLogger.Error(ex, "LogReceiverServiceTarget(Name={0}): Error in send async", new object[] { base.Name });
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00010F6C File Offset: 0x0000F16C
		internal NLogEvent TranslateEvent(LogEventInfo eventInfo, NLogEvents context, Dictionary<string, int> stringTable)
		{
			NLogEvent nlogEvent = new NLogEvent();
			nlogEvent.Id = eventInfo.SequenceID;
			nlogEvent.MessageOrdinal = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.FormattedMessage);
			nlogEvent.LevelOrdinal = eventInfo.Level.Ordinal;
			nlogEvent.LoggerOrdinal = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.LoggerName);
			nlogEvent.TimeDelta = eventInfo.TimeStamp.ToUniversalTime().Ticks - context.BaseTimeUtc;
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				string text = this.Parameters[i].Layout.Render(eventInfo);
				int num = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, text);
				nlogEvent.ValueIndexes.Add(num);
			}
			for (int j = this.Parameters.Count; j < context.LayoutNames.Count; j++)
			{
				object obj;
				string text2;
				if (eventInfo.HasProperties && eventInfo.Properties.TryGetValue(context.LayoutNames[j], out obj))
				{
					text2 = Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				else
				{
					text2 = string.Empty;
				}
				int num2 = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, text2);
				nlogEvent.ValueIndexes.Add(num2);
			}
			if (eventInfo.Exception != null)
			{
				nlogEvent.ValueIndexes.Add(LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.Exception.ToString()));
			}
			return nlogEvent;
		}

		// Token: 0x0400012B RID: 299
		private readonly LogEventInfoBuffer buffer = new LogEventInfoBuffer(10000, false, 10000);

		// Token: 0x0400012C RID: 300
		private bool inCall;
	}
}
