using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000224 RID: 548
	internal struct DMOptexStruct
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00039A64 File Offset: 0x00037C64
		internal bool Enter(int key)
		{
			while (Interlocked.Increment(ref this._counter) != 1)
			{
				while (!DMGlobal.ArraySignalHandle[key].WaitForSignal(ConfigManager.SignalTimeout))
				{
					if (Thread.VolatileRead(ref this._counter) <= 1)
					{
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00039AAB File Offset: 0x00037CAB
		internal void Exit(int key)
		{
			if (Interlocked.CompareExchange(ref this._counter, 0, 1) == 1)
			{
				return;
			}
			Thread.VolatileWrite(ref this._counter, 0);
			DMGlobal.ArraySignalHandle[key].SignalAll();
		}

		// Token: 0x04000B21 RID: 2849
		private int _counter;
	}
}
