using System;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000139 RID: 313
	internal abstract class Disposable : IDisposable
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x0003A4A4 File Offset: 0x000386A4
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003A4D4 File Offset: 0x000386D4
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x0003A4DC File Offset: 0x000386DC
		public bool IsDisposed { get; private set; }

		// Token: 0x060010B9 RID: 4281 RVA: 0x0003A4E5 File Offset: 0x000386E5
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003A503 File Offset: 0x00038703
		protected internal void ThrowIfAlreadyDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003A51E File Offset: 0x0003871E
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
