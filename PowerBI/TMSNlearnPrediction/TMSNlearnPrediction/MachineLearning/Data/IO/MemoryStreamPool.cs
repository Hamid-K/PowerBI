using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x0200032A RID: 810
	internal sealed class MemoryStreamPool
	{
		// Token: 0x06001211 RID: 4625 RVA: 0x0006526C File Offset: 0x0006346C
		public MemoryStreamPool()
		{
			this._memPool = new ObjectPool<MemoryStream>();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0006527F File Offset: 0x0006347F
		public void Return(ref MemoryStream mem)
		{
			mem.Position = 0L;
			mem.SetLength(0L);
			this._memPool.Return(mem);
			mem = null;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000652A4 File Offset: 0x000634A4
		public MemoryStream Get()
		{
			return this._memPool.Get();
		}

		// Token: 0x04000A8D RID: 2701
		private readonly ObjectPool<MemoryStream> _memPool;
	}
}
