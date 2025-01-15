using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012F RID: 303
	public sealed class CollectionPropertyAccessNode : CollectionNode
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00028E64 File Offset: 0x00027064
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00028EF7 File Offset: 0x000270F7
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00028EFF File Offset: 0x000270FF
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00028F07 File Offset: 0x00027107
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00028F0F File Offset: 0x0002710F
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00028F17 File Offset: 0x00027117
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionPropertyAccess;
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00028F1A File Offset: 0x0002711A
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000743 RID: 1859
		private readonly SingleValueNode source;

		// Token: 0x04000744 RID: 1860
		private readonly IEdmProperty property;

		// Token: 0x04000745 RID: 1861
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000746 RID: 1862
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
