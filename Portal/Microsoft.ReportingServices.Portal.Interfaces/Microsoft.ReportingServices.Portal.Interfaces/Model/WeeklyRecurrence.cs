using System;

namespace Model
{
	// Token: 0x02000079 RID: 121
	public class WeeklyRecurrence
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000046A5 File Offset: 0x000028A5
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000046AD File Offset: 0x000028AD
		public int WeeksInterval { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000046B6 File Offset: 0x000028B6
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000046BE File Offset: 0x000028BE
		public bool WeeksIntervalSpecified { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000046C7 File Offset: 0x000028C7
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000046CF File Offset: 0x000028CF
		public DaysOfWeekSelector DaysOfWeek { get; set; }
	}
}
