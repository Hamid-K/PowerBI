using System;

namespace Model
{
	// Token: 0x02000063 RID: 99
	public class ScheduleReference
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00003668 File Offset: 0x00001868
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00003670 File Offset: 0x00001870
		public string ScheduleID { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00003679 File Offset: 0x00001879
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00003681 File Offset: 0x00001881
		public ScheduleDefinition Definition { get; set; }
	}
}
