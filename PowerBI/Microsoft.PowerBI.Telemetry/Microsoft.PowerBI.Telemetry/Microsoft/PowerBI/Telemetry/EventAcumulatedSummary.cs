using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000A RID: 10
	public class EventAcumulatedSummary
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000290F File Offset: 0x00000B0F
		public EventAcumulatedSummary()
		{
			this.eventBins = new Dictionary<int, int>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002924 File Offset: 0x00000B24
		public void AccumulateSummaryBin(int bin)
		{
			if (!this.eventBins.ContainsKey(bin))
			{
				this.eventBins.Add(bin, 1);
				return;
			}
			Dictionary<int, int> dictionary = this.eventBins;
			int num = dictionary[bin];
			dictionary[bin] = num + 1;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002966 File Offset: 0x00000B66
		public string ConvertToSummaryMessage(string eventName)
		{
			return string.Concat(new string[]
			{
				"{\"n\":\"",
				eventName,
				"\",\"v\":",
				JsonConvert.SerializeObject(this.eventBins),
				"}"
			});
		}

		// Token: 0x04000032 RID: 50
		private Dictionary<int, int> eventBins;
	}
}
