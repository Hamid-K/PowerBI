using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C1 RID: 449
	public enum AggregationMethod
	{
		// Token: 0x040008F7 RID: 2295
		Sum,
		// Token: 0x040008F8 RID: 2296
		Min,
		// Token: 0x040008F9 RID: 2297
		Max,
		// Token: 0x040008FA RID: 2298
		Average,
		// Token: 0x040008FB RID: 2299
		CountDistinct,
		// Token: 0x040008FC RID: 2300
		VirtualPropertyCount,
		// Token: 0x040008FD RID: 2301
		Custom
	}
}
