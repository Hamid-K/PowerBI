using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000307 RID: 775
	internal class DelegateBasedCachePerfCounter : CachePerfCounter
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x00056514 File Offset: 0x00054714
		internal DelegateBasedCachePerfCounter(CachePerfCounter.Name counterName, string cacheName, PerfCounterValue callBack)
			: base(counterName, cacheName)
		{
			this._getValue = callBack;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00056530 File Offset: 0x00054730
		internal override long GetValue()
		{
			return this._getValue();
		}

		// Token: 0x04000F6B RID: 3947
		private PerfCounterValue _getValue = PerfCounter.DefaultValue;
	}
}
