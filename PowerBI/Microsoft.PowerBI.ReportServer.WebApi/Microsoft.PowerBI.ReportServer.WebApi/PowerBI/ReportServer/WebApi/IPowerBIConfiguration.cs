using System;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x02000009 RID: 9
	public interface IPowerBIConfiguration
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39
		bool ExportDataEnabled { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40
		bool ExportUnderlyingDataEnabled { get; }
	}
}
