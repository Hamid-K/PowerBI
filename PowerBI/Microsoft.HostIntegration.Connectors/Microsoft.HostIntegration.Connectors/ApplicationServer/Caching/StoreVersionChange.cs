using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000EC RID: 236
	internal struct StoreVersionChange
	{
		// Token: 0x17000132 RID: 306
		public bool this[string key]
		{
			get
			{
				return (this._changeFlag & StoreVersionChange.MapStringToEnum(key)) != StoreVersionChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= StoreVersionChange.MapStringToEnum(key);
					return;
				}
				this._changeFlag &= ~StoreVersionChange.MapStringToEnum(key);
			}
		}

		// Token: 0x17000133 RID: 307
		public bool this[StoreVersionChanges key]
		{
			get
			{
				return (this._changeFlag & key) != StoreVersionChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= key;
					return;
				}
				this._changeFlag &= ~key;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001A4AB File Offset: 0x000186AB
		public StoreVersionChange(StoreVersionChange original)
		{
			this._changeFlag = original._changeFlag;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001A4BC File Offset: 0x000186BC
		private static StoreVersionChanges MapStringToEnum(string name)
		{
			if (name != null && name == "clusterConfigurationStoreVersion")
			{
				return StoreVersionChanges.ClusterConfigStoreVersionChange;
			}
			throw new ArgumentException("Unknown Config Element", "name");
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001A4EC File Offset: 0x000186EC
		public bool Changed
		{
			get
			{
				return this._changeFlag != StoreVersionChanges.NoChange;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001A4FC File Offset: 0x000186FC
		internal bool CanUpdateConfigDynamically()
		{
			StoreVersionChange storeVersionChange = new StoreVersionChange(this);
			storeVersionChange[StoreVersionChanges.ClusterConfigStoreVersionChange] = false;
			return !storeVersionChange.Changed;
		}

		// Token: 0x0400042D RID: 1069
		private StoreVersionChanges _changeFlag;
	}
}
