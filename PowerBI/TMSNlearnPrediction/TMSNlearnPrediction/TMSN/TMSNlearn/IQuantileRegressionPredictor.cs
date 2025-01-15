using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B4 RID: 1204
	public interface IQuantileRegressionPredictor
	{
		// Token: 0x060018DE RID: 6366
		ISchemaBindableMapper CreateMapper(double[] quantiles);
	}
}
