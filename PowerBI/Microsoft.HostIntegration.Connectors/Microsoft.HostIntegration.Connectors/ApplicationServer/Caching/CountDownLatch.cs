using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002FC RID: 764
	internal sealed class CountDownLatch : IDisposable
	{
		// Token: 0x06001C66 RID: 7270 RVA: 0x00055CA5 File Offset: 0x00053EA5
		public CountDownLatch(int count)
		{
			this._count = count;
			this._aEvent = new AutoResetEvent(false);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00055CC0 File Offset: 0x00053EC0
		public bool Wait(TimeSpan timeSpan)
		{
			return this._aEvent.WaitOne(timeSpan, false);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00055CCF File Offset: 0x00053ECF
		public void Wait()
		{
			this._aEvent.WaitOne();
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00055CDD File Offset: 0x00053EDD
		public void Signal()
		{
			if (Interlocked.Decrement(ref this._count) == 0)
			{
				this._aEvent.Set();
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00055CF8 File Offset: 0x00053EF8
		public void Close()
		{
			this._aEvent.Close();
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00055D05 File Offset: 0x00053F05
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000F63 RID: 3939
		private int _count;

		// Token: 0x04000F64 RID: 3940
		private AutoResetEvent _aEvent;
	}
}
