using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C2 RID: 5826
	public class ArithmeticSetList : ReadOnlyListBase<ArithmeticSet>
	{
		// Token: 0x0600C246 RID: 49734 RVA: 0x0029DBA0 File Offset: 0x0029BDA0
		public ArithmeticSetList()
			: base(new ArithmeticSet[0])
		{
		}

		// Token: 0x0600C247 RID: 49735 RVA: 0x0029DBAE File Offset: 0x0029BDAE
		public ArithmeticSetList(IEnumerable<ArithmeticSet> items)
			: base(items)
		{
		}
	}
}
