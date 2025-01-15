using System;

namespace Microsoft.HostIntegration.CounterTelemetry.MQ
{
	// Token: 0x02000604 RID: 1540
	public class MqClientCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003436 RID: 13366 RVA: 0x000AE39C File Offset: 0x000AC59C
		static MqClientCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(MessageIntegrationSubFeature));
			MqClientCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(5U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(QueueProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			MqClientCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000AE3FB File Offset: 0x000AC5FB
		public MqClientCounterTelemetryContainer()
			: base(MqClientCounterTelemetryContainer.featureInformation, MqClientCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(QueueProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D79 RID: 7545
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D7A RID: 7546
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
