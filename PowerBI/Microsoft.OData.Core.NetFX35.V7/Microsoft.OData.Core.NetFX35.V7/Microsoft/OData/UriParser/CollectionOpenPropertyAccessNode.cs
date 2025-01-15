using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012E RID: 302
	public sealed class CollectionOpenPropertyAccessNode : CollectionNode
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00028E0B File Offset: 0x0002700B
		public CollectionOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<string>(openPropertyName, "openPropertyName");
			this.source = source;
			this.name = openPropertyName;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00028E39 File Offset: 0x00027039
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00028E41 File Offset: 0x00027041
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmTypeReference ItemType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00028E49 File Offset: 0x00027049
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionOpenPropertyAccess;
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00028E4D File Offset: 0x0002704D
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000741 RID: 1857
		private readonly SingleValueNode source;

		// Token: 0x04000742 RID: 1858
		private readonly string name;
	}
}
