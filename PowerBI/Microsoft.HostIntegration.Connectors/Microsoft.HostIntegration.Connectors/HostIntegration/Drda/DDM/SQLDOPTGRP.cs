using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008C0 RID: 2240
	public class SQLDOPTGRP
	{
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x000FCC0C File Offset: 0x000FAE0C
		// (set) Token: 0x0600472B RID: 18219 RVA: 0x000FCC14 File Offset: 0x000FAE14
		public int SqlUnnamed { get; set; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x000FCC1D File Offset: 0x000FAE1D
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x000FCC25 File Offset: 0x000FAE25
		public string SqlName { get; set; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x000FCC2E File Offset: 0x000FAE2E
		// (set) Token: 0x0600472F RID: 18223 RVA: 0x000FCC36 File Offset: 0x000FAE36
		public string SqlLabel { get; set; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x000FCC3F File Offset: 0x000FAE3F
		// (set) Token: 0x06004731 RID: 18225 RVA: 0x000FCC47 File Offset: 0x000FAE47
		public string SqlComments { get; set; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x000FCC50 File Offset: 0x000FAE50
		// (set) Token: 0x06004733 RID: 18227 RVA: 0x000FCC58 File Offset: 0x000FAE58
		public SQLUDTGRP SqlUdtGrp { get; set; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x000FCC61 File Offset: 0x000FAE61
		// (set) Token: 0x06004735 RID: 18229 RVA: 0x000FCC69 File Offset: 0x000FAE69
		public SQLDXGRP SqlDxGrp { get; set; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x000FCC72 File Offset: 0x000FAE72
		// (set) Token: 0x06004737 RID: 18231 RVA: 0x000FCC7A File Offset: 0x000FAE7A
		public SQLDCDT SqlDcdt { get; set; }
	}
}
