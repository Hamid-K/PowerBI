using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000114 RID: 276
	internal class StateEntryAdapter : IEntityStateEntry
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0003284E File Offset: 0x00030A4E
		public StateEntryAdapter(ObjectStateEntry stateEntry)
		{
			this._stateEntry = stateEntry;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0003285D File Offset: 0x00030A5D
		public object Entity
		{
			get
			{
				return this._stateEntry.Entity;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0003286A File Offset: 0x00030A6A
		public EntityState State
		{
			get
			{
				return this._stateEntry.State;
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00032877 File Offset: 0x00030A77
		public void ChangeState(EntityState state)
		{
			this._stateEntry.ChangeState(state);
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x00032885 File Offset: 0x00030A85
		public DbUpdatableDataRecord CurrentValues
		{
			get
			{
				return this._stateEntry.CurrentValues;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00032892 File Offset: 0x00030A92
		public DbUpdatableDataRecord GetUpdatableOriginalValues()
		{
			return this._stateEntry.GetUpdatableOriginalValues();
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0003289F File Offset: 0x00030A9F
		public EntitySetBase EntitySet
		{
			get
			{
				return this._stateEntry.EntitySet;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000328AC File Offset: 0x00030AAC
		public EntityKey EntityKey
		{
			get
			{
				return this._stateEntry.EntityKey;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000328B9 File Offset: 0x00030AB9
		public IEnumerable<string> GetModifiedProperties()
		{
			return this._stateEntry.GetModifiedProperties();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000328C6 File Offset: 0x00030AC6
		public void SetModifiedProperty(string propertyName)
		{
			this._stateEntry.SetModifiedProperty(propertyName);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000328D4 File Offset: 0x00030AD4
		public void RejectPropertyChanges(string propertyName)
		{
			this._stateEntry.RejectPropertyChanges(propertyName);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000328E2 File Offset: 0x00030AE2
		public bool IsPropertyChanged(string propertyName)
		{
			return this._stateEntry.IsPropertyChanged(propertyName);
		}

		// Token: 0x04000950 RID: 2384
		private readonly ObjectStateEntry _stateEntry;
	}
}
