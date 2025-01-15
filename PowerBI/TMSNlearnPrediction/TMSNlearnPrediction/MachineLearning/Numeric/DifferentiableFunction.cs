using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200043A RID: 1082
	// (Invoke) Token: 0x06001671 RID: 5745
	public delegate float DifferentiableFunction(ref VBuffer<float> input, ref VBuffer<float> gradient, IProgressChannelProvider progress);
}
