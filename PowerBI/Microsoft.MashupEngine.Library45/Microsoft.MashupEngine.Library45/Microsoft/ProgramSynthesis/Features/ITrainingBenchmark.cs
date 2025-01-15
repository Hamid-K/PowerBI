using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007E0 RID: 2016
	public interface ITrainingBenchmark
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002AF6 RID: 10998
		string Name { get; }

		// Token: 0x06002AF7 RID: 10999
		IEnumerable<IProgram> Learn(int k, int[] trainingIndices = null);

		// Token: 0x06002AF8 RID: 11000
		FeatureCalculationContext BuildFeatureCalculationContext(int[] trainingIndices = null);

		// Token: 0x06002AF9 RID: 11001
		double GetCorrectness(IProgram program);
	}
}
