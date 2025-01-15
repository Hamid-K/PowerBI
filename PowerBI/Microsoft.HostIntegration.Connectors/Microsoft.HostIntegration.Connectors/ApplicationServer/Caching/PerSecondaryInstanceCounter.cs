using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000357 RID: 855
	internal class PerSecondaryInstanceCounter : DistributedCachePerSecodaryMachineCounter
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x0005A27D File Offset: 0x0005847D
		internal PerSecondaryInstanceCounter(DistributedCachePerSecodaryMachineCounter.Name name, string instanceName, bool toRegister)
			: base(name, instanceName, toRegister)
		{
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0005A288 File Offset: 0x00058488
		internal override long GetValue()
		{
			return this._value;
		}

		// Token: 0x17000634 RID: 1588
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x0005A290 File Offset: 0x00058490
		internal long Value
		{
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040010F4 RID: 4340
		private long _value;
	}
}
