using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D6 RID: 214
	internal enum FilterOperator
	{
		// Token: 0x04000988 RID: 2440
		Equal,
		// Token: 0x04000989 RID: 2441
		GreaterThan,
		// Token: 0x0400098A RID: 2442
		GreaterThanOrEqual,
		// Token: 0x0400098B RID: 2443
		LessThanOrEqual,
		// Token: 0x0400098C RID: 2444
		LessThan,
		// Token: 0x0400098D RID: 2445
		Contains,
		// Token: 0x0400098E RID: 2446
		StartsWith,
		// Token: 0x0400098F RID: 2447
		DateTimeEqualToSecond,
		// Token: 0x04000990 RID: 2448
		AllValues,
		// Token: 0x04000991 RID: 2449
		EndsWith,
		// Token: 0x04000992 RID: 2450
		EqualIdentity
	}
}
