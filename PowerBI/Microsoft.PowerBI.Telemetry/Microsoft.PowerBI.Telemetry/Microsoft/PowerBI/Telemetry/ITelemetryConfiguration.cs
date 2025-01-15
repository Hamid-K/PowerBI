using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000019 RID: 25
	public interface ITelemetryConfiguration
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126
		List<ILoggerService> Loggers { get; }

		// Token: 0x0600007F RID: 127
		bool GetOrCreateAppDir(out DirectoryInfo dirInfo);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000080 RID: 128
		HostData HostData { get; }
	}
}
