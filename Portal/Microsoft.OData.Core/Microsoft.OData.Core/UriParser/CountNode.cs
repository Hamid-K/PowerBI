using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000177 RID: 375
	public sealed class CountNode : SingleValueNode
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x00038958 File Offset: 0x00036B58
		public CountNode(CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNode>(source, "source");
			this.source = source;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00038973 File Offset: 0x00036B73
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0003897B File Offset: 0x00036B7B
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetInt64(false);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00026039 File Offset: 0x00024239
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Count;
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00038988 File Offset: 0x00036B88
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400086D RID: 2157
		private readonly CollectionNode source;
	}
}
