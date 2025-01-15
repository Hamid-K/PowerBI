using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022E RID: 558
	internal enum RdlArgTypes
	{
		// Token: 0x040005EF RID: 1519
		Numeric,
		// Token: 0x040005F0 RID: 1520
		Variant,
		// Token: 0x040005F1 RID: 1521
		VariantOrBinary,
		// Token: 0x040005F2 RID: 1522
		VariantArray,
		// Token: 0x040005F3 RID: 1523
		Scope,
		// Token: 0x040005F4 RID: 1524
		Recursive,
		// Token: 0x040005F5 RID: 1525
		AggregateFunction
	}
}
