using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022B RID: 555
	public sealed class CollectionOpenPropertyAccessNode : CollectionNode
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x0004907D File Offset: 0x0004727D
		public CollectionOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<string>(openPropertyName, "openPropertyName");
			this.source = source;
			this.name = openPropertyName;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000490A9 File Offset: 0x000472A9
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000490B1 File Offset: 0x000472B1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000490B9 File Offset: 0x000472B9
		public override IEdmTypeReference ItemType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000490BC File Offset: 0x000472BC
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000490BF File Offset: 0x000472BF
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionOpenPropertyAccess;
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000490C3 File Offset: 0x000472C3
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000874 RID: 2164
		private readonly SingleValueNode source;

		// Token: 0x04000875 RID: 2165
		private readonly string name;
	}
}
