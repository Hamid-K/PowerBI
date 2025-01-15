using System;

namespace Model
{
	// Token: 0x0200004C RID: 76
	public sealed class DataSourceCheckResult
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000030C0 File Offset: 0x000012C0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000030C8 File Offset: 0x000012C8
		public bool IsSuccessful { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000030D1 File Offset: 0x000012D1
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000030D9 File Offset: 0x000012D9
		public string ErrorMessage { get; set; }
	}
}
