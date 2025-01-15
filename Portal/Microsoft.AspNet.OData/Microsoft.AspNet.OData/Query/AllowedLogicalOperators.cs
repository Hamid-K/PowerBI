using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000CB RID: 203
	[Flags]
	public enum AllowedLogicalOperators
	{
		// Token: 0x040001CA RID: 458
		None = 0,
		// Token: 0x040001CB RID: 459
		Or = 1,
		// Token: 0x040001CC RID: 460
		And = 2,
		// Token: 0x040001CD RID: 461
		Equal = 4,
		// Token: 0x040001CE RID: 462
		NotEqual = 8,
		// Token: 0x040001CF RID: 463
		GreaterThan = 16,
		// Token: 0x040001D0 RID: 464
		GreaterThanOrEqual = 32,
		// Token: 0x040001D1 RID: 465
		LessThan = 64,
		// Token: 0x040001D2 RID: 466
		LessThanOrEqual = 128,
		// Token: 0x040001D3 RID: 467
		Not = 256,
		// Token: 0x040001D4 RID: 468
		Has = 512,
		// Token: 0x040001D5 RID: 469
		All = 1023
	}
}
