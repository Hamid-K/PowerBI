using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015C RID: 348
	public sealed class SingleNavigationNode : SingleEntityNode
	{
		// Token: 0x06000F01 RID: 3841 RVA: 0x0002B50C File Offset: 0x0002970C
		public SingleNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, bindingPath)
		{
			this.source = source;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002B530 File Offset: 0x00029730
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

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002B5A1 File Offset: 0x000297A1
		internal SingleNavigationNode(SingleResourceNode source, IEdmNavigationProperty navigationProperty, List<ODataPathSegment> segments)
			: this(ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(source, "source").NavigationSource, navigationProperty, segments)
		{
			this.source = source;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002B5C4 File Offset: 0x000297C4
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

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002B64C File Offset: 0x0002984C
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0002B654 File Offset: 0x00029854
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002B65C File Offset: 0x0002985C
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.NavigationProperty.TargetMultiplicity();
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0002B669 File Offset: 0x00029869
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002B669 File Offset: 0x00029869
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002B671 File Offset: 0x00029871
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002B669 File Offset: 0x00029869
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002B679 File Offset: 0x00029879
		public IEdmPathExpression BindingPath
		{
			get
			{
				return this.bindingPath;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0002B681 File Offset: 0x00029881
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleNavigationNode;
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002B685 File Offset: 0x00029885
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000797 RID: 1943
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x04000798 RID: 1944
		private readonly SingleResourceNode source;

		// Token: 0x04000799 RID: 1945
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400079A RID: 1946
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x0400079B RID: 1947
		private readonly List<ODataPathSegment> parsedSegments;

		// Token: 0x0400079C RID: 1948
		private readonly IEdmPathExpression bindingPath;
	}
}
