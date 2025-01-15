using System;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002E5 RID: 741
	internal struct BlockLookup
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0005DDD7 File Offset: 0x0005BFD7
		public BlockLookup(long blockOffset, int blockLength, int decompressedBlockLength)
		{
			this.BlockOffset = blockOffset;
			this.BlockLength = blockLength;
			this.DecompressedBlockLength = decompressedBlockLength;
		}

		// Token: 0x04000982 RID: 2434
		public readonly long BlockOffset;

		// Token: 0x04000983 RID: 2435
		public readonly int BlockLength;

		// Token: 0x04000984 RID: 2436
		public readonly int DecompressedBlockLength;
	}
}
