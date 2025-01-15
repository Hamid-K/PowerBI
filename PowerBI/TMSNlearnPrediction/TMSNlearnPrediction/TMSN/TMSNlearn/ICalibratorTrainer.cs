using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C3 RID: 1219
	public interface ICalibratorTrainer
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060018F6 RID: 6390
		bool NeedsTraining { get; }

		// Token: 0x060018F7 RID: 6391
		bool ProcessTrainingExample(float output, bool labelIs1, float weight);

		// Token: 0x060018F8 RID: 6392
		ICalibrator FinishTraining(IChannel ch);
	}
}
