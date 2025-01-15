using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D9 RID: 729
	public interface IGeneratedFunction
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000FD0 RID: 4048
		IReadOnlyList<Record<string, Type>> Parameters { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000FD1 RID: 4049
		Type ReturnType { get; }

		// Token: 0x06000FD2 RID: 4050
		void Optimize(IOptimizer optimizer);

		// Token: 0x06000FD3 RID: 4051
		CodeForGeneratedFunction GenerateCode(string headerModuleName, OptimizeFor optimization);
	}
}
