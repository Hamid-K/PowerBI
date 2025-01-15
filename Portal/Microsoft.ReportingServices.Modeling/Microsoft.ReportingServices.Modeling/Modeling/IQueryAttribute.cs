using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000BB RID: 187
	public interface IQueryAttribute : IValidationScope
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A64 RID: 2660
		ModelAttribute ModelAttribute { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A65 RID: 2661
		Expression CalculatedAttribute { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A66 RID: 2662
		string Name { get; }

		// Token: 0x06000A67 RID: 2663
		ResultType GetResultType();

		// Token: 0x06000A68 RID: 2664
		bool IsAnchored();

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A69 RID: 2665
		bool IsInvalidRefTarget { get; }
	}
}
