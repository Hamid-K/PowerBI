using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000144 RID: 324
	internal abstract class Disposable : IDisposable
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x00037870 File Offset: 0x00035A70
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000378A0 File Offset: 0x00035AA0
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000378A8 File Offset: 0x00035AA8
		public bool IsDisposed { get; private set; }

		// Token: 0x0600101E RID: 4126 RVA: 0x000378B1 File Offset: 0x00035AB1
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000378CF File Offset: 0x00035ACF
		protected internal void ThrowIfAlreadyDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000378EA File Offset: 0x00035AEA
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
