using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010F RID: 271
	public class SingleComplexNode : SingleResourceNode
	{
		// Token: 0x06000F5D RID: 3933 RVA: 0x0002640B File Offset: 0x0002460B
		public SingleComplexNode(SingleResourceNode source, IEdmProperty property)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, property)
		{
			this.source = source;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002642C File Offset: 0x0002462C
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00026484 File Offset: 0x00024684
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0002648C File Offset: 0x0002468C
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00026494 File Offset: 0x00024694
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002649C File Offset: 0x0002469C
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00026494 File Offset: 0x00024694
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000264A4 File Offset: 0x000246A4
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleComplexNode;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000264A8 File Offset: 0x000246A8
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000783 RID: 1923
		private readonly SingleResourceNode source;

		// Token: 0x04000784 RID: 1924
		private readonly IEdmProperty property;

		// Token: 0x04000785 RID: 1925
		private readonly IEdmComplexTypeReference typeReference;

		// Token: 0x04000786 RID: 1926
		private readonly IEdmNavigationSource navigationSource;
	}
}
