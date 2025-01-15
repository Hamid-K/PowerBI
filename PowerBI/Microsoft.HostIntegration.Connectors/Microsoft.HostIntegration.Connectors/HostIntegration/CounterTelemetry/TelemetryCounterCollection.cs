using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000606 RID: 1542
	public abstract class TelemetryCounterCollection
	{
		// Token: 0x17000B50 RID: 2896
		public virtual uint this[uint uintIndex]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B51 RID: 2897
		public virtual uint this[string stringIndex]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600344D RID: 13389
		internal abstract uint CreateSentCountersAndClear(uint featureIndex, uint subFeatureIndex, uint collectionIndex, List<SentCounterInformation> countersToSend, bool isTelemetryEnabled);
	}
}
