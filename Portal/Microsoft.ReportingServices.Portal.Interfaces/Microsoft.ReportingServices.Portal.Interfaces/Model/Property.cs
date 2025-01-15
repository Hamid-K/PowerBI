using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000073 RID: 115
	public sealed class Property
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00003FE5 File Offset: 0x000021E5
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00003FED File Offset: 0x000021ED
		[Key]
		public string Name { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00003FF6 File Offset: 0x000021F6
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00003FFE File Offset: 0x000021FE
		public string Value { get; set; }
	}
}
