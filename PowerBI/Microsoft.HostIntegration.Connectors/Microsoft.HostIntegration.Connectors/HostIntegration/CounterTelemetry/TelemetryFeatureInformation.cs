using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x0200060B RID: 1547
	public class TelemetryFeatureInformation
	{
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000AEBB8 File Offset: 0x000ACDB8
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000AEBC0 File Offset: 0x000ACDC0
		public uint FeatureEnum { get; private set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000AEBC9 File Offset: 0x000ACDC9
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x000AEBD1 File Offset: 0x000ACDD1
		public uint SubFeatureCount { get; private set; }

		// Token: 0x0600345E RID: 13406 RVA: 0x000AEBDA File Offset: 0x000ACDDA
		public TelemetryFeatureInformation(uint featureEnum, uint subFeatureCount)
		{
			this.FeatureEnum = featureEnum;
			this.SubFeatureCount = subFeatureCount;
		}
	}
}
