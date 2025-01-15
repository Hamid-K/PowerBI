using System;

namespace Microsoft.HostIntegration.CounterTelemetry.SI
{
	// Token: 0x020005FC RID: 1532
	public class Lu0CounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x0600341E RID: 13342 RVA: 0x000ADFA4 File Offset: 0x000AC1A4
		static Lu0CounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(SessionIntegrationSubFeature));
			Lu0CounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(4U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(Lu0Process));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			Lu0CounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000AE003 File Offset: 0x000AC203
		public Lu0CounterTelemetryContainer()
			: base(Lu0CounterTelemetryContainer.featureInformation, Lu0CounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(Lu0Process counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D69 RID: 7529
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D6A RID: 7530
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
