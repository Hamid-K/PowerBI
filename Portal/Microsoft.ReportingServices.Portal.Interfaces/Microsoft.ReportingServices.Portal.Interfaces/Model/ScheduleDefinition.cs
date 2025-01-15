using System;

namespace Model
{
	// Token: 0x02000062 RID: 98
	public class ScheduleDefinition
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00003611 File Offset: 0x00001811
		public ScheduleDefinition()
		{
			this.Recurrence = new ScheduleRecurrence();
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000362C File Offset: 0x0000182C
		public DateTimeOffset StartDateTime { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003635 File Offset: 0x00001835
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000363D File Offset: 0x0000183D
		public DateTimeOffset EndDate { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003646 File Offset: 0x00001846
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000364E File Offset: 0x0000184E
		public bool EndDateSpecified { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00003657 File Offset: 0x00001857
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000365F File Offset: 0x0000185F
		public ScheduleRecurrence Recurrence { get; set; }
	}
}
