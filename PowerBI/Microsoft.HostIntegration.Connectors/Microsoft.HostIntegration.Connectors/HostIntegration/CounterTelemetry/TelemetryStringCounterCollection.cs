using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000609 RID: 1545
	public class TelemetryStringCounterCollection : TelemetryCounterCollection
	{
		// Token: 0x17000B54 RID: 2900
		public override uint this[string stringIndex]
		{
			get
			{
				string text = stringIndex.Trim().ToUpperInvariant();
				uint num = 0U;
				if (!this.counters.TryGetValue(text, out num))
				{
					this.counters.Add(text, 0U);
				}
				return num;
			}
			set
			{
				string text = stringIndex.Trim().ToUpperInvariant();
				if (!this.counters.ContainsKey(text))
				{
					this.counters.Add(text, 0U);
				}
				this.counters[text] = value;
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000AEAD4 File Offset: 0x000ACCD4
		internal override uint CreateSentCountersAndClear(uint featureIndex, uint subFeatureIndex, uint collectionIndex, List<SentCounterInformation> countersToSend, bool isTelemetryEnabled)
		{
			uint num = 0U;
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, uint> keyValuePair in this.counters)
			{
				if (keyValuePair.Value != 0U)
				{
					list.Add(keyValuePair.Key);
					if (isTelemetryEnabled)
					{
						StringSentCounterInformation stringSentCounterInformation = new StringSentCounterInformation(featureIndex, subFeatureIndex, collectionIndex, keyValuePair.Key, keyValuePair.Value);
						countersToSend.Add(stringSentCounterInformation);
						num += 20U + stringSentCounterInformation.ByteCount;
					}
				}
			}
			foreach (string text in list)
			{
				this.counters[text] = 0U;
			}
			return num;
		}

		// Token: 0x04001D85 RID: 7557
		private Dictionary<string, uint> counters = new Dictionary<string, uint>();
	}
}
