using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200005F RID: 95
	internal enum BinaryFilterOperator
	{
		// Token: 0x040000ED RID: 237
		Equal,
		// Token: 0x040000EE RID: 238
		GreaterThan,
		// Token: 0x040000EF RID: 239
		GreaterThanOrEqual,
		// Token: 0x040000F0 RID: 240
		LessThanOrEqual,
		// Token: 0x040000F1 RID: 241
		LessThan,
		// Token: 0x040000F2 RID: 242
		Contains,
		// Token: 0x040000F3 RID: 243
		StartsWith,
		// Token: 0x040000F4 RID: 244
		DateTimeEqualToSecond,
		// Token: 0x040000F5 RID: 245
		EndsWith,
		// Token: 0x040000F6 RID: 246
		EqualIdentity
	}
}
