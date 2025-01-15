using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000174 RID: 372
	public sealed class CollectionPropertyAccessNode : CollectionNode
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x00038798 File Offset: 0x00036998
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

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0003882B File Offset: 0x00036A2B
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00038833 File Offset: 0x00036A33
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0003883B File Offset: 0x00036A3B
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00038843 File Offset: 0x00036A43
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0003884B File Offset: 0x00036A4B
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionPropertyAccess;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003884E File Offset: 0x00036A4E
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000864 RID: 2148
		private readonly SingleValueNode source;

		// Token: 0x04000865 RID: 2149
		private readonly IEdmProperty property;

		// Token: 0x04000866 RID: 2150
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000867 RID: 2151
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
