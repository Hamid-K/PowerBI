using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A9 RID: 169
	public class PushDataDataset
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00010D9C File Offset: 0x0000EF9C
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		public string id { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00010DAD File Offset: 0x0000EFAD
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00010DB5 File Offset: 0x0000EFB5
		public PushDataRetentionPolicy defaultRetentionPolicy { get; set; }
	}
}
