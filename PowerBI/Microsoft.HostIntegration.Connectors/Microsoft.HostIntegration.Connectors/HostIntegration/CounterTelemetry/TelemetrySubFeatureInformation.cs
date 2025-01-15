using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x0200060C RID: 1548
	public class TelemetrySubFeatureInformation
	{
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000AEBF0 File Offset: 0x000ACDF0
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x000AEBF8 File Offset: 0x000ACDF8
		public uint SubFeatureEnum { get; private set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000AEC01 File Offset: 0x000ACE01
		// (set) Token: 0x06003462 RID: 13410 RVA: 0x000AEC09 File Offset: 0x000ACE09
		public TelemetryCounterCollection[] CounterCollections { get; private set; }

		// Token: 0x06003463 RID: 13411 RVA: 0x000AEC12 File Offset: 0x000ACE12
		public TelemetrySubFeatureInformation(uint subFeatureEnum, TelemetryCounterCollection[] counterCollections)
		{
			this.SubFeatureEnum = subFeatureEnum;
			this.CounterCollections = counterCollections;
		}
	}
}
