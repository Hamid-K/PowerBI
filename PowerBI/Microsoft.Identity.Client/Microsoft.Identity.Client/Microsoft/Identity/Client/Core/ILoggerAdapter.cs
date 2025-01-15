using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Core
{
	// Token: 0x0200022B RID: 555
	internal interface ILoggerAdapter
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060016B7 RID: 5815
		bool PiiLoggingEnabled { get; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060016B8 RID: 5816
		bool IsDefaultPlatformLoggingEnabled { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060016B9 RID: 5817
		string ClientName { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060016BA RID: 5818
		string ClientVersion { get; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060016BB RID: 5819
		IIdentityLogger IdentityLogger { get; }

		// Token: 0x060016BC RID: 5820
		bool IsLoggingEnabled(LogLevel logLevel);

		// Token: 0x060016BD RID: 5821
		void Log(LogLevel logLevel, string messageWithPii, string messageScrubbed);

		// Token: 0x060016BE RID: 5822
		DurationLogHelper LogBlockDuration(string measuredBlockName, LogLevel logLevel = LogLevel.Verbose);

		// Token: 0x060016BF RID: 5823
		DurationLogHelper LogMethodDuration(LogLevel logLevel = LogLevel.Verbose, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null);
	}
}
