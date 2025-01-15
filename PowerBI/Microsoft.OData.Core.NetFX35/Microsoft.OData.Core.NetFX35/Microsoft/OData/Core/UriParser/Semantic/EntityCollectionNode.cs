using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000229 RID: 553
	public abstract class EntityCollectionNode : CollectionNode
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060013F8 RID: 5112
		public abstract IEdmEntityTypeReference EntityItemType { get; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060013F9 RID: 5113
		public abstract IEdmNavigationSource NavigationSource { get; }
	}
}
