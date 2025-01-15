using System;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000253 RID: 595
	internal class IdentityLogger : IIdentityLogger
	{
		// Token: 0x060017F1 RID: 6129 RVA: 0x00050110 File Offset: 0x0004E310
		internal IdentityLogger(IIdentityLogger identityLogger, Guid correlationId, string clientName, string clientVersion, bool enablePiiLogging)
		{
			this._identityLogger = identityLogger;
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
			this._clientInformation = LoggerHelper.GetClientInfo(clientName, clientVersion);
			this._piiLoggingEnabled = enablePiiLogging;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00050174 File Offset: 0x0004E374
		public bool IsEnabled(EventLogLevel eventLevel)
		{
			return this._identityLogger.IsEnabled(eventLevel);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00050184 File Offset: 0x0004E384
		public void Log(LogEntry entry)
		{
			entry.Message = LoggerHelper.FormatLogMessage(entry.Message, this._piiLoggingEnabled, (!string.IsNullOrEmpty(entry.CorrelationId)) ? entry.CorrelationId : this._correlationId, this._clientInformation);
			this._identityLogger.Log(entry);
		}

		// Token: 0x04000A86 RID: 2694
		private readonly IIdentityLogger _identityLogger;

		// Token: 0x04000A87 RID: 2695
		private readonly string _correlationId;

		// Token: 0x04000A88 RID: 2696
		private readonly string _clientInformation;

		// Token: 0x04000A89 RID: 2697
		private readonly bool _piiLoggingEnabled;
	}
}
