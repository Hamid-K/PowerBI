using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012C RID: 300
	public sealed class CollectionNavigationNode : CollectionResourceNode
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00028CA1 File Offset: 0x00026EA1
		public CollectionNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, bindingPath)
		{
			this.source = source;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00028CC2 File Offset: 0x00026EC2
		internal CollectionNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(navigationProperty)
		{
			this.bindingPath = bindingPath;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, bindingPath) : null);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00028CE6 File Offset: 0x00026EE6
		internal CollectionNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> parsedSegments)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, parsedSegments)
		{
			this.source = source;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00028D07 File Offset: 0x00026F07
		private CollectionNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> parsedSegments)
			: this(navigationProperty)
		{
			this.parsedSegments = parsedSegments;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out this.bindingPath) : null);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00028D44 File Offset: 0x00026F44
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00028DA5 File Offset: 0x00026FA5
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00028DAD File Offset: 0x00026FAD
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.navigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00028DBA File Offset: 0x00026FBA
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00028DC2 File Offset: 0x00026FC2
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00028DCA File Offset: 0x00026FCA
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00028DC2 File Offset: 0x00026FC2
		public IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00028DC2 File Offset: 0x00026FC2
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00028DD2 File Offset: 0x00026FD2
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00028DDA File Offset: 0x00026FDA
		public IEdmPathExpression BindingPath
		{
			get
			{
				return this.bindingPath;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00028DE2 File Offset: 0x00026FE2
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionNavigationNode;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00028DE6 File Offset: 0x00026FE6
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400073A RID: 1850
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400073B RID: 1851
		private readonly IEdmEntityTypeReference edmEntityTypeReference;

		// Token: 0x0400073C RID: 1852
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x0400073D RID: 1853
		private readonly SingleResourceNode source;

		// Token: 0x0400073E RID: 1854
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x0400073F RID: 1855
		private readonly List<ODataPathSegment> parsedSegments;

		// Token: 0x04000740 RID: 1856
		private readonly IEdmPathExpression bindingPath;
	}
}
