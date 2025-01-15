using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000172 RID: 370
	public abstract class CollectionNode : QueryNode
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001291 RID: 4753
		public abstract IEdmTypeReference ItemType { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001292 RID: 4754
		public abstract IEdmCollectionTypeReference CollectionType { get; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0003872F File Offset: 0x0003692F
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
