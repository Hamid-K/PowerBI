using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AB RID: 427
	public class CollectionComplexNode : CollectionResourceNode
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x00030436 File Offset: 0x0002E636
		public CollectionComplexNode(SingleResourceNode source, IEdmProperty property)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, property)
		{
			this.source = source;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00030458 File Offset: 0x0002E658
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x000304C6 File Offset: 0x0002E6C6
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x000304CE File Offset: 0x0002E6CE
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x000304D6 File Offset: 0x0002E6D6
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000304DE File Offset: 0x0002E6DE
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000304D6 File Offset: 0x0002E6D6
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000304E6 File Offset: 0x0002E6E6
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000304EE File Offset: 0x0002E6EE
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionComplexNode;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000304F2 File Offset: 0x0002E6F2
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008C2 RID: 2242
		private readonly SingleResourceNode source;

		// Token: 0x040008C3 RID: 2243
		private readonly IEdmProperty property;

		// Token: 0x040008C4 RID: 2244
		private readonly IEdmComplexTypeReference itemType;

		// Token: 0x040008C5 RID: 2245
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x040008C6 RID: 2246
		private readonly IEdmNavigationSource navigationSource;
	}
}
