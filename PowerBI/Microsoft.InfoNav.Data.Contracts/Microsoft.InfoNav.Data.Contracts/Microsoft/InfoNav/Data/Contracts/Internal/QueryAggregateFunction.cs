using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200027C RID: 636
	public enum QueryAggregateFunction
	{
		// Token: 0x040007E8 RID: 2024
		Sum,
		// Token: 0x040007E9 RID: 2025
		Avg,
		// Token: 0x040007EA RID: 2026
		Count,
		// Token: 0x040007EB RID: 2027
		Min,
		// Token: 0x040007EC RID: 2028
		Max,
		// Token: 0x040007ED RID: 2029
		CountNonNull,
		// Token: 0x040007EE RID: 2030
		Median,
		// Token: 0x040007EF RID: 2031
		StandardDeviation,
		// Token: 0x040007F0 RID: 2032
		Variance,
		// Token: 0x040007F1 RID: 2033
		SingleValue
	}
}
