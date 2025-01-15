using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000327 RID: 807
	internal interface IDirectoryNodeFactory
	{
		// Token: 0x06001D30 RID: 7472
		MDHDirectoryNode GetDirectory(short maskOffset, MDHDirectoryNode parent, short parentIndex);
	}
}
