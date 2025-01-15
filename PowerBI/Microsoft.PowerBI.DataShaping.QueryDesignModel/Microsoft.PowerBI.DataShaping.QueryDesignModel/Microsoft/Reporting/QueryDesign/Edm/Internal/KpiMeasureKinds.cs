using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000249 RID: 585
	[Flags]
	public enum KpiMeasureKinds
	{
		// Token: 0x04000E53 RID: 3667
		None = 0,
		// Token: 0x04000E54 RID: 3668
		Value = 1,
		// Token: 0x04000E55 RID: 3669
		Status = 2,
		// Token: 0x04000E56 RID: 3670
		Goal = 4,
		// Token: 0x04000E57 RID: 3671
		Trend = 8,
		// Token: 0x04000E58 RID: 3672
		AllExceptValue = 14,
		// Token: 0x04000E59 RID: 3673
		All = 15
	}
}
