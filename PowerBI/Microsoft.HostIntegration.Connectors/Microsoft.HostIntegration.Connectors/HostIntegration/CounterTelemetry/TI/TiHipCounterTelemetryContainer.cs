using System;

namespace Microsoft.HostIntegration.CounterTelemetry.TI
{
	// Token: 0x020005FE RID: 1534
	public class TiHipCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x000AE08C File Offset: 0x000AC28C
		static TiHipCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(ApplicationIntegrationSubFeature));
			TiHipCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(3U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(HipProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			TiHipCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(1U, array);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000AE0EB File Offset: 0x000AC2EB
		public TiHipCounterTelemetryContainer()
			: base(TiHipCounterTelemetryContainer.featureInformation, TiHipCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(HipProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D6D RID: 7533
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D6E RID: 7534
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
