using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Core;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000251 RID: 593
	internal class CallbackIdentityLoggerAdapter : ILoggerAdapter
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0004FEF3 File Offset: 0x0004E0F3
		public bool PiiLoggingEnabled { get; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0004FEFB File Offset: 0x0004E0FB
		public bool IsDefaultPlatformLoggingEnabled { get; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0004FF03 File Offset: 0x0004E103
		public string ClientName { get; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0004FF0B File Offset: 0x0004E10B
		public string ClientVersion { get; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0004FF13 File Offset: 0x0004E113
		public IIdentityLogger IdentityLogger { get; }

		// Token: 0x060017E9 RID: 6121 RVA: 0x0004FF1B File Offset: 0x0004E11B
		public bool IsLoggingEnabled(LogLevel logLevel)
		{
			return this.IdentityLogger.IsEnabled(LoggerHelper.GetEventLogLevel(logLevel));
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0004FF30 File Offset: 0x0004E130
		internal CallbackIdentityLoggerAdapter(Guid correlationId, string clientName, string clientVersion, LogLevel logLevel, bool enablePiiLogging, bool isDefaultPlatformLoggingEnabled, LogCallback loggingCallback)
		{
			this.ClientName = clientName;
			this.ClientVersion = clientVersion;
			string text2;
			if (!correlationId.Equals(Guid.Empty))
			{
				string text = " - ";
				Guid guid = correlationId;
				text2 = text + guid.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			this._correlationId = text2;
			this.PiiLoggingEnabled = enablePiiLogging;
			this.IsDefaultPlatformLoggingEnabled = isDefaultPlatformLoggingEnabled;
			this.IdentityLogger = new CallbackIdentityLogger(loggingCallback, this._correlationId, clientName, clientVersion, enablePiiLogging, logLevel);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0004FFB0 File Offset: 0x0004E1B0
		public void Log(LogLevel logLevel, string messageWithPii, string messageScrubbed)
		{
			if (this.IsLoggingEnabled(logLevel))
			{
				string messageToLog = LoggerHelper.GetMessageToLog(messageWithPii, messageScrubbed, this.PiiLoggingEnabled);
				LogEntry logEntry = new LogEntry();
				logEntry.EventLogLevel = LoggerHelper.GetEventLogLevel(logLevel);
				logEntry.CorrelationId = this._correlationId;
				logEntry.Message = messageToLog;
				this.IdentityLogger.Log(logEntry);
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00050008 File Offset: 0x0004E208
		public static ILoggerAdapter Create(Guid correlationId, ApplicationConfiguration config, bool isDefaultPlatformLoggingEnabled = false)
		{
			return new CallbackIdentityLoggerAdapter(correlationId, ((config != null) ? config.ClientName : null) ?? string.Empty, ((config != null) ? config.ClientVersion : null) ?? string.Empty, (config != null) ? config.LogLevel : LogLevel.Verbose, config != null && config.EnablePiiLogging, (config != null) ? config.IsDefaultPlatformLoggingEnabled : isDefaultPlatformLoggingEnabled, (config != null) ? config.LoggingCallback : null);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00050075 File Offset: 0x0004E275
		public DurationLogHelper LogBlockDuration(string measuredBlockName, LogLevel logLevel = LogLevel.Verbose)
		{
			return new DurationLogHelper(this, measuredBlockName, logLevel);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0005007F File Offset: 0x0004E27F
		public DurationLogHelper LogMethodDuration(LogLevel logLevel = LogLevel.Verbose, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null)
		{
			return LoggerHelper.LogMethodDuration(this, logLevel, methodName, filePath);
		}

		// Token: 0x04000A7C RID: 2684
		private string _correlationId;
	}
}
