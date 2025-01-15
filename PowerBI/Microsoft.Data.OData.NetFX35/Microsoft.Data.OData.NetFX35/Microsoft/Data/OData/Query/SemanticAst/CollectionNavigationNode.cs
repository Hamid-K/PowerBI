using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000079 RID: 121
	public sealed class CollectionNavigationNode : EntityCollectionNode
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000AC8E File Offset: 0x00008E8E
		public CollectionNavigationNode(IEdmNavigationProperty navigationProperty, SingleEntityNode source)
			: this(navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleEntityNode>(source, "source");
			this.source = source;
			this.entitySet = ((source.EntitySet != null) ? source.EntitySet.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000ACC6 File Offset: 0x00008EC6
		public CollectionNavigationNode(IEdmNavigationProperty navigationProperty, IEdmEntitySet sourceSet)
			: this(navigationProperty)
		{
			this.entitySet = ((sourceSet != null) ? sourceSet.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		private CollectionNavigationNode(IEdmNavigationProperty navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			if (navigationProperty.TargetMultiplicityTemporary() != EdmMultiplicity.Many)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveManyMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.collectionTypeReference = navigationProperty.Type.AsCollection();
			this.edmEntityTypeReference = this.collectionTypeReference.ElementType().AsEntityOrNull();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000AD44 File Offset: 0x00008F44
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.navigationProperty.TargetMultiplicityTemporary();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000AD59 File Offset: 0x00008F59
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000AD61 File Offset: 0x00008F61
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000AD69 File Offset: 0x00008F69
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000AD71 File Offset: 0x00008F71
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmEntityTypeReference;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000AD79 File Offset: 0x00008F79
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000AD81 File Offset: 0x00008F81
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionNavigationNode;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AD85 File Offset: 0x00008F85
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000CB RID: 203
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040000CC RID: 204
		private readonly IEdmEntityTypeReference edmEntityTypeReference;

		// Token: 0x040000CD RID: 205
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x040000CE RID: 206
		private readonly SingleValueNode source;

		// Token: 0x040000CF RID: 207
		private readonly IEdmEntitySet entitySet;
	}
}
