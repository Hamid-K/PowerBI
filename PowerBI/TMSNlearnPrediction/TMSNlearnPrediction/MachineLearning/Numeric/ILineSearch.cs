using System;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000447 RID: 1095
	public interface ILineSearch : IDiffLineSearch
	{
		// Token: 0x060016B4 RID: 5812
		float Minimize(Func<float, float> Func);
	}
}
