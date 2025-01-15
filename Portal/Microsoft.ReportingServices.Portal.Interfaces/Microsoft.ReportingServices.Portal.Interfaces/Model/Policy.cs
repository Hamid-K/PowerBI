using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000072 RID: 114
	public sealed class Policy
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00003FC3 File Offset: 0x000021C3
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00003FCB File Offset: 0x000021CB
		public string GroupUserName { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00003FDC File Offset: 0x000021DC
		public IEnumerable<Role> Roles { get; set; }
	}
}
