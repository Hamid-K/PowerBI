using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AE RID: 430
	public abstract class CollectionResourceNode : CollectionNode
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600113F RID: 4415
		public abstract IEdmStructuredTypeReference ItemStructuredType { get; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001140 RID: 4416
		public abstract IEdmNavigationSource NavigationSource { get; }
	}
}
