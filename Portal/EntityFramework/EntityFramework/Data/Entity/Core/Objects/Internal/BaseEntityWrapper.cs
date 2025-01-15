using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000434 RID: 1076
	internal abstract class BaseEntityWrapper<TEntity> : IEntityWrapper where TEntity : class
	{
		// Token: 0x0600343D RID: 13373 RVA: 0x000A8907 File Offset: 0x000A6B07
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager, bool overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._relationshipManager = relationshipManager;
			if (overridesEquals)
			{
				this._flags = BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals;
			}
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000A8930 File Offset: 0x000A6B30
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, bool overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._identityType = identityType;
			this._relationshipManager = relationshipManager;
			if (overridesEquals)
			{
				this._flags = BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals;
			}
			this.RelationshipManager.SetWrappedOwner(this, entity);
			if (entitySet != null)
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000A89A0 File Offset: 0x000A6BA0
		public RelationshipManager RelationshipManager
		{
			get
			{
				return this._relationshipManager;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000A89A8 File Offset: 0x000A6BA8
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x000A89B0 File Offset: 0x000A6BB0
		public ObjectContext Context { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000A89B9 File Offset: 0x000A6BB9
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x000A89C8 File Offset: 0x000A6BC8
		public MergeOption MergeOption
		{
			get
			{
				if ((this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking) == BaseEntityWrapper<TEntity>.WrapperFlags.None)
				{
					return MergeOption.AppendOnly;
				}
				return MergeOption.NoTracking;
			}
			private set
			{
				if (value == MergeOption.NoTracking)
				{
					this._flags |= BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking;
					return;
				}
				this._flags &= ~BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000A89EC File Offset: 0x000A6BEC
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x000A89F9 File Offset: 0x000A6BF9
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return (this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds) > BaseEntityWrapper<TEntity>.WrapperFlags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds;
					return;
				}
				this._flags &= ~BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds;
			}
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000A8A1C File Offset: 0x000A6C1C
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			this.Context = context;
			this.MergeOption = mergeOption;
			if (entitySet != null)
			{
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000A8A3D File Offset: 0x000A6C3D
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this.Context != context)
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.ResetContextOnRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000A8A64 File Offset: 0x000A6C64
		public void DetachContext()
		{
			if (this.Context != null && this.Context.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				MergeOption? originalMergeOption = this.Context.ObjectStateManager.TransactionManager.OriginalMergeOption;
				MergeOption mergeOption = MergeOption.NoTracking;
				if ((originalMergeOption.GetValueOrDefault() == mergeOption) & (originalMergeOption != null))
				{
					this.MergeOption = MergeOption.NoTracking;
					goto IL_005B;
				}
			}
			this.Context = null;
			IL_005B:
			this.RelationshipManager.DetachContextFromRelatedEnds();
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000A8AD7 File Offset: 0x000A6CD7
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000A8ADF File Offset: 0x000A6CDF
		public EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000A8AE8 File Offset: 0x000A6CE8
		public Type IdentityType
		{
			get
			{
				if (this._identityType == null)
				{
					this._identityType = EntityUtil.GetEntityIdentityType(typeof(TEntity));
				}
				return this._identityType;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000A8B13 File Offset: 0x000A6D13
		public bool OverridesEqualsOrGetHashCode
		{
			get
			{
				return (this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals) > BaseEntityWrapper<TEntity>.WrapperFlags.None;
			}
		}

		// Token: 0x0600344D RID: 13389
		public abstract void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600344E RID: 13390
		// (set) Token: 0x0600344F RID: 13391
		public abstract EntityKey EntityKey { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003450 RID: 13392
		public abstract bool OwnsRelationshipManager { get; }

		// Token: 0x06003451 RID: 13393
		public abstract EntityKey GetEntityKeyFromEntity();

		// Token: 0x06003452 RID: 13394
		public abstract void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06003453 RID: 13395
		public abstract void TakeSnapshot(EntityEntry entry);

		// Token: 0x06003454 RID: 13396
		public abstract void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x06003455 RID: 13397
		public abstract object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06003456 RID: 13398
		public abstract void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06003457 RID: 13399
		public abstract void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06003458 RID: 13400
		public abstract void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06003459 RID: 13401
		public abstract bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600345A RID: 13402
		public abstract object Entity { get; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600345B RID: 13403
		public abstract TEntity TypedEntity { get; }

		// Token: 0x0600345C RID: 13404
		public abstract void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x0600345D RID: 13405
		public abstract void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600345E RID: 13406
		public abstract bool RequiresRelationshipChangeTracking { get; }

		// Token: 0x040010E1 RID: 4321
		private readonly RelationshipManager _relationshipManager;

		// Token: 0x040010E2 RID: 4322
		private Type _identityType;

		// Token: 0x040010E3 RID: 4323
		private BaseEntityWrapper<TEntity>.WrapperFlags _flags;

		// Token: 0x02000A3E RID: 2622
		[Flags]
		private enum WrapperFlags
		{
			// Token: 0x04002A1D RID: 10781
			None = 0,
			// Token: 0x04002A1E RID: 10782
			NoTracking = 1,
			// Token: 0x04002A1F RID: 10783
			InitializingRelatedEnds = 2,
			// Token: 0x04002A20 RID: 10784
			OverridesEquals = 4
		}
	}
}
