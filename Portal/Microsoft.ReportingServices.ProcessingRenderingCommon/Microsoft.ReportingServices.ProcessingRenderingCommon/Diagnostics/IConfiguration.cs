using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000076 RID: 118
	public interface IConfiguration
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600034A RID: 842
		IRdlSandboxConfig RdlSandboxing { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600034B RID: 843
		bool ShowSubreportErrorDetails { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600034C RID: 844
		IMapTileServerConfiguration MapTileServerConfiguration { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600034D RID: 845
		ProcessingUpgradeState UpgradeState { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600034E RID: 846
		bool ProhibitSerializableValues { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600034F RID: 847
		bool UseSafeExpressionsRuntime { get; }
	}
}
