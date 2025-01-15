using System;

namespace Microsoft.MachineLearning
{
	// Token: 0x020004A4 RID: 1188
	public interface ILossFunction<in TOutput, in TLabel>
	{
		// Token: 0x060018A1 RID: 6305
		double Loss(TOutput output, TLabel label);
	}
}
