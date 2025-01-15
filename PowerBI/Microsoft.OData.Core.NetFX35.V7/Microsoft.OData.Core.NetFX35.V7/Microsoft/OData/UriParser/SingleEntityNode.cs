using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015B RID: 347
	public abstract class SingleEntityNode : SingleResourceNode
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EFF RID: 3839
		public abstract IEdmEntityTypeReference EntityTypeReference { get; }
	}
}
