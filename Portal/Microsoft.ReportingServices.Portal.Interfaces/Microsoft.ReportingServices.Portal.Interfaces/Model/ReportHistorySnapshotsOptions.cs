using System;

namespace Model
{
	// Token: 0x02000026 RID: 38
	public class ReportHistorySnapshotsOptions
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000270C File Offset: 0x0000090C
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002714 File Offset: 0x00000914
		public bool ManualCreationEnabled { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000271D File Offset: 0x0000091D
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002725 File Offset: 0x00000925
		public bool KeepExecutionSnapshots { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000272E File Offset: 0x0000092E
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002736 File Offset: 0x00000936
		public bool UseDefaultSystemLimit { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000273F File Offset: 0x0000093F
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002747 File Offset: 0x00000947
		public int ScopedLimit { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002758 File Offset: 0x00000958
		public int SystemLimit { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002761 File Offset: 0x00000961
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002769 File Offset: 0x00000969
		public ScheduleReference Schedule { get; set; }
	}
}
