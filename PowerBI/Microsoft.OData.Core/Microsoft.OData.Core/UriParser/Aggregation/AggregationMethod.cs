using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FE RID: 510
	public enum AggregationMethod
	{
		// Token: 0x04000A33 RID: 2611
		Sum,
		// Token: 0x04000A34 RID: 2612
		Min,
		// Token: 0x04000A35 RID: 2613
		Max,
		// Token: 0x04000A36 RID: 2614
		Average,
		// Token: 0x04000A37 RID: 2615
		CountDistinct,
		// Token: 0x04000A38 RID: 2616
		VirtualPropertyCount,
		// Token: 0x04000A39 RID: 2617
		Custom
	}
}
