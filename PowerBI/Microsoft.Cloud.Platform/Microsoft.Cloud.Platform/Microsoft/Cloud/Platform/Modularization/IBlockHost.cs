using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C5 RID: 197
	public interface IBlockHost : IBlockServiceManager
	{
		// Token: 0x060005A2 RID: 1442
		void RequestShutdown(IBlock requestor);

		// Token: 0x060005A3 RID: 1443
		void RequestShutdown(IBlock requestor, int returnCode);

		// Token: 0x060005A4 RID: 1444
		void AddBlock(IBlock block);

		// Token: 0x060005A5 RID: 1445
		void AddBlocks(IEnumerable<IBlock> blocksToAdd);
	}
}
