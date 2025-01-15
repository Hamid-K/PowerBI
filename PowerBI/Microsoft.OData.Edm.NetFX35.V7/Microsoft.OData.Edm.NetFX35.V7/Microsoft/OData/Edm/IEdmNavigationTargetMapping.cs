using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000081 RID: 129
	public interface IEdmNavigationTargetMapping
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000457 RID: 1111
		IEdmNavigationProperty NavigationProperty { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000458 RID: 1112
		IEdmEntitySet TargetEntitySet { get; }
	}
}
