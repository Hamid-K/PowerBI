using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x020016B5 RID: 5813
	public class WitnessDisjunctiveContext<TOperatorOutput>
	{
		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x0600C201 RID: 49665 RVA: 0x0029CBFB File Offset: 0x0029ADFB
		// (set) Token: 0x0600C202 RID: 49666 RVA: 0x0029CC03 File Offset: 0x0029AE03
		public IRow InputRow { get; set; }

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x0600C203 RID: 49667 RVA: 0x0029CC0C File Offset: 0x0029AE0C
		// (set) Token: 0x0600C204 RID: 49668 RVA: 0x0029CC14 File Offset: 0x0029AE14
		public IReadOnlyList<TOperatorOutput> OperatorOutputs { get; set; }
	}
}
