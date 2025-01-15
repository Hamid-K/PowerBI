using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BA RID: 442
	public abstract class SingleResourceNode : SingleValueNode
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001186 RID: 4486
		public abstract IEdmNavigationSource NavigationSource { get; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001187 RID: 4487
		public abstract IEdmStructuredTypeReference StructuredTypeReference { get; }
	}
}
