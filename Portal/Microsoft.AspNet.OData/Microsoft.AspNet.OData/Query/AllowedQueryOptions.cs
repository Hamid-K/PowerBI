using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000CC RID: 204
	[Flags]
	public enum AllowedQueryOptions
	{
		// Token: 0x040001D7 RID: 471
		None = 0,
		// Token: 0x040001D8 RID: 472
		Filter = 1,
		// Token: 0x040001D9 RID: 473
		Expand = 2,
		// Token: 0x040001DA RID: 474
		Select = 4,
		// Token: 0x040001DB RID: 475
		OrderBy = 8,
		// Token: 0x040001DC RID: 476
		Top = 16,
		// Token: 0x040001DD RID: 477
		Skip = 32,
		// Token: 0x040001DE RID: 478
		Count = 64,
		// Token: 0x040001DF RID: 479
		Format = 128,
		// Token: 0x040001E0 RID: 480
		SkipToken = 256,
		// Token: 0x040001E1 RID: 481
		DeltaToken = 512,
		// Token: 0x040001E2 RID: 482
		Apply = 1024,
		// Token: 0x040001E3 RID: 483
		Supported = 1535,
		// Token: 0x040001E4 RID: 484
		All = 2047
	}
}
