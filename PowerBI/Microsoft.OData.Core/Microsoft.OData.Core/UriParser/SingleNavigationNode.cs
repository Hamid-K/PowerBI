using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AA RID: 426
	public sealed class SingleNavigationNode : SingleEntityNode
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x0003B5BA File Offset: 0x000397BA
		public SingleNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, bindingPath)
		{
			this.source = source;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003B5DC File Offset: 0x000397DC
		internal SingleNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicity();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.bindingPath = bindingPath;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, bindingPath) : null);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003B64D File Offset: 0x0003984D
		internal SingleNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> segments)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, segments)
		{
			this.source = source;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003B670 File Offset: 0x00039870
		private SingleNavigationNode(IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> segments)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicity();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.parsedSegments = segments;
			this.navigationSource = ((navigationSource != null) ? navigationSource.FindNavigationTarget(navigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out this.bindingPath) : null);
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0003B6F8 File Offset: 0x000398F8
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0003B700 File Offset: 0x00039900
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0003B708 File Offset: 0x00039908
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.NavigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0003B715 File Offset: 0x00039915
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0003B715 File Offset: 0x00039915
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003B71D File Offset: 0x0003991D
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0003B715 File Offset: 0x00039915
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0003B725 File Offset: 0x00039925
		public IEdmPathExpression BindingPath
		{
			get
			{
				return this.bindingPath;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003B72D File Offset: 0x0003992D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleNavigationNode;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0003B731 File Offset: 0x00039931
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008E4 RID: 2276
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008E5 RID: 2277
		private readonly SingleResourceNode source;

		// Token: 0x040008E6 RID: 2278
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040008E7 RID: 2279
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040008E8 RID: 2280
		private readonly List<ODataPathSegment> parsedSegments;

		// Token: 0x040008E9 RID: 2281
		private readonly IEdmPathExpression bindingPath;
	}
}
