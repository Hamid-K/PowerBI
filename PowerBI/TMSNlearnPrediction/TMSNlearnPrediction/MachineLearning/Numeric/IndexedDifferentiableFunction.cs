using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200043B RID: 1083
	// (Invoke) Token: 0x06001675 RID: 5749
	public delegate float IndexedDifferentiableFunction(int index, ref VBuffer<float> input, ref VBuffer<float> gradient);
}
