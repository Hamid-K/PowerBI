using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000243 RID: 579
	internal enum ExpressionType
	{
		// Token: 0x04000667 RID: 1639
		Variant,
		// Token: 0x04000668 RID: 1640
		String,
		// Token: 0x04000669 RID: 1641
		Boolean,
		// Token: 0x0400066A RID: 1642
		Integer,
		// Token: 0x0400066B RID: 1643
		DateTime,
		// Token: 0x0400066C RID: 1644
		Float,
		// Token: 0x0400066D RID: 1645
		Binary,
		// Token: 0x0400066E RID: 1646
		VariantArray,
		// Token: 0x0400066F RID: 1647
		Other,
		// Token: 0x04000670 RID: 1648
		Font,
		// Token: 0x04000671 RID: 1649
		Padding,
		// Token: 0x04000672 RID: 1650
		LineHeight
	}
}
