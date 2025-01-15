using System;

namespace Model
{
	// Token: 0x0200001C RID: 28
	public class ScheduleRecurrence
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000024F7 File Offset: 0x000006F7
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000024FF File Offset: 0x000006FF
		public MinuteRecurrence MinuteRecurrence { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002508 File Offset: 0x00000708
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002510 File Offset: 0x00000710
		public DailyRecurrence DailyRecurrence { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002519 File Offset: 0x00000719
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002521 File Offset: 0x00000721
		public WeeklyRecurrence WeeklyRecurrence { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000252A File Offset: 0x0000072A
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002532 File Offset: 0x00000732
		public MonthlyRecurrence MonthlyRecurrence { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000253B File Offset: 0x0000073B
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002543 File Offset: 0x00000743
		public MonthlyDOWRecurrence MonthlyDOWRecurrence { get; set; }
	}
}
