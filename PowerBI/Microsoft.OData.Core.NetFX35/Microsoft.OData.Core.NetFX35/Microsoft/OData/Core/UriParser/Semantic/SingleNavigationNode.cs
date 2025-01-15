using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000261 RID: 609
	public sealed class SingleNavigationNode : SingleEntityNode
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x0004B97C File Offset: 0x00049B7C
		public SingleNavigationNode(IEdmNavigationProperty navigationProperty, SingleEntityNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			ExceptionUtils.CheckArgumentNotNull<SingleEntityNode>(source, "source");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicity();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.source = source;
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.navigationSource = ((source.NavigationSource != null) ? source.NavigationSource.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0004BA00 File Offset: 0x00049C00
		public SingleNavigationNode(IEdmNavigationProperty navigationProperty, IEdmNavigationSource sourceNavigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicity();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.navigationSource = ((sourceNavigationSource != null) ? sourceNavigationSource.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0004BA68 File Offset: 0x00049C68
		public SingleEntityNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0004BA70 File Offset: 0x00049C70
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0004BA78 File Offset: 0x00049C78
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.NavigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0004BA85 File Offset: 0x00049C85
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0004BA8D File Offset: 0x00049C8D
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0004BA95 File Offset: 0x00049C95
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0004BA9D File Offset: 0x00049C9D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleNavigationNode;
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0004BAA1 File Offset: 0x00049CA1
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008F0 RID: 2288
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008F1 RID: 2289
		private readonly SingleEntityNode source;

		// Token: 0x040008F2 RID: 2290
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040008F3 RID: 2291
		private readonly IEdmEntityTypeReference entityTypeReference;
	}
}
