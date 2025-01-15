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

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	public class EntityCollection<TEntity> : RelatedEnd, ICollection<TEntity>, IEnumerable<TEntity>, IEnumerable, IListSource where TEntity : class
	{
		// Token: 0x060037A2 RID: 14242 RVA: 0x000B60AB File Offset: 0x000B42AB
		public EntityCollection()
		{
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000B60B3 File Offset: 0x000B42B3
		internal EntityCollection(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
			: base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060037A4 RID: 14244 RVA: 0x000B60BE File Offset: 0x000B42BE
		// (remove) Token: 0x060037A5 RID: 14245 RVA: 0x000B60D7 File Offset: 0x000B42D7
		internal override event CollectionChangeEventHandler AssociationChangedForObjectView
		{
			add
			{
				this._onAssociationChangedforObjectView = (CollectionChangeEventHandler)Delegate.Combine(this._onAssociationChangedforObjectView, value);
			}
			remove
			{
				this._onAssociationChangedforObjectView = (CollectionChangeEventHandler)Delegate.Remove(this._onAssociationChangedforObjectView, value);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000B60F0 File Offset: 0x000B42F0
		private Dictionary<TEntity, IEntityWrapper> WrappedRelatedEntities
		{
			get
			{
				if (this._wrappedRelatedEntities == null)
				{
					this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
				}
				return this._wrappedRelatedEntities;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000B6110 File Offset: 0x000B4310
		public int Count
		{
			get
			{
				base.DeferredLoad();
				return this.CountInternal;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000B611E File Offset: 0x000B431E
		internal int CountInternal
		{
			get
			{
				if (this._wrappedRelatedEntities == null)
				{
					return 0;
				}
				return this._wrappedRelatedEntities.Count;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000B6135 File Offset: 0x000B4335
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000B6138 File Offset: 0x000B4338
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000B613B File Offset: 0x000B433B
		internal override void OnAssociationChanged(CollectionChangeAction collectionChangeAction, object entity)
		{
			if (!this._suppressEvents)
			{
				if (this._onAssociationChangedforObjectView != null)
				{
					this._onAssociationChangedforObjectView(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
				}
				if (this._onAssociationChanged != null)
				{
					this._onAssociationChanged(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
				}
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000B617C File Offset: 0x000B437C
		IList IListSource.GetList()
		{
			EntityType entityType = null;
			if (this.WrappedOwner.Entity != null && this.RelationshipSet != null)
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.ToEndMember.Name].EntitySet;
				EntityType entityType2 = (EntityType)((RefType)this.ToEndMember.TypeUsage.EdmType).ElementType;
				EntityType elementType = entitySet.ElementType;
				if (entityType2.IsAssignableFrom(elementType))
				{
					entityType = elementType;
				}
				else
				{
					entityType = entityType2;
				}
			}
			return ObjectViewFactory.CreateViewForEntityCollection<TEntity>(entityType, this);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000B6205 File Offset: 0x000B4405
		public override void Load(MergeOption mergeOption)
		{
			this.CheckOwnerNull();
			this.Load(null, mergeOption);
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000B6215 File Offset: 0x000B4415
		public override Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			this.CheckOwnerNull();
			cancellationToken.ThrowIfCancellationRequested();
			return this.LoadAsync(null, mergeOption, cancellationToken);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000B6230 File Offset: 0x000B4430
		public void Attach(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.CheckOwnerNull();
			IList<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (TEntity tentity in entities)
			{
				list.Add(this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext));
			}
			base.Attach(list, true);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000B62B0 File Offset: 0x000B44B0
		public void Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			base.Attach(new IEntityWrapper[] { this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext) }, false);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000B62E8 File Offset: 0x000B44E8
		internal virtual void Load(List<IEntityWrapper> collection, MergeOption mergeOption)
		{
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityCollection", out flag);
			this._suppressEvents = true;
			try
			{
				if (collection == null)
				{
					IEnumerable<TEntity> enumerable;
					if (flag)
					{
						enumerable = objectQuery.Execute(objectQuery.MergeOption);
					}
					else
					{
						enumerable = Enumerable.Empty<TEntity>();
					}
					this.Merge<TEntity>(enumerable, mergeOption, true);
				}
				else
				{
					this.Merge<TEntity>(collection, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000B635C File Offset: 0x000B455C
		internal virtual async Task LoadAsync(List<IEntityWrapper> collection, MergeOption mergeOption, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityCollection", out flag);
			this._suppressEvents = true;
			try
			{
				if (collection == null)
				{
					IEnumerable<TEntity> enumerable;
					if (flag)
					{
						enumerable = await (await objectQuery.ExecuteAsync(objectQuery.MergeOption, cancellationToken).WithCurrentCulture<ObjectResult<TEntity>>()).ToListAsync(cancellationToken).WithCurrentCulture<List<TEntity>>();
					}
					else
					{
						enumerable = Enumerable.Empty<TEntity>();
					}
					this.Merge<TEntity>(enumerable, mergeOption, true);
				}
				else
				{
					this.Merge<TEntity>(collection, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000B63B9 File Offset: 0x000B45B9
		public void Add(TEntity item)
		{
			Check.NotNull<TEntity>(item, "item");
			base.Add(this.EntityWrapperFactory.WrapEntityUsingContext(item, this.ObjectContext));
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000B63E4 File Offset: 0x000B45E4
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddEntity);
			}
			this.VerifyType(wrappedEntity);
			base.AddToCache(wrappedEntity, false);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedEntity.Entity);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000B641E File Offset: 0x000B461E
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelatedEnd_UnableToRemoveEntity);
			}
			bool flag = base.RemoveFromCache(wrappedEntity, false, false);
			this.OnAssociationChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
			return flag;
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000B6452 File Offset: 0x000B4652
		public bool Remove(TEntity item)
		{
			Check.NotNull<TEntity>(item, "item");
			base.DeferredLoad();
			return this.RemoveInternal(item);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000B646D File Offset: 0x000B466D
		internal bool RemoveInternal(TEntity entity)
		{
			return base.Remove(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext), false);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000B6490 File Offset: 0x000B4690
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedRelatedEntities != null && this.ObjectContext != null)
			{
				foreach (IEntityWrapper entityWrapper in new List<IEntityWrapper>(this._wrappedRelatedEntities.Values))
				{
					IEntityWrapper entityWrapper2 = this.EntityWrapperFactory.WrapEntityUsingContext(entityWrapper.Entity, this.WrappedOwner.Context);
					if (entityWrapper2 != entityWrapper)
					{
						this._wrappedRelatedEntities[(TEntity)((object)entityWrapper2.Entity)] = entityWrapper2;
					}
					base.IncludeEntity(entityWrapper2, addRelationshipAsUnchanged, doAttach);
				}
			}
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000B653C File Offset: 0x000B473C
		internal override void Exclude()
		{
			if (this._wrappedRelatedEntities != null && this.ObjectContext != null)
			{
				if (!base.IsForeignKey)
				{
					using (Dictionary<TEntity, IEntityWrapper>.ValueCollection.Enumerator enumerator = this._wrappedRelatedEntities.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IEntityWrapper entityWrapper = enumerator.Current;
							base.ExcludeEntity(entityWrapper);
						}
						return;
					}
				}
				TransactionManager transactionManager = this.ObjectContext.ObjectStateManager.TransactionManager;
				foreach (IEntityWrapper entityWrapper2 in new List<IEntityWrapper>(this._wrappedRelatedEntities.Values))
				{
					EntityReference entityReference = base.GetOtherEndOfRelationship(entityWrapper2) as EntityReference;
					bool flag = transactionManager.PopulatedEntityReferences.Contains(entityReference);
					bool flag2 = transactionManager.AlignedEntityReferences.Contains(entityReference);
					if (flag || flag2)
					{
						entityReference.Remove(entityReference.CachedValue, flag, false, false, false, true);
						if (flag)
						{
							transactionManager.PopulatedEntityReferences.Remove(entityReference);
						}
						else
						{
							transactionManager.AlignedEntityReferences.Remove(entityReference);
						}
					}
					else
					{
						base.ExcludeEntity(entityWrapper2);
					}
				}
			}
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000B6684 File Offset: 0x000B4884
		internal override void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			if (this._wrappedRelatedEntities != null)
			{
				foreach (IEntityWrapper entityWrapper in new List<IEntityWrapper>(this._wrappedRelatedEntities.Values))
				{
					if (wrappedEntity.Entity == entityWrapper.Entity && navigation.Equals(base.RelationshipNavigation))
					{
						base.Remove(entityWrapper, false, false, false, false, false);
					}
					else
					{
						base.Remove(entityWrapper, true, doCascadeDelete, false, false, false);
					}
				}
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000B6718 File Offset: 0x000B4918
		internal override void ClearWrappedValues()
		{
			if (this._wrappedRelatedEntities != null)
			{
				this._wrappedRelatedEntities.Clear();
			}
			if (this._relatedEntities != null)
			{
				this._relatedEntities.Clear();
			}
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000B6740 File Offset: 0x000B4940
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000B6750 File Offset: 0x000B4950
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_InvalidContainedType_Collection(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName));
			}
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000B6785 File Offset: 0x000B4985
		internal override bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			if (this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.Remove((TEntity)((object)wrappedEntity.Entity)))
			{
				if (resetIsLoaded)
				{
					this._isLoaded = false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000B67B4 File Offset: 0x000B49B4
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			return base.TargetAccessor.HasProperty && this.WrappedOwner.CollectionRemove(this, wrappedEntity.Entity);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000B67D7 File Offset: 0x000B49D7
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000B67D9 File Offset: 0x000B49D9
		internal override bool IsEmpty()
		{
			return this._wrappedRelatedEntities == null || this._wrappedRelatedEntities.Count == 0;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000B67F3 File Offset: 0x000B49F3
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000B67F5 File Offset: 0x000B49F5
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000B67FE File Offset: 0x000B49FE
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey((TEntity)((object)wrappedEntity.Entity));
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000B6820 File Offset: 0x000B4A20
		public new IEnumerator<TEntity> GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000B683D File Offset: 0x000B4A3D
		IEnumerator IEnumerable.GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000B685A File Offset: 0x000B4A5A
		internal override IEnumerable GetInternalEnumerable()
		{
			return this.WrappedRelatedEntities.Keys;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B6867 File Offset: 0x000B4A67
		internal override IEnumerable<IEntityWrapper> GetWrappedEntities()
		{
			return this.WrappedRelatedEntities.Values;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000B6874 File Offset: 0x000B4A74
		public void Clear()
		{
			base.DeferredLoad();
			if (this.WrappedOwner.Entity != null)
			{
				bool flag = this.CountInternal > 0;
				if (this._wrappedRelatedEntities != null)
				{
					List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
					try
					{
						this._suppressEvents = true;
						foreach (IEntityWrapper entityWrapper in list)
						{
							base.Remove(entityWrapper, false);
							if (base.UsingNoTracking)
							{
								base.GetOtherEndOfRelationship(entityWrapper).OnRelatedEndClear();
							}
						}
					}
					finally
					{
						this._suppressEvents = false;
					}
					if (base.UsingNoTracking)
					{
						this._isLoaded = false;
					}
				}
				if (flag)
				{
					this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
					return;
				}
			}
			else if (this._wrappedRelatedEntities != null)
			{
				this._wrappedRelatedEntities.Clear();
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000B695C File Offset: 0x000B4B5C
		public bool Contains(TEntity item)
		{
			base.DeferredLoad();
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey(item);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000B697A File Offset: 0x000B4B7A
		public void CopyTo(TEntity[] array, int arrayIndex)
		{
			base.DeferredLoad();
			this.WrappedRelatedEntities.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000B6994 File Offset: 0x000B4B94
		internal virtual void BulkDeleteAll(List<object> list)
		{
			if (list.Count > 0)
			{
				this._suppressEvents = true;
				try
				{
					foreach (object obj in list)
					{
						this.RemoveInternal(obj as TEntity);
					}
				}
				finally
				{
					this._suppressEvents = false;
				}
				this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
			}
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000B6A1C File Offset: 0x000B4C1C
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			if (!base.TargetAccessor.HasProperty)
			{
				return false;
			}
			bool flag = base.DisableLazyLoading();
			try
			{
				object navigationPropertyValue = this.WrappedOwner.GetNavigationPropertyValue(this);
				if (navigationPropertyValue != null)
				{
					IEnumerable<TEntity> enumerable = navigationPropertyValue as IEnumerable<TEntity>;
					if (enumerable == null)
					{
						throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(base.TargetAccessor.PropertyName, this.WrappedOwner.Entity.GetType().FullName));
					}
					HashSet<TEntity> hashSet = navigationPropertyValue as HashSet<TEntity>;
					if (!wrapper.OverridesEqualsOrGetHashCode || (hashSet != null && hashSet.Comparer is ObjectReferenceEqualityComparer))
					{
						return enumerable.Contains((TEntity)((object)wrapper.Entity));
					}
					return enumerable.Any((TEntity o) => o == wrapper.Entity);
				}
			}
			finally
			{
				base.ResetLazyLoading(flag);
			}
			return false;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000B6B0C File Offset: 0x000B4D0C
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000B6B10 File Offset: 0x000B4D10
		[OnSerializing]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(this.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityCollection"));
			}
			this._relatedEntities = ((this._wrappedRelatedEntities == null) ? null : new HashSet<TEntity>(this._wrappedRelatedEntities.Keys, ObjectReferenceEqualityComparer.Default));
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000B6B68 File Offset: 0x000B4D68
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnCollectionDeserialized(StreamingContext context)
		{
			if (this._relatedEntities != null)
			{
				this._relatedEntities.OnDeserialization(null);
				this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
				foreach (TEntity tentity in this._relatedEntities)
				{
					this._wrappedRelatedEntities.Add(tentity, this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext));
				}
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000B6BFC File Offset: 0x000B4DFC
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			this.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000B6C1D File Offset: 0x000B4E1D
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000B6C25 File Offset: 0x000B4E25
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.WrappedRelatedEntities[(TEntity)((object)wrappedEntity.Entity)] = wrappedEntity;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000B6C3E File Offset: 0x000B4E3E
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.CollectionAdd(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x040012D8 RID: 4824
		private HashSet<TEntity> _relatedEntities;

		// Token: 0x040012D9 RID: 4825
		[NonSerialized]
		private CollectionChangeEventHandler _onAssociationChangedforObjectView;

		// Token: 0x040012DA RID: 4826
		[NonSerialized]
		private Dictionary<TEntity, IEntityWrapper> _wrappedRelatedEntities;
	}
}
