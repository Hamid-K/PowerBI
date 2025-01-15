using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000440 RID: 1088
	internal abstract class EntityWrapper<TEntity> : BaseEntityWrapper<TEntity> where TEntity : class
	{
		// Token: 0x0600350C RID: 13580 RVA: 0x000AABD0 File Offset: 0x000A8DD0
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, relationshipManager, overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000AAC34 File Offset: 0x000A8E34
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntityKey key, EntitySet set, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals)
			: base(entity, relationshipManager, set, context, mergeOption, identityType, overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
			this._keyStrategy.SetEntityKey(key);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000AACAD File Offset: 0x000A8EAD
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._changeTrackingStrategy.SetChangeTracker(changeTracker);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000AACBB File Offset: 0x000A8EBB
		public override void TakeSnapshot(EntityEntry entry)
		{
			this._changeTrackingStrategy.TakeSnapshot(entry);
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000AACC9 File Offset: 0x000A8EC9
		// (set) Token: 0x06003511 RID: 13585 RVA: 0x000AACD6 File Offset: 0x000A8ED6
		public override EntityKey EntityKey
		{
			get
			{
				return this._keyStrategy.GetEntityKey();
			}
			set
			{
				this._keyStrategy.SetEntityKey(value);
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000AACE4 File Offset: 0x000A8EE4
		public override EntityKey GetEntityKeyFromEntity()
		{
			return this._keyStrategy.GetEntityKeyFromEntity();
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000AACF1 File Offset: 0x000A8EF1
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.CollectionAdd(relatedEnd, value);
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000AAD08 File Offset: 0x000A8F08
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return this._propertyStrategy != null && this._propertyStrategy.CollectionRemove(relatedEnd, value);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000AAD24 File Offset: 0x000A8F24
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy != null && this._propertyStrategy.GetNavigationPropertyValue(relatedEnd) == null)
			{
				object obj = this._propertyStrategy.CollectionCreate(relatedEnd);
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, obj);
			}
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000AAD63 File Offset: 0x000A8F63
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy == null)
			{
				return null;
			}
			return this._propertyStrategy.GetNavigationPropertyValue(relatedEnd);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000AAD7B File Offset: 0x000A8F7B
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, value);
			}
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000AAD92 File Offset: 0x000A8F92
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null && this._propertyStrategy.GetNavigationPropertyValue(relatedEnd) == value)
			{
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, null);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000AADB8 File Offset: 0x000A8FB8
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000AADC5 File Offset: 0x000A8FC5
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000AADCD File Offset: 0x000A8FCD
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			this._changeTrackingStrategy.SetCurrentValue(entry, member, ordinal, target, value);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000AADE1 File Offset: 0x000A8FE1
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			this._changeTrackingStrategy.UpdateCurrentValueRecord(value, entry);
		}

		// Token: 0x04001130 RID: 4400
		private readonly TEntity _entity;

		// Token: 0x04001131 RID: 4401
		private readonly IPropertyAccessorStrategy _propertyStrategy;

		// Token: 0x04001132 RID: 4402
		private readonly IChangeTrackingStrategy _changeTrackingStrategy;

		// Token: 0x04001133 RID: 4403
		private readonly IEntityKeyStrategy _keyStrategy;
	}
}
