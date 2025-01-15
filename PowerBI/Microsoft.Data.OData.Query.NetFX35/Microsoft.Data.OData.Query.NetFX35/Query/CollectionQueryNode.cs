using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000011 RID: 17
	public abstract class CollectionQueryNode : QueryNode
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70
		public abstract IEdmTypeReference ItemType { get; }
	}
}
