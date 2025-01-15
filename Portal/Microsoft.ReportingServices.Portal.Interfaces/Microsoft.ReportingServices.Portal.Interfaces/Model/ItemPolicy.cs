using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200006C RID: 108
	public sealed class ItemPolicy
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00003CD9 File Offset: 0x00001ED9
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public Guid Id { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00003CEA File Offset: 0x00001EEA
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00003CF2 File Offset: 0x00001EF2
		public bool InheritParentPolicy { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00003CFB File Offset: 0x00001EFB
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00003D03 File Offset: 0x00001F03
		public IEnumerable<Policy> Policies { get; set; }
	}
}
