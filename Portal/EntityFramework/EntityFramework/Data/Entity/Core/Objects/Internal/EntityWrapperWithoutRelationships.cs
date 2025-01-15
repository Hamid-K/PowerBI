using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000442 RID: 1090
	internal sealed class EntityWrapperWithoutRelationships<TEntity> : EntityWrapper<TEntity> where TEntity : class
	{
		// Token: 0x06003530 RID: 13616 RVA: 0x000AB2D8 File Offset: 0x000A94D8
		internal EntityWrapperWithoutRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, RelationshipManager.Create(), key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000AB301 File Offset: 0x000A9501
		internal EntityWrapperWithoutRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, RelationshipManager.Create(), propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000AB315 File Offset: 0x000A9515
		public override bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000AB318 File Offset: 0x000A9518
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
			entry.TakeSnapshotOfRelationships();
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000AB320 File Offset: 0x000A9520
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return true;
			}
		}
	}
}
