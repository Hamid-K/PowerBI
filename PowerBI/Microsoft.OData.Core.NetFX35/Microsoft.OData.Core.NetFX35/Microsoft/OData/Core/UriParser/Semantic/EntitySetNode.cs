using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200023B RID: 571
	internal sealed class EntitySetNode : EntityCollectionNode
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x00049824 File Offset: 0x00047A24
		public EntitySetNode(IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			this.entitySet = entitySet;
			this.entityType = new EdmEntityTypeReference(this.NavigationSource.EntityType(), false);
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.entityType);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00049871 File Offset: 0x00047A71
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00049879 File Offset: 0x00047A79
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00049881 File Offset: 0x00047A81
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00049889 File Offset: 0x00047A89
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00049891 File Offset: 0x00047A91
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntitySet;
			}
		}

		// Token: 0x04000898 RID: 2200
		private readonly IEdmEntitySet entitySet;

		// Token: 0x04000899 RID: 2201
		private readonly IEdmEntityTypeReference entityType;

		// Token: 0x0400089A RID: 2202
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
