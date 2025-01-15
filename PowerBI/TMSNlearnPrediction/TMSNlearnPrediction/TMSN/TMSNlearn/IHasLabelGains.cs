using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C1 RID: 1217
	public interface IHasLabelGains : ITrainer
	{
		// Token: 0x060018F1 RID: 6385
		double[] GetLabelGains();
	}
}
