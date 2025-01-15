using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000240 RID: 576
	internal struct MDHConflictingHashNodeEnumerationState : IMDHEnumerationState
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x0003B1C6 File Offset: 0x000393C6
		internal MDHConflictingHashNodeEnumerationState(MDHConflictingHashNode node)
		{
			this._index = 0;
			this._node = node;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003B1D6 File Offset: 0x000393D6
		public MDHNode getNextNode()
		{
			return this._node.GetNextNode(ref this._index);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0003B1E9 File Offset: 0x000393E9
		public MDHNode getContainer()
		{
			return this._node;
		}

		// Token: 0x04000B76 RID: 2934
		private int _index;

		// Token: 0x04000B77 RID: 2935
		private MDHConflictingHashNode _node;
	}
}
