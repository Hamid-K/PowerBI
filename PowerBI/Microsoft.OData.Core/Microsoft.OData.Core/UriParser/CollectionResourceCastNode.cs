using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017A RID: 378
	public sealed class CollectionResourceCastNode : CollectionResourceNode
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x00038B98 File Offset: 0x00036D98
		public CollectionResourceCastNode(CollectionResourceNode source, IEdmStructuredType structuredType)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(structuredType, "structuredType");
			this.source = source;
			this.edmTypeReference = structuredType.GetTypeReference();
			this.navigationSource = source.NavigationSource;
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.edmTypeReference);
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00038BF3 File Offset: 0x00036DF3
		public CollectionResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00038BFB File Offset: 0x00036DFB
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00038C03 File Offset: 0x00036E03
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00038BFB File Offset: 0x00036DFB
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00038C0B File Offset: 0x00036E0B
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00038C13 File Offset: 0x00036E13
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionResourceCast;
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00038C17 File Offset: 0x00036E17
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400086F RID: 2159
		private readonly CollectionResourceNode source;

		// Token: 0x04000870 RID: 2160
		private readonly IEdmStructuredTypeReference edmTypeReference;

		// Token: 0x04000871 RID: 2161
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x04000872 RID: 2162
		private readonly IEdmNavigationSource navigationSource;
	}
}
