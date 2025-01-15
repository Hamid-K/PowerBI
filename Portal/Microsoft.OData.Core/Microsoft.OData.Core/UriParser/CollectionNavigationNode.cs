using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000171 RID: 369
	public sealed class CollectionNavigationNode : CollectionResourceNode
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x000385D5 File Offset: 0x000367D5
		public CollectionNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, bindingPath)
		{
			this.source = source;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000385F6 File Offset: 0x000367F6
		internal CollectionNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(navigationProperty)
		{
			this.bindingPath = bindingPath;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, bindingPath) : null);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003861A File Offset: 0x0003681A
		internal CollectionNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> parsedSegments)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, parsedSegments)
		{
			this.source = source;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003863B File Offset: 0x0003683B
		private CollectionNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> parsedSegments)
			: this(navigationProperty)
		{
			this.parsedSegments = parsedSegments;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out this.bindingPath) : null);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00038678 File Offset: 0x00036878
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x000386D9 File Offset: 0x000368D9
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000386E1 File Offset: 0x000368E1
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.navigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x000386EE File Offset: 0x000368EE
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000386F6 File Offset: 0x000368F6
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x000386FE File Offset: 0x000368FE
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000386F6 File Offset: 0x000368F6
		public IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000386F6 File Offset: 0x000368F6
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00038706 File Offset: 0x00036906
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0003870E File Offset: 0x0003690E
		public IEdmPathExpression BindingPath
		{
			get
			{
				return this.bindingPath;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00038716 File Offset: 0x00036916
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionNavigationNode;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003871A File Offset: 0x0003691A
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400085B RID: 2139
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400085C RID: 2140
		private readonly IEdmEntityTypeReference edmEntityTypeReference;

		// Token: 0x0400085D RID: 2141
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x0400085E RID: 2142
		private readonly SingleResourceNode source;

		// Token: 0x0400085F RID: 2143
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x04000860 RID: 2144
		private readonly List<ODataPathSegment> parsedSegments;

		// Token: 0x04000861 RID: 2145
		private readonly IEdmPathExpression bindingPath;
	}
}
