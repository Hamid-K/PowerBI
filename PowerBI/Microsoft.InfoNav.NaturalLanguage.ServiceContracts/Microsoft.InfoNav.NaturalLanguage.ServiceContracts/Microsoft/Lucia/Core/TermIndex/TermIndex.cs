using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200016D RID: 365
	public abstract class TermIndex : IDisposable
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000C342 File Offset: 0x0000A542
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000C34A File Offset: 0x0000A54A
		public bool Disposed { get; private set; }

		// Token: 0x0600071F RID: 1823 RVA: 0x0000C353 File Offset: 0x0000A553
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000C35C File Offset: 0x0000A55C
		protected virtual void Dispose(bool disposing)
		{
			this.Disposed = true;
		}
	}
}
