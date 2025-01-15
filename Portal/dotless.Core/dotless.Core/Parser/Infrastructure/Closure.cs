using System;
using System.Collections.Generic;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x0200004E RID: 78
	public class Closure
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000E441 File Offset: 0x0000C641
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000E449 File Offset: 0x0000C649
		public Ruleset Ruleset { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000E452 File Offset: 0x0000C652
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000E45A File Offset: 0x0000C65A
		public List<Ruleset> Context { get; set; }
	}
}
