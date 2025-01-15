using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000023 RID: 35
	public enum SecurityFilteringBehavior
	{
		// Token: 0x0400009E RID: 158
		OneDirection = 1,
		// Token: 0x0400009F RID: 159
		BothDirections,
		// Token: 0x040000A0 RID: 160
		[CompatibilityRequirement("1561")]
		None
	}
}
