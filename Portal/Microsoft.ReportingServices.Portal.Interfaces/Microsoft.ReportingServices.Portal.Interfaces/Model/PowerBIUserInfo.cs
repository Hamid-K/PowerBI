using System;

namespace Model
{
	// Token: 0x0200004D RID: 77
	public sealed class PowerBIUserInfo
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000030E2 File Offset: 0x000012E2
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000030EA File Offset: 0x000012EA
		public Guid Id { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000030F3 File Offset: 0x000012F3
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000030FB File Offset: 0x000012FB
		public string UserName { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000310C File Offset: 0x0000130C
		public PowerBIUserStatus Status { get; set; }
	}
}
