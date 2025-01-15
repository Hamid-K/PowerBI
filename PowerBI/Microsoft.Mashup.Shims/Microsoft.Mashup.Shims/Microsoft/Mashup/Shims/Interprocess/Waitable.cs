using System;
using System.Linq;
using System.Threading;

namespace Microsoft.Mashup.Shims.Interprocess
{
	// Token: 0x02000018 RID: 24
	public abstract class Waitable : IDisposable
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002526 File Offset: 0x00000726
		public static int WaitAny(params Waitable[] waitables)
		{
			return Waitable.WaitAny(-1, waitables);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000252F File Offset: 0x0000072F
		public static int WaitAny(TimeSpan timeout, params Waitable[] waitables)
		{
			return Waitable.WaitAny(checked((int)timeout.TotalMilliseconds), waitables);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000253F File Offset: 0x0000073F
		public void Wait()
		{
			this.Wait(-1);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000254C File Offset: 0x0000074C
		public bool Wait(TimeSpan timeout)
		{
			int num = checked((int)timeout.TotalMilliseconds);
			return this.Wait(num);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002569 File Offset: 0x00000769
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.Dispose(true);
				this.IsDisposed = true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002581 File Offset: 0x00000781
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002589 File Offset: 0x00000789
		private protected bool IsDisposed { protected get; private set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002592 File Offset: 0x00000792
		public Waitable()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64
		protected abstract WaitHandle WaitHandle { get; }

		// Token: 0x06000041 RID: 65 RVA: 0x0000259C File Offset: 0x0000079C
		public static int WaitAny(int millisecondsTimeout, params Waitable[] waitables)
		{
			int num = WaitHandle.WaitAny(waitables.Select((Waitable w) => w.WaitHandle).ToArray<WaitHandle>(), millisecondsTimeout);
			if (num != 258)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000025E5 File Offset: 0x000007E5
		public bool Wait(int millisecondsTimeout)
		{
			return this.WaitHandle.WaitOne(millisecondsTimeout);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025F3 File Offset: 0x000007F3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.WaitHandle.Close();
			}
		}

		// Token: 0x04000009 RID: 9
		public const int WaitTimeout = -1;
	}
}
