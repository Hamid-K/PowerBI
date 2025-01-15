using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x0200060E RID: 1550
	public class TelemetrySubFeature
	{
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000AEC5C File Offset: 0x000ACE5C
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000AEC64 File Offset: 0x000ACE64
		public TelemetryCounterCollection[] CounterCollections { get; private set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000AEC6D File Offset: 0x000ACE6D
		public uint NumberOfCounterCollections
		{
			get
			{
				return (uint)this.CounterCollections.Length;
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000AEC77 File Offset: 0x000ACE77
		public TelemetrySubFeature(TelemetrySubFeatureInformation subFeatureInformation)
		{
			this.CounterCollections = subFeatureInformation.CounterCollections;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000AEC8C File Offset: 0x000ACE8C
		internal uint CreateSentCountersAndClear(uint featureIndex, uint subFeatureIndex, List<SentCounterInformation> countersToSend, bool isTelemetryEnabled)
		{
			uint num = 0U;
			for (uint num2 = 0U; num2 < this.NumberOfCounterCollections; num2 += 1U)
			{
				num += this.CounterCollections[(int)num2].CreateSentCountersAndClear(featureIndex, subFeatureIndex, num2, countersToSend, isTelemetryEnabled);
			}
			return num;
		}
	}
}
