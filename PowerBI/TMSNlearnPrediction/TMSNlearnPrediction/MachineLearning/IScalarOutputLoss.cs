using System;

namespace Microsoft.MachineLearning
{
	// Token: 0x020004A5 RID: 1189
	public interface IScalarOutputLoss : ILossFunction<float, float>
	{
		// Token: 0x060018A2 RID: 6306
		float Derivative(float output, float label);
	}
}
