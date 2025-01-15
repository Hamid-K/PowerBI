using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C1 RID: 705
	public sealed class ScopedSimulatedTime : IDisposable
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x00041CB6 File Offset: 0x0003FEB6
		public ScopedSimulatedTime(DateTime utcNow)
		{
			ExtendedDateTime.SetSimulatedTime(utcNow);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00041CC4 File Offset: 0x0003FEC4
		public void Dispose()
		{
			ExtendedDateTime.StopSimulatingTime();
		}
	}
}
