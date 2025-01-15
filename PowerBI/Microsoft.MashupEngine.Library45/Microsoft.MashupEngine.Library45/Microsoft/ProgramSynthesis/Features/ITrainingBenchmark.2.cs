using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007E1 RID: 2017
	public interface ITrainingBenchmark<TInput> : ITrainingBenchmark
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002AFA RID: 11002
		IList<TInput> Inputs { get; }
	}
}
