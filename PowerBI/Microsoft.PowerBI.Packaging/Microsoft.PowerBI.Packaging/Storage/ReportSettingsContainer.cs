using System;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000052 RID: 82
	public sealed class ReportSettingsContainer
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00007A4E File Offset: 0x00005C4E
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00007A56 File Offset: 0x00005C56
		public ReportSettings ReportSettings { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00007A5F File Offset: 0x00005C5F
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00007A67 File Offset: 0x00005C67
		public QueriesSettings QueriesSettings { get; set; }

		// Token: 0x06000280 RID: 640 RVA: 0x00007A70 File Offset: 0x00005C70
		public ReportSettingsContainer()
		{
			this.ReportSettings = new ReportSettings();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007A8A File Offset: 0x00005C8A
		public ReportSettingsContainer(ReportSettings reportSettings, QueriesSettings queriesSettings, int version)
		{
			this.ReportSettings = reportSettings;
			this.QueriesSettings = queriesSettings;
			this.Version = version;
		}

		// Token: 0x04000147 RID: 327
		public const int V1 = 1;

		// Token: 0x04000148 RID: 328
		public const int V2 = 2;

		// Token: 0x04000149 RID: 329
		public const int V3 = 3;

		// Token: 0x0400014A RID: 330
		public const int V4 = 4;

		// Token: 0x0400014B RID: 331
		public const int Latest = 4;

		// Token: 0x0400014E RID: 334
		public int Version = 1;
	}
}
