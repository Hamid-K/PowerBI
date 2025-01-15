using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x0200175C RID: 5980
	internal interface IConcatStrategy
	{
		// Token: 0x0600C655 RID: 50773
		IEnumerable<string> Prefixes(WitnessContext<string> context);
	}
}
