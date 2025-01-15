using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200000C RID: 12
	public enum DataType
	{
		// Token: 0x0400002C RID: 44
		Automatic = 1,
		// Token: 0x0400002D RID: 45
		String,
		// Token: 0x0400002E RID: 46
		Int64 = 6,
		// Token: 0x0400002F RID: 47
		Double = 8,
		// Token: 0x04000030 RID: 48
		DateTime,
		// Token: 0x04000031 RID: 49
		Decimal,
		// Token: 0x04000032 RID: 50
		Boolean,
		// Token: 0x04000033 RID: 51
		Binary = 17,
		// Token: 0x04000034 RID: 52
		Unknown = 19,
		// Token: 0x04000035 RID: 53
		Variant
	}
}
