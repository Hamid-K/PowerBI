using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000423 RID: 1059
	public class ObjectStateManager : IEntityStateManager
	{
		// Token: 0x060032F2 RID: 13042 RVA: 0x000A280F File Offset: 0x000A0A0F
		internal ObjectStateManager()
		{
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000A2824 File Offset: 0x000A0A24
		public ObjectStateManager(MetadataWorkspace metadataWorkspace)
		{
			Check.NotNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			this._metadataWorkspace = metadataWorkspace;
			this._metadataStore = new Dictionary<EdmType, StateManagerTypeMetadata>();
			this._metadataMapping = new Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata>(EntitySetQualifiedType.EqualityComparer);
			this._isDisposed = false;
			this._entityWrapperFactory = new EntityWrapperFactory();
			this.TransactionManager = new TransactionManager();
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000A288D File Offset: 0x000A0A8D
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x000A2895 File Offset: 0x000A0A95
		internal virtual object ChangingObject { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000A289E File Offset: 0x000A0A9E
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x000A28A6 File Offset: 0x000A0AA6
		internal virtual string ChangingEntityMember { get; set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000A28AF File Offset: 0x000A0AAF
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x000A28B7 File Offset: 0x000A0AB7
		internal virtual string ChangingMember { get; set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000A28C0 File Offset: 0x000A0AC0
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x000A28C8 File Offset: 0x000A0AC8
		internal virtual EntityState ChangingState { get; set; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000A28D1 File Offset: 0x000A0AD1
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000A28D9 File Offset: 0x000A0AD9
		internal virtual bool SaveOriginalValues { get; set; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000A28E2 File Offset: 0x000A0AE2
		// (set) Token: 0x060032FF RID: 13055 RVA: 0x000A28EA File Offset: 0x000A0AEA
		internal virtual object ChangingOldValue { get; set; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000A28F3 File Offset: 0x000A0AF3
		internal virtual bool InRelationshipFixup
		{
			get
			{
				return this._inRelationshipFixup;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000A28FB File Offset: 0x000A0AFB
		internal virtual ComplexTypeMaterializer ComplexTypeMaterializer
		{
			get
			{
				if (this._complexTypeMaterializer == null)
				{
					this._complexTypeMaterializer = new ComplexTypeMaterializer(this.MetadataWorkspace);
				}
				return this._complexTypeMaterializer;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x000A291C File Offset: 0x000A0B1C
		// (set) Token: 0x06003303 RID: 13059 RVA: 0x000A2924 File Offset: 0x000A0B24
		internal virtual TransactionManager TransactionManager { get; private set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000A292D File Offset: 0x000A0B2D
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000A2935 File Offset: 0x000A0B35
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadataWorkspace;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06003306 RID: 13062 RVA: 0x000A293D File Offset: 0x000A0B3D
		// (remove) Token: 0x06003307 RID: 13063 RVA: 0x000A2956 File Offset: 0x000A0B56
		public event CollectionChangeEventHandler ObjectStateManagerChanged
		{
			add
			{
				this.onObjectStateManagerChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onObjectStateManagerChangedDelegate, value);
			}
			remove
			{
				this.onObjectStateManagerChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onObjectStateManagerChangedDelegate, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06003308 RID: 13064 RVA: 0x000A296F File Offset: 0x000A0B6F
		// (remove) Token: 0x06003309 RID: 13065 RVA: 0x000A2988 File Offset: 0x000A0B88
		internal event CollectionChangeEventHandler EntityDeleted
		{
			add
			{
				this.onEntityDeletedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onEntityDeletedDelegate, value);
			}
			remove
			{
				this.onEntityDeletedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onEntityDeletedDelegate, value);
			}
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000A29A1 File Offset: 0x000A0BA1
		internal virtual void OnObjectStateManagerChanged(CollectionChangeAction action, object entity)
		{
			if (this.onObjectStateManagerChangedDelegate != null)
			{
				this.onObjectStateManagerChangedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000A29BE File Offset: 0x000A0BBE
		private void OnEntityDeleted(CollectionChangeAction action, object entity)
		{
			if (this.onEntityDeletedDelegate != null)
			{
				this.onEntityDeletedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000A29DB File Offset: 0x000A0BDB
		internal virtual EntityEntry AddKeyEntry(EntityKey entityKey, EntitySet entitySet)
		{
			if (this.FindEntityEntry(entityKey) != null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(entitySet.ElementType.Name));
			}
			return this.InternalAddEntityEntry(entityKey, entitySet);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000A2A04 File Offset: 0x000A0C04
		internal EntityEntry GetOrAddKeyEntry(EntityKey entityKey, EntitySet entitySet)
		{
			EntityEntry entityEntry;
			if (this.TryGetEntityEntry(entityKey, out entityEntry))
			{
				return entityEntry;
			}
			return this.InternalAddEntityEntry(entityKey, entitySet);
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000A2A28 File Offset: 0x000A0C28
		private EntityEntry InternalAddEntityEntry(EntityKey entityKey, EntitySet entitySet)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(entitySet.ElementType);
			EntityEntry entityEntry = new EntityEntry(entityKey, entitySet, this, orAddStateManagerTypeMetadata);
			this.AddEntityEntryToDictionary(entityEntry, entityEntry.State);
			return entityEntry;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000A2A5C File Offset: 0x000A0C5C
		private void ValidateProxyType(IEntityWrapper wrappedEntity)
		{
			Type identityType = wrappedEntity.IdentityType;
			Type type = wrappedEntity.Entity.GetType();
			if (identityType != type)
			{
				EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(this.MetadataWorkspace.GetItem<ClrEntityType>(identityType.FullNameWithNesting(), DataSpace.OSpace), this.MetadataWorkspace);
				if (proxyType == null || proxyType.ProxyType != type)
				{
					throw new InvalidOperationException(Strings.EntityProxyTypeInfo_DuplicateOSpaceType(identityType.FullName));
				}
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000A2AC8 File Offset: 0x000A0CC8
		internal virtual EntityEntry AddEntry(IEntityWrapper wrappedObject, EntityKey passedKey, EntitySet entitySet, string argumentName, bool isAdded)
		{
			EntityKey entityKey = passedKey;
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedObject.IdentityType, entitySet);
			this.ValidateProxyType(wrappedObject);
			EdmType edmType = orAddStateManagerTypeMetadata.CdmMetadata.EdmType;
			if (isAdded && !entitySet.ElementType.IsAssignableFrom(edmType))
			{
				throw new ArgumentException(Strings.ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(wrappedObject.Entity.GetType().Name, TypeHelpers.GetFullName(entitySet.EntityContainer.Name, entitySet.Name)), argumentName);
			}
			EntityKey entityKey2;
			if (isAdded)
			{
				entityKey2 = wrappedObject.GetEntityKeyFromEntity();
			}
			else
			{
				entityKey2 = wrappedObject.EntityKey;
			}
			if (entityKey2 != null)
			{
				entityKey = entityKey2;
				if (entityKey == null)
				{
					throw new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
				}
				if (wrappedObject.EntityKey != entityKey)
				{
					throw new InvalidOperationException(Strings.EntityKey_DoesntMatchKeyOnEntity(wrappedObject.Entity.GetType().FullName));
				}
			}
			if (entityKey != null && !entityKey.IsTemporary && !isAdded)
			{
				this.CheckKeyMatchesEntity(wrappedObject, entityKey, entitySet, false);
			}
			EntityEntry entityEntry;
			if (!isAdded || ((!(entityKey2 == null) || (entityEntry = this.FindEntityEntry(wrappedObject.Entity)) == null) && (!(entityKey2 != null) || (entityEntry = this.FindEntityEntry(entityKey2)) == null)))
			{
				if (entityKey == null || (isAdded && !entityKey.IsTemporary))
				{
					entityKey = new EntityKey(entitySet);
					wrappedObject.EntityKey = entityKey;
				}
				if (!wrappedObject.OwnsRelationshipManager)
				{
					wrappedObject.RelationshipManager.ClearRelatedEndWrappers();
				}
				EntityEntry entityEntry2 = new EntityEntry(wrappedObject, entityKey, entitySet, this, orAddStateManagerTypeMetadata, isAdded ? EntityState.Added : EntityState.Unchanged);
				entityEntry2.AttachObjectStateManagerToEntity();
				this.AddEntityEntryToDictionary(entityEntry2, entityEntry2.State);
				this.OnObjectStateManagerChanged(CollectionChangeAction.Add, entityEntry2.Entity);
				if (!isAdded)
				{
					this.FixupReferencesByForeignKeys(entityEntry2, false);
				}
				return entityEntry2;
			}
			if (entityEntry.Entity != wrappedObject.Entity)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedObject.IdentityType.FullName));
			}
			if (entityEntry.State != EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(entityEntry.State));
			}
			return null;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000A2C9C File Offset: 0x000A0E9C
		internal virtual void FixupReferencesByForeignKeys(EntityEntry newEntry, bool replaceAddedRefs = false)
		{
			if (!((EntitySet)newEntry.EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			newEntry.FixupReferencesByForeignKeys(replaceAddedRefs, null);
			foreach (EntityEntry entityEntry in this.GetNonFixedupEntriesContainingForeignKey(newEntry.EntityKey))
			{
				entityEntry.FixupReferencesByForeignKeys(false, newEntry.EntitySet);
			}
			this.RemoveForeignKeyFromIndex(newEntry.EntityKey);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000A2D1C File Offset: 0x000A0F1C
		internal virtual void AddEntryContainingForeignKeyToIndex(EntityReference relatedEnd, EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> hashSet;
			if (!this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet = new HashSet<Tuple<EntityReference, EntityEntry>>();
				this._danglingForeignKeys.Add(foreignKey, hashSet);
			}
			hashSet.Add(Tuple.Create<EntityReference, EntityEntry>(relatedEnd, entry));
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000A2D5C File Offset: 0x000A0F5C
		[Conditional("DEBUG")]
		internal virtual void AssertEntryDoesNotExistInForeignKeyIndex(EntityEntry entry)
		{
			using (IEnumerator<Tuple<EntityReference, EntityEntry>> enumerator = this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> kv) => kv.Value).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Item2.State != EntityState.Detached)
					{
						EntityState state = entry.State;
					}
				}
			}
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000A2DDC File Offset: 0x000A0FDC
		[Conditional("DEBUG")]
		internal virtual void AssertAllForeignKeyIndexEntriesAreValid()
		{
			if (this.GetMaxEntityEntriesForDetectChanges() > 100)
			{
				return;
			}
			new HashSet<ObjectStateEntry>(this.GetObjectStateEntriesInternal(~EntityState.Detached));
			foreach (Tuple<EntityReference, EntityEntry> tuple in this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> kv) => kv.Value))
			{
			}
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000A2E60 File Offset: 0x000A1060
		internal virtual void RemoveEntryFromForeignKeyIndex(EntityReference relatedEnd, EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> hashSet;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet.Remove(Tuple.Create<EntityReference, EntityEntry>(relatedEnd, entry));
			}
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000A2E8B File Offset: 0x000A108B
		internal virtual void RemoveForeignKeyFromIndex(EntityKey foreignKey)
		{
			this._danglingForeignKeys.Remove(foreignKey);
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000A2E9C File Offset: 0x000A109C
		internal virtual IEnumerable<EntityEntry> GetNonFixedupEntriesContainingForeignKey(EntityKey foreignKey)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> hashSet;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				return hashSet.Select((Tuple<EntityReference, EntityEntry> e) => e.Item2).ToList<EntityEntry>();
			}
			return Enumerable.Empty<EntityEntry>();
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000A2EE9 File Offset: 0x000A10E9
		internal virtual void RememberEntryWithConceptualNull(EntityEntry entry)
		{
			if (this._entriesWithConceptualNulls == null)
			{
				this._entriesWithConceptualNulls = new HashSet<EntityEntry>();
			}
			this._entriesWithConceptualNulls.Add(entry);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000A2F0B File Offset: 0x000A110B
		internal virtual bool SomeEntryWithConceptualNullExists()
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Count != 0;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000A2F25 File Offset: 0x000A1125
		internal virtual bool EntryHasConceptualNull(EntityEntry entry)
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Contains(entry);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000A2F40 File Offset: 0x000A1140
		internal virtual void ForgetEntryWithConceptualNull(EntityEntry entry, bool resetAllKeys)
		{
			if (!entry.IsKeyEntry && this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Remove(entry) && entry.RelationshipManager.HasRelationships)
			{
				foreach (RelatedEnd relatedEnd in entry.RelationshipManager.Relationships)
				{
					EntityReference entityReference = relatedEnd as EntityReference;
					if (entityReference != null && ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						if (!resetAllKeys)
						{
							this._entriesWithConceptualNulls.Add(entry);
							break;
						}
						entityReference.SetCachedForeignKey(null, null);
					}
				}
			}
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000A2FEC File Offset: 0x000A11EC
		internal virtual void PromoteKeyEntryInitialization(ObjectContext contextToAttach, EntityEntry keyEntry, IEntityWrapper wrappedEntity, bool replacingEntry)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, (EntitySet)keyEntry.EntitySet);
			this.ValidateProxyType(wrappedEntity);
			keyEntry.PromoteKeyEntry(wrappedEntity, orAddStateManagerTypeMetadata);
			this.AddEntryToKeylessStore(keyEntry);
			if (replacingEntry)
			{
				wrappedEntity.SetChangeTracker(null);
			}
			wrappedEntity.SetChangeTracker(keyEntry);
			if (contextToAttach != null)
			{
				wrappedEntity.AttachContext(contextToAttach, (EntitySet)keyEntry.EntitySet, MergeOption.AppendOnly);
			}
			wrappedEntity.TakeSnapshot(keyEntry);
			this.OnObjectStateManagerChanged(CollectionChangeAction.Add, keyEntry.Entity);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000A3064 File Offset: 0x000A1264
		internal virtual void PromoteKeyEntry(EntityEntry keyEntry, IEntityWrapper wrappedEntity, bool replacingEntry, bool setIsLoaded, bool keyEntryInitialized)
		{
			if (!keyEntryInitialized)
			{
				this.PromoteKeyEntryInitialization(null, keyEntry, wrappedEntity, replacingEntry);
			}
			bool flag = true;
			try
			{
				foreach (RelationshipEntry relationshipEntry in this.CopyOfRelationshipsByKey(keyEntry.EntityKey))
				{
					if (relationshipEntry.State != EntityState.Deleted)
					{
						AssociationEndMember associationEndMember = keyEntry.GetAssociationEndMember(relationshipEntry);
						AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(associationEndMember);
						EntityEntry otherEndOfRelationship = keyEntry.GetOtherEndOfRelationship(relationshipEntry);
						ObjectStateManager.AddEntityToCollectionOrReference(MergeOption.AppendOnly, wrappedEntity, associationEndMember, otherEndOfRelationship.WrappedEntity, otherAssociationEnd, setIsLoaded, true, true);
					}
				}
				this.FixupReferencesByForeignKeys(keyEntry, false);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					keyEntry.DetachObjectStateManagerFromEntity();
					this.RemoveEntryFromKeylessStore(wrappedEntity);
					keyEntry.DegradeEntry();
				}
			}
			if (this.TransactionManager.IsAttachTracking)
			{
				this.TransactionManager.PromotedKeyEntries.Add(wrappedEntity.Entity, keyEntry);
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000A3134 File Offset: 0x000A1334
		internal virtual void TrackPromotedRelationship(RelatedEnd relatedEnd, IEntityWrapper wrappedEntity)
		{
			IList<IEntityWrapper> list;
			if (!this.TransactionManager.PromotedRelationships.TryGetValue(relatedEnd, out list))
			{
				list = new List<IEntityWrapper>();
				this.TransactionManager.PromotedRelationships.Add(relatedEnd, list);
			}
			list.Add(wrappedEntity);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000A3178 File Offset: 0x000A1378
		internal virtual void DegradePromotedRelationships()
		{
			foreach (KeyValuePair<RelatedEnd, IList<IEntityWrapper>> keyValuePair in this.TransactionManager.PromotedRelationships)
			{
				foreach (IEntityWrapper entityWrapper in keyValuePair.Value)
				{
					if (keyValuePair.Key.RemoveFromCache(entityWrapper, false, false))
					{
						keyValuePair.Key.OnAssociationChanged(CollectionChangeAction.Remove, entityWrapper.Entity);
					}
				}
			}
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000A3224 File Offset: 0x000A1424
		internal static void AddEntityToCollectionOrReference(MergeOption mergeOption, IEntityWrapper wrappedSource, AssociationEndMember sourceMember, IEntityWrapper wrappedTarget, AssociationEndMember targetMember, bool setIsLoaded, bool relationshipAlreadyExists, bool inKeyEntryPromotion)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			if (targetMember.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				EntityReference entityReference = (EntityReference)relatedEndInternal;
				switch (mergeOption)
				{
				case MergeOption.AppendOnly:
					if (inKeyEntryPromotion && !entityReference.IsEmpty() && entityReference.ReferenceValue.Entity != wrappedTarget.Entity)
					{
						throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
					}
					break;
				case MergeOption.OverwriteChanges:
				case MergeOption.PreserveChanges:
				{
					IEntityWrapper referenceValue = entityReference.ReferenceValue;
					if (referenceValue != null && referenceValue.Entity != null && referenceValue != wrappedTarget)
					{
						RelationshipEntry relationshipEntry = relatedEndInternal.FindRelationshipEntryInObjectStateManager(referenceValue);
						relatedEndInternal.RemoveAll();
						if (relationshipEntry != null && relationshipEntry.State == EntityState.Deleted)
						{
							relationshipEntry.AcceptChanges();
						}
					}
					break;
				}
				}
			}
			RelatedEnd relatedEnd = null;
			if (mergeOption == MergeOption.NoTracking)
			{
				relatedEnd = relatedEndInternal.GetOtherEndOfRelationship(wrappedTarget);
				if (relatedEnd.IsLoaded)
				{
					throw new InvalidOperationException(Strings.Collections_CannotFillTryDifferentMergeOption(relatedEnd.SourceRoleName, relatedEnd.RelationshipName));
				}
			}
			if (relatedEnd == null)
			{
				relatedEnd = relatedEndInternal.GetOtherEndOfRelationship(wrappedTarget);
			}
			relatedEndInternal.Add(wrappedTarget, true, true, relationshipAlreadyExists, true, true);
			ObjectStateManager.UpdateRelatedEnd(relatedEndInternal, wrappedTarget, setIsLoaded, mergeOption);
			ObjectStateManager.UpdateRelatedEnd(relatedEnd, wrappedSource, setIsLoaded, mergeOption);
			if (inKeyEntryPromotion && wrappedSource.Context.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEndInternal, wrappedTarget);
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEnd, wrappedSource);
			}
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000A3378 File Offset: 0x000A1578
		private static void UpdateRelatedEnd(RelatedEnd relatedEnd, IEntityWrapper wrappedRelatedEntity, bool setIsLoaded, MergeOption mergeOption)
		{
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEnd.ToEndMember;
			if (associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One || associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				if (setIsLoaded)
				{
					relatedEnd.IsLoaded = true;
				}
				if (mergeOption == MergeOption.NoTracking)
				{
					EntityKey entityKey = wrappedRelatedEntity.EntityKey;
					if (entityKey == null)
					{
						throw new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
					}
					((EntityReference)relatedEnd).DetachedEntityKey = entityKey;
				}
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000A33D4 File Offset: 0x000A15D4
		internal virtual int UpdateRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, AssociationEndMember sourceMember, IEntityWrapper wrappedSource, AssociationEndMember targetMember, IList targets, bool setIsLoaded)
		{
			int num = 0;
			EntityKey sourceKey = wrappedSource.EntityKey;
			context.ObjectStateManager.TransactionManager.BeginGraphUpdate();
			try
			{
				if (targets != null)
				{
					if (mergeOption == MergeOption.NoTracking)
					{
						RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
						if (!relatedEndInternal.IsEmpty())
						{
							throw new InvalidOperationException(Strings.Collections_CannotFillTryDifferentMergeOption(relatedEndInternal.SourceRoleName, relatedEndInternal.RelationshipName));
						}
					}
					Lazy<ILookup<EntityKey, RelationshipEntry>> lazy = new Lazy<ILookup<EntityKey, RelationshipEntry>>(() => ObjectStateManager.GetRelationshipLookup(context.ObjectStateManager, associationSet, sourceMember, sourceKey));
					foreach (object obj in targets)
					{
						IEntityWrapper entityWrapper = obj as IEntityWrapper;
						if (entityWrapper == null)
						{
							entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(obj, context);
						}
						num++;
						if (mergeOption == MergeOption.NoTracking)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(MergeOption.NoTracking, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, true, false);
						}
						else
						{
							ObjectStateManager objectStateManager = context.ObjectStateManager;
							EntityKey entityKey = entityWrapper.EntityKey;
							EntityState entityState;
							if (!ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, sourceMember, lazy.Value, wrappedSource, targetMember, entityKey, setIsLoaded, out entityState))
							{
								bool flag = true;
								RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
								if (relationshipMultiplicity > RelationshipMultiplicity.One)
								{
									if (relationshipMultiplicity != RelationshipMultiplicity.Many)
									{
									}
								}
								else
								{
									ILookup<EntityKey, RelationshipEntry> relationshipLookup = ObjectStateManager.GetRelationshipLookup(context.ObjectStateManager, associationSet, targetMember, entityKey);
									flag = !ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, targetMember, relationshipLookup, entityWrapper, sourceMember, sourceKey, setIsLoaded, out entityState);
								}
								if (flag)
								{
									if (entityState != EntityState.Deleted)
									{
										ObjectStateManager.AddEntityToCollectionOrReference(mergeOption, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, false, false);
									}
									else
									{
										RelationshipWrapper relationshipWrapper = new RelationshipWrapper(associationSet, sourceMember.Name, sourceKey, targetMember.Name, entityKey);
										objectStateManager.AddNewRelation(relationshipWrapper, EntityState.Deleted);
									}
								}
							}
						}
					}
				}
				if (num == 0)
				{
					ObjectStateManager.EnsureCollectionNotNull(sourceMember, wrappedSource, targetMember);
				}
			}
			finally
			{
				context.ObjectStateManager.TransactionManager.EndGraphUpdate();
			}
			return num;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000A365C File Offset: 0x000A185C
		internal static ILookup<EntityKey, RelationshipEntry> GetRelationshipLookup(ObjectStateManager manager, AssociationSet associationSet, AssociationEndMember sourceMember, EntityKey sourceKey)
		{
			List<RelationshipEntry> list = new List<RelationshipEntry>();
			foreach (RelationshipEntry relationshipEntry in manager.FindRelationshipsByKey(sourceKey))
			{
				if (relationshipEntry.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey))
				{
					list.Add(relationshipEntry);
				}
			}
			return list.ToLookup((RelationshipEntry r) => r.RelationshipWrapper.GetOtherEntityKey(sourceKey));
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000A36F0 File Offset: 0x000A18F0
		private static void EnsureCollectionNotNull(AssociationEndMember sourceMember, IEntityWrapper wrappedSource, AssociationEndMember targetMember)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEndInternal.ToEndMember;
			if (associationEndMember != null && associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many && relatedEndInternal.TargetAccessor.HasProperty)
			{
				wrappedSource.EnsureCollectionNotNull(relatedEndInternal);
			}
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000A3748 File Offset: 0x000A1948
		internal virtual void RemoveRelationships(MergeOption mergeOption, AssociationSet associationSet, EntityKey sourceKey, AssociationEndMember sourceMember)
		{
			List<RelationshipEntry> list = new List<RelationshipEntry>(16);
			if (mergeOption == MergeOption.OverwriteChanges)
			{
				using (EntityEntry.RelationshipEndEnumerator relationshipEndEnumerator = this.FindRelationshipsByKey(sourceKey).GetEnumerator())
				{
					while (relationshipEndEnumerator.MoveNext())
					{
						RelationshipEntry relationshipEntry = relationshipEndEnumerator.Current;
						if (relationshipEntry.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey))
						{
							list.Add(relationshipEntry);
						}
					}
					goto IL_00A9;
				}
			}
			if (mergeOption == MergeOption.PreserveChanges)
			{
				foreach (RelationshipEntry relationshipEntry2 in this.FindRelationshipsByKey(sourceKey))
				{
					if (relationshipEntry2.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey) && relationshipEntry2.State != EntityState.Added)
					{
						list.Add(relationshipEntry2);
					}
				}
			}
			IL_00A9:
			foreach (RelationshipEntry relationshipEntry3 in list)
			{
				ObjectStateManager.RemoveRelatedEndsAndDetachRelationship(relationshipEntry3, true);
			}
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000A3858 File Offset: 0x000A1A58
		internal static bool TryUpdateExistingRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, AssociationEndMember sourceMember, ILookup<EntityKey, RelationshipEntry> relationshipLookup, IEntityWrapper wrappedSource, AssociationEndMember targetMember, EntityKey targetKey, bool setIsLoaded, out EntityState newEntryState)
		{
			newEntryState = EntityState.Unchanged;
			if (associationSet.ElementType.IsForeignKey)
			{
				return true;
			}
			bool flag = true;
			ObjectStateManager objectStateManager = context.ObjectStateManager;
			List<RelationshipEntry> list = null;
			List<RelationshipEntry> list2 = null;
			foreach (RelationshipEntry relationshipEntry in relationshipLookup[targetKey])
			{
				if (list2 == null)
				{
					list2 = new List<RelationshipEntry>(16);
				}
				list2.Add(relationshipEntry);
			}
			RelationshipMultiplicity relationshipMultiplicity = targetMember.RelationshipMultiplicity;
			if (relationshipMultiplicity > RelationshipMultiplicity.One)
			{
				if (relationshipMultiplicity != RelationshipMultiplicity.Many)
				{
				}
			}
			else
			{
				Func<IGrouping<EntityKey, RelationshipEntry>, bool> <>9__0;
				Func<IGrouping<EntityKey, RelationshipEntry>, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IGrouping<EntityKey, RelationshipEntry> g) => g.Key != targetKey);
				}
				foreach (RelationshipEntry relationshipEntry2 in relationshipLookup.Where(func).SelectMany((IGrouping<EntityKey, RelationshipEntry> re) => re))
				{
					switch (mergeOption)
					{
					case MergeOption.AppendOnly:
						if (relationshipEntry2.State != EntityState.Deleted)
						{
							flag = false;
						}
						break;
					case MergeOption.OverwriteChanges:
						if (list == null)
						{
							list = new List<RelationshipEntry>(16);
						}
						list.Add(relationshipEntry2);
						break;
					case MergeOption.PreserveChanges:
					{
						EntityState state = relationshipEntry2.State;
						if (state != EntityState.Unchanged)
						{
							if (state != EntityState.Added)
							{
								if (state == EntityState.Deleted)
								{
									newEntryState = EntityState.Deleted;
									if (list == null)
									{
										list = new List<RelationshipEntry>(16);
									}
									list.Add(relationshipEntry2);
								}
							}
							else
							{
								newEntryState = EntityState.Deleted;
							}
						}
						else
						{
							if (list == null)
							{
								list = new List<RelationshipEntry>(16);
							}
							list.Add(relationshipEntry2);
						}
						break;
					}
					}
				}
			}
			if (list != null)
			{
				foreach (RelationshipEntry relationshipEntry3 in list)
				{
					if (relationshipEntry3.State != EntityState.Detached)
					{
						ObjectStateManager.RemoveRelatedEndsAndDetachRelationship(relationshipEntry3, setIsLoaded);
					}
				}
			}
			if (list2 != null)
			{
				foreach (RelationshipEntry relationshipEntry4 in list2)
				{
					flag = false;
					switch (mergeOption)
					{
					case MergeOption.OverwriteChanges:
						if (relationshipEntry4.State == EntityState.Added)
						{
							relationshipEntry4.AcceptChanges();
						}
						else if (relationshipEntry4.State == EntityState.Deleted)
						{
							EntityEntry entityEntry = objectStateManager.GetEntityEntry(targetKey);
							if (entityEntry.State != EntityState.Deleted)
							{
								if (!entityEntry.IsKeyEntry)
								{
									ObjectStateManager.AddEntityToCollectionOrReference(mergeOption, wrappedSource, sourceMember, entityEntry.WrappedEntity, targetMember, setIsLoaded, true, false);
								}
								relationshipEntry4.RevertDelete();
							}
						}
						break;
					case MergeOption.PreserveChanges:
						if (relationshipEntry4.State == EntityState.Added)
						{
							relationshipEntry4.AcceptChanges();
						}
						break;
					}
				}
			}
			return !flag;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000A3B34 File Offset: 0x000A1D34
		internal static void RemoveRelatedEndsAndDetachRelationship(RelationshipEntry relationshipToRemove, bool setIsLoaded)
		{
			if (setIsLoaded)
			{
				ObjectStateManager.UnloadReferenceRelatedEnds(relationshipToRemove);
			}
			if (relationshipToRemove.State != EntityState.Deleted)
			{
				relationshipToRemove.Delete();
			}
			if (relationshipToRemove.State != EntityState.Detached)
			{
				relationshipToRemove.AcceptChanges();
			}
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000A3B60 File Offset: 0x000A1D60
		private static void UnloadReferenceRelatedEnds(RelationshipEntry relationshipEntry)
		{
			ObjectStateManager objectStateManager = relationshipEntry.ObjectStateManager;
			ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = relationshipEntry.RelationshipWrapper.AssociationEndMembers;
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(0), associationEndMembers[1].Name);
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(1), associationEndMembers[0].Name);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000A3BBC File Offset: 0x000A1DBC
		private static void UnloadReferenceRelatedEnds(ObjectStateManager cache, RelationshipEntry relationshipEntry, EntityKey sourceEntityKey, string targetRoleName)
		{
			EntityEntry entityEntry = cache.GetEntityEntry(sourceEntityKey);
			if (entityEntry.WrappedEntity.Entity != null)
			{
				EntityReference entityReference = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(((AssociationSet)relationshipEntry.EntitySet).ElementType.FullName, targetRoleName) as EntityReference;
				if (entityReference != null)
				{
					entityReference.IsLoaded = false;
				}
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000A3C14 File Offset: 0x000A1E14
		internal virtual EntityEntry AttachEntry(EntityKey entityKey, IEntityWrapper wrappedObject, EntitySet entitySet)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedObject.IdentityType, entitySet);
			this.ValidateProxyType(wrappedObject);
			this.CheckKeyMatchesEntity(wrappedObject, entityKey, entitySet, true);
			if (!wrappedObject.OwnsRelationshipManager)
			{
				wrappedObject.RelationshipManager.ClearRelatedEndWrappers();
			}
			EntityEntry entityEntry = new EntityEntry(wrappedObject, entityKey, entitySet, this, orAddStateManagerTypeMetadata, EntityState.Unchanged);
			entityEntry.AttachObjectStateManagerToEntity();
			this.AddEntityEntryToDictionary(entityEntry, entityEntry.State);
			this.OnObjectStateManagerChanged(CollectionChangeAction.Add, entityEntry.Entity);
			return entityEntry;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000A3C80 File Offset: 0x000A1E80
		private void CheckKeyMatchesEntity(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySetForType, bool forAttach)
		{
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			if (entitySet == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_InvalidKey);
			}
			entityKey.ValidateEntityKey(this._metadataWorkspace, entitySet);
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, entitySetForType);
			for (int i = 0; i < entitySet.ElementType.KeyMembers.Count; i++)
			{
				EdmMember edmMember = entitySet.ElementType.KeyMembers[i];
				int ordinalforCLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(edmMember.Name);
				if (ordinalforCLayerMemberName < 0)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_InvalidKey);
				}
				object value = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(wrappedEntity.Entity);
				object obj = entityKey.FindValueByName(edmMember.Name);
				if (!ByValueEqualityComparer.Default.Equals(value, obj))
				{
					throw new InvalidOperationException(forAttach ? Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach : Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKey);
				}
			}
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000A3D5C File Offset: 0x000A1F5C
		internal virtual RelationshipEntry AddNewRelation(RelationshipWrapper wrapper, EntityState desiredState)
		{
			RelationshipEntry relationshipEntry = new RelationshipEntry(this, desiredState, wrapper);
			this.AddRelationshipEntryToDictionary(relationshipEntry, desiredState);
			this.AddRelationshipToLookup(relationshipEntry);
			return relationshipEntry;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000A3D84 File Offset: 0x000A1F84
		internal virtual RelationshipEntry AddRelation(RelationshipWrapper wrapper, EntityState desiredState)
		{
			RelationshipEntry relationshipEntry = this.FindRelationship(wrapper);
			if (relationshipEntry == null)
			{
				relationshipEntry = this.AddNewRelation(wrapper, desiredState);
			}
			else if (EntityState.Deleted != relationshipEntry.State)
			{
				if (EntityState.Unchanged == desiredState)
				{
					relationshipEntry.AcceptChanges();
				}
				else if (EntityState.Deleted == desiredState)
				{
					relationshipEntry.AcceptChanges();
					relationshipEntry.Delete(false);
				}
			}
			else if (EntityState.Deleted != desiredState)
			{
				relationshipEntry.RevertDelete();
			}
			return relationshipEntry;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000A3DDC File Offset: 0x000A1FDC
		private void AddRelationshipToLookup(RelationshipEntry relationship)
		{
			this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000A3E2A File Offset: 0x000A202A
		private void AddRelationshipEndToLookup(EntityKey key, RelationshipEntry relationship)
		{
			this.GetEntityEntry(key).AddRelationshipEnd(relationship);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000A3E3C File Offset: 0x000A203C
		private void DeleteRelationshipFromLookup(RelationshipEntry relationship)
		{
			this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000A3E8A File Offset: 0x000A208A
		private void DeleteRelationshipEndFromLookup(EntityKey key, RelationshipEntry relationship)
		{
			this.GetEntityEntry(key).RemoveRelationshipEnd(relationship);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000A3E99 File Offset: 0x000A2099
		internal virtual RelationshipEntry FindRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			if (roleAndKey1.Value == null || roleAndKey2.Value == null)
			{
				return null;
			}
			return this.FindRelationship(new RelationshipWrapper((AssociationSet)relationshipSet, roleAndKey1, roleAndKey2));
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000A3EC4 File Offset: 0x000A20C4
		internal virtual RelationshipEntry FindRelationship(RelationshipWrapper relationshipWrapper)
		{
			RelationshipEntry relationshipEntry = null;
			if ((this._unchangedRelationshipStore == null || !this._unchangedRelationshipStore.TryGetValue(relationshipWrapper, out relationshipEntry)) && (this._deletedRelationshipStore == null || !this._deletedRelationshipStore.TryGetValue(relationshipWrapper, out relationshipEntry)))
			{
				if (this._addedRelationshipStore != null)
				{
					this._addedRelationshipStore.TryGetValue(relationshipWrapper, out relationshipEntry);
				}
			}
			return relationshipEntry;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000A3F24 File Offset: 0x000A2124
		internal virtual RelationshipEntry DeleteRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			RelationshipEntry relationshipEntry = this.FindRelationship(relationshipSet, roleAndKey1, roleAndKey2);
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
			}
			return relationshipEntry;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000A3F46 File Offset: 0x000A2146
		internal virtual void DeleteKeyEntry(EntityEntry keyEntry)
		{
			if (keyEntry != null && keyEntry.IsKeyEntry)
			{
				this.ChangeState(keyEntry, keyEntry.State, EntityState.Detached);
			}
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000A3F64 File Offset: 0x000A2164
		internal virtual RelationshipEntry[] CopyOfRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key).ToArray();
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000A3F80 File Offset: 0x000A2180
		internal virtual EntityEntry.RelationshipEndEnumerable FindRelationshipsByKey(EntityKey key)
		{
			return new EntityEntry.RelationshipEndEnumerable(this.FindEntityEntry(key));
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000A3F8E File Offset: 0x000A218E
		IEnumerable<IEntityStateEntry> IEntityStateManager.FindRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000A3F9C File Offset: 0x000A219C
		[Conditional("DEBUG")]
		private void ValidateKeylessEntityStore()
		{
			Dictionary<EntityKey, EntityEntry>[] array = new Dictionary<EntityKey, EntityEntry>[] { this._unchangedEntityStore, this._modifiedEntityStore, this._addedEntityStore, this._deletedEntityStore };
			if (this._keylessEntityStore != null)
			{
				if (this._keylessEntityStore.Count == array.Sum(delegate(Dictionary<EntityKey, EntityEntry> s)
				{
					if (s != null)
					{
						return s.Count;
					}
					return 0;
				}))
				{
					return;
				}
			}
			if (this._keylessEntityStore != null)
			{
				foreach (EntityEntry entityEntry in this._keylessEntityStore.Values)
				{
					bool flag = false;
					if (this._addedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag = this._addedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._modifiedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._modifiedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._deletedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._deletedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._unchangedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._unchangedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
				}
			}
			foreach (Dictionary<EntityKey, EntityEntry> dictionary in array)
			{
				if (dictionary != null)
				{
					foreach (EntityEntry entityEntry3 in dictionary.Values)
					{
						if (entityEntry3.Entity != null && !(entityEntry3.Entity is IEntityWithKey))
						{
							EntityEntry entityEntry4;
							this._keylessEntityStore.TryGetValue(entityEntry3.Entity, out entityEntry4);
						}
					}
				}
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000A4170 File Offset: 0x000A2370
		private bool TryGetEntryFromKeylessStore(object entity, out EntityEntry entryRef)
		{
			entryRef = null;
			if (entity == null)
			{
				return false;
			}
			if (this._keylessEntityStore != null && this._keylessEntityStore.TryGetValue(entity, out entryRef))
			{
				return true;
			}
			entryRef = null;
			return false;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000A4197 File Offset: 0x000A2397
		public virtual IEnumerable<ObjectStateEntry> GetObjectStateEntries(EntityState state)
		{
			if ((EntityState.Detached & state) != (EntityState)0)
			{
				throw new ArgumentException(Strings.ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager);
			}
			return this.GetObjectStateEntriesInternal(state);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000A41B0 File Offset: 0x000A23B0
		IEnumerable<IEntityStateEntry> IEntityStateManager.GetEntityStateEntries(EntityState state)
		{
			foreach (ObjectStateEntry objectStateEntry in this.GetObjectStateEntriesInternal(state))
			{
				yield return objectStateEntry;
			}
			IEnumerator<ObjectStateEntry> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000A41C8 File Offset: 0x000A23C8
		internal virtual bool HasChanges()
		{
			return (this._addedRelationshipStore != null && this._addedRelationshipStore.Count > 0) || (this._addedEntityStore != null && this._addedEntityStore.Count > 0) || (this._modifiedEntityStore != null && this._modifiedEntityStore.Count > 0) || (this._deletedRelationshipStore != null && this._deletedRelationshipStore.Count > 0) || (this._deletedEntityStore != null && this._deletedEntityStore.Count > 0);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000A4248 File Offset: 0x000A2448
		internal virtual int GetObjectStateEntriesCount(EntityState state)
		{
			int num = 0;
			if ((EntityState.Added & state) != (EntityState)0)
			{
				num += ((this._addedRelationshipStore != null) ? this._addedRelationshipStore.Count : 0);
				num += ((this._addedEntityStore != null) ? this._addedEntityStore.Count : 0);
			}
			if ((EntityState.Modified & state) != (EntityState)0)
			{
				num += ((this._modifiedEntityStore != null) ? this._modifiedEntityStore.Count : 0);
			}
			if ((EntityState.Deleted & state) != (EntityState)0)
			{
				num += ((this._deletedRelationshipStore != null) ? this._deletedRelationshipStore.Count : 0);
				num += ((this._deletedEntityStore != null) ? this._deletedEntityStore.Count : 0);
			}
			if ((EntityState.Unchanged & state) != (EntityState)0)
			{
				num += ((this._unchangedRelationshipStore != null) ? this._unchangedRelationshipStore.Count : 0);
				num += ((this._unchangedEntityStore != null) ? this._unchangedEntityStore.Count : 0);
			}
			return num;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000A431C File Offset: 0x000A251C
		private int GetMaxEntityEntriesForDetectChanges()
		{
			int num = 0;
			if (this._addedEntityStore != null)
			{
				num += this._addedEntityStore.Count;
			}
			if (this._modifiedEntityStore != null)
			{
				num += this._modifiedEntityStore.Count;
			}
			if (this._deletedEntityStore != null)
			{
				num += this._deletedEntityStore.Count;
			}
			if (this._unchangedEntityStore != null)
			{
				num += this._unchangedEntityStore.Count;
			}
			return num;
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000A4384 File Offset: 0x000A2584
		internal virtual IEnumerable<ObjectStateEntry> GetObjectStateEntriesInternal(EntityState state)
		{
			int num = this.GetObjectStateEntriesCount(state);
			ObjectStateEntry[] array = new ObjectStateEntry[num];
			num = 0;
			if ((EntityState.Added & state) != (EntityState)0 && this._addedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair in this._addedRelationshipStore)
				{
					array[num++] = keyValuePair.Value;
				}
			}
			if ((EntityState.Deleted & state) != (EntityState)0 && this._deletedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair2 in this._deletedRelationshipStore)
				{
					array[num++] = keyValuePair2.Value;
				}
			}
			if ((EntityState.Unchanged & state) != (EntityState)0 && this._unchangedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair3 in this._unchangedRelationshipStore)
				{
					array[num++] = keyValuePair3.Value;
				}
			}
			if ((EntityState.Added & state) != (EntityState)0 && this._addedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair4 in this._addedEntityStore)
				{
					array[num++] = keyValuePair4.Value;
				}
			}
			if ((EntityState.Modified & state) != (EntityState)0 && this._modifiedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair5 in this._modifiedEntityStore)
				{
					array[num++] = keyValuePair5.Value;
				}
			}
			if ((EntityState.Deleted & state) != (EntityState)0 && this._deletedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair6 in this._deletedEntityStore)
				{
					array[num++] = keyValuePair6.Value;
				}
			}
			if ((EntityState.Unchanged & state) != (EntityState)0 && this._unchangedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair7 in this._unchangedEntityStore)
				{
					array[num++] = keyValuePair7.Value;
				}
			}
			return array;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000A460C File Offset: 0x000A280C
		private IList<EntityEntry> GetEntityEntriesForDetectChanges()
		{
			if (!this._detectChangesNeeded)
			{
				return null;
			}
			List<EntityEntry> list = null;
			this.GetEntityEntriesForDetectChanges(this._addedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._modifiedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._deletedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._unchangedEntityStore, ref list);
			if (list == null)
			{
				this._detectChangesNeeded = false;
			}
			return list;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000A4668 File Offset: 0x000A2868
		private void GetEntityEntriesForDetectChanges(Dictionary<EntityKey, EntityEntry> entityStore, ref List<EntityEntry> entries)
		{
			if (entityStore != null)
			{
				foreach (EntityEntry entityEntry in entityStore.Values)
				{
					if (entityEntry.RequiresAnyChangeTracking)
					{
						if (entries == null)
						{
							entries = new List<EntityEntry>(this.GetMaxEntityEntriesForDetectChanges());
						}
						entries.Add(entityEntry);
					}
				}
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000A46D8 File Offset: 0x000A28D8
		internal virtual void FixupKey(EntityEntry entry)
		{
			EntityKey entityKey = entry.EntityKey;
			EntitySet entitySet = (EntitySet)entry.EntitySet;
			bool hasForeignKeyRelationships = entitySet.HasForeignKeyRelationships;
			bool hasIndependentRelationships = entitySet.HasIndependentRelationships;
			if (hasForeignKeyRelationships)
			{
				entry.FixupForeignKeysByReference();
			}
			EntityKey entityKey2;
			try
			{
				entityKey2 = new EntityKey((EntitySet)entry.EntitySet, entry.CurrentValues);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException(Strings.ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid, ex);
			}
			EntityEntry entityEntry = this.FindEntityEntry(entityKey2);
			if (entityEntry != null)
			{
				if (!entityEntry.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_CannotFixUpKeyToExistingValues(entry.WrappedEntity.IdentityType.FullName));
				}
				entityKey2 = entityEntry.EntityKey;
			}
			RelationshipEntry[] array = null;
			if (hasIndependentRelationships)
			{
				array = entry.GetRelationshipEnds().ToArray();
				foreach (RelationshipEntry relationshipEntry in array)
				{
					this.RemoveObjectStateEntryFromDictionary(relationshipEntry, relationshipEntry.State);
				}
			}
			this.RemoveObjectStateEntryFromDictionary(entry, EntityState.Added);
			this.ResetEntityKey(entry, entityKey2);
			if (hasIndependentRelationships)
			{
				entry.UpdateRelationshipEnds(entityKey, entityEntry);
				foreach (RelationshipEntry relationshipEntry2 in array)
				{
					this.AddRelationshipEntryToDictionary(relationshipEntry2, relationshipEntry2.State);
				}
			}
			if (entityEntry != null)
			{
				this.PromoteKeyEntry(entityEntry, entry.WrappedEntity, true, false, false);
				entry = entityEntry;
			}
			else
			{
				this.AddEntityEntryToDictionary(entry, EntityState.Unchanged);
			}
			if (hasForeignKeyRelationships)
			{
				this.FixupReferencesByForeignKeys(entry, false);
			}
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000A483C File Offset: 0x000A2A3C
		internal virtual void ReplaceKeyWithTemporaryKey(EntityEntry entry)
		{
			EntityKey entityKey = entry.EntityKey;
			EntityKey entityKey2 = new EntityKey(entry.EntitySet);
			RelationshipEntry[] array = entry.GetRelationshipEnds().ToArray();
			foreach (RelationshipEntry relationshipEntry in array)
			{
				this.RemoveObjectStateEntryFromDictionary(relationshipEntry, relationshipEntry.State);
			}
			this.RemoveObjectStateEntryFromDictionary(entry, entry.State);
			this.ResetEntityKey(entry, entityKey2);
			entry.UpdateRelationshipEnds(entityKey, null);
			foreach (RelationshipEntry relationshipEntry2 in array)
			{
				this.AddRelationshipEntryToDictionary(relationshipEntry2, relationshipEntry2.State);
			}
			this.AddEntityEntryToDictionary(entry, EntityState.Added);
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000A48E8 File Offset: 0x000A2AE8
		private void ResetEntityKey(EntityEntry entry, EntityKey value)
		{
			EntityKey entityKey = entry.WrappedEntity.EntityKey;
			if (entityKey == null || value.Equals(entityKey))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_AcceptChangesEntityKeyIsNotValid);
			}
			try
			{
				this._inRelationshipFixup = true;
				entry.WrappedEntity.EntityKey = value;
				IEntityWrapper wrappedEntity = entry.WrappedEntity;
				if (wrappedEntity.EntityKey != value)
				{
					throw new InvalidOperationException(Strings.EntityKey_DoesntMatchKeyOnEntity(wrappedEntity.Entity.GetType().FullName));
				}
			}
			finally
			{
				this._inRelationshipFixup = false;
			}
			entry.EntityKey = value;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000A4984 File Offset: 0x000A2B84
		public virtual ObjectStateEntry ChangeObjectState(object entity, EntityState entityState)
		{
			Check.NotNull<object>(entity, "entity");
			EntityUtil.CheckValidStateForChangeEntityState(entityState);
			EntityEntry entityEntry = null;
			this.TransactionManager.BeginLocalPublicAPI();
			try
			{
				EntityKey entityKey = entity as EntityKey;
				entityEntry = ((entityKey != null) ? this.FindEntityEntry(entityKey) : this.FindEntityEntry(entity));
				if (entityEntry == null)
				{
					if (entityState == EntityState.Detached)
					{
						return null;
					}
					throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
				}
				else
				{
					entityEntry.ChangeObjectState(entityState);
				}
			}
			finally
			{
				this.TransactionManager.EndLocalPublicAPI();
			}
			return entityEntry;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000A4A1C File Offset: 0x000A2C1C
		public virtual ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string navigationProperty, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry entityEntry2;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out entityEntry2);
			Check.NotEmpty(navigationProperty, "navigationProperty");
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(navigationProperty, false);
			return this.ChangeRelationshipState(entityEntry, entityEntry2, relatedEnd, relationshipState);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000A4A60 File Offset: 0x000A2C60
		public virtual ObjectStateEntry ChangeRelationshipState<TEntity>(TEntity sourceEntity, object targetEntity, Expression<Func<TEntity, object>> navigationPropertySelector, EntityState relationshipState) where TEntity : class
		{
			EntityEntry entityEntry;
			EntityEntry entityEntry2;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out entityEntry2);
			bool flag;
			string text = ObjectContext.ParsePropertySelectorExpression<TEntity>(navigationPropertySelector, out flag);
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(text, flag);
			return this.ChangeRelationshipState(entityEntry, entityEntry2, relatedEnd, relationshipState);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000A4AA8 File Offset: 0x000A2CA8
		public virtual ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string relationshipName, string targetRoleName, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry entityEntry2;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out entityEntry2);
			RelatedEnd relatedEndInternal = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(relationshipName, targetRoleName);
			return this.ChangeRelationshipState(entityEntry, entityEntry2, relatedEndInternal, relationshipState);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		private ObjectStateEntry ChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
		{
			ObjectStateManager.VerifyInitialStateForChangeRelationshipState(sourceEntry, targetEntry, relatedEnd, relationshipState);
			RelationshipWrapper relationshipWrapper = new RelationshipWrapper((AssociationSet)relatedEnd.RelationshipSet, new KeyValuePair<string, EntityKey>(relatedEnd.SourceRoleName, sourceEntry.EntityKey), new KeyValuePair<string, EntityKey>(relatedEnd.TargetRoleName, targetEntry.EntityKey));
			RelationshipEntry relationshipEntry = this.FindRelationship(relationshipWrapper);
			if (relationshipEntry == null && relationshipState == EntityState.Detached)
			{
				return null;
			}
			this.TransactionManager.BeginLocalPublicAPI();
			try
			{
				if (relationshipEntry != null)
				{
					relationshipEntry.ChangeRelationshipState(targetEntry, relatedEnd, relationshipState);
				}
				else
				{
					relationshipEntry = this.CreateRelationship(targetEntry, relatedEnd, relationshipWrapper, relationshipState);
				}
			}
			finally
			{
				this.TransactionManager.EndLocalPublicAPI();
			}
			if (relationshipState != EntityState.Detached)
			{
				return relationshipEntry;
			}
			return null;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000A4B88 File Offset: 0x000A2D88
		private void VerifyParametersForChangeRelationshipState(object sourceEntity, object targetEntity, out EntityEntry sourceEntry, out EntityEntry targetEntry)
		{
			sourceEntry = this.GetEntityEntryByObjectOrEntityKey(sourceEntity);
			targetEntry = this.GetEntityEntryByObjectOrEntityKey(targetEntity);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000A4BA0 File Offset: 0x000A2DA0
		private static void VerifyInitialStateForChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
		{
			relatedEnd.VerifyType(targetEntry.WrappedEntity);
			if (relatedEnd.IsForeignKey)
			{
				throw new NotSupportedException(Strings.ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations);
			}
			EntityUtil.CheckValidStateForChangeRelationshipState(relationshipState, "relationshipState");
			if ((sourceEntry.State == EntityState.Deleted || targetEntry.State == EntityState.Deleted) && relationshipState != EntityState.Deleted && relationshipState != EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateEntityDeleted);
			}
			if ((sourceEntry.State == EntityState.Added || targetEntry.State == EntityState.Added) && relationshipState != EntityState.Added && relationshipState != EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateEntityAdded);
			}
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000A4C24 File Offset: 0x000A2E24
		private RelationshipEntry CreateRelationship(EntityEntry targetEntry, RelatedEnd relatedEnd, RelationshipWrapper relationshipWrapper, EntityState requestedState)
		{
			RelationshipEntry relationshipEntry = null;
			switch (requestedState)
			{
			case EntityState.Detached:
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Unchanged:
				relatedEnd.Add(targetEntry.WrappedEntity, true, false, false, false, true);
				relationshipEntry = this.FindRelationship(relationshipWrapper);
				relationshipEntry.AcceptChanges();
				break;
			case EntityState.Added:
				relatedEnd.Add(targetEntry.WrappedEntity, true, false, false, false, true);
				relationshipEntry = this.FindRelationship(relationshipWrapper);
				break;
			default:
				if (requestedState == EntityState.Deleted)
				{
					relationshipEntry = this.AddNewRelation(relationshipWrapper, EntityState.Deleted);
				}
				break;
			}
			return relationshipEntry;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000A4C9C File Offset: 0x000A2E9C
		private EntityEntry GetEntityEntryByObjectOrEntityKey(object o)
		{
			EntityKey entityKey = o as EntityKey;
			EntityEntry entityEntry = ((entityKey != null) ? this.FindEntityEntry(entityKey) : this.FindEntityEntry(o));
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(o.GetType().FullName));
			}
			if (entityEntry.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateKeyEntry);
			}
			return entityEntry;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000A4CF5 File Offset: 0x000A2EF5
		IEntityStateEntry IEntityStateManager.GetEntityStateEntry(EntityKey key)
		{
			return this.GetEntityEntry(key);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000A4D00 File Offset: 0x000A2F00
		public virtual ObjectStateEntry GetObjectStateEntry(EntityKey key)
		{
			ObjectStateEntry objectStateEntry;
			if (!this.TryGetObjectStateEntry(key, out objectStateEntry))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistForEntityKey);
			}
			return objectStateEntry;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000A4D24 File Offset: 0x000A2F24
		internal virtual EntityEntry GetEntityEntry(EntityKey key)
		{
			EntityEntry entityEntry;
			if (!this.TryGetEntityEntry(key, out entityEntry))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistForEntityKey);
			}
			return entityEntry;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000A4D48 File Offset: 0x000A2F48
		public virtual ObjectStateEntry GetObjectStateEntry(object entity)
		{
			ObjectStateEntry objectStateEntry;
			if (!this.TryGetObjectStateEntry(entity, out objectStateEntry))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
			}
			return objectStateEntry;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000A4D77 File Offset: 0x000A2F77
		internal virtual EntityEntry GetEntityEntry(object entity)
		{
			EntityEntry entityEntry = this.FindEntityEntry(entity);
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
			}
			return entityEntry;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000A4D9C File Offset: 0x000A2F9C
		public virtual bool TryGetObjectStateEntry(object entity, out ObjectStateEntry entry)
		{
			Check.NotNull<object>(entity, "entity");
			entry = null;
			EntityKey entityKey = entity as EntityKey;
			if (entityKey != null)
			{
				return this.TryGetObjectStateEntry(entityKey, out entry);
			}
			entry = this.FindEntityEntry(entity);
			return entry != null;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000A4DE0 File Offset: 0x000A2FE0
		bool IEntityStateManager.TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry entry)
		{
			ObjectStateEntry objectStateEntry;
			bool flag = this.TryGetObjectStateEntry(key, out objectStateEntry);
			entry = objectStateEntry;
			return flag;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000A4DFC File Offset: 0x000A2FFC
		bool IEntityStateManager.TryGetReferenceKey(EntityKey dependentKey, AssociationEndMember principalRole, out EntityKey principalKey)
		{
			EntityEntry entityEntry;
			if (!this.TryGetEntityEntry(dependentKey, out entityEntry))
			{
				principalKey = null;
				return false;
			}
			return entityEntry.TryGetReferenceKey(principalRole, out principalKey);
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000A4E24 File Offset: 0x000A3024
		public virtual bool TryGetObjectStateEntry(EntityKey key, out ObjectStateEntry entry)
		{
			EntityEntry entityEntry;
			bool flag = this.TryGetEntityEntry(key, out entityEntry);
			entry = entityEntry;
			return flag;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000A4E40 File Offset: 0x000A3040
		internal virtual bool TryGetEntityEntry(EntityKey key, out EntityEntry entry)
		{
			entry = null;
			bool flag;
			if (key.IsTemporary)
			{
				flag = this._addedEntityStore != null && this._addedEntityStore.TryGetValue(key, out entry);
			}
			else
			{
				flag = (this._unchangedEntityStore != null && this._unchangedEntityStore.TryGetValue(key, out entry)) || (this._modifiedEntityStore != null && this._modifiedEntityStore.TryGetValue(key, out entry)) || (this._deletedEntityStore != null && this._deletedEntityStore.TryGetValue(key, out entry));
			}
			return flag;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000A4EC0 File Offset: 0x000A30C0
		internal virtual EntityEntry FindEntityEntry(EntityKey key)
		{
			EntityEntry entityEntry = null;
			if (key != null)
			{
				this.TryGetEntityEntry(key, out entityEntry);
			}
			return entityEntry;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000A4EE0 File Offset: 0x000A30E0
		internal virtual EntityEntry FindEntityEntry(object entity)
		{
			EntityEntry entityEntry = null;
			IEntityWithKey entityWithKey = entity as IEntityWithKey;
			if (entityWithKey != null)
			{
				EntityKey entityKey = entityWithKey.EntityKey;
				if (entityKey != null)
				{
					this.TryGetEntityEntry(entityKey, out entityEntry);
				}
			}
			else
			{
				this.TryGetEntryFromKeylessStore(entity, out entityEntry);
			}
			if (entityEntry != null && entity != entityEntry.Entity)
			{
				entityEntry = null;
			}
			return entityEntry;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000A4F28 File Offset: 0x000A3128
		public virtual RelationshipManager GetRelationshipManager(object entity)
		{
			RelationshipManager relationshipManager;
			if (!this.TryGetRelationshipManager(entity, out relationshipManager))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity);
			}
			return relationshipManager;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000A4F4C File Offset: 0x000A314C
		public virtual bool TryGetRelationshipManager(object entity, out RelationshipManager relationshipManager)
		{
			Check.NotNull<object>(entity, "entity");
			IEntityWithRelationships entityWithRelationships = entity as IEntityWithRelationships;
			if (entityWithRelationships != null)
			{
				relationshipManager = entityWithRelationships.RelationshipManager;
				if (relationshipManager == null)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
				}
				if (relationshipManager.WrappedOwner.Entity != entity)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
				}
			}
			else
			{
				IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingStateManager(entity, this);
				if (entityWrapper.Context == null)
				{
					relationshipManager = null;
					return false;
				}
				relationshipManager = entityWrapper.RelationshipManager;
			}
			return true;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000A4FC4 File Offset: 0x000A31C4
		internal virtual void ChangeState(RelationshipEntry entry, EntityState oldState, EntityState newState)
		{
			if (newState == EntityState.Detached)
			{
				this.DeleteRelationshipFromLookup(entry);
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				entry.Reset();
				return;
			}
			this.RemoveObjectStateEntryFromDictionary(entry, oldState);
			this.AddRelationshipEntryToDictionary(entry, newState);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000A4FF0 File Offset: 0x000A31F0
		internal virtual void ChangeState(EntityEntry entry, EntityState oldState, EntityState newState)
		{
			bool flag = !entry.IsKeyEntry;
			if (newState == EntityState.Detached)
			{
				foreach (RelationshipEntry relationshipEntry in this.CopyOfRelationshipsByKey(entry.EntityKey))
				{
					this.ChangeState(relationshipEntry, relationshipEntry.State, EntityState.Detached);
				}
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				IEntityWrapper wrappedEntity = entry.WrappedEntity;
				entry.Reset();
				if (flag && wrappedEntity.Entity != null && !this.TransactionManager.IsAttachTracking)
				{
					this.OnEntityDeleted(CollectionChangeAction.Remove, wrappedEntity.Entity);
					this.OnObjectStateManagerChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
				}
			}
			else
			{
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				this.AddEntityEntryToDictionary(entry, newState);
			}
			if (newState == EntityState.Deleted)
			{
				entry.RemoveFromForeignKeyIndex();
				this.ForgetEntryWithConceptualNull(entry, true);
				if (flag)
				{
					this.OnEntityDeleted(CollectionChangeAction.Remove, entry.Entity);
					this.OnObjectStateManagerChanged(CollectionChangeAction.Remove, entry.Entity);
				}
			}
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000A50C4 File Offset: 0x000A32C4
		private void AddRelationshipEntryToDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state == EntityState.Deleted)
					{
						if (this._deletedRelationshipStore == null)
						{
							this._deletedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
						}
						dictionary = this._deletedRelationshipStore;
					}
				}
				else
				{
					if (this._addedRelationshipStore == null)
					{
						this._addedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
					}
					dictionary = this._addedRelationshipStore;
				}
			}
			else
			{
				if (this._unchangedRelationshipStore == null)
				{
					this._unchangedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
				}
				dictionary = this._unchangedRelationshipStore;
			}
			dictionary.Add(entry.RelationshipWrapper, entry);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000A5140 File Offset: 0x000A3340
		private void AddEntityEntryToDictionary(EntityEntry entry, EntityState state)
		{
			if (entry.RequiresAnyChangeTracking)
			{
				this._detectChangesNeeded = true;
			}
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state == EntityState.Added)
					{
						if (this._addedEntityStore == null)
						{
							this._addedEntityStore = new Dictionary<EntityKey, EntityEntry>();
						}
						dictionary = this._addedEntityStore;
					}
				}
				else
				{
					if (this._unchangedEntityStore == null)
					{
						this._unchangedEntityStore = new Dictionary<EntityKey, EntityEntry>();
					}
					dictionary = this._unchangedEntityStore;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state == EntityState.Modified)
				{
					if (this._modifiedEntityStore == null)
					{
						this._modifiedEntityStore = new Dictionary<EntityKey, EntityEntry>();
					}
					dictionary = this._modifiedEntityStore;
				}
			}
			else
			{
				if (this._deletedEntityStore == null)
				{
					this._deletedEntityStore = new Dictionary<EntityKey, EntityEntry>();
				}
				dictionary = this._deletedEntityStore;
			}
			dictionary.Add(entry.EntityKey, entry);
			this.AddEntryToKeylessStore(entry);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000A51FC File Offset: 0x000A33FC
		private void AddEntryToKeylessStore(EntityEntry entry)
		{
			if (entry.Entity != null && !(entry.Entity is IEntityWithKey))
			{
				if (this._keylessEntityStore == null)
				{
					this._keylessEntityStore = new Dictionary<object, EntityEntry>(ObjectReferenceEqualityComparer.Default);
				}
				if (!this._keylessEntityStore.ContainsKey(entry.Entity))
				{
					this._keylessEntityStore.Add(entry.Entity, entry);
				}
			}
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000A525C File Offset: 0x000A345C
		private void RemoveObjectStateEntryFromDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state == EntityState.Deleted)
					{
						dictionary = this._deletedRelationshipStore;
					}
				}
				else
				{
					dictionary = this._addedRelationshipStore;
				}
			}
			else
			{
				dictionary = this._unchangedRelationshipStore;
			}
			dictionary.Remove(entry.RelationshipWrapper);
			if (dictionary.Count == 0)
			{
				if (state == EntityState.Unchanged)
				{
					this._unchangedRelationshipStore = null;
					return;
				}
				if (state == EntityState.Added)
				{
					this._addedRelationshipStore = null;
					return;
				}
				if (state != EntityState.Deleted)
				{
					return;
				}
				this._deletedRelationshipStore = null;
			}
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000A52CC File Offset: 0x000A34CC
		private void RemoveObjectStateEntryFromDictionary(EntityEntry entry, EntityState state)
		{
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state == EntityState.Added)
					{
						dictionary = this._addedEntityStore;
					}
				}
				else
				{
					dictionary = this._unchangedEntityStore;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state == EntityState.Modified)
				{
					dictionary = this._modifiedEntityStore;
				}
			}
			else
			{
				dictionary = this._deletedEntityStore;
			}
			dictionary.Remove(entry.EntityKey);
			this.RemoveEntryFromKeylessStore(entry.WrappedEntity);
			if (dictionary.Count == 0)
			{
				if (state <= EntityState.Added)
				{
					if (state == EntityState.Unchanged)
					{
						this._unchangedEntityStore = null;
						return;
					}
					if (state != EntityState.Added)
					{
						return;
					}
					this._addedEntityStore = null;
					return;
				}
				else
				{
					if (state == EntityState.Deleted)
					{
						this._deletedEntityStore = null;
						return;
					}
					if (state != EntityState.Modified)
					{
						return;
					}
					this._modifiedEntityStore = null;
				}
			}
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000A536D File Offset: 0x000A356D
		internal virtual void RemoveEntryFromKeylessStore(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity != null && wrappedEntity.Entity != null && !(wrappedEntity.Entity is IEntityWithKey))
			{
				this._keylessEntityStore.Remove(wrappedEntity.Entity);
			}
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000A539C File Offset: 0x000A359C
		internal virtual StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(Type entityType, EntitySet entitySet)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata;
			if (!this._metadataMapping.TryGetValue(new EntitySetQualifiedType(entityType, entitySet), out stateManagerTypeMetadata))
			{
				stateManagerTypeMetadata = this.AddStateManagerTypeMetadata(entitySet, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(entityType.FullNameWithNesting(), DataSpace.OSpace, DataSpace.OCSpace));
			}
			return stateManagerTypeMetadata;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000A53E0 File Offset: 0x000A35E0
		internal virtual StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(EdmType edmType)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata;
			if (!this._metadataStore.TryGetValue(edmType, out stateManagerTypeMetadata))
			{
				stateManagerTypeMetadata = this.AddStateManagerTypeMetadata(edmType, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(edmType, DataSpace.OCSpace));
			}
			return stateManagerTypeMetadata;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000A5418 File Offset: 0x000A3618
		private StateManagerTypeMetadata AddStateManagerTypeMetadata(EntitySet entitySet, ObjectTypeMapping mapping)
		{
			EdmType edmType = mapping.EdmType;
			StateManagerTypeMetadata stateManagerTypeMetadata;
			if (!this._metadataStore.TryGetValue(edmType, out stateManagerTypeMetadata))
			{
				stateManagerTypeMetadata = new StateManagerTypeMetadata(edmType, mapping);
				this._metadataStore.Add(edmType, stateManagerTypeMetadata);
			}
			EntitySetQualifiedType entitySetQualifiedType = new EntitySetQualifiedType(mapping.ClrType.ClrType, entitySet);
			if (!this._metadataMapping.ContainsKey(entitySetQualifiedType))
			{
				this._metadataMapping.Add(entitySetQualifiedType, stateManagerTypeMetadata);
				return stateManagerTypeMetadata;
			}
			throw new InvalidOperationException(Strings.Mapping_CannotMapCLRTypeMultipleTimes(stateManagerTypeMetadata.CdmMetadata.EdmType.FullName));
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000A54A0 File Offset: 0x000A36A0
		private StateManagerTypeMetadata AddStateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata = new StateManagerTypeMetadata(edmType, mapping);
			this._metadataStore.Add(edmType, stateManagerTypeMetadata);
			return stateManagerTypeMetadata;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000A54C3 File Offset: 0x000A36C3
		internal virtual void Dispose()
		{
			this._isDisposed = true;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000A54CC File Offset: 0x000A36CC
		internal virtual bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000A54D4 File Offset: 0x000A36D4
		internal virtual void DetectChanges()
		{
			IList<EntityEntry> entityEntriesForDetectChanges = this.GetEntityEntriesForDetectChanges();
			if (entityEntriesForDetectChanges == null)
			{
				return;
			}
			if (this.TransactionManager.BeginDetectChanges())
			{
				try
				{
					ObjectStateManager.DetectChangesInNavigationProperties(entityEntriesForDetectChanges);
					ObjectStateManager.DetectChangesInScalarAndComplexProperties(entityEntriesForDetectChanges);
					ObjectStateManager.DetectChangesInForeignKeys(entityEntriesForDetectChanges);
					this.DetectConflicts(entityEntriesForDetectChanges);
					this.TransactionManager.BeginAlignChanges();
					this.AlignChangesInRelationships(entityEntriesForDetectChanges);
				}
				finally
				{
					this.TransactionManager.EndAlignChanges();
					this.TransactionManager.EndDetectChanges();
				}
			}
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000A5550 File Offset: 0x000A3750
		private void DetectConflicts(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary;
				transactionManager.AddedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary);
				Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary2;
				transactionManager.AddedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary2);
				if (dictionary != null && dictionary.Count > 0 && entityEntry.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
				}
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<RelatedEnd, HashSet<EntityKey>> keyValuePair in dictionary2)
					{
						if ((entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified) && keyValuePair.Key.IsDependentEndOfReferentialConstraint(true) && keyValuePair.Value.Count > 0)
						{
							throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
						}
						if (keyValuePair.Key is EntityReference && keyValuePair.Value.Count > 1)
						{
							throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(keyValuePair.Key.RelationshipNavigation.To, keyValuePair.Key.RelationshipNavigation.RelationshipName));
						}
					}
				}
				if (dictionary != null)
				{
					Dictionary<string, KeyValuePair<object, IntBox>> dictionary3 = new Dictionary<string, KeyValuePair<object, IntBox>>();
					foreach (KeyValuePair<RelatedEnd, HashSet<IEntityWrapper>> keyValuePair2 in dictionary)
					{
						if (keyValuePair2.Key.IsForeignKey && (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified) && keyValuePair2.Key.IsDependentEndOfReferentialConstraint(true) && keyValuePair2.Value.Count > 0)
						{
							throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
						}
						EntityReference entityReference = keyValuePair2.Key as EntityReference;
						if (entityReference != null)
						{
							if (keyValuePair2.Value.Count > 1)
							{
								throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(keyValuePair2.Key.RelationshipNavigation.To, keyValuePair2.Key.RelationshipNavigation.RelationshipName));
							}
							if (keyValuePair2.Value.Count == 1)
							{
								IEntityWrapper entityWrapper = keyValuePair2.Value.First<IEntityWrapper>();
								HashSet<EntityKey> hashSet = null;
								Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary4;
								if (dictionary2 != null)
								{
									dictionary2.TryGetValue(keyValuePair2.Key, out hashSet);
								}
								else if (transactionManager.AddedRelationshipsByPrincipalKey.TryGetValue(entityEntry.WrappedEntity, out dictionary4))
								{
									dictionary4.TryGetValue(keyValuePair2.Key, out hashSet);
								}
								Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary5;
								HashSet<EntityKey> hashSet2;
								if (hashSet != null && hashSet.Count > 0)
								{
									if (this.GetPermanentKey(entityEntry.WrappedEntity, entityReference, entityWrapper) != hashSet.First<EntityKey>())
									{
										throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName));
									}
								}
								else if (transactionManager.DeletedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary5) && dictionary5.TryGetValue(keyValuePair2.Key, out hashSet2) && hashSet2.Count > 0)
								{
									throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName));
								}
								EntityEntry entityEntry2 = this.FindEntityEntry(entityWrapper.Entity);
								if (entityEntry2 != null && (entityEntry2.State == EntityState.Unchanged || entityEntry2.State == EntityState.Modified))
								{
									Dictionary<string, KeyValuePair<object, IntBox>> dictionary6 = new Dictionary<string, KeyValuePair<object, IntBox>>();
									entityEntry2.GetOtherKeyProperties(dictionary6);
									foreach (ReferentialConstraint referentialConstraint in ((AssociationType)entityReference.RelationMetadata).ReferentialConstraints)
									{
										if (referentialConstraint.ToRole == entityReference.FromEndMember)
										{
											for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
											{
												EntityEntry.AddOrIncreaseCounter(referentialConstraint, dictionary3, referentialConstraint.ToProperties[i].Name, dictionary6[referentialConstraint.FromProperties[i].Name].Key);
											}
											break;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000A59C0 File Offset: 0x000A3BC0
		internal virtual EntityKey GetPermanentKey(IEntityWrapper entityFrom, RelatedEnd relatedEndFrom, IEntityWrapper entityTo)
		{
			EntityKey entityKey = null;
			if (entityTo.ObjectStateEntry != null)
			{
				entityKey = entityTo.ObjectStateEntry.EntityKey;
			}
			if (entityKey == null || entityKey.IsTemporary)
			{
				entityKey = this.CreateEntityKey(ObjectStateManager.GetEntitySetOfOtherEnd(entityFrom, relatedEndFrom), entityTo.Entity);
			}
			return entityKey;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000A5A0C File Offset: 0x000A3C0C
		private static EntitySet GetEntitySetOfOtherEnd(IEntityWrapper entity, RelatedEnd relatedEnd)
		{
			AssociationSet associationSet = (AssociationSet)relatedEnd.RelationshipSet;
			EntitySet entitySet = associationSet.AssociationSetEnds[0].EntitySet;
			if (entitySet.Name != entity.EntityKey.EntitySetName)
			{
				return entitySet;
			}
			return associationSet.AssociationSetEnds[1].EntitySet;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000A5A64 File Offset: 0x000A3C64
		private static void DetectChangesInForeignKeys(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
				{
					entityEntry.DetectChangesInForeignKeys();
				}
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000A5AC0 File Offset: 0x000A3CC0
		private void AlignChangesInRelationships(IList<EntityEntry> entries)
		{
			this.PerformDelete(entries);
			this.PerformAdd(entries);
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000A5AD0 File Offset: 0x000A3CD0
		private void PerformAdd(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Detached && !entityEntry.IsKeyEntry)
				{
					foreach (RelatedEnd relatedEnd in entityEntry.WrappedEntity.RelationshipManager.Relationships)
					{
						HashSet<EntityKey> hashSet = null;
						Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
						if (relatedEnd is EntityReference && transactionManager.AddedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary))
						{
							dictionary.TryGetValue(relatedEnd, out hashSet);
						}
						HashSet<IEntityWrapper> hashSet2 = null;
						Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
						if (transactionManager.AddedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary2))
						{
							dictionary2.TryGetValue(relatedEnd, out hashSet2);
						}
						if (hashSet != null)
						{
							foreach (EntityKey entityKey in hashSet)
							{
								EntityEntry entityEntry2;
								if (this.TryGetEntityEntry(entityKey, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									hashSet2 = ((hashSet2 != null) ? hashSet2 : new HashSet<IEntityWrapper>());
									if (entityEntry2.State != EntityState.Deleted)
									{
										hashSet2.Remove(entityEntry2.WrappedEntity);
										this.PerformAdd(entityEntry.WrappedEntity, relatedEnd, entityEntry2.WrappedEntity, true);
									}
								}
								else
								{
									EntityReference entityReference = relatedEnd as EntityReference;
									entityEntry.FixupEntityReferenceByForeignKey(entityReference);
								}
							}
						}
						if (hashSet2 != null)
						{
							foreach (IEntityWrapper entityWrapper in hashSet2)
							{
								this.PerformAdd(entityEntry.WrappedEntity, relatedEnd, entityWrapper, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000A5CF8 File Offset: 0x000A3EF8
		private void PerformAdd(IEntityWrapper wrappedOwner, RelatedEnd relatedEnd, IEntityWrapper entityToAdd, bool isForeignKeyChange)
		{
			relatedEnd.ValidateStateForAdd(relatedEnd.WrappedOwner);
			relatedEnd.ValidateStateForAdd(entityToAdd);
			if (relatedEnd.IsPrincipalEndOfReferentialConstraint())
			{
				EntityReference entityReference = relatedEnd.GetOtherEndOfRelationship(entityToAdd) as EntityReference;
				if (entityReference != null && this.IsReparentingReference(entityToAdd, entityReference))
				{
					this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
				}
			}
			else if (relatedEnd.IsDependentEndOfReferentialConstraint(false))
			{
				EntityReference entityReference2 = relatedEnd as EntityReference;
				if (entityReference2 != null && this.IsReparentingReference(wrappedOwner, entityReference2))
				{
					this.TransactionManager.EntityBeingReparented = entityReference2.GetDependentEndOfReferentialConstraint(entityReference2.ReferenceValue.Entity);
				}
			}
			try
			{
				relatedEnd.Add(entityToAdd, false, false, false, true, !isForeignKeyChange);
			}
			finally
			{
				this.TransactionManager.EntityBeingReparented = null;
			}
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000A5DC4 File Offset: 0x000A3FC4
		private void PerformDelete(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Detached && entityEntry.State != EntityState.Deleted && !entityEntry.IsKeyEntry)
				{
					foreach (RelatedEnd relatedEnd in entityEntry.WrappedEntity.RelationshipManager.Relationships)
					{
						HashSet<EntityKey> hashSet = null;
						EntityReference entityReference = relatedEnd as EntityReference;
						Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
						if (entityReference != null && transactionManager.DeletedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary))
						{
							dictionary.TryGetValue(entityReference, out hashSet);
						}
						HashSet<IEntityWrapper> hashSet2 = null;
						Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
						if (transactionManager.DeletedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary2))
						{
							dictionary2.TryGetValue(relatedEnd, out hashSet2);
						}
						if (hashSet != null)
						{
							foreach (EntityKey entityKey in hashSet)
							{
								IEntityWrapper entityWrapper = null;
								EntityEntry entityEntry2;
								if (this.TryGetEntityEntry(entityKey, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									entityWrapper = entityEntry2.WrappedEntity;
								}
								else if (entityReference != null && entityReference.ReferenceValue != NullEntityWrapper.NullWrapper && entityReference.ReferenceValue.EntityKey.IsTemporary && this.TryGetEntityEntry(entityReference.ReferenceValue.EntityKey, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									EntityKey entityKey2 = new EntityKey((EntitySet)entityEntry2.EntitySet, entityEntry2.CurrentValues);
									if (entityKey == entityKey2)
									{
										entityWrapper = entityEntry2.WrappedEntity;
									}
								}
								if (entityWrapper != null)
								{
									hashSet2 = ((hashSet2 != null) ? hashSet2 : new HashSet<IEntityWrapper>());
									bool flag = this.ShouldPreserveForeignKeyForDependent(entityEntry.WrappedEntity, relatedEnd, entityWrapper, hashSet2);
									hashSet2.Remove(entityWrapper);
									if (entityReference != null && this.IsReparentingReference(entityEntry.WrappedEntity, entityReference))
									{
										this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
									}
									try
									{
										relatedEnd.Remove(entityWrapper, flag);
									}
									finally
									{
										this.TransactionManager.EntityBeingReparented = null;
									}
									if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
									{
										break;
									}
									if (entityEntry.IsKeyEntry)
									{
										break;
									}
								}
								if (entityReference != null && entityReference.IsForeignKey && entityReference.IsDependentEndOfReferentialConstraint(false))
								{
									entityReference.SetCachedForeignKey(ForeignKeyFactory.CreateKeyFromForeignKeyValues(entityEntry, entityReference), entityEntry);
								}
							}
						}
						if (hashSet2 != null)
						{
							foreach (IEntityWrapper entityWrapper2 in hashSet2)
							{
								bool flag2 = this.ShouldPreserveForeignKeyForPrincipal(entityEntry.WrappedEntity, relatedEnd, entityWrapper2, hashSet2);
								if (entityReference != null && this.IsReparentingReference(entityEntry.WrappedEntity, entityReference))
								{
									this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
								}
								try
								{
									relatedEnd.Remove(entityWrapper2, flag2);
								}
								finally
								{
									this.TransactionManager.EntityBeingReparented = null;
								}
								if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
								{
									break;
								}
								if (entityEntry.IsKeyEntry)
								{
									break;
								}
							}
						}
						if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
						{
							break;
						}
						if (entityEntry.IsKeyEntry)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000A61BC File Offset: 0x000A43BC
		private bool ShouldPreserveForeignKeyForPrincipal(IEntityWrapper entity, RelatedEnd relatedEnd, IEntityWrapper relatedEntity, HashSet<IEntityWrapper> entitiesToDelete)
		{
			bool flag = false;
			if (relatedEnd.IsForeignKey)
			{
				RelatedEnd otherEndOfRelationship = relatedEnd.GetOtherEndOfRelationship(relatedEntity);
				if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
				{
					HashSet<EntityKey> hashSet = null;
					Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
					Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
					if (this.TransactionManager.DeletedRelationshipsByForeignKey.TryGetValue(relatedEntity, out dictionary) && dictionary.TryGetValue(otherEndOfRelationship, out hashSet) && hashSet.Count > 0 && this.TransactionManager.DeletedRelationshipsByGraph.TryGetValue(relatedEntity, out dictionary2) && dictionary2.TryGetValue(otherEndOfRelationship, out entitiesToDelete))
					{
						flag = this.ShouldPreserveForeignKeyForDependent(relatedEntity, otherEndOfRelationship, entity, entitiesToDelete);
					}
				}
			}
			return flag;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000A6240 File Offset: 0x000A4440
		private bool ShouldPreserveForeignKeyForDependent(IEntityWrapper entity, RelatedEnd relatedEnd, IEntityWrapper relatedEntity, HashSet<IEntityWrapper> entitiesToDelete)
		{
			bool flag = entitiesToDelete.Contains(relatedEntity);
			return !flag || (flag && !this.HasAddedReference(entity, relatedEnd as EntityReference));
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000A6270 File Offset: 0x000A4470
		private bool HasAddedReference(IEntityWrapper wrappedOwner, EntityReference reference)
		{
			HashSet<IEntityWrapper> hashSet = null;
			Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary;
			return reference != null && this.TransactionManager.AddedRelationshipsByGraph.TryGetValue(wrappedOwner, out dictionary) && dictionary.TryGetValue(reference, out hashSet) && hashSet.Count > 0;
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000A62B0 File Offset: 0x000A44B0
		private bool IsReparentingReference(IEntityWrapper wrappedEntity, EntityReference reference)
		{
			TransactionManager transactionManager = this.TransactionManager;
			if (reference.IsPrincipalEndOfReferentialConstraint())
			{
				wrappedEntity = reference.ReferenceValue;
				reference = ((wrappedEntity.Entity == null) ? null : (reference.GetOtherEndOfRelationship(wrappedEntity) as EntityReference));
			}
			if (wrappedEntity.Entity != null && reference != null)
			{
				HashSet<EntityKey> hashSet = null;
				Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
				if (transactionManager.AddedRelationshipsByForeignKey.TryGetValue(wrappedEntity, out dictionary) && dictionary.TryGetValue(reference, out hashSet) && hashSet.Count > 0)
				{
					return true;
				}
				HashSet<IEntityWrapper> hashSet2 = null;
				Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
				if (transactionManager.AddedRelationshipsByGraph.TryGetValue(wrappedEntity, out dictionary2) && dictionary2.TryGetValue(reference, out hashSet2) && hashSet2.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000A634C File Offset: 0x000A454C
		private static void DetectChangesInNavigationProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.WrappedEntity.RequiresRelationshipChangeTracking)
				{
					entityEntry.DetectChangesInRelationshipsOfSingleEntity();
				}
			}
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000A63A0 File Offset: 0x000A45A0
		private static void DetectChangesInScalarAndComplexProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Added && (entityEntry.RequiresScalarChangeTracking || entityEntry.RequiresComplexChangeTracking))
				{
					entityEntry.DetectChangesInProperties(!entityEntry.RequiresScalarChangeTracking);
				}
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000A640C File Offset: 0x000A460C
		internal virtual EntityKey CreateEntityKey(EntitySet entitySet, object entity)
		{
			ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(EntityUtil.GetEntityIdentityType(entity.GetType()), entitySet);
			object[] array = new object[keyMembers.Count];
			for (int i = 0; i < keyMembers.Count; i++)
			{
				string name = keyMembers[i].Name;
				int ordinalforCLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(name);
				if (ordinalforCLayerMemberName < 0)
				{
					throw new ArgumentException(Strings.ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(entity.GetType().FullName, entitySet.Name), "entity");
				}
				array[i] = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(entity);
				if (array[i] == null)
				{
					throw new InvalidOperationException(Strings.EntityKey_NullKeyValue(name, entitySet.ElementType.Name));
				}
			}
			if (array.Length == 1)
			{
				return new EntityKey(entitySet, array[0]);
			}
			return new EntityKey(entitySet, array);
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000A64D8 File Offset: 0x000A46D8
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000A64E0 File Offset: 0x000A46E0
		internal virtual object EntityInvokingFKSetter { get; set; }

		// Token: 0x0400108E RID: 4238
		private const int InitialListSize = 16;

		// Token: 0x0400108F RID: 4239
		private Dictionary<EntityKey, EntityEntry> _addedEntityStore;

		// Token: 0x04001090 RID: 4240
		private Dictionary<EntityKey, EntityEntry> _modifiedEntityStore;

		// Token: 0x04001091 RID: 4241
		private Dictionary<EntityKey, EntityEntry> _deletedEntityStore;

		// Token: 0x04001092 RID: 4242
		private Dictionary<EntityKey, EntityEntry> _unchangedEntityStore;

		// Token: 0x04001093 RID: 4243
		private Dictionary<object, EntityEntry> _keylessEntityStore;

		// Token: 0x04001094 RID: 4244
		private Dictionary<RelationshipWrapper, RelationshipEntry> _addedRelationshipStore;

		// Token: 0x04001095 RID: 4245
		private Dictionary<RelationshipWrapper, RelationshipEntry> _deletedRelationshipStore;

		// Token: 0x04001096 RID: 4246
		private Dictionary<RelationshipWrapper, RelationshipEntry> _unchangedRelationshipStore;

		// Token: 0x04001097 RID: 4247
		private readonly Dictionary<EdmType, StateManagerTypeMetadata> _metadataStore;

		// Token: 0x04001098 RID: 4248
		private readonly Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata> _metadataMapping;

		// Token: 0x04001099 RID: 4249
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x0400109A RID: 4250
		private CollectionChangeEventHandler onObjectStateManagerChangedDelegate;

		// Token: 0x0400109B RID: 4251
		private CollectionChangeEventHandler onEntityDeletedDelegate;

		// Token: 0x0400109C RID: 4252
		private bool _inRelationshipFixup;

		// Token: 0x0400109D RID: 4253
		private bool _isDisposed;

		// Token: 0x0400109E RID: 4254
		private ComplexTypeMaterializer _complexTypeMaterializer;

		// Token: 0x0400109F RID: 4255
		private readonly Dictionary<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> _danglingForeignKeys = new Dictionary<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>>();

		// Token: 0x040010A0 RID: 4256
		private HashSet<EntityEntry> _entriesWithConceptualNulls;

		// Token: 0x040010A1 RID: 4257
		private readonly EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x040010A2 RID: 4258
		private bool _detectChangesNeeded;
	}
}
