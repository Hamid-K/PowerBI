using System;

namespace Azure
{
	// Token: 0x0200002B RID: 43
	public class RequestConditions : MatchConditions
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000031D4 File Offset: 0x000013D4
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000031DC File Offset: 0x000013DC
		public DateTimeOffset? IfModifiedSince { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000031E5 File Offset: 0x000013E5
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000031ED File Offset: 0x000013ED
		public DateTimeOffset? IfUnmodifiedSince { get; set; }
	}
}
