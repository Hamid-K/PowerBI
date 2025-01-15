using System;

namespace Microsoft.HostIntegration.CounterTelemetry.DB
{
	// Token: 0x02000602 RID: 1538
	public class MsDb2AdapterCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003430 RID: 13360 RVA: 0x000AE2B4 File Offset: 0x000AC4B4
		static MsDb2AdapterCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(BizTalkIntegrationSubFeature));
			MsDb2AdapterCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(6U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(MsDb2Process));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			MsDb2AdapterCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(1U, array);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000AE313 File Offset: 0x000AC513
		public MsDb2AdapterCounterTelemetryContainer()
			: base(MsDb2AdapterCounterTelemetryContainer.featureInformation, MsDb2AdapterCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(MsDb2Process counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D75 RID: 7541
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D76 RID: 7542
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
