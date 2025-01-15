using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200000B RID: 11
	public class DatasourceTelemetryInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003BAE File Offset: 0x00001DAE
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public string Type { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003BBF File Offset: 0x00001DBF
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003BC7 File Offset: 0x00001DC7
		public string Kind { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003BD0 File Offset: 0x00001DD0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public string AuthType { get; set; }
	}
}
