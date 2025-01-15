using System;

namespace Model
{
	// Token: 0x02000058 RID: 88
	public class MonthlyRecurrence
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000334D File Offset: 0x0000154D
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00003355 File Offset: 0x00001555
		public string Days { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000335E File Offset: 0x0000155E
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00003366 File Offset: 0x00001566
		public MonthsOfYearSelector MonthsOfYear { get; set; }
	}
}
