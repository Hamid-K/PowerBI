using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000306 RID: 774
	public sealed class ChangeCollection<TChange> : IEnumerable<TChange>, IEnumerable where TChange : Change
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x000498B1 File Offset: 0x000488B1
		internal ChangeCollection()
		{
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000498C4 File Offset: 0x000488C4
		internal void Add(TChange change)
		{
			EwsUtilities.Assert(change != null, "ChangeList.Add", "change is null");
			this.changes.Add(change);
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000498ED File Offset: 0x000488ED
		public int Count
		{
			get
			{
				return this.changes.Count;
			}
		}

		// Token: 0x1700068B RID: 1675
		public TChange this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.changes[index];
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0004992A File Offset: 0x0004892A
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00049932 File Offset: 0x00048932
		public string SyncState
		{
			get
			{
				return this.syncState;
			}
			internal set
			{
				this.syncState = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0004993B File Offset: 0x0004893B
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00049943 File Offset: 0x00048943
		public bool MoreChangesAvailable
		{
			get
			{
				return this.moreChangesAvailable;
			}
			internal set
			{
				this.moreChangesAvailable = value;
			}
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0004994C File Offset: 0x0004894C
		public IEnumerator<TChange> GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0004995E File Offset: 0x0004895E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x0400145A RID: 5210
		private List<TChange> changes = new List<TChange>();

		// Token: 0x0400145B RID: 5211
		private string syncState;

		// Token: 0x0400145C RID: 5212
		private bool moreChangesAvailable;
	}
}
