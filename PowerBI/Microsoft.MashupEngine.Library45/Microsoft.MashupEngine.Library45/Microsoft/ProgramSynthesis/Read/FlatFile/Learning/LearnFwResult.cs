using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012CB RID: 4811
	public class LearnFwResult : LearnResult
	{
		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x0600911C RID: 37148 RVA: 0x001E9355 File Offset: 0x001E7555
		// (set) Token: 0x0600911D RID: 37149 RVA: 0x001E935D File Offset: 0x001E755D
		public IReadOnlyList<Record<int, int?>> FieldPositions { get; set; }
	}
}
