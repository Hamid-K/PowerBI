using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A9 RID: 425
	public abstract class SingleEntityNode : SingleResourceNode
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001436 RID: 5174
		public abstract IEdmEntityTypeReference EntityTypeReference { get; }
	}
}
