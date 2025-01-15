using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000233 RID: 563
	public sealed class EntityCollectionCastNode : EntityCollectionNode
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x000494E0 File Offset: 0x000476E0
		public EntityCollectionCastNode(EntityCollectionNode source, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.source = source;
			this.edmTypeReference = new EdmEntityTypeReference(entityType, false);
			this.navigationSource = source.NavigationSource;
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.edmTypeReference);
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0004953A File Offset: 0x0004773A
		public EntityCollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00049542 File Offset: 0x00047742
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0004954A File Offset: 0x0004774A
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00049552 File Offset: 0x00047752
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0004955A File Offset: 0x0004775A
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00049562 File Offset: 0x00047762
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityCollectionCast;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00049566 File Offset: 0x00047766
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000884 RID: 2180
		private readonly EntityCollectionNode source;

		// Token: 0x04000885 RID: 2181
		private readonly IEdmEntityTypeReference edmTypeReference;

		// Token: 0x04000886 RID: 2182
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x04000887 RID: 2183
		private readonly IEdmNavigationSource navigationSource;
	}
}
