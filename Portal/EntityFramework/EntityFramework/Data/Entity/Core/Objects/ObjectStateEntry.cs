using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Diagnostics;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041E RID: 1054
	public abstract class ObjectStateEntry : IEntityStateEntry, IEntityChangeTracker
	{
		// Token: 0x0600329A RID: 12954 RVA: 0x000A2099 File Offset: 0x000A0299
		internal ObjectStateEntry()
		{
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000A20A1 File Offset: 0x000A02A1
		internal ObjectStateEntry(ObjectStateManager cache, EntitySet entitySet, EntityState state)
		{
			this._cache = cache;
			this._entitySet = entitySet;
			this._state = state;
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000A20BE File Offset: 0x000A02BE
		public ObjectStateManager ObjectStateManager
		{
			get
			{
				this.ValidateState();
				return this._cache;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000A20CC File Offset: 0x000A02CC
		public EntitySetBase EntitySet
		{
			get
			{
				this.ValidateState();
				return this._entitySet;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000A20DA File Offset: 0x000A02DA
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x000A20E2 File Offset: 0x000A02E2
		public EntityState State
		{
			get
			{
				return this._state;
			}
			internal set
			{
				this._state = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060032A0 RID: 12960
		public abstract object Entity { get; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060032A1 RID: 12961
		// (set) Token: 0x060032A2 RID: 12962
		public abstract EntityKey EntityKey { get; internal set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060032A3 RID: 12963
		public abstract bool IsRelationship { get; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060032A4 RID: 12964
		internal abstract BitArray ModifiedProperties { get; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000A20EB File Offset: 0x000A02EB
		BitArray IEntityStateEntry.ModifiedProperties
		{
			get
			{
				return this.ModifiedProperties;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060032A6 RID: 12966
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract DbDataRecord OriginalValues { get; }

		// Token: 0x060032A7 RID: 12967
		public abstract OriginalValueRecord GetUpdatableOriginalValues();

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060032A8 RID: 12968
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract CurrentValueRecord CurrentValues { get; }

		// Token: 0x060032A9 RID: 12969
		public abstract void AcceptChanges();

		// Token: 0x060032AA RID: 12970
		public abstract void Delete();

		// Token: 0x060032AB RID: 12971
		public abstract IEnumerable<string> GetModifiedProperties();

		// Token: 0x060032AC RID: 12972
		public abstract void SetModified();

		// Token: 0x060032AD RID: 12973
		public abstract void SetModifiedProperty(string propertyName);

		// Token: 0x060032AE RID: 12974
		public abstract void RejectPropertyChanges(string propertyName);

		// Token: 0x060032AF RID: 12975
		public abstract bool IsPropertyChanged(string propertyName);

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060032B0 RID: 12976
		public abstract RelationshipManager RelationshipManager { get; }

		// Token: 0x060032B1 RID: 12977
		public abstract void ChangeState(EntityState state);

		// Token: 0x060032B2 RID: 12978
		public abstract void ApplyCurrentValues(object currentEntity);

		// Token: 0x060032B3 RID: 12979
		public abstract void ApplyOriginalValues(object originalEntity);

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000A20F3 File Offset: 0x000A02F3
		IEntityStateManager IEntityStateEntry.StateManager
		{
			get
			{
				return this.ObjectStateManager;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000A20FB File Offset: 0x000A02FB
		bool IEntityStateEntry.IsKeyEntry
		{
			get
			{
				return this.IsKeyEntry;
			}
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000A2103 File Offset: 0x000A0303
		void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
		{
			this.EntityMemberChanging(entityMemberName);
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000A210C File Offset: 0x000A030C
		void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
		{
			this.EntityMemberChanged(entityMemberName);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000A2115 File Offset: 0x000A0315
		void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000A2120 File Offset: 0x000A0320
		void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000A212B File Offset: 0x000A032B
		EntityState IEntityChangeTracker.EntityState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060032BB RID: 12987
		internal abstract bool IsKeyEntry { get; }

		// Token: 0x060032BC RID: 12988
		internal abstract int GetFieldCount(StateManagerTypeMetadata metadata);

		// Token: 0x060032BD RID: 12989
		internal abstract Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060032BE RID: 12990
		internal abstract string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060032BF RID: 12991
		internal abstract int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata);

		// Token: 0x060032C0 RID: 12992
		internal abstract void RevertDelete();

		// Token: 0x060032C1 RID: 12993
		internal abstract void SetModifiedAll();

		// Token: 0x060032C2 RID: 12994
		internal abstract void EntityMemberChanging(string entityMemberName);

		// Token: 0x060032C3 RID: 12995
		internal abstract void EntityMemberChanged(string entityMemberName);

		// Token: 0x060032C4 RID: 12996
		internal abstract void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060032C5 RID: 12997
		internal abstract void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060032C6 RID: 12998
		internal abstract DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject);

		// Token: 0x060032C7 RID: 12999 RVA: 0x000A2133 File Offset: 0x000A0333
		internal virtual void Reset()
		{
			this._cache = null;
			this._entitySet = null;
			this._state = EntityState.Detached;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000A214A File Offset: 0x000A034A
		internal void ValidateState()
		{
			if (this._state == EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_InvalidState);
			}
		}

		// Token: 0x04001086 RID: 4230
		internal ObjectStateManager _cache;

		// Token: 0x04001087 RID: 4231
		internal EntitySetBase _entitySet;

		// Token: 0x04001088 RID: 4232
		internal EntityState _state;
	}
}
