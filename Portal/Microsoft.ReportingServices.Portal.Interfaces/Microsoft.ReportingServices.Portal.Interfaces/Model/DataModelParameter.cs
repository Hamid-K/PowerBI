using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x0200003D RID: 61
	public sealed class DataModelParameter
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002CC3 File Offset: 0x00000EC3
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002CCB File Offset: 0x00000ECB
		[Required]
		[Key]
		public string Name { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002CD4 File Offset: 0x00000ED4
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002CDC File Offset: 0x00000EDC
		public string Value { get; set; }
	}
}
