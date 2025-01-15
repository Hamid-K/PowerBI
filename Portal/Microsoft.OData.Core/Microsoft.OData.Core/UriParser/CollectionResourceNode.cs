using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017C RID: 380
	public abstract class CollectionResourceNode : CollectionNode
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060012E3 RID: 4835
		public abstract IEdmStructuredTypeReference ItemStructuredType { get; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060012E4 RID: 4836
		public abstract IEdmNavigationSource NavigationSource { get; }
	}
}
