using System;

namespace Model
{
	// Token: 0x02000057 RID: 87
	public class MonthlyDOWRecurrence
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00003309 File Offset: 0x00001509
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00003311 File Offset: 0x00001511
		public WeekNumberEnum WhichWeek { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000331A File Offset: 0x0000151A
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00003322 File Offset: 0x00001522
		public bool WhichWeekSpecified { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000332B File Offset: 0x0000152B
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00003333 File Offset: 0x00001533
		public DaysOfWeekSelector DaysOfWeek { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000333C File Offset: 0x0000153C
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00003344 File Offset: 0x00001544
		public MonthsOfYearSelector MonthsOfYear { get; set; }
	}
}
