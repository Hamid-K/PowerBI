using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000044 RID: 68
	public sealed class SystemPolicy
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00002F7D File Offset: 0x0000117D
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00002F85 File Offset: 0x00001185
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00002F8E File Offset: 0x0000118E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00002F96 File Offset: 0x00001196
		public IEnumerable<Policy> Policies { get; set; }
	}
}
