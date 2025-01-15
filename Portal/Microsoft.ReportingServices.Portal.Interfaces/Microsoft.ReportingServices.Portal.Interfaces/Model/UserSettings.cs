using System;

namespace Model
{
	// Token: 0x02000045 RID: 69
	public class UserSettings
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00002F9F File Offset: 0x0000119F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00002FA7 File Offset: 0x000011A7
		public Guid Id { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00002FB0 File Offset: 0x000011B0
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00002FB8 File Offset: 0x000011B8
		public string EmailAddress { get; set; }
	}
}
