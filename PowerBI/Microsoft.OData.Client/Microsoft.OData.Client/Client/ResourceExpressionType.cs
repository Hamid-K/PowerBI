using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A4 RID: 164
	internal enum ResourceExpressionType
	{
		// Token: 0x0400022C RID: 556
		RootResourceSet = 10000,
		// Token: 0x0400022D RID: 557
		RootSingleResource,
		// Token: 0x0400022E RID: 558
		ResourceNavigationProperty,
		// Token: 0x0400022F RID: 559
		ResourceNavigationPropertySingleton,
		// Token: 0x04000230 RID: 560
		TakeQueryOption,
		// Token: 0x04000231 RID: 561
		SkipQueryOption,
		// Token: 0x04000232 RID: 562
		OrderByQueryOption,
		// Token: 0x04000233 RID: 563
		FilterQueryOption,
		// Token: 0x04000234 RID: 564
		InputReference,
		// Token: 0x04000235 RID: 565
		ProjectionQueryOption,
		// Token: 0x04000236 RID: 566
		ExpandQueryOption
	}
}
