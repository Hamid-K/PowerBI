using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	internal class MDHObjectNode : MDHLeafNode
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00040A7E File Offset: 0x0003EC7E
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00040A86 File Offset: 0x0003EC86
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00040A8F File Offset: 0x0003EC8F
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x00040A9C File Offset: 0x0003EC9C
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

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00040AAA File Offset: 0x0003ECAA
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x00040AB7 File Offset: 0x0003ECB7
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

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00040AC5 File Offset: 0x0003ECC5
		internal bool IsCommitLocked
		{
			get
			{
				return this._flag.IsCommitLocked;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000360A4 File Offset: 0x000342A4
		public MDHObjectNode(int hkey)
			: base(hkey)
		{
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000360AD File Offset: 0x000342AD
		public MDHObjectNode()
		{
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00040AD2 File Offset: 0x0003ECD2
		public MDHObjectNode(AMDHObjectNode node)
			: base(node)
		{
			this._flag = node.GetPrunedFlag();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00040AE7 File Offset: 0x0003ECE7
		internal bool EqualsKey(int hkey, object key)
		{
			return hkey == base.HashCode && this.Key.Equals(key);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override object Get(object key, int hashKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override object GetData(FixedDepthEnumeratorState state, int level)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00040B00 File Offset: 0x0003ED00
		internal override bool CompareKey(object key)
		{
			return this._key.Equals(key);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00006F04 File Offset: 0x00005104
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.MDHObjectNode;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00040B0E File Offset: 0x0003ED0E
		internal override bool CanNodeBeMoved()
		{
			return !this._flag.IsCommitLocked;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00040B1E File Offset: 0x0003ED1E
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

		// Token: 0x04000C5F RID: 3167
		private object _key;

		// Token: 0x04000C60 RID: 3168
		protected CacheItemFlag _flag;
	}
}
