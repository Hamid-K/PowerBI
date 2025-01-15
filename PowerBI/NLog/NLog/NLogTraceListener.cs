using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Xml;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200001D RID: 29
	public class NLogTraceListener : TraceListener
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000089A7 File Offset: 0x00006BA7
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x000089B5 File Offset: 0x00006BB5
		public LogFactory LogFactory
		{
			get
			{
				this.InitAttributes();
				return this._logFactory;
			}
			set
			{
				this._logFactory = value;
				this._attributesLoaded = true;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000089C5 File Offset: 0x00006BC5
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x000089D3 File Offset: 0x00006BD3
		public LogLevel DefaultLogLevel
		{
			get
			{
				this.InitAttributes();
				return this._defaultLogLevel;
			}
			set
			{
				this._defaultLogLevel = value;
				this._attributesLoaded = true;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000089E3 File Offset: 0x00006BE3
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x000089F1 File Offset: 0x00006BF1
		public LogLevel ForceLogLevel
		{
			get
			{
				this.InitAttributes();
				return this._forceLogLevel;
			}
			set
			{
				this._forceLogLevel = value;
				this._attributesLoaded = true;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00008A01 File Offset: 0x00006C01
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00008A0F File Offset: 0x00006C0F
		public bool DisableFlush
		{
			get
			{
				this.InitAttributes();
				return this._disableFlush;
			}
			set
			{
				this._disableFlush = value;
				this._attributesLoaded = true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00008A1F File Offset: 0x00006C1F
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00008A22 File Offset: 0x00006C22
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00008A30 File Offset: 0x00006C30
		public bool AutoLoggerName
		{
			get
			{
				this.InitAttributes();
				return this._autoLoggerName;
			}
			set
			{
				this._autoLoggerName = value;
				this._attributesLoaded = true;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00008A40 File Offset: 0x00006C40
		public override void Write(string message)
		{
			this.ProcessLogEventInfo(this.DefaultLogLevel, null, message, null, null, new TraceEventType?(TraceEventType.Resume), null);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00008A78 File Offset: 0x00006C78
		public override void WriteLine(string message)
		{
			this.ProcessLogEventInfo(this.DefaultLogLevel, null, message, null, null, new TraceEventType?(TraceEventType.Resume), null);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public override void Close()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public override void Fail(string message)
		{
			this.ProcessLogEventInfo(LogLevel.Error, null, message, null, null, new TraceEventType?(TraceEventType.Error), null);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public override void Fail(string message, string detailMessage)
		{
			this.ProcessLogEventInfo(LogLevel.Error, null, message + " " + detailMessage, null, null, new TraceEventType?(TraceEventType.Error), null);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00008B26 File Offset: 0x00006D26
		public override void Flush()
		{
			if (!this.DisableFlush)
			{
				if (this.LogFactory != null)
				{
					this.LogFactory.Flush();
					return;
				}
				LogManager.Flush();
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00008B4C File Offset: 0x00006D4C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			this.TraceData(eventCache, source, eventType, id, new object[] { data });
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00008B70 File Offset: 0x00006D70
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, string.Empty, null, null, data))
			{
				return;
			}
			string text = string.Empty;
			if (data != null && data.Length != 0)
			{
				if (data.Length == 1)
				{
					text = "{0}";
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(data.Length * 5 - 2);
					for (int i = 0; i < data.Length; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append('{');
						stringBuilder.AppendInvariant(i);
						stringBuilder.Append('}');
					}
					text = stringBuilder.ToString();
				}
			}
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, text, data, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00008C34 File Offset: 0x00006E34
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, string.Empty, null, null, null))
			{
				return;
			}
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, string.Empty, null, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00008C90 File Offset: 0x00006E90
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
			{
				return;
			}
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, format, args, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
			{
				return;
			}
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, message, null, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00008D3C File Offset: 0x00006F3C
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, TraceEventType.Transfer, id, message, null, null, null))
			{
				return;
			}
			this.ProcessLogEventInfo(LogLevel.Debug, source, message, null, new int?(id), new TraceEventType?(TraceEventType.Transfer), new Guid?(relatedActivityId));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008D92 File Offset: 0x00006F92
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "defaultLogLevel", "autoLoggerName", "forceLogLevel", "disableFlush" };
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008DBC File Offset: 0x00006FBC
		private static LogLevel TranslateLogLevel(TraceEventType eventType)
		{
			switch (eventType)
			{
			case TraceEventType.Critical:
				return LogLevel.Fatal;
			case TraceEventType.Error:
				return LogLevel.Error;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				return LogLevel.Warn;
			default:
				if (eventType == TraceEventType.Information)
				{
					return LogLevel.Info;
				}
				if (eventType == TraceEventType.Verbose)
				{
					return LogLevel.Trace;
				}
				break;
			}
			return LogLevel.Debug;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00008E10 File Offset: 0x00007010
		protected virtual void ProcessLogEventInfo(LogLevel logLevel, string loggerName, [Localizable(false)] string message, object[] arguments, int? eventId, TraceEventType? eventType, Guid? relatedActiviyId)
		{
			StackTrace stackTrace = (this.AutoLoggerName ? new StackTrace() : null);
			int num;
			ILogger logger = this.GetLogger(loggerName, stackTrace, out num);
			logLevel = this._forceLogLevel ?? logLevel;
			if (!logger.IsEnabled(logLevel))
			{
				return;
			}
			LogEventInfo logEventInfo = new LogEventInfo();
			logEventInfo.LoggerName = logger.Name;
			logEventInfo.Level = logLevel;
			if (eventType != null)
			{
				logEventInfo.Properties.Add("EventType", eventType.Value);
			}
			if (relatedActiviyId != null)
			{
				logEventInfo.Properties.Add("RelatedActivityID", relatedActiviyId.Value);
			}
			logEventInfo.Message = message;
			logEventInfo.Parameters = arguments;
			logEventInfo.Level = this._forceLogLevel ?? logLevel;
			if (eventId != null)
			{
				logEventInfo.Properties.Add("EventID", eventId.Value);
			}
			if (stackTrace != null && num >= 0)
			{
				logEventInfo.SetStackTrace(stackTrace, num);
			}
			logger.Log(logEventInfo);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00008F10 File Offset: 0x00007110
		private ILogger GetLogger(string loggerName, StackTrace stackTrace, out int userFrameIndex)
		{
			loggerName = (loggerName ?? this.Name) ?? string.Empty;
			userFrameIndex = -1;
			if (stackTrace != null)
			{
				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					loggerName = StackTraceUsageUtils.LookupClassNameFromStackFrame(stackTrace.GetFrame(i));
					if (!string.IsNullOrEmpty(loggerName))
					{
						userFrameIndex = i;
						break;
					}
				}
			}
			if (this.LogFactory != null)
			{
				return this.LogFactory.GetLogger(loggerName);
			}
			return LogManager.GetLogger(loggerName);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00008F80 File Offset: 0x00007180
		private void InitAttributes()
		{
			if (!this._attributesLoaded)
			{
				this._attributesLoaded = true;
				if (Trace.AutoFlush)
				{
					this._disableFlush = true;
				}
				foreach (object obj in base.Attributes)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					string text2 = (string)dictionaryEntry.Value;
					string text3 = text.ToUpperInvariant();
					if (!(text3 == "DEFAULTLOGLEVEL"))
					{
						if (!(text3 == "FORCELOGLEVEL"))
						{
							if (!(text3 == "AUTOLOGGERNAME"))
							{
								if (text3 == "DISABLEFLUSH")
								{
									this._disableFlush = bool.Parse(text2);
								}
							}
							else
							{
								this.AutoLoggerName = XmlConvert.ToBoolean(text2);
							}
						}
						else
						{
							this._forceLogLevel = LogLevel.FromString(text2);
						}
					}
					else
					{
						this._defaultLogLevel = LogLevel.FromString(text2);
					}
				}
			}
		}

		// Token: 0x04000049 RID: 73
		private LogFactory _logFactory;

		// Token: 0x0400004A RID: 74
		private LogLevel _defaultLogLevel = LogLevel.Debug;

		// Token: 0x0400004B RID: 75
		private bool _attributesLoaded;

		// Token: 0x0400004C RID: 76
		private bool _autoLoggerName;

		// Token: 0x0400004D RID: 77
		private LogLevel _forceLogLevel;

		// Token: 0x0400004E RID: 78
		private bool _disableFlush;
	}
}
