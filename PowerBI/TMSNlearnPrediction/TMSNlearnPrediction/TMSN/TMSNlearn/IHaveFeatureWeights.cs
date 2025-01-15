using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004BF RID: 1215
	public interface IHaveFeatureWeights
	{
		// Token: 0x060018F0 RID: 6384
		void GetFeatureWeights(ref VBuffer<float> weights);
	}
}
