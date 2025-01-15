using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000309 RID: 777
	internal class DelegateBasedHostMultiplePerfCounter : HostMultiplePerfCounter
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x00056699 File Offset: 0x00054899
		internal DelegateBasedHostMultiplePerfCounter(HostPerfCounter.Name[] counterNames, PerfCounterValues callBack)
			: base(counterNames)
		{
			this._getValues = callBack;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000566A9 File Offset: 0x000548A9
		internal override long[] GetValues()
		{
			if (this._getValues != null)
			{
				return this._getValues();
			}
			return null;
		}

		// Token: 0x04000F6D RID: 3949
		private PerfCounterValues _getValues;
	}
}
