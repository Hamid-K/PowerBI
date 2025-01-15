using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000241 RID: 577
	internal struct MDHDirectoryEnumerationState : IMDHEnumerationState
	{
		// Token: 0x06001317 RID: 4887 RVA: 0x0003B1F1 File Offset: 0x000393F1
		internal MDHDirectoryEnumerationState(MDHDirectoryNode directory)
		{
			this._dir = directory;
			this._index = 0;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003B204 File Offset: 0x00039404
		public MDHNode getNextNode()
		{
			MDHNode mdhnode;
			this._index = this._dir.GetNextNode(this._index, out mdhnode);
			return mdhnode;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0003B22B File Offset: 0x0003942B
		public MDHNode getContainer()
		{
			return this._dir;
		}

		// Token: 0x04000B78 RID: 2936
		private MDHDirectoryNode _dir;

		// Token: 0x04000B79 RID: 2937
		private short _index;
	}
}
