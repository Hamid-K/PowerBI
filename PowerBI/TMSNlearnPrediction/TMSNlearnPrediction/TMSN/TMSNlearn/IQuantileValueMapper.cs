using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B6 RID: 1206
	public interface IQuantileValueMapper
	{
		// Token: 0x060018E3 RID: 6371
		ValueMapper<VBuffer<float>, VBuffer<float>> GetMapper(float[] quantiles);
	}
}
