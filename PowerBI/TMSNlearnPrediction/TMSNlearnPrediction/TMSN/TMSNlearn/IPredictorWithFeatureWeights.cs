using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C0 RID: 1216
	public interface IPredictorWithFeatureWeights<out TResult> : IHaveFeatureWeights, IPredictorProducing<TResult>, IPredictor
	{
	}
}
