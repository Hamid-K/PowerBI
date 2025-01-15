using System;
using System.ComponentModel;
using System.Diagnostics;
using NLog.Common;
using NLog.Config;
using NLog.Internal.Fakeables;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000038 RID: 56
	[Target("EventLog")]
	public class EventLogTarget : TargetWithLayout, IInstallable
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0000D774 File Offset: 0x0000B974
		public EventLogTarget()
			: this(null, null)
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000D77E File Offset: 0x0000B97E
		public EventLogTarget(string name)
			: this(null, null)
		{
			base.Name = name;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000D78F File Offset: 0x0000B98F
		[Obsolete("This constructor will be removed in NLog 5. Marked obsolete on NLog 4.6")]
		public EventLogTarget(IAppDomain appDomain)
			: this(null, appDomain)
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000D79C File Offset: 0x0000B99C
		internal EventLogTarget(EventLogTarget.IEventLogWrapper eventLogWrapper, IAppDomain appDomain)
		{
			this._eventLogWrapper = eventLogWrapper ?? new EventLogTarget.EventLogWrapper();
			appDomain = appDomain ?? LogFactory.CurrentAppDomain;
			this.Source = appDomain.FriendlyName;
			this.Log = "Application";
			this.MachineName = ".";
			this.MaxMessageLength = 16384;
			base.OptimizeBufferReuse = base.GetType() == typeof(EventLogTarget);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000D818 File Offset: 0x0000BA18
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000D820 File Offset: 0x0000BA20
		[DefaultValue(".")]
		public string MachineName { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000D829 File Offset: 0x0000BA29
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000D831 File Offset: 0x0000BA31
		public Layout EventId { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000D83A File Offset: 0x0000BA3A
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000D842 File Offset: 0x0000BA42
		public Layout Category { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000D84B File Offset: 0x0000BA4B
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000D853 File Offset: 0x0000BA53
		public Layout EntryType { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000D85C File Offset: 0x0000BA5C
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000D864 File Offset: 0x0000BA64
		public Layout Source { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000D86D File Offset: 0x0000BA6D
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000D875 File Offset: 0x0000BA75
		[DefaultValue("Application")]
		public string Log { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000D87E File Offset: 0x0000BA7E
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0000D886 File Offset: 0x0000BA86
		[DefaultValue(16384)]
		public int MaxMessageLength
		{
			get
			{
				return this._maxMessageLength;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("MaxMessageLength cannot be zero or negative.");
				}
				this._maxMessageLength = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000D89E File Offset: 0x0000BA9E
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		[DefaultValue(null)]
		public long? MaxKilobytes
		{
			get
			{
				return this._maxKilobytes;
			}
			set
			{
				if (value != null)
				{
					long? num = value;
					long num2 = 64L;
					if (!((num.GetValueOrDefault() < num2) & (num != null)))
					{
						num = value;
						num2 = 4194240L;
						if (!((num.GetValueOrDefault() > num2) & (num != null)))
						{
							num = value % 64L;
							num2 = 0L;
							if ((num.GetValueOrDefault() == num2) & (num != null))
							{
								goto IL_008F;
							}
						}
					}
					throw new ArgumentException("MaxKilobytes must be a multiple of 64, and between 64 and 4194240");
				}
				IL_008F:
				this._maxKilobytes = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000D94B File Offset: 0x0000BB4B
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000D953 File Offset: 0x0000BB53
		[DefaultValue(EventLogTargetOverflowAction.Truncate)]
		public EventLogTargetOverflowAction OnOverflow { get; set; }

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000D95C File Offset: 0x0000BB5C
		public void Install(InstallationContext installationContext)
		{
			this.CreateEventSourceIfNeeded(this.GetFixedSource(), true);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public void Uninstall(InstallationContext installationContext)
		{
			string fixedSource = this.GetFixedSource();
			if (string.IsNullOrEmpty(fixedSource))
			{
				InternalLogger.Debug<string>("EventLogTarget(Name={0}): Skipping removing of event source because it contains layout renderers", base.Name);
				return;
			}
			this._eventLogWrapper.DeleteEventSource(fixedSource, this.MachineName);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public bool? IsInstalled(InstallationContext installationContext)
		{
			string fixedSource = this.GetFixedSource();
			if (!string.IsNullOrEmpty(fixedSource))
			{
				return new bool?(this._eventLogWrapper.SourceExists(fixedSource, this.MachineName));
			}
			InternalLogger.Debug<string>("EventLogTarget(Name={0}): Unclear if event source exists because it contains layout renderers", base.Name);
			return null;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D9F9 File Offset: 0x0000BBF9
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this.CreateEventSourceIfNeeded(this.GetFixedSource(), false);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000DA10 File Offset: 0x0000BC10
		protected override void Write(LogEventInfo logEvent)
		{
			string text = base.RenderLogEvent(this.Layout, logEvent);
			EventLogEntryType entryType = this.GetEntryType(logEvent);
			int num = 0;
			string text2 = base.RenderLogEvent(this.EventId, logEvent);
			if (!string.IsNullOrEmpty(text2) && !int.TryParse(text2, out num))
			{
				InternalLogger.Warn<string, string>("EventLogTarget(Name={0}): WriteEntry failed to parse EventId={1}", base.Name, text2);
			}
			short num2 = 0;
			string text3 = base.RenderLogEvent(this.Category, logEvent);
			if (!string.IsNullOrEmpty(text3) && !short.TryParse(text3, out num2))
			{
				InternalLogger.Warn<string, string>("EventLogTarget(Name={0}): WriteEntry failed to parse Category={1}", base.Name, text3);
			}
			if (text.Length <= this.MaxMessageLength)
			{
				this.WriteEntry(logEvent, text, entryType, num, num2);
				return;
			}
			if (this.OnOverflow == EventLogTargetOverflowAction.Truncate)
			{
				text = text.Substring(0, this.MaxMessageLength);
				this.WriteEntry(logEvent, text, entryType, num, num2);
				return;
			}
			if (this.OnOverflow == EventLogTargetOverflowAction.Split)
			{
				for (int i = 0; i < text.Length; i += this.MaxMessageLength)
				{
					string text4 = text.Substring(i, Math.Min(this.MaxMessageLength, text.Length - i));
					this.WriteEntry(logEvent, text4, entryType, num, num2);
				}
				return;
			}
			EventLogTargetOverflowAction onOverflow = this.OnOverflow;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000DB34 File Offset: 0x0000BD34
		internal virtual void WriteEntry(LogEventInfo logEventInfo, string message, EventLogEntryType entryType, int eventId, short category)
		{
			this.GetEventLog(logEventInfo).WriteEntry(message, entryType, eventId, category);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000DB48 File Offset: 0x0000BD48
		private EventLogEntryType GetEntryType(LogEventInfo logEvent)
		{
			string text = base.RenderLogEvent(this.EntryType, logEvent);
			if (!string.IsNullOrEmpty(text))
			{
				EventLogEntryType eventLogEntryType;
				if (ConversionHelpers.TryParseEnum<EventLogEntryType>(text, out eventLogEntryType, (EventLogEntryType)0))
				{
					return eventLogEntryType;
				}
				InternalLogger.Warn<string, string>("EventLogTarget(Name={0}): WriteEntry failed to parse EntryType={1}", base.Name, text);
			}
			if (logEvent.Level >= LogLevel.Error)
			{
				return EventLogEntryType.Error;
			}
			if (logEvent.Level >= LogLevel.Warn)
			{
				return EventLogEntryType.Warning;
			}
			return EventLogEntryType.Information;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		internal string GetFixedSource()
		{
			SimpleLayout simpleLayout;
			if ((simpleLayout = this.Source as SimpleLayout) != null && simpleLayout.IsFixedText)
			{
				return simpleLayout.FixedText;
			}
			return null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		private EventLogTarget.IEventLogWrapper GetEventLog(LogEventInfo logEvent)
		{
			string text = this.RenderSource(logEvent);
			if (!this._eventLogWrapper.IsEventLogAssociated || !(this._eventLogWrapper.Log == this.Log) || !(this._eventLogWrapper.MachineName == this.MachineName) || !(this._eventLogWrapper.Source == text))
			{
				this._eventLogWrapper.AssociateNewEventLog(this.Log, this.MachineName, text);
			}
			return this._eventLogWrapper;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000DC66 File Offset: 0x0000BE66
		internal string RenderSource(LogEventInfo logEvent)
		{
			return base.RenderLogEvent(this.Source, logEvent);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private void CreateEventSourceIfNeeded(string fixedSource, bool alwaysThrowError)
		{
			if (string.IsNullOrEmpty(fixedSource))
			{
				InternalLogger.Debug<string>("EventLogTarget(Name={0}): Skipping creation of event source because it contains layout renderers", base.Name);
				return;
			}
			try
			{
				if (this._eventLogWrapper.SourceExists(fixedSource, this.MachineName))
				{
					if (!this._eventLogWrapper.LogNameFromSourceName(fixedSource, this.MachineName).Equals(this.Log, StringComparison.CurrentCultureIgnoreCase))
					{
						this._eventLogWrapper.DeleteEventSource(fixedSource, this.MachineName);
						EventSourceCreationData eventSourceCreationData = new EventSourceCreationData(fixedSource, this.Log)
						{
							MachineName = this.MachineName
						};
						this._eventLogWrapper.CreateEventSource(eventSourceCreationData);
					}
				}
				else
				{
					EventSourceCreationData eventSourceCreationData2 = new EventSourceCreationData(fixedSource, this.Log)
					{
						MachineName = this.MachineName
					};
					this._eventLogWrapper.CreateEventSource(eventSourceCreationData2);
				}
				if (this.MaxKilobytes != null)
				{
					long maximumKilobytes = this.GetEventLog(null).MaximumKilobytes;
					long? maxKilobytes = this.MaxKilobytes;
					if ((maximumKilobytes < maxKilobytes.GetValueOrDefault()) & (maxKilobytes != null))
					{
						this.GetEventLog(null).MaximumKilobytes = this.MaxKilobytes.Value;
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "EventLogTarget(Name={0}): Error when connecting to EventLog.", new object[] { base.Name });
				if (alwaysThrowError || LogManager.ThrowExceptions)
				{
					throw;
				}
			}
		}

		// Token: 0x040000C8 RID: 200
		internal const int EventLogMaxMessageLength = 16384;

		// Token: 0x040000C9 RID: 201
		private readonly EventLogTarget.IEventLogWrapper _eventLogWrapper;

		// Token: 0x040000D0 RID: 208
		private int _maxMessageLength;

		// Token: 0x040000D1 RID: 209
		private long? _maxKilobytes;

		// Token: 0x02000224 RID: 548
		internal interface IEventLogWrapper
		{
			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x060014F2 RID: 5362
			// (set) Token: 0x060014F3 RID: 5363
			string Source { get; set; }

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x060014F4 RID: 5364
			// (set) Token: 0x060014F5 RID: 5365
			string Log { get; set; }

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x060014F6 RID: 5366
			// (set) Token: 0x060014F7 RID: 5367
			string MachineName { get; set; }

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x060014F8 RID: 5368
			// (set) Token: 0x060014F9 RID: 5369
			long MaximumKilobytes { get; set; }

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x060014FA RID: 5370
			bool IsEventLogAssociated { get; }

			// Token: 0x060014FB RID: 5371
			void WriteEntry(string message, EventLogEntryType entryType, int eventId, short category);

			// Token: 0x060014FC RID: 5372
			void AssociateNewEventLog(string logName, string machineName, string source);

			// Token: 0x060014FD RID: 5373
			void DeleteEventSource(string source, string machineName);

			// Token: 0x060014FE RID: 5374
			bool SourceExists(string source, string machineName);

			// Token: 0x060014FF RID: 5375
			string LogNameFromSourceName(string source, string machineName);

			// Token: 0x06001500 RID: 5376
			void CreateEventSource(EventSourceCreationData sourceData);
		}

		// Token: 0x02000225 RID: 549
		private sealed class EventLogWrapper : EventLogTarget.IEventLogWrapper
		{
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001501 RID: 5377 RVA: 0x00037D6B File Offset: 0x00035F6B
			// (set) Token: 0x06001502 RID: 5378 RVA: 0x00037D78 File Offset: 0x00035F78
			public string Source
			{
				get
				{
					return this._windowsEventLog.Source;
				}
				set
				{
					this._windowsEventLog.Source = value;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06001503 RID: 5379 RVA: 0x00037D86 File Offset: 0x00035F86
			// (set) Token: 0x06001504 RID: 5380 RVA: 0x00037D93 File Offset: 0x00035F93
			public string Log
			{
				get
				{
					return this._windowsEventLog.Log;
				}
				set
				{
					this._windowsEventLog.Log = value;
				}
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x00037DA1 File Offset: 0x00035FA1
			// (set) Token: 0x06001506 RID: 5382 RVA: 0x00037DAE File Offset: 0x00035FAE
			public string MachineName
			{
				get
				{
					return this._windowsEventLog.MachineName;
				}
				set
				{
					this._windowsEventLog.MachineName = value;
				}
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x06001507 RID: 5383 RVA: 0x00037DBC File Offset: 0x00035FBC
			// (set) Token: 0x06001508 RID: 5384 RVA: 0x00037DC9 File Offset: 0x00035FC9
			public long MaximumKilobytes
			{
				get
				{
					return this._windowsEventLog.MaximumKilobytes;
				}
				set
				{
					this._windowsEventLog.MaximumKilobytes = value;
				}
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06001509 RID: 5385 RVA: 0x00037DD7 File Offset: 0x00035FD7
			public bool IsEventLogAssociated
			{
				get
				{
					return this._windowsEventLog != null;
				}
			}

			// Token: 0x0600150A RID: 5386 RVA: 0x00037DE2 File Offset: 0x00035FE2
			public void WriteEntry(string message, EventLogEntryType entryType, int eventId, short category)
			{
				this._windowsEventLog.WriteEntry(message, entryType, eventId, category);
			}

			// Token: 0x0600150B RID: 5387 RVA: 0x00037DF4 File Offset: 0x00035FF4
			public void AssociateNewEventLog(string logName, string machineName, string source)
			{
				this._windowsEventLog = new EventLog(logName, machineName, source);
			}

			// Token: 0x0600150C RID: 5388 RVA: 0x00037E04 File Offset: 0x00036004
			public void DeleteEventSource(string source, string machineName)
			{
				EventLog.DeleteEventSource(source, machineName);
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x00037E0D File Offset: 0x0003600D
			public bool SourceExists(string source, string machineName)
			{
				return EventLog.SourceExists(source, machineName);
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x00037E16 File Offset: 0x00036016
			public string LogNameFromSourceName(string source, string machineName)
			{
				return EventLog.LogNameFromSourceName(source, machineName);
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x00037E1F File Offset: 0x0003601F
			public void CreateEventSource(EventSourceCreationData sourceData)
			{
				EventLog.CreateEventSource(sourceData);
			}

			// Token: 0x040005EB RID: 1515
			private EventLog _windowsEventLog;
		}
	}
}
