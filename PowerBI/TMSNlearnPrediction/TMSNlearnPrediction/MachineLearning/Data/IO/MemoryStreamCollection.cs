using System;
using System.Threading;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x0200032B RID: 811
	internal sealed class MemoryStreamCollection
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x000652BE File Offset: 0x000634BE
		public MemoryStreamCollection()
		{
			this._pools = new MemoryStreamPool[MemoryStreamCollection.IndexFor(int.MaxValue) + 1];
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000652DD File Offset: 0x000634DD
		private static int IndexFor(int maxSize)
		{
			return Math.Max(Utils.IbitHigh((uint)maxSize) - 15, 0);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000652F0 File Offset: 0x000634F0
		public MemoryStreamPool Get(int maxSize)
		{
			Contracts.CheckParam(0 <= maxSize, "maxSize", "Must be positive");
			int num = MemoryStreamCollection.IndexFor(maxSize);
			if (this._pools[num] == null)
			{
				Interlocked.CompareExchange<MemoryStreamPool>(ref this._pools[num], new MemoryStreamPool(), null);
			}
			return this._pools[num];
		}

		// Token: 0x04000A8E RID: 2702
		private readonly MemoryStreamPool[] _pools;
	}
}
