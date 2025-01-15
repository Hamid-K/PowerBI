using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000009 RID: 9
	public sealed class EmptyTelemetryConfiguration : ITelemetryConfiguration
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028B2 File Offset: 0x00000AB2
		public List<ILoggerService> Loggers
		{
			get
			{
				return new List<ILoggerService>();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028B9 File Offset: 0x00000AB9
		public bool GetOrCreateAppDir(out DirectoryInfo dirInfo)
		{
			dirInfo = null;
			return false;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028C0 File Offset: 0x00000AC0
		public HostData HostData
		{
			get
			{
				return new HostData("Unknown", "00000000-0000-0000-0000-000000000000", "WinDesktop", "Unknown", "InProc", "Unknown", false, false, "Unknown", "Unknown", "Unknown", AppType.Unknown, "Unknown");
			}
		}
	}
}
