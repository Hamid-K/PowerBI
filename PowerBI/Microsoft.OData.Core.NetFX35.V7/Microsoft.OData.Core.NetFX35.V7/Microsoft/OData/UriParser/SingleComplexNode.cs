using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B6 RID: 438
	public class SingleComplexNode : SingleResourceNode
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x0003093E File Offset: 0x0002EB3E
		public SingleComplexNode(SingleResourceNode source, IEdmProperty property)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, property)
		{
			this.source = source;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00030960 File Offset: 0x0002EB60
		private SingleComplexNode(IEdmNavigationSource navigationSource, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			this.property = property;
			this.navigationSource = navigationSource;
			this.typeReference = property.Type.AsComplex();
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000309B8 File Offset: 0x0002EBB8
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000309C0 File Offset: 0x0002EBC0
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000309C8 File Offset: 0x0002EBC8
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000309D0 File Offset: 0x0002EBD0
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x000309C8 File Offset: 0x0002EBC8
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0002BC1E File Offset: 0x00029E1E
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleComplexNode;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000309D8 File Offset: 0x0002EBD8
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008E4 RID: 2276
		private readonly SingleResourceNode source;

		// Token: 0x040008E5 RID: 2277
		private readonly IEdmProperty property;

		// Token: 0x040008E6 RID: 2278
		private readonly IEdmComplexTypeReference typeReference;

		// Token: 0x040008E7 RID: 2279
		private readonly IEdmNavigationSource navigationSource;
	}
}
