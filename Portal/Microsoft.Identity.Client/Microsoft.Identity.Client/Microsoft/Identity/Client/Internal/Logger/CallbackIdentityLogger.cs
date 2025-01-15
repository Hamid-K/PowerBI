using System;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000250 RID: 592
	internal class CallbackIdentityLogger : IIdentityLogger
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x0004FE39 File Offset: 0x0004E039
		public CallbackIdentityLogger(LogCallback logCallback, string correlationId, string clientName, string clientVersion, bool enablePiiLogging, LogLevel minLogLevel)
		{
			this._correlationId = correlationId;
			this._clientInformation = LoggerHelper.GetClientInfo(clientName, clientVersion);
			this._piiLoggingEnabled = enablePiiLogging;
			this._logCallback = logCallback;
			this._minLogLevel = minLogLevel;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0004FE6D File Offset: 0x0004E06D
		public bool IsEnabled(EventLogLevel eventLevel)
		{
			return this._logCallback != null && CallbackIdentityLogger.GetLogLevel(eventLevel) <= this._minLogLevel;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004FE8C File Offset: 0x0004E08C
		public void Log(LogEntry entry)
		{
			string text = LoggerHelper.FormatLogMessage(entry.Message, this._piiLoggingEnabled, string.IsNullOrEmpty(entry.CorrelationId) ? entry.CorrelationId : this._correlationId, this._clientInformation);
			this._logCallback(CallbackIdentityLogger.GetLogLevel(entry.EventLogLevel), text, this._piiLoggingEnabled);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0004FEE9 File Offset: 0x0004E0E9
		private static LogLevel GetLogLevel(EventLogLevel eventLogLevel)
		{
			if (eventLogLevel == EventLogLevel.LogAlways)
			{
				return LogLevel.Always;
			}
			return (LogLevel)(eventLogLevel - 2);
		}

		// Token: 0x04000A77 RID: 2679
		private LogCallback _logCallback;

		// Token: 0x04000A78 RID: 2680
		private readonly string _correlationId;

		// Token: 0x04000A79 RID: 2681
		private readonly string _clientInformation;

		// Token: 0x04000A7A RID: 2682
		private readonly bool _piiLoggingEnabled;

		// Token: 0x04000A7B RID: 2683
		private readonly LogLevel _minLogLevel;
	}
}
