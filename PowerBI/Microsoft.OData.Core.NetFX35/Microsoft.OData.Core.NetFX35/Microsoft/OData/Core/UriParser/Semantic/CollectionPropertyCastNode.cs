using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022D RID: 557
	public sealed class CollectionPropertyCastNode : CollectionNode
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x000491A0 File Offset: 0x000473A0
		public CollectionPropertyCastNode(CollectionPropertyAccessNode source, IEdmComplexType complexType)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionPropertyAccessNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmComplexType>(complexType, "complexType");
			this.source = source;
			this.edmTypeReference = new EdmComplexTypeReference(complexType, false);
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.edmTypeReference);
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000491EE File Offset: 0x000473EE
		public CollectionPropertyAccessNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x000491F6 File Offset: 0x000473F6
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x000491FE File Offset: 0x000473FE
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00049206 File Offset: 0x00047406
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionPropertyCast;
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004920A File Offset: 0x0004740A
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400087A RID: 2170
		private readonly CollectionPropertyAccessNode source;

		// Token: 0x0400087B RID: 2171
		private readonly IEdmComplexTypeReference edmTypeReference;

		// Token: 0x0400087C RID: 2172
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
