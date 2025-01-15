using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x0200060D RID: 1549
	public class TelemetryFeature
	{
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000AEC28 File Offset: 0x000ACE28
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000AEC30 File Offset: 0x000ACE30
		public TelemetrySubFeature[] SubFeatures { get; private set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000AEC39 File Offset: 0x000ACE39
		public uint NumberOfSubFeatures
		{
			get
			{
				return (uint)this.SubFeatures.Length;
			}
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000AEC43 File Offset: 0x000ACE43
		public TelemetryFeature(TelemetryFeatureInformation featureInformation)
		{
			this.SubFeatures = new TelemetrySubFeature[featureInformation.SubFeatureCount];
		}
	}
}
