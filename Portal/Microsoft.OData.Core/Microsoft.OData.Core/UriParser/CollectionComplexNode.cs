using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000106 RID: 262
	public class CollectionComplexNode : CollectionResourceNode
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00025DD3 File Offset: 0x00023FD3
		public CollectionComplexNode(SingleResourceNode source, IEdmProperty property)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, property)
		{
			this.source = source;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00025DF4 File Offset: 0x00023FF4
		private CollectionComplexNode(IEdmNavigationSource navigationSource, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			this.property = property;
			this.collectionTypeReference = property.Type.AsCollection();
			this.itemType = this.collectionTypeReference.ElementType().AsComplex();
			this.navigationSource = navigationSource;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00025E62 File Offset: 0x00024062
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00025E6A File Offset: 0x0002406A
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00025E72 File Offset: 0x00024072
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00025E7A File Offset: 0x0002407A
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00025E72 File Offset: 0x00024072
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00025E82 File Offset: 0x00024082
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00025E8A File Offset: 0x0002408A
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionComplexNode;
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00025E8E File Offset: 0x0002408E
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400076E RID: 1902
		private readonly SingleResourceNode source;

		// Token: 0x0400076F RID: 1903
		private readonly IEdmProperty property;

		// Token: 0x04000770 RID: 1904
		private readonly IEdmComplexTypeReference itemType;

		// Token: 0x04000771 RID: 1905
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x04000772 RID: 1906
		private readonly IEdmNavigationSource navigationSource;
	}
}
