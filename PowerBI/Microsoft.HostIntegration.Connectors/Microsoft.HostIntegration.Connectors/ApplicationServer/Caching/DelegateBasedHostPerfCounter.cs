using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200030C RID: 780
	internal class DelegateBasedHostPerfCounter : HostPerfCounter
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00057252 File Offset: 0x00055452
		internal DelegateBasedHostPerfCounter(HostPerfCounter.Name counterName, PerfCounterValue callBack)
			: base(counterName)
		{
			this._getValue = callBack;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0005726D File Offset: 0x0005546D
		internal override long GetValue()
		{
			return this._getValue();
		}

		// Token: 0x04000FC0 RID: 4032
		private PerfCounterValue _getValue = PerfCounter.DefaultValue;
	}
}
