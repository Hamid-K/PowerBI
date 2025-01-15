using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000061 RID: 97
	[Flags]
	internal enum DsqFilterType
	{
		// Token: 0x04000268 RID: 616
		DataShape = 0,
		// Token: 0x04000269 RID: 617
		Scope = 1,
		// Token: 0x0400026A RID: 618
		Exist = 2,
		// Token: 0x0400026B RID: 619
		Context = 3,
		// Token: 0x0400026C RID: 620
		AnyValue = 4,
		// Token: 0x0400026D RID: 621
		AnyValueDefaultValueOverridesAncestors = 5,
		// Token: 0x0400026E RID: 622
		DefaultValue = 6,
		// Token: 0x0400026F RID: 623
		Apply = 7
	}
}
