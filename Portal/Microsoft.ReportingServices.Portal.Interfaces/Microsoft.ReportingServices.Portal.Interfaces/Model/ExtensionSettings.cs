using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000054 RID: 84
	public sealed class ExtensionSettings
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000032D6 File Offset: 0x000014D6
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000032DE File Offset: 0x000014DE
		public string Extension { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000032E7 File Offset: 0x000014E7
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000032EF File Offset: 0x000014EF
		public IEnumerable<ParameterValue> ParameterValues { get; set; }
	}
}
