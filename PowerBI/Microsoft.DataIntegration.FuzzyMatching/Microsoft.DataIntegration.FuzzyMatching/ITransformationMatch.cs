using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010E RID: 270
	public interface ITransformationMatch
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B35 RID: 2869
		int Position { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B36 RID: 2870
		Transformation Transformation { get; }
	}
}
