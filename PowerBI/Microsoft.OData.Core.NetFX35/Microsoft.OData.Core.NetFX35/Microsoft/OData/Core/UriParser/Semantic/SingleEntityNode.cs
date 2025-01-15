using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000238 RID: 568
	public abstract class SingleEntityNode : SingleValueNode
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001466 RID: 5222
		public abstract IEdmNavigationSource NavigationSource { get; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001467 RID: 5223
		public abstract IEdmEntityTypeReference EntityTypeReference { get; }
	}
}
