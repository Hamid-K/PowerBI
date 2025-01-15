using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000111 RID: 273
	public abstract class SingleResourceNode : SingleValueNode
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F6B RID: 3947
		public abstract IEdmNavigationSource NavigationSource { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F6C RID: 3948
		public abstract IEdmStructuredTypeReference StructuredTypeReference { get; }
	}
}
