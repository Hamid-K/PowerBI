using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000608 RID: 1544
	public class TelemetryIntegerCounterCollection : TelemetryCounterCollection
	{
		// Token: 0x17000B53 RID: 2899
		public override uint this[uint uintIndex]
		{
			get
			{
				return this.counters[uintIndex];
			}
			set
			{
				this.counters[uintIndex] = value;
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000AE92C File Offset: 0x000ACB2C
		public TelemetryIntegerCounterCollection(uint[] identifiers)
		{
			foreach (uint num in identifiers)
			{
				this.counters.Add(num, 0U);
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000AE96C File Offset: 0x000ACB6C
		internal override uint CreateSentCountersAndClear(uint featureIndex, uint subFeatureIndex, uint collectionIndex, List<SentCounterInformation> countersToSend, bool isTelemetryEnabled)
		{
			uint num = 0U;
			List<uint> list = new List<uint>();
			foreach (KeyValuePair<uint, uint> keyValuePair in this.counters)
			{
				if (keyValuePair.Value != 0U)
				{
					list.Add(keyValuePair.Key);
					if (isTelemetryEnabled)
					{
						countersToSend.Add(new IntegerSentCounterInformation(featureIndex, subFeatureIndex, collectionIndex, keyValuePair.Key, keyValuePair.Value));
						num += 20U;
					}
				}
			}
			foreach (uint num2 in list)
			{
				this.counters[num2] = 0U;
			}
			return num;
		}

		// Token: 0x04001D84 RID: 7556
		private Dictionary<uint, uint> counters = new Dictionary<uint, uint>();
	}
}
