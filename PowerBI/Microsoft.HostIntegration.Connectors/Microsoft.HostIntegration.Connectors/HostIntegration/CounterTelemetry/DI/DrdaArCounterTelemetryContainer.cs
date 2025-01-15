using System;

namespace Microsoft.HostIntegration.CounterTelemetry.DI
{
	// Token: 0x020005FA RID: 1530
	public class DrdaArCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003417 RID: 13335 RVA: 0x000ADE94 File Offset: 0x000AC094
		static DrdaArCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(DataIntegrationSubFeature));
			DrdaArCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(2U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[Enum.GetValues(typeof(DrdArCounterCollection)).Length];
			array[0] = new TelemetryStringCounterCollection();
			array[1] = new TelemetryStringCounterCollection();
			Array values2 = Enum.GetValues(typeof(DrdaArProcess));
			array[2] = new TelemetryEnumCounterCollection((uint)values2.Length);
			DrdaArCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(1U, array);
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000ADF16 File Offset: 0x000AC116
		public DrdaArCounterTelemetryContainer()
			: base(DrdaArCounterTelemetryContainer.featureInformation, DrdaArCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000ADE80 File Offset: 0x000AC080
		public void Increment(DrdArCounterCollection counterCollection, DrdaAsProcess counterIdentifier)
		{
			base.Increment((uint)counterCollection, (uint)counterIdentifier);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000ADE8A File Offset: 0x000AC08A
		public void Increment(DrdArCounterCollection counterCollection, string counterIdentifier)
		{
			base.Increment((uint)counterCollection, counterIdentifier);
		}

		// Token: 0x04001D65 RID: 7525
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D66 RID: 7526
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
