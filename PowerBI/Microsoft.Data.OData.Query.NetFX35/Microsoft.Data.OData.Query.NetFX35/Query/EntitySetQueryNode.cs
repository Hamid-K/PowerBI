using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000050 RID: 80
	public sealed class EntitySetQueryNode : CollectionQueryNode
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000AC6B File Offset: 0x00008E6B
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000AC73 File Offset: 0x00008E73
		public IEdmEntitySet EntitySet { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public override IEdmTypeReference ItemType
		{
			get
			{
				if (this.EntitySet == null)
				{
					return null;
				}
				return this.EntitySet.ElementType.ToTypeReference();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000AC98 File Offset: 0x00008E98
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.EntitySet;
			}
		}
	}
}
