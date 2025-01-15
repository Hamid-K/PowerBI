using System;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020000DD RID: 221
	public class HistoryRow
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x000279C2 File Offset: 0x00025BC2
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x000279CA File Offset: 0x00025BCA
		public string MigrationId { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x000279D3 File Offset: 0x00025BD3
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x000279DB File Offset: 0x00025BDB
		public string ContextKey { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000279E4 File Offset: 0x00025BE4
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x000279EC File Offset: 0x00025BEC
		public byte[] Model { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000279F5 File Offset: 0x00025BF5
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000279FD File Offset: 0x00025BFD
		public string ProductVersion { get; set; }
	}
}
