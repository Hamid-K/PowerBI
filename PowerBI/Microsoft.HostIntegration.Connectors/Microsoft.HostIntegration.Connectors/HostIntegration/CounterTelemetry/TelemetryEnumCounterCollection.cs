using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000607 RID: 1543
	public class TelemetryEnumCounterCollection : TelemetryCounterCollection
	{
		// Token: 0x17000B52 RID: 2898
		public override uint this[uint uintIndex]
		{
			get
			{
				return this.counters[(int)uintIndex];
			}
			set
			{
				this.counters[(int)uintIndex] = value;
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000AE89F File Offset: 0x000ACA9F
		public TelemetryEnumCounterCollection(uint numberOfCounters)
		{
			this.counters = new uint[numberOfCounters];
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000AE8B4 File Offset: 0x000ACAB4
		internal override uint CreateSentCountersAndClear(uint featureIndex, uint subFeatureIndex, uint collectionIndex, List<SentCounterInformation> countersToSend, bool isTelemetryEnabled)
		{
			uint num = 0U;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this.counters.Length))
			{
				if (this.counters[(int)num2] != 0U)
				{
					if (isTelemetryEnabled)
					{
						countersToSend.Add(new IntegerSentCounterInformation(featureIndex, subFeatureIndex, collectionIndex, num2, this.counters[(int)num2]));
						num += 20U;
					}
					this.counters[(int)num2] = 0U;
				}
				num2 += 1U;
			}
			return num;
		}

		// Token: 0x04001D83 RID: 7555
		private uint[] counters;
	}
}
