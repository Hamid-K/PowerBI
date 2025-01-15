using System;

namespace Microsoft.HostIntegration.CounterTelemetry.MQ
{
	// Token: 0x02000603 RID: 1539
	public class MqscAdapterCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x000AE328 File Offset: 0x000AC528
		static MqscAdapterCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(BizTalkIntegrationSubFeature));
			MqscAdapterCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(6U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(MqscProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			MqscAdapterCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000AE387 File Offset: 0x000AC587
		public MqscAdapterCounterTelemetryContainer()
			: base(MqscAdapterCounterTelemetryContainer.featureInformation, MqscAdapterCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(MqscProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D77 RID: 7543
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D78 RID: 7544
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
