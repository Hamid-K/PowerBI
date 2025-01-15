using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x0200166F RID: 5743
	public class MatchCacheItem
	{
		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x0600C00A RID: 49162 RVA: 0x0029636D File Offset: 0x0029456D
		// (set) Token: 0x0600C00B RID: 49163 RVA: 0x00296375 File Offset: 0x00294575
		public MatchDescriptor Descriptor { get; set; }

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x0600C00C RID: 49164 RVA: 0x0029637E File Offset: 0x0029457E
		// (set) Token: 0x0600C00D RID: 49165 RVA: 0x00296386 File Offset: 0x00294586
		public int EndIndex { get; set; }

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x0600C00E RID: 49166 RVA: 0x0029638F File Offset: 0x0029458F
		// (set) Token: 0x0600C00F RID: 49167 RVA: 0x00296397 File Offset: 0x00294597
		public int StartIndex { get; set; }

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x0600C010 RID: 49168 RVA: 0x002963A0 File Offset: 0x002945A0
		// (set) Token: 0x0600C011 RID: 49169 RVA: 0x002963A8 File Offset: 0x002945A8
		public string Value { get; set; }
	}
}
