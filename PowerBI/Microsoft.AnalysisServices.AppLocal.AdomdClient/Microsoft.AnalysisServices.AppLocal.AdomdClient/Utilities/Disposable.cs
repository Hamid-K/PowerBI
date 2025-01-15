using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000144 RID: 324
	internal abstract class Disposable : IDisposable
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x00037BA0 File Offset: 0x00035DA0
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00037BD0 File Offset: 0x00035DD0
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00037BD8 File Offset: 0x00035DD8
		public bool IsDisposed { get; private set; }

		// Token: 0x0600102B RID: 4139 RVA: 0x00037BE1 File Offset: 0x00035DE1
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00037BFF File Offset: 0x00035DFF
		protected internal void ThrowIfAlreadyDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00037C1A File Offset: 0x00035E1A
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
