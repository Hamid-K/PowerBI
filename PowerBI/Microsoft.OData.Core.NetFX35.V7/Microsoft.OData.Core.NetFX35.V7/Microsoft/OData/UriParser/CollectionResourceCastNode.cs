using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AC RID: 428
	public sealed class CollectionResourceCastNode : CollectionResourceNode
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x00030508 File Offset: 0x0002E708
		public CollectionResourceCastNode(CollectionResourceNode source, IEdmStructuredType structuredType)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(structuredType, "structuredType");
			this.source = source;
			this.edmTypeReference = structuredType.GetTypeReference();
			this.navigationSource = source.NavigationSource;
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.edmTypeReference);
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00030563 File Offset: 0x0002E763
		public CollectionResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0003056B File Offset: 0x0002E76B
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00030573 File Offset: 0x0002E773
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0003056B File Offset: 0x0002E76B
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0003057B File Offset: 0x0002E77B
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0002BB9D File Offset: 0x00029D9D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionResourceCast;
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00030583 File Offset: 0x0002E783
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008C7 RID: 2247
		private readonly CollectionResourceNode source;

		// Token: 0x040008C8 RID: 2248
		private readonly IEdmStructuredTypeReference edmTypeReference;

		// Token: 0x040008C9 RID: 2249
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x040008CA RID: 2250
		private readonly IEdmNavigationSource navigationSource;
	}
}
