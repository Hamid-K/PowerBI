using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022C RID: 556
	public sealed class CollectionPropertyAccessNode : CollectionNode
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x000490D8 File Offset: 0x000472D8
		public CollectionPropertyAccessNode(SingleValueNode source, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			if (!property.Type.IsCollection())
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessTypeMustBeCollection(property.Name));
			}
			this.source = source;
			this.property = property;
			this.collectionTypeReference = property.Type.AsCollection();
			this.itemType = this.collectionTypeReference.ElementType();
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00049169 File Offset: 0x00047369
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00049171 File Offset: 0x00047371
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00049179 File Offset: 0x00047379
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00049181 File Offset: 0x00047381
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00049189 File Offset: 0x00047389
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionPropertyAccess;
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004918C File Offset: 0x0004738C
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000876 RID: 2166
		private readonly SingleValueNode source;

		// Token: 0x04000877 RID: 2167
		private readonly IEdmProperty property;

		// Token: 0x04000878 RID: 2168
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000879 RID: 2169
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
