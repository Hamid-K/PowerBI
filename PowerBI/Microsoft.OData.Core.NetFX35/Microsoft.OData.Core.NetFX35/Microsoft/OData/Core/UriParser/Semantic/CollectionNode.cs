using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000227 RID: 551
	public abstract class CollectionNode : QueryNode
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060013EB RID: 5099
		public abstract IEdmTypeReference ItemType { get; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060013EC RID: 5100
		public abstract IEdmCollectionTypeReference CollectionType { get; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00048E59 File Offset: 0x00047059
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
