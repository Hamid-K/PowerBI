using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E0 RID: 736
	public interface IOptimizer
	{
		// Token: 0x06000FEA RID: 4074
		List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps);
	}
}
