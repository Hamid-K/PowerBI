using System;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000465 RID: 1125
	[Obsolete("This API is deprecated.")]
	public class DirectoryConfiguration
	{
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x00077A12 File Offset: 0x00075C12
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x00077A1A File Offset: 0x00075C1A
		public string Path { get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00077A23 File Offset: 0x00075C23
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x00077A2B File Offset: 0x00075C2B
		public string Container { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x00077A34 File Offset: 0x00075C34
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x00077A3C File Offset: 0x00075C3C
		public int DirectoryQuotaInMB { get; set; }
	}
}
