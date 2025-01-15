using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000230 RID: 560
	public sealed class CountNode : SingleValueNode
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x00049312 File Offset: 0x00047512
		public CountNode(CollectionNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNode>(source, "source");
			this.source = source;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004932C File Offset: 0x0004752C
		public CollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00049334 File Offset: 0x00047534
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetInt64(false);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00049341 File Offset: 0x00047541
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionCount;
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00049345 File Offset: 0x00047545
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000882 RID: 2178
		private readonly CollectionNode source;
	}
}
