using System;

namespace Model
{
	// Token: 0x0200002C RID: 44
	public class ExpirationReference
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000298F File Offset: 0x00000B8F
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002997 File Offset: 0x00000B97
		public int Minutes { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000029A0 File Offset: 0x00000BA0
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000029A8 File Offset: 0x00000BA8
		public ScheduleReference Schedule { get; set; }
	}
}
