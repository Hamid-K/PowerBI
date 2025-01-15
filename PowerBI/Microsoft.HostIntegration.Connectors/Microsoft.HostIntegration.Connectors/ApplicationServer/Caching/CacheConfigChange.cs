using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000EF RID: 239
	internal struct CacheConfigChange
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001AA2B File Offset: 0x00018C2B
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001AA33 File Offset: 0x00018C33
		internal bool MaxNamedCacheCountChange
		{
			get
			{
				return this._maxNamedCacheCountChange;
			}
			set
			{
				this._maxNamedCacheCountChange = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001AA3C File Offset: 0x00018C3C
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001AA44 File Offset: 0x00018C44
		internal bool BasePartitionCountChange
		{
			get
			{
				return this._basePartitionCountChange;
			}
			set
			{
				this._basePartitionCountChange = value;
			}
		}

		// Token: 0x1700013F RID: 319
		public bool this[CacheChanges key]
		{
			get
			{
				return (this._changeFlag & key) != CacheChanges.NoChange;
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

		// Token: 0x060006BC RID: 1724 RVA: 0x0001AA80 File Offset: 0x00018C80
		internal void ChangeAll(bool on)
		{
			this._maxNamedCacheCountChange = on;
			this._basePartitionCountChange = on;
			this[CacheChanges.ChangeAll] = on;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001AA98 File Offset: 0x00018C98
		internal bool CachePropertiesChanged
		{
			get
			{
				return this._changeFlag != CacheChanges.NoChange;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001AAA6 File Offset: 0x00018CA6
		public bool Changed
		{
			get
			{
				return this._maxNamedCacheCountChange || this._basePartitionCountChange || this._changeFlag != CacheChanges.NoChange;
			}
		}

		// Token: 0x04000432 RID: 1074
		private bool _maxNamedCacheCountChange;

		// Token: 0x04000433 RID: 1075
		private bool _basePartitionCountChange;

		// Token: 0x04000434 RID: 1076
		private CacheChanges _changeFlag;
	}
}
