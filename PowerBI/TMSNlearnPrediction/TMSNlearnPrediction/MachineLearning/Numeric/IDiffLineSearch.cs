using System;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000446 RID: 1094
	public interface IDiffLineSearch
	{
		// Token: 0x060016B3 RID: 5811
		float Minimize(DiffFunc1D Func, float initValue, float initDeriv);
	}
}
