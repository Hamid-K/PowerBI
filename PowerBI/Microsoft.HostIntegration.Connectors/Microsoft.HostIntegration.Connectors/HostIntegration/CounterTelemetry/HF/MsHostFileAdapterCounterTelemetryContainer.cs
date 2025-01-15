using System;

namespace Microsoft.HostIntegration.CounterTelemetry.HF
{
	// Token: 0x02000601 RID: 1537
	public class MsHostFileAdapterCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x000AE240 File Offset: 0x000AC440
		static MsHostFileAdapterCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(BizTalkIntegrationSubFeature));
			MsHostFileAdapterCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(6U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(HostFileProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			MsHostFileAdapterCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(3U, array);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000AE29F File Offset: 0x000AC49F
		public MsHostFileAdapterCounterTelemetryContainer()
			: base(MsHostFileAdapterCounterTelemetryContainer.featureInformation, MsHostFileAdapterCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(HostFileProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D73 RID: 7539
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D74 RID: 7540
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
