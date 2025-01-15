using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200034C RID: 844
	internal class ObjectDirectoryNodeFactory : IDirectoryNodeFactory
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x00059E54 File Offset: 0x00058054
		public MDHDirectoryNode GetDirectory(short maskOffset, MDHDirectoryNode parent, short parentIndex)
		{
			return new MDHDirectoryNode(maskOffset, parent, parentIndex);
		}
	}
}
