using System;
using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x0200046A RID: 1130
	[Obsolete("This API is deprecated.")]
	public class WindowsEventLogsBufferConfiguration : DiagnosticDataBufferConfiguration
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x00077A87 File Offset: 0x00075C87
		// (set) Token: 0x06002755 RID: 10069 RVA: 0x00077A8F File Offset: 0x00075C8F
		public LogLevel ScheduledTransferLogLevelFilter { get; set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x00077A98 File Offset: 0x00075C98
		public IList<string> DataSources
		{
			get
			{
				return new List<string>();
			}
		}
	}
}
