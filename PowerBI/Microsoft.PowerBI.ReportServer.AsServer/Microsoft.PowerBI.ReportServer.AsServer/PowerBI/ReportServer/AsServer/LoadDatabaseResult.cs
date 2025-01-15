using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200001C RID: 28
	public class LoadDatabaseResult
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000436F File Offset: 0x0000256F
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00004377 File Offset: 0x00002577
		public bool Loaded { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004380 File Offset: 0x00002580
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004388 File Offset: 0x00002588
		public long DatabaseId { get; set; }
	}
}
