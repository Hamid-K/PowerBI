using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024E RID: 590
	internal sealed class VelocitySignalWaitHandle
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0003DED0 File Offset: 0x0003C0D0
		public bool WaitForSignal(int millisecondTimeout)
		{
			bool flag2;
			lock (this._lock)
			{
				flag2 = Monitor.Wait(this._lock, millisecondTimeout);
			}
			return flag2;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0003DF18 File Offset: 0x0003C118
		public void WaitForSignal()
		{
			lock (this._lock)
			{
				Monitor.Wait(this._lock);
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0003DF60 File Offset: 0x0003C160
		public void SignalAll()
		{
			lock (this._lock)
			{
				Monitor.PulseAll(this._lock);
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
		public void Signal()
		{
			lock (this._lock)
			{
				Monitor.Pulse(this._lock);
			}
		}

		// Token: 0x04000BE9 RID: 3049
		private object _lock = new object();
	}
}
