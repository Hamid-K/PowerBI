using System;
using System.Threading;

namespace Azure
{
	// Token: 0x02000033 RID: 51
	public class SyncAsyncEventArgs : EventArgs
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003EC4 File Offset: 0x000020C4
		public bool IsRunningSynchronously { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003ECC File Offset: 0x000020CC
		public CancellationToken CancellationToken { get; }

		// Token: 0x06000119 RID: 281 RVA: 0x00003ED4 File Offset: 0x000020D4
		public SyncAsyncEventArgs(bool isRunningSynchronously, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.IsRunningSynchronously = isRunningSynchronously;
			this.CancellationToken = cancellationToken;
		}
	}
}
