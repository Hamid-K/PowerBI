using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000104 RID: 260
	public sealed class AggregatedCollectionPropertyNode : SingleResourceNode
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x00025C30 File Offset: 0x00023E30
		public AggregatedCollectionPropertyNode(CollectionNavigationNode source, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNavigationNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			if (property.Type.IsCollection())
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessTypeShouldNotBeCollection(property.Name));
			}
			this.source = source;
			this.property = property;
			this.typeReference = property.Type.AsComplex();
			this.navigationSource = source.NavigationSource;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00025CBE File Offset: 0x00023EBE
		public CollectionNavigationNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00025CC6 File Offset: 0x00023EC6
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00025CCE File Offset: 0x00023ECE
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.Property.Type;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00025CDB File Offset: 0x00023EDB
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00025CE3 File Offset: 0x00023EE3
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00025CEB File Offset: 0x00023EEB
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.AggregatedCollectionPropertyNode;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00025CEF File Offset: 0x00023EEF
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000769 RID: 1897
		private readonly CollectionNavigationNode source;

		// Token: 0x0400076A RID: 1898
		private readonly IEdmProperty property;

		// Token: 0x0400076B RID: 1899
		private readonly IEdmComplexTypeReference typeReference;

		// Token: 0x0400076C RID: 1900
		private readonly IEdmNavigationSource navigationSource;
	}
}
