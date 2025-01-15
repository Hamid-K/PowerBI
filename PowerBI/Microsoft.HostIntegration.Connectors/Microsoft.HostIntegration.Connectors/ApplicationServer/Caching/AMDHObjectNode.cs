using System;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	internal abstract class AMDHObjectNode : MDHLeafNode
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00035FF8 File Offset: 0x000341F8
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x00036000 File Offset: 0x00034200
		public ADMCacheItem UncommittedCacheItem
		{
			get
			{
				return this._uncommittedCacheItem;
			}
			set
			{
				this._uncommittedCacheItem = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00035FF8 File Offset: 0x000341F8
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x00036000 File Offset: 0x00034200
		internal ADMCacheItem LastCommittedItem
		{
			get
			{
				return this._uncommittedCacheItem;
			}
			set
			{
				this._uncommittedCacheItem = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00036009 File Offset: 0x00034209
		public ADMCacheItem DMCacheItem
		{
			get
			{
				return this.GetCacheItem();
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00036011 File Offset: 0x00034211
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x0003601E File Offset: 0x0003421E
		public bool IsCommitted
		{
			get
			{
				return this._flag.Committed;
			}
			set
			{
				this._flag.Committed = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003602C File Offset: 0x0003422C
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00036039 File Offset: 0x00034239
		internal bool IsItemGettingDeleted
		{
			get
			{
				return this._flag.IsItemGettingDeleted;
			}
			set
			{
				this._flag.IsItemGettingDeleted = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00036047 File Offset: 0x00034247
		internal bool IsCommitLocked
		{
			get
			{
				return this._flag.IsCommitLocked;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00036054 File Offset: 0x00034254
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00036061 File Offset: 0x00034261
		internal bool IsTagsPresent
		{
			get
			{
				return this._flag.IsTagsPresent;
			}
			set
			{
				this._flag.IsTagsPresent = value;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00036070 File Offset: 0x00034270
		public ADMCacheItem GetCacheItem()
		{
			ADMCacheItem lastCommittedItem = this.LastCommittedItem;
			if (this.IsCommitted)
			{
				return (ADMCacheItem)this;
			}
			if (lastCommittedItem != null)
			{
				ReleaseAssert.IsTrue(lastCommittedItem.IsCommitted);
				return lastCommittedItem;
			}
			return null;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000360A4 File Offset: 0x000342A4
		public AMDHObjectNode(int hkey)
			: base(hkey)
		{
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000360AD File Offset: 0x000342AD
		public AMDHObjectNode()
		{
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000360B5 File Offset: 0x000342B5
		public AMDHObjectNode(AMDHObjectNode node)
			: base(node)
		{
			this._flag = node._flag.GetPrunedFlags();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000360CF File Offset: 0x000342CF
		internal CacheItemFlag GetPrunedFlag()
		{
			return this._flag.GetPrunedFlags();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000360DC File Offset: 0x000342DC
		internal bool EqualsKey(int hkey, object key)
		{
			return hkey == base.HashCode && this.Equals(key);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000360F0 File Offset: 0x000342F0
		internal override object Get(object key, int hashKey)
		{
			if (this.EqualsKey(hashKey, key))
			{
				return this.GetCacheItem();
			}
			return null;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override object GetData(FixedDepthEnumeratorState state, int level)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00036104 File Offset: 0x00034304
		internal void Commit(out ADMCacheItem oldValue, out ADMCacheItem newValue)
		{
			oldValue = (ADMCacheItem)this;
			if (!this.IsCommitted)
			{
				newValue = oldValue;
				oldValue.ReleaseCommitLock();
				oldValue = null;
				return;
			}
			newValue = this._uncommittedCacheItem;
			if (newValue != null)
			{
				newValue.LastCommittedItem = oldValue;
				oldValue.ReleaseCommitLock();
				return;
			}
			if (!oldValue.IsItemGettingDeleted)
			{
				newValue = oldValue;
				oldValue.ReleaseCommitLock();
				if (oldValue.IsLockPlaceHolderObject && !oldValue.IsLocked())
				{
					newValue = null;
				}
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00036176 File Offset: 0x00034376
		internal override bool CompareKey(object key)
		{
			return this.Equals(key);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00036180 File Offset: 0x00034380
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			ADMCacheItem dmcacheItem = this.DMCacheItem;
			return dmcacheItem == null || info.Scan(dmcacheItem);
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00006F04 File Offset: 0x00005104
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.MDHObjectNode;
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000361A0 File Offset: 0x000343A0
		internal override bool CanNodeBeMoved()
		{
			return !this._flag.IsCommitLocked;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000361B0 File Offset: 0x000343B0
		internal override void VerifyState()
		{
			if (this._flag.IsCommitLocked)
			{
				throw new InvalidOperationException("CacheItem is locked");
			}
			if (!this._flag.Committed)
			{
				throw new InvalidOperationException("CacheItem is not committed.");
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000361E2 File Offset: 0x000343E2
		protected new void Clean()
		{
			this._flag = default(CacheItemFlag);
			this._uncommittedCacheItem = null;
			base.Clean();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000361FD File Offset: 0x000343FD
		protected new void Init()
		{
			base.Init();
			this._flag = default(CacheItemFlag);
		}

		// Token: 0x04000AA9 RID: 2729
		protected CacheItemFlag _flag;

		// Token: 0x04000AAA RID: 2730
		[NonSerialized]
		private ADMCacheItem _uncommittedCacheItem;
	}
}
