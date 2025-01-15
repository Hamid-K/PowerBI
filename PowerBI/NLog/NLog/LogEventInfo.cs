using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Internal;
using NLog.Layouts;
using NLog.MessageTemplates;
using NLog.Time;

namespace NLog
{
	// Token: 0x0200000B RID: 11
	public class LogEventInfo
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000023F2 File Offset: 0x000005F2
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000023F9 File Offset: 0x000005F9
		internal static LogMessageFormatter DefaultMessageFormatter { get; private set; } = LogMessageTemplateFormatter.DefaultAuto.MessageFormatter;

		// Token: 0x06000187 RID: 391 RVA: 0x00002401 File Offset: 0x00000601
		public LogEventInfo()
		{
			this.TimeStamp = TimeSource.Current.Time;
			this.SequenceID = Interlocked.Increment(ref LogEventInfo.globalSequenceId);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00002434 File Offset: 0x00000634
		public LogEventInfo(LogLevel level, string loggerName, [Localizable(false)] string message)
			: this(level, loggerName, null, message, null, null)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00002442 File Offset: 0x00000642
		public LogEventInfo(LogLevel level, string loggerName, [Localizable(false)] string message, IList<MessageTemplateParameter> messageTemplateParameters)
			: this(level, loggerName, null, message, null, null)
		{
			if (messageTemplateParameters != null && messageTemplateParameters.Count > 0)
			{
				this._properties = new PropertiesDictionary(messageTemplateParameters);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000246B File Offset: 0x0000066B
		public LogEventInfo(LogLevel level, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters)
			: this(level, loggerName, formatProvider, message, parameters, null)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000247C File Offset: 0x0000067C
		public LogEventInfo(LogLevel level, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters, Exception exception)
			: this()
		{
			this.Level = level;
			this.LoggerName = loggerName;
			this.Message = message;
			this.Parameters = parameters;
			this.FormatProvider = formatProvider;
			this.Exception = exception;
			if (LogEventInfo.NeedToPreformatMessage(parameters))
			{
				this.CalcFormattedMessage();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000024CB File Offset: 0x000006CB
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000024D3 File Offset: 0x000006D3
		public int SequenceID { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000024DC File Offset: 0x000006DC
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000024E4 File Offset: 0x000006E4
		public DateTime TimeStamp { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000024ED File Offset: 0x000006ED
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000024F5 File Offset: 0x000006F5
		public LogLevel Level { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000024FE File Offset: 0x000006FE
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002506 File Offset: 0x00000706
		[CanBeNull]
		internal CallSiteInformation CallSiteInformation { get; private set; }

		// Token: 0x06000194 RID: 404 RVA: 0x00002510 File Offset: 0x00000710
		[NotNull]
		internal CallSiteInformation GetCallSiteInformationInternal()
		{
			CallSiteInformation callSiteInformation;
			if ((callSiteInformation = this.CallSiteInformation) == null)
			{
				callSiteInformation = (this.CallSiteInformation = new CallSiteInformation());
			}
			return callSiteInformation;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00002535 File Offset: 0x00000735
		public bool HasStackTrace
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				return ((callSiteInformation != null) ? callSiteInformation.StackTrace : null) != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000254C File Offset: 0x0000074C
		public StackFrame UserStackFrame
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return null;
				}
				return callSiteInformation.UserStackFrame;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00002560 File Offset: 0x00000760
		public int UserStackFrameNumber
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				int? num = ((callSiteInformation != null) ? callSiteInformation.UserStackFrameNumberLegacy : null);
				if (num != null)
				{
					return num.GetValueOrDefault();
				}
				CallSiteInformation callSiteInformation2 = this.CallSiteInformation;
				if (callSiteInformation2 == null)
				{
					return 0;
				}
				return callSiteInformation2.UserStackFrameNumber;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000025AA File Offset: 0x000007AA
		public StackTrace StackTrace
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return null;
				}
				return callSiteInformation.StackTrace;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000025BD File Offset: 0x000007BD
		public string CallerClassName
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return null;
				}
				return callSiteInformation.GetCallerClassName(null, true, true, true);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000025D4 File Offset: 0x000007D4
		public string CallerMemberName
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return null;
				}
				return callSiteInformation.GetCallerMemberName(null, false, true, true);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000025EB File Offset: 0x000007EB
		public string CallerFilePath
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return null;
				}
				return callSiteInformation.GetCallerFilePath(0);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000025FF File Offset: 0x000007FF
		public int CallerLineNumber
		{
			get
			{
				CallSiteInformation callSiteInformation = this.CallSiteInformation;
				if (callSiteInformation == null)
				{
					return 0;
				}
				return callSiteInformation.GetCallerLineNumber(0);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002613 File Offset: 0x00000813
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000261B File Offset: 0x0000081B
		[CanBeNull]
		public Exception Exception { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000262C File Offset: 0x0000082C
		[CanBeNull]
		public string LoggerName { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002638 File Offset: 0x00000838
		[Obsolete("This property should not be used. Marked obsolete on NLog 2.0")]
		public string LoggerShortName
		{
			get
			{
				if (this.LoggerName == null)
				{
					return this.LoggerName;
				}
				int num = this.LoggerName.LastIndexOf('.');
				if (num >= 0)
				{
					return this.LoggerName.Substring(num + 1);
				}
				return this.LoggerName;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000267B File Offset: 0x0000087B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00002684 File Offset: 0x00000884
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				bool flag = this.ResetMessageTemplateParameters();
				this._message = value;
				this.ResetFormattedMessage(flag);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000026A6 File Offset: 0x000008A6
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000026B0 File Offset: 0x000008B0
		public object[] Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				bool flag = this.ResetMessageTemplateParameters();
				this._parameters = value;
				this.ResetFormattedMessage(flag);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000026D2 File Offset: 0x000008D2
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000026DA File Offset: 0x000008DA
		public IFormatProvider FormatProvider
		{
			get
			{
				return this._formatProvider;
			}
			set
			{
				if (this._formatProvider != value)
				{
					this._formatProvider = value;
					this.ResetFormattedMessage(false);
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000026F3 File Offset: 0x000008F3
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000026FB File Offset: 0x000008FB
		public LogMessageFormatter MessageFormatter
		{
			get
			{
				return this._messageFormatter;
			}
			set
			{
				this._messageFormatter = value ?? LogEventInfo.StringFormatMessageFormatter;
				this.ResetFormattedMessage(false);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00002714 File Offset: 0x00000914
		public string FormattedMessage
		{
			get
			{
				if (this._formattedMessage == null)
				{
					this.CalcFormattedMessage();
				}
				return this._formattedMessage;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000272A File Offset: 0x0000092A
		public bool HasProperties
		{
			get
			{
				if (this._properties != null)
				{
					return this._properties.Count > 0;
				}
				PropertiesDictionary propertiesDictionary = this.CreateOrUpdatePropertiesInternal(false, null);
				return propertiesDictionary != null && propertiesDictionary.Count > 0;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002759 File Offset: 0x00000959
		public IDictionary<object, object> Properties
		{
			get
			{
				return this.CreateOrUpdatePropertiesInternal(true, null);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00002764 File Offset: 0x00000964
		internal PropertiesDictionary CreateOrUpdatePropertiesInternal(bool forceCreate = true, IList<MessageTemplateParameter> templateParameters = null)
		{
			PropertiesDictionary propertiesDictionary = this._properties;
			if (propertiesDictionary == null)
			{
				if (forceCreate || (templateParameters != null && templateParameters.Count > 0) || (templateParameters == null && this.HasMessageTemplateParameters))
				{
					propertiesDictionary = new PropertiesDictionary(templateParameters);
					Interlocked.CompareExchange<PropertiesDictionary>(ref this._properties, propertiesDictionary, null);
					if (templateParameters == null && (!forceCreate || this.HasMessageTemplateParameters))
					{
						this.CalcFormattedMessage();
					}
				}
			}
			else if (templateParameters != null)
			{
				propertiesDictionary.MessageProperties = templateParameters;
			}
			return this._properties;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000027D0 File Offset: 0x000009D0
		internal bool HasMessageTemplateParameters
		{
			get
			{
				if (this._formattedMessage == null)
				{
					LogMessageFormatter messageFormatter = this._messageFormatter;
					ILogMessageFormatter logMessageFormatter = ((messageFormatter != null) ? messageFormatter.Target : null) as ILogMessageFormatter;
					return logMessageFormatter != null && logMessageFormatter.HasProperties(this);
				}
				return false;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002800 File Offset: 0x00000A00
		public MessageTemplateParameters MessageTemplateParameters
		{
			get
			{
				if (this._properties != null && this._properties.MessageProperties.Count > 0)
				{
					return new MessageTemplateParameters(this._properties.MessageProperties, this._message, this._parameters);
				}
				return new MessageTemplateParameters(this._message, this._parameters);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00002856 File Offset: 0x00000A56
		[Obsolete("Use LogEventInfo.Properties instead.  Marked obsolete on NLog 2.0", true)]
		public IDictionary Context
		{
			get
			{
				return this.CreateOrUpdatePropertiesInternal(true, null).EventContext;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00002865 File Offset: 0x00000A65
		public static LogEventInfo CreateNullEvent()
		{
			return new LogEventInfo(LogLevel.Off, string.Empty, string.Empty);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000287B File Offset: 0x00000A7B
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, [Localizable(false)] string message)
		{
			return new LogEventInfo(logLevel, loggerName, null, message, null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00002887 File Offset: 0x00000A87
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters)
		{
			return new LogEventInfo(logLevel, loggerName, formatProvider, message, parameters);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00002894 File Offset: 0x00000A94
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, IFormatProvider formatProvider, object message)
		{
			Exception ex = message as Exception;
			LogEventInfo logEventInfo;
			if (ex == null && (logEventInfo = message as LogEventInfo) != null)
			{
				logEventInfo.LoggerName = loggerName;
				logEventInfo.Level = logLevel;
				logEventInfo.FormatProvider = formatProvider ?? logEventInfo.FormatProvider;
				return logEventInfo;
			}
			return new LogEventInfo(logLevel, loggerName, formatProvider, "{0}", new object[] { message }, ex);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000028EE File Offset: 0x00000AEE
		[Obsolete("use Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, string message) instead. Marked obsolete before v4.3.11")]
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, [Localizable(false)] string message, Exception exception)
		{
			return new LogEventInfo(logLevel, loggerName, null, message, null, exception);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000028FB File Offset: 0x00000AFB
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message)
		{
			return LogEventInfo.Create(logLevel, loggerName, exception, formatProvider, message, null);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00002909 File Offset: 0x00000B09
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters)
		{
			return new LogEventInfo(logLevel, loggerName, formatProvider, message, parameters, exception);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00002918 File Offset: 0x00000B18
		public AsyncLogEventInfo WithContinuation(AsyncContinuation asyncContinuation)
		{
			return new AsyncLogEventInfo(this, asyncContinuation);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00002921 File Offset: 0x00000B21
		public override string ToString()
		{
			return string.Format("Log Event: Logger='{0}' Level={1} Message='{2}' SequenceID={3}", new object[] { this.LoggerName, this.Level, this.FormattedMessage, this.SequenceID });
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000295C File Offset: 0x00000B5C
		public void SetStackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this.GetCallSiteInformationInternal().SetStackTrace(stackTrace, userStackFrame, null);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000297F File Offset: 0x00000B7F
		public void SetCallerInfo(string callerClassName, string callerMemberName, string callerFilePath, int callerLineNumber)
		{
			this.GetCallSiteInformationInternal().SetCallerInfo(callerClassName, callerMemberName, callerFilePath, callerLineNumber);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00002994 File Offset: 0x00000B94
		internal void AddCachedLayoutValue(Layout layout, object value)
		{
			if (this._layoutCache == null)
			{
				Dictionary<Layout, object> dictionary = new Dictionary<Layout, object>();
				dictionary[layout] = value;
				if (Interlocked.CompareExchange<IDictionary<Layout, object>>(ref this._layoutCache, dictionary, null) == null)
				{
					return;
				}
			}
			IDictionary<Layout, object> layoutCache = this._layoutCache;
			lock (layoutCache)
			{
				this._layoutCache[layout] = value;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00002A04 File Offset: 0x00000C04
		internal bool TryGetCachedLayoutValue(Layout layout, out object value)
		{
			if (this._layoutCache == null)
			{
				value = null;
				return false;
			}
			IDictionary<Layout, object> layoutCache = this._layoutCache;
			bool flag2;
			lock (layoutCache)
			{
				if (this._layoutCache.Count == 0)
				{
					value = null;
					flag2 = false;
				}
				else
				{
					flag2 = this._layoutCache.TryGetValue(layout, out value);
				}
			}
			return flag2;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002A70 File Offset: 0x00000C70
		private static bool NeedToPreformatMessage(object[] parameters)
		{
			if (parameters == null || parameters.Length == 0)
			{
				return false;
			}
			if (parameters.Length > 5)
			{
				return true;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (!LogEventInfo.IsSafeToDeferFormatting(parameters[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00002AA9 File Offset: 0x00000CA9
		private static bool IsSafeToDeferFormatting(object value)
		{
			return Convert.GetTypeCode(value) != TypeCode.Object;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00002AB8 File Offset: 0x00000CB8
		internal bool IsLogEventMutableSafe()
		{
			if (this.Exception != null || this._formattedMessage != null)
			{
				return false;
			}
			PropertiesDictionary propertiesDictionary = this.CreateOrUpdatePropertiesInternal(false, null);
			if (propertiesDictionary == null || propertiesDictionary.Count == 0)
			{
				return true;
			}
			if (propertiesDictionary.Count > 5)
			{
				return false;
			}
			int count = propertiesDictionary.Count;
			object[] parameters = this._parameters;
			int? num = ((parameters != null) ? new int?(parameters.Length) : null);
			return (((count == num.GetValueOrDefault()) & (num != null)) && propertiesDictionary.Count == propertiesDictionary.MessageProperties.Count) || LogEventInfo.HasImmutableProperties(propertiesDictionary);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00002B4C File Offset: 0x00000D4C
		private static bool HasImmutableProperties(PropertiesDictionary properties)
		{
			if (properties.Count != properties.MessageProperties.Count)
			{
				foreach (KeyValuePair<object, object> keyValuePair in properties)
				{
					if (!LogEventInfo.IsSafeToDeferFormatting(keyValuePair.Value))
					{
						return false;
					}
				}
				return true;
			}
			for (int i = 0; i < properties.MessageProperties.Count; i++)
			{
				if (!LogEventInfo.IsSafeToDeferFormatting(properties.MessageProperties[i].Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00002BEC File Offset: 0x00000DEC
		internal bool CanLogEventDeferMessageFormat()
		{
			if (this._formattedMessage != null)
			{
				return false;
			}
			if (this._parameters == null || this._parameters.Length == 0)
			{
				return false;
			}
			string message = this._message;
			return message != null && message.Length < 256 && this.MessageFormatter == LogMessageTemplateFormatter.DefaultAuto.MessageFormatter;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00002C46 File Offset: 0x00000E46
		private static string GetStringFormatMessageFormatter(LogEventInfo logEvent)
		{
			if (logEvent.Parameters == null || logEvent.Parameters.Length == 0)
			{
				return logEvent.Message;
			}
			return string.Format(logEvent.FormatProvider ?? CultureInfo.CurrentCulture, logEvent.Message, logEvent.Parameters);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00002C80 File Offset: 0x00000E80
		private void CalcFormattedMessage()
		{
			try
			{
				this._formattedMessage = this._messageFormatter(this);
			}
			catch (Exception ex)
			{
				this._formattedMessage = this.Message;
				InternalLogger.Warn(ex, "Error when formatting a message.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal void AppendFormattedMessage(ILogMessageFormatter messageFormatter, StringBuilder builder)
		{
			if (this._formattedMessage != null)
			{
				builder.Append(this._formattedMessage);
				return;
			}
			int length = builder.Length;
			try
			{
				messageFormatter.AppendFormattedMessage(this, builder);
			}
			catch (Exception ex)
			{
				builder.Length = length;
				builder.Append(this._message ?? string.Empty);
				InternalLogger.Warn(ex, "Error when formatting a message.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00002D4C File Offset: 0x00000F4C
		private void ResetFormattedMessage(bool rebuildMessageTemplateParameters)
		{
			this._formattedMessage = null;
			if (rebuildMessageTemplateParameters && this.HasMessageTemplateParameters)
			{
				this.CalcFormattedMessage();
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00002D66 File Offset: 0x00000F66
		private bool ResetMessageTemplateParameters()
		{
			if (this._properties != null)
			{
				if (this.HasMessageTemplateParameters)
				{
					this._properties.MessageProperties = null;
				}
				return this._properties.MessageProperties.Count == 0;
			}
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00002D9C File Offset: 0x00000F9C
		internal static void SetDefaultMessageFormatter(bool? mode)
		{
			bool? flag = mode;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				InternalLogger.Info("Message Template Format always enabled");
				LogEventInfo.DefaultMessageFormatter = LogMessageTemplateFormatter.Default.MessageFormatter;
				return;
			}
			flag = mode;
			flag2 = false;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				InternalLogger.Info("Message Template String Format always enabled");
				LogEventInfo.DefaultMessageFormatter = LogEventInfo.StringFormatMessageFormatter;
				return;
			}
			InternalLogger.Info("Message Template Auto Format enabled");
			LogEventInfo.DefaultMessageFormatter = LogMessageTemplateFormatter.DefaultAuto.MessageFormatter;
		}

		// Token: 0x04000004 RID: 4
		public static readonly DateTime ZeroDate = DateTime.UtcNow;

		// Token: 0x04000005 RID: 5
		internal static readonly LogMessageFormatter StringFormatMessageFormatter = new LogMessageFormatter(LogEventInfo.GetStringFormatMessageFormatter);

		// Token: 0x04000007 RID: 7
		private static int globalSequenceId;

		// Token: 0x04000008 RID: 8
		private string _formattedMessage;

		// Token: 0x04000009 RID: 9
		private string _message;

		// Token: 0x0400000A RID: 10
		private object[] _parameters;

		// Token: 0x0400000B RID: 11
		private IFormatProvider _formatProvider;

		// Token: 0x0400000C RID: 12
		private LogMessageFormatter _messageFormatter = LogEventInfo.DefaultMessageFormatter;

		// Token: 0x0400000D RID: 13
		private IDictionary<Layout, object> _layoutCache;

		// Token: 0x0400000E RID: 14
		private PropertiesDictionary _properties;
	}
}
