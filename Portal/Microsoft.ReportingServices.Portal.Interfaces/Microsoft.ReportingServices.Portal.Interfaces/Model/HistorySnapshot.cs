using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x0200003B RID: 59
	public sealed class HistorySnapshot
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002C5D File Offset: 0x00000E5D
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002C65 File Offset: 0x00000E65
		[Key]
		public Guid Id { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002C6E File Offset: 0x00000E6E
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002C76 File Offset: 0x00000E76
		public string HistoryId { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002C7F File Offset: 0x00000E7F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002C87 File Offset: 0x00000E87
		public DateTime CreationDate { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002C90 File Offset: 0x00000E90
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002C98 File Offset: 0x00000E98
		public int Size { get; set; }
	}
}
