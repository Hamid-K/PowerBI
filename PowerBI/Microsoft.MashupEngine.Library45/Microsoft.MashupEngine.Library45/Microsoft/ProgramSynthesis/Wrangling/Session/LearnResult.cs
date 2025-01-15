using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000113 RID: 275
	public struct LearnResult<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00014275 File Offset: 0x00012475
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0001427D File Offset: 0x0001247D
		public LearnResultKind Kind { readonly get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00014286 File Offset: 0x00012486
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001428E File Offset: 0x0001248E
		public IReadOnlyList<TProgram> LearnedPrograms { readonly get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00014297 File Offset: 0x00012497
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001429F File Offset: 0x0001249F
		public IReadOnlyCollection<Conflict> Conflicts { readonly get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x000142A8 File Offset: 0x000124A8
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x000142B0 File Offset: 0x000124B0
		public IReadOnlyDictionary<TProgram, IReadOnlyList<Example<TInput, TOutput>>> ProgramOutputs { readonly get; set; }
	}
}
