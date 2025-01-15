using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200042D RID: 1069
	internal sealed class RelationshipEntry : ObjectStateEntry
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000A78DB File Offset: 0x000A5ADB
		internal EntityKey Key0
		{
			get
			{
				return this.RelationshipWrapper.Key0;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000A78E8 File Offset: 0x000A5AE8
		internal EntityKey Key1
		{
			get
			{
				return this.RelationshipWrapper.Key1;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000A78F5 File Offset: 0x000A5AF5
		internal override BitArray ModifiedProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000A78F8 File Offset: 0x000A5AF8
		internal RelationshipEntry(ObjectStateManager cache, EntityState state, RelationshipWrapper relationshipWrapper)
			: base(cache, null, state)
		{
			this._entitySet = relationshipWrapper.AssociationSet;
			this._relationshipWrapper = relationshipWrapper;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000A7916 File Offset: 0x000A5B16
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return true;
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000A7920 File Offset: 0x000A5B20
		public override void AcceptChanges()
		{
			base.ValidateState();
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this._cache.ChangeState(this, EntityState.Added, EntityState.Unchanged);
					base.State = EntityState.Unchanged;
				}
			}
			else
			{
				if (state != EntityState.Deleted)
				{
					return;
				}
				this.DeleteUnnecessaryKeyEntries();
				if (this._cache != null)
				{
					this._cache.ChangeState(this, EntityState.Deleted, EntityState.Detached);
					return;
				}
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000A7983 File Offset: 0x000A5B83
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000A798C File Offset: 0x000A5B8C
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			yield break;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000A799C File Offset: 0x000A5B9C
		public override void SetModified()
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000A79AE File Offset: 0x000A5BAE
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return null;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000A79B7 File Offset: 0x000A5BB7
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000A79C0 File Offset: 0x000A5BC0
		public override EntityKey EntityKey
		{
			get
			{
				base.ValidateState();
				return null;
			}
			internal set
			{
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000A79C2 File Offset: 0x000A5BC2
		public override void SetModifiedProperty(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000A79D4 File Offset: 0x000A5BD4
		public override void RejectPropertyChanges(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000A79E6 File Offset: 0x000A5BE6
		public override bool IsPropertyChanged(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x000A79F8 File Offset: 0x000A5BF8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Added)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
				}
				return new ObjectStateEntryDbDataRecord(this);
			}
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000A7A1A File Offset: 0x000A5C1A
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x000A7A26 File Offset: 0x000A5C26
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
				return new ObjectStateEntryDbUpdatableDataRecord(this);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000A7A48 File Offset: 0x000A5C48
		public override RelationshipManager RelationshipManager
		{
			get
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers);
			}
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000A7A54 File Offset: 0x000A5C54
		public override void ChangeState(EntityState state)
		{
			EntityUtil.CheckValidStateForChangeRelationshipState(state, "state");
			if (base.State == EntityState.Detached && state == EntityState.Detached)
			{
				return;
			}
			base.ValidateState();
			if (this.RelationshipWrapper.Key0 == this.Key0)
			{
				base.ObjectStateManager.ChangeRelationshipState(this.Key0, this.Key1, this.RelationshipWrapper.AssociationSet.ElementType.FullName, this.RelationshipWrapper.AssociationEndMembers[1].Name, state);
				return;
			}
			base.ObjectStateManager.ChangeRelationshipState(this.Key0, this.Key1, this.RelationshipWrapper.AssociationSet.ElementType.FullName, this.RelationshipWrapper.AssociationEndMembers[0].Name, state);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000A7B21 File Offset: 0x000A5D21
		public override void ApplyCurrentValues(object currentEntity)
		{
			Check.NotNull<object>(currentEntity, "currentEntity");
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000A7B39 File Offset: 0x000A5D39
		public override void ApplyOriginalValues(object originalEntity)
		{
			Check.NotNull<object>(originalEntity, "originalEntity");
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000A7B51 File Offset: 0x000A5D51
		internal override bool IsKeyEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000A7B54 File Offset: 0x000A5D54
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return this._relationshipWrapper.AssociationEndMembers.Count;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000A7B66 File Offset: 0x000A5D66
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			return new DataRecordInfo(TypeUsage.Create(((RelationshipSet)base.EntitySet).ElementType));
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000A7B82 File Offset: 0x000A5D82
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000A7B94 File Offset: 0x000A5D94
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return typeof(EntityKey);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000A7BA0 File Offset: 0x000A5DA0
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			return this._relationshipWrapper.AssociationEndMembers[ordinal].Name;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000A7BC0 File Offset: 0x000A5DC0
		internal override int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata)
		{
			ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = this._relationshipWrapper.AssociationEndMembers;
			AssociationEndMember associationEndMember;
			if (associationEndMembers.TryGetValue(name, false, out associationEndMember))
			{
				return associationEndMembers.IndexOf(associationEndMember);
			}
			return -1;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000A7BEE File Offset: 0x000A5DEE
		internal override void RevertDelete()
		{
			base.State = EntityState.Unchanged;
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000A7C0A File Offset: 0x000A5E0A
		internal override void EntityMemberChanging(string entityMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000A7C16 File Offset: 0x000A5E16
		internal override void EntityMemberChanged(string entityMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000A7C22 File Offset: 0x000A5E22
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000A7C2E File Offset: 0x000A5E2E
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000A7C3C File Offset: 0x000A5E3C
		internal bool IsSameAssociationSetAndRole(AssociationSet associationSet, AssociationEndMember associationMember, EntityKey entityKey)
		{
			if (this._entitySet != associationSet)
			{
				return false;
			}
			if (this._relationshipWrapper.AssociationSet.ElementType.AssociationEndMembers[0].Name == associationMember.Name)
			{
				return entityKey == this.Key0;
			}
			return entityKey == this.Key1;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000A7C9A File Offset: 0x000A5E9A
		private object GetCurrentRelationValue(int ordinal, bool throwException)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			base.ValidateState();
			if (base.State == EntityState.Deleted && throwException)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CurrentValuesDoesNotExist);
			}
			return this._relationshipWrapper.GetEntityKey(ordinal);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000A7CCC File Offset: 0x000A5ECC
		private static void ValidateRelationshipRange(int ordinal)
		{
			if (1 < ordinal)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000A7CDD File Offset: 0x000A5EDD
		internal object GetCurrentRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, true);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000A7CE7 File Offset: 0x000A5EE7
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000A7CEF File Offset: 0x000A5EEF
		internal RelationshipWrapper RelationshipWrapper
		{
			get
			{
				return this._relationshipWrapper;
			}
			set
			{
				this._relationshipWrapper = value;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000A7CF8 File Offset: 0x000A5EF8
		internal override void Reset()
		{
			this._relationshipWrapper = null;
			base.Reset();
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000A7D08 File Offset: 0x000A5F08
		internal void ChangeRelatedEnd(EntityKey oldKey, EntityKey newKey)
		{
			if (!oldKey.Equals(this.Key0))
			{
				this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper, 1, newKey);
				return;
			}
			if (oldKey.Equals(this.Key1))
			{
				this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper.AssociationSet, newKey);
				return;
			}
			this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper, 0, newKey);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000A7D70 File Offset: 0x000A5F70
		internal void DeleteUnnecessaryKeyEntries()
		{
			for (int i = 0; i < 2; i++)
			{
				EntityKey entityKey = this.GetCurrentRelationValue(i, false) as EntityKey;
				EntityEntry entityEntry = this._cache.GetEntityEntry(entityKey);
				if (entityEntry.IsKeyEntry)
				{
					bool flag = false;
					using (EntityEntry.RelationshipEndEnumerator enumerator = this._cache.FindRelationshipsByKey(entityKey).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current != this)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						this._cache.DeleteKeyEntry(entityEntry);
						return;
					}
				}
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000A7E10 File Offset: 0x000A6010
		internal void Delete(bool doFixup)
		{
			base.ValidateState();
			if (doFixup)
			{
				if (base.State != EntityState.Deleted)
				{
					EntityEntry entityEntry = this._cache.GetEntityEntry((EntityKey)this.GetCurrentRelationValue(0));
					IEntityWrapper wrappedEntity = entityEntry.WrappedEntity;
					EntityEntry entityEntry2 = this._cache.GetEntityEntry((EntityKey)this.GetCurrentRelationValue(1));
					IEntityWrapper wrappedEntity2 = entityEntry2.WrappedEntity;
					if (wrappedEntity.Entity != null && wrappedEntity2.Entity != null)
					{
						string name = this._relationshipWrapper.AssociationEndMembers[1].Name;
						string fullName = ((AssociationSet)this._entitySet).ElementType.FullName;
						wrappedEntity.RelationshipManager.RemoveEntity(name, fullName, wrappedEntity2);
						return;
					}
					EntityKey entityKey;
					RelationshipManager relationshipManager;
					if (wrappedEntity.Entity == null)
					{
						entityKey = entityEntry.EntityKey;
						relationshipManager = wrappedEntity2.RelationshipManager;
					}
					else
					{
						entityKey = entityEntry2.EntityKey;
						relationshipManager = wrappedEntity.RelationshipManager;
					}
					AssociationEndMember associationEndMember = this.RelationshipWrapper.GetAssociationEndMember(entityKey);
					((EntityReference)relationshipManager.GetRelatedEndInternal(associationEndMember.DeclaringType.FullName, associationEndMember.Name)).DetachedEntityKey = null;
					if (base.State == EntityState.Added)
					{
						this.DeleteUnnecessaryKeyEntries();
						this.DetachRelationshipEntry();
						return;
					}
					this._cache.ChangeState(this, base.State, EntityState.Deleted);
					base.State = EntityState.Deleted;
					return;
				}
			}
			else
			{
				EntityState state = base.State;
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this.DeleteUnnecessaryKeyEntries();
					this.DetachRelationshipEntry();
					return;
				}
				else
				{
					this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
					base.State = EntityState.Deleted;
				}
			}
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000A7F93 File Offset: 0x000A6193
		internal object GetOriginalRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, false);
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000A7F9D File Offset: 0x000A619D
		internal void DetachRelationshipEntry()
		{
			if (this._cache != null)
			{
				this._cache.ChangeState(this, base.State, EntityState.Detached);
			}
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000A7FBC File Offset: 0x000A61BC
		internal void ChangeRelationshipState(EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState requestedState)
		{
			EntityState state = base.State;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state != EntityState.Deleted)
					{
						return;
					}
					switch (requestedState)
					{
					case EntityState.Detached:
						this.AcceptChanges();
						break;
					case EntityState.Unchanged:
						relatedEnd.Add(targetEntry.WrappedEntity, true, false, true, false, true);
						base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Unchanged);
						base.State = EntityState.Unchanged;
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						relatedEnd.Add(targetEntry.WrappedEntity, true, false, true, false, true);
						base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Added);
						base.State = EntityState.Added;
						return;
					default:
						return;
					}
				}
				else
				{
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Delete();
						return;
					case EntityState.Unchanged:
						this.AcceptChanges();
						return;
					case EntityState.Detached | EntityState.Unchanged:
					case EntityState.Added:
						break;
					default:
						if (requestedState != EntityState.Deleted)
						{
							return;
						}
						this.AcceptChanges();
						this.Delete();
						return;
					}
				}
			}
			else
			{
				switch (requestedState)
				{
				case EntityState.Detached:
					this.Delete();
					this.AcceptChanges();
					return;
				case EntityState.Unchanged:
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					base.ObjectStateManager.ChangeState(this, EntityState.Unchanged, EntityState.Added);
					base.State = EntityState.Added;
					return;
				default:
					if (requestedState != EntityState.Deleted)
					{
						return;
					}
					this.Delete();
					return;
				}
			}
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000A80D0 File Offset: 0x000A62D0
		internal RelationshipEntry GetNextRelationshipEnd(EntityKey entityKey)
		{
			if (!entityKey.Equals(this.Key0))
			{
				return this.NextKey1;
			}
			return this.NextKey0;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000A80ED File Offset: 0x000A62ED
		internal void SetNextRelationshipEnd(EntityKey entityKey, RelationshipEntry nextEnd)
		{
			if (entityKey.Equals(this.Key0))
			{
				this.NextKey0 = nextEnd;
				return;
			}
			this.NextKey1 = nextEnd;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000A810C File Offset: 0x000A630C
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000A8114 File Offset: 0x000A6314
		internal RelationshipEntry NextKey0 { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000A811D File Offset: 0x000A631D
		// (set) Token: 0x0600340F RID: 13327 RVA: 0x000A8125 File Offset: 0x000A6325
		internal RelationshipEntry NextKey1 { get; set; }

		// Token: 0x040010C9 RID: 4297
		internal RelationshipWrapper _relationshipWrapper;
	}
}
