using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000052 RID: 82
	public sealed class ReportHistorySnapshot
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00003211 File Offset: 0x00001411
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00003219 File Offset: 0x00001419
		[Key]
		public string HistoryId { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00003222 File Offset: 0x00001422
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000322A File Offset: 0x0000142A
		public DateTime CreationDate { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00003233 File Offset: 0x00001433
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000323B File Offset: 0x0000143B
		public int Size { get; set; }
	}
}
