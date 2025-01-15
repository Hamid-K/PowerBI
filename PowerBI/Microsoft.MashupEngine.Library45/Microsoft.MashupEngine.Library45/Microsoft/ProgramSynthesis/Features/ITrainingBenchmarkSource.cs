using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007DF RID: 2015
	public interface ITrainingBenchmarkSource
	{
		// Token: 0x06002AF5 RID: 10997
		IEnumerable<ITrainingBenchmark> GetBenchmarks();
	}
}
