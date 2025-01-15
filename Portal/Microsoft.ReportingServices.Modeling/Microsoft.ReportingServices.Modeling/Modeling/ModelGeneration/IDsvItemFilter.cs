using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E8 RID: 232
	public interface IDsvItemFilter
	{
		// Token: 0x06000BF1 RID: 3057
		bool Evaluate(DsvItem dsvItem);
	}
}
