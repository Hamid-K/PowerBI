using System;

namespace Microsoft.HostIntegration.CounterTelemetry.DI
{
	// Token: 0x020005F9 RID: 1529
	public class DrdaAsCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003413 RID: 13331 RVA: 0x000ADDEC File Offset: 0x000ABFEC
		static DrdaAsCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(DataIntegrationSubFeature));
			DrdaAsCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(2U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[Enum.GetValues(typeof(DrdAsCounterCollection)).Length];
			array[0] = new TelemetryStringCounterCollection();
			array[1] = new TelemetryStringCounterCollection();
			Array values2 = Enum.GetValues(typeof(DrdaAsProcess));
			array[2] = new TelemetryEnumCounterCollection((uint)values2.Length);
			DrdaAsCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000ADE6E File Offset: 0x000AC06E
		public DrdaAsCounterTelemetryContainer()
			: base(DrdaAsCounterTelemetryContainer.featureInformation, DrdaAsCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000ADE80 File Offset: 0x000AC080
		public void Increment(DrdAsCounterCollection counterCollection, DrdaAsProcess counterIdentifier)
		{
			base.Increment((uint)counterCollection, (uint)counterIdentifier);
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000ADE8A File Offset: 0x000AC08A
		public void Increment(DrdAsCounterCollection counterCollection, string counterIdentifier)
		{
			base.Increment((uint)counterCollection, counterIdentifier);
		}

		// Token: 0x04001D63 RID: 7523
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D64 RID: 7524
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
