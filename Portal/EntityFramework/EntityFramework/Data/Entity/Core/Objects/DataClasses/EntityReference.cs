using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000477 RID: 1143
	[DataContract]
	[Serializable]
	public abstract class EntityReference : RelatedEnd
	{
		// Token: 0x060037E3 RID: 14307 RVA: 0x000B6DC3 File Offset: 0x000B4FC3
		internal EntityReference()
		{
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000B6DCB File Offset: 0x000B4FCB
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
			: base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000B6DD8 File Offset: 0x000B4FD8
		// (set) Token: 0x060037E6 RID: 14310 RVA: 0x000B6F18 File Offset: 0x000B5118
		[DataMember]
		public EntityKey EntityKey
		{
			get
			{
				if (this.ObjectContext != null && !base.UsingNoTracking)
				{
					EntityKey entityKey = null;
					if (this.CachedValue.Entity != null)
					{
						entityKey = this.CachedValue.EntityKey;
						if (entityKey != null && !RelatedEnd.IsValidEntityKeyType(entityKey))
						{
							entityKey = null;
						}
					}
					else if (base.IsForeignKey)
					{
						if (base.IsDependentEndOfReferentialConstraint(false) && this._cachedForeignKey != null)
						{
							if (!ForeignKeyFactory.IsConceptualNullKey(this._cachedForeignKey))
							{
								entityKey = this._cachedForeignKey;
							}
						}
						else
						{
							entityKey = this.DetachedEntityKey;
						}
					}
					else
					{
						EntityKey entityKey2 = this.WrappedOwner.EntityKey;
						foreach (RelationshipEntry relationshipEntry in this.ObjectContext.ObjectStateManager.FindRelationshipsByKey(entityKey2))
						{
							if (relationshipEntry.State != EntityState.Deleted && relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)this.RelationshipSet, (AssociationEndMember)this.FromEndMember, entityKey2))
							{
								entityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(entityKey2);
							}
						}
					}
					return entityKey;
				}
				return this.DetachedEntityKey;
			}
			set
			{
				this.SetEntityKey(value, false);
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000B6F24 File Offset: 0x000B5124
		internal void SetEntityKey(EntityKey value, bool forceFixup)
		{
			if (value != null && value == this.EntityKey && (this.ReferenceValue.Entity != null || (this.ReferenceValue.Entity == null && !forceFixup)))
			{
				return;
			}
			if (this.ObjectContext != null && !base.UsingNoTracking)
			{
				if (value != null && !RelatedEnd.IsValidEntityKeyType(value))
				{
					throw new ArgumentException(Strings.EntityReference_CannotSetSpecialKeys, "value");
				}
				if (value == null)
				{
					if (this.AttemptToNullFKsOnRefOrKeySetToNull())
					{
						this.DetachedEntityKey = null;
						return;
					}
					this.ReferenceValue = NullEntityWrapper.NullWrapper;
					return;
				}
				else
				{
					EntitySet entitySet = value.GetEntitySet(this.ObjectContext.MetadataWorkspace);
					base.CheckRelationEntitySet(entitySet);
					value.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, entitySet, true, "value");
					ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
					bool flag = false;
					bool flag2 = false;
					EntityEntry entityEntry = objectStateManager.FindEntityEntry(value);
					if (entityEntry != null)
					{
						if (!entityEntry.IsKeyEntry)
						{
							this.ReferenceValue = entityEntry.WrappedEntity;
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag2 = !base.IsForeignKey;
						flag = true;
					}
					if (flag)
					{
						EntityKey entityKey = this.ValidateOwnerWithRIConstraints((entityEntry == null) ? null : entityEntry.WrappedEntity, value, true);
						base.ValidateStateForAdd(this.WrappedOwner);
						if (flag2)
						{
							objectStateManager.AddKeyEntry(value, entitySet);
						}
						objectStateManager.TransactionManager.EntityBeingReparented = this.WrappedOwner.Entity;
						try
						{
							this.ClearCollectionOrRef(null, null, false);
						}
						finally
						{
							objectStateManager.TransactionManager.EntityBeingReparented = null;
						}
						if (!base.IsForeignKey)
						{
							RelationshipWrapper relationshipWrapper = new RelationshipWrapper((AssociationSet)this.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, value);
							EntityState entityState = EntityState.Added;
							if (!entityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(false))
							{
								entityState = EntityState.Unchanged;
							}
							objectStateManager.AddNewRelation(relationshipWrapper, entityState);
							return;
						}
						this.DetachedEntityKey = value;
						if (base.IsDependentEndOfReferentialConstraint(false))
						{
							this.UpdateForeignKeyValues(this.WrappedOwner, value);
							return;
						}
					}
				}
			}
			else
			{
				this.DetachedEntityKey = value;
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000B7128 File Offset: 0x000B5328
		internal bool AttemptToNullFKsOnRefOrKeySetToNull()
		{
			if (this.ReferenceValue.Entity != null || this.WrappedOwner.Entity == null || this.WrappedOwner.Context == null || base.UsingNoTracking || !base.IsForeignKey)
			{
				return false;
			}
			if (this.WrappedOwner.ObjectStateEntry.State != EntityState.Added && base.IsDependentEndOfReferentialConstraint(true))
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
			}
			this.RemoveFromLocalCache(NullEntityWrapper.NullWrapper, true, false);
			return true;
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000B71A4 File Offset: 0x000B53A4
		internal EntityKey AttachedEntityKey
		{
			get
			{
				return this.EntityKey;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000B71AC File Offset: 0x000B53AC
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x000B71B4 File Offset: 0x000B53B4
		internal EntityKey DetachedEntityKey
		{
			get
			{
				return this._detachedEntityKey;
			}
			set
			{
				this._detachedEntityKey = value;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000B71BD File Offset: 0x000B53BD
		internal EntityKey CachedForeignKey
		{
			get
			{
				return this.EntityKey ?? this._cachedForeignKey;
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000B71D0 File Offset: 0x000B53D0
		internal void SetCachedForeignKey(EntityKey newForeignKey, EntityEntry source)
		{
			if (this.ObjectContext != null && this.ObjectContext.ObjectStateManager != null && source != null && this._cachedForeignKey != null && !ForeignKeyFactory.IsConceptualNullKey(this._cachedForeignKey) && this._cachedForeignKey != newForeignKey)
			{
				this.ObjectContext.ObjectStateManager.RemoveEntryFromForeignKeyIndex(this, this._cachedForeignKey, source);
			}
			this._cachedForeignKey = newForeignKey;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000B723D File Offset: 0x000B543D
		internal IEnumerable<EntityKey> GetAllKeyValues()
		{
			if (this.EntityKey != null)
			{
				yield return this.EntityKey;
			}
			if (this._cachedForeignKey != null)
			{
				yield return this._cachedForeignKey;
			}
			if (this._detachedEntityKey != null)
			{
				yield return this._detachedEntityKey;
			}
			yield break;
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060037EF RID: 14319
		internal abstract IEntityWrapper CachedValue { get; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060037F0 RID: 14320
		// (set) Token: 0x060037F1 RID: 14321
		internal abstract IEntityWrapper ReferenceValue { get; set; }

		// Token: 0x060037F2 RID: 14322 RVA: 0x000B7250 File Offset: 0x000B5450
		internal EntityKey ValidateOwnerWithRIConstraints(IEntityWrapper targetEntity, EntityKey targetEntityKey, bool checkBothEnds)
		{
			EntityKey entityKey = this.WrappedOwner.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true))
			{
				this.ValidateSettingRIConstraints(targetEntity, targetEntityKey == null, this.CachedForeignKey != null && this.CachedForeignKey != targetEntityKey);
			}
			else if (checkBothEnds && targetEntity != null && targetEntity.Entity != null)
			{
				EntityReference entityReference = base.GetOtherEndOfRelationship(targetEntity) as EntityReference;
				if (entityReference != null)
				{
					entityReference.ValidateOwnerWithRIConstraints(this.WrappedOwner, entityKey, false);
				}
			}
			return entityKey;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000B72D8 File Offset: 0x000B54D8
		internal void ValidateSettingRIConstraints(IEntityWrapper targetEntity, bool settingToNull, bool changingForeignKeyValue)
		{
			bool flag = targetEntity != null && targetEntity.MergeOption == MergeOption.NoTracking;
			if (settingToNull || changingForeignKeyValue || (targetEntity != null && !flag && (targetEntity.ObjectStateEntry == null || (this.EntityKey == null && targetEntity.ObjectStateEntry.State == EntityState.Deleted) || (this.CachedForeignKey == null && targetEntity.ObjectStateEntry.State == EntityState.Added))))
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000B734B File Offset: 0x000B554B
		internal override bool CanDeferredLoad
		{
			get
			{
				return this.IsEmpty();
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000B7354 File Offset: 0x000B5554
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, IEntityWrapper principalEntity, Dictionary<int, object> changedFKs, bool forceChange)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			bool flag = this.WrappedOwner.EntityKey != null && !this.WrappedOwner.EntityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true);
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.ToEndMember.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(principalEntity.IdentityType, entitySet);
				EntitySet entitySet2 = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata2 = objectStateManager.GetOrAddStateManagerTypeMetadata(dependentEntity.IdentityType, entitySet2);
				ReadOnlyMetadataCollection<EdmProperty> fromProperties = referentialConstraint.FromProperties;
				int count = fromProperties.Count;
				string[] array = null;
				object[] array2 = null;
				if (count > 1)
				{
					array = entitySet.ElementType.KeyMemberNames;
					array2 = new object[count];
				}
				for (int i = 0; i < count; i++)
				{
					int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(fromProperties[i].Name);
					object value = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName).GetValue(principalEntity.Entity);
					int ordinalforOLayerMemberName2 = orAddStateManagerTypeMetadata2.GetOrdinalforOLayerMemberName(referentialConstraint.ToProperties[i].Name);
					bool flag2 = !ByValueEqualityComparer.Default.Equals(orAddStateManagerTypeMetadata2.Member(ordinalforOLayerMemberName2).GetValue(dependentEntity.Entity), value);
					if (forceChange || flag2)
					{
						if (flag)
						{
							this.ValidateSettingRIConstraints(principalEntity, value == null, flag2);
						}
						if (changedFKs != null)
						{
							object obj;
							if (changedFKs.TryGetValue(ordinalforOLayerMemberName2, out obj))
							{
								if (!ByValueEqualityComparer.Default.Equals(obj, value))
								{
									throw new InvalidOperationException(Strings.Update_ReferentialConstraintIntegrityViolation);
								}
							}
							else
							{
								changedFKs[ordinalforOLayerMemberName2] = value;
							}
						}
						if (flag2)
						{
							dependentEntity.SetCurrentValue(dependentEntity.ObjectStateEntry, orAddStateManagerTypeMetadata2.Member(ordinalforOLayerMemberName2), -1, dependentEntity.Entity, value);
						}
					}
					if (count > 1)
					{
						int num = Array.IndexOf<string>(array, fromProperties[i].Name);
						array2[num] = value;
					}
					else
					{
						this.SetCachedForeignKey((value == null) ? null : new EntityKey(entitySet, value), dependentEntity.ObjectStateEntry);
					}
				}
				if (count > 1)
				{
					this.SetCachedForeignKey(array2.Any((object v) => v == null) ? null : new EntityKey(entitySet, array2), dependentEntity.ObjectStateEntry);
				}
				if (this.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(this.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000B7618 File Offset: 0x000B5818
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, EntityKey principalKey)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(dependentEntity.IdentityType, entitySet);
				for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
				{
					object obj = principalKey.FindValueByName(referentialConstraint.FromProperties[i].Name);
					int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(referentialConstraint.ToProperties[i].Name);
					object value = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName).GetValue(dependentEntity.Entity);
					if (!ByValueEqualityComparer.Default.Equals(value, obj))
					{
						dependentEntity.SetCurrentValue(dependentEntity.ObjectStateEntry, orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName), -1, dependentEntity.Entity, obj);
					}
				}
				this.SetCachedForeignKey(principalKey, dependentEntity.ObjectStateEntry);
				if (this.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(this.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000B7768 File Offset: 0x000B5968
		internal object GetDependentEndOfReferentialConstraint(object relatedValue)
		{
			if (!base.IsDependentEndOfReferentialConstraint(false))
			{
				return relatedValue;
			}
			return this.WrappedOwner.Entity;
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000B7780 File Offset: 0x000B5980
		internal bool NavigationPropertyIsNullOrMissing()
		{
			return !base.TargetAccessor.HasProperty || this.WrappedOwner.GetNavigationPropertyValue(this) == null;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000B77A0 File Offset: 0x000B59A0
		internal override void AddEntityToObjectStateManager(IEntityWrapper wrappedEntity, bool doAttach)
		{
			base.AddEntityToObjectStateManager(wrappedEntity, doAttach);
			if (this.DetachedEntityKey != null)
			{
				EntityKey entityKey = wrappedEntity.EntityKey;
				if (this.DetachedEntityKey != entityKey)
				{
					throw new InvalidOperationException(Strings.EntityReference_EntityKeyValueMismatch);
				}
			}
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000B77E4 File Offset: 0x000B59E4
		internal override void AddToNavigationPropertyIfCompatible(RelatedEnd otherRelatedEnd)
		{
			if (this.NavigationPropertyIsNullOrMissing())
			{
				base.AddToNavigationProperty(otherRelatedEnd.WrappedOwner);
				if (otherRelatedEnd.ObjectContext.ObjectStateManager.FindEntityEntry(otherRelatedEnd.WrappedOwner.Entity) != null && otherRelatedEnd.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && otherRelatedEnd.IsForeignKey && base.IsDependentEndOfReferentialConstraint(false))
				{
					base.MarkForeignKeyPropertiesModified();
					return;
				}
			}
			else if (!this.CheckIfNavigationPropertyContainsEntity(otherRelatedEnd.WrappedOwner))
			{
				throw Error.ObjectStateManager_ConflictingChangesOfRelationshipDetected(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName);
			}
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000B787B File Offset: 0x000B5A7B
		internal override bool CachedForeignKeyIsConceptualNull()
		{
			return ForeignKeyFactory.IsConceptualNullKey(this.CachedForeignKey);
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000B7888 File Offset: 0x000B5A88
		internal override bool UpdateDependentEndForeignKey(RelatedEnd targetRelatedEnd, bool forceForeignKeyChanges)
		{
			if (base.IsDependentEndOfReferentialConstraint(false))
			{
				this.UpdateForeignKeyValues(this.WrappedOwner, targetRelatedEnd.WrappedOwner, null, forceForeignKeyChanges);
				return true;
			}
			return false;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000B78AC File Offset: 0x000B5AAC
		internal override void ValidateDetachedEntityKey()
		{
			if (this.IsEmpty() && this.DetachedEntityKey != null)
			{
				EntityKey detachedEntityKey = this.DetachedEntityKey;
				if (!RelatedEnd.IsValidEntityKeyType(detachedEntityKey))
				{
					throw Error.EntityReference_CannotSetSpecialKeys();
				}
				EntitySet entitySet = detachedEntityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace);
				base.CheckRelationEntitySet(entitySet);
				detachedEntityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, entitySet);
			}
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000B7910 File Offset: 0x000B5B10
		internal override void VerifyDetachedKeyMatches(EntityKey entityKey)
		{
			if (!(this.DetachedEntityKey != null) || !(this.DetachedEntityKey != entityKey))
			{
				return;
			}
			if (entityKey.IsTemporary)
			{
				throw Error.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(base.RelationshipNavigation.To);
			}
			throw new InvalidOperationException(Strings.EntityReference_EntityKeyValueMismatch);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000B795F File Offset: 0x000B5B5F
		internal override void DetachAll(EntityState ownerEntityState)
		{
			this.DetachedEntityKey = this.AttachedEntityKey;
			base.DetachAll(ownerEntityState);
			if (base.IsForeignKey)
			{
				this.DetachedEntityKey = null;
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000B7984 File Offset: 0x000B5B84
		internal override bool CheckReferentialConstraintPrincipalProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			EntityKey entityKey;
			if (!this.IsEmpty())
			{
				IEntityWrapper referenceValue = this.ReferenceValue;
				if (referenceValue.ObjectStateEntry != null && referenceValue.ObjectStateEntry.State == EntityState.Added)
				{
					return true;
				}
				entityKey = this.ExtractPrincipalKey(referenceValue);
			}
			else
			{
				if ((this.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne && this.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.One) || !(this.DetachedEntityKey != null))
				{
					return true;
				}
				if (base.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking)
				{
					entityKey = this.EntityKey;
				}
				else
				{
					entityKey = this.DetachedEntityKey;
				}
			}
			return RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(ownerEntry.GetCurrentEntityValue), entityKey);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000B7A48 File Offset: 0x000B5C48
		internal override bool CheckReferentialConstraintDependentProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			if (!this.IsEmpty())
			{
				return base.CheckReferentialConstraintDependentProperty(ownerEntry, constraint);
			}
			if ((this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One) && this.DetachedEntityKey != null)
			{
				EntityKey detachedEntityKey = this.DetachedEntityKey;
				if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(detachedEntityKey.FindValueByName), ownerEntry.EntityKey))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000B7AB4 File Offset: 0x000B5CB4
		private EntityKey ExtractPrincipalKey(IEntityWrapper wrappedRelatedEntity)
		{
			EntitySet targetEntitySetFromRelationshipSet = base.GetTargetEntitySetFromRelationshipSet();
			EntityKey entityKey = wrappedRelatedEntity.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, targetEntitySetFromRelationshipSet);
				entityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, targetEntitySetFromRelationshipSet);
			}
			else
			{
				entityKey = this.ObjectContext.ObjectStateManager.CreateEntityKey(targetEntitySetFromRelationshipSet, wrappedRelatedEntity.Entity);
			}
			return entityKey;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000B7B10 File Offset: 0x000B5D10
		internal void NullAllForeignKeys()
		{
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			EntityEntry objectStateEntry = this.WrappedOwner.ObjectStateEntry;
			TransactionManager transactionManager = objectStateManager.TransactionManager;
			if (!transactionManager.IsGraphUpdate && !transactionManager.IsAttachTracking && !transactionManager.IsRelatedEndAdd)
			{
				ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints.Single<ReferentialConstraint>();
				if (this.TargetRoleName == referentialConstraint.FromRole.Name)
				{
					if (transactionManager.IsDetaching)
					{
						EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(objectStateEntry, this);
						if (entityKey != null)
						{
							objectStateManager.AddEntryContainingForeignKeyToIndex(this, entityKey, objectStateEntry);
							return;
						}
					}
					else if (objectStateManager.EntityInvokingFKSetter != this.WrappedOwner.Entity && !transactionManager.IsForeignKeyUpdate)
					{
						transactionManager.BeginForeignKeyUpdate(this);
						try
						{
							bool flag = true;
							bool flag2 = objectStateEntry != null && (objectStateEntry.State == EntityState.Modified || objectStateEntry.State == EntityState.Unchanged);
							EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
							StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(this.WrappedOwner.IdentityType, entitySet);
							for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
							{
								string name = referentialConstraint.ToProperties[i].Name;
								int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(name);
								StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName);
								if (stateManagerMemberMetadata.ClrMetadata.Nullable)
								{
									if (stateManagerMemberMetadata.GetValue(this.WrappedOwner.Entity) != null)
									{
										this.WrappedOwner.SetCurrentValue(this.WrappedOwner.ObjectStateEntry, orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName), -1, this.WrappedOwner.Entity, null);
									}
									else if (flag2 && this.WrappedOwner.ObjectStateEntry.OriginalValues.GetValue(ordinalforOLayerMemberName) != null)
									{
										objectStateEntry.SetModifiedProperty(name);
									}
									flag = false;
								}
								else if (flag2)
								{
									objectStateEntry.SetModifiedProperty(name);
								}
							}
							if (flag)
							{
								if (objectStateEntry != null)
								{
									EntityKey entityKey2 = this.CachedForeignKey;
									if (entityKey2 == null)
									{
										entityKey2 = ForeignKeyFactory.CreateKeyFromForeignKeyValues(objectStateEntry, this);
									}
									if (entityKey2 != null)
									{
										this.SetCachedForeignKey(ForeignKeyFactory.CreateConceptualNullKey(entityKey2), objectStateEntry);
										objectStateManager.RememberEntryWithConceptualNull(objectStateEntry);
									}
								}
							}
							else
							{
								this.SetCachedForeignKey(null, objectStateEntry);
							}
						}
						finally
						{
							transactionManager.EndForeignKeyUpdate();
						}
					}
				}
			}
		}

		// Token: 0x040012DF RID: 4831
		private EntityKey _detachedEntityKey;

		// Token: 0x040012E0 RID: 4832
		[NonSerialized]
		private EntityKey _cachedForeignKey;
	}
}
