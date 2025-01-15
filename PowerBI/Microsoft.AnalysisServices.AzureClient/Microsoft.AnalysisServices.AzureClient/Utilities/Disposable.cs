using System;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000028 RID: 40
	internal abstract class Disposable : IDisposable
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006898 File Offset: 0x00004A98
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000068C8 File Offset: 0x00004AC8
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000068D0 File Offset: 0x00004AD0
		public bool IsDisposed { get; private set; }

		// Token: 0x06000134 RID: 308 RVA: 0x000068D9 File Offset: 0x00004AD9
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000068F7 File Offset: 0x00004AF7
		protected internal void ThrowIfAlreadyDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006912 File Offset: 0x00004B12
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
