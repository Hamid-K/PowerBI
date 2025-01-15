using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NLog.Fluent
{
	// Token: 0x0200016E RID: 366
	public class LogBuilder
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		[CLSCompliant(false)]
		public LogBuilder(ILogger logger)
			: this(logger, LogLevel.Debug)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		[CLSCompliant(false)]
		public LogBuilder(ILogger logger, LogLevel logLevel)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (logLevel == null)
			{
				throw new ArgumentNullException("logLevel");
			}
			this._logger = logger;
			this._logEvent = new LogEventInfo
			{
				LoggerName = logger.Name,
				Level = logLevel
			};
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0002C82E File Offset: 0x0002AA2E
		public LogEventInfo LogEventInfo
		{
			get
			{
				return this._logEvent;
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002C836 File Offset: 0x0002AA36
		public LogBuilder Exception(Exception exception)
		{
			this._logEvent.Exception = exception;
			return this;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0002C845 File Offset: 0x0002AA45
		public LogBuilder Level(LogLevel logLevel)
		{
			if (logLevel == null)
			{
				throw new ArgumentNullException("logLevel");
			}
			this._logEvent.Level = logLevel;
			return this;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0002C868 File Offset: 0x0002AA68
		public LogBuilder LoggerName(string loggerName)
		{
			this._logEvent.LoggerName = loggerName;
			return this;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0002C877 File Offset: 0x0002AA77
		public LogBuilder Message(string message)
		{
			this._logEvent.Message = message;
			return this;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0002C886 File Offset: 0x0002AA86
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(string format, object arg0)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0 };
			return this;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0002C8AA File Offset: 0x0002AAAA
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1 };
			return this;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0002C8D2 File Offset: 0x0002AAD2
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1, object arg2)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1, arg2 };
			return this;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0002C8FF File Offset: 0x0002AAFF
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1, arg2, arg3 };
			return this;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0002C931 File Offset: 0x0002AB31
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(string format, params object[] args)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0002C94C File Offset: 0x0002AB4C
		[MessageTemplateFormatMethod("format")]
		public LogBuilder Message(IFormatProvider provider, string format, params object[] args)
		{
			this._logEvent.FormatProvider = provider;
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0002C973 File Offset: 0x0002AB73
		public LogBuilder Property(object name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._logEvent.Properties[name] = value;
			return this;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0002C998 File Offset: 0x0002AB98
		public LogBuilder Properties(IDictionary properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			foreach (object obj in properties.Keys)
			{
				this._logEvent.Properties[obj] = properties[obj];
			}
			return this;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		public LogBuilder TimeStamp(DateTime timeStamp)
		{
			this._logEvent.TimeStamp = timeStamp;
			return this;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0002CA1B File Offset: 0x0002AC1B
		public LogBuilder StackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this._logEvent.SetStackTrace(stackTrace, userStackFrame);
			return this;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0002CA2C File Offset: 0x0002AC2C
		public void Write([CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (!this._logger.IsEnabled(this._logEvent.Level))
			{
				return;
			}
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logEvent.SetCallerInfo(null, callerMemberName, callerFilePath, callerLineNumber);
			this._logger.Log(this._logEvent);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		public void WriteIf(Func<bool> condition, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (condition == null || !condition() || !this._logger.IsEnabled(this._logEvent.Level))
			{
				return;
			}
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logEvent.SetCallerInfo(null, callerMemberName, callerFilePath, callerLineNumber);
			this._logger.Log(this._logEvent);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002CB34 File Offset: 0x0002AD34
		public void WriteIf(bool condition, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (!condition || !this._logger.IsEnabled(this._logEvent.Level))
			{
				return;
			}
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logEvent.SetCallerInfo(null, callerMemberName, callerFilePath, callerLineNumber);
			this._logger.Log(this._logEvent);
		}

		// Token: 0x0400049B RID: 1179
		private readonly LogEventInfo _logEvent;

		// Token: 0x0400049C RID: 1180
		private readonly ILogger _logger;
	}
}
