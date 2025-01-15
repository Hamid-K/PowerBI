using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002DA RID: 730
	public interface IModuleOptimizer
	{
		// Token: 0x06000FD4 RID: 4052
		Dictionary<string, IGeneratedFunction> Optimize(IReadOnlyDictionary<string, IGeneratedFunction> boundFunctions, out List<string> toBeRemovedFunctions);
	}
}
