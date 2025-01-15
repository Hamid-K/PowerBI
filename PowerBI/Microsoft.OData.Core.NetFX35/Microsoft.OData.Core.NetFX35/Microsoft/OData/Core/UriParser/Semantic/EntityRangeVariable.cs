using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000237 RID: 567
	public sealed class EntityRangeVariable : RangeVariable
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x000496A0 File Offset: 0x000478A0
		public EntityRangeVariable(string name, IEdmEntityTypeReference entityType, EntityCollectionNode entityCollectionNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityType, "entityType");
			this.name = name;
			this.entityTypeReference = entityType;
			this.entityCollectionNode = entityCollectionNode;
			this.navigationSource = ((entityCollectionNode != null) ? entityCollectionNode.NavigationSource : null);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000496F0 File Offset: 0x000478F0
		public EntityRangeVariable(string name, IEdmEntityTypeReference entityType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityType, "entityType");
			this.name = name;
			this.entityTypeReference = entityType;
			this.entityCollectionNode = null;
			this.navigationSource = navigationSource;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004972A File Offset: 0x0004792A
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00049732 File Offset: 0x00047932
		public EntityCollectionNode EntityCollectionNode
		{
			get
			{
				return this.entityCollectionNode;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0004973A File Offset: 0x0004793A
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00049742 File Offset: 0x00047942
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004974A File Offset: 0x0004794A
		public IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00049752 File Offset: 0x00047952
		public override int Kind
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000890 RID: 2192
		private readonly string name;

		// Token: 0x04000891 RID: 2193
		private readonly EntityCollectionNode entityCollectionNode;

		// Token: 0x04000892 RID: 2194
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x04000893 RID: 2195
		private readonly IEdmEntityTypeReference entityTypeReference;
	}
}
