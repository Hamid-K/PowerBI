using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000406 RID: 1030
	internal sealed class EntityEntry : ObjectStateEntry
	{
		// Token: 0x0600300C RID: 12300 RVA: 0x00097C40 File Offset: 0x00095E40
		internal EntityEntry()
			: base(new ObjectStateManager(), null, EntityState.Unchanged)
		{
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00097C4F File Offset: 0x00095E4F
		internal EntityEntry(ObjectStateManager stateManager)
			: base(stateManager, null, EntityState.Unchanged)
		{
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00097C5A File Offset: 0x00095E5A
		internal EntityEntry(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata, EntityState state)
			: base(cache, entitySet, state)
		{
			this._wrappedEntity = wrappedEntity;
			this._cacheTypeMetadata = typeMetadata;
			this._entityKey = entityKey;
			wrappedEntity.ObjectStateEntry = this;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00097C8C File Offset: 0x00095E8C
		private void SetChangeTrackingFlags()
		{
			this._requiresScalarChangeTracking = this.Entity != null && !(this.Entity is IEntityWithChangeTracker);
			bool flag;
			if (this.Entity != null)
			{
				if (!this._requiresScalarChangeTracking)
				{
					if (this.WrappedEntity.IdentityType != this.Entity.GetType())
					{
						flag = this._cacheTypeMetadata.Members.Any((StateManagerMemberMetadata m) => m.IsComplex);
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
			this._requiresComplexChangeTracking = flag;
			this._requiresAnyChangeTracking = this.Entity != null && (!(this.Entity is IEntityWithRelationships) || this._requiresComplexChangeTracking || this._requiresScalarChangeTracking);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00097D55 File Offset: 0x00095F55
		internal EntityEntry(EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata)
			: base(cache, entitySet, EntityState.Unchanged)
		{
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this._entityKey = entityKey;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x00097D80 File Offset: 0x00095F80
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return false;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x00097D89 File Offset: 0x00095F89
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return this._wrappedEntity.Entity;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x00097D9C File Offset: 0x00095F9C
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x00097DAA File Offset: 0x00095FAA
		public override EntityKey EntityKey
		{
			get
			{
				base.ValidateState();
				return this._entityKey;
			}
			internal set
			{
				this._entityKey = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x00097DB3 File Offset: 0x00095FB3
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyDependents
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in ((EntitySet)base.EntitySet).ForeignKeyDependents)
				{
					if (MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)tuple.Item2.ToRole).IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return tuple;
					}
				}
				IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x00097DC3 File Offset: 0x00095FC3
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyPrincipals
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in ((EntitySet)base.EntitySet).ForeignKeyPrincipals)
				{
					if (MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)tuple.Item2.FromRole).IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return tuple;
					}
				}
				IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00097DD3 File Offset: 0x00095FD3
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			if (EntityState.Modified == base.State && this._modifiedFields != null)
			{
				int num;
				for (int i = 0; i < this._modifiedFields.Length; i = num + 1)
				{
					if (this._modifiedFields[i])
					{
						yield return this.GetCLayerName(i, this._cacheTypeMetadata);
					}
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00097DE4 File Offset: 0x00095FE4
		public override void SetModifiedProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			int num = this.ValidateAndGetOrdinalForProperty(propertyName, "SetModifiedProperty");
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
			}
			this.SetModifiedPropertyInternal(num);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00097E35 File Offset: 0x00096035
		internal void SetModifiedPropertyInternal(int ordinal)
		{
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields[ordinal] = true;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00097E64 File Offset: 0x00096064
		private int ValidateAndGetOrdinalForProperty(string propertyName, string methodName)
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
			}
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(propertyName);
			if (ordinalforOLayerMemberName == -1)
			{
				throw new ArgumentException(Strings.ObjectStateEntry_SetModifiedOnInvalidProperty(propertyName));
			}
			if (base.State == EntityState.Added || base.State == EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetModifiedStates(methodName));
			}
			return ordinalforOLayerMemberName;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x00097EC4 File Offset: 0x000960C4
		public override void RejectPropertyChanges(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			int num = this.ValidateAndGetOrdinalForProperty(propertyName, "RejectPropertyChanges");
			if (base.State == EntityState.Unchanged)
			{
				return;
			}
			if (this._modifiedFields != null && this._modifiedFields[num])
			{
				this.DetectChangesInComplexProperties();
				object originalEntityValue = this.GetOriginalEntityValue(this._cacheTypeMetadata, num, this._wrappedEntity.Entity, ObjectStateValueRecord.OriginalReadonly);
				this.SetCurrentEntityValue(this._cacheTypeMetadata, num, this._wrappedEntity.Entity, originalEntityValue);
				this._modifiedFields[num] = false;
				for (int i = 0; i < this._modifiedFields.Length; i++)
				{
					if (this._modifiedFields[i])
					{
						return;
					}
				}
				this.ChangeObjectState(EntityState.Unchanged);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x00097F7F File Offset: 0x0009617F
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				return this.InternalGetOriginalValues(true);
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x00097F88 File Offset: 0x00096188
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			return (OriginalValueRecord)this.InternalGetOriginalValues(false);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00097F98 File Offset: 0x00096198
		private DbDataRecord InternalGetOriginalValues(bool readOnly)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
			}
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.DetectChangesInComplexProperties();
			if (readOnly)
			{
				return new ObjectStateEntryDbDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
			return new ObjectStateEntryOriginalDbUpdatableDataRecord_Public(this, this._cacheTypeMetadata, this._wrappedEntity.Entity, -1);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x0009800C File Offset: 0x0009620C
		private void DetectChangesInComplexProperties()
		{
			if (this.RequiresScalarChangeTracking)
			{
				base.ObjectStateManager.TransactionManager.BeginOriginalValuesGetter();
				try
				{
					this.DetectChangesInProperties(true);
				}
				finally
				{
					base.ObjectStateManager.TransactionManager.EndOriginalValuesGetter();
				}
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x0009805C File Offset: 0x0009625C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override CurrentValueRecord CurrentValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_CurrentValuesDoesNotExist);
				}
				if (this.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
				}
				return new ObjectStateEntryDbUpdatableDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000980AD File Offset: 0x000962AD
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000980B8 File Offset: 0x000962B8
		public override void AcceptChanges()
		{
			base.ValidateState();
			if (base.ObjectStateManager.EntryHasConceptualNull(this))
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					bool flag = this.RetrieveAndCheckReferentialConstraintValuesInAcceptChanges();
					this._cache.FixupKey(this);
					this._modifiedFields = null;
					this._originalValues = null;
					this._originalComplexObjects = null;
					base.State = EntityState.Unchanged;
					if (flag)
					{
						this.RelationshipManager.CheckReferentialConstraintProperties(this);
					}
					this._wrappedEntity.TakeSnapshot(this);
					return;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state != EntityState.Modified)
				{
					return;
				}
				this._cache.ChangeState(this, EntityState.Modified, EntityState.Unchanged);
				this._modifiedFields = null;
				this._originalValues = null;
				this._originalComplexObjects = null;
				base.State = EntityState.Unchanged;
				this._cache.FixupReferencesByForeignKeys(this, false);
				this.RelationshipManager.CheckReferentialConstraintProperties(this);
				this._wrappedEntity.TakeSnapshot(this);
			}
			else
			{
				this.CascadeAcceptChanges();
				if (this._cache != null)
				{
					this._cache.ChangeState(this, EntityState.Deleted, EntityState.Detached);
					return;
				}
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000981C0 File Offset: 0x000963C0
		public override void SetModified()
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
			}
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
				return;
			}
			if (EntityState.Modified != base.State)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetModifiedStates("SetModified"));
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x00098225 File Offset: 0x00096425
		public override RelationshipManager RelationshipManager
		{
			get
			{
				base.ValidateState();
				if (this.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers);
				}
				if (this.WrappedEntity.Entity == null)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity);
				}
				return this.WrappedEntity.RelationshipManager;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x00098263 File Offset: 0x00096463
		internal override BitArray ModifiedProperties
		{
			get
			{
				return this._modifiedFields;
			}
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0009826C File Offset: 0x0009646C
		public override void ChangeState(EntityState state)
		{
			EntityUtil.CheckValidStateForChangeEntityState(state);
			if (base.State == EntityState.Detached && state == EntityState.Detached)
			{
				return;
			}
			base.ValidateState();
			ObjectStateManager objectStateManager = base.ObjectStateManager;
			objectStateManager.TransactionManager.BeginLocalPublicAPI();
			try
			{
				this.ChangeObjectState(state);
			}
			finally
			{
				objectStateManager.TransactionManager.EndLocalPublicAPI();
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000982CC File Offset: 0x000964CC
		public override void ApplyCurrentValues(object currentEntity)
		{
			Check.NotNull<object>(currentEntity, "currentEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			IEntityWrapper entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(currentEntity, base.ObjectStateManager);
			this.ApplyCurrentValuesInternal(entityWrapper);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00098320 File Offset: 0x00096520
		public override void ApplyOriginalValues(object originalEntity)
		{
			Check.NotNull<object>(originalEntity, "originalEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			IEntityWrapper entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(originalEntity, base.ObjectStateManager);
			this.ApplyOriginalValuesInternal(entityWrapper);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00098371 File Offset: 0x00096571
		internal void AddRelationshipEnd(RelationshipEntry item)
		{
			item.SetNextRelationshipEnd(this.EntityKey, this._headRelationshipEnds);
			this._headRelationshipEnds = item;
			this._countRelationshipEnds++;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0009839C File Offset: 0x0009659C
		internal bool ContainsRelationshipEnd(RelationshipEntry item)
		{
			for (RelationshipEntry relationshipEntry = this._headRelationshipEnds; relationshipEntry != null; relationshipEntry = relationshipEntry.GetNextRelationshipEnd(this.EntityKey))
			{
				if (relationshipEntry == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000983CC File Offset: 0x000965CC
		internal void RemoveRelationshipEnd(RelationshipEntry item)
		{
			RelationshipEntry relationshipEntry = this._headRelationshipEnds;
			RelationshipEntry relationshipEntry2 = null;
			bool flag = false;
			while (relationshipEntry != null)
			{
				bool flag2 = this.EntityKey == relationshipEntry.Key0 || (this.EntityKey != relationshipEntry.Key1 && this.EntityKey.Equals(relationshipEntry.Key0));
				if (item == relationshipEntry)
				{
					RelationshipEntry relationshipEntry3;
					if (flag2)
					{
						relationshipEntry3 = relationshipEntry.NextKey0;
						relationshipEntry.NextKey0 = null;
					}
					else
					{
						relationshipEntry3 = relationshipEntry.NextKey1;
						relationshipEntry.NextKey1 = null;
					}
					if (relationshipEntry2 == null)
					{
						this._headRelationshipEnds = relationshipEntry3;
					}
					else if (flag)
					{
						relationshipEntry2.NextKey0 = relationshipEntry3;
					}
					else
					{
						relationshipEntry2.NextKey1 = relationshipEntry3;
					}
					this._countRelationshipEnds--;
					return;
				}
				relationshipEntry2 = relationshipEntry;
				relationshipEntry = (flag2 ? relationshipEntry.NextKey0 : relationshipEntry.NextKey1);
				flag = flag2;
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x00098494 File Offset: 0x00096694
		internal void UpdateRelationshipEnds(EntityKey oldKey, EntityEntry promotedEntry)
		{
			int num = 0;
			RelationshipEntry relationshipEntry = this._headRelationshipEnds;
			while (relationshipEntry != null)
			{
				RelationshipEntry relationshipEntry2 = relationshipEntry;
				relationshipEntry = relationshipEntry.GetNextRelationshipEnd(oldKey);
				relationshipEntry2.ChangeRelatedEnd(oldKey, this.EntityKey);
				if (promotedEntry != null && !promotedEntry.ContainsRelationshipEnd(relationshipEntry2))
				{
					promotedEntry.AddRelationshipEnd(relationshipEntry2);
				}
				num++;
			}
			if (promotedEntry != null)
			{
				this._headRelationshipEnds = null;
				this._countRelationshipEnds = 0;
			}
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000984EE File Offset: 0x000966EE
		internal EntityEntry.RelationshipEndEnumerable GetRelationshipEnds()
		{
			return new EntityEntry.RelationshipEndEnumerable(this);
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000984F6 File Offset: 0x000966F6
		internal override bool IsKeyEntry
		{
			get
			{
				return this._wrappedEntity.Entity == null;
			}
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x00098506 File Offset: 0x00096706
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			if (Helper.IsEntityType(metadata.CdmMetadata.EdmType) && this._entityKey != null)
			{
				return new EntityRecordInfo(metadata.DataRecordInfo, this._entityKey, (EntitySet)base.EntitySet);
			}
			return metadata.DataRecordInfo;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x00098548 File Offset: 0x00096748
		internal override void Reset()
		{
			this.RemoveFromForeignKeyIndex();
			this._cache.ForgetEntryWithConceptualNull(this, true);
			this.DetachObjectStateManagerFromEntity();
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this._entityKey = null;
			this._modifiedFields = null;
			this._originalValues = null;
			this._originalComplexObjects = null;
			this.SetChangeTrackingFlags();
			base.Reset();
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000985A1 File Offset: 0x000967A1
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.GetFieldType(ordinal);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000985AA File Offset: 0x000967AA
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.CLayerMemberName(ordinal);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000985B3 File Offset: 0x000967B3
		internal override int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata)
		{
			return metadata.GetOrdinalforCLayerMemberName(name);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000985BC File Offset: 0x000967BC
		internal override void RevertDelete()
		{
			base.State = ((this._modifiedFields == null) ? EntityState.Unchanged : EntityState.Modified);
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000985E4 File Offset: 0x000967E4
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return metadata.FieldCount;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000985EC File Offset: 0x000967EC
		private void CascadeAcceptChanges()
		{
			RelationshipEntry[] array = this._cache.CopyOfRelationshipsByKey(this.EntityKey);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AcceptChanges();
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x00098621 File Offset: 0x00096821
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields.SetAll(true);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00098654 File Offset: 0x00096854
		internal override void EntityMemberChanging(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanging(entityMemberName, null, null);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00098672 File Offset: 0x00096872
		internal override void EntityMemberChanged(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanged(entityMemberName, null, null);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00098690 File Offset: 0x00096890
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000986AE File Offset: 0x000968AE
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000986CC File Offset: 0x000968CC
		internal IEntityWrapper WrappedEntity
		{
			get
			{
				return this._wrappedEntity;
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000986D4 File Offset: 0x000968D4
		private void EntityMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			try
			{
				StateManagerTypeMetadata stateManagerTypeMetadata;
				string text;
				object obj;
				int andValidateChangeMemberInfo = this.GetAndValidateChangeMemberInfo(entityMemberName, complexObject, complexObjectMemberName, out stateManagerTypeMetadata, out text, out obj);
				if (andValidateChangeMemberInfo != -2)
				{
					if (obj != this._cache.ChangingObject || text != this._cache.ChangingMember || entityMemberName != this._cache.ChangingEntityMember)
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging);
					}
					if (base.State != this._cache.ChangingState)
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_ChangedInDifferentStateFromChanging(this._cache.ChangingState, base.State));
					}
					object changingOldValue = this._cache.ChangingOldValue;
					object obj2 = null;
					StateManagerMemberMetadata stateManagerMemberMetadata = null;
					if (this._cache.SaveOriginalValues)
					{
						stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
						if (stateManagerMemberMetadata.IsComplex && changingOldValue != null)
						{
							obj2 = stateManagerMemberMetadata.GetValue(obj);
							this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, changingOldValue, obj2, false);
						}
						else
						{
							this.AddOriginalValueAt(-1, stateManagerMemberMetadata, obj, changingOldValue);
						}
					}
					TransactionManager transactionManager = base.ObjectStateManager.TransactionManager;
					List<Pair<string, string>> list;
					if (complexObject == null && (transactionManager.IsAlignChanges || !transactionManager.IsDetectChanges) && this.IsPropertyAForeignKey(entityMemberName, out list))
					{
						foreach (Pair<string, string> pair in list)
						{
							string first = pair.First;
							string second = pair.Second;
							EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(first, second) as EntityReference;
							if (!transactionManager.IsFixupByReference)
							{
								if (stateManagerMemberMetadata == null)
								{
									stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
								}
								if (obj2 == null)
								{
									obj2 = stateManagerMemberMetadata.GetValue(obj);
								}
								bool flag = ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey);
								if (!ByValueEqualityComparer.Default.Equals(changingOldValue, obj2) || flag)
								{
									this.FixupEntityReferenceByForeignKey(entityReference);
								}
							}
						}
					}
					if (this._cache != null && !this._cache.TransactionManager.IsOriginalValuesGetter)
					{
						EntityState state = base.State;
						if (base.State != EntityState.Added)
						{
							base.State = EntityState.Modified;
						}
						if (base.State == EntityState.Modified)
						{
							this.SetModifiedProperty(entityMemberName);
						}
						if (state != base.State)
						{
							this._cache.ChangeState(this, state, base.State);
						}
					}
				}
			}
			finally
			{
				this.SetCachedChangingValues(null, null, null, EntityState.Detached, null);
			}
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x00098954 File Offset: 0x00096B54
		internal void SetCurrentEntityValue(string memberName, object newValue)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			this.SetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, newValue);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x00098988 File Offset: 0x00096B88
		internal void SetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
			}
			int state = (int)base.State;
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, userObject);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (num >= 0)
				{
					this._originalValues.RemoveAt(num);
				}
				object value = stateManagerMemberMetadata.GetValue(userObject);
				if (value == null)
				{
					throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName));
				}
				IExtendedDataRecord extendedDataRecord = newValue as IExtendedDataRecord;
				if (extendedDataRecord != null)
				{
					newValue = this._cache.ComplexTypeMaterializer.CreateComplex(extendedDataRecord, extendedDataRecord.DataRecordInfo, null);
				}
				this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, value, newValue, true);
			}
			else
			{
				this.AddOriginalValueAt(num, stateManagerMemberMetadata, userObject, newValue);
			}
			if (state == 2)
			{
				base.State = EntityState.Modified;
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00098A44 File Offset: 0x00096C44
		private void EntityMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata;
			string text;
			object obj;
			int andValidateChangeMemberInfo = this.GetAndValidateChangeMemberInfo(entityMemberName, complexObject, complexObjectMemberName, out stateManagerTypeMetadata, out text, out obj);
			if (andValidateChangeMemberInfo == -2)
			{
				return;
			}
			StateManagerMemberMetadata stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
			this._cache.SaveOriginalValues = (base.State == EntityState.Unchanged || base.State == EntityState.Modified) && this.FindOriginalValueIndex(stateManagerMemberMetadata, obj) == -1;
			object value = stateManagerMemberMetadata.GetValue(obj);
			this.SetCachedChangingValues(entityMemberName, obj, text, base.State, value);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x00098AB8 File Offset: 0x00096CB8
		internal object GetOriginalEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetOriginalEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00098AEB File Offset: 0x00096CEB
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetOriginalEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x00098AF9 File Offset: 0x00096CF9
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			return this.GetOriginalEntityValue(metadata, metadata.Member(ordinal), ordinal, userObject, updatableRecord, parentEntityPropertyIndex);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x00098B18 File Offset: 0x00096D18
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, StateManagerMemberMetadata memberMetadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			int num = this.FindOriginalValueIndex(memberMetadata, userObject);
			if (num >= 0)
			{
				return this._originalValues[num].OriginalValue ?? DBNull.Value;
			}
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, parentEntityPropertyIndex);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x00098B5C File Offset: 0x00096D5C
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x00098B6C File Offset: 0x00096D6C
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			object obj = stateManagerMemberMetadata.GetValue(userObject);
			if (stateManagerMemberMetadata.IsComplex && obj != null)
			{
				switch (updatableRecord)
				{
				case ObjectStateValueRecord.OriginalReadonly:
					obj = new ObjectStateEntryDbDataRecord(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
					break;
				case ObjectStateValueRecord.CurrentUpdatable:
					obj = new ObjectStateEntryDbUpdatableDataRecord(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
					break;
				case ObjectStateValueRecord.OriginalUpdatableInternal:
					obj = new ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
					break;
				case ObjectStateValueRecord.OriginalUpdatablePublic:
					obj = new ObjectStateEntryOriginalDbUpdatableDataRecord_Public(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj, parentEntityPropertyIndex);
					break;
				}
			}
			return obj ?? DBNull.Value;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x00098C5C File Offset: 0x00096E5C
		internal int FindOriginalValueIndex(StateManagerMemberMetadata metadata, object instance)
		{
			if (this._originalValues != null)
			{
				for (int i = 0; i < this._originalValues.Count; i++)
				{
					if (this._originalValues[i].UserObject == instance && this._originalValues[i].MemberMetadata == metadata)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00098CB2 File Offset: 0x00096EB2
		internal AssociationEndMember GetAssociationEndMember(RelationshipEntry relationshipEntry)
		{
			base.ValidateState();
			return relationshipEntry.RelationshipWrapper.GetAssociationEndMember(this.EntityKey);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x00098CCB File Offset: 0x00096ECB
		internal EntityEntry GetOtherEndOfRelationship(RelationshipEntry relationshipEntry)
		{
			return this._cache.GetEntityEntry(relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey));
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x00098CEC File Offset: 0x00096EEC
		internal void ExpandComplexTypeAndAddValues(StateManagerMemberMetadata memberMetadata, object oldComplexObject, object newComplexObject, bool useOldComplexObject)
		{
			if (newComplexObject == null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(memberMetadata.CLayerName));
			}
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(memberMetadata.CdmMetadata.TypeUsage.EdmType);
			for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
				if (stateManagerMemberMetadata.IsComplex)
				{
					object obj = null;
					if (oldComplexObject != null)
					{
						obj = stateManagerMemberMetadata.GetValue(oldComplexObject);
						if (obj == null)
						{
							int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, oldComplexObject);
							if (num >= 0)
							{
								this._originalValues.RemoveAt(num);
							}
						}
					}
					this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, obj, stateManagerMemberMetadata.GetValue(newComplexObject), useOldComplexObject);
				}
				else
				{
					object obj2 = newComplexObject;
					int num2 = -1;
					object obj3;
					if (useOldComplexObject)
					{
						obj3 = stateManagerMemberMetadata.GetValue(newComplexObject);
						obj2 = oldComplexObject;
					}
					else if (oldComplexObject != null)
					{
						obj3 = stateManagerMemberMetadata.GetValue(oldComplexObject);
						num2 = this.FindOriginalValueIndex(stateManagerMemberMetadata, oldComplexObject);
						if (num2 >= 0)
						{
							obj3 = this._originalValues[num2].OriginalValue;
						}
					}
					else
					{
						obj3 = stateManagerMemberMetadata.GetValue(newComplexObject);
					}
					this.AddOriginalValueAt(num2, stateManagerMemberMetadata, obj2, obj3);
				}
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x00098DF0 File Offset: 0x00096FF0
		internal int GetAndValidateChangeMemberInfo(string entityMemberName, object complexObject, string complexObjectMemberName, out StateManagerTypeMetadata typeMetadata, out string changingMemberName, out object changingObject)
		{
			Check.NotNull<string>(entityMemberName, "entityMemberName");
			typeMetadata = null;
			changingMemberName = null;
			changingObject = null;
			base.ValidateState();
			int num = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(entityMemberName);
			if (num != -1)
			{
				StateManagerTypeMetadata stateManagerTypeMetadata;
				string text;
				object obj;
				if (complexObject != null)
				{
					if (!this._cacheTypeMetadata.Member(num).IsComplex)
					{
						throw new ArgumentException(Strings.ComplexObject_ComplexChangeRequestedOnScalarProperty(entityMemberName));
					}
					stateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(complexObject.GetType(), (EntitySet)base.EntitySet);
					num = stateManagerTypeMetadata.GetOrdinalforOLayerMemberName(complexObjectMemberName);
					if (num == -1)
					{
						throw new ArgumentException(Strings.ObjectStateEntry_ChangeOnUnmappedComplexProperty(complexObjectMemberName));
					}
					text = complexObjectMemberName;
					obj = complexObject;
				}
				else
				{
					stateManagerTypeMetadata = this._cacheTypeMetadata;
					text = entityMemberName;
					obj = this.Entity;
					if (this.WrappedEntity.IdentityType != this.Entity.GetType() && this.Entity is IEntityWithChangeTracker && this.IsPropertyAForeignKey(entityMemberName))
					{
						this._cache.EntityInvokingFKSetter = this.WrappedEntity.Entity;
					}
				}
				this.VerifyEntityValueIsEditable(stateManagerTypeMetadata, num, text);
				typeMetadata = stateManagerTypeMetadata;
				changingMemberName = text;
				changingObject = obj;
				return num;
			}
			if (!(entityMemberName == "-EntityKey-"))
			{
				throw new ArgumentException(Strings.ObjectStateEntry_ChangeOnUnmappedProperty(entityMemberName));
			}
			if (!this._cache.InRelationshipFixup)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantSetEntityKey);
			}
			this.SetCachedChangingValues(null, null, null, base.State, null);
			return -2;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x00098F3C File Offset: 0x0009713C
		private void SetCachedChangingValues(string entityMemberName, object changingObject, string changingMember, EntityState changingState, object oldValue)
		{
			this._cache.ChangingEntityMember = entityMemberName;
			this._cache.ChangingObject = changingObject;
			this._cache.ChangingMember = changingMember;
			this._cache.ChangingState = changingState;
			this._cache.ChangingOldValue = oldValue;
			if (changingState == EntityState.Detached)
			{
				this._cache.SaveOriginalValues = false;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x00098F98 File Offset: 0x00097198
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal OriginalValueRecord EditableOriginalValues
		{
			get
			{
				return new ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x00098FB4 File Offset: 0x000971B4
		internal void DetachObjectStateManagerFromEntity()
		{
			if (!this.IsKeyEntry)
			{
				this._wrappedEntity.SetChangeTracker(null);
				this._wrappedEntity.DetachContext();
				if (this._cache.TransactionManager.IsAttachTracking)
				{
					MergeOption? originalMergeOption = this._cache.TransactionManager.OriginalMergeOption;
					MergeOption mergeOption = MergeOption.NoTracking;
					if ((originalMergeOption.GetValueOrDefault() == mergeOption) & (originalMergeOption != null))
					{
						return;
					}
				}
				this._wrappedEntity.EntityKey = null;
			}
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x00099028 File Offset: 0x00097228
		internal void TakeSnapshot(bool onlySnapshotComplexProperties)
		{
			if (base.State != EntityState.Added)
			{
				StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
				int fieldCount = this.GetFieldCount(cacheTypeMetadata);
				for (int i = 0; i < fieldCount; i++)
				{
					StateManagerMemberMetadata stateManagerMemberMetadata = cacheTypeMetadata.Member(i);
					if (stateManagerMemberMetadata.IsComplex)
					{
						object obj = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
						this.AddComplexObjectSnapshot(this.Entity, i, obj);
						this.TakeSnapshotOfComplexType(stateManagerMemberMetadata, obj);
					}
					else if (!onlySnapshotComplexProperties)
					{
						object obj = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
						this.AddOriginalValueAt(-1, stateManagerMemberMetadata, this._wrappedEntity.Entity, obj);
					}
				}
			}
			this.TakeSnapshotOfForeignKeys();
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000990CC File Offset: 0x000972CC
		internal void TakeSnapshotOfForeignKeys()
		{
			Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
			this.FindRelatedEntityKeysByForeignKeys(out dictionary, false);
			if (dictionary != null)
			{
				foreach (KeyValuePair<RelatedEnd, HashSet<EntityKey>> keyValuePair in dictionary)
				{
					EntityReference entityReference = keyValuePair.Key as EntityReference;
					if (!ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						entityReference.SetCachedForeignKey(keyValuePair.Value.First<EntityKey>(), this);
					}
				}
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0009914C File Offset: 0x0009734C
		private void TakeSnapshotOfComplexType(StateManagerMemberMetadata member, object complexValue)
		{
			if (complexValue == null)
			{
				return;
			}
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(member.CdmMetadata.TypeUsage.EdmType);
			for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
				object value = stateManagerMemberMetadata.GetValue(complexValue);
				if (stateManagerMemberMetadata.IsComplex)
				{
					this.AddComplexObjectSnapshot(complexValue, i, value);
					this.TakeSnapshotOfComplexType(stateManagerMemberMetadata, value);
				}
				else if (this.FindOriginalValueIndex(stateManagerMemberMetadata, complexValue) == -1)
				{
					this.AddOriginalValueAt(-1, stateManagerMemberMetadata, complexValue, value);
				}
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000991CC File Offset: 0x000973CC
		private void AddComplexObjectSnapshot(object userObject, int ordinal, object complexObject)
		{
			if (complexObject == null)
			{
				return;
			}
			this.CheckForDuplicateComplexObjects(complexObject);
			if (this._originalComplexObjects == null)
			{
				this._originalComplexObjects = new Dictionary<object, Dictionary<int, object>>(ObjectReferenceEqualityComparer.Default);
			}
			Dictionary<int, object> dictionary;
			if (!this._originalComplexObjects.TryGetValue(userObject, out dictionary))
			{
				dictionary = new Dictionary<int, object>();
				this._originalComplexObjects.Add(userObject, dictionary);
			}
			dictionary.Add(ordinal, complexObject);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x00099228 File Offset: 0x00097428
		private void CheckForDuplicateComplexObjects(object complexObject)
		{
			if (this._originalComplexObjects == null || complexObject == null)
			{
				return;
			}
			foreach (Dictionary<int, object> dictionary in this._originalComplexObjects.Values)
			{
				foreach (object obj in dictionary.Values)
				{
					if (complexObject == obj)
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_ComplexObjectUsedMultipleTimes(this.Entity.GetType().FullName, complexObject.GetType().FullName));
					}
				}
			}
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000992E8 File Offset: 0x000974E8
		public override bool IsPropertyChanged(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return this.DetectChangesInProperty(this.ValidateAndGetOrdinalForProperty(propertyName, "IsPropertyChanged"), false, true);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0009930C File Offset: 0x0009750C
		private bool DetectChangesInProperty(int ordinal, bool detectOnlyComplexProperties, bool detectOnly)
		{
			bool flag = false;
			StateManagerMemberMetadata stateManagerMemberMetadata = this._cacheTypeMetadata.Member(ordinal);
			object value = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (base.State != EntityState.Deleted)
				{
					object complexObjectSnapshot = this.GetComplexObjectSnapshot(this.Entity, ordinal);
					if (this.DetectChangesInComplexType(stateManagerMemberMetadata, stateManagerMemberMetadata, value, complexObjectSnapshot, ref flag, detectOnly))
					{
						this.CheckForDuplicateComplexObjects(value);
						if (!detectOnly)
						{
							((IEntityChangeTracker)this).EntityMemberChanging(stateManagerMemberMetadata.CLayerName);
							this._cache.ChangingOldValue = complexObjectSnapshot;
							((IEntityChangeTracker)this).EntityMemberChanged(stateManagerMemberMetadata.CLayerName);
						}
						this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, this.Entity, ordinal, value);
						if (!flag)
						{
							this.DetectChangesInComplexType(stateManagerMemberMetadata, stateManagerMemberMetadata, value, complexObjectSnapshot, ref flag, detectOnly);
						}
					}
				}
			}
			else if (!detectOnlyComplexProperties)
			{
				int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, this._wrappedEntity.Entity);
				if (num < 0)
				{
					return this.GetModifiedProperties().Contains(stateManagerMemberMetadata.CLayerName);
				}
				object originalValue = this._originalValues[num].OriginalValue;
				if (!object.Equals(value, originalValue))
				{
					flag = true;
					if (stateManagerMemberMetadata.IsPartOfKey)
					{
						if (!ByValueEqualityComparer.Default.Equals(value, originalValue))
						{
							throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName));
						}
					}
					else if (base.State != EntityState.Deleted && !detectOnly)
					{
						((IEntityChangeTracker)this).EntityMemberChanging(stateManagerMemberMetadata.CLayerName);
						((IEntityChangeTracker)this).EntityMemberChanged(stateManagerMemberMetadata.CLayerName);
					}
				}
			}
			return flag;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x00099468 File Offset: 0x00097668
		internal void DetectChangesInProperties(bool detectOnlyComplexProperties)
		{
			int fieldCount = this.GetFieldCount(this._cacheTypeMetadata);
			for (int i = 0; i < fieldCount; i++)
			{
				this.DetectChangesInProperty(i, detectOnlyComplexProperties, false);
			}
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x00099498 File Offset: 0x00097698
		private bool DetectChangesInComplexType(StateManagerMemberMetadata topLevelMember, StateManagerMemberMetadata complexMember, object complexValue, object oldComplexValue, ref bool changeDetected, bool detectOnly)
		{
			if (complexValue == null)
			{
				if (oldComplexValue == null)
				{
					return false;
				}
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(complexMember.CLayerName));
			}
			else
			{
				if (oldComplexValue != complexValue)
				{
					return true;
				}
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(complexMember.CdmMetadata.TypeUsage.EdmType);
				for (int i = 0; i < this.GetFieldCount(orAddStateManagerTypeMetadata); i++)
				{
					StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
					object value = stateManagerMemberMetadata.GetValue(complexValue);
					if (stateManagerMemberMetadata.IsComplex)
					{
						if (base.State != EntityState.Deleted)
						{
							object complexObjectSnapshot = this.GetComplexObjectSnapshot(complexValue, i);
							if (this.DetectChangesInComplexType(topLevelMember, stateManagerMemberMetadata, value, complexObjectSnapshot, ref changeDetected, detectOnly))
							{
								this.CheckForDuplicateComplexObjects(value);
								if (!detectOnly)
								{
									((IEntityChangeTracker)this).EntityComplexMemberChanging(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
									this._cache.ChangingOldValue = complexObjectSnapshot;
									((IEntityChangeTracker)this).EntityComplexMemberChanged(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
								}
								this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, complexValue, i, value);
								if (!changeDetected)
								{
									this.DetectChangesInComplexType(topLevelMember, stateManagerMemberMetadata, value, complexObjectSnapshot, ref changeDetected, detectOnly);
								}
							}
						}
					}
					else
					{
						int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, complexValue);
						object obj = ((num == -1) ? null : this._originalValues[num].OriginalValue);
						if (!object.Equals(value, obj))
						{
							changeDetected = true;
							if (!detectOnly)
							{
								((IEntityChangeTracker)this).EntityComplexMemberChanging(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
								((IEntityChangeTracker)this).EntityComplexMemberChanged(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x00099600 File Offset: 0x00097800
		private object GetComplexObjectSnapshot(object parentObject, int parentOrdinal)
		{
			object obj = null;
			Dictionary<int, object> dictionary;
			if (this._originalComplexObjects != null && this._originalComplexObjects.TryGetValue(parentObject, out dictionary))
			{
				dictionary.TryGetValue(parentOrdinal, out obj);
			}
			return obj;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x00099634 File Offset: 0x00097834
		internal void UpdateComplexObjectSnapshot(StateManagerMemberMetadata member, object userObject, int ordinal, object currentValue)
		{
			bool flag = true;
			Dictionary<int, object> dictionary;
			if (this._originalComplexObjects != null && this._originalComplexObjects.TryGetValue(userObject, out dictionary))
			{
				object obj;
				dictionary.TryGetValue(ordinal, out obj);
				dictionary[ordinal] = currentValue;
				if (obj != null && this._originalComplexObjects.TryGetValue(obj, out dictionary))
				{
					this._originalComplexObjects.Remove(obj);
					this._originalComplexObjects.Add(currentValue, dictionary);
					StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(member.CdmMetadata.TypeUsage.EdmType);
					for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
					{
						StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
						if (stateManagerMemberMetadata.IsComplex)
						{
							object value = stateManagerMemberMetadata.GetValue(currentValue);
							this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, currentValue, i, value);
						}
					}
				}
				flag = false;
			}
			if (flag)
			{
				this.AddComplexObjectSnapshot(userObject, ordinal, currentValue);
			}
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x00099714 File Offset: 0x00097914
		internal void FixupFKValuesFromNonAddedReferences()
		{
			if (!((EntitySet)base.EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				if (entityReference.TargetAccessor.HasProperty)
				{
					object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(entityReference);
					ObjectStateEntry objectStateEntry;
					if (navigationPropertyValue != null && this._cache.TryGetObjectStateEntry(navigationPropertyValue, out objectStateEntry) && (objectStateEntry.State == EntityState.Modified || objectStateEntry.State == EntityState.Unchanged))
					{
						entityReference.UpdateForeignKeyValues(this.WrappedEntity, ((EntityEntry)objectStateEntry).WrappedEntity, dictionary, false);
					}
				}
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0009980C File Offset: 0x00097A0C
		internal void TakeSnapshotOfRelationships()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			foreach (NavigationProperty navigationProperty in (this._cacheTypeMetadata.CdmMetadata.EdmType as EntityType).NavigationProperties)
			{
				RelatedEnd relatedEndInternal = relationshipManager.GetRelatedEndInternal(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name);
				object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(relatedEndInternal);
				if (navigationPropertyValue != null)
				{
					if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						IEnumerable enumerable = navigationPropertyValue as IEnumerable;
						if (enumerable == null)
						{
							throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(navigationProperty.Name, this.Entity.GetType().FullName));
						}
						using (IEnumerator enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								if (obj != null)
								{
									this.TakeSnapshotOfSingleRelationship(relatedEndInternal, navigationProperty, obj);
								}
							}
							continue;
						}
					}
					this.TakeSnapshotOfSingleRelationship(relatedEndInternal, navigationProperty, navigationPropertyValue);
				}
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x00099940 File Offset: 0x00097B40
		private void TakeSnapshotOfSingleRelationship(RelatedEnd relatedEnd, NavigationProperty n, object o)
		{
			EntityEntry entityEntry = base.ObjectStateManager.FindEntityEntry(o);
			IEntityWrapper entityWrapper;
			if (entityEntry != null)
			{
				entityWrapper = entityEntry._wrappedEntity;
				RelatedEnd relatedEndInternal = entityWrapper.RelationshipManager.GetRelatedEndInternal(n.RelationshipType.FullName, n.FromEndMember.Name);
				if (!relatedEndInternal.ContainsEntity(this._wrappedEntity))
				{
					if (entityWrapper.ObjectStateEntry.State == EntityState.Deleted)
					{
						throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
					}
					if (base.ObjectStateManager.TransactionManager.IsAttachTracking && (base.State & (EntityState.Unchanged | EntityState.Modified)) != (EntityState)0 && (entityWrapper.ObjectStateEntry.State & (EntityState.Unchanged | EntityState.Modified)) != (EntityState)0)
					{
						EntityEntry entityEntry2 = null;
						EntityEntry entityEntry3 = null;
						if (relatedEnd.IsDependentEndOfReferentialConstraint(false))
						{
							entityEntry2 = entityWrapper.ObjectStateEntry;
							entityEntry3 = this;
						}
						else if (relatedEndInternal.IsDependentEndOfReferentialConstraint(false))
						{
							entityEntry2 = this;
							entityEntry3 = entityWrapper.ObjectStateEntry;
						}
						if (entityEntry2 != null)
						{
							ReferentialConstraint referentialConstraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints[0];
							if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(entityEntry3.GetCurrentEntityValue), entityEntry2.EntityKey))
							{
								throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
							}
						}
					}
					EntityReference entityReference = relatedEndInternal as EntityReference;
					if (entityReference != null && entityReference.NavigationPropertyIsNullOrMissing())
					{
						base.ObjectStateManager.TransactionManager.AlignedEntityReferences.Add(entityReference);
					}
					relatedEndInternal.AddToLocalCache(this._wrappedEntity, true);
					relatedEndInternal.OnAssociationChanged(CollectionChangeAction.Add, this._wrappedEntity.Entity);
				}
			}
			else if (!base.ObjectStateManager.TransactionManager.WrappedEntities.TryGetValue(o, out entityWrapper))
			{
				entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(o, base.ObjectStateManager);
			}
			if (!relatedEnd.ContainsEntity(entityWrapper))
			{
				relatedEnd.AddToLocalCache(entityWrapper, true);
				relatedEnd.OnAssociationChanged(CollectionChangeAction.Add, entityWrapper.Entity);
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x00099AF4 File Offset: 0x00097CF4
		internal void DetectChangesInRelationshipsOfSingleEntity()
		{
			foreach (NavigationProperty navigationProperty in (this._cacheTypeMetadata.CdmMetadata.EdmType as EntityType).NavigationProperties)
			{
				RelatedEnd relatedEndInternal = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name);
				object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(relatedEndInternal);
				HashSet<object> hashSet = new HashSet<object>(ObjectReferenceEqualityComparer.Default);
				if (navigationPropertyValue != null)
				{
					if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						IEnumerable enumerable = navigationPropertyValue as IEnumerable;
						if (enumerable == null)
						{
							throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(navigationProperty.Name, this.Entity.GetType().FullName));
						}
						using (IEnumerator enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								if (obj != null)
								{
									hashSet.Add(obj);
								}
							}
							goto IL_00F4;
						}
					}
					hashSet.Add(navigationPropertyValue);
				}
				IL_00F4:
				foreach (object obj2 in relatedEndInternal.GetInternalEnumerable())
				{
					if (!hashSet.Contains(obj2))
					{
						this.AddRelationshipDetectedByGraph(base.ObjectStateManager.TransactionManager.DeletedRelationshipsByGraph, obj2, relatedEndInternal, false);
					}
					else
					{
						hashSet.Remove(obj2);
					}
				}
				foreach (object obj3 in hashSet)
				{
					this.AddRelationshipDetectedByGraph(base.ObjectStateManager.TransactionManager.AddedRelationshipsByGraph, obj3, relatedEndInternal, true);
				}
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x00099D28 File Offset: 0x00097F28
		private void AddRelationshipDetectedByGraph(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> relationships, object relatedObject, RelatedEnd relatedEndFrom, bool verifyForAdd)
		{
			IEntityWrapper entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(relatedObject, base.ObjectStateManager);
			EntityEntry.AddDetectedRelationship<IEntityWrapper>(relationships, entityWrapper, relatedEndFrom);
			RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(entityWrapper);
			if (verifyForAdd && otherEndOfRelationship is EntityReference && base.ObjectStateManager.FindEntityEntry(relatedObject) == null)
			{
				otherEndOfRelationship.VerifyNavigationPropertyForAdd(this._wrappedEntity);
			}
			EntityEntry.AddDetectedRelationship<IEntityWrapper>(relationships, this._wrappedEntity, otherEndOfRelationship);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x00099D90 File Offset: 0x00097F90
		private void AddRelationshipDetectedByForeignKey(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> relationships, Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> principalRelationships, EntityKey relatedKey, EntityEntry relatedEntry, RelatedEnd relatedEndFrom)
		{
			EntityEntry.AddDetectedRelationship<EntityKey>(relationships, relatedKey, relatedEndFrom);
			if (relatedEntry != null)
			{
				IEntityWrapper wrappedEntity = relatedEntry.WrappedEntity;
				RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(wrappedEntity);
				EntityKey permanentKey = base.ObjectStateManager.GetPermanentKey(relatedEntry.WrappedEntity, otherEndOfRelationship, this.WrappedEntity);
				EntityEntry.AddDetectedRelationship<EntityKey>(principalRelationships, permanentKey, otherEndOfRelationship);
			}
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x00099DE0 File Offset: 0x00097FE0
		private static void AddDetectedRelationship<T>(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<T>>> relationships, T relatedObject, RelatedEnd relatedEnd)
		{
			Dictionary<RelatedEnd, HashSet<T>> dictionary;
			if (!relationships.TryGetValue(relatedEnd.WrappedOwner, out dictionary))
			{
				dictionary = new Dictionary<RelatedEnd, HashSet<T>>();
				relationships.Add(relatedEnd.WrappedOwner, dictionary);
			}
			HashSet<T> hashSet;
			if (!dictionary.TryGetValue(relatedEnd, out hashSet))
			{
				hashSet = new HashSet<T>();
				dictionary.Add(relatedEnd, hashSet);
			}
			else if (relatedEnd is EntityReference && !object.Equals(hashSet.First<T>(), relatedObject))
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(relatedEnd.RelationshipNavigation.To, relatedEnd.RelationshipNavigation.RelationshipName));
			}
			hashSet.Add(relatedObject);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x00099E74 File Offset: 0x00098074
		internal void Detach()
		{
			base.ValidateState();
			bool flag = false;
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			flag = base.State != EntityState.Added && this.IsOneEndOfSomeRelationship();
			this._cache.TransactionManager.BeginDetaching();
			try
			{
				relationshipManager.DetachEntityFromRelationships(base.State);
			}
			finally
			{
				this._cache.TransactionManager.EndDetaching();
			}
			this.DetachRelationshipsEntries(relationshipManager);
			IEntityWrapper wrappedEntity = this._wrappedEntity;
			EntityKey entityKey = this._entityKey;
			int state = (int)base.State;
			if (flag)
			{
				this.DegradeEntry();
			}
			else
			{
				this._wrappedEntity.ObjectStateEntry = null;
				this._cache.ChangeState(this, base.State, EntityState.Detached);
			}
			if (state != 4)
			{
				wrappedEntity.EntityKey = entityKey;
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x00099F38 File Offset: 0x00098138
		internal void Delete(bool doFixup)
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotDeleteOnKeyEntry);
			}
			if (doFixup && base.State != EntityState.Deleted)
			{
				this.RelationshipManager.NullAllFKsInDependentsForWhichThisIsThePrincipal();
				this.NullAllForeignKeys();
				this.FixupRelationships();
			}
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this._cache.ChangeState(this, EntityState.Added, EntityState.Detached);
					return;
				}
				else
				{
					if (!doFixup)
					{
						this.DeleteRelationshipsThatReferenceKeys(null, null);
					}
					this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
					base.State = EntityState.Deleted;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state != EntityState.Modified)
				{
					return;
				}
				if (!doFixup)
				{
					this.DeleteRelationshipsThatReferenceKeys(null, null);
				}
				this._cache.ChangeState(this, EntityState.Modified, EntityState.Deleted);
				base.State = EntityState.Deleted;
				return;
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x00099FF0 File Offset: 0x000981F0
		private void NullAllForeignKeys()
		{
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				(this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference).NullAllForeignKeys();
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0009A070 File Offset: 0x00098270
		private bool IsOneEndOfSomeRelationship()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				RelationshipMultiplicity relationshipMultiplicity = this.GetAssociationEndMember(relationshipEntry).RelationshipMultiplicity;
				if (relationshipMultiplicity == RelationshipMultiplicity.One || relationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				{
					EntityKey otherEntityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey);
					if (!this._cache.GetEntityEntry(otherEntityKey).IsKeyEntry)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x0009A110 File Offset: 0x00098310
		private void DetachRelationshipsEntries(RelationshipManager relationshipManager)
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				EntityKey otherEntityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey);
				if (this._cache.GetEntityEntry(otherEntityKey).IsKeyEntry)
				{
					if (relationshipEntry.State != EntityState.Deleted)
					{
						AssociationEndMember associationEndMember = relationshipEntry.RelationshipWrapper.GetAssociationEndMember(otherEntityKey);
						((EntityReference)relationshipManager.GetRelatedEndInternal(associationEndMember.DeclaringType.FullName, associationEndMember.Name)).DetachedEntityKey = otherEntityKey;
					}
					relationshipEntry.DeleteUnnecessaryKeyEntries();
					relationshipEntry.DetachRelationshipEntry();
				}
				else if (relationshipEntry.State == EntityState.Deleted && this.GetAssociationEndMember(relationshipEntry).RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					relationshipEntry.DetachRelationshipEntry();
				}
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0009A1D3 File Offset: 0x000983D3
		private void FixupRelationships()
		{
			this._wrappedEntity.RelationshipManager.RemoveEntityFromRelationships();
			this.DeleteRelationshipsThatReferenceKeys(null, null);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0009A1F0 File Offset: 0x000983F0
		internal void DeleteRelationshipsThatReferenceKeys(RelationshipSet relationshipSet, RelationshipEndMember endMember)
		{
			if (base.State != EntityState.Detached)
			{
				foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
				{
					if (relationshipEntry.State != EntityState.Deleted && (relationshipSet == null || relationshipSet == relationshipEntry.EntitySet))
					{
						EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
						if (endMember == null || endMember == otherEndOfRelationship.GetAssociationEndMember(relationshipEntry))
						{
							for (int j = 0; j < 2; j++)
							{
								EntityKey entityKey = relationshipEntry.GetCurrentRelationValue(j) as EntityKey;
								if (entityKey != null && this._cache.GetEntityEntry(entityKey).IsKeyEntry)
								{
									relationshipEntry.Delete(false);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0009A298 File Offset: 0x00098498
		private bool RetrieveAndCheckReferentialConstraintValuesInAcceptChanges()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			List<string> list;
			bool flag2;
			bool flag = relationshipManager.FindNamesOfReferentialConstraintProperties(out list, out flag2, true);
			if (list != null)
			{
				HashSet<object> hashSet = new HashSet<object>();
				Dictionary<string, KeyValuePair<object, IntBox>> dictionary;
				relationshipManager.RetrieveReferentialConstraintProperties(out dictionary, hashSet, false);
				foreach (KeyValuePair<string, KeyValuePair<object, IntBox>> keyValuePair in dictionary)
				{
					this.SetCurrentEntityValue(keyValuePair.Key, keyValuePair.Value.Key);
				}
			}
			if (flag2)
			{
				this.CheckReferentialConstraintPropertiesInDependents();
			}
			return flag;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0009A338 File Offset: 0x00098538
		internal void RetrieveReferentialConstraintPropertiesFromKeyEntries(Dictionary<string, KeyValuePair<object, IntBox>> properties)
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
				if (otherEndOfRelationship.IsKeyEntry)
				{
					foreach (ReferentialConstraint referentialConstraint in ((AssociationSet)relationshipEntry.EntitySet).ElementType.ReferentialConstraints)
					{
						string name = this.GetAssociationEndMember(relationshipEntry).Name;
						if (referentialConstraint.ToRole.Name == name)
						{
							foreach (EntityKeyMember entityKeyMember in ((IEnumerable<EntityKeyMember>)otherEndOfRelationship.EntityKey.EntityKeyValues))
							{
								for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
								{
									if (referentialConstraint.FromProperties[i].Name == entityKeyMember.Key)
									{
										EntityEntry.AddOrIncreaseCounter(referentialConstraint, properties, referentialConstraint.ToProperties[i].Name, entityKeyMember.Value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0009A4E8 File Offset: 0x000986E8
		internal static void AddOrIncreaseCounter(ReferentialConstraint constraint, Dictionary<string, KeyValuePair<object, IntBox>> properties, string propertyName, object propertyValue)
		{
			if (!properties.ContainsKey(propertyName))
			{
				properties[propertyName] = new KeyValuePair<object, IntBox>(propertyValue, new IntBox(1));
				return;
			}
			KeyValuePair<object, IntBox> keyValuePair = properties[propertyName];
			if (!ByValueEqualityComparer.Default.Equals(keyValuePair.Key, propertyValue))
			{
				throw new InvalidOperationException(constraint.BuildConstraintExceptionMessage());
			}
			keyValuePair.Value.Value = keyValuePair.Value.Value + 1;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0009A554 File Offset: 0x00098754
		private void CheckReferentialConstraintPropertiesInDependents()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
				if (otherEndOfRelationship.State == EntityState.Unchanged || otherEndOfRelationship.State == EntityState.Modified)
				{
					foreach (ReferentialConstraint referentialConstraint in ((AssociationSet)relationshipEntry.EntitySet).ElementType.ReferentialConstraints)
					{
						string name = this.GetAssociationEndMember(relationshipEntry).Name;
						if (referentialConstraint.FromRole.Name == name)
						{
							foreach (EntityKeyMember entityKeyMember in ((IEnumerable<EntityKeyMember>)otherEndOfRelationship.EntityKey.EntityKeyValues))
							{
								for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
								{
									if (referentialConstraint.ToProperties[i].Name == entityKeyMember.Key && !ByValueEqualityComparer.Default.Equals(this.GetCurrentEntityValue(referentialConstraint.FromProperties[i].Name), entityKeyMember.Value))
									{
										throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0009A728 File Offset: 0x00098928
		internal void PromoteKeyEntry(IEntityWrapper wrappedEntity, StateManagerTypeMetadata typeMetadata)
		{
			this._wrappedEntity = wrappedEntity;
			this._wrappedEntity.ObjectStateEntry = this;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0009A74C File Offset: 0x0009894C
		internal void DegradeEntry()
		{
			this._entityKey = this.EntityKey;
			this.RemoveFromForeignKeyIndex();
			this._wrappedEntity.SetChangeTracker(null);
			this._modifiedFields = null;
			this._originalValues = null;
			this._originalComplexObjects = null;
			if (base.State == EntityState.Added)
			{
				this._wrappedEntity.EntityKey = null;
				this._entityKey = null;
			}
			if (base.State != EntityState.Unchanged)
			{
				this._cache.ChangeState(this, base.State, EntityState.Unchanged);
				base.State = EntityState.Unchanged;
			}
			this._cache.RemoveEntryFromKeylessStore(this._wrappedEntity);
			this._wrappedEntity.DetachContext();
			this._wrappedEntity.ObjectStateEntry = null;
			object entity = this._wrappedEntity.Entity;
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this.SetChangeTrackingFlags();
			this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Remove, entity);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0009A81D File Offset: 0x00098A1D
		internal void AttachObjectStateManagerToEntity()
		{
			this._wrappedEntity.SetChangeTracker(this);
			this._wrappedEntity.TakeSnapshot(this);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0009A838 File Offset: 0x00098A38
		internal void GetOtherKeyProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties)
		{
			foreach (EdmMember edmMember in (this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType as EntityType).KeyMembers)
			{
				if (!properties.ContainsKey(edmMember.Name))
				{
					properties[edmMember.Name] = new KeyValuePair<object, IntBox>(this.GetCurrentEntityValue(edmMember.Name), new IntBox(1));
				}
			}
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0009A8D0 File Offset: 0x00098AD0
		internal void AddOriginalValueAt(int index, StateManagerMemberMetadata memberMetadata, object userObject, object value)
		{
			StateManagerValue stateManagerValue = new StateManagerValue(memberMetadata, userObject, value);
			if (index >= 0)
			{
				this._originalValues[index] = stateManagerValue;
				return;
			}
			if (this._originalValues == null)
			{
				this._originalValues = new List<StateManagerValue>();
			}
			this._originalValues.Add(stateManagerValue);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0009A91C File Offset: 0x00098B1C
		internal void CompareKeyProperties(object changed)
		{
			StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
			int fieldCount = this.GetFieldCount(cacheTypeMetadata);
			for (int i = 0; i < fieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = cacheTypeMetadata.Member(i);
				if (stateManagerMemberMetadata.IsPartOfKey)
				{
					object value = stateManagerMemberMetadata.GetValue(changed);
					object value2 = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
					if (!ByValueEqualityComparer.Default.Equals(value, value2))
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName));
					}
				}
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0009A998 File Offset: 0x00098B98
		internal object GetCurrentEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0009A9CB File Offset: 0x00098BCB
		internal void VerifyEntityValueIsEditable(StateManagerTypeMetadata typeMetadata, int ordinal, string memberName)
		{
			if (base.State == EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyDetachedDeletedEntries);
			}
			if (typeMetadata.Member(ordinal).IsPartOfKey && base.State != EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(memberName));
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0009AA04 File Offset: 0x00098C04
		internal void SetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (newValue == null || newValue == DBNull.Value)
				{
					throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName));
				}
				IExtendedDataRecord extendedDataRecord = newValue as IExtendedDataRecord;
				if (extendedDataRecord == null)
				{
					throw new ArgumentException(Strings.ObjectStateEntry_InvalidTypeForComplexTypeProperty, "newValue");
				}
				newValue = this._cache.ComplexTypeMaterializer.CreateComplex(extendedDataRecord, extendedDataRecord.DataRecordInfo, null);
			}
			this._wrappedEntity.SetCurrentValue(this, stateManagerMemberMetadata, ordinal, userObject, newValue);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x0009AA8C File Offset: 0x00098C8C
		private void TransitionRelationshipsForAdd()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				if (relationshipEntry.State == EntityState.Unchanged)
				{
					base.ObjectStateManager.ChangeState(relationshipEntry, EntityState.Unchanged, EntityState.Added);
					relationshipEntry.State = EntityState.Added;
				}
				else if (relationshipEntry.State == EntityState.Deleted)
				{
					relationshipEntry.DeleteUnnecessaryKeyEntries();
					relationshipEntry.DetachRelationshipEntry();
				}
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0009AAF2 File Offset: 0x00098CF2
		[Conditional("DEBUG")]
		private void VerifyIsNotRelated()
		{
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0009AAF4 File Offset: 0x00098CF4
		internal void ChangeObjectState(EntityState requestedState)
		{
			if (!this.IsKeyEntry)
			{
				EntityState state = base.State;
				switch (state)
				{
				case EntityState.Detached:
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Unchanged:
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
						this._modifiedFields = null;
						this._originalValues = null;
						this._originalComplexObjects = null;
						base.State = EntityState.Added;
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
				case EntityState.Added:
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						this.AcceptChanges();
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this._cache.ForgetEntryWithConceptualNull(this, true);
							this.AcceptChanges();
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.AcceptChanges();
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
				default:
					if (state == EntityState.Deleted)
					{
						switch (requestedState)
						{
						case EntityState.Detached:
							this.Detach();
							return;
						case EntityState.Unchanged:
							this._modifiedFields = null;
							this._originalValues = null;
							this._originalComplexObjects = null;
							base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Unchanged);
							base.State = EntityState.Unchanged;
							this._wrappedEntity.TakeSnapshot(this);
							this._cache.FixupReferencesByForeignKeys(this, false);
							this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
							return;
						case EntityState.Detached | EntityState.Unchanged:
							break;
						case EntityState.Added:
							this.TransitionRelationshipsForAdd();
							base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
							this._modifiedFields = null;
							this._originalValues = null;
							this._originalComplexObjects = null;
							base.State = EntityState.Added;
							this._cache.FixupReferencesByForeignKeys(this, false);
							this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
							return;
						default:
							if (requestedState == EntityState.Deleted)
							{
								return;
							}
							if (requestedState == EntityState.Modified)
							{
								base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Modified);
								base.State = EntityState.Modified;
								this.SetModifiedAll();
								this._cache.FixupReferencesByForeignKeys(this, false);
								this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
								return;
							}
							break;
						}
						throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
					}
					if (state != EntityState.Modified)
					{
						return;
					}
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						this.AcceptChanges();
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
						this._modifiedFields = null;
						this._originalValues = null;
						this._originalComplexObjects = null;
						base.State = EntityState.Added;
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
				}
				return;
			}
			if (requestedState == EntityState.Unchanged)
			{
				return;
			}
			throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0009ADE8 File Offset: 0x00098FE8
		internal void UpdateOriginalValues(object entity)
		{
			EntityState state = base.State;
			this.UpdateRecordWithSetModified(entity, this.EditableOriginalValues);
			if (state == EntityState.Unchanged && base.State == EntityState.Modified)
			{
				base.ObjectStateManager.ChangeState(this, state, EntityState.Modified);
			}
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0009AE26 File Offset: 0x00099026
		internal void UpdateRecordWithoutSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithoutSetModified, -1);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0009AE32 File Offset: 0x00099032
		internal void UpdateRecordWithSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithSetModified, -1);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0009AE40 File Offset: 0x00099040
		private void UpdateRecord(object value, DbUpdatableDataRecord current, EntityEntry.UpdateRecordBehavior behavior, int propertyIndex)
		{
			StateManagerTypeMetadata metadata = current._metadata;
			foreach (FieldMetadata fieldMetadata in metadata.DataRecordInfo.FieldMetadata)
			{
				int ordinal = fieldMetadata.Ordinal;
				StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
				object obj = stateManagerMemberMetadata.GetValue(value) ?? DBNull.Value;
				if (Helper.IsComplexType(fieldMetadata.FieldType.TypeUsage.EdmType))
				{
					object value2 = current.GetValue(ordinal);
					if (value2 == DBNull.Value)
					{
						throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(fieldMetadata.FieldType.Name));
					}
					if (obj != DBNull.Value)
					{
						this.UpdateRecord(obj, (DbUpdatableDataRecord)value2, behavior, (propertyIndex == -1) ? ordinal : propertyIndex);
					}
				}
				else if (this.HasRecordValueChanged(current, ordinal, obj) && !stateManagerMemberMetadata.IsPartOfKey)
				{
					current.SetValue(ordinal, obj);
					if (behavior == EntityEntry.UpdateRecordBehavior.WithSetModified)
					{
						this.SetModifiedPropertyInternal((propertyIndex == -1) ? ordinal : propertyIndex);
					}
				}
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0009AF54 File Offset: 0x00099154
		internal bool HasRecordValueChanged(DbDataRecord record, int propertyIndex, object newFieldValue)
		{
			object value = record.GetValue(propertyIndex);
			return (value != newFieldValue && (DBNull.Value == newFieldValue || DBNull.Value == value || !ByValueEqualityComparer.Default.Equals(value, newFieldValue))) || (this._cache.EntryHasConceptualNull(this) && this._modifiedFields != null && this._modifiedFields[propertyIndex]);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0009AFB4 File Offset: 0x000991B4
		internal void ApplyCurrentValuesInternal(IEntityWrapper wrappedCurrentEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModified(base.State.ToString()));
			}
			if (this.WrappedEntity.IdentityType != wrappedCurrentEntity.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedCurrentEntity.Entity.GetType().FullName));
			}
			this.CompareKeyProperties(wrappedCurrentEntity.Entity);
			this.UpdateCurrentValueRecord(wrappedCurrentEntity.Entity);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0009B04E File Offset: 0x0009924E
		internal void UpdateCurrentValueRecord(object value)
		{
			this._wrappedEntity.UpdateCurrentValueRecord(value, this);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0009B060 File Offset: 0x00099260
		internal void ApplyOriginalValuesInternal(IEntityWrapper wrappedOriginalEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged && base.State != EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(base.State.ToString()));
			}
			if (this.WrappedEntity.IdentityType != wrappedOriginalEntity.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedOriginalEntity.Entity.GetType().FullName));
			}
			this.CompareKeyProperties(wrappedOriginalEntity.Entity);
			this.UpdateOriginalValues(wrappedOriginalEntity.Entity);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0009B104 File Offset: 0x00099304
		internal void RemoveFromForeignKeyIndex()
		{
			if (!this.IsKeyEntry)
			{
				foreach (EntityReference entityReference in this.FindFKRelatedEnds())
				{
					foreach (EntityKey entityKey in entityReference.GetAllKeyValues())
					{
						this._cache.RemoveEntryFromForeignKeyIndex(entityReference, entityKey, this);
					}
				}
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x0009B198 File Offset: 0x00099398
		internal void FixupReferencesByForeignKeys(bool replaceAddedRefs, EntitySetBase restrictTo = null)
		{
			this._cache.TransactionManager.BeginGraphUpdate();
			bool flag = !this._cache.TransactionManager.IsAttachTracking && !this._cache.TransactionManager.IsAddTracking;
			try
			{
				IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> foreignKeyDependents = this.ForeignKeyDependents;
				Func<Tuple<AssociationSet, ReferentialConstraint>, bool> <>9__0;
				Func<Tuple<AssociationSet, ReferentialConstraint>, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Tuple<AssociationSet, ReferentialConstraint> t) => restrictTo == null || t.Item1.SourceSet.Identity == restrictTo.Identity || t.Item1.TargetSet.Identity == restrictTo.Identity);
				}
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in foreignKeyDependents.Where(func))
				{
					EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType, (AssociationEndMember)tuple.Item2.FromRole) as EntityReference;
					if (!ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						this.FixupEntityReferenceToPrincipal(entityReference, null, flag, replaceAddedRefs);
					}
				}
			}
			finally
			{
				this._cache.TransactionManager.EndGraphUpdate();
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0009B2B4 File Offset: 0x000994B4
		internal void FixupEntityReferenceByForeignKey(EntityReference reference)
		{
			reference.IsLoaded = false;
			if (ForeignKeyFactory.IsConceptualNullKey(reference.CachedForeignKey))
			{
				base.ObjectStateManager.ForgetEntryWithConceptualNull(this, false);
			}
			IEntityWrapper referenceValue = reference.ReferenceValue;
			EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, reference);
			bool flag;
			if (entityKey == null || referenceValue.Entity == null)
			{
				flag = true;
			}
			else
			{
				EntityKey entityKey2 = referenceValue.EntityKey;
				EntityEntry objectStateEntry = referenceValue.ObjectStateEntry;
				if ((entityKey2 == null || entityKey2.IsTemporary) && objectStateEntry != null)
				{
					entityKey2 = new EntityKey((EntitySet)objectStateEntry.EntitySet, objectStateEntry.CurrentValues);
				}
				flag = !entityKey.Equals(entityKey2);
			}
			if (this._cache.TransactionManager.RelationshipBeingUpdated != reference)
			{
				if (!flag)
				{
					return;
				}
				this._cache.TransactionManager.BeginGraphUpdate();
				if (entityKey != null)
				{
					this._cache.TransactionManager.EntityBeingReparented = this.Entity;
				}
				try
				{
					this.FixupEntityReferenceToPrincipal(reference, entityKey, false, true);
					return;
				}
				finally
				{
					this._cache.TransactionManager.EntityBeingReparented = null;
					this._cache.TransactionManager.EndGraphUpdate();
				}
			}
			this.FixupEntityReferenceToPrincipal(reference, entityKey, false, false);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0009B3D0 File Offset: 0x000995D0
		internal void FixupEntityReferenceToPrincipal(EntityReference relatedEnd, EntityKey foreignKey, bool setIsLoaded, bool replaceExistingRef)
		{
			if (foreignKey == null)
			{
				foreignKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, relatedEnd);
			}
			bool flag = this._cache.TransactionManager.RelationshipBeingUpdated != relatedEnd && (!this._cache.TransactionManager.IsForeignKeyUpdate || relatedEnd.ReferenceValue.ObjectStateEntry == null || relatedEnd.ReferenceValue.ObjectStateEntry.State != EntityState.Added);
			relatedEnd.SetCachedForeignKey(foreignKey, this);
			base.ObjectStateManager.ForgetEntryWithConceptualNull(this, false);
			if (foreignKey != null)
			{
				EntityEntry entityEntry;
				if (this._cache.TryGetEntityEntry(foreignKey, out entityEntry) && !entityEntry.IsKeyEntry && entityEntry.State != EntityState.Deleted && (replaceExistingRef || EntityEntry.WillNotRefSteal(relatedEnd, entityEntry.WrappedEntity)) && relatedEnd.CanSetEntityType(entityEntry.WrappedEntity))
				{
					if (flag)
					{
						if (this._cache.TransactionManager.PopulatedEntityReferences != null)
						{
							this._cache.TransactionManager.PopulatedEntityReferences.Add(relatedEnd);
						}
						relatedEnd.SetEntityKey(foreignKey, true);
						if (this._cache.TransactionManager.PopulatedEntityReferences != null)
						{
							EntityReference entityReference = relatedEnd.GetOtherEndOfRelationship(entityEntry.WrappedEntity) as EntityReference;
							if (entityReference != null)
							{
								this._cache.TransactionManager.PopulatedEntityReferences.Add(entityReference);
							}
						}
					}
					if (setIsLoaded && entityEntry.State != EntityState.Added)
					{
						relatedEnd.IsLoaded = true;
						return;
					}
				}
				else
				{
					this._cache.AddEntryContainingForeignKeyToIndex(relatedEnd, foreignKey, this);
					if (flag && replaceExistingRef && relatedEnd.ReferenceValue.Entity != null)
					{
						relatedEnd.ReferenceValue = NullEntityWrapper.NullWrapper;
						return;
					}
				}
			}
			else if (flag)
			{
				if (replaceExistingRef && (relatedEnd.ReferenceValue.Entity != null || relatedEnd.EntityKey != null))
				{
					relatedEnd.ReferenceValue = NullEntityWrapper.NullWrapper;
				}
				if (setIsLoaded)
				{
					relatedEnd.IsLoaded = true;
				}
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0009B59C File Offset: 0x0009979C
		private static bool WillNotRefSteal(EntityReference refToPrincipal, IEntityWrapper wrappedPrincipal)
		{
			EntityReference entityReference = refToPrincipal.GetOtherEndOfRelationship(wrappedPrincipal) as EntityReference;
			if (refToPrincipal.ReferenceValue.Entity == null && refToPrincipal.NavigationPropertyIsNullOrMissing() && (entityReference == null || (entityReference.ReferenceValue.Entity == null && entityReference.NavigationPropertyIsNullOrMissing())))
			{
				return true;
			}
			if (entityReference != null && (entityReference.ReferenceValue.Entity == refToPrincipal.WrappedOwner.Entity || entityReference.CheckIfNavigationPropertyContainsEntity(refToPrincipal.WrappedOwner)))
			{
				return true;
			}
			if (entityReference == null || refToPrincipal.ReferenceValue.Entity == wrappedPrincipal.Entity || refToPrincipal.CheckIfNavigationPropertyContainsEntity(wrappedPrincipal))
			{
				return false;
			}
			throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName));
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0009B654 File Offset: 0x00099854
		internal bool TryGetReferenceKey(AssociationEndMember principalRole, out EntityKey principalKey)
		{
			EntityReference entityReference = this.RelationshipManager.GetRelatedEnd(principalRole.DeclaringType.FullName, principalRole.Name) as EntityReference;
			if (entityReference.CachedValue.Entity == null || entityReference.CachedValue.ObjectStateEntry == null)
			{
				principalKey = null;
				return false;
			}
			principalKey = entityReference.EntityKey ?? entityReference.CachedValue.ObjectStateEntry.EntityKey;
			return principalKey != null;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0009B6C8 File Offset: 0x000998C8
		internal void FixupForeignKeysByReference()
		{
			this._cache.TransactionManager.BeginFixupKeysByReference();
			try
			{
				this.FixupForeignKeysByReference(null);
			}
			finally
			{
				this._cache.TransactionManager.EndFixupKeysByReference();
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0009B710 File Offset: 0x00099910
		private void FixupForeignKeysByReference(List<EntityEntry> visited)
		{
			if (!(base.EntitySet as EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				IEntityWrapper referenceValue = entityReference.ReferenceValue;
				if (referenceValue.Entity != null)
				{
					EntityEntry objectStateEntry = referenceValue.ObjectStateEntry;
					bool? flag = null;
					if (objectStateEntry != null && objectStateEntry.State == EntityState.Added)
					{
						if (objectStateEntry == this)
						{
							flag = new bool?(entityReference.GetOtherEndOfRelationship(referenceValue) is EntityReference);
							bool? flag2 = flag;
							if (!flag2.Value)
							{
								goto IL_0119;
							}
						}
						visited = visited ?? new List<EntityEntry>();
						if (visited.Contains(this))
						{
							if (flag == null)
							{
								flag = new bool?(entityReference.GetOtherEndOfRelationship(referenceValue) is EntityReference);
							}
							if (flag.Value)
							{
								throw new InvalidOperationException(Strings.RelationshipManager_CircularRelationshipsWithReferentialConstraints);
							}
						}
						else
						{
							visited.Add(this);
							objectStateEntry.FixupForeignKeysByReference(visited);
							visited.Remove(this);
						}
					}
					IL_0119:
					entityReference.UpdateForeignKeyValues(this.WrappedEntity, referenceValue, null, false);
				}
				else
				{
					EntityKey entityKey = entityReference.EntityKey;
					if (entityKey != null && !entityKey.IsTemporary)
					{
						entityReference.UpdateForeignKeyValues(this.WrappedEntity, entityKey);
					}
				}
			}
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple2 in this.ForeignKeyPrincipals)
			{
				bool flag3 = false;
				bool flag4 = false;
				RelatedEnd relatedEndInternal = this.RelationshipManager.GetRelatedEndInternal(tuple2.Item1.ElementType.FullName, tuple2.Item2.ToRole.Name);
				foreach (IEntityWrapper entityWrapper in relatedEndInternal.GetWrappedEntities())
				{
					EntityEntry objectStateEntry2 = entityWrapper.ObjectStateEntry;
					if (objectStateEntry2.State != EntityState.Added && !flag4)
					{
						flag4 = true;
						foreach (EdmProperty edmProperty in tuple2.Item2.ToProperties)
						{
							int ordinalforOLayerMemberName = objectStateEntry2._cacheTypeMetadata.GetOrdinalforOLayerMemberName(edmProperty.Name);
							if (objectStateEntry2._cacheTypeMetadata.Member(ordinalforOLayerMemberName).IsPartOfKey)
							{
								flag3 = true;
								break;
							}
						}
					}
					if (objectStateEntry2.State == EntityState.Added || (objectStateEntry2.State == EntityState.Modified && !flag3))
					{
						(relatedEndInternal.GetOtherEndOfRelationship(entityWrapper) as EntityReference).UpdateForeignKeyValues(entityWrapper, this.WrappedEntity, null, false);
					}
				}
			}
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0009BA3C File Offset: 0x00099C3C
		private bool IsPropertyAForeignKey(string propertyName)
		{
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = tuple.Item2.ToProperties.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Name == propertyName)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0009BAD4 File Offset: 0x00099CD4
		private bool IsPropertyAForeignKey(string propertyName, out List<Pair<string, string>> relationships)
		{
			relationships = null;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = tuple.Item2.ToProperties.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Name == propertyName)
						{
							if (relationships == null)
							{
								relationships = new List<Pair<string, string>>();
							}
							relationships.Add(new Pair<string, string>(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name));
							break;
						}
					}
				}
			}
			return relationships != null;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0009BBB0 File Offset: 0x00099DB0
		internal void FindRelatedEntityKeysByForeignKeys(out Dictionary<RelatedEnd, HashSet<EntityKey>> relatedEntities, bool useOriginalValues)
		{
			relatedEntities = null;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				string identity = item2.ToRole.Identity;
				ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = item.AssociationSetEnds;
				AssociationEndMember associationEndMember;
				if (associationSetEnds[0].CorrespondingAssociationEndMember.Identity == identity)
				{
					associationEndMember = associationSetEnds[1].CorrespondingAssociationEndMember;
				}
				else
				{
					associationEndMember = associationSetEnds[0].CorrespondingAssociationEndMember;
				}
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(item, associationEndMember);
				EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, item2, entitySetAtEnd, useOriginalValues);
				if (entityKey != null)
				{
					EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(item.ElementType, (AssociationEndMember)item2.FromRole) as EntityReference;
					relatedEntities = ((relatedEntities != null) ? relatedEntities : new Dictionary<RelatedEnd, HashSet<EntityKey>>());
					HashSet<EntityKey> hashSet;
					if (!relatedEntities.TryGetValue(entityReference, out hashSet))
					{
						hashSet = new HashSet<EntityKey>();
						relatedEntities.Add(entityReference, hashSet);
					}
					hashSet.Add(entityKey);
				}
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0009BCD4 File Offset: 0x00099ED4
		internal IEnumerable<EntityReference> FindFKRelatedEnds()
		{
			HashSet<EntityReference> hashSet = new HashSet<EntityReference>();
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				hashSet.Add(entityReference);
			}
			return hashSet;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0009BD5C File Offset: 0x00099F5C
		internal void DetectChangesInForeignKeys()
		{
			TransactionManager transactionManager = base.ObjectStateManager.TransactionManager;
			foreach (EntityReference entityReference in this.FindFKRelatedEnds())
			{
				EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, entityReference);
				EntityKey cachedForeignKey = entityReference.CachedForeignKey;
				bool flag = ForeignKeyFactory.IsConceptualNullKey(cachedForeignKey);
				if (cachedForeignKey != null || entityKey != null)
				{
					if (cachedForeignKey == null)
					{
						EntityEntry entityEntry;
						base.ObjectStateManager.TryGetEntityEntry(entityKey, out entityEntry);
						this.AddRelationshipDetectedByForeignKey(transactionManager.AddedRelationshipsByForeignKey, transactionManager.AddedRelationshipsByPrincipalKey, entityKey, entityEntry, entityReference);
					}
					else if (entityKey == null)
					{
						EntityEntry.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
					}
					else if (!entityKey.Equals(cachedForeignKey) && (!flag || ForeignKeyFactory.IsConceptualNullKeyChanged(cachedForeignKey, entityKey)))
					{
						EntityEntry entityEntry2;
						base.ObjectStateManager.TryGetEntityEntry(entityKey, out entityEntry2);
						this.AddRelationshipDetectedByForeignKey(transactionManager.AddedRelationshipsByForeignKey, transactionManager.AddedRelationshipsByPrincipalKey, entityKey, entityEntry2, entityReference);
						if (!flag)
						{
							EntityEntry.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
						}
					}
				}
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x0009BE7C File Offset: 0x0009A07C
		internal bool RequiresComplexChangeTracking
		{
			get
			{
				return this._requiresComplexChangeTracking;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x0009BE84 File Offset: 0x0009A084
		internal bool RequiresScalarChangeTracking
		{
			get
			{
				return this._requiresScalarChangeTracking;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x0009BE8C File Offset: 0x0009A08C
		internal bool RequiresAnyChangeTracking
		{
			get
			{
				return this._requiresAnyChangeTracking;
			}
		}

		// Token: 0x04001019 RID: 4121
		private StateManagerTypeMetadata _cacheTypeMetadata;

		// Token: 0x0400101A RID: 4122
		private EntityKey _entityKey;

		// Token: 0x0400101B RID: 4123
		private IEntityWrapper _wrappedEntity;

		// Token: 0x0400101C RID: 4124
		private BitArray _modifiedFields;

		// Token: 0x0400101D RID: 4125
		private List<StateManagerValue> _originalValues;

		// Token: 0x0400101E RID: 4126
		private Dictionary<object, Dictionary<int, object>> _originalComplexObjects;

		// Token: 0x0400101F RID: 4127
		private bool _requiresComplexChangeTracking;

		// Token: 0x04001020 RID: 4128
		private bool _requiresScalarChangeTracking;

		// Token: 0x04001021 RID: 4129
		private bool _requiresAnyChangeTracking;

		// Token: 0x04001022 RID: 4130
		private RelationshipEntry _headRelationshipEnds;

		// Token: 0x04001023 RID: 4131
		private int _countRelationshipEnds;

		// Token: 0x04001024 RID: 4132
		internal const int s_EntityRoot = -1;

		// Token: 0x02000A0D RID: 2573
		internal struct RelationshipEndEnumerable : IEnumerable<RelationshipEntry>, IEnumerable, IEnumerable<IEntityStateEntry>
		{
			// Token: 0x060060B1 RID: 24753 RVA: 0x0014C58E File Offset: 0x0014A78E
			internal RelationshipEndEnumerable(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
			}

			// Token: 0x060060B2 RID: 24754 RVA: 0x0014C597 File Offset: 0x0014A797
			public EntityEntry.RelationshipEndEnumerator GetEnumerator()
			{
				return new EntityEntry.RelationshipEndEnumerator(this._entityEntry);
			}

			// Token: 0x060060B3 RID: 24755 RVA: 0x0014C5A4 File Offset: 0x0014A7A4
			IEnumerator<IEntityStateEntry> IEnumerable<IEntityStateEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060060B4 RID: 24756 RVA: 0x0014C5B1 File Offset: 0x0014A7B1
			IEnumerator<RelationshipEntry> IEnumerable<RelationshipEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060060B5 RID: 24757 RVA: 0x0014C5BE File Offset: 0x0014A7BE
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060060B6 RID: 24758 RVA: 0x0014C5CC File Offset: 0x0014A7CC
			internal RelationshipEntry[] ToArray()
			{
				RelationshipEntry[] array = null;
				if (this._entityEntry != null && 0 < this._entityEntry._countRelationshipEnds)
				{
					RelationshipEntry relationshipEntry = this._entityEntry._headRelationshipEnds;
					array = new RelationshipEntry[this._entityEntry._countRelationshipEnds];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = relationshipEntry;
						relationshipEntry = relationshipEntry.GetNextRelationshipEnd(this._entityEntry.EntityKey);
					}
				}
				return array ?? EntityEntry.RelationshipEndEnumerable.EmptyRelationshipEntryArray;
			}

			// Token: 0x0400291D RID: 10525
			internal static readonly RelationshipEntry[] EmptyRelationshipEntryArray = new RelationshipEntry[0];

			// Token: 0x0400291E RID: 10526
			private readonly EntityEntry _entityEntry;
		}

		// Token: 0x02000A0E RID: 2574
		internal struct RelationshipEndEnumerator : IEnumerator<RelationshipEntry>, IDisposable, IEnumerator, IEnumerator<IEntityStateEntry>
		{
			// Token: 0x060060B8 RID: 24760 RVA: 0x0014C649 File Offset: 0x0014A849
			internal RelationshipEndEnumerator(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
				this._current = null;
			}

			// Token: 0x170010A2 RID: 4258
			// (get) Token: 0x060060B9 RID: 24761 RVA: 0x0014C659 File Offset: 0x0014A859
			public RelationshipEntry Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170010A3 RID: 4259
			// (get) Token: 0x060060BA RID: 24762 RVA: 0x0014C661 File Offset: 0x0014A861
			IEntityStateEntry IEnumerator<IEntityStateEntry>.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170010A4 RID: 4260
			// (get) Token: 0x060060BB RID: 24763 RVA: 0x0014C669 File Offset: 0x0014A869
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x060060BC RID: 24764 RVA: 0x0014C671 File Offset: 0x0014A871
			public void Dispose()
			{
			}

			// Token: 0x060060BD RID: 24765 RVA: 0x0014C674 File Offset: 0x0014A874
			public bool MoveNext()
			{
				if (this._entityEntry != null)
				{
					if (this._current == null)
					{
						this._current = this._entityEntry._headRelationshipEnds;
					}
					else
					{
						this._current = this._current.GetNextRelationshipEnd(this._entityEntry.EntityKey);
					}
				}
				return this._current != null;
			}

			// Token: 0x060060BE RID: 24766 RVA: 0x0014C6C9 File Offset: 0x0014A8C9
			public void Reset()
			{
			}

			// Token: 0x0400291F RID: 10527
			private readonly EntityEntry _entityEntry;

			// Token: 0x04002920 RID: 10528
			private RelationshipEntry _current;
		}

		// Token: 0x02000A0F RID: 2575
		private enum UpdateRecordBehavior
		{
			// Token: 0x04002922 RID: 10530
			WithoutSetModified,
			// Token: 0x04002923 RID: 10531
			WithSetModified
		}
	}
}
