using System;

namespace Microsoft.HostIntegration.CounterTelemetry.SI
{
	// Token: 0x020005FD RID: 1533
	public class Lu2CounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003421 RID: 13345 RVA: 0x000AE018 File Offset: 0x000AC218
		static Lu2CounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(SessionIntegrationSubFeature));
			Lu2CounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(4U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(Lu2Process));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			Lu2CounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(1U, array);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000AE077 File Offset: 0x000AC277
		public Lu2CounterTelemetryContainer()
			: base(Lu2CounterTelemetryContainer.featureInformation, Lu2CounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(Lu2Process counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D6B RID: 7531
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D6C RID: 7532
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
