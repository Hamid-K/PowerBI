using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Telemetry
{
	// Token: 0x0200001B RID: 27
	internal sealed class RSTelemetryConfiguration : ITelemetryConfiguration
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003970 File Offset: 0x00001B70
		public RSTelemetryConfiguration(ILoggerService loggerService, string requestId, string clientSessionId)
		{
			this.Loggers = new List<ILoggerService>(new ILoggerService[] { loggerService });
			this.RequestId = requestId;
			this.ClientSessionId = clientSessionId;
			this.HostData = new HostData(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, true, string.Empty, string.Empty, string.Empty, AppType.Unknown, string.Empty);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000039E6 File Offset: 0x00001BE6
		public bool GetOrCreateAppDir(out DirectoryInfo dirInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000039F5 File Offset: 0x00001BF5
		public List<ILoggerService> Loggers { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000039FE File Offset: 0x00001BFE
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003A06 File Offset: 0x00001C06
		public HostData HostData { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003A0F File Offset: 0x00001C0F
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003A17 File Offset: 0x00001C17
		public string ClientSessionId { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003A28 File Offset: 0x00001C28
		public string RequestId { get; private set; }
	}
}
