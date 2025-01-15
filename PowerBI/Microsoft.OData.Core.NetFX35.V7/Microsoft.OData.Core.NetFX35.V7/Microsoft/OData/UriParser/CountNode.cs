using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000132 RID: 306
	public sealed class CountNode : SingleValueNode
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00029024 File Offset: 0x00027224
		public CountNode(CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNode>(source, "source");
			this.source = source;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0002903F File Offset: 0x0002723F
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00029047 File Offset: 0x00027247
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetInt64(false);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00029054 File Offset: 0x00027254
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Count;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00029058 File Offset: 0x00027258
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400074C RID: 1868
		private readonly CollectionNode source;
	}
}
