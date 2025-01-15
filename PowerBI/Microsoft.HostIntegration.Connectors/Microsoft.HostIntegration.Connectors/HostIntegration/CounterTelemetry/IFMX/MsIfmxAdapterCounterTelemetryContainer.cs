using System;

namespace Microsoft.HostIntegration.CounterTelemetry.IFMX
{
	// Token: 0x02000600 RID: 1536
	public class MsIfmxAdapterCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x000AE1CC File Offset: 0x000AC3CC
		static MsIfmxAdapterCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(BizTalkIntegrationSubFeature));
			MsIfmxAdapterCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(6U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(InformixProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			MsIfmxAdapterCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(2U, array);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000AE22B File Offset: 0x000AC42B
		public MsIfmxAdapterCounterTelemetryContainer()
			: base(MsIfmxAdapterCounterTelemetryContainer.featureInformation, MsIfmxAdapterCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(InformixProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D71 RID: 7537
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D72 RID: 7538
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
