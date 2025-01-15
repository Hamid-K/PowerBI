using System;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004C RID: 76
	public sealed class QueriesSettings
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007515 File Offset: 0x00005715
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000751D File Offset: 0x0000571D
		public bool TypeDetectionEnabled { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007526 File Offset: 0x00005726
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000752E File Offset: 0x0000572E
		public bool RelationshipImportEnabled { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007537 File Offset: 0x00005737
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000753F File Offset: 0x0000573F
		public bool RelationshipRefreshEnabled { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007548 File Offset: 0x00005748
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00007550 File Offset: 0x00005750
		public bool RunBackgroundAnalysis { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007559 File Offset: 0x00005759
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00007561 File Offset: 0x00005761
		public string Version { get; set; }
	}
}
