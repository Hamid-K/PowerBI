using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000443 RID: 1091
	internal sealed class EntityWrapperWithRelationships<TEntity> : EntityWrapper<TEntity> where TEntity : class, IEntityWithRelationships
	{
		// Token: 0x06003535 RID: 13621 RVA: 0x000AB324 File Offset: 0x000A9524
		internal EntityWrapperWithRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, entity.RelationshipManager, key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000AB353 File Offset: 0x000A9553
		internal EntityWrapperWithRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, entity.RelationshipManager, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000AB36D File Offset: 0x000A956D
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000AB370 File Offset: 0x000A9570
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000AB372 File Offset: 0x000A9572
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}
	}
}
