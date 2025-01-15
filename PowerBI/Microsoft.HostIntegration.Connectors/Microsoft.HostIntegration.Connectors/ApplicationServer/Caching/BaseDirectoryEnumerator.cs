using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FE RID: 510
	internal sealed class BaseDirectoryEnumerator : BaseEnumerator
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00037539 File Offset: 0x00035739
		internal BaseDirectoryEnumerator(MDHDirectoryNode dirNode)
		{
			this._directoryNode = dirNode;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00037548 File Offset: 0x00035748
		protected override object NextData(ref int index)
		{
			MDHNode mdhnode;
			index = (int)this._directoryNode.GetNextNode((short)index, out mdhnode);
			return mdhnode;
		}

		// Token: 0x04000ACF RID: 2767
		private MDHDirectoryNode _directoryNode;
	}
}
