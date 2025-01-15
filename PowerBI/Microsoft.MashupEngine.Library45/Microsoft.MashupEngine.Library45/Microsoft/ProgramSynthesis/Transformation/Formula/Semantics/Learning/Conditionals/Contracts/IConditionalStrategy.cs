using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts
{
	// Token: 0x02001756 RID: 5974
	internal interface IConditionalStrategy
	{
		// Token: 0x0600C643 RID: 50755
		IEnumerable<IConditionalBranch[]> Paths();
	}
}
