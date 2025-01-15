using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Core;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000254 RID: 596
	internal class IdentityLoggerAdapter : ILoggerAdapter
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000501D5 File Offset: 0x0004E3D5
		public bool PiiLoggingEnabled { get; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x000501DD File Offset: 0x0004E3DD
		public bool IsDefaultPlatformLoggingEnabled { get; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x000501E5 File Offset: 0x0004E3E5
		public string ClientName { get; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x000501ED File Offset: 0x0004E3ED
		public string ClientVersion { get; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000501F5 File Offset: 0x0004E3F5
		public IIdentityLogger IdentityLogger { get; }

		// Token: 0x060017F9 RID: 6137 RVA: 0x00050200 File Offset: 0x0004E400
		internal IdentityLoggerAdapter(IIdentityLogger identityLogger, Guid correlationId, string clientName, string clientVersion, bool enablePiiLogging)
		{
			this.ClientName = clientName;
			this.ClientVersion = clientVersion;
			this.IdentityLogger = new IdentityLogger(identityLogger, correlationId, clientName, clientVersion, enablePiiLogging);
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
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00050270 File Offset: 0x0004E470
		public static ILoggerAdapter Create(Guid correlationId, ApplicationConfiguration config)
		{
			return new IdentityLoggerAdapter((config != null) ? config.IdentityLogger : null, correlationId, ((config != null) ? config.ClientName : null) ?? string.Empty, ((config != null) ? config.ClientVersion : null) ?? string.Empty, config != null && config.EnablePiiLogging);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000502C8 File Offset: 0x0004E4C8
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

		// Token: 0x060017FC RID: 6140 RVA: 0x0005031D File Offset: 0x0004E51D
		public bool IsLoggingEnabled(LogLevel logLevel)
		{
			return this.IdentityLogger.IsEnabled(LoggerHelper.GetEventLogLevel(logLevel));
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00050330 File Offset: 0x0004E530
		public DurationLogHelper LogBlockDuration(string measuredBlockName, LogLevel logLevel = LogLevel.Verbose)
		{
			return new DurationLogHelper(this, measuredBlockName, logLevel);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0005033A File Offset: 0x0004E53A
		public DurationLogHelper LogMethodDuration(LogLevel logLevel = LogLevel.Verbose, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null)
		{
			return LoggerHelper.LogMethodDuration(this, logLevel, methodName, filePath);
		}

		// Token: 0x04000A8A RID: 2698
		private string _correlationId;
	}
}
