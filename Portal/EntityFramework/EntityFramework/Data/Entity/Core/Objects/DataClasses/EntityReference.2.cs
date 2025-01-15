using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000478 RID: 1144
	[DataContract]
	[Serializable]
	public class EntityReference<TEntity> : EntityReference where TEntity : class
	{
		// Token: 0x06003804 RID: 14340 RVA: 0x000B7D7C File Offset: 0x000B5F7C
		public EntityReference()
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000B7D8F File Offset: 0x000B5F8F
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
			: base(wrappedOwner, navigation, relationshipFixer)
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000B7DA5 File Offset: 0x000B5FA5
		// (set) Token: 0x06003807 RID: 14343 RVA: 0x000B7DBD File Offset: 0x000B5FBD
		[SoapIgnore]
		[XmlIgnore]
		public TEntity Value
		{
			get
			{
				base.DeferredLoad();
				return (TEntity)((object)this.ReferenceValue.Entity);
			}
			set
			{
				this.ReferenceValue = this.EntityWrapperFactory.WrapEntityUsingContext(value, this.ObjectContext);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000B7DDC File Offset: 0x000B5FDC
		internal override IEntityWrapper CachedValue
		{
			get
			{
				return this._wrappedCachedValue;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000B7DE4 File Offset: 0x000B5FE4
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x000B7DF4 File Offset: 0x000B5FF4
		internal override IEntityWrapper ReferenceValue
		{
			get
			{
				this.CheckOwnerNull();
				return this._wrappedCachedValue;
			}
			set
			{
				this.CheckOwnerNull();
				if (value.Entity != null && value.Entity == this._wrappedCachedValue.Entity)
				{
					return;
				}
				if (value.Entity != null)
				{
					base.ValidateOwnerWithRIConstraints(value, (value == NullEntityWrapper.NullWrapper) ? null : value.EntityKey, true);
					ObjectContext objectContext = this.ObjectContext ?? value.Context;
					if (objectContext != null)
					{
						objectContext.ObjectStateManager.TransactionManager.EntityBeingReparented = base.GetDependentEndOfReferentialConstraint(value.Entity);
					}
					try
					{
						base.Add(value, false);
						return;
					}
					finally
					{
						if (objectContext != null)
						{
							objectContext.ObjectStateManager.TransactionManager.EntityBeingReparented = null;
						}
					}
				}
				if (base.UsingNoTracking)
				{
					if (this._wrappedCachedValue.Entity != null)
					{
						base.GetOtherEndOfRelationship(this._wrappedCachedValue).OnRelatedEndClear();
					}
					this._isLoaded = false;
				}
				else if (this.ObjectContext != null && this.ObjectContext.ContextOptions.UseConsistentNullReferenceBehavior)
				{
					base.AttemptToNullFKsOnRefOrKeySetToNull();
				}
				this.ClearCollectionOrRef(null, null, false);
			}
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000B7F00 File Offset: 0x000B6100
		public override void Load(MergeOption mergeOption)
		{
			this.CheckOwnerNull();
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityReference", out flag);
			this._suppressEvents = true;
			try
			{
				IList<TEntity> list = null;
				if (flag)
				{
					list = objectQuery.Execute(objectQuery.MergeOption).ToList<TEntity>();
				}
				this.HandleRefreshedValue(mergeOption, list);
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000B7F6C File Offset: 0x000B616C
		public override async Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			this.CheckOwnerNull();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityReference", out flag);
			this._suppressEvents = true;
			try
			{
				IList<TEntity> list = null;
				if (flag)
				{
					list = await (await objectQuery.ExecuteAsync(objectQuery.MergeOption, cancellationToken).WithCurrentCulture<ObjectResult<TEntity>>()).ToListAsync(cancellationToken).WithCurrentCulture<List<TEntity>>();
				}
				this.HandleRefreshedValue(mergeOption, list);
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000B7FC4 File Offset: 0x000B61C4
		private void HandleRefreshedValue(MergeOption mergeOption, IList<TEntity> refreshedValue)
		{
			if (refreshedValue == null || !refreshedValue.Any<TEntity>())
			{
				if (!((AssociationType)this.RelationMetadata).IsForeignKey && this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
				{
					throw Error.EntityReference_LessThanExpectedRelatedEntitiesFound();
				}
				if (mergeOption == MergeOption.OverwriteChanges || mergeOption == MergeOption.PreserveChanges)
				{
					EntityKey entityKey = this.WrappedOwner.EntityKey;
					if (entityKey == null)
					{
						throw Error.EntityKey_UnexpectedNull();
					}
					this.ObjectContext.ObjectStateManager.RemoveRelationships(mergeOption, (AssociationSet)this.RelationshipSet, entityKey, (AssociationEndMember)this.FromEndMember);
				}
				this._isLoaded = true;
				return;
			}
			else
			{
				if (refreshedValue.Count<TEntity>() == 1)
				{
					this.Merge<TEntity>(refreshedValue, mergeOption, true);
					return;
				}
				throw Error.EntityReference_MoreThanExpectedRelatedEntitiesFound();
			}
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000B8067 File Offset: 0x000B6267
		internal override IEnumerable GetInternalEnumerable()
		{
			this.CheckOwnerNull();
			if (this.ReferenceValue.Entity != null)
			{
				return new object[] { this.ReferenceValue.Entity };
			}
			return Enumerable.Empty<object>();
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000B8096 File Offset: 0x000B6296
		internal override IEnumerable<IEntityWrapper> GetWrappedEntities()
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				return new IEntityWrapper[] { this._wrappedCachedValue };
			}
			return new IEntityWrapper[0];
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000B80BB File Offset: 0x000B62BB
		public void Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.CheckOwnerNull();
			base.Attach(new IEntityWrapper[] { this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext) }, false);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000B80F8 File Offset: 0x000B62F8
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(this._wrappedCachedValue.Entity, this.WrappedOwner.Context);
				if (entityWrapper != this._wrappedCachedValue)
				{
					this._wrappedCachedValue = entityWrapper;
				}
				base.IncludeEntity(this._wrappedCachedValue, addRelationshipAsUnchanged, doAttach);
				return;
			}
			if (base.DetachedEntityKey != null)
			{
				this.IncludeEntityKey(doAttach);
			}
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000B8168 File Offset: 0x000B6368
		private void IncludeEntityKey(bool doAttach)
		{
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			bool flag = false;
			bool flag2 = false;
			EntityEntry entityEntry = objectStateManager.FindEntityEntry(base.DetachedEntityKey);
			if (entityEntry == null)
			{
				flag2 = true;
				flag = true;
			}
			else if (entityEntry.IsKeyEntry)
			{
				if (this.FromEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					foreach (RelationshipEntry relationshipEntry in this.ObjectContext.ObjectStateManager.FindRelationshipsByKey(base.DetachedEntityKey))
					{
						if (relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)this.RelationshipSet, (AssociationEndMember)this.ToEndMember, base.DetachedEntityKey) && relationshipEntry.State != EntityState.Deleted)
						{
							throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
						}
					}
				}
				flag = true;
			}
			else
			{
				IEntityWrapper wrappedEntity = entityEntry.WrappedEntity;
				if (entityEntry.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
				}
				RelatedEnd relatedEndInternal = wrappedEntity.RelationshipManager.GetRelatedEndInternal(base.RelationshipName, base.RelationshipNavigation.From);
				if (this.FromEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many && !relatedEndInternal.IsEmpty())
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
				}
				base.Add(wrappedEntity, true, doAttach, false, true, true);
				objectStateManager.TransactionManager.PopulatedEntityReferences.Add(this);
			}
			if (flag && !base.IsForeignKey)
			{
				if (flag2)
				{
					EntitySet entitySet = base.DetachedEntityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace);
					objectStateManager.AddKeyEntry(base.DetachedEntityKey, entitySet);
				}
				EntityKey entityKey = this.WrappedOwner.EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				RelationshipWrapper relationshipWrapper = new RelationshipWrapper((AssociationSet)this.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, base.DetachedEntityKey);
				objectStateManager.AddNewRelation(relationshipWrapper, doAttach ? EntityState.Unchanged : EntityState.Added);
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000B8358 File Offset: 0x000B6558
		internal override void Exclude()
		{
			if (this._wrappedCachedValue.Entity == null)
			{
				if (base.DetachedEntityKey != null)
				{
					this.ExcludeEntityKey();
				}
				return;
			}
			TransactionManager transactionManager = this.ObjectContext.ObjectStateManager.TransactionManager;
			bool flag = transactionManager.PopulatedEntityReferences.Contains(this);
			bool flag2 = transactionManager.AlignedEntityReferences.Contains(this);
			if ((transactionManager.ProcessedEntities != null && transactionManager.ProcessedEntities.Contains(this._wrappedCachedValue)) || (!flag && !flag2))
			{
				base.ExcludeEntity(this._wrappedCachedValue);
				return;
			}
			RelationshipEntry relationshipEntry = (base.IsForeignKey ? null : base.FindRelationshipEntryInObjectStateManager(this._wrappedCachedValue));
			base.Remove(this._wrappedCachedValue, flag, false, false, false, true);
			if (relationshipEntry != null && relationshipEntry.State != EntityState.Detached)
			{
				relationshipEntry.AcceptChanges();
			}
			if (flag)
			{
				transactionManager.PopulatedEntityReferences.Remove(this);
				return;
			}
			transactionManager.AlignedEntityReferences.Remove(this);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000B843C File Offset: 0x000B663C
		private void ExcludeEntityKey()
		{
			EntityKey entityKey = this.WrappedOwner.EntityKey;
			RelationshipEntry relationshipEntry = this.ObjectContext.ObjectStateManager.FindRelationship(this.RelationshipSet, new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.From, entityKey), new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.To, base.DetachedEntityKey));
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
				if (relationshipEntry.State != EntityState.Detached)
				{
					relationshipEntry.AcceptChanges();
				}
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000B84AC File Offset: 0x000B66AC
		internal override void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			if (wrappedEntity == null)
			{
				wrappedEntity = NullEntityWrapper.NullWrapper;
			}
			if (this._wrappedCachedValue.Entity != null)
			{
				if (wrappedEntity.Entity == this._wrappedCachedValue.Entity && navigation.Equals(base.RelationshipNavigation))
				{
					base.Remove(this._wrappedCachedValue, false, false, false, false, false);
				}
				else
				{
					base.Remove(this._wrappedCachedValue, true, doCascadeDelete, false, true, false);
				}
			}
			else if (this.WrappedOwner.Entity != null && this.WrappedOwner.Context != null && !base.UsingNoTracking)
			{
				this.WrappedOwner.Context.ObjectStateManager.GetEntityEntry(this.WrappedOwner.Entity).DeleteRelationshipsThatReferenceKeys(this.RelationshipSet, this.ToEndMember);
			}
			if (this.WrappedOwner.Entity != null)
			{
				base.DetachedEntityKey = null;
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000B857E File Offset: 0x000B677E
		internal override void ClearWrappedValues()
		{
			this._cachedValue = default(TEntity);
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000B8597 File Offset: 0x000B6797
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000B85A7 File Offset: 0x000B67A7
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_InvalidContainedType_Reference(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName));
			}
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000B85DC File Offset: 0x000B67DC
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			this.CheckOwnerNull();
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000B85E4 File Offset: 0x000B67E4
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			this.CheckOwnerNull();
			return false;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000B85ED File Offset: 0x000B67ED
		internal override bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
			this._cachedValue = default(TEntity);
			if (resetIsLoaded)
			{
				this._isLoaded = false;
			}
			if (this.ObjectContext != null && base.IsForeignKey && !preserveForeignKey)
			{
				base.NullAllForeignKeys();
			}
			return true;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000B862A File Offset: 0x000B682A
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.RemoveNavigationPropertyValue(this, wrappedEntity.Entity);
			}
			return true;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000B864C File Offset: 0x000B684C
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndMember)
					{
						if (visited.Contains(this._wrappedCachedValue))
						{
							throw new InvalidOperationException(Strings.RelationshipManager_CircularRelationshipsWithReferentialConstraints);
						}
						visited.Add(this._wrappedCachedValue);
						Dictionary<string, KeyValuePair<object, IntBox>> dictionary;
						this._wrappedCachedValue.RelationshipManager.RetrieveReferentialConstraintProperties(out dictionary, visited, true);
						for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
						{
							EntityEntry.AddOrIncreaseCounter(referentialConstraint, properties, referentialConstraint.ToProperties[i].Name, dictionary[referentialConstraint.FromProperties[i].Name].Key);
						}
					}
				}
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000B8754 File Offset: 0x000B6954
		internal override bool IsEmpty()
		{
			return this._wrappedCachedValue.Entity == null;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000B8764 File Offset: 0x000B6964
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
			if (applyConstraints && !this.IsEmpty())
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
			}
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000B8792 File Offset: 0x000B6992
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000B879B File Offset: 0x000B699B
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			return this._wrappedCachedValue.Entity != null && this._wrappedCachedValue.Entity == wrappedEntity.Entity;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000B87C0 File Offset: 0x000B69C0
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			this.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000B87E1 File Offset: 0x000B69E1
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000B87EC File Offset: 0x000B69EC
		internal void InitializeWithValue(RelatedEnd relatedEnd)
		{
			EntityReference<TEntity> entityReference = relatedEnd as EntityReference<TEntity>;
			if (entityReference != null && entityReference._wrappedCachedValue.Entity != null)
			{
				this._wrappedCachedValue = entityReference._wrappedCachedValue;
				this._cachedValue = (TEntity)((object)this._wrappedCachedValue.Entity);
			}
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000B8832 File Offset: 0x000B6A32
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			return base.TargetAccessor.HasProperty && this.WrappedOwner.GetNavigationPropertyValue(this) == wrapper.Entity;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000B8858 File Offset: 0x000B6A58
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
			if (base.TargetAccessor.HasProperty)
			{
				object navigationPropertyValue = this.WrappedOwner.GetNavigationPropertyValue(this);
				if (navigationPropertyValue != null && navigationPropertyValue != wrapper.Entity)
				{
					throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
				}
			}
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000B88AC File Offset: 0x000B6AAC
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnRefDeserialized(StreamingContext context)
		{
			this._wrappedCachedValue = this.EntityWrapperFactory.WrapEntityUsingContext(this._cachedValue, this.ObjectContext);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000B88D0 File Offset: 0x000B6AD0
		[OnSerializing]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(this.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityReference"));
			}
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000B88F4 File Offset: 0x000B6AF4
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (wrappedEntity != this._wrappedCachedValue)
			{
				TransactionManager transactionManager = ((this.ObjectContext != null) ? this.ObjectContext.ObjectStateManager.TransactionManager : null);
				if (applyConstraints && this._wrappedCachedValue.Entity != null && (transactionManager == null || transactionManager.ProcessedEntities == null || transactionManager.ProcessedEntities.Contains(this._wrappedCachedValue)))
				{
					throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
				}
				if (transactionManager != null && wrappedEntity.Entity != null)
				{
					transactionManager.BeginRelatedEndAdd();
				}
				try
				{
					this.ClearCollectionOrRef(null, null, false);
					this._wrappedCachedValue = wrappedEntity;
					this._cachedValue = (TEntity)((object)wrappedEntity.Entity);
				}
				finally
				{
					if (transactionManager != null && transactionManager.IsRelatedEndAdd)
					{
						transactionManager.EndRelatedEndAdd();
					}
				}
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000B89D0 File Offset: 0x000B6BD0
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.SetNavigationPropertyValue(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x040012E1 RID: 4833
		private TEntity _cachedValue;

		// Token: 0x040012E2 RID: 4834
		[NonSerialized]
		private IEntityWrapper _wrappedCachedValue;
	}
}
