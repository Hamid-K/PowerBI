using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024A RID: 586
	internal sealed class SleepSpinLock : ILatch
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0003DB14 File Offset: 0x0003BD14
		public SleepSpinLock(int st)
		{
			this._sleepTime = st;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003DB23 File Offset: 0x0003BD23
		public void Latch()
		{
			while (Interlocked.CompareExchange(ref this._mutex, 1, 0) != 0)
			{
				Thread.Sleep(this._sleepTime);
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0003DB41 File Offset: 0x0003BD41
		public void UnLatch()
		{
			Thread.VolatileWrite(ref this._mutex, 0);
		}

		// Token: 0x04000BCD RID: 3021
		private int _mutex;

		// Token: 0x04000BCE RID: 3022
		private int _sleepTime;
	}
}
