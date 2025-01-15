using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012D RID: 301
	public abstract class CollectionNode : QueryNode
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000DB9 RID: 3513
		public abstract IEdmTypeReference ItemType { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000DBA RID: 3514
		public abstract IEdmCollectionTypeReference CollectionType { get; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00028DFB File Offset: 0x00026FFB
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
