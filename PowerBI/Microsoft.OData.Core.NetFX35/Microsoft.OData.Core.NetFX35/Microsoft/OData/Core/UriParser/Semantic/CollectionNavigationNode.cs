using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022A RID: 554
	public sealed class CollectionNavigationNode : EntityCollectionNode
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x00048F72 File Offset: 0x00047172
		public CollectionNavigationNode(IEdmNavigationProperty navigationProperty, SingleEntityNode source)
			: this(navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleEntityNode>(source, "source");
			this.source = source;
			this.navigationSource = ((source.NavigationSource != null) ? source.NavigationSource.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00048FAA File Offset: 0x000471AA
		public CollectionNavigationNode(IEdmNavigationProperty navigationProperty, IEdmNavigationSource source)
			: this(navigationProperty)
		{
			this.navigationSource = ((source != null) ? source.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00048FC8 File Offset: 0x000471C8
		private CollectionNavigationNode(IEdmNavigationProperty navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			if (navigationProperty.TargetMultiplicity() != EdmMultiplicity.Many)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveManyMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.collectionTypeReference = navigationProperty.Type.AsCollection();
			this.edmEntityTypeReference = this.collectionTypeReference.ElementType().AsEntityOrNull();
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00049028 File Offset: 0x00047228
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00049030 File Offset: 0x00047230
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.navigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0004903D File Offset: 0x0004723D
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00049045 File Offset: 0x00047245
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004904D File Offset: 0x0004724D
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00049055 File Offset: 0x00047255
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0004905D File Offset: 0x0004725D
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00049065 File Offset: 0x00047265
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionNavigationNode;
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00049069 File Offset: 0x00047269
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400086F RID: 2159
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x04000870 RID: 2160
		private readonly IEdmEntityTypeReference edmEntityTypeReference;

		// Token: 0x04000871 RID: 2161
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x04000872 RID: 2162
		private readonly SingleValueNode source;

		// Token: 0x04000873 RID: 2163
		private readonly IEdmNavigationSource navigationSource;
	}
}
