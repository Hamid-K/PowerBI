using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Core;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000256 RID: 598
	internal class NullLogger : ILoggerAdapter
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0005069F File Offset: 0x0004E89F
		public string ClientName { get; } = string.Empty;

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000506A7 File Offset: 0x0004E8A7
		public string ClientVersion { get; } = string.Empty;

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000506AF File Offset: 0x0004E8AF
		public Guid CorrelationId { get; } = Guid.Empty;

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000506B7 File Offset: 0x0004E8B7
		public bool PiiLoggingEnabled { get; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000506BF File Offset: 0x0004E8BF
		public string ClientInformation { get; } = string.Empty;

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000506C7 File Offset: 0x0004E8C7
		public bool IsDefaultPlatformLoggingEnabled { get; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000506CF File Offset: 0x0004E8CF
		public IIdentityLogger IdentityLogger { get; } = NullIdentityModelLogger.Instance;

		// Token: 0x06001811 RID: 6161 RVA: 0x000506D7 File Offset: 0x0004E8D7
		public bool IsLoggingEnabled(LogLevel logLevel)
		{
			return false;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000506DA File Offset: 0x0004E8DA
		public void Log(LogLevel logLevel, string messageWithPii, string messageScrubbed)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000506DC File Offset: 0x0004E8DC
		public DurationLogHelper LogBlockDuration(string measuredBlockName, LogLevel logLevel = LogLevel.Verbose)
		{
			return null;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000506DF File Offset: 0x0004E8DF
		public DurationLogHelper LogMethodDuration(LogLevel logLevel = LogLevel.Verbose, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null)
		{
			return null;
		}
	}
}
