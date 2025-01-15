using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044C RID: 1100
	internal sealed class LightweightEntityWrapper<TEntity> : BaseEntityWrapper<TEntity> where TEntity : class, IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
	{
		// Token: 0x06003583 RID: 13699 RVA: 0x000AC950 File Offset: 0x000AAB50
		internal LightweightEntityWrapper(TEntity entity, bool overridesEquals)
			: base(entity, entity.RelationshipManager, overridesEquals)
		{
			this._entity = entity;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000AC96C File Offset: 0x000AAB6C
		internal LightweightEntityWrapper(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, bool overridesEquals)
			: base(entity, entity.RelationshipManager, entitySet, context, mergeOption, identityType, overridesEquals)
		{
			this._entity = entity;
			this._entity.EntityKey = key;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000AC9A1 File Offset: 0x000AABA1
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000AC9B4 File Offset: 0x000AABB4
		public override void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000AC9B6 File Offset: 0x000AABB6
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000AC9B8 File Offset: 0x000AABB8
		// (set) Token: 0x06003589 RID: 13705 RVA: 0x000AC9CA File Offset: 0x000AABCA
		public override EntityKey EntityKey
		{
			get
			{
				return this._entity.EntityKey;
			}
			set
			{
				this._entity.EntityKey = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000AC9DD File Offset: 0x000AABDD
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000AC9E0 File Offset: 0x000AABE0
		public override EntityKey GetEntityKeyFromEntity()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000AC9F2 File Offset: 0x000AABF2
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000AC9F4 File Offset: 0x000AABF4
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000AC9F7 File Offset: 0x000AABF7
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000AC9F9 File Offset: 0x000AABF9
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000AC9FB File Offset: 0x000AABFB
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000AC9FD File Offset: 0x000AABFD
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000ACA00 File Offset: 0x000AAC00
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000ACA0D File Offset: 0x000AAC0D
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000ACA15 File Offset: 0x000AAC15
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000ACA21 File Offset: 0x000AAC21
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000ACA30 File Offset: 0x000AAC30
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001156 RID: 4438
		private readonly TEntity _entity;
	}
}
