using System;
using System.ComponentModel;

namespace Model
{
	// Token: 0x0200005B RID: 91
	public sealed class Schedule
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000343B File Offset: 0x0000163B
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00003443 File Offset: 0x00001643
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000344C File Offset: 0x0000164C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00003454 File Offset: 0x00001654
		public string Name { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000345D File Offset: 0x0000165D
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00003465 File Offset: 0x00001665
		public ScheduleDefinition Definition { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000346E File Offset: 0x0000166E
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00003476 File Offset: 0x00001676
		public string Description { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000347F File Offset: 0x0000167F
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00003487 File Offset: 0x00001687
		public string Creator { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00003490 File Offset: 0x00001690
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00003498 File Offset: 0x00001698
		public DateTimeOffset NextRunTime { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000034A1 File Offset: 0x000016A1
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000034A9 File Offset: 0x000016A9
		public bool NextRunTimeSpecified { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000034B2 File Offset: 0x000016B2
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000034BA File Offset: 0x000016BA
		public DateTimeOffset LastRunTime { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000034C3 File Offset: 0x000016C3
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000034CB File Offset: 0x000016CB
		public bool LastRunTimeSpecified { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000034D4 File Offset: 0x000016D4
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000034DC File Offset: 0x000016DC
		public bool ReferencesPresent { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000034E5 File Offset: 0x000016E5
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000034ED File Offset: 0x000016ED
		public ScheduleStateEnum State { get; set; }
	}
}
